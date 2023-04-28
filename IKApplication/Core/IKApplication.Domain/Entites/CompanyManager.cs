using IKApplication.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace IKApplication.Domain.Entites
{
    public class CompanyManager : IBaseEntity
    {
        // Implement
        public DateTime CreateDate { get; set ; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeleteDate { get; set; }
        public Status Status { get; set; }


        // Eklediğimiz Property ler
        public int Id { get; set; }
        public string Name { get; set; }
        public string SecondName { get; set; }
        public string Surname { get; set; }
        public string EMail { get; set; }
        public string Title { get; set; }   // Unvan
        public string PhoneNumber { get; set; }
        public string BloodGroup { get; set; }   // kan grubu
        public string Profession { get; set; }   //meslek
        public DateTime BirthDate { get; set; }
        public string Password { get; set; }
        public string IdentityId { get; set; }  // TC Kimlik No - Pasaport no vb.
        public string ImagePath { get; set; }  // Fotoğraf Yolu

        [NotMapped]   // DB ile bağlantı olmasın
        public IFormFile UploadPath { get; set; }  // IFormFile usinglere eklendi.  (using Microsoft.AspNetCore.Http;)  bir tane resim seçeceğiz resmi tutabilmek için bunu kullanacağız. Veritabanı ile bağlantı olmadan.  ImagePath veritabanı ile bağlantılı olacak, resmin yolu


        // Navigation Property 'ler yazılacak. List varsa ctor da new lenecek

    }
}
