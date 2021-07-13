using Microsoft.Extensions.Logging;
using MixAssessment.Application.Interfaces;
using System;
using System.Linq;

namespace MixAssessment.App.Services
{
    public class ConsoleService : IConsoleService
    {
        private readonly ILogger<ConsoleService> logger;
        private readonly IMetricService metricService;

        public ConsoleService(ILogger<ConsoleService> logger,
            IMetricService metricService)
        {
            this.logger = logger;
            this.metricService = metricService;
        }

        public void Run()
        {
            logger.LogInformation("Press any key to begin...");
            Console.ReadKey();

            logger.LogInformation("Processing...");

            var startTime = DateTime.Now;

            metricService.GetDriverMetrics()
                .OrderByDescending(driverMetric => driverMetric.AverageSpeed)
                .Take(10)
                .ToList()
                .ForEach(driverMetric => logger.LogInformation($"{driverMetric.Driver.Name}: {driverMetric.AverageSpeed}km/h"));

            var duration = DateTime.Now - startTime;

            logger.LogInformation($"Duration: {duration.TotalMilliseconds}ms");

            logger.LogInformation("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
