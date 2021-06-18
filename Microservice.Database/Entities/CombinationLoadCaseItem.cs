namespace Microservice.Database.Entities
{
    public class CombinationLoadCaseItem : BaseEntity
    {
        public int CombinationLoadCaseRowId { get; set; }

        public int PrimaryLoadCaseId { get; set; }
        public float Weight { get; set; }
    }
}