using Microsoft.Extensions.Logging;
using SimpleTest.Utils;
using System.Text.RegularExpressions;

namespace SimpleTest.Services;

public class AnalysisService : IAnalysisService
{
    private readonly ILogger<AnalysisService> _logger;
    private readonly IWordComparer _wordComparer;

    public AnalysisService(ILogger<AnalysisService> logger, IWordComparer wordComparer)
    {
        _logger = logger;
        _wordComparer = wordComparer;
    }

    public string Analyse(string? input)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            _logger.LogCritical("Input data not provided");
            throw new ArgumentOutOfRangeException(nameof(input), "Input data not provided");
        }

        _logger.LogInformation("[Analyse] - Starting analysis of text '{input}'", input);

        var words = SplitIntoWords(RemovePunctuation(input));
        var output = ReorderWords(words);

        _logger.LogInformation("[Analyse] - Analysis complete");
        _logger.LogDebug("[Analyse] - Analysed text '{input}' became '{output}'", input, output);
        return output;
    }

    private string RemovePunctuation(string input)
    {
        var regex = new Regex("[.,;']");
        return regex.Replace(input, "");
    }

    private IEnumerable<string> SplitIntoWords(string input)
    {
        return Regex.Split(input, "\\s");
    }

    private string ReorderWords(IEnumerable<string> input)
    {
        var orderedWords = input.Where(x => !string.IsNullOrWhiteSpace(x))
            .OrderBy(x => x, _wordComparer);
        return string.Join(' ', orderedWords);
    }
}
