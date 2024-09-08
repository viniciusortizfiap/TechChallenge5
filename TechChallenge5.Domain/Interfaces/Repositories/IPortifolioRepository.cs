using TechChallenge5.Domain.Entities;

namespace TechChallenge5.Domain.Interfaces.Repositories
{
    public interface IPortifolioRepository : IRepository<PortifolioEntity>
    {
        Task<IList<PortifolioEntity>> GetAllWithUser(int usuarioId);
    }
}
