using TechChallenge5.Domain.DTO.Login;
using TechChallenge5.Domain.DTO.Usuario;
using TechChallenge5.Domain.Entities;

namespace TechChallenge5.Domain.Interfaces.Services
{
    public interface IUsuarioService
    {
        Task<UsuarioEntity> GetById(int usuarioId);
        Task<IList<UsuarioEntity>> GetAll();
        Task<UsuarioEntity> Add(CadastrarUsuarioDTO cadastrarUsuarioDTO);
        Task<UsuarioEntity> Update(int usuarioId, CadastrarUsuarioDTO cadastrarUsuarioDTO);
        Task Delete(int usuarioId);
        Task<UsuarioEntity> GetByEmailSenha(AutenticarDto autenticarDto);
        string GerarHashSenha(string senha);
    }
}
