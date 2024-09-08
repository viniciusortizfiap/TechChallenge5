using TechChallenge5.Domain.Entities;

namespace TechChallenge5.Domain.Interfaces.Repositories
{
    public interface IUsuarioRepository : IRepository<UsuarioEntity>
    {
        Task<UsuarioEntity> GetByEmailSenha(string email, string senha);
    }
}
