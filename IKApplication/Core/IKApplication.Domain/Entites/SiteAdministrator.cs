using IKApplication.Domain.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace IKApplication.Domain.Entites
{
    public class SiteAdministrator : IdentityUser, IBaseEntity  // using Microsoft.AspNetCore.Identity;
       // ("IdentityUser" sınıfı, ASP.NET Core Identity API'si tarafından sağlanan bir kullanıcı modeli sınıfıdır. Bu sınıf, kullanıcı kimlik bilgilerini depolamak için kullanılabilir ve genellikle kullanıcı yönetim işlemleriyle ilişkilendirilir.)
    {
        //  IdentityUser  den Id yi aldığı için default olarak id si string
        //  guid kullanalım Id olarak

        

        // Implement
        public DateTime CreateDate { get ; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeleteDate { get; set; }
        public Status Status { get; set; }

        // bool tipinde IsDeleted  -  IsActive


        // Eklediğimiz Property ler
        public string ImagePath { get; set; }

        [NotMapped]   // DB de oluşmasın
        public IFormFile UploadPath { get; set; }


        // Navigation Property 'ler yazılacak. List varsa ctor da new lenecek
    }
}
