using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSD.Contract.Enums;

namespace TSD.Contract.Request
{
    public class UpdateTeamMemberRequest
    {
        public int Id { get; set; }
        [Required]
        public EmployeeRole Role { get; set; }

        public string Department { get; set; } = string.Empty;

        [Required]
        public bool IsActive { get; set; }
    }
}
