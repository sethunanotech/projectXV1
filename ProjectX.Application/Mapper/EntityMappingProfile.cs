using AutoMapper;
using ProjectX.Application.Usecases.Entity;
using ProjectX.Domain.Entities;

namespace ProjectX.Application.Mapper
{
    public class EntityMappingProfile:Profile
    {
        public EntityMappingProfile()
        {
            CreateMap<EntityAddRequest, Entity>(MemberList.Source)
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
            .ForMember(dest => dest.CreatedOn, opt => opt.MapFrom(src => DateTime.UtcNow));

            CreateMap<EntityUpdateRequest, Entity>(MemberList.Source)
                .ForMember(dest => dest.LastModifiedOn, opt => opt.MapFrom(src => DateTime.UtcNow));

            CreateMap<GetEntityResponse, Entity>().ReverseMap();
        }
    }
}
