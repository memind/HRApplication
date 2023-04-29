using IKApplication.Domain.Enums;

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
        public string Sector { get; set; }
        public int NumberOfEmployees { get; set; }

        // Navigation Properties
        public List<AppUser> CompanyManagers { get; set; }

        // Create new Lists in constructor
        public Company()
        {
            CompanyManagers = new List<AppUser>();
        }
    }
}
