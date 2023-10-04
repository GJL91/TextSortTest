using Microsoft.Extensions.DependencyInjection;
using SimpleTest.Services;
using SimpleTest.Utils;

namespace SimpleTest.Extensions;

public static class DependencyExtensions
{
    public static IServiceCollection RegisterDependencies(this IServiceCollection services)
    {
        return services
            .AddSingleton<Application>()
            .AddSingleton<IAnalysisService, AnalysisService>()
            .AddSingleton<IWordComparer, WordComparer>();
    }
}
