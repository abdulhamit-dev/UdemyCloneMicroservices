using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Shared.Dtos;
using UdemyClone.UI.Models.Baskets;
using UdemyClone.UI.Services.Interfaces;

namespace UdemyClone.UI.Services;

public class BasketService : IBasketService
{
    private readonly HttpClient _httpClient;


    public BasketService(HttpClient httpClient)
    {
        _httpClient = httpClient;

    }

    public async Task AddBasketItem(BasketItemVM basketItemVM)
    {
        var basket = await Get();

        if (basket != null)
        {
            if (!basket.BasketItems.Any(x => x.CourseId == basketItemVM.CourseId))
            {
                basket.BasketItems.Add(basketItemVM);
            }
        }
        else
        {
            basket = new BasketVM();

            basket.BasketItems.Add(basketItemVM);
        }

        await SaveOrUpdate(basket);
    }

    public Task<bool> ApplyDiscount(string discountCode)
    {
        throw new System.NotImplementedException();
    }

    public Task<bool> CancelApplyDiscount()
    {
        throw new System.NotImplementedException();
    }

    public async Task<bool> Delete()
    {
        var result = await _httpClient.DeleteAsync("baskets");

        return result.IsSuccessStatusCode;
    }

    public async Task<BasketVM> Get()
    {
        var response = await _httpClient.GetAsync("baskets");

        if (!response.IsSuccessStatusCode)
        {
            return null;
        }
        var basketViewModel = await response.Content.ReadFromJsonAsync<Response<BasketVM>>();

        return basketViewModel.Data;
    }

    public async Task<bool> RemoveBasketItem(string courseId)
    {
        var basket = await Get();

        if (basket == null)

        {
            return false;
        }

        var deleteBasketItem = basket.BasketItems.FirstOrDefault(x => x.CourseId == courseId);

        if (deleteBasketItem == null)
        {
            return false;
        }

        var deleteResult = basket.BasketItems.Remove(deleteBasketItem);

        if (!deleteResult)
        {
            return false;
        }

        if (!basket.BasketItems.Any())
        {
            basket.DiscountCode = null;
        }

        return await SaveOrUpdate(basket);
    }

    public async Task<bool> SaveOrUpdate(BasketVM basketVM)
    {
        var response = await _httpClient.PostAsJsonAsync<BasketVM>("baskets", basketVM);
        return response.IsSuccessStatusCode;
    }
}
