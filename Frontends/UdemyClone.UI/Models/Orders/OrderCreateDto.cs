using System.Collections.Generic;

namespace UdemyClone.UI.Models.Orders;

public class OrderCreateDto
{
    public OrderCreateDto()
    {
        OrderItems = new List<OrderItemCreateDto>();
    }
    public string BuyerId { get; set; }

    public List<OrderItemCreateDto> OrderItems { get; set; }

    public AddressCreateDto Address { get; set; }
}
