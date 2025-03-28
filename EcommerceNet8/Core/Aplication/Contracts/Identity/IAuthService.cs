using EcommerceNet8.Core.Domain;

namespace EcommerceNet8.Core.Aplication.Contracts.Identity
{
    public interface IAuthService
    {

        string GetSessionUser();

        string CreateToken(Usuario usuario, IList<string>? roles);
    }
}
