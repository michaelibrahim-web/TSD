using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSD.Domain.Entities
{
    public class TimeEntry : BaseEntity
    {
        public int EmployeeId { get; set; } 
        public int ProjectId { get; set; }
        public string Description { get; set; } = string.Empty;
        public int CategoryId { get; set; }
        public DateTime EntryDate { get; set; }
        public decimal Hours { get; set; }
        public decimal OverTime { get; set; }

        // Navigation properties
        public Employee Employee { get; set; } = null!;
        public Project Project { get; set; } = null!;
        public Category Category { get; set; } = null!;
    }
}
