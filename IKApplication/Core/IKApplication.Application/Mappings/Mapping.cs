using AutoMapper;
using IKApplication.Application.dtos.CashAdvanceDTOs;
using IKApplication.Application.dtos.ExpenseDTOs;
using IKApplication.Application.dtos.TitleDTOs;
using IKApplication.Application.DTOs.CompanyDTOs;
using IKApplication.Application.DTOs.LeaveDTOs;
using IKApplication.Application.DTOs.PersonalDTO;
using IKApplication.Application.DTOs.TitleDTOs;
using IKApplication.Application.DTOs.UserDTOs;
using IKApplication.Application.VMs.CashAdvanceVMs;
using IKApplication.Application.VMs.ExpenseVMs;
using IKApplication.Application.VMs.LeaveVMs;
using IKApplication.Application.VMs.PersonalVM;
using IKApplication.Application.VMs.SectorVMs;
using IKApplication.Application.VMs.TitleVMs;
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
            CreateMap<Title, TitleUpdateDTO>().ReverseMap();
            CreateMap<Title, TitleVM>().ReverseMap();
            CreateMap<TitleUpdateDTO, TitleVM>().ReverseMap();

            CreateMap<Expense, ExpenseVM>().ReverseMap();
            CreateMap<Expense, ExpenseCreateDTO>().ReverseMap();
            CreateMap<Expense, ExpenseUpdateDTO>().ReverseMap();
            CreateMap<ExpenseUpdateDTO, ExpenseVM>().ReverseMap();

            CreateMap<CashAdvance, CashAdvanceVM>().ReverseMap();
            CreateMap<CashAdvance, CashAdvanceCreateDTO>().ReverseMap();
            CreateMap<CashAdvance, CashAdvanceUpdateDTO>().ReverseMap();

            // Personel için aşağıdakiler eklendi
            CreateMap<AppUser, PersonalCreateDTO>().ReverseMap();
            CreateMap<AppUser, PersonalUpdateDTO>().ReverseMap();
            CreateMap<AppUser, PersonalVM>().ReverseMap();
            CreateMap<AppUserCreateDTO, PersonalCreateDTO>().ReverseMap();

            CreateMap<Leave, LeaveVM>().ReverseMap();
            CreateMap<Leave, CreateLeaveDTO>().ReverseMap();
            CreateMap<Leave, UpdateLeaveDTO>().ReverseMap();
        }
    }
}
