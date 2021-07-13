﻿using MixAssessment.Application.Interfaces;
using MixAssessment.Domain.Aggregates;
using MixAssessment.Domain.Entities;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace MixAssessment.Application.Services
{
    public class MetricService : IMetricService
    {
        private readonly IFileReaderFactory fileReaderFactory;

        public MetricService(IFileReaderFactory fileReaderFactory)
        {
            this.fileReaderFactory = fileReaderFactory;
        }

        public IEnumerable<DriverMetric> GetDriverMetrics()
        {
            var bag = new ConcurrentBag<DriverMetric>();

            Parallel.ForEach(GetData().Drivers, driver =>
            {
                var metric = new DriverMetric(driver.Driver, driver.Trips.ToList());
                metric.CalculateAverageSpeed();

                bag.Add(metric);
            });

            return bag.ToList();
        }

        private Data GetData()
        {
            List<Driver> drivers = null;
            List<Vehicle> vehicles = null;
            List<Trip> trips = null;

            var tasks = new List<Task>
            {
                new Task(() =>
                {
                    drivers = fileReaderFactory
                       .GetFileReader<Driver>("./Input Data/DriverList.csv")
                       .ReadLines()
                       .ToList();
                }),
                new Task(() =>
                {
                    vehicles = fileReaderFactory
                       .GetFileReader<Vehicle>("./Input Data/VehicleList.csv")
                       .ReadLines()
                       .ToList();
                }),
                new Task(() =>
                {
                    trips = fileReaderFactory
                        .GetFileReader<Trip>("./Input Data/Trips.dat")
                        .ReadLines()
                        .ToList();
                })
            };

            tasks.ForEach(task => task.Start());
            Task.WaitAll(tasks.ToArray());

            var data = new Data
            {
                Drivers = drivers?.Select(driver => new DriverData(driver)).ToList()
            };

            var identifications = fileReaderFactory
                .GetFileReader<VehicleDriverIdentification>("./Input Data/VehicleDriverIdentifications.txt")
                .ReadLines();

            Parallel.ForEach(identifications, identification => {
                var matchingTrip = trips?.FirstOrDefault(trip =>
                    trip.VehicleId == identification.VehicleId &&
                    trip.TripStart <= identification.Timestamp &&
                    trip.TripEnd >= identification.Timestamp);

                if (matchingTrip == null)
                {
                    return;
                }     

                var driver = data.Drivers.FirstOrDefault(driver => driver.Driver.Id == identification?.DriverId);

                driver?.Trips.Add(matchingTrip);
            });

            return data;
        }

        private class Data
        {
            public List<DriverData> Drivers { get; set; }
        }

        private class DriverData
        {
            public DriverData(Driver driver)
            {
                Driver = driver;
                Trips = new ConcurrentBag<Trip>();
            }

            public Driver Driver { get; }
            public ConcurrentBag<Trip> Trips { get; }
        }
    }
}
