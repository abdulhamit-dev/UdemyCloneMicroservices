using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityAPI.Models
{
    public class LoginResponseModel
    {
        public string UserName { get; set; }
        public string Token { get; set; }
    }
}
