﻿using IKApplication.Domain.Entites;

namespace IKApplication.Application.DTOs.CompanyDTOs
{
    public class CompanyDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Sector { get; set; }
        public int NumberOfEmployees { get; set; }
        //public List<AppUser>? CompanyManagers { get; set; }
    }
}
