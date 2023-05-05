using AutoMapper;
using Data.Repositories;
using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FacultyDashboard.Controllers
{
    //[AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly IRepository<User> _Userrepository;
        private readonly IUserReport_Repository _repository;
        private readonly IRepository<UserReport> _datarepository;

        private readonly IRepository<Reminder> Remin;
        private readonly ILogger<HomeController> logger;
        private readonly IMapper _mapper;
        private readonly ISettingRepository _Settingrepository;
        private readonly IRepository<Setting> _Settingdatarepository;

        public HomeController(IRepository<User> _userRepository, UserManager<User> userManager, IRepository<UserReport> _Repository, IUserReport_Repository ar
            , ISettingRepository Settingrepository, IRepository<Setting> sett, ILogger<HomeController> logger, IMapper mapper, IRepository<Reminder> _Remin)
        {
            this.userManager = userManager;
            this._Userrepository = _userRepository;
            this._repository = ar;
            this._datarepository = _Repository;
            this.logger = logger;
            _mapper = mapper;
            this.Remin = _Remin;
            this._Settingrepository = Settingrepository;
            this._Settingdatarepository = sett;
        }
        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Index1()
        {
            return View();
        }
        public async Task<IActionResult> DefaultSetting()
        {
            var set = await _Settingdatarepository.GetByIdAsync(new System.Threading.CancellationToken(), 1);
            return new JsonResult(new { Resource_7_All = set.Resource_7_All,
                Resource_7_1 = set.Resource_7_1,
                Resource_7_2 = set.Resource_7_2,
                Resource_7_3 = set.Resource_7_3,
                Resource_7_4 = set.Resource_7_4,
                Research_Count = set.Research_Count,
                Education_1_All = set.Education_1_All,
                Education_1_1 = set.Education_1_1,
                Education_1_2 = set.Education_1_2,
                Education_1_3 = set.Education_1_3,
                Education_1_4 = set.Education_1_4,
                Education_2_All = set.Education_2_All,
                Education_2_1 = set.Education_2_1,
                Education_2_2 = set.Education_2_2,
                Education_2_3 = set.Education_2_3,
                Education_2_4 = set.Education_2_4,
            });
        }
        public async Task<IActionResult> OnGetLoadReports()
        {
            var reminder = Remin.Table.Where(x => x.Message_Type.HasValue && x.Message_Type.Value == Message_Type.UnRead).ToList();
            List<Tuple<string, string>> rem = new List<Tuple<string, string>>();
            if (reminder != null && reminder.Count > 0)
            {
                foreach (var r in reminder)
                    rem.Add(new Tuple<string, string>(r.Title, r.Id.ToString()));
            }
            var user = await userManager.FindByNameAsync(this.User.FindFirst(ClaimTypes.Name).Value);
            var rep_list = await _repository.GetByUserId(user.Id);
            return new JsonResult(new { state = rep_list, Remiders = rem.Count > 0 ? rem_list(rem) : new string[,] { } });
        }
        private string[,] rem_list(List<Tuple<string, string>> rem)
        {
            string[,] list = new string[rem.Count, 2];
            for (int i = 0; i < rem.Count; i++)
            {
                list[i, 0] = rem[i].Item1;
                list[i, 1] = rem[i].Item2;
            }
            return list;
        }
    }
}
