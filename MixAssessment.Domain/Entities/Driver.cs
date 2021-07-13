namespace MixAssessment.Domain.Entities
{
    public class Driver : IEntity
    {
        public short Id { get; }
        public string Name { get; }

        public Driver(short id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
