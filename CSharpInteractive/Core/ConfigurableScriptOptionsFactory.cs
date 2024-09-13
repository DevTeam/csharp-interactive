// ReSharper disable ClassNeverInstantiated.Global

namespace CSharpInteractive.Core;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;

internal class ConfigurableScriptOptionsFactory(
    ISettingGetter<LanguageVersion> languageVersion,
    ISettingGetter<OptimizationLevel> optimizationLevel,
    ISettingGetter<WarningLevel> warningLevel,
    ISettingGetter<CheckOverflow> checkOverflow,
    ISettingGetter<AllowUnsafe> allowUnsafe)
    : IScriptOptionsFactory
{
    public ScriptOptions Create(ScriptOptions baseOptions) =>
        baseOptions
            .WithLanguageVersion(languageVersion.GetSetting())
            .WithOptimizationLevel(optimizationLevel.GetSetting())
            .WithWarningLevel((int)warningLevel.GetSetting())
            .WithCheckOverflow(checkOverflow.GetSetting() == CheckOverflow.On)
            .WithAllowUnsafe(allowUnsafe.GetSetting() == AllowUnsafe.On);
}