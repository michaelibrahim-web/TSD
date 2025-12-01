using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSD.Contract.Request
{
    public class CreateProjectRequest
    {
        [Required]
        public int ClientId { get; set; } // Must link to an existing client

        [Required]
        public string ProjectName { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        [Required]
        public int LeadId { get; set; } // Must link to an existing employee
    }
}
