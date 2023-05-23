using AutoMapper;
using IKApplication.Application.AbstractRepositories;
using IKApplication.Application.AbstractServices;
using IKApplication.Application.dtos.TitleDTOs;
using IKApplication.Application.DTOs.TitleDTOs;
using IKApplication.Application.VMs.ExpenseVMs;
using IKApplication.Application.VMs.TitleVMs;
using IKApplication.Domain.Entites;
using IKApplication.Domain.Enums;
using IKApplication.Persistance.ConcreteRepositories;
using Microsoft.EntityFrameworkCore;

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

        public async Task Update(TitleUpdateDTO titleUpdateDTO)
        {
            var title = await _titleRepository.GetDefault(x => x.Id == titleUpdateDTO.Id);

            if (title != null)
            {
                title.Name = titleUpdateDTO.Name;
                title.UpdateDate = titleUpdateDTO.UpdateDate;
                title.Status = titleUpdateDTO.Status;

                await _titleRepository.Update(title);
            }
        }

        public async Task Delete(Guid titleId)
        {
            var title = await _titleRepository.GetDefault(x => x.Id == titleId);

            title.DeleteDate = DateTime.Now;
            title.Status = Status.Deleted;

            await _titleRepository.Delete(title);
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

        public async Task<TitleVM> GetVMById(Guid id)
        {
            var title = await _titleRepository.GetDefault(x => x.Id == id);
            var map = _mapper.Map<TitleVM>(title);

            return map;
        }

        public async Task<List<TitleVM>> GetCompanyTitles(Guid companyId)
        {
            var companyTitles = await _titleRepository.GetFilteredList
                (
                    select: x => new TitleVM()
                    {
                        Id = x.Id,
                        Name = x.Name,
                        CompanyId = x.CompanyId
                    },
                    where: x => x.CompanyId == companyId && (x.Status != Status.Deleted),
                    orderBy: x => x.OrderBy(x => x.Name),
                    include: x => x.Include(x => x.Company)
                );

            return companyTitles;
        }

        public async Task<TitleUpdateDTO> GetById(Guid id)
        {
            var expense = await _titleRepository.GetFilteredFirstOrDefault
                (
                    select: x => new TitleUpdateDTO()
                    {
                        Id = x.Id,
                        Name = x.Name,
                        CompanyId = x.CompanyId
                    },
                    where: x => x.Id == id && (x.Status != Status.Deleted),
                    orderBy: null,
                    include: x => x.Include(x => x.Company)
                );

            return expense;
        }
    }
}
