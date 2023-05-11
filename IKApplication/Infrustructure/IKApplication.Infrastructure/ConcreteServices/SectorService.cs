using AutoMapper;
using IKApplication.Application.AbstractRepositories;
using IKApplication.Application.AbstractServices;
using IKApplication.Application.VMs.SectorVMs;
using IKApplication.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace IKApplication.Infrastructure.ConcreteServices
{
    public class SectorService : ISectorService
    {
        ISectorRepository _sectorRepository;
        IMapper _mapper;

        public SectorService(ISectorRepository sectorRepository, IMapper mapper)
        {
            _sectorRepository = sectorRepository;
            _mapper = mapper;
        }

        public async Task<List<SectorVM>> GetAllSectors()
        {
            List<SectorVM> model = new List<SectorVM>();
            int companyCount;

            var sectors = await _sectorRepository.GetFilteredList(
                    select: x => x,
                    where: x => (x.Status == Status.Active || x.Status == Status.Modified),
                    orderBy: x => x.OrderByDescending(x => x.Companies.Count),
                    include: x => x.Include(x => x.Companies));

            foreach (var sector in sectors)
            {
                companyCount = 0;
                foreach(var company in sector.Companies)
                {
                    if(company.Status == Status.Active || company.Status == Status.Modified)
                        companyCount++;
                }
                var sectorVM = _mapper.Map<SectorVM>(sector);
                sectorVM.CompanyCount = companyCount;
                model.Add(sectorVM);
            }
            return model;
        }
    }
}
