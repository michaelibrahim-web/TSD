using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSD.Data.Repository;
using TSD.Domain.Entities;
using TSD.Domain.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace TSD.Data.Extension
{




    namespace SolutionName.Data.Extensions
    {
        public static class DataServiceExtensions
        {
            // This is the single method the API layer will call to set up data persistence
            public static IServiceCollection AddDataInfrastructure(this IServiceCollection services, string connectionString)
            {
                // 1. Configure EF Core DbContext (using SQL Server in this example)
                services.AddDbContext<TSD_DbContext>(options =>
                    options.UseSqlServer(connectionString)
                );

                // 2. Register Generic Repository Implementation
                // Maps IGenericRepository<T> (Domain) to GenericRepository<T> (Data)
                services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

                // 3. Register Specific Repository Implementations
                // Maps I[Entity]Repository (Domain) to [Entity]Repository (Data)

                services.AddScoped<IEmployeeRepository, EmployeeRepository>();
                services.AddScoped<IProjectRepository, ProjectRepository>();
                services.AddScoped<IClientRepository, ClientRepository>();
                services.AddScoped<ITimeEntryRepository, TimeEntryRepository>();
                services.AddScoped<ICategoryRepository, CategoryRepository>();
                services.AddScoped<ITeamMemberRepository, TeamMemberRepository>();

                return services;
            }

        }
    }
}
