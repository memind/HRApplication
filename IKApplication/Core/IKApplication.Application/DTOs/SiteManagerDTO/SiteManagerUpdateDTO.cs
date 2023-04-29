using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKApplication.Application.DTOs.SiteManagerDTO
{
    public class SiteManagerUpdateDTO
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
