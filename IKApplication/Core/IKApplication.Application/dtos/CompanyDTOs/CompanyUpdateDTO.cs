using IKApplication.Domain.Enums;

namespace IKApplication.Application.dtos.CompanyDTOs
{
    public class CompanyUpdateDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Sector { get; set; }
        public int NumberOfEmployees { get; set; }
        public DateTime UpdateDate { get; set; } = DateTime.Now;
        public Status Status { get; set; } = Status.Modified;
        //public List<AppUser>? CompanyManagers { get; set; }
    }
}
