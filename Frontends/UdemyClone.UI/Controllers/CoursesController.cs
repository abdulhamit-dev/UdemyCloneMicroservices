using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Services;
using UdemyClone.UI.Services.Interfaces;

namespace UdemyClone.UI.Controllers;

[Authorize]
public class CoursesController : Controller
{
    private readonly ICatalogService _catalogService;
    private readonly ISharedIdentityService _sharedIdentityService;
    
    public CoursesController(ICatalogService catalogService, ISharedIdentityService sharedIdentityService)
    {
        _catalogService = catalogService;
        _sharedIdentityService = sharedIdentityService;
    }

    // GET
    public async Task<IActionResult> Index()
    {
        var models = await _catalogService.GetAllCourseByUserIdAsycn(_sharedIdentityService.GetUserId);
        return View(models);
    }


    public IActionResult IndexIki()
    {

        return View();
    }
}