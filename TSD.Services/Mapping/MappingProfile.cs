using AutoMapper;
using TSD.Domain.Entities;
using TSD.Contract.Request;
using TSD.Contract.Response;

namespace TSD.Services.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            
            CreateMap<CreateEmployeeRequest, Employee>()
                .ForMember(dest => dest.Status, opt => opt.Ignore())
                .ForMember(dest => dest.TimeEntries, opt => opt.Ignore())
                .ForMember(dest => dest.TeamMembers, opt => opt.Ignore())
                .ForMember(dest => dest.LedProjects, opt => opt.Ignore());

            CreateMap<Employee, EmployeeResponse>();

            CreateMap<CreateProjectRequest, Project>()
                .ForMember(dest => dest.Status, opt => opt.Ignore())
                .ForMember(dest => dest.Archive, opt => opt.Ignore())
                .ForMember(dest => dest.Client, opt => opt.Ignore())
                .ForMember(dest => dest.Lead, opt => opt.Ignore());

            CreateMap<Project, ProjectResponse>()
                .ForMember(dest => dest.ClientName, opt => opt.MapFrom(src => src.Client != null ? src.Client.ClientName : "Unknown"))
                .ForMember(dest => dest.LeadFullName, opt => opt.MapFrom(src => src.Lead != null ? src.Lead.FullName : "Unassigned"))
                .ForMember(dest => dest.IsArchived, opt => opt.MapFrom(src => src.Archive))
                .ForMember(dest => dest.TeamMemberCount, opt => opt.MapFrom(src => src.TeamMembers != null ? src.TeamMembers.Count : 0));
            CreateMap<CreatTimeEntryRequest, TimeEntry>();

            CreateMap<TimeEntry, TimeEntryResponse>()
                .ForMember(dest => dest.Hours, opt => opt.MapFrom(src => src.Hours))
                .ForMember(dest => dest.OverTime, opt => opt.MapFrom(src => src.OverTime))
                .ForMember(dest => dest.UserFullName, opt => opt.MapFrom(src => src.Employee != null ? src.Employee.FullName : "Unknown"))
                .ForMember(dest => dest.ProjectName, opt => opt.MapFrom(src => src.Project != null ? src.Project.ProjectName : "Unknown"))
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category != null ? src.Category.CategoryName : "Unknown"));

            CreateMap<CreateClientRequest, Client>()
                .ForMember(dest => dest.Projects, opt => opt.Ignore());

            CreateMap<UpdateClientRequest, Client>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Projects, opt => opt.Ignore());

            CreateMap<Client, ClientResponse>();
            CreateMap<CreateCategoryRequest, Category>()
                .ForMember(dest => dest.TimeEntries, opt => opt.Ignore());

            CreateMap<Category, CategoryResponse>();

            CreateMap<AddTeamMemberRequest, TeamMember>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.FullName, opt => opt.Ignore())
                .ForMember(dest => dest.Role, opt => opt.Ignore())
                .ForMember(dest => dest.Department, opt => opt.Ignore())
                .ForMember(dest => dest.IsActive, opt => opt.Ignore());

            CreateMap<UpdateTeamMemberRequest, TeamMember>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.EmployeeId, opt => opt.Ignore())
                .ForMember(dest => dest.ProjectId, opt => opt.Ignore())
                .ForMember(dest => dest.FullName, opt => opt.Ignore());

            CreateMap<TeamMember, TeamMemberResponse>()
                .ForMember(dest => dest.EmployeeFullName, opt => opt.MapFrom(src => src.Employee != null ? src.Employee.FullName : "Unknown"))
                .ForMember(dest => dest.ProjectName, opt => opt.MapFrom(src => src.Project != null ? src.Project.ProjectName : "Unknown"));
        }
    }
}
