using System.Threading.Tasks;
using UdemyClone.UI.Models.Discounts;

namespace UdemyClone.UI.Services.Interfaces;

public interface IDiscountService
{
     Task<DiscountVM> GetDiscount(string discountCode);
}
