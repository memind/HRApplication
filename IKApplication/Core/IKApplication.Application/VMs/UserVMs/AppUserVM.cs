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
        public string Title { get; set; }
        public string? BloodGroup { get; set; }
        public string? Profession { get; set; }
        public DateTime BirthDate { get; set; }
        public string IdentityId { get; set; }
        public Guid? CompanyId { get; set; }
        public string? CompanyName { get; set; }
        public string ImagePath { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public List<string> Roles { get; set; }
    }
}
