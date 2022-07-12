using IdentityAPI.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace IdentityAPI.Services
{
    public class IdentityService : IIdentityService
    {
        IConfiguration _configuration;
            
        
        public IdentityService(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public Task<LoginResponseModel> Login(LoginRequestModel loginRequestModel)
        {
            //db den kontrol yapılarak da alınabilir.
            //burada amaç microservice'de token dağıtma örneği
            var claims = new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier,loginRequestModel.UserName),
                new Claim(ClaimTypes.Name,"Hamit")
            };
            var now = DateTime.UtcNow;
            var tokenOptions = _configuration.GetSection("TokenOptions");
            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(tokenOptions["SecurityKey"]));
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey,
                ValidateIssuer = true,
                ValidIssuer = tokenOptions["Iss"],
                ValidateAudience = true,
                ValidAudience = tokenOptions["Aud"],
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,
                RequireExpirationTime = true,
            };

            var jwt = new JwtSecurityToken(
                issuer: tokenOptions["Iss"],
                audience: tokenOptions["Aud"],
                claims: claims,
                notBefore: now,
                expires: now.Add(TimeSpan.FromMinutes(500)),
                signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256)
            );

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            LoginResponseModel response = new()
            {
                Token = encodedJwt,
                UserName = loginRequestModel.UserName
            };

            return Task.FromResult(response);
        }
    }
}
