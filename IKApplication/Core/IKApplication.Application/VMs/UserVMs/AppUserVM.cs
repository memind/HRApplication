using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace IKApplication.Application.VMs.UserVMs
{
    public class AppUserVM
    {
        public string Name { get; set; }
        public string? SecondName { get; set; }
        public string Surname { get; set; }
        public string Title { get; set; }
        public string? BloodGroup { get; set; }
        public string? Profession { get; set; }
        public DateTime BirthDate { get; set; }
        public string IdentityId { get; set; }
        public string ImagePath { get; set; }
        [NotMapped]
        public IFormFile? UploadPath { get; set; }
    }
}
