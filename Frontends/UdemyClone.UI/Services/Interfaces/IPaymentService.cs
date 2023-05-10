using System.Threading.Tasks;

namespace UdemyClone.UI.Services.Interfaces;

public interface IPaymentService
{
      Task<bool> ReceivePayment(PaymentInfoDto paymentInfoDto);
}
