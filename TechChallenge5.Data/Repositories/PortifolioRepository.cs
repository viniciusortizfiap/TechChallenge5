using Microsoft.EntityFrameworkCore;
using TechChallenge5.Data.DataContext;
using TechChallenge5.Domain.Entities;
using TechChallenge5.Domain.Interfaces.Repositories;

namespace TechChallenge5.Data.Repositories
{
    public class PortifolioRepository : EFRepository<PortifolioEntity>, IPortifolioRepository
    {
        public PortifolioRepository(DatabaseContext context) : base(context)
        {          
        }

        public async Task<IList<PortifolioEntity>> GetAllWithUser(int usuarioId)
        {
            return await _dbSet.Include(p => p.Usuario).Where(p => p.UsuarioId.Equals(usuarioId)).ToListAsync();
        }
    }
}
