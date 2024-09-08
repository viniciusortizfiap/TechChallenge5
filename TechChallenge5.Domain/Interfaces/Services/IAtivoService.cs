using TechChallenge5.Domain.DTO.Ativo;
using TechChallenge5.Domain.Entities;

namespace TechChallenge5.Domain.Interfaces.Services
{
    public interface IAtivoService
    {
        Task<AtivoEntity> GetById(int ativoId);
        Task<IList<AtivoEntity>> GetAll();
        Task<AtivoEntity> Add(CadastrarAtivoDTO cadastrarAtivoDTO);
        Task<AtivoEntity> Update(int ativoId, CadastrarAtivoDTO cadastrarAtivoDTO);
        Task Delete(int ativoId);
    }
}
