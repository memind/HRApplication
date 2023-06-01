using AutoMapper;
using IKApplication.Application.AbstractRepositories;
using IKApplication.Application.AbstractServices;
using IKApplication.Application.dtos.CashAdvanceDTOs;
using IKApplication.Application.DTOs.ReportDTOs;
using IKApplication.Application.VMs.CashAdvanceVMs;
using IKApplication.Application.VMs.ReportVMs;
using IKApplication.Domain.Entites;
using IKApplication.Domain.Enums;
using IKApplication.Persistance.ConcreteRepositories;
using Microsoft.EntityFrameworkCore;

namespace IKApplication.Infrastructure.ConcreteServices
{
    public class ReportService : IReportService
    {
        private readonly IReportRepository _reportRepository;
        private readonly IMapper _mapper;

        public ReportService(IReportRepository reportRepository, IMapper mapper)
        {
            _reportRepository = reportRepository;
            _mapper = mapper;
        }

        public async Task Create(CreateReportDTO createReportDTO)
        {
            var model = _mapper.Map<Report>(createReportDTO);
            await _reportRepository.Create(model);
        }

        public async Task Delete(Guid id)
        {
            var company = await _reportRepository.GetDefault(x => x.Id == id);

            if (company != null)
            {
                company.DeleteDate = DateTime.Now;
                company.Status = Status.Deleted;

                await _reportRepository.Delete(company);
            }
        }

        public async Task<string> GetReportPathById(Guid id)
        {
            var reportPath = await _reportRepository.GetFilteredFirstOrDefault
                (
                    select: x => x.ReportPath,
                    where: x => x.Id == id
                );

            return reportPath;
        }

        public async Task<FileType> GetFileTypeById(Guid id)
        {
            var reportPath = await _reportRepository.GetFilteredFirstOrDefault
                (
                    select: x => x.FileType,
                    where: x => x.Id == id
                );

            return reportPath;
        }

        public async Task<List<ReportVM>> GetAllReportsByCompanyId(Guid id)
        {
            return await _reportRepository.GetFilteredList(
                select: x => new ReportVM
                {
                    Id = x.Id,
                    Name = x.Name,
                    CreateDate = DateTime.Now,
                    FileType = x.FileType,
                    CreatorName = x.Creator.Name + x.Creator.Surname,
                },
                where: x => (x.Status == Status.Active || x.Status == Status.Modified) && (x.Creator.CompanyId == id),
                orderBy: x => x.OrderBy(x => x.CreateDate),
                include: x => x.Include(x => x.Creator));
        }
    }
}
