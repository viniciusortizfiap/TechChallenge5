using Bogus;
using TechChallenge5.Domain.DTO.Usuario;
using TechChallenge5.Domain.Entities;

namespace TechChallenge5.Tests.Fixtures.Domain.Services
{
    public class UsuarioServiceFixture
    {
        public static Faker _faker { get; set; } = new Faker();

        public static UsuarioEntity CreateUsuarioEntity()
        {
            return new UsuarioEntity(
                nome: _faker.Person.FullName,
                email: _faker.Person.Email,
                senha: _faker.Random.AlphaNumeric(8));
        }

        public static CadastrarUsuarioDTO CreateUsuarioDto()
        {
            return new CadastrarUsuarioDTO
            {
                Nome = _faker.Person.FullName,
                Email = _faker.Person.Email,
                Senha = _faker.Random.AlphaNumeric(8)
            };
        }
    }
}
