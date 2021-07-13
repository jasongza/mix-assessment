using MixAssessment.Domain.Entities;
using System.Collections.Generic;

namespace MixAssessment.Application.Interfaces
{
    public interface IFileReader<T> where T : IEntity
    {
        void SetPath(string path);
        IEnumerable<T> ReadLines();
    }
}
