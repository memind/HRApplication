using AutoMapper;
using IKApplication.Application.DTOs.CompanyDTOs;
using IKApplication.Application.DTOs.UserDTOs;
using IKApplication.Application.VMs.UserVMs;
using IKApplication.Domain.Entites;

namespace IKApplication.Application.Mappings
{
    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Company,UpdateCompanyDTO>().ReverseMap();
            CreateMap<Company,CreateCompanyDTO>().ReverseMap();
            
            CreateMap<AppUser, AppUserUpdateDTO>().ReverseMap();
            CreateMap<AppUser, AppUserVM>().ReverseMap();

        }
    }
}
