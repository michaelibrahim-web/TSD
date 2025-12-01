using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using TSD.Domain.Interfaces.Services;
using TSD.Services.Mapping;
using TSD.Services.Services;


namespace TSD.Services.Extension
{
    public static class LogicServiceCollectionExtensions
    {
        // This is the single method the API layer calls to register all Logic components.
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // Register all application services (Mapping Domain Interfaces to Logic Implementations)
            // We use AddScoped to ensure a new service instance per API request.
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IProjectService, ProjectService>();
            services.AddScoped<ITimeEntryService, TimeEntryService>();
            services.AddScoped<ITeamMemberService, TeamMemberService>();
            services.AddScoped<IClientService, ClientService>();
            services.AddScoped<ICategoryService, CategoryService>();

            // ✅ Correct AutoMapper registration
            // Scan the assembly where MappingProfile is located
            services.AddAutoMapper(typeof(MappingProfile).Assembly);





            return services;
        }
    }
}
