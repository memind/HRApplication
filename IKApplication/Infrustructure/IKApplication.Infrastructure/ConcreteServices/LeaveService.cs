using AutoMapper;
using IKApplication.Application.AbstractRepositories;
using IKApplication.Application.AbstractServices;
using IKApplication.Application.dtos.ExpenseDTOs;
using IKApplication.Application.DTOs.LeaveDTOs;
using IKApplication.Application.VMs.ExpenseVMs;
using IKApplication.Application.VMs.LeaveVMs;
using IKApplication.Domain.Entites;
using IKApplication.Domain.Enums;
using IKApplication.Persistance.ConcreteRepositories;
using Microsoft.EntityFrameworkCore;

namespace IKApplication.Infrastructure.ConcreteServices
{
    public class LeaveService : ILeaveService
    {
        private readonly IMapper _mapper;
        private readonly ILeaveRepository _leaveRepository;
        private readonly IAppUserRepository _appUserRepository;
        private readonly IAppUserService _appUserService;

        public LeaveService(IMapper mapper, ILeaveRepository leaveRepository, IAppUserRepository appUserRepository, IAppUserService appUserService)
        {
            _mapper = mapper;
            _leaveRepository = leaveRepository;
            _appUserRepository = appUserRepository;
            _appUserService = appUserService;
        }

        public async Task Create(CreateLeaveDTO model, string userName)
        {
            Leave leaveRequest = _mapper.Map<Leave>(model);
            leaveRequest.AppUserId = await _appUserService.GetUserId(userName);
            await _leaveRepository.Create(leaveRequest);
        }

        public async Task Delete(Guid id)
        {
            Leave leave = await _leaveRepository.GetDefault(x => x.Id == id);
            leave.Status = Status.Deleted;
            leave.DeleteDate = DateTime.Now;
            await _leaveRepository.Delete(leave);
        }

        public async Task<UpdateLeaveDTO> GetByID(Guid id)
        {
            Leave leaveRequest = await _leaveRepository.GetDefault(x => x.Id == id);

            var model = _mapper.Map<UpdateLeaveDTO>(leaveRequest);

            return model;
        }

        public async Task<List<LeaveVM>> GetLeaves()
        {
            List<LeaveVM> leaves = (List<LeaveVM>)await _leaveRepository.GetFilteredList(

              select: x => new LeaveVM()
              {
                  Id = x.Id,
                  StartDate = x.StartDate,
                  EndDate = x.EndDate,
                  Explanation = x.Explanation,
                  Status = x.Status,
                  CreateDate = x.CreateDate.ToShortDateString(),
                  AppUserId = x.AppUser.Id
              },
            where: null,
            orderBy: x => x.OrderByDescending(x => x.CreateDate),
            include: x => x.Include(x => x.AppUser)
              );
            return leaves;
        }



        public async Task<List<LeaveVM>> GetPersonelLeaves(string userName)
        {
            var user = await _appUserService.GetByUserName(userName);
            var leaves = await _leaveRepository.GetFilteredList(
            select: x => new LeaveVM()
            {
                Id = x.Id,
                StartDate = x.StartDate,
                EndDate = x.EndDate,
                Explanation = x.Explanation,
                Status = x.Status,
                CreateDate = x.CreateDate.ToShortDateString(),
                AppUserId = x.AppUser.Id,
            },
                where: x => x.AppUserId == user.Id && x.Status != Status.Passive && x.Status != Status.Deleted,
                orderBy: x => x.OrderByDescending(x => x.CreateDate),
                include: x => x.Include(x => x.AppUser));

            return leaves;
        }

        public async Task Update(UpdateLeaveDTO model)
        {
            //Leave leaveRequest = _mapper.Map<Leave>(model);
            //leaveRequest.UpdateDate = DateTime.Now;
            //leaveRequest.Status = Status.Modified;
            //if (leaveRequest != null)
            //{
            //    await _leaveRepository.Update(leaveRequest);
            //}


            var leave = await _leaveRepository.GetDefault(x => x.Id == model.Id);
            if (leave != null)
            {
                leave.UpdateDate = model.UpdateDate;
                leave.CreateDate = model.CreateDate;
                leave.DeleteDate = model.DeleteDate;
                leave.Status = model.Status;
                leave.StartDate = model.StartDate;
                leave.EndDate = model.EndDate;
                leave.Explanation = model.Explanation;
                leave.LeaveStatus = model.LeaveStatus;
                leave.AppUserId = model.AppUserId;
                await _leaveRepository.Update(leave);
            }
        }

        public async Task<List<LeaveVM>> GetLeaveRequests(Guid companyId)
        {
            var leaves = await _leaveRepository.GetDefaults(x => x.Status == Status.Passive);
            List<LeaveVM> companyLeaves = new List<LeaveVM>();

            foreach (var leave in leaves)
            {
                if (leave.CompanyId == companyId)
                {
                    var leaveMap = _mapper.Map<LeaveVM>(leave);
                    companyLeaves.Add(leaveMap);
                }
            }
            return (companyLeaves);
        }

        public async Task<LeaveVM> GetVMById(Guid id)
        {
            var leave = await _leaveRepository.GetDefault(x => x.Id == id);
            if (leave != null)
            {
                var map = _mapper.Map<LeaveVM>(leave);
                return map;
            }
            return null;
        }

        public async Task<List<LeaveVM>> GetAllLeaves(Guid companyId)
        {
            var leaves = await _leaveRepository.GetDefaults(x => x.Status == Status.Active || x.Status == Status.Modified);
            List<LeaveVM> companyLeaves = new List<LeaveVM>();

            foreach (var leave in leaves)
            {
                if (leave.CompanyId == companyId)
                {
                    var leaveMap = _mapper.Map<LeaveVM>(leave);
                    companyLeaves.Add(leaveMap);
                }
            }
            return (companyLeaves);
        }

        public async Task<string> GetPersonalName(Guid id)
        {
            var user = await _appUserService.GetById(id);
            var name = $"{user.Name} {user.SecondName} {user.Surname}";

            return name;
        }
    }
}