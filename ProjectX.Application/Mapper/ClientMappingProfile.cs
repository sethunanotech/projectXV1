using AutoMapper;
using ProjectX.Application.Usecases.Clients;
using ProjectX.Domain.Entities;

namespace ProjectX.Application.Mapper
{
    public class ClientMappingProfile : Profile
    {
        public ClientMappingProfile() 
        {
            CreateMap<ClientAddRequest , Client>(MemberList.Source)
                .ForMember(dest=>dest.Id,opt=>opt.MapFrom(src=>Guid.NewGuid()))
                .ForMember(dest=>dest.CreatedOn,opt=>opt.MapFrom(src=>DateTime.UtcNow));

            CreateMap<ClientUpdateRequest, Client>(MemberList.Source)
                .ForMember(dest => dest.LastModifiedOn, opt => opt.MapFrom(src => DateTime.UtcNow));

            CreateMap<GetClientResponse, Client>().ReverseMap();
        }
    }
}
