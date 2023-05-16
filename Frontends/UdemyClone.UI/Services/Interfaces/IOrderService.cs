using System.Collections.Generic;
using System.Threading.Tasks;
using UdemyClone.UI.Models.Orders;

namespace UdemyClone.UI.Services.Interfaces;

public interface IOrderService
{
        Task<OrderCreatedVM> CreateOrder(CheckoutInfoDto checkoutInfoDto);
        Task<OrderSuspendVM> SuspendOrder(CheckoutInfoDto checkoutInfoInput);
        Task<List<OrderVM>> GetOrder();
}
