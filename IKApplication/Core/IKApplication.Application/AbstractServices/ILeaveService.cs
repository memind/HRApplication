using IKApplication.Application.DTOs.LeaveDTOs;
using IKApplication.Application.VMs.LeaveVMs;
using IKApplication.Domain.Entites;

namespace IKApplication.Application.AbstractServices
{
    public interface ILeaveService
    {
        Task Create(CreateLeaveDTO model, string userName);
        Task Update(UpdateLeaveDTO model);
        Task Delete(Guid id);
        Task<List<LeaveVM>> GetAllLeaves(Guid companyId);
        Task<List<LeaveVM>> GetPersonelLeaves(string userName);
        Task<List<LeaveVM>> GetLeaveRequests(Guid companyId);
        Task<LeaveVM> GetVMById(Guid id);
        Task AcceptLeave(LeaveVM model);
        Task<UpdateLeaveDTO> GetByID(Guid id);
        Task<string> GetPersonalName(Guid id);
        Task<int> GetRemainingLeaveDays(Guid appUserId);
    }
}
