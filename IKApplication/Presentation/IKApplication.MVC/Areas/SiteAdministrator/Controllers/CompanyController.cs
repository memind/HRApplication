using IKApplication.Application.AbstractServices;
using IKApplication.Application.DTOs.CompanyDTOs;
using IKApplication.Application.VMs.CompanyVMs;
using IKApplication.Application.VMs.ExcelVMs;
using IKApplication.Application.VMs.ExpenseVMs;
using IKApplication.Application.VMs.UserVMs;
using IKApplication.Infrastructure.ConcreteServices;
using IKApplication.MVC.ResultMessages;
using iTextSharp.text.pdf;
using iTextSharp.text;
using iTextSharp.tool.xml;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;
using OfficeOpenXml;
using System.Text;
using static IKApplication.MVC.ResultMessages.Messages;
using IKApplication.Application.DTOs.ReportDTOs;
using IKApplication.Domain.Enums;

namespace IKApplication.MVC.Areas.CompanyAdministrator.Controllers
{
    [Area("SiteAdministrator")]
    [Authorize(Roles = "Site Administrator")]
    public class CompanyController : Controller
    {
        private readonly ICompanyService _companyService;
        private readonly IToastNotification _toast;
        private readonly IReportService _reportService;
        private readonly IAppUserService _appUserService;

        public CompanyController(ICompanyService companyService, IToastNotification toast, IReportService reportService, IAppUserService appUserService)
        {
            _companyService = companyService;
            _toast = toast;
            _reportService = reportService;
            _appUserService = appUserService;
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
            var currentUser = await _appUserService.GetCurrentUserInfo(User.Identity.Name);

            ExcelPackage pck = new ExcelPackage(stream);
            ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Companies");

            ws.Cells["A1"].Value = "All Companies";
            ws.Cells["A1:B2"].Style.Font.Bold = true;

            ws.Cells["A2"].Value = "Date";
            ws.Cells["B2"].Value = $"{date.Day} - {date.Month} - {date.Year}";

            ws.Cells["A5"].Value = "Total Company Count: ";
            ws.Cells["A5"].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            ws.Cells["A5"].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            ws.Cells["A5"].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            ws.Cells["A5"].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

            ws.Cells["B5"].Value = allCompanies.Count;
            ws.Cells["B5"].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            ws.Cells["B5"].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            ws.Cells["B5"].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            ws.Cells["B5"].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

            ws.Cells["A5"].Style.Font.Bold = true;
            ws.Cells["B5"].Style.Font.Bold = true;

            ws.Cells["A6"].Value = "Company Name";
            ws.Cells["B6"].Value = "Email Address";
            ws.Cells["C6"].Value = "Phone Number";
            ws.Cells["D6"].Value = "Sector Name";
            ws.Cells["E6"].Value = "Number of Employees";
            ws.Cells["F6"].Value = "Contact Person";

            ws.Cells["A6:F6"].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            ws.Cells["A6:F6"].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            ws.Cells["A6:F6"].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            ws.Cells["A6:F6"].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            ws.Cells["A6:F6"].Style.Font.Bold = true;

            int rowStart = 7;

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

                ws.Cells[string.Format("F{0}", rowStart)].Value = company.ContactPerson;
                ws.Cells[string.Format("F{0}", rowStart)].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                ws.Cells[string.Format("F{0}", rowStart)].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                ws.Cells[string.Format("F{0}", rowStart)].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                ws.Cells[string.Format("F{0}", rowStart)].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

                rowStart++;
            }

            ws.Cells["A:AZ"].AutoFitColumns();
            pck.Save();
            stream.Position = 0;

            var report = new CreateReportDTO()
            {
                Id = Guid.NewGuid(),
                Name = $"All_Companies_Report_{date.Month}/{date.Year}",
                ReportPath = "..\\IKApplication.MVC\\wwwroot\\Reports\\" + Guid.NewGuid() + ".xlsx",
                CreatorId = currentUser.Id,
                FileType = FileType.xls,
            };

            using (FileStream file = new FileStream(report.ReportPath, FileMode.Create, System.IO.FileAccess.Write))
            {
                byte[] bytes = new byte[stream.Length];
                stream.Read(bytes, 0, (int)stream.Length);
                file.Write(bytes, 0, bytes.Length);
                stream.Close();
            }

