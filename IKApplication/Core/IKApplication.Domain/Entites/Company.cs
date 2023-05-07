﻿using IKApplication.Domain.Enums;

namespace IKApplication.Domain.Entites
{
    public class Company  : IBaseEntity
    {
        // Implement IBaseEntity
        public Guid Id { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeleteDate { get; set; }
        public Status Status { get; set; }

        // Company Properties
        public string Name { get; set; }
        public string Email { get; set;}
        public string PhoneNumber { get; set; }
        public int NumberOfEmployees { get; set; }
        public Guid SectorId { get; set; }

        // Navigation Properties
        public Sector Sector { get; set; }
        public List<AppUser> CompanyManagers { get; set; }
        public List<Title> Titles { get; set; }

        // Create new Lists in constructor
        public Company()
        {
            CompanyManagers = new List<AppUser>();
            Titles = new List<Title>();
        }
    }
}
