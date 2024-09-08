using Bogus;
using TechChallenge5.Domain.Entities;

namespace TechChallenge5.Tests.Fixtures.Data.Repositories
{
    public class AtivoRepositoryFixture
    {
        public static Faker _faker { get; set; } = new Faker();

        public static AtivoEntity CreateAtivoEntity()
        {
            return new AtivoEntity(
                tipoAtivo: _faker.Random.String2(10),
                nome: _faker.Random.String2(20),
                codigo: _faker.Random.String2(10));
        }
    }
}
