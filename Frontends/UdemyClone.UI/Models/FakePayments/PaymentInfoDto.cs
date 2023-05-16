using UdemyClone.UI.Models.Orders;

public class PaymentInfoDto
{
    public string CardName { get; set; }
    public string CardNumber { get; set; }
    public string Expiration { get; set; }
    public string CVV { get; set; }
    public decimal TotalPrice { get; set; }
    public OrderCreateDto Order { get; set; }
}