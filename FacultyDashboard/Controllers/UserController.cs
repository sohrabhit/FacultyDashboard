using AutoMapper;
using Common.Utilities;
using Data.Repositories;
using Entities;
using FacultyDashboard.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace FacultyDashboard.Controllers
{
    [Authorize(Roles = "admin")]
    public class UserController : Controller
    {
        private readonly IUserRepository _repository;
        private readonly ILogger<UserController> logger;
        private readonly IMapper _mapper;

        public UserController(IUserRepository _examRepository, ILogger<UserController> logger, IMapper mapper)
        {
            this._repository = _examRepository;
            this.logger = logger;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index(int pageId = 1)
        {
            User_ListViewModel Model = new User_ListViewModel();
            if (HttpContext.Request.QueryString.HasValue && HttpContext.Request.QueryString.Value.Contains("&"))
            {
                var SearchParameters = SearchUtilities.SearchUrlParameters(HttpContext.Request.QueryString.Value);
                if (SearchParameters.Count > 0)
                {
                    Model = new User_ListViewModel();
                    Model.Datas = _repository.Search(SearchParameters, new User());
                }
            }
            else
                Model = await _repository.GetAllDatas(10, pageId);
            return View(Model);
        }
        [HttpGet]
        public async Task<IActionResult> Operations(int? id)
        {
            UserRegisterDto Model = new UserRegisterDto();
            if (id.HasValue)
            {
                var model = await _repository.GetByIdAsync(new CancellationToken(), id.Value);
                //Model = UserRegisterDto.FromEntity(_mapper, model); // Entity => DTO
            }
            return View(Model);
        }
    }
}
