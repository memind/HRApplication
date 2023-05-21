using IKApplication.Domain.Enums;

namespace IKApplication.Application.DTOs.TitleDTOs
{
    public class TitleCreateDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid CompanyId { get; set; }
        public DateTime CreateDate => DateTime.Now;
        public Status Status => Status.Active;
    }
}
