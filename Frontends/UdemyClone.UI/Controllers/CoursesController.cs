using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Shared.Services;
using UdemyClone.UI.Models.Catalog;
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
    
    public async Task<IActionResult> Create()
    {
        var categories = await _catalogService.GetAllCategoryAsync();

        ViewBag.categoryList = new SelectList(categories, "Id", "Name");

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(CourseCreateDto courseCreateDto)
    {
        var categories = await _catalogService.GetAllCategoryAsync();
        ViewBag.categoryList = new SelectList(categories, "Id", "Name");
        if (!ModelState.IsValid)
        {
            return View();
        }
        courseCreateDto.UserId = _sharedIdentityService.GetUserId;

        await _catalogService.CreateCourseAsync(courseCreateDto);

        return RedirectToAction(nameof(Index));
    }
}