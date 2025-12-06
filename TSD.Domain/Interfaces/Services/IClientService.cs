using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSD.Domain.Entities;
using TSD.Contract.Request;
using TSD.Contract.Response;
namespace TSD.Domain.Interfaces.Services
{
    public interface IClientService
    {
        Task<ClientResponse> GetClientByIdAsync(int id);
        Task<IEnumerable<ClientResponse>> GetAllClientsAsync();
        Task<IEnumerable<ClientResponse>> GetClientsWithActiveProjectsAsync(); // Business-specific query
        Task<ClientResponse> CreateClientAsync(CreateClientRequest newClient);
        Task UpdateClientAsync(UpdateClientRequest updatedClient);
        Task DeleteClientAsync(int id);
    }
}
