using MixAssessment.Infrastructure.FileReaders;
using System.Linq;
using Xunit;

namespace MixAssessment.Tests.Infrastructure
{
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
}
