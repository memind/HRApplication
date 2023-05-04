using IKApplication.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace IKApplication.Domain.Entites
{
    public class AppUser : IdentityUser<Guid>, IBaseEntity
    {
        // Implement IBaseEntity
        // IdentityUser  den Id yi aldığı için default olarak id si string guid kullanalım Id olarak
        public DateTime CreateDate { get; set ; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeleteDate { get; set; }
        public Status Status { get; set; }

        // AppUser Properties
        public string Name { get; set; }
        public string? SecondName { get; set; }
        public string Surname { get; set; }
        public string Title { get; set; }   // Unvan
        public string? BloodGroup { get; set; }   // kan grubu
        public string? Profession { get; set; }   //meslek
        public DateTime BirthDate { get; set; }
        public string IdentityId { get; set; }  // TC Kimlik No - Pasaport no vb.
        public string ImagePath { get; set; } = $"/images/defaultuser.jpg";  // Fotoğraf Yolu
        [NotMapped]   // DB ile bağlantı olmasın
        public IFormFile? UploadPath { get; set; }  // IFormFile usinglere eklendi.  (using Microsoft.AspNetCore.Http;)  bir tane resim seçeceğiz resmi tutabilmek için bunu kullanacağız. Veritabanı ile bağlantı olmadan.  ImagePath veritabanı ile bağlantılı olacak, resmin yolu
        public Guid? CompanyId { get; set; }

        // Navigation Properties
        public Company? Company { get; set; }
    }
}
