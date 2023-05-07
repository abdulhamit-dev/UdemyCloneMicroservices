using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UdemyClone.UI.Models.Baskets;
using UdemyClone.UI.Services.Interfaces;

namespace UdemyClone.UI.Controllers;


public class BasketController : Controller
{
    private readonly ICatalogService _catalogService;
    private readonly IBasketService _basketService;

    public BasketController(ICatalogService catalogService, IBasketService basketService)
    {
        _catalogService = catalogService;
        _basketService = basketService;
    }

    public async Task<IActionResult> Index()
    {
        return View(await _basketService.Get());
    }

    public async Task<IActionResult> AddBasketItem(string courseId)
    {
        var course = await _catalogService.GetByCourseId(courseId);

        var basketItem = new BasketItemVM { CourseId = course.Id, CourseName = course.Name, Price = course.Price };

        await _basketService.AddBasketItem(basketItem);

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> RemoveBasketItem(string courseId)
    {
        var result = await _basketService.RemoveBasketItem(courseId);

        return RedirectToAction(nameof(Index));
    }
}