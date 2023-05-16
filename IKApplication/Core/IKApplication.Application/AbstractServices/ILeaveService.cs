using IKApplication.Application.DTOs.LeaveDTOs;
using IKApplication.Domain.Entites;

namespace IKApplication.Application.AbstractServices
{
    public interface ILeaveService
    {
        Task<List<LeaveDTO>> GetById(Guid id);
        Task<List<LeaveDTO>> GetActiveLeaves();
        Task Create(LeaveDTO leave);
        Task Update(LeaveDTO leave);
        Task Delete(Guid id);
    }
}
