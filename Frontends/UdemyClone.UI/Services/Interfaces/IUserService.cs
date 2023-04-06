using System.Threading.Tasks;
using UdemyClone.UI.Models;

namespace UdemyClone.UI.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserViewModel> GetUser();
    }
}
