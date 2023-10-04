using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SimpleTest;
using SimpleTest.Extensions;

var hostBuilder = Host.CreateApplicationBuilder(args);
hostBuilder.Logging.AddConsole();
hostBuilder.Services.RegisterDependencies();

using var host = hostBuilder.Build();
await host.StartAsync();

var application = host.Services.GetRequiredService<Application>();
application.Run(args);
