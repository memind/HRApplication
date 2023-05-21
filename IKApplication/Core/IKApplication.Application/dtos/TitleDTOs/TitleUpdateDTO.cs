using IKApplication.Domain.Enums;

namespace IKApplication.Application.dtos.TitleDTOs
{
    public class TitleUpdateDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid CompanyId { get; set; }
        public DateTime UpdateDate => DateTime.Now;
        public Status Status => Status.Modified;
    }
}
