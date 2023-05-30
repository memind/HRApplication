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
        public Status Status { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime? JobStartDate { get; set; }
        public string IdentityNumber { get; set; }
        public string PhoneNumber { get; set; }
        public string ImagePath { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public Status Status { get; set; } // eklendi
        public Guid CompanyId { get; set; }
        public Guid TitleId { get; set; }
        public Guid ProfessionId { get; set; }
        public Guid PatronId { get; set; }
        public AppUser Patron { get; set; }
        public string CompanyName { get; set; }
        public Title Title { get; set; }
        public Profession Profession { get; set; }
        public List<string> Roles { get; set; }
    }
}
