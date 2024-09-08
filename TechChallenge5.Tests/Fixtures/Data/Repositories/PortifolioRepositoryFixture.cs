using Bogus;
using TechChallenge5.Domain.Entities;

namespace TechChallenge5.Tests.Fixtures.Data.Repositories
{
    public class PortifolioRepositoryFixture
    {
        public static Faker _faker { get; set; } = new Faker();

        public static PortifolioEntity CreatePortifolioEntity()
        {
            return new PortifolioEntity(
                nome: _faker.Person.FullName,
                descricao: _faker.Lorem.Sentence(),
                usuarioId: _faker.Random.Int(1, 100));
        }


    }
}
