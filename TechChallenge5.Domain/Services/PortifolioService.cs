using AutoMapper;
using TechChallenge5.Domain.DTO.Portfolio;
using TechChallenge5.Domain.DTO.Portifolio;
using TechChallenge5.Domain.Entities;
using TechChallenge5.Domain.Exceptions;
using TechChallenge5.Domain.Interfaces.Repositories;
using TechChallenge5.Domain.Interfaces.Services;

namespace TechChallenge5.Domain.Services
{
    public class PortifolioService : IPortifolioService
    {
        private readonly IPortifolioRepository _portifolioRepository;
        private readonly ITransacaoRepository _transacaoRepository;
        private readonly IMapper _mapper;

        public PortifolioService(
            IPortifolioRepository portfolioRepository,
            ITransacaoRepository transacaoRepository,
            IMapper mapper
        )
        {
            _portifolioRepository = portfolioRepository;
            _transacaoRepository = transacaoRepository;
            _mapper = mapper;
        }

        public async Task<PortifolioEntity> Add(int usuarioId, CadastrarPortifolioDTO cadastrarPortfolioDTO)
        {
            var portifolio = new PortifolioEntity(usuarioId, cadastrarPortfolioDTO.Nome, cadastrarPortfolioDTO.Descricao);

            return await _portifolioRepository.Add(portifolio);
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

        public async Task<IList<PortifolioEntity>> GetAll(int usuarioId)
        {
            return await _portifolioRepository.GetAllWithUser(usuarioId);
        }

        public async Task<PortifolioDetalhesOutputDto?> GetById(int usuarioId, int portifolioId)
        {
            var portifolio = await _portifolioRepository.GetById(portifolioId);

            if (portifolio is not null && !portifolio.UsuarioId.Equals(usuarioId))
            {
                return null;
            }

            var transacoes = await _transacaoRepository.GetAllByPortifolio(portifolioId);

            var resultadoPorAtivo = transacoes
                .GroupBy(t => t.Ativo.Codigo)
                .Select(g => new PortifolioDetalhesQuantidadeOutputDto
                {
                    Codigo = g.Key,
                    Quantidade = g.Sum(t => t.TipoTransacao == "COMPRA" ? t.Quantidade : -t.Quantidade)
                });

            var output = new PortifolioDetalhesOutputDto
            {
                Descricao = portifolio.Descricao,
                Nome = portifolio.Nome,
                Detalhes = resultadoPorAtivo
            };

            return output;
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
