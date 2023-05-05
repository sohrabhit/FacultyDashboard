using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebFramework.Filters;

namespace WebFramework.Api
{
    [ApiController]
    [AllowAnonymous]
    [ApiResultFilter]
    [Route("api/v{version:apiVersion}/[controller]")]// api/v1/[controller]
    //[Route("api/[controller]")]// api/[controller]?api-version=1
    public class BaseController : ControllerBase
    {
        //public UserRepository UserRepository { get; set; } => property injection

        // مثلا این خصوصیت رو میزاریم که راحتتر بفهمیم کاربر وارد شده یا نه
        public bool UserIsAutheticated => HttpContext.User.Identity.IsAuthenticated;
    }
}
