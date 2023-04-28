using IKApplication.Application.DTOs.DashBoardDTOs;

namespace IKApplication.Application.AbstractServices.DashboardServices
{
    public interface IDashboardService
    {
        Task<int> GetFirmsCount();
        Task<int> GetFirmAdminsCount();
        Task<List<DashboardFirmsCountBySectorDto>> GetFirmsBySector();
    }
}
