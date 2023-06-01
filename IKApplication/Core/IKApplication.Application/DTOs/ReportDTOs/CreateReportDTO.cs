using IKApplication.Domain.Enums;

namespace IKApplication.Application.DTOs.ReportDTOs
{
    public class CreateReportDTO
    {
        public DateTime CreateDate => DateTime.Now;
        public Status Status => Status.Active;

        // Entity Properties
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ReportPath { get; set; }
        public Guid CreatorId { get; set; }
        public FileType FileType { get; set; }
    }
}
