using AutoMapper;
using WBLMS.DTO;
using WBLMS.Models;

namespace WBLMS.API.Profiles
{
    public class OrganizationProfile : Profile
    {
        public OrganizationProfile()
        {
            // Source -> Target
            CreateMap<Organization, OrganizationReadDto>();
            CreateMap<OrganizationCreateDto, Organization>();
            CreateMap<OrganizationUpdateDto, Organization>();
        }
    }
}
