using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSD.Contract.Response
{
    public class TimeEntryResponse
    {
        public int Id { get; set; }
        public DateTime EntryDate { get; set; }
        public string Description { get; set; } = string.Empty;

        // Flattened Duration Value Object
        public decimal Hours { get; set; }
        public decimal OverTime { get; set; }
        public decimal TotalTime { get; set; } // Calculated for the client

        // Related Ids and Names
        public int UserId { get; set; }
        public string UserFullName { get; set; } = string.Empty;
        public int ProjectId { get; set; }
        public string ProjectName { get; set; } = string.Empty;
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty;
    }
}
