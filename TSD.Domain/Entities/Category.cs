using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSD.Domain.Entities
{
    public class Category : BaseEntity
    {
        public required string CategoryName { get; set; }

        // Navigation property for TimeEntries (1:M relationship with TimeEntry)
        public ICollection<TimeEntry> TimeEntries { get; set; } = new HashSet<TimeEntry>();
    }
}
