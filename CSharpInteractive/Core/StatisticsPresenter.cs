// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable RedundantExplicitParamsArrayCreation
namespace CSharpInteractive.Core;

using HostApi;

[ExcludeFromCodeCoverage]
internal class StatisticsPresenter(ILog<StatisticsPresenter> log) : IPresenter<IStatistics>
{
    private static readonly Text[] Tab = [Text.Tab];

    public void Show(IStatistics statistics)
    {
        var tests = statistics.CommandLines
            .Select(i => i.CommandLineResult)
            .OfType<IBuildResult>()
            .SelectMany(i => i.Tests);

        Show(tests);

        var warningsCount = 0;
        var errorsCount = 0;
        var statisticsItems = statistics.Items;
        foreach (var item in statisticsItems.OrderBy(i => i.Type))
        {
            // ReSharper disable once SwitchStatementMissingSomeEnumCasesNoDefault
            switch (item.Type)
            {
                case StatisticsType.Warning:
                    warningsCount++;
                    break;

                case StatisticsType.Error:
                    errorsCount++;
                    break;
            }

            log.Info(item.Text.AddPrefix(_ => Tab));
        }

        if (warningsCount > 0)
        {
            log.Info(new Text($"{warningsCount} Warning{ (warningsCount > 1 ? "s" : "") }", Color.Warning));
        }

        if (errorsCount > 0)
        {
            log.Info(new Text($"{errorsCount} Error{ (errorsCount > 1 ? "s" : "") }", Color.Error));
        }

        log.Info(new Text($"Time Elapsed {statistics.TimeElapsed:g}"));
    }

    private void Show(IEnumerable<TestResult> tests)
    {
        var testText = tests
            .Where(i => i.State is TestState.Failed or TestState.Ignored)
            .GroupBy(i => (i.State, i.Name))
            .OrderBy(i => i.Key.State)
            .ThenBy(i => i.Key.Name)
            .Select(i => FormatTests(i.ToList()));

        foreach (var text in testText)
        {
            log.Info(text.AddPrefix(_ => Tab));
        }
    }

    private static Text[] FormatTests(IReadOnlyCollection<TestResult> tests)
    {
        var test = tests.First();
        var stateColor = test.State switch
        {
            TestState.Finished => Color.Success,
            TestState.Failed => Color.Error,
            TestState.Ignored => Color.Warning,
            _ => Color.Default
        };

        var text = new List<Text>
        {
            new(test.State.ToString(), stateColor)
        };

        var name = GetName(test.Name, test.ResultDisplayName, test.FullyQualifiedName, test.DisplayName, $"Test {test.Id}");
        text.Add(Text.Space);
        text.Add(new Text(name));
        if (test.State != TestState.Failed)
        {
            return text.ToArray();
        }

        var groups = tests.GroupBy(i => i.Message.TrimEnd());
        foreach (var group in groups)
        {
            var count = group.Count();
            if (count > 1)
            {
                text.Add(Text.NewLine);
                text.Add(Text.Tab);
                text.Add(new Text($"{count} times", Color.Trace));
            }

            var message = group.Key.TrimEnd();
            if (string.IsNullOrWhiteSpace(message))
            {
                continue;
            }

            text.Add(Text.NewLine);
            text.AddRange(message.TrimEnd().SplitLines(maxLines: 3).AddPrefix(_ => Tab));
        }

        return text.ToArray();

        string GetName(params string[] names) => names.FirstOrDefault(i => !string.IsNullOrWhiteSpace(i)) ?? "";
    }
}