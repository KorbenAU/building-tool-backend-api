namespace Microservice.Database.Entities
{
    public class PrimaryLoadCase : BaseEntity
    {
        public int IndexNo { get; set; }
        public string Identifier { get; set; }
        public string Abrvn { get; set; }
        public string Title { get; set; }
        public bool NationalLoads { get; set; }
        public string Description { get; set; }
    }
}