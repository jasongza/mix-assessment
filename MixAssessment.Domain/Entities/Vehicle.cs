namespace MixAssessment.Domain.Entities
{
    public class Vehicle : IEntity
    {
        public short Id { get; }
        public string Description { get; }
        public string RegistrationNumber { get; }

        public Vehicle(short id, string description, string registrationNumber)
        {
            Id = id;
            Description = description;
            RegistrationNumber = registrationNumber;
        }
    }
}
