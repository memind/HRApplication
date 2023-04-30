using AutoMapper;
using IKApplication.Application.DTOs.CompanyDTOs;
using IKApplication.Application.DTOs.SiteManagerDTO;
using IKApplication.Application.VMs.SiteManagerVMs;
using IKApplication.Domain.Entites;

namespace IKApplication.Application.Mappings
{
    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Company,UpdateCompanyDTO>().ReverseMap();
            CreateMap<Company,CreateCompanyDTO>().ReverseMap();
            
            CreateMap<AppUser, SiteManagerUpdateDTO>().ReverseMap();
            CreateMap<AppUser, SiteManagerVMs>().ReverseMap();

        }
    }
}
