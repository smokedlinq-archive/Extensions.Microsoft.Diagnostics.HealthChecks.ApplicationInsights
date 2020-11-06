using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.Reflection;

namespace Extensions.Microsoft.Diagnostics.HealthChecks.ApplicationInsights
{
    public static class ApplicationInsightsHealthCheckPublisherServiceCollectionExtensions
    {
        public static IHealthChecksBuilder AddApplicationInsightsPublisher(this IHealthChecksBuilder builder, Action<ApplicationInsightsHealthCheckPublisherOptions>? configure = null)
        {
            _ = builder ?? throw new ArgumentNullException(nameof(builder));
            builder.Services.AddOptions<ApplicationInsightsHealthCheckPublisherOptions>()
                .Configure<IConfiguration>((options, configuration) => configuration.GetSection(nameof(ApplicationInsightsHealthCheckPublisher)).Bind(options))
                .Configure(options =>
                {
                    options.RunLocation = Environment.MachineName;
                    options.ApplicationName = Assembly.GetEntryAssembly().GetName().Name;
                    options.EnvironmentName = Environment.MachineName;
                    configure?.Invoke(options);
                });
            builder.Services.AddSingleton<IHealthCheckPublisher, ApplicationInsightsHealthCheckPublisher>();
            return builder;
        }
    }
}
