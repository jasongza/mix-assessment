using MixAssessment.Application.Interfaces;
using MixAssessment.Domain.Entities;
using System;

namespace MixAssessment.Infrastructure.FileReaders
{
    public class FileReaderFactory : IFileReaderFactory
    {
        private readonly IServiceProvider serviceProvider;

        public FileReaderFactory(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public IFileReader<T> GetFileReader<T>(string path) where T : IEntity
        {
            var extension = path[(path.LastIndexOf('.') + 1)..];
            IFileReader<T> reader = null;

            if (extension.Equals("csv", StringComparison.OrdinalIgnoreCase) && typeof(T) == typeof(Driver))
            {
                reader = serviceProvider.GetService(typeof(DriverCsvFileReader)) as IFileReader<T>;
            }
            else if (extension.Equals("csv", StringComparison.OrdinalIgnoreCase) && typeof(T) == typeof(Vehicle))
            {
                reader = serviceProvider.GetService(typeof(VehicleCsvFileReader)) as IFileReader<T>;
            }
            else if (extension.Equals("txt", StringComparison.OrdinalIgnoreCase) && typeof(T) == typeof(VehicleDriverIdentification))
            {
                reader = serviceProvider.GetService(typeof(VehicleDriverIdentificationTextFileReader)) as IFileReader<T>;
            }
            else if (extension.Equals("dat", StringComparison.OrdinalIgnoreCase) && typeof(T) == typeof(Trip))
            {
                reader = serviceProvider.GetService(typeof(TripDatFileReader)) as IFileReader<T>;
            }

            if (reader == null)
            {
                throw new NotImplementedException();
            }

            reader.SetPath(path);

            return reader;
        }
    }
}
