using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSD.Domain.Enums;

namespace TSD.Domain.Entities
{
    public class TeamMember : BaseEntity
    {
        public int EmployeeId { get; set; }
        public int ProjectId { get; set; } 

        public required string FullName { get; set; }
        public EmployeeRole Role { get; set; } 
        public Department Department { get; set; }
        public bool IsActive { get; set; }

        // Navigation properties
        public Employee Employee { get; set; } = null!; 
        public Project Project { get; set; } = null!; 
    }
}
