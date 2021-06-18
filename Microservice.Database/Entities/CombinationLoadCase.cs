using System.Collections.Generic;

namespace Microservice.Database.Entities
{
    public class CombinationLoadCase : BaseEntity
    {
        public string Identifier { get; set; }
        public string Name { get; set; }

        public virtual List<CombinationLoadCaseRow> Rows { get; set; }
    }
}