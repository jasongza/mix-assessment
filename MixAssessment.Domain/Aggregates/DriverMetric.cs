using MixAssessment.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace MixAssessment.Domain.Aggregates
{
    public class DriverMetric
    {
        public Driver Driver { get; }
        public ICollection<Trip> Trips { get; }
        public float AverageSpeed { get; private set; }

        public DriverMetric(Driver driver, ICollection<Trip> trips)
        {
            Driver = driver;
            Trips = trips;
        }

        public float CalculateAverageSpeed()
        {
            AverageSpeed = Trips.Count > 0 ? Trips
                .Select(trip => new
                {
                    trip.Distance,
                    TotalHours = (float)(trip.TripEnd - trip.TripStart).TotalMilliseconds / 1000 / 60 / 60
                })
                .Select(trip => trip.Distance / trip.TotalHours)
                .Average() : 0;

            return AverageSpeed;
        }
    }
}
