using IKApplication.Domain.Entites;
using IKApplication.Domain.Enums;

namespace IKApplication.Application.DTOs.CompanyDTOs
{
    public class CompanyCreateDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public Sector Sector { get; set; }
        public int NumberOfEmployees { get; set; }
        public DateTime CreateDate => DateTime.Now;
        public Status Status => Status.Active;
    }
}
