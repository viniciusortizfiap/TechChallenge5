using Microsoft.EntityFrameworkCore;
using TechChallenge5.Data.DataContext;
using TechChallenge5.Data.Repositories;
using TechChallenge5.Tests.Fixtures.Data.Repositories;

namespace TechChallenge5.Tests.Infra.Data.Repositories
{
    public class UsuarioRepositoryTests
    {
        private UsuarioRepository _repository;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _repository = new UsuarioRepository(new DatabaseContext(options));
        }

        [Test]
        public async Task GetByEmailSenha_WhenNoData_ShouldReturnNull()
        {
            var response = await _repository.GetByEmailSenha("email", "senha");

            Assert.IsNull(response);
        }

        [Test]
        public async Task GetByEmailSenha_WhenWrongEmail_ShouldReturnNull()
        {
            var usuario = UsuarioRepositoryFixture.CreateUsuarioEntity();
            usuario.Email = "email@email.com";

            await _repository.Add(usuario);

            var response = await _repository.GetByEmailSenha("email2@email.com", usuario.Senha);

            Assert.IsNull(response);
        }

        [Test]
        public async Task GetByEmailSenha_WhenWrongSenha_ShouldReturnNull()
        {
            var usuario = UsuarioRepositoryFixture.CreateUsuarioEntity();
            usuario.Senha = "senha";

            await _repository.Add(usuario);

            var response = await _repository.GetByEmailSenha(usuario.Email, "1234");

            Assert.IsNull(response);
        }

        [Test]
        public async Task GetByEmailSenha_WhenCOrrectEmailSenha_ShouldReturnUsuario()
        {
            var usuario = UsuarioRepositoryFixture.CreateUsuarioEntity();

            await _repository.Add(usuario);

            var response = await _repository.GetByEmailSenha(usuario.Email, usuario.Senha);

            Assert.IsNotNull(response);
        }
    }
}
