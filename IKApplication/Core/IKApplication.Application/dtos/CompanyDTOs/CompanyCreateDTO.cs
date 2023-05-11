using IKApplication.Application.VMs.SectorVMs;
using IKApplication.Domain.Enums;

namespace IKApplication.Application.DTOs.CompanyDTOs
{
    public class CompanyCreateDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int NumberOfEmployees { get; set; }
        public Guid SectorId { get; set; }
        public DateTime CreateDate => DateTime.Now;
        public Status Status => Status.Passive;
        public List<SectorVM>? Sectors { get; set; }
    }
}
