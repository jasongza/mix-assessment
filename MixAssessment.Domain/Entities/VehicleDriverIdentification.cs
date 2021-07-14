using System;

namespace MixAssessment.Domain.Entities
{
    public class VehicleDriverIdentification : IEntity
    {
        public DateTime Timestamp { get; }
        public long TimestampTicks { get; }
        public short VehicleId { get; }
        public short DriverId { get; }

        public VehicleDriverIdentification(DateTime timestamp, short vehicleId, short driverId)
        {
            Timestamp = timestamp;
            TimestampTicks = timestamp.Ticks;
            VehicleId = vehicleId;
            DriverId = driverId;
        }
    }
}
