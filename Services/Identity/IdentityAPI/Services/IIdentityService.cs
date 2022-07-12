using IdentityAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityAPI.Services
{
    public interface IIdentityService
    {
        Task<LoginResponseModel> Login(LoginRequestModel loginRequestModel);
    }
}
