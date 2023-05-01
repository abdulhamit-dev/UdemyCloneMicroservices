using System.Threading.Tasks;
using UdemyClone.UI.Models.Baskets;

namespace UdemyClone.UI.Services.Interfaces;

public interface IBasketService
{

    Task<bool> SaveOrUpdate(BasketVM basketVM);

    Task<BasketVM> Get();

    Task<bool> Delete();

    Task AddBasketItem(BasketItemVM basketItemVM);

    Task<bool> RemoveBasketItem(string courseId);

    Task<bool> ApplyDiscount(string discountCode);

    Task<bool> CancelApplyDiscount();
}
