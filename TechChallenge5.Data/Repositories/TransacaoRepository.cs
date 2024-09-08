using Microsoft.EntityFrameworkCore;
using TechChallenge5.Data.DataContext;
using TechChallenge5.Domain.Entities;
using TechChallenge5.Domain.Interfaces.Repositories;

namespace TechChallenge5.Data.Repositories
{
    public class TransacaoRepository : EFRepository<TransacaoEntity>, ITransacaoRepository
    {
        public TransacaoRepository(DatabaseContext context) : base(context)
        {
        }

        public async Task<IEnumerable<TransacaoEntity>> GetAllWithAtivoAndPortifolio()
        {
            return await _dbSet
                .AsNoTracking()
                .Include(p => p.Ativo)
                .Include(p => p.Portifolio)
                .ToListAsync();
        }

        public async Task<TransacaoEntity?> GetByIdWithAtivoAndPortifolio(int id)
        {
            return await _dbSet
                .AsNoTracking()
                .Include(p => p.Ativo)
                .Include(p => p.Portifolio)
                .FirstOrDefaultAsync(p => p.Id.Equals(id));
        }
    }
}