            await _reportService.Create(report);

            return new FileStreamResult(new FileStream(report.ReportPath, FileMode.Open, FileAccess.Read), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        [HttpGet]
        public async Task<FileResult> CompanyPDF()
        {
            List<CompanyVM> allCompanies = await _companyService.GetAllCompanies();
            var currentUser = await _appUserService.GetCurrentUserInfo(User.Identity.Name);
            var date = DateTime.Now;

            //Building an HTML string.
            StringBuilder sb = new StringBuilder();

            //Table start.
            sb.Append("<table border='1' cellpadding='5' cellspacing='0' style='border: 1px solid #ccc;font-family: Arial; font-size: 10pt;'>");
            sb.Append("<tr style='border: none'>");

            sb.Append("<td style='font-weight: bold;border: 1px solid #ccc'>");
            sb.Append("Total Company Count: ");
            sb.Append("</td>");

            sb.Append("<td style='font-weight: bold;border: 1px solid #ccc'>");
            sb.Append(allCompanies.Count());
            sb.Append("</td>");

            sb.Append("</tr>");

            //Building the Header row.
            sb.Append("<tr>");
            sb.Append("<th style='font-weight: bold;border: 1px solid #ccc'>Company Name</th>");
            sb.Append("<th style='font-weight: bold;border: 1px solid #ccc'>Email Address</th>");
            sb.Append("<th style='font-weight: bold;border: 1px solid #ccc'>Phone Number</th>");
            sb.Append("<th style='font-weight: bold;border: 1px solid #ccc'>Sector Name</th>");
            sb.Append("<th style='font-weight: bold;border: 1px solid #ccc'>Number of Employees</th>");
            sb.Append("<th style='font-weight: bold;border: 1px solid #ccc'>Contact Person</th>");
            sb.Append("</tr>");

            //Building the Data rows.
            foreach (CompanyVM company in allCompanies)
            {

                sb.Append("<tr>");

                sb.Append("<td style='border: 1px solid #ccc'>");
                sb.Append($"{company.Name}");
                sb.Append("</td>");

                sb.Append("<td style='border: 1px solid #ccc'>");
                sb.Append($"{company.Email}");
                sb.Append("</td>");

                sb.Append("<td style='border: 1px solid #ccc'>");
                sb.Append(company.PhoneNumber);
                sb.Append("</td>");

                sb.Append("<td style='border: 1px solid #ccc'>");
                sb.Append(company.SectorName);
                sb.Append("</td>");

                sb.Append("<td style='border: 1px solid #ccc'>");
                sb.Append(company.NumberOfEmployees);
                sb.Append("</td>");

                sb.Append("<td style='border: 1px solid #ccc'>");
                sb.Append(company.ContactPerson);
                sb.Append("</td>");

                sb.Append("</tr>");
            }

            //Table end.
            sb.Append("</table>");

            MemoryStream stream = new MemoryStream();
            StringReader sr = new StringReader(sb.ToString());
            Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 30f, 10f);
            PdfWriter writer = PdfWriter.GetInstance(pdfDoc, stream);
            pdfDoc.Open();
            XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
            pdfDoc.Close();

            var report = new CreateReportDTO()
            {
                Id = Guid.NewGuid(),
                Name = $"All_Companies_Report_{date.Month}/{date.Year}",
                ReportPath = "..\\IKApplication.MVC\\wwwroot\\Reports\\" + Guid.NewGuid() + ".pdf",
                CreatorId = currentUser.Id,
                FileType = FileType.pdf,
            };

            using (FileStream file = new FileStream(report.ReportPath, FileMode.Create, System.IO.FileAccess.Write))
            {
                var memoryStream = new MemoryStream(stream.ToArray());
                byte[] bytes = new byte[memoryStream.Length];
                memoryStream.Read(bytes, 0, (int)memoryStream.Length);
                file.Write(bytes, 0, bytes.Length);
            }

            await _reportService.Create(report);

            return new FileStreamResult(new FileStream(report.ReportPath, FileMode.Open, FileAccess.Read), "application/pdf");
        }
    }
}
