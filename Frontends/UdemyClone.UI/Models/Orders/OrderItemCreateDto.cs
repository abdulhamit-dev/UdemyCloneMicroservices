using System;

namespace UdemyClone.UI.Models.Orders;

public class OrderItemCreateDto
{
    public string ProductId { get; set; }
    public string ProductName { get; set; }
    public string PictureUrl { get; set; }
    public Decimal Price { get; set; }
}
