using Bogus;
using TechChallenge5.Domain.Entities;

namespace TechChallenge5.Tests.Fixtures.Data.Repositories
{
    public class UsuarioRepositoryFixture
    {
        public static Faker _faker { get; set; } = new Faker();

        public static UsuarioEntity CreateUsuarioEntity()
        {
            return new UsuarioEntity(
                nome: _faker.Person.FullName,
                email: _faker.Person.Email,
                senha: _faker.Random.AlphaNumeric(8));
        }
    }
}
