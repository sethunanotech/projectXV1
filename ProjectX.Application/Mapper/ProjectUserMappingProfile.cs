using AutoMapper;
using ProjectX.Application.Usecases.Projects;
using ProjectX.Application.Usecases.ProjectUsers;
using ProjectX.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectX.Application.Mapper
{
    public class ProjectUserMappingProfile:Profile
    {
        public ProjectUserMappingProfile()
        {
            CreateMap<ProjectUserAddRequest, ProjectUser>(MemberList.Source)
              .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
              .ForMember(dest => dest.CreatedOn, opt => opt.MapFrom(src => DateTime.UtcNow));

            CreateMap<ProjectUserUpdateRequest, ProjectUser>(MemberList.Source)
                .ForMember(dest => dest.LastModifiedOn, opt => opt.MapFrom(src => DateTime.UtcNow));

            CreateMap<GetProjectUserResponse, ProjectUser>().ReverseMap();
        }
      
    }
}
