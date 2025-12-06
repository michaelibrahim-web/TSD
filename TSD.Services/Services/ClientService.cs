using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSD.Contract.Request;
using TSD.Contract.Response;
using TSD.Domain.Entities;
using TSD.Domain.Exceptions;
using TSD.Domain.Interfaces.Repository;
using TSD.Domain.Interfaces.Services;


namespace TSD.Services.Services
{
     public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;
        private readonly IMapper _mapper;

        public ClientService(IClientRepository clientRepository,IMapper mapper)
        {
            _clientRepository = clientRepository;
            _mapper = mapper;
        }

        public async Task<ClientResponse> GetClientByIdAsync(int id)
        {
            var client = await _clientRepository.GetByIdAsync(id);
            if (client == null)
            {
                throw new EntityNotFoundException(nameof(Client), id);
            }
            return _mapper.Map<ClientResponse>(client);
        }

        public async Task<IEnumerable<ClientResponse>> GetAllClientsAsync()
        {
            return _mapper.Map < IEnumerable < ClientResponse >>( await _clientRepository.GetAllAsync());
        }

        public async Task<IEnumerable<ClientResponse>> GetClientsWithActiveProjectsAsync()
        {
            // Delegates the complex, filtered query to the specific repository method
            return _mapper.Map<IEnumerable<ClientResponse>>(await _clientRepository.GetAllAsync());
        }

        public async Task<ClientResponse> CreateClientAsync(CreateClientRequest newClient)
        {
            // Business Rule: Validate basic data before saving
            if (string.IsNullOrWhiteSpace(newClient.ClientName))
            {
                throw new ArgumentException("Client name is required.");
            }

            // In a real application, you would check for uniqueness here if required
            // (e.g., check if ClientName already exists)

          await _clientRepository.AddAsync(_mapper.Map<Client>(newClient));
            await _clientRepository.SaveChangesAsync();
            var result = _mapper.Map<Client>(newClient);
            return _mapper.Map<ClientResponse>(result);
        }

        public async Task UpdateClientAsync(UpdateClientRequest updatedClient)
        {
            var existingClient = await _clientRepository.GetByIdAsync(updatedClient.Id); // Includes existence check

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
           

            _clientRepository.Delete(_mapper.Map<Client>(clientToDelete));
            await _clientRepository.SaveChangesAsync();
        }
    }
}
