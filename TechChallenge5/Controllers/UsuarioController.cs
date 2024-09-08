using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TechChallenge5.Domain.DTO.Usuario;
using TechChallenge5.Domain.Interfaces.Services;

namespace TechChallenge5.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpGet("todos-usuarios")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _usuarioService.GetAll());
        }

        [HttpGet("usuario-id/{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var usuario = await _usuarioService.GetById(id);

            if (usuario == null)
                return NotFound();

            return Ok(usuario);
        }

        [HttpPost("cadastrar")]
        public async Task<IActionResult> Cadastrar(CadastrarUsuarioDTO usuarioDto)
        {
            var usuario = await _usuarioService.Add(usuarioDto);
            return Ok(usuario);
        }

        [Authorize]
        [HttpPut("alterar")]
        public async Task<IActionResult> Alterar([FromBody]  CadastrarUsuarioDTO usuarioDto)
        {
            var id = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "Id").Value);
            var usuario = await _usuarioService.Update(id, usuarioDto);

            return Ok(usuario);
        }

        [HttpDelete("deletar/{id:int}")]
        public async Task<IActionResult> Deletar(int id)
        {
            await _usuarioService.Delete(id);

            return Ok("Usuário Deletado");
        }
    }
}
