using MixAssessment.Infrastructure.FileReaders;
using System.Linq;
using Xunit;

namespace MixAssessment.Tests.Infrastructure
{
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
}
