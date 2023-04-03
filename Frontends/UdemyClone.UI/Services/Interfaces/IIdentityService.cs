using IdentityModel.Client;
using Shared.Dtos;
using System.Threading.Tasks;
using UdemyClone.UI.Models;

namespace UdemyClone.UI.Services.Interfaces
{
    public interface IIdentityService
    {
        Task<Response<bool>> SignIn(SignInDto signInDto);
        Task<TokenResponse> GetAccessTokenByRefreshToken();
        Task RevokeRefreshToken();
    }
}
