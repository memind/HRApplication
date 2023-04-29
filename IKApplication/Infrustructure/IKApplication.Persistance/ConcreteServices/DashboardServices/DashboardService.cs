using IKApplication.Application.AbstractServices.DashboardServices;
using IKApplication.Application.DTOs.DashBoardDTOs;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace IKApplication.Persistance.ConcreteServices.DashboardServices
{
    public class DashboardService : IDashboardService
    {
        //private readonly IFirmService _firmService;
        //private readonly IFirmAdminService _firmAdminService;
        //public DashboardService(IFirmService firmService, IFirmAdminService firmAdminService)
        //{
        //    _firmService = firmService;
        //    _firmAdminService = firmAdminService;

        //}
        //private async Task<List<Firm>> GetAllFirms()
        //{
        //    List<Firm> firms = await _firmService.GetAll().ToList();
        //    return firms;
        //}

        //private async Task<int> GetFirmAdminsCount()
        //{
        //    int firmAdminsCount = await _firmAdminService.GetAll().CountAsync();
        //    return firmAdminsCount;
        //}

        //private async Task<int> GetFirmsCount()
        //{
        //    int firmsCount = await _firmService.GetAll().CountAsync();
        //    return firmsCount;
        //}

        //public async Task<List<DashboardFirmsCountBySectorDto>> GetFirmsBySector()
        //{
        //    // 1) Get all firms
        //    var firms = GetAllFirms();

        //    // 2) Categorize them by sector
        //    // 3) Return that count list
        //    throw new NotImplementedException();
        //}

        //public async Task<DashboardCountInfosDto> GetCountInfos()
        //{
        //    DashboardCountInfosDto infos = new();

        //    infos.FirmsCount = await GetFirmsCount();
        //    infos.FirmAdminsCount = await GetFirmAdminsCount();

        //    return infos;
        //}
    }
}
