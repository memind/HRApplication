using IKApplication.Application.VMs.CompanyVMs;
using IKApplication.Application.VMs.TitleVMs;
using IKApplication.Domain.Enums;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace IKApplication.Application.VMs.PersonalVM
{
    public class PersonalVM
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? SecondName { get; set; }
        public string Surname { get; set; }
        public string FullName => Name + " " + SecondName + " " + Surname;
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public Guid AddressId { get; set; }
        public BloodGroup BloodGroup { get; set; }
        public Guid TitleId { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime JobStartDate { get; set; }
        public Guid CompanyManagersId { get; set; }
        public string IdentityNumber { get; set; }
        public string ImagePath { get; set; }
        public string? CompanyName { get; set; }
        public string Title { get; set; }
        public List<string> Roles { get; set; }
    }
}
