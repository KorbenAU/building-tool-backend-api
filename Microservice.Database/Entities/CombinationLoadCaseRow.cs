using System.Collections.Generic;

namespace Microservice.Database.Entities
{
    public class CombinationLoadCaseRow
    {
        public int CombinationLoadCaseId { get; set; }
        public bool IsInUser { get; set; }
        public string Axis { get; set; }
        public string Ampl { get; set; }
        public int IndexNo { get; set; }
        public string Abrn { get; set; }
        public string LongTitle { get; set; }

        public virtual List<CombinationLoadCaseItem> Itmes { get; set; }
    }
}