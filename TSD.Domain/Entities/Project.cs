using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSD.Contract.Enums;

namespace TSD.Domain.Entities
{
    public class Project : BaseEntity
    {
        public int ClientId { get; set; }
        public required string ProjectName { get; set; } 
        public required string Description { get; set; } 
        public int LeadId { get; set; } // Foreign key to Employee
        public ProjectStatus Status { get; set; }
        public bool Archive { get; set; }

        // Navigation property for Client (M:1 relationship with Client)
        public Client Client { get; set; } = null!; 

        // Navigation property for Project Lead (M:1 relationship with Employee)
        public Employee Lead { get; set; } = null!; 

        // Navigation property for TimeEntries (1:M relationship with TimeEntry)
        public ICollection<TimeEntry> TimeEntries { get; set; } = new HashSet<TimeEntry>();

        // Navigation property for TeamMembers (M:M through TeamMember entity with Employee)
        public ICollection<TeamMember> TeamMembers { get; set; } = new HashSet<TeamMember>();
    }
}
