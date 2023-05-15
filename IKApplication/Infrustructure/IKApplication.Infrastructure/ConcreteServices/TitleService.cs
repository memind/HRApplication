using AutoMapper;
using IKApplication.Application.AbstractRepositories;
using IKApplication.Application.AbstractServices;
using IKApplication.Application.DTOs.TitleDTOs;
using IKApplication.Application.VMs.TitleVMs;
using IKApplication.Domain.Entites;
using IKApplication.Domain.Enums;

namespace IKApplication.Infrastructure.ConcreteServices
{
    public class TitleService : ITitleService
    {
        ITitleRepository _titleRepository;
        IMapper _mapper;

        public TitleService(ITitleRepository titleRepository, IMapper mapper)
        {
            _titleRepository = titleRepository;
            _mapper = mapper;
        }

        public async Task Create(TitleCreateDTO createTitleDTO)
        {
            var model = _mapper.Map<Title>(createTitleDTO);
            await _titleRepository.Create(model);
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
