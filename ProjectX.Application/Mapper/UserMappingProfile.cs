using AutoMapper;
using ProjectX.Application.Usecases.Login;
using ProjectX.Application.Usecases.User;
using ProjectX.Domain.Entities;

namespace ProjectX.Application.Mapper
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<User,GetUserResponse>().ReverseMap();

            CreateMap<UserAddRequest, User>(MemberList.Source)
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.CreatedOn, opt => opt.MapFrom(src => DateTime.UtcNow));

            CreateMap<UserUpdateRequest, User>(MemberList.Source)
                .ForMember(dest => dest.LastModifiedOn, opt => opt.MapFrom(src => DateTime.UtcNow));

            CreateMap<UserLogin,User>();
           
        }
    }
}
