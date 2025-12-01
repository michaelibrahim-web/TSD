using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSD.Domain.Entities;

namespace TSD.Domain.Interfaces.Services
{
    public interface IClientService
    {
        Task<Client> GetClientByIdAsync(int id);
        Task<IEnumerable<Client>> GetAllClientsAsync();
        Task<IEnumerable<Client>> GetClientsWithActiveProjectsAsync(); // Business-specific query
        Task<Client> CreateClientAsync(Client newClient);
        Task UpdateClientAsync(Client updatedClient);
        Task DeleteClientAsync(int id);
    }
}
