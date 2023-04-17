using Microsoft.AspNetCore.Mvc;

namespace UdemyClone.UI.Controllers;

public class CoursesController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}