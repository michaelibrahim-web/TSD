using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSD.Domain.Entities;
using TSD.Domain.Interfaces.Repository;

namespace TSD.Data.Repository
{
    public class TimeEntryRepository : GenericRepository<TimeEntry>, ITimeEntryRepository
    {
        public TimeEntryRepository(TSD_DbContext context) : base(context)
        {
        }

        // Helper method to include common navigation properties
        private IQueryable<TimeEntry> IncludeTimeEntryDetails(IQueryable<TimeEntry> query)
        {
            return query
                .Include(t => t.Employee)
                .Include(t => t.Project)
                .Include(t => t.Category);
        }

        public async Task<TimeEntry?> GetTimeEntryWithDetailsAsync(int id)
        {
            return await IncludeTimeEntryDetails(_dbSet).FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<IEnumerable<TimeEntry>> GetTimeEntriesForEmployeeAsync(int employeeId, DateTime? startDate, DateTime? endDate)
        {
            var query = IncludeTimeEntryDetails(_dbSet)
                .Where(t => t.EmployeeId == employeeId);

            if (startDate.HasValue)
            {
                // Filter by start of the day
                query = query.Where(t => t.EntryDate >= startDate.Value.Date);
            }

            if (endDate.HasValue)
            {
                // Filter by end of the day (less than the start of the next day)
                query = query.Where(t => t.EntryDate < endDate.Value.Date.AddDays(1));
            }

            return await query.OrderByDescending(t => t.EntryDate).ToListAsync();
        }

        public async Task<IEnumerable<TimeEntry>> GetTimeEntriesForProjectAsync(int projectId)
        {
            return await IncludeTimeEntryDetails(_dbSet)
                .Where(t => t.ProjectId == projectId)
                .OrderByDescending(t => t.EntryDate)
                .ToListAsync();
        }

        public Task<TimeEntry> UpdateTimeEntryAsync(TimeEntry timeEntry)
        {
            throw new NotImplementedException();
        }
    }
}
