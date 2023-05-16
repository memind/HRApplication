using AutoMapper;
using IKApplication.Application.dtos.ExpenseDTOs;
using IKApplication.Application.DTOs.CompanyDTOs;
using IKApplication.Application.DTOs.TitleDTOs;
using IKApplication.Application.DTOs.UserDTOs;
using IKApplication.Application.VMs.ExpenseVMs;
using IKApplication.Application.VMs.SectorVMs;
using IKApplication.Application.VMs.UserVMs;
using IKApplication.Domain.Entites;

namespace IKApplication.Application.Mappings
{
    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Company, CompanyCreateDTO>().ReverseMap();
            CreateMap<Company, CompanyUpdateDTO>().ReverseMap();

            CreateMap<AppUser, AppUserUpdateDTO>().ReverseMap();
            CreateMap<AppUser, AppUserCreateDTO>().ReverseMap();
            CreateMap<AppUser, AppUserVM>().ReverseMap();

            CreateMap<Sector, SectorVM>().ReverseMap();

            CreateMap<Title, TitleCreateDTO>().ReverseMap();

            CreateMap<Expense, ExpenseVM>().ReverseMap();
            CreateMap<Expense, ExpenseCreateDTO>().ReverseMap();
            CreateMap<Expense, ExpenseUpdateDTO>().ReverseMap();
        }
    }
}
