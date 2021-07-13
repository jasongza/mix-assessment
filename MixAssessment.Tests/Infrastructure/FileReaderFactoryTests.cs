using AutoMoq;
using MixAssessment.Application.Interfaces;
using MixAssessment.Domain.Entities;
using MixAssessment.Infrastructure.FileReaders;
using Moq;
using System;
using System.Linq;
using Xunit;

namespace MixAssessment.Tests.Infrastructure
{
    public class FileReaderFactoryTests
    {
        private readonly AutoMoqer autoMoq;

        public FileReaderFactoryTests()
        {
            autoMoq = new AutoMoqer();
        }

        [Fact]
        public void GetFileReader_WithDriverCsv_ReturnsExpectedReader()
        {
            var sut = autoMoq.Create<FileReaderFactory>();

            var serviceProviderMock = autoMoq.GetMock<IServiceProvider>();
            var readerMock = autoMoq.GetMock<IFileReader<Driver>>();

            serviceProviderMock.Setup(mock => mock.GetService(typeof(DriverCsvFileReader)))
                .Returns(readerMock.Object);

            var result = sut.GetFileReader<Driver>("/file.csv");

            Assert.Equal(readerMock.Object, result);

            readerMock.Verify(mock => mock.SetPath("/file.csv"), Times.Once);
        }

        [Fact]
        public void GetFileReader_WithVehicleCsv_ReturnsExpectedReader()
        {
            var sut = autoMoq.Create<FileReaderFactory>();

            var serviceProviderMock = autoMoq.GetMock<IServiceProvider>();
            var readerMock = autoMoq.GetMock<IFileReader<Vehicle>>();

            serviceProviderMock.Setup(mock => mock.GetService(typeof(VehicleCsvFileReader)))
                .Returns(readerMock.Object);

            var result = sut.GetFileReader<Vehicle>("/file.csv");

            Assert.Equal(readerMock.Object, result);

            readerMock.Verify(mock => mock.SetPath("/file.csv"), Times.Once);
        }

        [Fact]
        public void GetFileReader_WithTripDat_ReturnsExpectedReader()
        {
            var sut = autoMoq.Create<FileReaderFactory>();

            var serviceProviderMock = autoMoq.GetMock<IServiceProvider>();
            var readerMock = autoMoq.GetMock<IFileReader<Trip>>();

            serviceProviderMock.Setup(mock => mock.GetService(typeof(TripDatFileReader)))
                .Returns(readerMock.Object);

            var result = sut.GetFileReader<Trip>("/file.dat");

            Assert.Equal(readerMock.Object, result);

            readerMock.Verify(mock => mock.SetPath("/file.dat"), Times.Once);
        }

        [Fact]
        public void GetFileReader_WithVehicleDriverIdentificationTxt_ReturnsExpectedReader()
        {
            var sut = autoMoq.Create<FileReaderFactory>();

            var serviceProviderMock = autoMoq.GetMock<IServiceProvider>();
            var readerMock = autoMoq.GetMock<IFileReader<VehicleDriverIdentification>>();

            serviceProviderMock.Setup(mock => mock.GetService(typeof(VehicleDriverIdentificationTextFileReader)))
                .Returns(readerMock.Object);

            var result = sut.GetFileReader<VehicleDriverIdentification>("/file.txt");

            Assert.Equal(readerMock.Object, result);

            readerMock.Verify(mock => mock.SetPath("/file.txt"), Times.Once);
        }

        [Fact]
        public void GetFileReader_WithUnknownReader_ThrowsException()
        {
            var sut = autoMoq.Create<FileReaderFactory>();

            var serviceProviderMock = autoMoq.GetMock<IServiceProvider>();
            var readerMock = autoMoq.GetMock<IFileReader<Vehicle>>();

            serviceProviderMock.Setup(mock => mock.GetService(typeof(VehicleCsvFileReader)))
                .Returns(readerMock.Object);

            Assert.Throws<NotImplementedException>(() => sut.GetFileReader<Vehicle>("/file.unknown"));
        }
    }

    public class DriverCsvFileReaderTests
    {
        [Fact]
        public void ReadLines_ReturnsAllRecords()
        {
            var sut = new DriverCsvFileReader();
            sut.SetPath("./Test Files/DriverList.csv");

            Assert.Equal(200, sut.ReadLines().Count());
        }
    }

    public class TripDatFileReaderTests
    {
        [Fact]
        public void ReadLines_ReturnsAllRecords()
        {
            var sut = new TripDatFileReader();
            sut.SetPath("./Test Files/Trips.dat");

            Assert.Equal(47316, sut.ReadLines().Count());
        }
    }

    public class VehicleCsvFileReaderTests
    {
        [Fact]
        public void ReadLines_ReturnsAllRecords()
        {
            var sut = new VehicleCsvFileReader();
            sut.SetPath("./Test Files/VehicleList.csv");

            Assert.Equal(100, sut.ReadLines().Count());
        }
    }

    public class VehicleDriverIdentificationTextFileReaderTests
    {
        [Fact]
        public void ReadLines_ReturnsAllRecords()
        {
            var sut = new VehicleDriverIdentificationTextFileReader();
            sut.SetPath("./Test Files/VehicleDriverIdentifications.txt");

            Assert.Equal(47316, sut.ReadLines().Count());
        }
    }
}
