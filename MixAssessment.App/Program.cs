using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MixAssessment.App.Services;
using MixAssessment.Application.Interfaces;
using MixAssessment.Application.Services;
using MixAssessment.Infrastructure.FileReaders;

namespace MixAssessment.App
{
    class Program
    {
        static void Main(string[] _)
        {
            var serviceProvider = new ServiceCollection()
                .AddLogging(configure => configure.AddConsole())
                .AddSingleton<IConsoleService, ConsoleService>()
                .AddSingleton<IMetricService, MetricService>()
                .AddSingleton<IFileReaderFactory, FileReaderFactory>()
                .AddSingleton<DriverCsvFileReader>()
                .AddSingleton<VehicleCsvFileReader>()
                .AddSingleton<VehicleDriverIdentificationTextFileReader>()
                .AddSingleton<TripDatFileReader>()
                .BuildServiceProvider();

            serviceProvider.GetService<IConsoleService>().Run();
        }
    }
}
