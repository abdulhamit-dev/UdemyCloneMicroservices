using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using UdemyClone.UI.Services.Interfaces;

namespace UdemyClone.UI.Services;

public class PaymentService : IPaymentService
{
    private readonly HttpClient _httpClient;

    public PaymentService(HttpClient httpClient = null)
    {
        _httpClient = httpClient;
    }
    public async Task<bool> ReceivePayment(PaymentInfoDto paymentInfoDto)
    {
        var response = await _httpClient.PostAsJsonAsync<PaymentInfoDto>("fakepayments", paymentInfoDto);
        return response.IsSuccessStatusCode;
    }
}
