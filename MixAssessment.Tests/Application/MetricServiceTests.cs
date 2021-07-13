using AutoMoq;
using MixAssessment.Application.Interfaces;
using MixAssessment.Application.Services;
using MixAssessment.Domain.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace MixAssessment.Tests.Application
{
    public class MetricServiceTests
    {
        private readonly AutoMoqer autoMoq;

        public MetricServiceTests()
        {
            autoMoq = new AutoMoqer();
        }

        [Fact]
        public void GetDriverMetrics_CallsAllExpectedEndpoints()
        {
            var sut = autoMoq.Create<MetricService>();
            var fileReaderFactoryMock = autoMoq.GetMock<IFileReaderFactory>();
            var driverFileReaderMock = autoMoq.GetMock<IFileReader<Driver>>();
            var tripFileReaderMock = autoMoq.GetMock<IFileReader<Trip>>();
            var vehicleFileReaderMock = autoMoq.GetMock<IFileReader<Vehicle>>();
            var identificationFileReaderMock = autoMoq.GetMock<IFileReader<VehicleDriverIdentification>>();

            fileReaderFactoryMock.Setup(mock => mock.GetFileReader<Driver>(It.IsAny<string>()))
                .Returns(driverFileReaderMock.Object);

            driverFileReaderMock.Setup(mock => mock.ReadLines())
                .Returns(new List<Driver>());

            fileReaderFactoryMock.Setup(mock => mock.GetFileReader<Trip>(It.IsAny<string>()))
                .Returns(tripFileReaderMock.Object);

            tripFileReaderMock.Setup(mock => mock.ReadLines())
                .Returns(new List<Trip>());

            fileReaderFactoryMock.Setup(mock => mock.GetFileReader<Vehicle>(It.IsAny<string>()))
                .Returns(vehicleFileReaderMock.Object);

            vehicleFileReaderMock.Setup(mock => mock.ReadLines())
                .Returns(new List<Vehicle>());

            fileReaderFactoryMock.Setup(mock => mock.GetFileReader<VehicleDriverIdentification>(It.IsAny<string>()))
                .Returns(identificationFileReaderMock.Object);

            identificationFileReaderMock.Setup(mock => mock.ReadLines())
                .Returns(new List<VehicleDriverIdentification>());

            sut.GetDriverMetrics();

            fileReaderFactoryMock.Verify(mock => mock.GetFileReader<Driver>(It.IsAny<string>()), Times.Once);
            fileReaderFactoryMock.Verify(mock => mock.GetFileReader<Trip>(It.IsAny<string>()), Times.Once);
            fileReaderFactoryMock.Verify(mock => mock.GetFileReader<Vehicle>(It.IsAny<string>()), Times.Once);
            fileReaderFactoryMock.Verify(mock => mock.GetFileReader<VehicleDriverIdentification>(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void GetDriverMetrics_WithData_ReturnsExpectedAggregates()
        {
            var sut = autoMoq.Create<MetricService>();
            var fileReaderFactoryMock = autoMoq.GetMock<IFileReaderFactory>();
            var driverFileReaderMock = autoMoq.GetMock<IFileReader<Driver>>();
            var tripFileReaderMock = autoMoq.GetMock<IFileReader<Trip>>();
            var vehicleFileReaderMock = autoMoq.GetMock<IFileReader<Vehicle>>();
            var identificationFileReaderMock = autoMoq.GetMock<IFileReader<VehicleDriverIdentification>>();

            fileReaderFactoryMock.Setup(mock => mock.GetFileReader<Driver>(It.IsAny<string>()))
                .Returns(driverFileReaderMock.Object);

            driverFileReaderMock.Setup(mock => mock.ReadLines())
                .Returns(new List<Driver>
                {
                    new Driver(1, "One"),
                    new Driver(2, "Two")
                });

            fileReaderFactoryMock.Setup(mock => mock.GetFileReader<Trip>(It.IsAny<string>()))
                .Returns(tripFileReaderMock.Object);

            tripFileReaderMock.Setup(mock => mock.ReadLines())
                .Returns(new List<Trip>
                {
                    new Trip(11, DateTime.Now.AddHours(-24), 12345, DateTime.Now.AddHours(-23), 23456, 80),
                    new Trip(12, DateTime.Now.AddHours(-24), 12345, DateTime.Now.AddHours(-23), 23456, 90),
                    new Trip(13, DateTime.Now.AddHours(-24), 12345, DateTime.Now.AddHours(-23), 23456, 100),
                });

            fileReaderFactoryMock.Setup(mock => mock.GetFileReader<Vehicle>(It.IsAny<string>()))
                .Returns(vehicleFileReaderMock.Object);

            vehicleFileReaderMock.Setup(mock => mock.ReadLines())
                .Returns(new List<Vehicle>());

            fileReaderFactoryMock.Setup(mock => mock.GetFileReader<VehicleDriverIdentification>(It.IsAny<string>()))
                .Returns(identificationFileReaderMock.Object);

            identificationFileReaderMock.Setup(mock => mock.ReadLines())
                .Returns(new List<VehicleDriverIdentification>
                {
                    new VehicleDriverIdentification(DateTime.Now.AddHours(-24), 11, 1),
                    new VehicleDriverIdentification(DateTime.Now.AddHours(-24).AddMinutes(5), 12, 1),
                    new VehicleDriverIdentification(DateTime.Now.AddHours(-24).AddMinutes(30), 13, 2)
                });

            var result = sut.GetDriverMetrics().ToList();

            Assert.Equal(2, result.Count);

            var driver1 = result.First(x => x.Driver.Id == 1);
            var driver2 = result.First(x => x.Driver.Id == 2);

            Assert.Equal(2, driver1.Trips.Count);
            Assert.Equal(1, driver2.Trips.Count);
        }
    }
}
