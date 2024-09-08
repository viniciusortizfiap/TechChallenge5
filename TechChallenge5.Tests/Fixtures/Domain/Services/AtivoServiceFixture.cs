using Bogus;
using TechChallenge5.Domain.DTO.Ativo;
using TechChallenge5.Domain.Entities;

namespace TechChallenge5.Tests.Fixtures.Domain.Services
{
    public class AtivoServiceFixture
    {
        public static Faker _faker { get; set; } = new Faker();

        public static AtivoEntity CreateAtivoEntity()
        {
            return new AtivoEntity(
                tipoAtivo: _faker.Random.AlphaNumeric(8),
                nome: _faker.Random.AlphaNumeric(8),
                codigo: _faker.Random.AlphaNumeric(8));
        }

        public static CadastrarAtivoDTO CreateAtivoDto()
        {
            return new CadastrarAtivoDTO
            {
                TipoAtivo = _faker.Random.AlphaNumeric(8),
                Nome = _faker.Random.AlphaNumeric(8),
                Codigo = _faker.Random.AlphaNumeric(8)
            };
        }
    }
}
