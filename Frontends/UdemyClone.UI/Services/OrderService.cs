using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Shared.Dtos;
using Shared.Services;
using UdemyClone.UI.Models.Orders;
using UdemyClone.UI.Services.Interfaces;

namespace UdemyClone.UI.Services;

public class OrderService : IOrderService
{
    private readonly IPaymentService _paymentService;
    private readonly HttpClient _httpClient;
    private readonly IBasketService _basketService;
    private readonly ISharedIdentityService _sharedIdentityService;

    public OrderService(IPaymentService paymentService, HttpClient httpClient, IBasketService basketService, ISharedIdentityService sharedIdentityService)
    {
        _paymentService = paymentService;
        _httpClient = httpClient;
        _basketService = basketService;
        _sharedIdentityService = sharedIdentityService;
    }
    public async Task<OrderCreatedVM> CreateOrder(CheckoutInfoDto checkoutInfoDto)
    {
        var basket = await _basketService.Get();

        var paymentInfoDto = new PaymentInfoDto()
        {
            CardName = checkoutInfoDto.CardName,
            CardNumber = checkoutInfoDto.CardNumber,
            Expiration = checkoutInfoDto.Expiration,
            CVV = checkoutInfoDto.CVV,
            TotalPrice = basket.TotalPrice
        };
        var responsePayment = await _paymentService.ReceivePayment(paymentInfoDto);

        if (!responsePayment)
        {
            return new OrderCreatedVM() { Error = "Ödeme alınamadı", IsSuccessful = false };
        }

        var orderCreateDto = new OrderCreateDto()
        {
            BuyerId = _sharedIdentityService.GetUserId,
            Address = new AddressCreateDto
            {
                Province = checkoutInfoDto.Province,
                District = checkoutInfoDto.District,
                Street = checkoutInfoDto.Street,
                Line = checkoutInfoDto.Line,
                ZipCode = checkoutInfoDto.ZipCode
            },
        };

        basket.BasketItems.ForEach(x =>
        {
            var orderItem = new OrderItemCreateDto
            {
                ProductId = x.CourseId,
                Price = x.GetCurrentPrice,
                PictureUrl = "",
                ProductName = x.CourseName
            };
            orderCreateDto.OrderItems.Add(orderItem);
        });

        var response = await _httpClient.PostAsJsonAsync<OrderCreateDto>("orders", orderCreateDto);

        if (!response.IsSuccessStatusCode)
        {
            return new OrderCreatedVM() { Error = "Sipariş oluşturulamadı", IsSuccessful = false };
        }

        var orderCreatedViewModel = await response.Content.ReadFromJsonAsync<Response<OrderCreatedVM>>();

        orderCreatedViewModel.Data.IsSuccessful = true;
        await _basketService.Delete();
        return orderCreatedViewModel.Data;
    }

    public async Task<List<OrderVM>> GetOrder()
    {
        var response = await _httpClient.GetFromJsonAsync<Response<List<OrderVM>>>("orders");

        return response.Data;
    }

    public async Task<OrderSuspendVM> SuspendOrder(CheckoutInfoDto checkoutInfoDto)
    {
        var basket = await _basketService.Get();
        var orderCreateDto = new OrderCreateDto()
        {
            BuyerId = _sharedIdentityService.GetUserId,
            Address = new AddressCreateDto
            {
                Province = checkoutInfoDto.Province,
                District = checkoutInfoDto.District,
                Street = checkoutInfoDto.Street,
                Line = checkoutInfoDto.Line,
                ZipCode = checkoutInfoDto.ZipCode
            },
        };

        basket.BasketItems.ForEach(x =>
        {
            var orderItem = new OrderItemCreateDto { ProductId = x.CourseId, Price = x.GetCurrentPrice, PictureUrl = "", ProductName = x.CourseName };
            orderCreateDto.OrderItems.Add(orderItem);
        });

        var paymentInfoDto = new PaymentInfoDto()
        {
            CardName = checkoutInfoDto.CardName,
            CardNumber = checkoutInfoDto.CardNumber,
            Expiration = checkoutInfoDto.Expiration,
            CVV = checkoutInfoDto.CVV,
            TotalPrice = basket.TotalPrice,
            Order = orderCreateDto
        };

        var responsePayment = await _paymentService.ReceivePayment(paymentInfoDto);

        if (!responsePayment)
        {
            return new OrderSuspendVM() { Error = "Ödeme alınamadı", IsSuccessful = false };
        }

        await _basketService.Delete();
        return new OrderSuspendVM() { IsSuccessful = true };
    }
}

