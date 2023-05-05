using AutoMapper;
using Common.Utilities;
using Data.Repositories;
using Entities;
using FacultyDashboard.Hubs;
using FacultyDashboard.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace FacultyDashboard.Controllers
{
    public class ReminderController : Controller
    {
        private readonly IHubContext<dataHub> dataHub;
        private readonly UserManager<User> userManager;
        private readonly IReminder_Repository _repository;
        private readonly IRepository<Reminder> _datarepository;
        private readonly ILogger<ReminderController> logger;
        private readonly IMapper _mapper;

        public ReminderController(IHubContext<dataHub> _DataHub, UserManager<User> userManager, IRepository<Reminder> _Repository, IReminder_Repository ar
            , ILogger<ReminderController> logger, IMapper mapper)
        {
            this.userManager = userManager;
            this.dataHub = _DataHub;
            this._repository = ar;
            this._datarepository = _Repository;
            this.logger = logger;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index(int pageId = 1)
        {
            Reminder_ListViewModel Model = new Reminder_ListViewModel();
            if (HttpContext.Request.QueryString.HasValue && HttpContext.Request.QueryString.Value.Contains("&"))
            {
                var SearchParameters = SearchUtilities.SearchUrlParameters(HttpContext.Request.QueryString.Value);
                if (SearchParameters.Count > 0)
                {
                    Model = new Reminder_ListViewModel();
                    Model.Datas = _datarepository.Search(SearchParameters, new Reminder());
                }
            }
            else
                Model = await _repository.GetAllDatas(10, pageId);
            return View(Model);
        }
        [HttpGet]
        public async Task<IActionResult> Operations(int? id)
        {
            ReminderDto Model = new ReminderDto();
            if (id.HasValue)
            {
                var model = await _datarepository.GetByIdAsync(new CancellationToken(), id.Value);
                Model = ReminderDto.FromEntity(_mapper, model); // Entity => DTO
            }
            return View(Model);
        }
        public async Task<IActionResult> Save(int uriidForEdit, string datas)
        {
            var model = JsonConvert.DeserializeObject<ReminderDto>(datas);
            if (model != null)
            {
                if (uriidForEdit == 0)
                {
                    var user = await userManager.FindByNameAsync(this.User.FindFirst(ClaimTypes.Name).Value);
                    model.RegistereUser = user;
                    var model2 = model.ToEntity(_mapper);
                    model2.Message_Type = Message_Type.UnRead;
                    model2.RegisterDate = DateAndTime.GetPersianDateTime(model.RegisterDate.Value);
                    await _datarepository.AddAsync(model2, new CancellationToken());
                }
                else
                {
                    var editmodel = await _datarepository.GetByIdAsync(new CancellationToken(), uriidForEdit);
                    editmodel = model.ToEntity(_mapper, editmodel);
                    editmodel.Id = uriidForEdit;
                    editmodel.Message_Type = Message_Type.UnRead;
                    editmodel.RegisterDate = DateAndTime.GetPersianDateTime(editmodel.RegisterDate);
                    await _datarepository.UpdateAsync(editmodel, new CancellationToken());
                }



                await dataHub.Clients.All.SendAsync("GetNotification", "Reminder");
            }
            return new JsonResult(datas);
        }
        public async Task<IActionResult> Read(int? id)
        {
            ReminderDto Model = new ReminderDto();
            if (id.HasValue)
            {
                var model = await _datarepository.GetByIdAsync(new CancellationToken(), id.Value);
                Model = ReminderDto.FromEntity(_mapper, model); // Entity => DTO
                model.Message_Type = Message_Type.Read;
                await _datarepository.UpdateAsync(model, new CancellationToken());
            }
            return View(Model);
        }
    }
}
