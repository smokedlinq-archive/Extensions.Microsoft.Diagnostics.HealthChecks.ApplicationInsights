using A3;
using AutoFixture;
using FluentAssertions;
using Microsoft.ApplicationInsights;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks.ApplicationInsights;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading;
using Xunit;

namespace Extensions.Microsoft.Diagnostics.HealthChecks.ApplicationInsights.Tests
{
    public class ApplicationInsightsHealthCheckPublisherTests
    {
        [Theory]
        [A3Data]
        public void PublishAsyncDoesNotThrow(TelemetryClient client)
            => A3<ApplicationInsightsHealthCheckPublisher>
            .Arrange<HealthReport>(setup =>
            {
                var entries = new Dictionary<string, HealthReportEntry>
                {
                    [setup.Fixture.Create<string>()] = setup.Fixture.Create<HealthReportEntry>()
                };
                setup.Sut(new ApplicationInsightsHealthCheckPublisher(client, Options.Create(new ApplicationInsightsHealthCheckPublisherOptions())));
                return new HealthReport(entries, setup.Fixture.Create<TimeSpan>());
            })
            .Act((sut, report) => sut.Invoking(x => x.PublishAsync(report, CancellationToken.None)))
            .Assert(result => result.Should().NotThrow());
    }
}
