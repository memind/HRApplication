using AutoMapper;
using IKApplication.Application.AbstractRepositories;
using IKApplication.Application.AbstractServices;
using IKApplication.Application.dtos.TitleDTOs;
using IKApplication.Application.DTOs.ProfessionDTOs;
using IKApplication.Application.DTOs.TitleDTOs;
using IKApplication.Application.VMs.ProfessionVMs;
using IKApplication.Application.VMs.TitleVMs;
using IKApplication.Domain.Entites;
using IKApplication.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKApplication.Infrastructure.ConcreteServices
{
    public class ProfessionService : IProfessionService
    {
        IProfessionRepository _professionRepository;
        IMapper _mapper;

        public ProfessionService(IProfessionRepository professionRepository, IMapper mapper)
        {
            _professionRepository = professionRepository;
            _mapper = mapper;
        }

        public async Task<bool> Create(ProfessionCreateDTO createProfessionDTO)
        {
            var model = _mapper.Map<Profession>(createProfessionDTO);

            var companyProfessions = await GetCompanyProfessionsWithDeleted(createProfessionDTO.CompanyId);
            bool validation = true;

            foreach (var profession in companyProfessions)
            {
                if (createProfessionDTO.Name.ToLower() == profession.Name.ToLower())
                {
                    validation = false;
                }
            }

            if (validation)
            {
                await _professionRepository.Create(model);
                return true;
            }

            return false;
        }

        public async Task<bool> Update(ProfessionUpdateDTO professionUpdateDTO)
        {
            var companyProfessions = await GetCompanyProfessionsWithDeleted(professionUpdateDTO.CompanyId);
            bool validation = true;

            foreach (var updatingProfession in companyProfessions)
            {
                if (professionUpdateDTO.Name.ToLower() == updatingProfession.Name.ToLower())
                {
                    validation = false;
                }
            }
            var profession = await _professionRepository.GetDefault(x => x.Id == professionUpdateDTO.Id);

            if (profession != null)
            {
                if (validation)
                {
                    profession.Name = professionUpdateDTO.Name;
                    profession.UpdateDate = professionUpdateDTO.UpdateDate;
                    profession.Status = professionUpdateDTO.Status;

                    await _professionRepository.Update(profession);
                    return true;
                }
            }

            return false;
        }

        public async Task Delete(Guid professionId)
        {
            var profession = await _professionRepository.GetDefault(x => x.Id == professionId);

            profession.DeleteDate = DateTime.Now;
            profession.Status = Status.Deleted;

            await _professionRepository.Delete(profession);
        }

        public async Task<List<ProfessionVM>> GetAllProfessions()
        {
            return await _professionRepository.GetFilteredList(
                    select: x => new ProfessionVM
                    {
                        Id = x.Id,
                        Name = x.Name,
                        CompanyId = x.CompanyId,
                        Status = x.Status
                    },
                    where: x => (x.Status == Status.Active || x.Status == Status.Modified),
                    orderBy: x => x.OrderBy(x => x.CreateDate));
        }

        public async Task<ProfessionVM> GetVMById(Guid id)
        {
            var profession = await _professionRepository.GetDefault(x => x.Id == id);
            var map = _mapper.Map<ProfessionVM>(profession);

            return map;
        }

        public async Task<List<ProfessionVM>> GetCompanyProfessions(Guid companyId)
        {
            var companyProfessions = await _professionRepository.GetFilteredList
                (
                    select: x => new ProfessionVM()
                    {
                        Id = x.Id,
                        Name = x.Name,
                        CompanyId = x.CompanyId,
                        Status = x.Status
                    },
                    where: x => x.CompanyId == companyId && (x.Status != Status.Deleted),
                    orderBy: x => x.OrderBy(x => x.Name),
                    include: x => x.Include(x => x.Company)
                );

            return companyProfessions;
        }

        public async Task<List<ProfessionVM>> GetCompanyProfessionsWithDeleted(Guid companyId)
        {
            var companyProfessions = await _professionRepository.GetFilteredList
                (
                    select: x => new ProfessionVM()
                    {
                        Id = x.Id,
                        Name = x.Name,
                        CompanyId = x.CompanyId,
                        Status = x.Status
                    },
                    where: x => x.CompanyId == companyId && (x.Status != Status.Passive),
                    orderBy: x => x.OrderBy(x => x.Name),
                    include: x => x.Include(x => x.Company)
                );

            return companyProfessions;
        }

        public async Task<ProfessionUpdateDTO> GetById(Guid id)
        {
            var profession = await _professionRepository.GetFilteredFirstOrDefault
                (
                    select: x => new ProfessionUpdateDTO()
                    {
                        Id = x.Id,
                        Name = x.Name,
                        CompanyId = x.CompanyId
                    },
                    where: x => x.Id == id && (x.Status != Status.Deleted),
                    orderBy: null,
                    include: x => x.Include(x => x.Company)
                );

            return profession;
        }

        public async Task Recover(Guid professionId)
        {
            var profession = await _professionRepository.GetDefault(x => x.Id == professionId);

            profession.DeleteDate = null;
            profession.Status = Status.Modified;

            await _professionRepository.Update(profession);
        }
    }
}
