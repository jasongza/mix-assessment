using MixAssessment.Infrastructure.FileReaders;
using System.Linq;
using Xunit;

namespace MixAssessment.Tests.Infrastructure
{
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
}
