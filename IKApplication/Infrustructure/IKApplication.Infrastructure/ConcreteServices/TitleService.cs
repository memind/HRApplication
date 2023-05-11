using IKApplication.Application.AbstractRepositories;
using IKApplication.Application.AbstractServices;
using IKApplication.Application.VMs.TitleVMs;
using IKApplication.Domain.Enums;

namespace IKApplication.Infrastructure.ConcreteServices
{
    public class TitleService : ITitleService
    {
        ITitleRepository _titleRepository;

        public TitleService(ITitleRepository titleRepository)
        {
            _titleRepository = titleRepository;
        }

        public async Task<List<TitleVM>> GetAllTitles()
        {
            return await _titleRepository.GetFilteredList(
                    select: x => new TitleVM
                    {
                        Id = x.Id,
                        Name = x.Name,
                        CompanyId = x.CompanyId
                    },
                    where: x => (x.Status == Status.Active || x.Status == Status.Modified),
                    orderBy: x => x.OrderBy(x => x.CreateDate));
        }
    }
}
