using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSD.Contract.Enums;

namespace TSD.Contract.Response
{
    public class EmployeeResponse
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public EmployeeStatus Status { get; set; }
        public EmployeeRole Role { get; set; }
        public int HoursPerWeek { get; set; }
    }
}
