using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechChallenge5.Domain.DTO.Ativo
{
    public class CadastrarAtivoDTO
    {
        public string TipoAtivo { get; set; } = null!;
        public string Nome { get; set; } = null!;
        public string Codigo { get; set; } = null!;
    }
}
