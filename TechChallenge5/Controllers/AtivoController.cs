using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TechChallenge5.Domain.DTO.Ativo;
using TechChallenge5.Domain.Entities;
using TechChallenge5.Domain.Interfaces.Services;

namespace TechChallenge5.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AtivoController : ControllerBase
    {
        private readonly IAtivoService _ativoService;

        public AtivoController(IAtivoService ativoService)
        {
            _ativoService = ativoService;
        }

        [HttpGet("todos-ativos")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _ativoService.GetAll());
        }

        [HttpGet("ativo-id/{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var ativo = await _ativoService.GetById(id);

            if (ativo == null)
                return NotFound();

            return Ok(ativo);
        }

        [HttpPost("cadastrar")]
        public async Task<IActionResult> Cadastrar(CadastrarAtivoDTO ativoDTO)
        {
            var ativo = await _ativoService.Add(ativoDTO);
            return Ok(ativo);
        }

        [HttpPut("alterar/{id:int}")]
        public async Task<IActionResult> Alterar(int id, [FromBody] CadastrarAtivoDTO ativoDTO)
        {
            var ativo = await _ativoService.Update(id, ativoDTO);

            return Ok(ativo);
        }

        [HttpDelete("deletar/{id:int}")]
        public async Task<IActionResult> Deletar(int id)
        {
            await _ativoService.Delete(id);

            return Ok("Usuário Deletado");
        }
    }
}
