using MixAssessment.Infrastructure.FileReaders;
using System.Linq;
using Xunit;

namespace MixAssessment.Tests.Infrastructure
{
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
