using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSD.Domain.Entities;

namespace TSD.Domain.Interfaces.Repository
{
    public interface ITimeEntryRepository : IGenericRepository<TimeEntry>
    {
        IQueryable<TimeEntry> Query();

        Task<IEnumerable<TimeEntry>> GetTimeEntriesForEmployeeAsync(int userId, DateTime? startDate, DateTime? endDate);

        
        Task<IEnumerable<TimeEntry>> GetTimeEntriesForProjectAsync(int projectId);

        
        Task<TimeEntry?> GetTimeEntryWithDetailsAsync(int id);
        Task<TimeEntry> UpdateTimeEntryAsync(TimeEntry timeEntry);
    }
}
