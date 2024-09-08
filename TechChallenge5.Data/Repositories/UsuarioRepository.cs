using Microsoft.EntityFrameworkCore;
using TechChallenge5.Data.DataContext;
using TechChallenge5.Domain.Entities;
using TechChallenge5.Domain.Interfaces.Repositories;

namespace TechChallenge5.Data.Repositories
{
    public class UsuarioRepository : EFRepository<UsuarioEntity>, IUsuarioRepository
    {
        public UsuarioRepository(DatabaseContext context) : base(context)
        {
        }

        public async Task<UsuarioEntity> GetByEmailSenha(string email, string senha)
        {
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(
                u => u.Email == email && u.Senha == senha);

            return usuario;
        }
    }
}
