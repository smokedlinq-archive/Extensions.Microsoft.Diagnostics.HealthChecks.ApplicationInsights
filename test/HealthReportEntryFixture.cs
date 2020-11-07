using AutoFixture;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.Collections.Generic;

namespace Extensions.Microsoft.Diagnostics.HealthChecks.ApplicationInsights.Tests
{
    public class HealthReportEntryFixture : ICustomizeFixture<HealthReportEntry>
    {
        public HealthReportEntry Customize(IFixture fixture)
            => new HealthReportEntry(fixture.Create<HealthStatus>(), fixture.Create<string>(), fixture.Create<TimeSpan>(), null, new Dictionary<string, object>());
    }
}
