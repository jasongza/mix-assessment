using System;

namespace MixAssessment.Domain.Entities
{
    public class Trip : IEntity
    {
        public short VehicleId { get; }
        public DateTime TripStart { get; }
        public float OdometerStart { get; }
        public DateTime TripEnd { get; }
        public long TripStartTicks { get; }
        public long TripEndTicks { get; }
        public float OdometerEnd { get; }
        public float Distance { get; }

        public Trip(short vehicleId, DateTime tripStart, float odometerStart, DateTime tripEnd, float odometerEnd, float distance)
        {
            VehicleId = vehicleId;
            OdometerStart = odometerStart;
            OdometerEnd = odometerEnd;
            Distance = distance;
            TripStart = tripStart;
            TripEnd = tripEnd;
            TripStartTicks = tripStart.Ticks;
            TripEndTicks = tripEnd.Ticks;
        }
    }
}
