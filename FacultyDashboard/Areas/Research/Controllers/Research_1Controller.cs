using AutoMapper;
using Common.Utilities;
using Data.Repositories;
using Entities;
using FacultyDashboard.Areas.Research.Models;
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



namespace FacultyDashboard.Areas.Research.Controllers
{
    [Area("Research")]
    [Authorize(Roles = "admin")]
    public class Research_1Controller : Controller
    {
        private IWebHostEnvironment Environment;
        private readonly IRepository<User> _Userrepository;
        private readonly IHubContext<dataHub> dataHub;
        private readonly UserManager<User> userManager;
        private readonly IResearch_1_Repository _repository;
        private readonly IRepository<Notification> _Noti_repository;
        private readonly IRepository<Research_1> _datarepository;
        private readonly ILogger<Research_1Controller> logger;
        private readonly IMapper _mapper;
        private readonly ISettingRepository _Settingrepository;
        private readonly IRepository<Setting> _Settingdatarepository;

        public Research_1Controller(IHubContext<dataHub> _DataHub, IRepository<Notification> noti,
            IRepository<Research_1> _Repository, IResearch_1_Repository ar, UserManager<User> userManager
            , ILogger<Research_1Controller> logger, IMapper mapper, IWebHostEnvironment _environment
            , ISettingRepository Settingrepository, IRepository<Setting> sett, IRepository<User> _userRepository)
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
            this._Settingrepository = Settingrepository;
            this._Settingdatarepository = sett;
        }
        public async Task<IActionResult> Index(int pageId = 1)
        {
            Research_1_ListViewModel Model = new Research_1_ListViewModel();
            if (HttpContext.Request.QueryString.HasValue && HttpContext.Request.QueryString.Value.Contains("&"))
            {
                var SearchParameters = SearchUtilities.SearchUrlParameters(HttpContext.Request.QueryString.Value);
                if (SearchParameters.Count > 0)
                {
                    Model = new Research_1_ListViewModel();
                    Model.Datas = _datarepository.Search(SearchParameters, new Research_1());
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

            var set = await _Settingrepository.Research_Count(pat.Count, false);
            await _Settingdatarepository.UpdateAsync(set, new CancellationToken());
            await dataHub.Clients.All.SendAsync("GetNotification", "SettingResearch_Count");

            return new JsonResult(new { msg = "Success Removed" });
        }
        public async Task<IActionResult> ExportToExcel(int pageId = 1)
        {
            var fileDownloadName = "sample.xlsx";
            var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            var package = await _repository.ExportAllToExcel(10, "تعداد دانشجویان شاغل به تحصیل", Entities.LanguageType.FA, pageId);

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
            Research_1_Dto Model = new Research_1_Dto();
            if (id.HasValue)
            {
                var model = await _datarepository.GetByIdAsync(new CancellationToken(), id.Value);
                Model = Research_1_Dto.FromEntity(_mapper, model); // Entity => DTO
            }
            return View(Model);
        }
        public async Task<IActionResult> Save(int uriidForEdit, string datas)
        {
            var model = JsonConvert.DeserializeObject<Research_1_Dto>(datas);
            if (model != null)
            {
                int setval = 0;
                if (uriidForEdit == 0)
                {
                    var user = await userManager.FindByNameAsync(this.User.FindFirst(ClaimTypes.Name).Value);
                    model.RegistereUser = user;
                    var model2 = model.ToEntity(_mapper);
                    model2.RegisterDate = DateAndTime.GetPersianDateTime(model.RegisterDate);
                    await _datarepository.AddAsync(model2, new CancellationToken());
                    var set = await _Settingrepository.Research_Count(model.Count, true);
                    await _Settingdatarepository.UpdateAsync(set, new CancellationToken());
                }
                else
                {
                    var editmodel = await _datarepository.GetByIdAsync(new CancellationToken(), uriidForEdit);
                    if (editmodel != null && editmodel.Count.HasValue)
                    {
                        if (editmodel.Count.Value > model.Count.Value)
                        {
                            setval = editmodel.Count.Value - model.Count.Value;
                            var set = await _Settingrepository.Research_Count(setval, false);
                            await _Settingdatarepository.UpdateAsync(set, new CancellationToken());
                        }
                        else if (editmodel.Count.Value < model.Count.Value)
                        {
                            setval = model.Count.Value - editmodel.Count.Value;
                            var set = await _Settingrepository.Research_Count(setval, true);
                            await _Settingdatarepository.UpdateAsync(set, new CancellationToken());
                        }
                    }
                    editmodel = model.ToEntity(_mapper, editmodel);
                    editmodel.Id = uriidForEdit;
                    editmodel.RegisterDate = DateAndTime.GetPersianDateTime(editmodel.RegisterDate);
                    await _datarepository.UpdateAsync(editmodel, new CancellationToken());
                }
                await dataHub.Clients.All.SendAsync("GetNotification", "Research_1");

                // notification
                await dataHub.Clients.All.SendAsync("GetNotification", "SettingResearch_Count");

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
            Type thetyp = typeof(ResourceGroupType/*ResourceType*/);
            var myData2 = thedata.GroupBy(x => new { start = x.ResourceGroup/*Resource_Type*/.Value },
                       (y, c) => new ChartWith3ColString()
                       {
                           Col1 = y.start.GetDisplayName(),
                           Col2 = (c.Sum(k => k.Count).Value).ToString(),
                           Col3 = (c.Where(p => p.Resource_Type.Value == ResourceType.L1).Sum(k => k.Count)).ToString(),
                           Col4 = (c.Where(p => p.Resource_Type.Value == ResourceType.L2).Sum(k => k.Count)).ToString(),
                           Col5 = (c.Where(p => p.Resource_Type.Value == ResourceType.L3).Sum(k => k.Count)).ToString(),
                           Col6 = (c.Where(p => p.Resource_Type.Value == ResourceType.L4).Sum(k => k.Count)).ToString(),
                           Col7 = (c.Where(p => p.Resource_Type.Value == ResourceType.L5).Sum(k => k.Count)).ToString(),
                           Col8 = (c.Where(p => p.Resource_Type.Value == ResourceType.L6).Sum(k => k.Count)).ToString(),
                           Col9 = (c.Where(p => p.Resource_Type.Value == ResourceType.L7).Sum(k => k.Count)).ToString(),
                           //Col3 = (c.Where(p => p.Resource_Type.Value == ResourceType.L1).Sum(k => k.Count)).ToString(),
                           //Col4 = (c.Where(p => p.Resource_Type.Value == ResourceType.L2).Sum(k => k.Count)).ToString(),
                           //Col5 = (c.Where(p => p.Resource_Type.Value == ResourceType.L3).Sum(k => k.Count)).ToString(),
                           //Col6 = (c.Where(p => p.Resource_Type.Value == ResourceType.L4).Sum(k => k.Count)).ToString(),
                           //Col7 = (c.Where(p => p.Resource_Type.Value == ResourceType.L5).Sum(k => k.Count)).ToString(),
                           //Col8 = (c.Where(p => p.Resource_Type.Value == ResourceType.L6).Sum(k => k.Count)).ToString(),
                           //Col9 = (c.Where(p => p.Resource_Type.Value == ResourceType.L7).Sum(k => k.Count)).ToString(),
                       }).ToList();

            //int enumitemcount_2 = Enum.GetNames(typeof(ResourceType)).Length;
            //string[] colname_2 = new string[enumitemcount_2];
            //var test_2 = "";
            //for (int i = 0; i < enumitemcount_2; i++)
            //{
            //    test_2 = Enum.Parse(typeof(ResourceType), i.ToString()).ToString();
            //    colname_2[i] = test_2.GetDisplayName(typeof(ResourceType));
            //}


            List<ChartWith3ColString> final = new List<ChartWith3ColString>();
            int enumitemcount = Enum.GetNames(thetyp).Length;
            string[] colname = new string[enumitemcount];
            var test = "";
            int sum = 0;
            for (int i = 0; i < enumitemcount; i++)
            {
                test = Enum.Parse(thetyp, i.ToString()).ToString();
                colname[i] = test.GetDisplayName(thetyp);
            }
            if (myData2 != null && myData2.Count() > 0)
            {
                var item_1 = myData2.Where(x => x.Col1 == "گروه علوم آزمایشگاهی").FirstOrDefault();
                if (item_1 != null)
                    final.Add(item_1);
                else
                {
                    ChartWith3ColString theitem = new ChartWith3ColString();
                    theitem.Col1 = "گروه علوم آزمایشگاهی";
                    theitem.Col2 = "0";
                    theitem.Col3 = "0";
                    theitem.Col4 = "0";
                    theitem.Col5 = "0";
                    theitem.Col6 = "0";
                    theitem.Col7 = "0";
                    theitem.Col8 = "0";
                    theitem.Col9 = "0";
                    final.Add(theitem);
                }
                item_1 = myData2.Where(x => x.Col1 == "گروه رادیولوژی").FirstOrDefault();
                if (item_1 != null)
                    final.Add(item_1);
                else
                {
                    ChartWith3ColString theitem = new ChartWith3ColString();
                    theitem.Col1 = "گروه رادیولوژی";
                    theitem.Col2 = "0";
                    theitem.Col3 = "0";
                    theitem.Col4 = "0";
                    theitem.Col5 = "0";
                    theitem.Col6 = "0";
                    theitem.Col7 = "0";
                    theitem.Col8 = "0";
                    theitem.Col9 = "0";
                    final.Add(theitem);
                }
                item_1 = myData2.Where(x => x.Col1 == "گروه هوشبری").FirstOrDefault();
                if (item_1 != null)
                    final.Add(item_1);
                else
                {
                    ChartWith3ColString theitem = new ChartWith3ColString();
                    theitem.Col1 = "گروه هوشبری";
                    theitem.Col2 = "0";
                    theitem.Col3 = "0";
                    theitem.Col4 = "0";
                    theitem.Col5 = "0";
                    theitem.Col6 = "0";
                    theitem.Col7 = "0";
                    theitem.Col8 = "0";
                    theitem.Col9 = "0";
                    final.Add(theitem);
                }
                item_1 = myData2.Where(x => x.Col1 == "گروه فناوری اطلاعات سلامت").FirstOrDefault();
                if (item_1 != null)
                    final.Add(item_1);
                else
                {
                    ChartWith3ColString theitem = new ChartWith3ColString();
                    theitem.Col1 = "گروه فناوری اطلاعات سلامت";
                    theitem.Col2 = "0";
                    theitem.Col3 = "0";
                    theitem.Col4 = "0";
                    theitem.Col5 = "0";
                    theitem.Col6 = "0";
                    theitem.Col7 = "0";
                    theitem.Col8 = "0";
                    theitem.Col9 = "0";
                    final.Add(theitem);
                }
                item_1 = myData2.Where(x => x.Col1 == "گروه اتاق عمل").FirstOrDefault();
                if (item_1 != null)
                    final.Add(item_1);
                else
                {
                    ChartWith3ColString theitem = new ChartWith3ColString();
                    theitem.Col1 = "گروه اتاق عمل";
                    theitem.Col2 = "0";
                    theitem.Col3 = "0";
                    theitem.Col4 = "0";
                    theitem.Col5 = "0";
                    theitem.Col6 = "0";
                    theitem.Col7 = "0";
                    theitem.Col8 = "0";
                    theitem.Col9 = "0";
                    final.Add(theitem);
                }
                item_1 = myData2.Where(x => x.Col1 == "گروه فوریتهای پزشکی").FirstOrDefault();
                if (item_1 != null)
                    final.Add(item_1);
                else
                {
                    ChartWith3ColString theitem = new ChartWith3ColString();
                    theitem.Col1 = "گروه فوریتهای پزشکی";
                    theitem.Col2 = "0";
                    theitem.Col3 = "0";
                    theitem.Col4 = "0";
                    theitem.Col5 = "0";
                    theitem.Col6 = "0";
                    theitem.Col7 = "0";
                    theitem.Col8 = "0";
                    theitem.Col9 = "0";
                    final.Add(theitem);
                }
                item_1 = myData2.Where(x => x.Col1 == "گروه زبان").FirstOrDefault();
                if (item_1 != null)
                    final.Add(item_1);
                else
                {
                    ChartWith3ColString theitem = new ChartWith3ColString();
                    theitem.Col1 = "گروه زبان";
                    theitem.Col2 = "0";
                    theitem.Col3 = "0";
                    theitem.Col4 = "0";
                    theitem.Col5 = "0";
                    theitem.Col6 = "0";
                    theitem.Col7 = "0";
                    theitem.Col8 = "0";
                    theitem.Col9 = "0";
                    final.Add(theitem);
                }
                //foreach (var col in colname)
                //{
                //    var item = myData2.Where(x => x.Col1 == col).FirstOrDefault();
                //    if (item != null)
                //    {
                //        final.Add(item);
                //        sum += int.Parse(item.Col2);
                //    }
                //    else
                //    {
                //        ChartWith3ColString theitem = new ChartWith3ColString();
                //        theitem.Col1 = col;
                //        theitem.Col2 = "0";
                //        theitem.Col3 = "0";
                //        theitem.Col4 = "0";
                //        theitem.Col5 = "0";
                //        theitem.Col6 = "0";
                //        theitem.Col7 = "0";
                //        theitem.Col8 = "0";
                //        theitem.Col9 = "0";
                //        final.Add(theitem);
                //    }
                //}
            }
            return new JsonResult(new { koldata = final, cate = colname/*colname_2*/.ToList(), doctor = Doctor_id });
        }
        public static Expression<Func<Research_1, bool>> Condition(DateTime? StartDate, DateTime? EndDate)
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