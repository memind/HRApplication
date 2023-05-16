using AutoMapper;
using IKApplication.Application.AbstractRepositories;
using IKApplication.Application.AbstractServices;
using IKApplication.Application.DTOs.LeaveDTOs;
using IKApplication.Domain.Entites;
using IKApplication.Domain.Enums;

namespace IKApplication.Infrastructure.ConcreteServices
{
    public class LeaveService : ILeaveService
    {
        private readonly ILeaveRepository _leaveRepository;
        private readonly IMapper _mapper;
        private readonly ILeaveService _leaveService;
        private readonly IAppUserRepository _userRepository;
        public LeaveService(ILeaveRepository leaveRepository, IMapper mapper, ILeaveService leaveService, IAppUserRepository userRepository)
        {
            _leaveRepository = leaveRepository;
            _mapper = mapper;
            _leaveService = leaveService;
            _userRepository = userRepository;
        }

        public async Task Create(LeaveDTO leave)
        {
            var model = _mapper.Map<Leave>(leave);
            await _leaveRepository.Create(model);
        }


        public async Task Delete(Guid id)
        {
            Leave leave = await _leaveRepository.GetDefault(x => x.Id == id);
            if (leave != null)
            {
                leave.DeleteDate = DateTime.UtcNow;
                leave.Status = Status.Deleted;
                await _leaveRepository.Delete(leave);
            }
        }

        public async Task<List<LeaveDTO>> GetActiveLeaves()
        {
            return await _leaveRepository.GetFilteredList(
                select: x => new LeaveDTO
                {
                    Id = x.Id,
                    StartDate = x.StartDate,
                    EndDate = x.EndDate,
                    Explanation = x.Explanation,
                    LeaveType = x.LeaveType,
                    CreateDate = x.CreateDate,
                    UpdateDate = x.UpdateDate,
                    DeleteDate = x.DeleteDate,
                    Status = x.Status,
                    AppUserId = x.AppUserId

                },
                where: x => (x.Status == Status.Active),
                orderBy: x => x.OrderBy(x => x.CreateDate)
                );
        }


        public async Task<List<LeaveDTO>> GetById(Guid id)
        {
            var user = await _userRepository.GetDefault(x => x.Id == id);

            if (user != null)
            {
                List<LeaveDTO> leaves = new List<LeaveDTO>();
                foreach (var leave in user.Leaves)
                {
                    var leavemap = _mapper.Map<LeaveDTO>(leave);
                    leaves.Add(leavemap);
                }

                return leaves;
            }
            return null;
        }

        public async Task Update(LeaveDTO leave)
        {
            var model = _mapper.Map<Leave>(leave);
            await _leaveRepository.Create(model);
        }
    }
}
