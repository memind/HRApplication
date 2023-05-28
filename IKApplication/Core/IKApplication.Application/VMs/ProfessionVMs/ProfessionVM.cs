
using IKApplication.Domain.Enums;

namespace IKApplication.Application.VMs.ProfessionVMs
{
    public class ProfessionVM
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Status Status { get; set; }
        public Guid CompanyId { get; set; }
    }
}
