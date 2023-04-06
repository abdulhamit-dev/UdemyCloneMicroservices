using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UdemyClone.UI.Services.Interfaces;

namespace UdemyClone.UI.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _userService.GetUser());
        }
    }
}
