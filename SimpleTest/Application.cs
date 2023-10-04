using Microsoft.Extensions.Logging;
using SimpleTest.Services;

namespace SimpleTest;

public class Application
{
    private readonly ILogger<Application> _logger;
    private readonly IAnalysisService _analysisService;

    public Application(ILogger<Application> logger, IAnalysisService analysisService)
    {
        _logger = logger;
        _analysisService = analysisService;
    }

    public string Run(string[] args)
    {
        var output = _analysisService.Analyse(GetInputString(args));
        _logger.LogInformation("Output: {output}", output);
        return output;
    }

    private string? GetInputString(IList<string> args)
    {
        if (args.Any())
        {
            return string.Join(' ', args);
        }

        Console.WriteLine("Enter string to analyse");
        return Console.ReadLine();
    }
}
