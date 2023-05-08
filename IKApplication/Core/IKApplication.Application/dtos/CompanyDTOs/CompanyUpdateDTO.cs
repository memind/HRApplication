using IKApplication.Application.VMs.SectorVMs;
using IKApplication.Domain.Enums;

namespace IKApplication.Application.DTOs.CompanyDTOs
{
    public class CompanyUpdateDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int NumberOfEmployees { get; set; }
        public Guid SectorId { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate => DateTime.Now;
        public Status Status => Status.Modified;
        public List<SectorVM>? Sectors { get; set; }
    }
}
