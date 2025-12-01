using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSD.Domain.Entities;

namespace TSD.Domain.Interfaces.Services
{
    public interface ITimeEntryService
    {
        Task<TimeEntry> GetTimeEntryByIdAsync(int id);
        Task<IEnumerable<TimeEntry>> GetTimeEntriesForUserAsync(int userId, DateTime? startDate, DateTime? endDate);
        Task<IEnumerable<TimeEntry>> GetTimeEntriesForProjectAsync(int projectId);
        Task<TimeEntry> LogTimeAsync(TimeEntry timeEntry);
        Task UpdateTimeEntryAsync(TimeEntry timeEntry);
        Task DeleteTimeEntryAsync(int id);
    }
}
