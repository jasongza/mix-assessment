using MixAssessment.Application.Interfaces;
using MixAssessment.Domain.Entities;
using System;
using System.Collections.Generic;
using System.IO;

namespace MixAssessment.Infrastructure.FileReaders
{
    public class TripDatFileReader : IFileReader<Trip>
    {
        private string path;

        public void SetPath(string path)
        {
            this.path = path;
        }

        public IEnumerable<Trip> ReadLines()
        {
            using var stream = new FileStream(path, FileMode.Open);
            var reader = new BinaryReader(stream);

            var position = 0;
            var length = (int)reader.BaseStream.Length;

            while (position < length)
            {
                var vehicleId = reader.ReadInt16();
                position += sizeof(short);

                var tripStartTicks = long.MaxValue + reader.ReadInt64();
                position += sizeof(long);

                var odometerStart = reader.ReadSingle();
                position += sizeof(float);

                var tripEndTicks = long.MaxValue + reader.ReadInt64();
                position += sizeof(long);

                var odometerEnd = reader.ReadSingle();
                position += sizeof(float);

                var distance = reader.ReadSingle();
                position += sizeof(float);

                var tripStart = new DateTime(tripStartTicks);
                var tripEnd = new DateTime(tripEndTicks);

                yield return new Trip(vehicleId, tripStart, odometerStart, tripEnd, odometerEnd, distance);
            }
        }
    }
}
