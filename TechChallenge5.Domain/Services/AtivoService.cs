using AutoMapper;
using TechChallenge5.Domain.DTO.Ativo;
using TechChallenge5.Domain.Entities;
using TechChallenge5.Domain.Interfaces.Repositories;
using TechChallenge5.Domain.Interfaces.Services;

namespace TechChallenge5.Domain.Services
{
    // O desafio consiste em criar uma plataforma que permita que usuários gerenciem seus
    // portfólios de investimentos, incluindo ações, títulos e criptomoedas.
    // A aplicação deve ser desenvolvida de acordo com a Clean Architecture
    // para garantir uma boa separação de preocupações e facilitar a manutenção e a escalabilidade do sistema.


    public class AtivoService : IAtivoService
    {
        private readonly IAtivoRepository _ativoRepository;
        private readonly IMapper _mapper;

        public AtivoService(
            IAtivoRepository ativoRepository,
            IMapper mapper)
        {
            _ativoRepository = ativoRepository;
            _mapper = mapper;
        }

        public async Task<AtivoEntity> Add(CadastrarAtivoDTO cadastrarAtivoDTO)
        {
            var ativo = _mapper.Map<AtivoEntity>(cadastrarAtivoDTO);

            return await _ativoRepository.Add(ativo);
        }

        public async Task Delete(int ativoId)
        {
            var ativo = await _ativoRepository.GetById(ativoId);

            if (ativo == null)
            {
                throw new Exception("Ativo inexistente");
            }

            await _ativoRepository.Delete(ativo);
        }

        public async Task<IList<AtivoEntity>> GetAll()
        {
            return await _ativoRepository.GetAll();
        }

        public async Task<AtivoEntity> GetById(int ativoId)
        {
            var ativo = await _ativoRepository.GetById(ativoId);

            return ativo;
        }

        public async Task<AtivoEntity> Update(int ativoId, CadastrarAtivoDTO cadastrarAtivoDTO)
        {
            var ativo = await _ativoRepository.GetById(ativoId);

            if (ativo == null)
            {
                throw new Exception("Ativo inexistente");

            }

            var ativoMap = _mapper.Map(cadastrarAtivoDTO, ativo);

            var ativoAlterado = await _ativoRepository.Update(ativoMap);

            return ativoAlterado;
        }
    }
}