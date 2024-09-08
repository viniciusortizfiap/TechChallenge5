using AutoMapper;
using TechChallenge5.Domain.DTO.Portfolio;
using TechChallenge5.Domain.Entities;
using TechChallenge5.Domain.Exceptions;
using TechChallenge5.Domain.Interfaces.Repositories;
using TechChallenge5.Domain.Interfaces.Services;

namespace TechChallenge5.Domain.Services
{
    public class PortifolioService : IPortifolioService
    {
        private readonly IPortifolioRepository _portifolioRepository;
        private readonly IMapper _mapper;

        public PortifolioService(IPortifolioRepository portfolioRepository, IMapper mapper)
        {
            _portifolioRepository = portfolioRepository;
            _mapper = mapper;
        }

        public async Task<PortifolioEntity> Add(int usuarioId, CadastrarPortifolioDTO cadastrarPortfolioDTO)
        {
            var portfolio = _mapper.Map<PortifolioEntity>(cadastrarPortfolioDTO);
            portfolio.UsuarioId = usuarioId;

            return await _portifolioRepository.Add(portfolio);
        }

        public async Task Delete(int portifolioId)
        {
            var portfolio = await _portifolioRepository.GetById(portifolioId);

            if (portfolio == null)
            {
                throw new NotFoundException($"Portfolio não encontrado: {portifolioId}");
            }

            await _portifolioRepository.Delete(portfolio);

        }

        public async Task<IList<PortifolioEntity>> GetAll()
        {
            return await _portifolioRepository.GetAllWithUser();
        }

        public async Task<PortifolioEntity> GetById(int portifolioId)
        {
            var portifolio = await _portifolioRepository.GetById(portifolioId);

            return portifolio;
        }

        public async Task<PortifolioEntity> Update(int portifolioId, AtualizarPortifolioDTO atualizarPortifolioDTO)
        {
            var portfolio = await _portifolioRepository.GetById(portifolioId);

            if (portfolio == null)
            {
                throw new NotFoundException($"Portfolio não encontrado: {portifolioId}");
            }

            var portfolioMap = _mapper.Map(atualizarPortifolioDTO, portfolio);

            var portfolioAlterado = await _portifolioRepository.Update(portfolioMap);

            return portfolioAlterado;
        }
    }
}
