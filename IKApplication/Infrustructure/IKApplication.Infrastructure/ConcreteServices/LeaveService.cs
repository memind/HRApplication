﻿using AutoMapper;
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
            var map = _mapper.Map<Leave>(model);
            var leaveFor = await _appUserService.GetCurrentUserInfo((Guid)model.AppUserId);


            map.ApprovedById = (Guid)leaveFor.PatronId;
            map.AppUserId = leaveFor.Id;
            map.CompanyId = leaveFor.CompanyId;

            await _leaveRepository.Create(map);
        }

        public async Task Update(UpdateLeaveDTO model)
        {
            var leave = await _leaveRepository.GetDefault(x => x.Id == model.Id);
            if (leave != null)
            {
                leave.Status = Status.Passive;
                leave.UpdateDate = model.UpdateDate;
                leave.DeleteDate = model.DeleteDate;
                leave.Explanation = model.Explanation;
                leave.LeaveStatus = model.LeaveStatus;
                leave.StartDate = model.StartDate;
                leave.EndDate = model.EndDate;
                leave.TotalLeaveDays = model.TotalLeaveDays;
                await _leaveRepository.Update(leave);
            }
        }

        public async Task Delete(Guid id)
        {
            var leave = await _leaveRepository.GetDefault(x => x.Id == id);
            leave.DeleteDate = DateTime.Now;
            leave.Status = Status.Deleted;
            await _leaveRepository.Delete(leave);
        }

        public async Task<List<LeaveVM>> GetAllLeaves(Guid companyId)
        {
            var companyLeaves = await _leaveRepository.GetFilteredList
                (
                    select: x => new LeaveVM()
                    {
                        Id = x.Id,
                        Status = x.Status,
                        StartDate = x.StartDate,
                        EndDate = x.EndDate,
                        Explanation = x.Explanation,
                        LeaveStatus = x.LeaveStatus,
                        CreateDate = x.CreateDate,
                        UpdateDate = x.UpdateDate,
                        AppUserId = x.AppUserId,
                        CompanyId = x.CompanyId,
                        AppUser = x.AppUser,
                        LeaveType = x.LeaveType,
                        ApprovedBy = x.ApprovedBy,
                        ApprovedById = x.ApprovedById,
                        TotalLeaveDays = x.TotalLeaveDays
                    },
                    where: x => x.CompanyId == companyId && (x.Status != Status.Deleted),
                    orderBy: x => x.OrderBy(x => x.CreateDate),
                    include: x => x.Include(x => x.AppUser).Include(x => x.ApprovedBy)
                ) ;

            return companyLeaves;
        }

        public async Task<UpdateLeaveDTO> GetByID(Guid id)
        {
            var leave = await _leaveRepository.GetFilteredFirstOrDefault
                (
                    select: x => new UpdateLeaveDTO()
                    {
                        Id = x.Id,
                        StartDate = x.StartDate,
                        AppUserId = x.AppUserId,
                        EndDate = x.EndDate,
                        Explanation = x.Explanation,
                        LeaveStatus = x.LeaveStatus,
                        LeaveType = x.LeaveType,
                        TotalLeaveDays = x.TotalLeaveDays
                    },
                    where: x => x.Id == id && (x.Status != Status.Deleted),
                    orderBy: x => x.OrderBy(x => x.CreateDate),
                    include: x => x.Include(x => x.AppUser).Include(x => x.ApprovedBy)
                );

            return leave;
        }

        public async Task<List<LeaveVM>> GetLeaveRequests(Guid companyId)
        {
            var companyLeaves = await _leaveRepository.GetFilteredList
                (
                    select: x => new LeaveVM()
                    {
                        Id = x.Id,
                        Status = x.Status,
                        StartDate = x.StartDate,
                        EndDate = x.EndDate,
                        Explanation = x.Explanation,
                        LeaveStatus = x.LeaveStatus,
                        CreateDate = x.CreateDate,
                        UpdateDate = x.UpdateDate,
                        AppUserId = x.AppUserId,
                        AppUser = x.AppUser,
                        CompanyId = x.CompanyId,
                        LeaveType = x.LeaveType,
                        ApprovedBy = x.ApprovedBy,
                        ApprovedById = x.ApprovedById,
                        TotalLeaveDays = x.TotalLeaveDays
                    },
                    where: x => x.Status == Status.Passive && x.AppUser.CompanyId == companyId && x.ApprovedById == x.AppUser.PatronId,
                    orderBy: x => x.OrderBy(x => x.CreateDate),
                    include: x => x.Include(x => x.AppUser).Include(x => x.ApprovedBy)
                );

            return companyLeaves;
        }

        public async Task<List<LeaveVM>> GetPersonelLeaves(string userName)
        {
            var companyLeaves = await _leaveRepository.GetFilteredList
                (
                    select: x => new LeaveVM()
                    {
                        Id = x.Id,
                        Status = x.Status,
                        StartDate = x.StartDate,
                        EndDate = x.EndDate,
                        Explanation = x.Explanation,
                        LeaveStatus = x.LeaveStatus,
                        CreateDate = x.CreateDate,
                        UpdateDate = x.UpdateDate,
                        AppUserId = x.AppUserId,
                        AppUser = x.AppUser,
                        LeaveType = x.LeaveType,
                        CompanyId = x.CompanyId,
                        ApprovedBy = x.ApprovedBy,
                        ApprovedById = x.ApprovedById,
                        TotalLeaveDays = x.TotalLeaveDays
                    },
                    where: x => x.AppUser.UserName == userName && x.Status != Status.Deleted,
                    orderBy: x => x.OrderBy(x => x.CreateDate),
                    include: x => x.Include(x => x.AppUser).Include(x => x.ApprovedBy)
                );

            return companyLeaves;
        }

        public async Task<string> GetPersonalName(Guid id)
        {
            var user = await _appUserService.GetById(id);
            var name = $"{user.Name} {user.SecondName} {user.Surname}";

            return name;
        }

        public async Task<LeaveVM> GetVMById(Guid id)
        {
            var leave = await _leaveRepository.GetFilteredFirstOrDefault
                (
                    select: x => new LeaveVM()
                    {
                        Id = x.Id,
                        Status = x.Status,
                        StartDate = x.StartDate,
                        EndDate = x.EndDate,
                        Explanation = x.Explanation,
                        LeaveStatus = x.LeaveStatus,
                        CreateDate = x.CreateDate,
                        UpdateDate = x.UpdateDate,
                        AppUserId = x.AppUserId,
                        AppUser = x.AppUser,
                        LeaveType = x.LeaveType,
                        CompanyId = x.CompanyId,
                        ApprovedBy = x.ApprovedBy,
                        ApprovedById = x.ApprovedById,
                        TotalLeaveDays = x.TotalLeaveDays
                    },
                    where: x => x.Id == id && x.Status != Status.Deleted,
                    orderBy: x => x.OrderBy(x => x.CreateDate),
                    include: x => x.Include(x => x.AppUser).Include(x => x.ApprovedBy).Include(x => x.AppUser.Company).Include(x => x.AppUser.Patron)
                );

            return leave;
        }

        public async Task AcceptLeave(LeaveVM model)
        {
            var map = _mapper.Map<Leave>(model);
            map.Status = Status.Active;
            map.AppUser = null;
            map.ApprovedBy = null;
            await _leaveRepository.Update(map);
        }

        public async Task<int> GetCurrentUsersTotals(Guid userId)
        {
            var user = await _appUserService.GetById(userId);
            var usersLeaves = await GetPersonelLeaves(user.Email);
            var userTotal = 0;

            foreach (var leave in usersLeaves)
            {
                userTotal += leave.TotalLeaveDays;
            }

            return userTotal;
        }

        public async Task<List<LeaveVM>> GetPersonelLeaves(Guid id)
        {
            var companyLeaves = await _leaveRepository.GetFilteredList
                (
                    select: x => new LeaveVM()
                    {
                        Id = x.Id,
                        Status = x.Status,
                        CreateDate = x.CreateDate,
                        UpdateDate = x.UpdateDate,
                        CompanyId = x.CompanyId,
                        StartDate = x.StartDate,
                        EndDate = x.EndDate,
                        Explanation = x.Explanation,
                        LeaveStatus = x.LeaveStatus,
                        ApprovedById = x.ApprovedById,
                        ApprovedBy = x.ApprovedBy,
                        AppUserId = x.AppUserId,
                        AppUser = x.AppUser,
                        LeaveType = x.LeaveType,
                        TotalLeaveDays = x.TotalLeaveDays
                    },
                    where: x => x.ApprovedById == id && x.Status != Status.Deleted,
                    orderBy: x => x.OrderBy(x => x.CreateDate),
                    include: x => x.Include(x => x.AppUser).Include(x => x.ApprovedBy)
                );

            return companyLeaves;
        }
    }
}
