﻿using IKApplication.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace IKApplication.Domain.Entites
{
    public class AppUser : IdentityUser<Guid>, IBaseEntity
    {
        // Implement IBaseEntity
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeleteDate { get; set; }
        public Status Status { get; set; }

        // Entity Properties
        // IdentityUser  den Id yi aldığı için default olarak id si string guid kullanalım Id olarak
        public string Name { get; set; }
        public string? SecondName { get; set; }
        public string Surname { get; set; }
        public BloodGroup? BloodGroup { get; set; }   // kan grubu
        public string? Profession { get; set; }   //meslek
        [Range(typeof(DateTime), "1900-01-01", "2005-01-01", ErrorMessage = "You must be older than 18.")]
        public DateTime BirthDate { get; set; }
        public string IdentityNumber { get; set; }  // TC Kimlik No - Pasaport no vb.
        public string ImagePath { get; set; }  // Fotoğraf Yolu
        [NotMapped]   // DB ile bağlantı olmasın
        public IFormFile? UploadPath { get; set; }  // IFormFile usinglere eklendi.  (using Microsoft.AspNetCore.Http;)  bir tane resim seçeceğiz resmi tutabilmek için bunu kullanacağız. Veritabanı ile bağlantı olmadan.  ImagePath veritabanı ile bağlantılı olacak, resmin yolu
        public Guid CompanyId { get; set; }
        public Guid TitleId { get; set; }   // Unvan

        // Navigation Properties
        public Company Company { get; set; }
        public Title Title { get; set; }
    }
}
