using AutoMapper;
using ProjectX.Application.Usecases.Login;
using ProjectX.Application.Usecases.Projects;
using ProjectX.Application.Usecases.User;
using ProjectX.Domain.Entities;

namespace ProjectX.Application.Mapper
{
    public class ProjectMappingProfile: Profile
    {
        
        public ProjectMappingProfile()
        {

            CreateMap<ProjectAddRequest, Project>(MemberList.Source)
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.CreatedOn, opt => opt.MapFrom(src => DateTime.UtcNow));

            CreateMap<ProjectUpdateRequest, Project>(MemberList.Source)
                .ForMember(dest => dest.LastModifiedOn, opt => opt.MapFrom(src => DateTime.UtcNow));

                    CreateMap<GetProjectResponse, Project>().ReverseMap();
        }
    }
}
