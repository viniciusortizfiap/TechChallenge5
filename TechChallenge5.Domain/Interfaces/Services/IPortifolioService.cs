using TechChallenge5.Domain.DTO.Portfolio;
using TechChallenge5.Domain.Entities;

namespace TechChallenge5.Domain.Interfaces.Services
{
    public interface IPortifolioService
    {
        Task<PortifolioEntity> GetById(int portifolioId);
        Task<IList<PortifolioEntity>> GetAll();
        Task<PortifolioEntity> Add(CadastrarPortifolioDTO cadastrarPortfolioDTO);
        Task<PortifolioEntity> Update(int portifolioId, AtualizarPortifolioDTO atualizarPortifolioDTO);
        Task Delete(int portifolioId);
    }
}
