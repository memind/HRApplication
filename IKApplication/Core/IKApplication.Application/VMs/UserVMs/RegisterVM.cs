using IKApplication.Application.VMs.CompanyVMs;
using IKApplication.Domain.Entites;
using IKApplication.Domain.Enums;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace IKApplication.Application.VMs.UserVMs
{
    public class RegisterVM
    {
        // Company Administrator
        public string UserName { get; set; }
        public string? UserSecondName { get; set; }
        public string UserSurname { get; set; }
        public string UserTitle { get; set; }
        public string? UserBloodGroup { get; set; }
        public string? UserProfession { get; set; }
        public DateTime UserBirthDate { get; set; }
        public string UserIdentityId { get; set; }
        public string UserPassword { get; set; }
        public string UserConfirmPassword { get; set; }
        public string UserEmail { get; set; }
        public string UserImagePath { get; set; } = $"/images/defaultuser.jpg";
        [NotMapped]
        public IFormFile? UserUploadPath { get; set; }
        public DateTime UserCreateDate => DateTime.Now;
        public Status UserStatus => Status.Passive;
        public Guid UserCompanyId { get; set; }
        //public List<CompanyVM>? UserCompanies { get; set; }


        // Company
        public string CompanyName { get; set; }
        public string CompanyEmail { get; set; }
        public string CompanyPhoneNumber { get; set; }
        public Guid CompanySectorId { get; set; }
        public Sector CompanySector { get; set; }
        public int CompanyNumberOfEmployees { get; set; }
        public DateTime CompanyCreateDate => DateTime.Now;
        public Status CompanyStatus => Status.Passive;

        // Sector list for passing data
        public List<Sector>? SectorList { get; set; }

        // Constructor
        public RegisterVM()
        {
            SectorList = new List<Sector>();
        }
    }
}
