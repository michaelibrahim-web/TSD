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
    public interface ITimeEntryService
    {
     

        Task<TimeEntryResponse> GetTimeEntryByIdAsync(int id);
       
        Task<IEnumerable<TimeEntryResponse>> GetTimeEntriesForProjectAsync(int projectId);
        Task<TimeEntryResponse> LogTimeAsync(CreatTimeEntryRequest timeEntry);
        Task UpdateTimeEntryAsync(CreatTimeEntryRequest timeEntry);
        Task DeleteTimeEntryAsync(int id);
    }
}
