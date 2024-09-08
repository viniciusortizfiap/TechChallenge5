using Microsoft.AspNetCore.Mvc;
using TechChallenge5.Domain.DTO.Login;
using TechChallenge5.Domain.Interfaces.Services;

namespace TechChallenge5.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        private readonly ITokenService _tokenService;

        public LoginController(IUsuarioService usuarioService, ITokenService tokenService)
        {
            _usuarioService = usuarioService;
            _tokenService = tokenService;
        }

        [HttpPost]
        public async Task<IActionResult> Autenticar([FromBody] AutenticarDto autenticarDto)
        {
            var usuario = await _usuarioService.GetByEmailSenha(autenticarDto);

            if (usuario == null)
            {
                return NotFound("Usuário não encontrado. Email ou Senha incorretos");
            }

            var token = _tokenService.GetToken(usuario);

            return Ok(new { Token = token });
        }
    }
}
