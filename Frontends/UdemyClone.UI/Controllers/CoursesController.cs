using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Shared.Services;
using UdemyClone.UI.Models.Catalogs;
using UdemyClone.UI.Services.Interfaces;

namespace UdemyClone.UI.Controllers;

//[Authorize]
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
    
    public async Task<IActionResult> Creates()
    {
        var categories = await _catalogService.GetAllCategoryAsync();

        ViewBag.categoryList = new SelectList(categories, "Id", "Name");

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Creates(CourseCreateDto courseCreateDto)
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
    public async Task<IActionResult> Updates(string id)
    {
        var course = await _catalogService.GetByCourseId(id);
        var categories = await _catalogService.GetAllCategoryAsync();

        if (course == null)
        {
             RedirectToAction(nameof(Index));
        }
        ViewBag.categoryList = new SelectList(categories, "Id", "Name", course.Id);
        CourseUpdateDto courseUpdateDto = new()
        {
            Id = course.Id,
            Name = course.Name,
            Description = course.Description,
            Price = course.Price,
            Feature = course.Feature,
            CategoryId = course.CategoryId,
            UserId = course.UserId,
            Picture = course.Picture
        };

        return View(courseUpdateDto);
    }

    [HttpPost]
    public async Task<IActionResult> Updates(CourseUpdateDto courseUpdateDto)
    {
        var categories = await _catalogService.GetAllCategoryAsync();
        ViewBag.categoryList = new SelectList(categories, "Id", "Name", courseUpdateDto.Id);
        if (!ModelState.IsValid)
        {
            return View();
        }
        await _catalogService.UpdateCourseAsync(courseUpdateDto);

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Deletes(string id)
    {
        await _catalogService.DeleteCourseAsync(id);

        return RedirectToAction(nameof(Index));
    }
}