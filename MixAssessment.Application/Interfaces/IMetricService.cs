using MixAssessment.Domain.Aggregates;
using System.Collections.Generic;

namespace MixAssessment.Application.Interfaces
{
    public interface IMetricService
    {
        IEnumerable<DriverMetric> GetDriverMetrics();
    }
}
