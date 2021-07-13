using CsvHelper;
using CsvHelper.Configuration;
using MixAssessment.Application.Interfaces;
using MixAssessment.Domain.Entities;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace MixAssessment.Infrastructure.FileReaders
{
    public class DriverCsvFileReader : IFileReader<Driver>
    {
        private readonly CsvConfiguration config;
        private string path;

        public DriverCsvFileReader()
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

        public IEnumerable<Driver> ReadLines()
        {
            using var reader = new StreamReader(path);
            using var csv = new CsvReader(reader, config);

            foreach (var driver in csv.GetRecords<Driver>())
            {
                yield return driver;
            }
        }
    }
}
