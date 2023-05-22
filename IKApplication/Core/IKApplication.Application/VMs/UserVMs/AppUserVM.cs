using IKApplication.Domain.Entites;
using IKApplication.Domain.Enums;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace IKApplication.Application.VMs.UserVMs
{
    public class AppUserVM
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? SecondName { get; set; }
        public string Surname { get; set; }
        public string FullName => Name + " " + SecondName + " " + Surname;
        public BloodGroup? BloodGroup { get; set; }
        public string? Profession { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime? JobStartDate { get; set; }
        public string IdentityNumber { get; set; }
        public string PhoneNumber { get; set; }
        public string ImagePath { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public Guid CompanyId { get; set; }
        public Guid TitleId { get; set; }
        public string CompanyName { get; set; }
        public Title Title { get; set; }
        public List<string> Roles { get; set; }
    }
}
