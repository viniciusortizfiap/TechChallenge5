using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TechChallenge5.Domain.DTO.Transacao;
using TechChallenge5.Domain.Interfaces.Services;

namespace TechChallenge5.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class TransacaoController : ControllerBase
    {
        private readonly ITransacaoService _transacaoService;

        public TransacaoController(ITransacaoService transacaoService)
        {
            _transacaoService = transacaoService;
        }

        // Método para criar uma nova transação
        [HttpPost]
        public async Task<ActionResult<TransacaoOutputDto>> Create([FromBody] TransacaoInputDto input)
        {
            if (input == null)
            {
                return BadRequest("Input não pode ser nulo");
            }

            var result = await _transacaoService.Criar(input);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        // Método para buscar uma transação por ID
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TransacaoOutputDto))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<TransacaoOutputDto>> GetById(int id)
        {
            var result = await _transacaoService.BuscarPorId(id);
            if (result == null)
            {
                return NoContent();
            }

            return Ok(result);
        }

        // Método para atualizar uma transação existente
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TransacaoOutputDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<TransacaoOutputDto>> Update(int id, [FromBody] TransacaoInputDto input)
        {
            if (input == null)
            {
                return BadRequest("Input não pode ser nulo");
            }

            var result = await _transacaoService.Atualizar(id, input);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        // Método para deletar uma transação
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TransacaoOutputDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TransacaoOutputDto>> Delete(int id)
        {
            var result = await _transacaoService.Deletar(id);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        // Método para listar todas as transações
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TransacaoOutputDto>))]
        public async Task<ActionResult<IEnumerable<TransacaoOutputDto>>> ListAll()
        {
            var result = await _transacaoService.ListarTodos();
            return Ok(result);
        }
    }
}
