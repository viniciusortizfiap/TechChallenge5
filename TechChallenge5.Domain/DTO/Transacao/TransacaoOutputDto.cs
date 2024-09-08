using System.Text.Json.Serialization;
using TechChallenge5.Domain.Entities;

namespace TechChallenge5.Domain.DTO.Transacao
{
    public class TransacaoOutputDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("portifolio")]
        public PortifolioEntity Portifolio { get; set; } = null!;

        [JsonPropertyName("ativo")]
        public AtivoEntity Ativo { get; set; } = null!;

        [JsonPropertyName("tipoTransacao")]
        public string TipoTransacao { get; set; } = string.Empty;

        [JsonPropertyName("quantidade")]
        public int Quantidade { get; set; }

        [JsonPropertyName("preco")]
        public decimal Preco { get; set; }

        [JsonPropertyName("criadoEm")]
        public DateTime CriadoEm { get; set; }
    }
}
