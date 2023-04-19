using System.Threading.Tasks;

namespace UdemyClone.UI.Services.Interfaces;

public interface IClientCredentialTokenService
{
    Task<string> GetToken();
}
