// ReSharper disable ClassNeverInstantiated.Global

namespace CSharpInteractive.Core;

using HostApi;
using NuGet.Commands;
using NuGet.Common;
using NuGet.Configuration;
using NuGet.Frameworks;
using NuGet.LibraryModel;
using NuGet.ProjectModel;
using NuGet.Protocol;
using NuGet.Protocol.Core.Types;
using NuGet.Versioning;

[ExcludeFromCodeCoverage]
internal class NuGetRestoreService : INuGetRestoreService, ISettingSetter<NuGetRestoreSetting>
{
    private const string ProjectName = "restore";
    private readonly ILog<NuGetRestoreService> _log;
    private readonly ILogger _logger;
    private readonly IUniqueNameGenerator _uniqueNameGenerator;
    private readonly IEnvironment _environment;
    private readonly IDotNetEnvironment _dotnetEnvironment;
    private readonly ITargetFrameworkMonikerParser _targetFrameworkMonikerParser;

    private bool _restoreDisableParallel;
    private bool _restoreIgnoreFailedSources;
    private bool _hideWarningsAndErrors;
    private bool _restoreNoCache;

    public NuGetRestoreService(
        ILog<NuGetRestoreService> log,
        ILogger logger,
        IUniqueNameGenerator uniqueNameGenerator,
        IEnvironment environment,
        IDotNetEnvironment dotnetEnvironment,
        ITargetFrameworkMonikerParser targetFrameworkMonikerParser)
    {
        _log = log;
        _logger = logger;
        _uniqueNameGenerator = uniqueNameGenerator;
        _environment = environment;
        _dotnetEnvironment = dotnetEnvironment;
        _targetFrameworkMonikerParser = targetFrameworkMonikerParser;
        SetSetting(NuGetRestoreSetting.Default);
    }

    public bool TryRestore(NuGetRestoreSettings settings, out string projectAssetsJson)
    {
        var tempDirectory = _environment.GetPath(SpecialFolder.Temp);
        var outputPath = Path.Combine(tempDirectory, _uniqueNameGenerator.Generate());
        Directory.CreateDirectory(outputPath);
        var tfm = settings.TargetFrameworkMoniker ?? _dotnetEnvironment.TargetFrameworkMoniker;
        var targetFrameworkMoniker = _targetFrameworkMonikerParser.Parse(tfm);
        var framework = NuGetFramework.ParseFrameworkName(targetFrameworkMoniker, DefaultFrameworkNameProvider.Instance);
        var projectStyle = settings.PackageType switch
        {
            NuGetPackageType.Tool => ProjectStyle.DotnetToolReference,
            _ => ProjectStyle.PackageReference
        };

        projectAssetsJson = Path.Combine(outputPath, "project.assets.json");
        var projectFile = Path.Combine(outputPath, $"{ProjectName}.csproj");

        _log.Trace(() => [new Text($"Restore nuget package {settings.PackageId} {settings.VersionRange} to \"{outputPath}\" and \"{settings.PackagesPath}\".")]);

        var dependency = new LibraryDependency
        {
            LibraryRange = new LibraryRange(
                settings.PackageId,
                settings.VersionRange ?? VersionRange.Parse("*"),
                LibraryDependencyTarget.Package),
            IncludeType = LibraryIncludeFlags.All
        };

        var packageSpec = new PackageSpec(
            [
                new TargetFrameworkInformation
                {
                    FrameworkName = framework,
                    TargetAlias = tfm,
                    Dependencies = [dependency]
                }
            ])
        {
            Name = ProjectName,
            FilePath = projectFile,
            RestoreMetadata = new ProjectRestoreMetadata
            {
                ProjectStyle = projectStyle,
                ProjectName = ProjectName,
                ProjectUniqueName = projectFile,
                ProjectPath = projectFile,
                OutputPath = outputPath,
                OriginalTargetFrameworks = [tfm],
                Sources = settings.Sources.Select(s => new PackageSource(s)).ToList(),
                FallbackFolders = settings.FallbackFolders.ToList(),
                PackagesPath = settings.PackagesPath ?? string.Empty,
                ConfigFilePaths = [],
                ValidateRuntimeAssets = false,
                TargetFrameworks =
                [
                    new ProjectRestoreMetadataFrameworkInfo(framework) { TargetAlias = tfm }
                ]
            }
        };

        var noCache = settings.NoCache ?? _restoreNoCache;
        var ignoreFailedSources = settings.IgnoreFailedSources ?? _restoreIgnoreFailedSources;
        var disableParallel = settings.DisableParallel ?? _restoreDisableParallel;
        var hideWarningsAndErrors = settings.HideWarningsAndErrors ?? _hideWarningsAndErrors;

        using var cacheContext = new SourceCacheContext
        {
            NoCache = noCache,
            DirectDownload = false,
            IgnoreFailedSources = ignoreFailedSources
        };

        var sourceRepositories = settings.Sources
            .Select(source => Repository.Factory.GetCoreV3(source))
            .ToList();

        var nugetSettings = NuGet.Configuration.Settings.LoadDefaultSettings(_environment.GetPath(SpecialFolder.Script));
        var globalFolderPath = string.IsNullOrWhiteSpace(settings.PackagesPath)
            ? SettingsUtility.GetGlobalPackagesFolder(nugetSettings)
            : settings.PackagesPath;

        var providers = new RestoreCommandProvidersCache().GetOrCreate(
            globalFolderPath,
            settings.FallbackFolders.ToList(),
            sourceRepositories,
            cacheContext,
            _logger);

        var request = new RestoreRequest(
            packageSpec,
            providers,
            cacheContext,
            clientPolicyContext: null,
            packageSourceMapping: PackageSourceMapping.GetPackageSourceMapping(nugetSettings),
            _logger,
            new LockFileBuilderCache())
        {
            LockFilePath = projectAssetsJson,
            ProjectStyle = projectStyle,
            AllowNoOp = false,
            HideWarningsAndErrors = hideWarningsAndErrors,
            RestoreForceEvaluate = false,
            CacheContext = cacheContext
        };

        if (disableParallel)
        {
            request.MaxDegreeOfConcurrency = 1;
        }

        try
        {
            var command = new RestoreCommand(request);
            var result = command.ExecuteAsync(CancellationToken.None).GetAwaiter().GetResult();
            if (result.Success)
            {
                result.CommitAsync(_logger, CancellationToken.None).GetAwaiter().GetResult();
            }

            return result.Success;
        }
        catch (Exception ex)
        {
            _log.Error(ErrorId.NuGet, ex.Message);
            return false;
        }
    }

