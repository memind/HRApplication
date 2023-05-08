using IKApplication.Domain.Enums;

namespace IKApplication.Domain.Entites
{
    public class Sector : IBaseEntity
    {
        // Implement IBaseEntity
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DeleteDate { get; set; }
        public Status Status { get; set; }

        // Entity Properties
        public Guid Id { get; set; }
        public string Name { get; set; }

        // Navigation Properties
        public List<Company> Companies { get; set; }

        // Create new Lists in constructor
        public Sector()
        {
            Companies = new List<Company>();
        }
    }
}
