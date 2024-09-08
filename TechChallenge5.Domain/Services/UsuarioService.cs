using AutoMapper;
using System.Security.Cryptography;
using System.Text;
using TechChallenge5.Domain.DTO.Login;
using TechChallenge5.Domain.DTO.Usuario;
using TechChallenge5.Domain.Entities;
using TechChallenge5.Domain.Exceptions;
using TechChallenge5.Domain.Interfaces.Repositories;
using TechChallenge5.Domain.Interfaces.Services;

namespace TechChallenge5.Domain.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMapper _mapper;

        public UsuarioService(
            IUsuarioRepository usuarioRepository,
            IMapper mapper)
        {
            _usuarioRepository = usuarioRepository;
            _mapper = mapper;
        }

        public async Task<UsuarioEntity> Add(CadastrarUsuarioDTO cadastrarUsuarioDTO)
        {
            var usuario = _mapper.Map<UsuarioEntity>(cadastrarUsuarioDTO);
            usuario.Senha = GerarHashSenha(usuario.Senha);

            return await _usuarioRepository.Add(usuario);
        }

        public async Task Delete(int usuarioId)
        {
            var usuario = await _usuarioRepository.GetById(usuarioId);

            if (usuario == null)
            {
                throw new NotFoundException("Usuário inexistente");
            }

            await _usuarioRepository.Delete(usuario);
        }

        public string GerarHashSenha(string senha)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(senha));

                return BitConverter.ToString(bytes).Replace("-", "");
            }
        }

        public async Task<IList<UsuarioEntity>> GetAll()
        {
            return await _usuarioRepository.GetAll();
        }

        public Task<UsuarioEntity> GetByEmailSenha(AutenticarDto autenticarDto)
        {
            autenticarDto.senha = GerarHashSenha(autenticarDto.senha);

            return _usuarioRepository.GetByEmailSenha(autenticarDto.email, autenticarDto.senha);
        }

        public async Task<UsuarioEntity> GetById(int usuarioId)
        {
            var usuario = await _usuarioRepository.GetById(usuarioId);

            return usuario;
        }

        public async Task<UsuarioEntity> Update(int usuarioId, CadastrarUsuarioDTO cadastrarUsuarioDTO)
        {
            var usuario = await _usuarioRepository.GetById(usuarioId);

            if (usuario == null)
            {
                throw new NotFoundException("Usuário inexistente");
            }

            var usuarioMap = _mapper.Map(cadastrarUsuarioDTO, usuario);
            usuarioMap.Senha = GerarHashSenha(usuarioMap.Senha);

            var usuarioAlterado = await _usuarioRepository.Update(usuarioMap);

            return usuarioAlterado;
        }
    }
}
