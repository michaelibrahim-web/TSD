using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSD.Domain.Entities;
using TSD.Domain.Exceptions;
using TSD.Domain.Interfaces.Repository;
using TSD.Domain.Interfaces.Services;

namespace TSD.Services.Services
{
     public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;

        public ClientService(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public async Task<Client> GetClientByIdAsync(int id)
        {
            var client = await _clientRepository.GetByIdAsync(id);
            if (client == null)
            {
                throw new EntityNotFoundException(nameof(Client), id);
            }
            return client;
        }

        public async Task<IEnumerable<Client>> GetAllClientsAsync()
        {
            return await _clientRepository.GetAllAsync();
        }

        public async Task<IEnumerable<Client>> GetClientsWithActiveProjectsAsync()
        {
            // Delegates the complex, filtered query to the specific repository method
            return await _clientRepository.GetClientsWithActiveProjectsAsync();
        }

        public async Task<Client> CreateClientAsync(Client newClient)
        {
            // Business Rule: Validate basic data before saving
            if (string.IsNullOrWhiteSpace(newClient.ClientName))
            {
                throw new ArgumentException("Client name is required.");
            }

            // In a real application, you would check for uniqueness here if required
            // (e.g., check if ClientName already exists)

            await _clientRepository.AddAsync(newClient);
            await _clientRepository.SaveChangesAsync();
            return newClient;
        }

        public async Task UpdateClientAsync(Client updatedClient)
        {
            var existingClient = await GetClientByIdAsync(updatedClient.Id); // Includes existence check

            // Update only the allowed fields
            existingClient.ClientName = updatedClient.ClientName;
            existingClient.Email = updatedClient.Email;
            existingClient.Address = updatedClient.Address;
            existingClient.City = updatedClient.City;
            existingClient.ZipCode = updatedClient.ZipCode;
            existingClient.Country = updatedClient.Country;

            _clientRepository.Update(existingClient);
            await _clientRepository.SaveChangesAsync();
        }

        public async Task DeleteClientAsync(int id)
        {
            var clientToDelete = await GetClientByIdAsync(id);

            // Business Rule: Cannot delete a client if they have existing projects (or active projects)
            if (clientToDelete.Projects != null && clientToDelete.Projects.Count > 0)
            {
                throw new InvalidOperationException($"Cannot delete client '{clientToDelete.ClientName}' because they have associated projects.");
            }

            _clientRepository.Delete(clientToDelete);
            await _clientRepository.SaveChangesAsync();
        }
    }
}
