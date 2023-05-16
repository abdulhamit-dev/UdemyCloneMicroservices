using FakePaymentAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.ControllerBases;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FakePaymentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FakePaymentsController : CustomBaseController
    {
        [HttpPost]
        public IActionResult ReceivePayment(PaymentDto paymentDto)
        {
            return CreateActionResultInstance(Response<NoContent>.Success(200));
        }
    }
}
