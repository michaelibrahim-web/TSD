using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSD.Contract.Enums;

namespace TSD.Domain.Entities
{
    public class Employee : BaseEntity
    {
        public string FullName { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty; // In a real app, hash and salt this!
        public EmployeeStatus Status { get; set; }
        public EmployeeRole Role { get; set; }
        public int HoursPerWeek { get; set; }

        // Navigation property for TimeEntries (1:M relationship with TimeEntry)
        public ICollection<TimeEntry> TimeEntries { get; set; } = new HashSet<TimeEntry>();

        // Navigation property for TeamMembers (1:M relationship with TeamMember, where Employee is a team member)
        public ICollection<TeamMember> TeamMembers { get; set; } = new HashSet<TeamMember>();

        // Navigation property for Projects where this employee is a LeadId (1:M relationship with Projects)
        public ICollection<Project> LedProjects { get; set; } = new HashSet<Project>();
    }
}
