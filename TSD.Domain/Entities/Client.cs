using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSD.Domain.Entities
{
    public class Client : BaseEntity
    {
        public required string ClientName { get; set; }
        public required string Email { get; set; }
        public required string Address { get; set; }
        public required string City { get; set; }
        public required string ZipCode { get; set; }
        public required string Country { get; set; }

        // Navigation property for Projects (1:M relationship with Projects)
        public ICollection<Project> Projects { get; set; } = new HashSet<Project>();
    }
}
