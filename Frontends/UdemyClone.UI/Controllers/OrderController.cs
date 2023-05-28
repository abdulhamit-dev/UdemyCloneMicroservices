using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UdemyClone.UI.Models.Orders;
using UdemyClone.UI.Services.Interfaces;

namespace UdemyClone.UI.Controllers;


public class OrderController : Controller
{
    private readonly IBasketService _basketService;
    private readonly IOrderService _orderService;

    public OrderController(IBasketService basketService, IOrderService orderService)
    {
        _basketService = basketService;
        _orderService = orderService;
    }

    public async Task<IActionResult> Checkout()
    {
        var basket = await _basketService.Get();

        ViewBag.basket = basket;
        return View(new CheckoutInfoDto());
    }

    [HttpPost]
    public async Task<IActionResult> Checkout(CheckoutInfoDto checkoutInfoDto)
    {
        //1. yol senkron iletişim
        // var orderStatus = await _orderService.CreateOrder(checkoutInfoDto);
        // if (!orderStatus.IsSuccessful)
        // {
        //     var basket = await _basketService.Get();
        //     ViewBag.basket = basket;
        //     ViewBag.error = orderStatus.Error;
        //     return View();
        // }
        
        // 2.yol asenkron iletişim
        var orderSuspend = await _orderService.SuspendOrder(checkoutInfoDto);
        if (!orderSuspend.IsSuccessful)
        {
            var basket = await _basketService.Get();

            ViewBag.basket = basket;

            ViewBag.error = orderSuspend.Error;

            return View();
        }

        //1. yol senkron iletişim
        //  return RedirectToAction(nameof(SuccessfulCheckout), new { orderId = orderStatus.OrderId });

        //2.yol asenkron iletişim
        return RedirectToAction(nameof(SuccessfulCheckout), new { orderId = new Random().Next(1, 1000) });
    }

    public IActionResult SuccessfulCheckout(int orderId)
    {
        ViewBag.orderId = orderId;
        return View();
    }

    public async Task<IActionResult> CheckoutHistory()
    {
        return View(await _orderService.GetOrder());
    }
    
}