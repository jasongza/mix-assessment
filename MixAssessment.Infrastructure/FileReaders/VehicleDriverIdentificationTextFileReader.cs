using MixAssessment.Application.Interfaces;
using MixAssessment.Domain.Entities;
using System;
using System.Collections.Generic;
using System.IO;

namespace MixAssessment.Infrastructure.FileReaders
{
    public class VehicleDriverIdentificationTextFileReader : IFileReader<VehicleDriverIdentification>
    {
        private string path;

        public VehicleDriverIdentificationTextFileReader()
        {
        }

        public void SetPath(string path)
        {
            this.path = path;
        }

        public IEnumerable<VehicleDriverIdentification> ReadLines()
        {
            using var reader = new StreamReader(path);

            string line;

            while ((line = reader.ReadLine()) != null)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }

                var sections = line.Split(' ');
                var timestamp = DateTime.Parse($"{sections[0]} {sections[1]}");
                var vehicleId = short.Parse(sections[2].Split("::")[0][1..]);
                var driverId = short.Parse(sections[2].Split("::")[1][..^1]);

                yield return new VehicleDriverIdentification(timestamp, vehicleId, driverId);
            }
        }
    }
}
