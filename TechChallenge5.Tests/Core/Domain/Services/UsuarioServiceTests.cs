using AutoMapper;
using Moq;
using TechChallenge5.Domain.DTO.Login;
using TechChallenge5.Domain.DTO.Usuario;
using TechChallenge5.Domain.Entities;
using TechChallenge5.Domain.Exceptions;
using TechChallenge5.Domain.Interfaces.Repositories;
using TechChallenge5.Domain.Services;
using TechChallenge5.Tests.Fixtures.Domain.Services;

namespace TechChallenge5.Tests.Core.Domain.Services
{
    public class UsuarioServiceTests
    {
        private UsuarioService _usuarioService;
        private Mock<IUsuarioRepository> _usuarioRepositoryMock;
        private Mock<IMapper> _mockMapper;

        [SetUp]
        public void Setup()
        {
            _mockMapper = new Mock<IMapper>();
            _usuarioRepositoryMock = new Mock<IUsuarioRepository>();

            _usuarioService = new UsuarioService(_usuarioRepositoryMock.Object, _mockMapper.Object);
        }

        [Test]
        public async Task Add_WhenCalled_ShouldReturnUsuario()
        {
            var usuarioEntity = UsuarioServiceFixture.CreateUsuarioEntity();
            var senhaInformada = usuarioEntity.Senha;
            var usuarioDto = UsuarioServiceFixture.CreateUsuarioDto();
            _mockMapper.Setup(x => x.Map<UsuarioEntity>(It.IsAny<CadastrarUsuarioDTO>())).Returns(usuarioEntity);
            _usuarioRepositoryMock.Setup(x =>
                x.Add(It.IsAny<UsuarioEntity>())).ReturnsAsync(usuarioEntity);

            var result = await _usuarioService.Add(usuarioDto);

            Assert.Multiple(() =>
            {
                _usuarioRepositoryMock.Verify(x => x.Add(It.IsAny<UsuarioEntity>()), Times.Once);
                Assert.That(usuarioEntity.Email, Is.EqualTo(result.Email));
                Assert.That(senhaInformada, Is.Not.EqualTo(result.Senha));
            });
        }

        [Test]
        public async Task Delete_WhenUsuarioNotFound_ShouldThrowException()
        {
            var usuario = UsuarioServiceFixture.CreateUsuarioEntity();

            _usuarioRepositoryMock.Setup(x => x.GetById(It.IsAny<int>())).ReturnsAsync((UsuarioEntity)null);

            Assert.ThrowsAsync<NotFoundException>(() => _usuarioService.Delete(1));
        }

        [Test]
        public async Task Delete_WhenUsuarioFound_ShouldDeleteOnce()
        {
            var usuario = UsuarioServiceFixture.CreateUsuarioEntity();
            usuario.Id = 1;

            _usuarioRepositoryMock.Setup(x => x.GetById(It.IsAny<int>())).ReturnsAsync(usuario);

            await _usuarioService.Delete(usuario.Id);

            _usuarioRepositoryMock.Verify(x => x.Delete(It.IsAny<UsuarioEntity>()), Times.Once);
        }

        [Test]
        public void GerarHashSenha_WhenCalled_ShouldReturnHash()
        {
            var usuario = UsuarioServiceFixture.CreateUsuarioEntity();

            var hash = _usuarioService.GerarHashSenha(usuario.Senha);

            Assert.That(usuario.Senha, Is.Not.EqualTo(hash));
        }

        [Test]
        public async Task GetAll_WhenCalled_ShouldReturnAllUsuarios()
        {
            var usuario1 = UsuarioServiceFixture.CreateUsuarioEntity();
            var usuario2 = UsuarioServiceFixture.CreateUsuarioEntity();
            var listaUsuarios = new List<UsuarioEntity>
            {
                usuario1,
                usuario2
            };

            _usuarioRepositoryMock.Setup(x =>
                x.GetAll()).ReturnsAsync(listaUsuarios);

            var lista = await _usuarioService.GetAll();

            Assert.Multiple(() =>
            {
                _usuarioRepositoryMock.Verify(x => x.GetAll(), Times.Once);
                Assert.IsNotNull(lista);
                Assert.That(lista.Count, Is.EqualTo(2));
            });
        }

