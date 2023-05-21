using IKApplication.Application.AbstractServices;
using IKApplication.Application.DTOs.CompanyDTOs;
using IKApplication.Application.VMs.CompanyVMs;
using IKApplication.Application.VMs.UserVMs;
using IKApplication.Infrastructure.ConcreteServices;
using IKApplication.MVC.ResultMessages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using OfficeOpenXml;
using static IKApplication.MVC.ResultMessages.Messages;

namespace IKApplication.MVC.Areas.CompanyAdministrator.Controllers
{
    [Area("SiteAdministrator")]
    [Authorize(Roles = "Site Administrator")]
    public class CompanyController : Controller
    {
        private readonly ICompanyService _companyService;
        private readonly IToastNotification _toast;
        public CompanyController(ICompanyService companyService, IToastNotification toast)
        {
            _companyService = companyService;
            _toast = toast;
        }

        public async Task<IActionResult> Index()
        {
            var companies = await _companyService.GetAllCompanies();
            return View(companies);
        }

        public async Task<IActionResult> Update(Guid id)
        {
            return View(await _companyService.GetById(id));
        }

        [HttpPost]
        public async Task<IActionResult> Update(CompanyUpdateDTO updateCompanyDTO)
        {
            if (ModelState.IsValid)
            {
                await _companyService.Update(updateCompanyDTO);
                _toast.AddSuccessToastMessage(Company.Update(updateCompanyDTO.Name), new ToastrOptions { Title = "Updating Company" });
                return RedirectToAction("Index", "Dashboard");
            }

            _toast.AddErrorToastMessage(Errors.Error(), new ToastrOptions { Title = "Updating Company" });
            return View(updateCompanyDTO);
        }

        [HttpGet]
        public async Task<IActionResult> CompanyExcel()
        {
            var stream = new MemoryStream();

            var date = DateTime.Now;
            var startDate = new DateTime(date.Year, date.Month, 1);
            var endDate = new DateTime(date.Year, date.Month + 1, 1);

            List<CompanyVM> allCompanies = await _companyService.GetAllCompanies();

            ExcelPackage pck = new ExcelPackage(stream);
            ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Companies");

            ws.Cells["A1"].Value = "All Companies";
            ws.Cells["A1"].Style.Font.Bold = true;

            ws.Cells["A2"].Value = "Date";
            ws.Cells["B2"].Value = $"{date.Month} - {date.Year}";

            ws.Cells["A5"].Value = "Company Name";
            ws.Cells["B5"].Value = "Email Address";
            ws.Cells["C5"].Value = "Phone Number";
            ws.Cells["D5"].Value = "Sector Name";
            ws.Cells["E5"].Value = "Number of Employees";

            ws.Cells["A5:E5"].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            ws.Cells["A5:E5"].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            ws.Cells["A5:E5"].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            ws.Cells["A5:E5"].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            ws.Cells["A5:E5"].Style.Font.Bold = true;

            int rowStart = 6;

            foreach (var company in allCompanies)
            {
                ws.Cells[string.Format("A{0}", rowStart)].Value = company.Name;
                ws.Cells[string.Format("A{0}", rowStart)].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                ws.Cells[string.Format("A{0}", rowStart)].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                ws.Cells[string.Format("A{0}", rowStart)].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                ws.Cells[string.Format("A{0}", rowStart)].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

                ws.Cells[string.Format("B{0}", rowStart)].Value = company.Email;
                ws.Cells[string.Format("B{0}", rowStart)].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                ws.Cells[string.Format("B{0}", rowStart)].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                ws.Cells[string.Format("B{0}", rowStart)].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                ws.Cells[string.Format("B{0}", rowStart)].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

                ws.Cells[string.Format("C{0}", rowStart)].Value = company.PhoneNumber;
                ws.Cells[string.Format("C{0}", rowStart)].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                ws.Cells[string.Format("C{0}", rowStart)].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                ws.Cells[string.Format("C{0}", rowStart)].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                ws.Cells[string.Format("C{0}", rowStart)].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

                ws.Cells[string.Format("D{0}", rowStart)].Value = company.SectorName;
                ws.Cells[string.Format("D{0}", rowStart)].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                ws.Cells[string.Format("D{0}", rowStart)].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                ws.Cells[string.Format("D{0}", rowStart)].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                ws.Cells[string.Format("D{0}", rowStart)].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

                ws.Cells[string.Format("E{0}", rowStart)].Value = company.NumberOfEmployees;
                ws.Cells[string.Format("E{0}", rowStart)].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                ws.Cells[string.Format("E{0}", rowStart)].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                ws.Cells[string.Format("E{0}", rowStart)].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                ws.Cells[string.Format("E{0}", rowStart)].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

                rowStart++;
            }

            ws.Cells[string.Format("D{0}", rowStart)].Value = "Total Company Count: ";
            ws.Cells[string.Format("D{0}", rowStart)].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            ws.Cells[string.Format("D{0}", rowStart)].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            ws.Cells[string.Format("D{0}", rowStart)].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            ws.Cells[string.Format("D{0}", rowStart)].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

            ws.Cells[string.Format("E{0}", rowStart)].Value = allCompanies.Count;
            ws.Cells[string.Format("E{0}", rowStart)].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            ws.Cells[string.Format("E{0}", rowStart)].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            ws.Cells[string.Format("E{0}", rowStart)].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            ws.Cells[string.Format("E{0}", rowStart)].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                                    
            ws.Cells[string.Format("E{0}", rowStart)].Style.Font.Bold = true;
            ws.Cells[string.Format("D{0}", rowStart)].Style.Font.Bold = true;

            ws.Cells["A:AZ"].AutoFitColumns();
            pck.Save();
            stream.Position = 0;
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"All_Companies_Report_{date.Month}/{date.Year}.xlsx");
        }
    }
}
