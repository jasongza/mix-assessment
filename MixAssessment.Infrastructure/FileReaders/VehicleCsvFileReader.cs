using CsvHelper;
using CsvHelper.Configuration;
using MixAssessment.Application.Interfaces;
using MixAssessment.Domain.Entities;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace MixAssessment.Infrastructure.FileReaders
{
    public class VehicleCsvFileReader : IFileReader<Vehicle>
    {
        private readonly CsvConfiguration config;
        private string path;

        public VehicleCsvFileReader()
        {
            config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = false
            };
        }

        public void SetPath(string path)
        {
            this.path = path;
        }

        public IEnumerable<Vehicle> ReadLines()
        {
            using var reader = new StreamReader(path);
            using var csv = new CsvReader(reader, config);

            foreach (var Vehicle in csv.GetRecords<Vehicle>())
            {
                yield return Vehicle;
            }
        }
    }
}
