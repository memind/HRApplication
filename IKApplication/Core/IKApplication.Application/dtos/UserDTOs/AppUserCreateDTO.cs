using IKApplication.Application.VMs.CompanyVMs;
using IKApplication.Domain.Enums;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace IKApplication.Application.DTOs.UserDTOs
{
    public class AppUserCreateDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? SecondName { get; set; }
        public string Surname { get; set; }
        public string Title { get; set; }
        public string? BloodGroup { get; set; }
        public string? Profession { get; set; }
        public DateTime BirthDate { get; set; }
        public string IdentityId { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Email { get; set; }
        public string ImagePath { get; set; } = $"/images/defaultuser.jpg";
        [NotMapped]
        public IFormFile? UploadPath { get; set; }
        public DateTime CreateDate => DateTime.Now;
        public Status Status => Status.Active;
        public Guid? CompanyId { get; set; }
        public List<CompanyVM>? Companies { get; set; }
    }
}
