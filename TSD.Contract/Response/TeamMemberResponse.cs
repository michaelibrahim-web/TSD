using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSD.Contract.Enums;

namespace TSD.Contract.Response
{
    public class TeamMemberResponse
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public int EmployeeId { get; set; }

        // Flattened Employee details
        public string EmployeeFullName { get; set; } = string.Empty;
        public string ProjectName { get; set; } = string.Empty;

        public EmployeeRole Role { get; set; }
        public string Department { get; set; } = string.Empty;
        public bool IsActive { get; set; }
    }
}
