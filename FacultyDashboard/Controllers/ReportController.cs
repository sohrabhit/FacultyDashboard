using AutoMapper;
using AutoMapper.QueryableExtensions;
using Data.Repositories;
using Entities;
using FacultyDashboard.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace FacultyDashboard.Controllers
{
    public class ReportController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly IRepository<User> _Userrepository;
        private readonly IUserReport_Repository _repository;
        private readonly IRepository<UserReport> _datarepository;
        private readonly ILogger<ReportController> logger;
        private readonly IMapper _mapper;

        public ReportController(IRepository<User> _userRepository, UserManager<User> userManager, IRepository<UserReport> _Repository, IUserReport_Repository ar
            , ILogger<ReportController> logger, IMapper mapper)
        {
            this.userManager = userManager;
            this._Userrepository = _userRepository;
            this._repository = ar;
            this._datarepository = _Repository;
            this.logger = logger;
            _mapper = mapper;
        }
        public IActionResult Choose()
        {
            return View();
        }
        public async Task<IActionResult> Load()
        {
            ClaimsPrincipal currentUser = this.User;
            var currentUserName = currentUser.FindFirst(ClaimTypes.Name).Value;
            var user = await userManager.FindByNameAsync(currentUserName);
            List<UserReportDto> model = new List<UserReportDto>();
            var dataModel = await _repository.GetByUserId(user.Id);
            if (dataModel != null && dataModel.Count > 0)
            {
                model = dataModel.AsQueryable().ProjectTo<UserReportDto>(_mapper.ConfigurationProvider).ToList();
                return new JsonResult(model);
            }
            else
                return new JsonResult("");
        }
        public async Task<IActionResult> Save(string datas)
        {
            ClaimsPrincipal currentUser = this.User;
            var currentUserName = currentUser.FindFirst(ClaimTypes.Name).Value;
            var user = await userManager.FindByNameAsync(currentUserName);
            var dataModel = await _repository.GetByUserId(user.Id);
            //examViewModel.Interprate_Data = examViewModel.Interprate_Data.Replace(@"\", "");
            if (string.IsNullOrEmpty(datas))
            {
                await _datarepository.DeleteRangeAsync(dataModel, new CancellationToken());
            }
            else
            {
                var model = JsonConvert.DeserializeObject<List<UserReportDto>>(datas);
                //var r = model;

                List<UserReport> listnew = new List<UserReport>();
                List<UserReport> listold = new List<UserReport>();
                List<UserReport> listdelete = new List<UserReport>();
                if (model != null && model.Count > 0)
                {
                    foreach (var ques in model)
                    {
                        if (ques.Id == 0)
                        {
                            var newQuestion = ques.ToEntity(_mapper);
                            newQuestion.RegistereUser = user;
                            listnew.Add(newQuestion);
                        }
                        else
                        {
                            var db = dataModel.Where(x => x.Id == ques.Id).FirstOrDefault();
                            //db.ChartType = ques.ChartType;
                            db.Indicator_Name = ques.Indicator_Name;
                            listold.Add(db);
                        }
                    }
                }
                if (dataModel != null && dataModel.Count > 0)
                {
                    foreach (var db in dataModel)
                    {
                        if (!model.Any(x => x.Id == db.Id))
                        {
                            listdelete.Add(db);
                        }
                    }
                }
                if (listnew.Count > 0)
                    await _datarepository.AddRangeAsync(listnew, new CancellationToken());
                if (listold.Count > 0)
                    await _datarepository.UpdateRangeAsync(listold, new CancellationToken());
                if (listdelete.Count > 0)
                    await _datarepository.DeleteRangeAsync(listdelete, new CancellationToken());
            }
            return new ObjectResult(new { status = "success" });
        }
    }
}
