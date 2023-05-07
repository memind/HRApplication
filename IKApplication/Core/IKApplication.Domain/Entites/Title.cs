using IKApplication.Domain.Enums;

namespace IKApplication.Domain.Entites
{
    public class Title : IBaseEntity
    {
        // Interface properties
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeleteDate { get; set; }
        public Status Status { get; set; } = Status.Active;

        // Entity properties
        public Guid Id { get; set; }
        public string TitleName { get; set; }

        // Navigation properties
        public Company Company { get; set; }
    }
}
