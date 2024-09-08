using AutoMapper;
using TechChallenge5.Domain.DTO.Transacao;
using TechChallenge5.Domain.Entities;
using TechChallenge5.Domain.Exceptions;
using TechChallenge5.Domain.Interfaces.Repositories;
using TechChallenge5.Domain.Interfaces.Services;

namespace TechChallenge5.Domain.Services
{
    public class TransacaoService : ITransacaoService
    {
        private readonly ITransacaoRepository _transacaoRepository;
        private readonly IAtivoRepository _ativoRepository;
        private readonly IPortifolioRepository _portifolioRepository;
        private readonly IMapper _mapper;

        public TransacaoService(
            ITransacaoRepository transacaoRepository,
            IAtivoRepository ativoRepository,
            IPortifolioRepository portifolioRepository,
            IMapper mapper
        )
        {
            _transacaoRepository = transacaoRepository;
            _ativoRepository = ativoRepository;
            _portifolioRepository = portifolioRepository;
            _mapper = mapper;
        }
        public async Task<TransacaoOutputDto?> Atualizar(int id, TransacaoInputDto input)
        {
            var transacao = await _transacaoRepository.GetByIdWithAtivoAndPortifolio(id);

            if (transacao is null)
            {
                return null;
            }

            var entity = _mapper.Map<TransacaoEntity>(input);
            entity.Id = transacao.Id;
            entity.Portifolio = transacao.Portifolio;
            entity.Ativo = transacao.Ativo;

            await _transacaoRepository.Update(entity);

            return DtoFromEntity(entity);
        }

        public async Task<TransacaoOutputDto?> BuscarPorId(int id)
        {
            var transacao = await _transacaoRepository.GetByIdWithAtivoAndPortifolio(id);

            if (transacao is null)
            {
                return null;
            }

            return DtoFromEntity(transacao);
        }

        public async Task<TransacaoOutputDto> Criar(TransacaoInputDto input)
        {
            var ativo = await _ativoRepository.GetById(input.AtivoId);

            if (ativo is null)
            {
                throw new NotFoundException($"Ativo não localizado com id: {input.AtivoId}");
            }

            var portifolio = await _portifolioRepository.GetById(input.PortifolioId);

            if (portifolio is null)
            {
                throw new NotFoundException($"Portifolio não localizado com id: {input.PortifolioId}");
            }

            var entity = _mapper.Map<TransacaoEntity>(input);

            await _transacaoRepository.Add(entity);

            return DtoFromEntity(entity);
        }

        public async Task<TransacaoOutputDto?> Deletar(int id)
        {
            var transacao = await _transacaoRepository.GetById(id);

            if (transacao is null)
            {
                return null;
            }

            await _transacaoRepository.Delete(transacao);

            return DtoFromEntity(transacao);
        }

        public async Task<IEnumerable<TransacaoOutputDto>> ListarTodos()
        {
            var list = await _transacaoRepository.GetAllWithAtivoAndPortifolio();

            var listOutput = list.Select(DtoFromEntity);

            return listOutput;
        }

        private TransacaoOutputDto DtoFromEntity(TransacaoEntity entity)
        {
            return new TransacaoOutputDto
            {
                Id = entity.Id,
                Ativo = entity.Ativo,
                CriadoEm = entity.CriadoEm,
                Portifolio = entity.Portifolio,
                Preco = entity.Preco,
                Quantidade = entity.Quantidade
            };
        }
    }
}
