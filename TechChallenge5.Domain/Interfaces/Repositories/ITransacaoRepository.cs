using TechChallenge5.Domain.Entities;

namespace TechChallenge5.Domain.Interfaces.Repositories
{
    public interface ITransacaoRepository : IRepository<TransacaoEntity>
    {
        Task<TransacaoEntity?> GetByIdWithAtivoAndPortifolio(int id);

        Task<IEnumerable<TransacaoEntity>> GetAllWithAtivoAndPortifolio();

        Task<IEnumerable<TransacaoEntity>> GetAllByPortifolio(int portifolioId);
    }
}
