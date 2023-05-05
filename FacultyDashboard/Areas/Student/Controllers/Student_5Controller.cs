using AutoMapper;
using Common.Utilities;
using Data.Repositories;
using Entities;
using FacultyDashboard.Areas.Student.Models;
using FacultyDashboard.Hubs;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Common;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace FacultyDashboard.Areas.Student.Controllers
{
    [Area("Student")]
    [Authorize(Roles = "admin")]
    public class Student_5Controller : Controller
    {
        private IWebHostEnvironment Environment;
        private readonly IRepository<User> _Userrepository;
        private readonly IHubContext<dataHub> dataHub;
        private readonly UserManager<User> userManager;
        private readonly IStudent_5_Repository _repository;
        private readonly IRepository<Notification> _Noti_repository;
        private readonly IRepository<Student_5> _datarepository;
        private readonly ILogger<Student_5Controller> logger;
        private readonly IMapper _mapper;

        public Student_5Controller(IHubContext<dataHub> _DataHub, IRepository<Notification> noti,
            IRepository<Student_5> _Repository, IStudent_5_Repository ar, UserManager<User> userManager
            , ILogger<Student_5Controller> logger, IMapper mapper, IWebHostEnvironment _environment
            , IRepository<User> _userRepository)
        {
            this.userManager = userManager;
            this.dataHub = _DataHub;
            this._repository = ar;
            this._Noti_repository = noti;
            this._datarepository = _Repository;
            this.logger = logger;
            _mapper = mapper;
            Environment = _environment;
            this._Userrepository = _userRepository;
        }
        public async Task<IActionResult> Index(int pageId = 1)
        {
            Student_5_ListViewModel Model = new Student_5_ListViewModel();
            if (HttpContext.Request.QueryString.HasValue && HttpContext.Request.QueryString.Value.Contains("&"))
            {
                var SearchParameters = SearchUtilities.SearchUrlParameters(HttpContext.Request.QueryString.Value);
                if (SearchParameters.Count > 0)
                {
                    Model = new Student_5_ListViewModel();
                    Model.Datas = _datarepository.Search(SearchParameters, new Student_5());
                }
            }
            else
                Model = await _repository.GetAllDatas(10, pageId);
            return View(Model);
        }
        public async Task<IActionResult> Delete(int id)
        {
            var pat = await _repository.GetById(id);
            await _datarepository.DeleteAsync(pat, new CancellationToken());
            return new JsonResult(new { msg = "Success Removed" });
        }
        public async Task<IActionResult> ExportToExcel(int pageId = 1)
        {
            var fileDownloadName = "sample.xlsx";
            var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            var package = await _repository.ExportAllToExcel(10, "آمار فارغ‌التحصیلان به تفکیک مقطع تحصیلی و جنسیت", Entities.LanguageType.FA, pageId);

            var fileStream = new MemoryStream();
            package.SaveAs(fileStream);
            fileStream.Position = 0;

            var fsr = new FileStreamResult(fileStream, contentType);
            fsr.FileDownloadName = fileDownloadName;
            return fsr;
        }
        public async Task<IActionResult> ExportToWord(int pageId = 1)
        {
            var sbDocumentBody = await _repository.ExportAllToWord(10, Entities.LanguageType.FA, pageId);
            byte[] byteArray = ASCIIEncoding.UTF8.GetBytes(sbDocumentBody.ToString());
            return File(byteArray, "application/msword", "Report.doc");
        }
        public async Task<IActionResult> ExportToPdf(int pageId = 1)
        {
            string wwwPath = this.Environment.WebRootPath;
            using (MemoryStream stream = new System.IO.MemoryStream())
            {
                Document document = new Document(PageSize.A4, 5f, 5f, 10f, 10f);
                PdfWriter writer = PdfWriter.GetInstance(document, stream);
                document.Open();

                var table = await _repository.ExportAllToPDF(10, Entities.LanguageType.FA, wwwPath, pageId);
                ColumnText ct = new ColumnText(writer.DirectContent);
                ct.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
                ct.SetSimpleColumn(100, 100, 500, 800, 24, Element.ALIGN_RIGHT);

                document.Add(table);
                document.Close();
                document.CloseDocument();
                document.Dispose();
                writer.Close();
                writer.Dispose();

                return File(stream.ToArray(), "application/pdf", "Report.pdf");
            }
        }
        [HttpGet]
        public async Task<IActionResult> Operations(int? id)
        {
            Student_5_Dto Model = new Student_5_Dto();
            if (id.HasValue)
            {
                var model = await _datarepository.GetByIdAsync(new CancellationToken(), id.Value);
                Model = Student_5_Dto.FromEntity(_mapper, model); // Entity => DTO
            }
            return View(Model);
        }
        public async Task<IActionResult> Save(int uriidForEdit, string datas)
        {
            var model = JsonConvert.DeserializeObject<Student_5_Dto>(datas);
            if (model != null)
            {
                if (uriidForEdit == 0)
                {
                    var user = await userManager.FindByNameAsync(this.User.FindFirst(ClaimTypes.Name).Value);
                    model.RegistereUser = user;
                    var model2 = model.ToEntity(_mapper);
                    model2.RegisterDate = DateAndTime.GetPersianDateTime(model.RegisterDate);
                    await _datarepository.AddAsync(model2, new CancellationToken());
                }
                else
                {
                    var editmodel = await _datarepository.GetByIdAsync(new CancellationToken(), uriidForEdit);
                    editmodel = model.ToEntity(_mapper, editmodel);
                    editmodel.Id = uriidForEdit;
                    editmodel.RegisterDate = DateAndTime.GetPersianDateTime(editmodel.RegisterDate);
                    await _datarepository.UpdateAsync(editmodel, new CancellationToken());
                }
                // notification


                await dataHub.Clients.All.SendAsync("GetNotification", "Student_5");
                return new JsonResult(new { result = "ok" });
            }
            else
                return new JsonResult(new { result = "no" });
        }
        public async Task<IActionResult> Chart()
        {
            var allProvince = _Userrepository.Table.ToList();
            ViewData["doctors"] = new SelectList(allProvince, "Id", "LastName");
            return View();
        }
        public async Task<IActionResult> Report(int Doctor_id, DateTime? StartDate, DateTime? EndDate)
        {
            List<ChartWith3ColString> amar_Report = new List<ChartWith3ColString>();
            var thedata = _datarepository.Table.OrderBy(x => x.Id).Where(Condition(StartDate, EndDate));
            var countkol = thedata.Count();
            ///////
            Type thetyp = typeof(GenderType);
            var myData2 = thedata.GroupBy(x => new { start = x.Gender.Value },
                       (y, c) => new ChartWith3ColString()
                       {
                           Col1 = y.start.GetDisplayName(),
                           Col2 = (c.Sum(k => k.Count).Value).ToString(),
                       }).ToList();
            List<ChartWith3ColString> final = new List<ChartWith3ColString>();
            int enumitemcount = Enum.GetNames(thetyp).Length;
            string[] colname = new string[enumitemcount];
            var test = "";
            decimal sum = 0;
            for (int i = 0; i < enumitemcount; i++)
            {
                test = Enum.Parse(thetyp, i.ToString()).ToString();
                colname[i] = test.GetDisplayName(thetyp);
            }
            if (myData2 != null && myData2.Count() > 0)
            {
                foreach (var col in colname)
                {
                    var item = myData2.Where(x => x.Col1 == col).FirstOrDefault();
                    if (item != null)
                    {
                        if (!string.IsNullOrEmpty(item.Col2))
                        {
                            final.Add(item);
                            sum += decimal.Parse(item.Col2);
                        }
                    }
                    else
                    {
                        ChartWith3ColString theitem = new ChartWith3ColString();
                        theitem.Col1 = col;
                        theitem.Col2 = "0";
                        final.Add(theitem);
                    }
                }
            }
            return new JsonResult(new { koldata = final, cate = colname.ToList(), doctor = Doctor_id });
        }
        public static Expression<Func<Student_5, bool>> Condition(DateTime? StartDate, DateTime? EndDate)
        {
            if (StartDate.HasValue && EndDate.HasValue)
                return (x => (x.RegisterDate >= StartDate.Value && x.RegisterDate <= EndDate.Value));
            else if (StartDate.HasValue && !EndDate.HasValue)
                return (x => (x.RegisterDate >= StartDate.Value));
            else if (!StartDate.HasValue && EndDate.HasValue)
                return (x => (x.RegisterDate <= EndDate.Value));
            else
                return (x => x.Id > 0);
        }
    }
}