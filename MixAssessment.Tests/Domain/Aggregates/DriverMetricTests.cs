using MixAssessment.Domain.Aggregates;
using MixAssessment.Domain.Entities;
using System;
using System.Collections.Generic;
using Xunit;

namespace MixAssessment.Tests.Domain.Aggregates
{
    public class DriverMetricTests
    {
        [Fact]
        public void Aggregate_WithoutTrips_ReturnsZeroResults()
        {
            var sut = new DriverMetric(new Driver(1, "One"), new List<Trip>());

            var result = sut.CalculateAverageSpeed();

            Assert.Equal(0, result);
        }

        [Fact]
        public void Aggregate_WithTrips_ReturnsExpectedResults()
        {
            var sut = new DriverMetric(new Driver(1, "One"), new List<Trip>
            {
                new Trip(11, DateTime.Now.AddHours(-24), 12345, DateTime.Now.AddHours(-22), 23456, 80),
                new Trip(12, DateTime.Now.AddHours(-24), 12345, DateTime.Now.AddHours(-22), 23456, 90),
            });

            var result = sut.CalculateAverageSpeed();

            Assert.Equal(42.5, result);
        }
    }
}
