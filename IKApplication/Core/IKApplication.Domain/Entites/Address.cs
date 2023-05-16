using IKApplication.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKApplication.Domain.Entites
{
    public class Address : IBaseEntity
    {
        // Implement
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeleteDate { get; set; }
        public Status Status { get; set; }

        // Properties
        public Guid Id { get; set; }
        public string OpenAddress { get; set; }
        public string PostCode { get; set; }


        // Navigation Properties
        public City City { get; set; }
        public District? District { get; set; }
        public AppUser AppUser { get; set; }
        public Guid AppUserId { get; set; }

    }
}