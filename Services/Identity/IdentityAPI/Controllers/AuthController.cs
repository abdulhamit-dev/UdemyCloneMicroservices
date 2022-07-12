using IdentityAPI.Models;
using IdentityAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IIdentityService _identityService;

        public AuthController(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginRequestModel loginRequestModel)
        {
            var result = await _identityService.Login(loginRequestModel);
            return Ok(result);
        }
    }
}
