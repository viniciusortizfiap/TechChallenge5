using Bogus;
using TechChallenge5.Domain.DTO.Transacao;

namespace TechChallenge5.Tests.Fixtures.Domain.Services
{
    public class TransacaoServiceFixture
    {
        public static Faker _faker { get; set; } = new Faker();
        public static TransacaoInputDto BuscarTransacaoInputDto()
        {
            return new TransacaoInputDto
            {
                TipoTransacao = "Transacao",
                AtivoId = 1,
                PortifolioId = 1,
                Preco = 2,
                Quantidade = 1
            };
        }
    }
}
