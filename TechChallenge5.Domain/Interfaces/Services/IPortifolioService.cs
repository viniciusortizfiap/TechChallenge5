using TechChallenge5.Domain.DTO.Portfolio;
using TechChallenge5.Domain.DTO.Portifolio;
using TechChallenge5.Domain.Entities;

namespace TechChallenge5.Domain.Interfaces.Services
{
    public interface IPortifolioService
    {
        Task<PortifolioDetalhesOutputDto?> GetById(int usuarioId, int portifolioId);
        Task<IList<PortifolioEntity>> GetAll(int usuarioId);
        Task<PortifolioEntity> Add(int usuarioId, CadastrarPortifolioDTO cadastrarPortfolioDTO);
        Task<PortifolioEntity> Update(int portifolioId, AtualizarPortifolioDTO atualizarPortifolioDTO);
        Task Delete(int portifolioId);
    }
}
