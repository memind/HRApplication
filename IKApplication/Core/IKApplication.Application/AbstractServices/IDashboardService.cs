﻿using IKApplication.Application.DTOs.DashBoardDTOs;

namespace IKApplication.Application.AbstractServices
{
    public interface IDashboardService
    {
        Task<int> GetCompaniesCount();
        Task<int> GetCompanyAdminsCount();
        Task<List<DashboardCompaniesCountBySectorDTO>> GetCompanyBySector();
        Task<DashboardCountInfosDTO> GetCountInfos();
    }
}
