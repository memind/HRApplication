using IKApplication.Domain.Entites;
using IKApplication.Domain.Enums;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

namespace IKApplication.Application.VMs.UserVMs
{
    public class RegisterVM
    {
        public Guid UserId { get; set; }
        public Guid CompanyId { get; set; }
        public string UserName { get; set; }
        public string? UserSecondName { get; set; }
        public string UserSurname { get; set; }
        public string FullName => UserName + " " + UserSecondName + " " + UserSurname;
        public string? UserTitle { get; set; }
        public string UserEmail { get; set; }
        public string CompanyName { get; set; }
        public string CompanySector { get; set; }
        public int NumberOfEmployees { get; set; }
    }
}
