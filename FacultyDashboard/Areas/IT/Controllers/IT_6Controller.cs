using AutoMapper;
using Common.Utilities;
using Data.Repositories;
using Entities;
using FacultyDashboard.Areas.IT.Models;
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


namespace FacultyDashboard.Areas.IT.Controllers
{
    [Area("IT")]
    [Authorize(Roles = "admin")]
    public class IT_6Controller : Controller
    {
        private IWebHostEnvironment Environment;
        private readonly IRepository<User> _Userrepository;
        private readonly IHubContext<dataHub> dataHub;
        private readonly UserManager<User> userManager;
        private readonly IIT_6_Repository _repository;
        private readonly IRepository<Notification> _Noti_repository;
        private readonly IRepository<IT_6> _datarepository;
        private readonly ILogger<IT_6Controller> logger;
        private readonly IMapper _mapper;

        public IT_6Controller(IHubContext<dataHub> _DataHub, IRepository<Notification> noti,
            IRepository<IT_6> _Repository, IIT_6_Repository ar, UserManager<User> userManager
            , ILogger<IT_6Controller> logger, IMapper mapper, IWebHostEnvironment _environment
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
            IT_6_ListViewModel Model = new IT_6_ListViewModel();
            if (HttpContext.Request.QueryString.HasValue && HttpContext.Request.QueryString.Value.Contains("&"))
            {
                var SearchParameters = SearchUtilities.SearchUrlParameters(HttpContext.Request.QueryString.Value);
                if (SearchParameters.Count > 0)
                {
                    Model = new IT_6_ListViewModel();
                    Model.Datas = _datarepository.Search(SearchParameters, new IT_6());
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
            var package = await _repository.ExportAllToExcel(10, "نسبت دانشجویان به اساتید", Entities.LanguageType.FA, pageId);

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
            IT_6_Dto Model = new IT_6_Dto();
            if (id.HasValue)
            {
                var model = await _datarepository.GetByIdAsync(new CancellationToken(), id.Value);
                Model = IT_6_Dto.FromEntity(_mapper, model); // Entity => DTO
            }
            return View(Model);
        }
        public async Task<IActionResult> Save(int uriidForEdit, string datas)
        {
            var model = JsonConvert.DeserializeObject<IT_6_Dto>(datas);
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


                await dataHub.Clients.All.SendAsync("GetNotification", "IT_6");
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
    }
}
