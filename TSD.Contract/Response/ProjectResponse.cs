using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSD.Contract.Enums;

namespace TSD.Contract.Response
{
    public class ProjectResponse
    {
        public int Id { get; set; }
        public string ProjectName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public ProjectStatus Status { get; set; }
        public bool IsArchived { get; set; }
        public int ClientId { get; set; }
        public string ClientName { get; set; } = string.Empty;
        public int LeadId { get; set; }
        public string LeadFullName { get; set; } = string.Empty;

        // Optionally, include a count of active team members
        public int TeamMemberCount { get; set; }
    }
}
