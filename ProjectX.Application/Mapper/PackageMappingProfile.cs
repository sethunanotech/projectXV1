using AutoMapper;
using ProjectX.Application.Usecases.Package;
using ProjectX.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectX.Application.Mapper
{
    public class PackageMappingProfile:Profile
    {
        public PackageMappingProfile()
        {
            CreateMap<PackageAddRequest, Package>(MemberList.Source)
               .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
               .ForMember(dest => dest.CreatedOn, opt => opt.MapFrom(src => DateTime.UtcNow));

            CreateMap<PackageUpdateRequest, Package>(MemberList.Source)
                .ForMember(dest => dest.LastModifiedOn, opt => opt.MapFrom(src => DateTime.UtcNow));

            CreateMap<GetPackageResponse, Package>().ReverseMap();
        }
    }
}
