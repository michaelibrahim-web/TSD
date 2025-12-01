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
    public class TimeEntryService : ITimeEntryService
    {
        private readonly ITimeEntryRepository _timeEntryRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IEmployeeRepository _employeeRepository;

        public TimeEntryService(
            ITimeEntryRepository timeEntryRepository,
            IProjectRepository projectRepository,
            IEmployeeRepository employeeRepository)
        {
            _timeEntryRepository = timeEntryRepository;
            _projectRepository = projectRepository;
            _employeeRepository = employeeRepository;
        }

        public async Task<TimeEntry> GetTimeEntryByIdAsync(int id)
        {
            var timeEntry = await _timeEntryRepository.GetByIdAsync(id);

            if (timeEntry == null)
                throw new EntityNotFoundException($"Time entry with ID {id} not found.");

            return timeEntry;
        }

        public async Task<IEnumerable<TimeEntry>> GetTimeEntriesForEmployeeAsync(int userId, DateTime? startDate, DateTime? endDate)
        {
            return await _timeEntryRepository.GetTimeEntriesForEmployeeAsync(userId, startDate, endDate);
        }

        public async Task<IEnumerable<TimeEntry>> GetTimeEntriesForProjectAsync(int projectId)
        {
            return await _timeEntryRepository.GetTimeEntriesForProjectAsync(projectId);
        }

        public async Task<TimeEntry> LogTimeAsync(TimeEntry timeEntry)
        {
            // Validate employee exists
            var employee = await _employeeRepository.GetByIdAsync(timeEntry.EmployeeId);
            if (employee == null)
                throw new EntityNotFoundException($"Employee with ID {timeEntry.EmployeeId} does not exist.");

            // Validate project exists
            var project = await _projectRepository.GetByIdAsync(timeEntry.ProjectId);
            if (project == null)
                throw new EntityNotFoundException($"Project with ID {timeEntry.ProjectId} does not exist.");

            // Business rule: Prevent logging time on archived projects
            if (project.Archive)
                throw new EntityNotFoundException($"Cannot log time on archived project '{project.ProjectName}'.");

            // Additional rule: Validate time duration
            if (timeEntry.Hours < 0 || timeEntry.OverTime < 0)
                throw new EntityNotFoundException("Logged time cannot contain negative values.");

            await _timeEntryRepository.AddAsync(timeEntry);
            return timeEntry;
        }

        public async Task UpdateTimeEntryAsync(TimeEntry timeEntry)
        {
            var existing = await _timeEntryRepository.GetByIdAsync(timeEntry.Id);

            if (existing == null)
                throw new EntityNotFoundException($"Time entry with ID {timeEntry.Id} not found.");

            await _timeEntryRepository.UpdateTimeEntryAsync(timeEntry);
        }

        public async Task DeleteTimeEntryAsync(int id)
        {
            var existing = await _timeEntryRepository.GetByIdAsync(id);

            if (existing == null)
                throw new EntityNotFoundException($"Time entry with ID {id} cannot be deleted because it does not exist.");

            await _timeEntryRepository.UpdateTimeEntryAsync(existing);
        }

        public Task<IEnumerable<TimeEntry>> GetTimeEntriesForUserAsync(int userId, DateTime? startDate, DateTime? endDate)
        {
            throw new NotImplementedException();
        }
    }
}
