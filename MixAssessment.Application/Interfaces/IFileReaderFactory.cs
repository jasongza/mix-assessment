using MixAssessment.Domain.Entities;

namespace MixAssessment.Application.Interfaces
{
    public interface IFileReaderFactory
    {
        IFileReader<T> GetFileReader<T>(string path) where T : IEntity;
    }
}
