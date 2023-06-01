using IKApplication.Domain.Enums;

namespace IKApplication.Application.VMs.ReportVMs
{
    public class ReportVM
    {
        public Guid Id { get; set; }
        public DateTime CreateDate { get; set; }
        public string Name { get; set; }
        public string CreatorName { get; set; }
        public FileType FileType { get; set; }
    }
}