        [Test]
        public async Task GetByEmailSenha_WhenEmailSenhaExists_ShouldReturnUsuario()
        {
            var usuario = UsuarioServiceFixture.CreateUsuarioEntity();
            var autenticarDto = new AutenticarDto()
            {
                email = usuario.Email,
                senha = usuario.Senha
            };
            usuario.Senha = _usuarioService.GerarHashSenha(usuario.Senha);

            _usuarioRepositoryMock.Setup(x => x.GetByEmailSenha(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(usuario);

            var usuarioAutenticado = await _usuarioService.GetByEmailSenha(autenticarDto);

            Assert.Multiple(() =>
            {
                Assert.That(usuarioAutenticado, Is.Not.Null);
                Assert.That(usuarioAutenticado.Email, Is.EqualTo(autenticarDto.email));
                Assert.That(usuarioAutenticado.Senha, Is.EqualTo(autenticarDto.senha));
            });
        }

        [Test]
        public async Task GetByEmailSenha_WhenEmailNotExists_ShouldReturnNull()
        {
            var usuario = UsuarioServiceFixture.CreateUsuarioEntity();
            usuario.Email = "user@user.com";
            var autenticarDto = new AutenticarDto()
            {
                email = "email@email.com",
                senha = usuario.Senha
            };
            usuario.Senha = _usuarioService.GerarHashSenha(usuario.Senha);

            _usuarioRepositoryMock.Setup(x => x.GetByEmailSenha(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync((UsuarioEntity)null);

            var usuarioAutenticado = await _usuarioService.GetByEmailSenha(autenticarDto);

            Assert.Multiple(() =>
            {
                Assert.That(usuarioAutenticado, Is.Null);
                Assert.That(usuario.Email, Is.Not.EqualTo(autenticarDto.email));
                Assert.That(usuario.Senha, Is.EqualTo(autenticarDto.senha));
            });
        }

        [Test]
        public async Task GetByEmailSenha_WhenSenhaIncorrect_ShouldReturnNull()
        {
            var usuario = UsuarioServiceFixture.CreateUsuarioEntity();
            usuario.Senha = _usuarioService.GerarHashSenha("1234");
            var autenticarDto = new AutenticarDto()
            {
                email = usuario.Email,
                senha = "4321"
            };

            _usuarioRepositoryMock.Setup(x => x.GetByEmailSenha(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync((UsuarioEntity)null);

            var usuarioAutenticado = await _usuarioService.GetByEmailSenha(autenticarDto);

            Assert.Multiple(() =>
            {
                Assert.That(usuarioAutenticado, Is.Null);
                Assert.That(usuario.Email, Is.EqualTo(autenticarDto.email));
                Assert.That(usuario.Senha, Is.Not.EqualTo(autenticarDto.senha));
            });
        }

        [Test]
        public async Task GetById_WhenValidId_ShouldReturnUsuario()
        {
            var usuario = UsuarioServiceFixture.CreateUsuarioEntity();
            usuario.Id = 1;
            var id = 1;

            _usuarioRepositoryMock.Setup(x => x.GetById(It.IsAny<int>())).ReturnsAsync(usuario);

            var usuarioRetorno = await _usuarioService.GetById(id);

            Assert.Multiple(() =>
            {
                Assert.That(usuarioRetorno, Is.Not.Null);
                Assert.That(usuarioRetorno.Id, Is.EqualTo(id));
            });
        }

        [Test]
        public async Task GetById_WhenInvalidId_ShouldReturnNull()
        {
            var usuario = UsuarioServiceFixture.CreateUsuarioEntity();
            usuario.Id = 1;
            var id = 2;

            _usuarioRepositoryMock.Setup(x => x.GetById(It.IsAny<int>())).ReturnsAsync((UsuarioEntity)null);

            var usuarioRetorno = await _usuarioService.GetById(id);

            Assert.Multiple(() =>
            {
                Assert.That(usuarioRetorno, Is.Null);
                Assert.That(usuario.Id, Is.Not.EqualTo(id));
            });
        }

        [Test]
        public async Task Update_WhenUsuarioNull_ShouldThrowException()
        {
            var usuario = UsuarioServiceFixture.CreateUsuarioEntity();
            usuario.Id = 1;
            var usuarioDTo = UsuarioServiceFixture.CreateUsuarioDto();
            var id = 2;

            _usuarioRepositoryMock.Setup(x => x.GetById(It.IsAny<int>())).ReturnsAsync((UsuarioEntity)null);

            Assert.Multiple(() =>
            {
                Assert.ThrowsAsync<NotFoundException>(async () => await _usuarioService.Update(id, usuarioDTo));
                Assert.That(usuario.Id, Is.Not.EqualTo(id));
            });
        }

        [Test]
        public async Task Update_WhenUsuarioExists_ShouldReturnUsuarioAtualizado()
        {
            var usuario = UsuarioServiceFixture.CreateUsuarioEntity();
            usuario.Id = 1;
            var usuarioDTo = UsuarioServiceFixture.CreateUsuarioDto();
            var id = 1;

            _usuarioRepositoryMock.Setup(x => x.GetById(It.IsAny<int>())).ReturnsAsync(usuario);

            _mockMapper.Setup(x => x.Map<CadastrarUsuarioDTO, UsuarioEntity>(It.IsAny<CadastrarUsuarioDTO>(), It.IsAny<UsuarioEntity>()))
                .Callback<CadastrarUsuarioDTO, UsuarioEntity>((dto, user) =>
                {
                    usuario.Nome = dto.Nome;
                    usuario.Email = dto.Email;
                    usuario.Senha = dto.Senha;
                })
                .Returns(() => usuario);

            _usuarioRepositoryMock.Setup(x => x.Update(It.IsAny<UsuarioEntity>())).ReturnsAsync(usuario);

            var usuarioAtualizado = await _usuarioService.Update(id, usuarioDTo);
            usuarioDTo.Senha = _usuarioService.GerarHashSenha(usuarioDTo.Senha);

            Assert.Multiple(() =>
            {
                _usuarioRepositoryMock.Verify(x => x.Update(It.IsAny<UsuarioEntity>()), Times.Once);
                Assert.That(usuarioAtualizado.Id, Is.EqualTo(id));
                Assert.That(usuarioAtualizado.Email, Is.EqualTo(usuarioDTo.Email));
                Assert.That(usuarioAtualizado.Nome, Is.EqualTo(usuarioDTo.Nome));
                Assert.That(usuarioAtualizado.Senha, Is.EqualTo(usuarioDTo.Senha));
            });
        }
    }
}
