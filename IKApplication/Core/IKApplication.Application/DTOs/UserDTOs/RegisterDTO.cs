using IKApplication.Application.VMs.CompanyVMs;
using IKApplication.Application.VMs.SectorVMs;
using IKApplication.Application.VMs.TitleVMs;
using IKApplication.Domain.Enums;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace IKApplication.Application.DTOs.UserDTOs
{
    public class RegisterDTO
    {
        // Company Administrator
        public string UserName { get; set; }
        public string? UserSecondName { get; set; }
        public string UserSurname { get; set; }
        public BloodGroup? UserBloodGroup { get; set; }
        public string? UserProfession { get; set; }
        public DateTime UserBirthDate { get; set; }
        public string UserIdentityNumber { get; set; }
        public string UserEmail { get; set; }
        public string UserPassword { get; set; }
        public string UserConfirmPassword { get; set; }
        public string UserImagePath { get; set; } = $"/images/defaultuser.jpg";
        [NotMapped]
        public IFormFile? UserUploadPath { get; set; }
        public Guid? UserCompanyId { get; set; }
        public Guid UserTitleId { get; set; }
        public List<TitleVM>? Titles { get; set; }


        // Company
        public string CompanyName { get; set; }
        public string CompanyEmail { get; set; }
        public string CompanyPhoneNumber { get; set; }
        public int CompanyNumberOfEmployees { get; set; }
        public DateTime CompanyCreateDate => DateTime.Now;
        public Status CompanyStatus => Status.Passive;
        public Guid CompanySectorId { get; set; }
        public List<SectorVM>? Sectors { get; set; }
    }
}
