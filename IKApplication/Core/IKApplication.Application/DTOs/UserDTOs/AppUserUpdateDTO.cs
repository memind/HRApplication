using IKApplication.Application.VMs.CompanyVMs;
using IKApplication.Application.VMs.TitleVMs;
using IKApplication.Domain.Enums;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IKApplication.Application.DTOs.UserDTOs
{
    public class AppUserUpdateDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? SecondName { get; set; }
        public string Surname { get; set; }
        public BloodGroup? BloodGroup { get; set; }
        public string? Profession { get; set; }
        public DateTime BirthDate { get; set; }
        public string IdentityNumber { get; set; }
        public string Email { get; set; }
        public string? Password { get; set; }
        public string? ConfirmPassword { get; set; }
        public string PhoneNumber { get; set; }
        public string ImagePath { get; set; }
        [NotMapped]
        public IFormFile? UploadPath { get; set; }
        public DateTime JobStartDate { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate => DateTime.Now;
        public Status Status => Status.Modified;
        public Guid CompanyId { get; set; }
        public Guid TitleId { get; set; }
        public Guid PatronId { get; set; }
        public Guid? AddressId { get; set; }
        public List<CompanyVM>? Companies { get; set; }
        public List<TitleVM>? Titles { get; set; }
    }
}
