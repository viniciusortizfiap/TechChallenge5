using TechChallenge5.Domain.Entities;

namespace TechChallenge5.Domain.Interfaces.Services
{
    public interface ITokenService
    {
        string GetToken(UsuarioEntity usuario);
    }
}
