using IKApplication.Application.AbstractServices;
using IKApplication.Application.VMs.ExcelVMs;
using IKApplication.Domain.Entites;
using IKApplication.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace IKApplication.MVC.Areas.CompanyAdministrator.Controllers
{
    [Area("CompanyAdministrator")]
    [Authorize(Roles = "Company Administrator")]
    public class ReportController : Controller
    {
        private readonly IAppUserService _appUserService;
        private readonly IReportService _reportService;

        public ReportController(IAppUserService userService, IReportService reportService)
        {
            _appUserService = userService;
            _reportService = reportService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var user = await _appUserService.GetCurrentUserInfo(User.Identity.Name);

            var reports = await _reportService.GetAllReportsByCompanyId(user.CompanyId);

            return View(reports);
        }

        [HttpGet]
        public async Task<FileResult> Download(Guid reportId)
        {
            var ReportPath = await _reportService.GetReportPathById(reportId);

            var fileType = await _reportService.GetFileTypeById(reportId);
            var fileTypeString = "application/pdf";

            if (fileType == FileType.xls)
                fileTypeString = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

            return new FileStreamResult(new FileStream(ReportPath, FileMode.Open, FileAccess.Read), fileTypeString);
        }
    }
}
