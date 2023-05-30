using IKApplication.Domain.Entites;
using IKApplication.Domain.Enums;

namespace IKApplication.Application.VMs.CompanyVMs
{
    public class CompanyVM
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string SectorName { get; set; }
        public int NumberOfEmployees { get; set; }
        public string ContactPerson { get; set; }
        public Status Status { get; set; }
    }
}
