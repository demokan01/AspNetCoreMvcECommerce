using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreMvcECommerce.Areas.Admin.Controllers
{
    [Authorize(Roles ="Admin")]
    [Area("admin")]
    [Route("admin/dashboard")]
    public class DashBoardController : Controller
    {
        [Route("")]
        [Route("index")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