    public NuGetRestoreSetting SetSetting(NuGetRestoreSetting value)
    {
        var prevVal = NuGetRestoreSetting.Default;
        switch (value)
        {
            case NuGetRestoreSetting.Default:
                _restoreDisableParallel = false;
                _restoreIgnoreFailedSources = false;
                _hideWarningsAndErrors = false;
                _restoreNoCache = false;
                break;

            case NuGetRestoreSetting.Parallel:
                prevVal = _restoreDisableParallel ? NuGetRestoreSetting.Parallel : NuGetRestoreSetting.NonParallel;
                _restoreDisableParallel = true;
                break;

            case NuGetRestoreSetting.NonParallel:
                prevVal = _restoreDisableParallel ? NuGetRestoreSetting.Parallel : NuGetRestoreSetting.NonParallel;
                _restoreDisableParallel = false;
                break;

            case NuGetRestoreSetting.IgnoreFailedSources:
                prevVal = _restoreIgnoreFailedSources ? NuGetRestoreSetting.IgnoreFailedSources : NuGetRestoreSetting.ConsiderFailedSources;
                _restoreIgnoreFailedSources = true;
                break;

            case NuGetRestoreSetting.ConsiderFailedSources:
                prevVal = _restoreIgnoreFailedSources ? NuGetRestoreSetting.IgnoreFailedSources : NuGetRestoreSetting.ConsiderFailedSources;
                _restoreIgnoreFailedSources = false;
                break;

            case NuGetRestoreSetting.HideWarningsAndErrors:
                prevVal = _hideWarningsAndErrors ? NuGetRestoreSetting.HideWarningsAndErrors : NuGetRestoreSetting.ShowWarningsAndErrors;
                _hideWarningsAndErrors = true;
                break;

            case NuGetRestoreSetting.ShowWarningsAndErrors:
                prevVal = _hideWarningsAndErrors ? NuGetRestoreSetting.HideWarningsAndErrors : NuGetRestoreSetting.ShowWarningsAndErrors;
                _hideWarningsAndErrors = false;
                break;

            case NuGetRestoreSetting.NoCache:
                prevVal = _restoreNoCache ? NuGetRestoreSetting.NoCache : NuGetRestoreSetting.WithCache;
                _restoreNoCache = true;
                break;

            case NuGetRestoreSetting.WithCache:
                prevVal = _restoreNoCache ? NuGetRestoreSetting.NoCache : NuGetRestoreSetting.WithCache;
                _restoreNoCache = false;
                break;

            default:
                throw new ArgumentOutOfRangeException(nameof(value), value, null);
        }

        return prevVal;
    }
}
