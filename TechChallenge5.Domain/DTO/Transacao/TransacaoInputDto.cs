using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TechChallenge5.Domain.DTO.Transacao
{
    public class TransacaoInputDto
    {
        [Required]
        [JsonPropertyName("portifolioId")]
        public int PortifolioId { get; set; }

        [Required]
        [JsonPropertyName("ativoId")]
        public int AtivoId { get; set; }

        [Required]
        [JsonPropertyName("tipoTransacao")]
        public string TipoTransacao { get; set; } = string.Empty;

        [Required]
        [JsonPropertyName("quantidade")]
        public int Quantidade { get; set; }

        [Required]
        [JsonPropertyName("preco")]
        public decimal Preco { get; set; }
    }
}
