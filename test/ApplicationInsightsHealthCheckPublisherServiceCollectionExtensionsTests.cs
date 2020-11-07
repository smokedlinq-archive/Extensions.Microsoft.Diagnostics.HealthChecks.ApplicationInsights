using A3;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using Xunit;

namespace Extensions.Microsoft.Diagnostics.HealthChecks.ApplicationInsights.Tests
{
    public class ApplicationInsightsHealthCheckPublisherServiceCollectionExtensionsTests
    {
        [Fact]
        public void AddApplicationInsightsPublisherShouldRegisterIHealthCheckPublisher()
            => A3<ServiceCollection>
            .Arrange(setup =>
            {
                var services = new ServiceCollection();
                services.AddOptions();
                setup.Sut(services);
            })
            .Act(sut => { sut.AddHealthChecks().AddApplicationInsightsPublisher(); })
            .Assert(context => context.Sut.Any(x => x.ServiceType == typeof(IHealthCheckPublisher) && x.ImplementationType == typeof(ApplicationInsightsHealthCheckPublisher)).Should().BeTrue());

        [Fact]
        public void AfterAddApplicationInsightsPublisherShouldBeAbleToGetApplicationInsightsHealthCheckPublisherOptions()
            => A3<IServiceProvider>
            .Arrange(setup =>
            {
                var services = new ServiceCollection();
                services.AddSingleton<IConfiguration>(new ConfigurationBuilder().Build());
                services.AddOptions();
                services.AddHealthChecks().AddApplicationInsightsPublisher();
                setup.Sut(services.BuildServiceProvider());
            })
            .Act(sut => sut.GetService<IOptions<ApplicationInsightsHealthCheckPublisherOptions>>())
            .Assert(result => result?.Value.Should().NotBeNull());
    }
}
