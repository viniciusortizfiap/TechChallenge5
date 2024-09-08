namespace TechChallenge5.Domain.DTO.Portifolio
{
    public class PortifolioDetalhesOutputDto
    {
        public string Nome { get; set; } = null!;
        public string Descricao { get; set; } = null!;
        public IEnumerable<PortifolioDetalhesQuantidadeOutputDto> Detalhes { get; set; } = [];
    }
}
