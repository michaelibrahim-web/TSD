using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSD.Domain.Entities;
using TSD.Domain.Enums;
using TSD.Domain.Interfaces.Repository;

namespace TSD.Data.Repository
{
    public class ClientRepository : GenericRepository<Client>, IClientRepository
    {
        public ClientRepository(TSD_DbContext context) : base(context)
        {
        }

        // Implementation of the specific query from IClientRepository
        public async Task<IEnumerable<Client>> GetClientsWithActiveProjectsAsync()
        {
            // Query clients who have at least one project that is currently 'Active'
            return await _dbSet
                .Where(c => c.Projects.Any(p => p.Status == ProjectStatus.Active))
                .ToListAsync();
        }

        // Overriding the generic GetAllAsync to include projects for potential efficiency
        public new async Task<IEnumerable<Client>> GetAllAsync()
        {
            return await _dbSet
               .Include(c => c.Projects)
               .ToListAsync();
        }
    }
}
