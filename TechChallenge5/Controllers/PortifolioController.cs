using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TechChallenge5.Domain.DTO.Portfolio;
using TechChallenge5.Domain.DTO.Portifolio;
using TechChallenge5.Domain.Interfaces.Services;

namespace TechChallenge5.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class PortifolioController : ControllerBase
    {
        private readonly IPortifolioService _portifolioService;

        public PortifolioController(IPortifolioService portfolioService)
        {
            _portifolioService = portfolioService;
        }

        [HttpGet("todos-portfolios")]
        public async Task<IActionResult> GetAll()
        {
            var id = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "Id").Value);
            return Ok(await _portifolioService.GetAll(id));
        }

        [HttpGet("portfolio-id/{id:int}")]
        public async Task<ActionResult<PortifolioDetalhesOutputDto>> GetById(int id)
        {
            var usuarioId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "Id").Value);

            var portfolio = await _portifolioService.GetById(usuarioId, id);

            if (portfolio == null)
                return NotFound();

            return Ok(portfolio);
        }

        [HttpPost("cadastrar")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CadastrarPortifolioDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Cadastrar(CadastrarPortifolioDTO portfolioDTO)
        {
            var id = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "Id").Value);
            var portfolio = await _portifolioService.Add(id, portfolioDTO);
            return Ok(portfolio);
        }

        [HttpPut("alterar/{id:int}")]
        public async Task<IActionResult> Alterar(int id, [FromBody] AtualizarPortifolioDTO portfolioDTO)
        {
            var portfolio = await _portifolioService.Update(id, portfolioDTO);

            return Ok(portfolio);
        }

        [HttpDelete("deletar/{id:int}")]
        public async Task<IActionResult> Deletar(int id)
        {
            await _portifolioService.Delete(id);

            return Ok("Portfolio Deletado");
        }
    }
}
