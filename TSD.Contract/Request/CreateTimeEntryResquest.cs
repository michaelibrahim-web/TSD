using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSD.Contract.Request
{
    public class CreatTimeEntryRequest
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public int ProjectId { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public string Description { get; set; } = string.Empty;

        [Required]
        public DateTime EntryDate { get; set; }

        [Range(0.01, 24.0)] // Basic validation: time must be logged
        public decimal Hours { get; set; } = 0m;

        [Range(0.0, 24.0)]
        public decimal OverTime { get; set; } = 0m;
    }
}
