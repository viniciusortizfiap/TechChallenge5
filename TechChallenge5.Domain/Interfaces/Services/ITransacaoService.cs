using TechChallenge5.Domain.DTO.Transacao;

namespace TechChallenge5.Domain.Interfaces.Services
{
    public interface ITransacaoService
    {
        Task<TransacaoOutputDto> Criar(TransacaoInputDto input);

        Task<IEnumerable<TransacaoOutputDto>> ListarTodos();

        Task<TransacaoOutputDto?> BuscarPorId(int id);

        Task<TransacaoOutputDto?> Atualizar(int id, TransacaoInputDto input);

        Task<TransacaoOutputDto?> Deletar(int id);
    }
}
