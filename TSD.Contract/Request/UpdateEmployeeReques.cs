using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSD.Contract.Enums;

namespace TSD.Contract.Request
{
    public class UpdateEmployeeRequest
    {
        [Required]
        public string FullName { get; set; } = string.Empty;

        [Required]
        public string UserName { get; set; } = string.Empty;

        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        // This password will be hashed and salted in the Logic layer
        public string Password { get; set; } = string.Empty;

        [Required]
        public EmployeeRole Role { get; set; }
        public EmployeeStatus? Status { get; set; }

        public int HoursPerWeek { get; set; } = 40;
        public int Id { get; set; }
    }
}
