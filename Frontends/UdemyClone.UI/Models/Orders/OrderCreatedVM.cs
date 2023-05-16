namespace UdemyClone.UI.Models.Orders;

public class OrderCreatedVM
{
    public int OrderId { get; set; }
    public string Error { get; set; }
    public bool IsSuccessful { get; set; }
}
