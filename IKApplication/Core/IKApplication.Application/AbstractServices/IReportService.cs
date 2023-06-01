using IKApplication.Application.DTOs.ReportDTOs;
using IKApplication.Application.VMs.ReportVMs;
using IKApplication.Domain.Enums;

namespace IKApplication.Application.AbstractServices
{
    public interface IReportService
    {
        Task Create(CreateReportDTO createReportDTO);
        Task Delete(Guid id);
        Task<List<ReportVM>> GetAllReportsByCompanyId(Guid id);
        Task<string> GetReportPathById(Guid id);
        Task<FileType> GetFileTypeById(Guid id);
    }
}
