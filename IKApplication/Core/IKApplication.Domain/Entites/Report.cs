using IKApplication.Domain.Enums;

namespace IKApplication.Domain.Entites
{
    public class Report : IBaseEntity
    {
        // Implement IBaseEntity
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeleteDate { get; set; }
        public Status Status { get; set; }

        // Entity Properties
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ReportPath { get; set; }
        public Guid CreatorId { get; set; }
        public FileType FileType { get; set; }

        // Navigation Properties
        public AppUser Creator { get; set; }
    }
}
