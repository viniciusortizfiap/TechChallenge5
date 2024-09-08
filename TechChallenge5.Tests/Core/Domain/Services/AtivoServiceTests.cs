using AutoMapper;
using Moq;
using TechChallenge5.Domain.DTO.Ativo;
using TechChallenge5.Domain.Entities;
using TechChallenge5.Domain.Interfaces.Repositories;
using TechChallenge5.Domain.Services;
using TechChallenge5.Tests.Fixtures.Domain.Services;

namespace TechChallenge5.Tests.Core.Domain.Services
{
    public class AtivoServiceTests
    {
        private AtivoService _AtivoService;
        private Mock<IAtivoRepository> _AtivoRepositoryMock;
        private Mock<IMapper> _mockMapper;

        [SetUp]
        public void Setup()
        {
            _AtivoRepositoryMock = new Mock<IAtivoRepository>();
            _mockMapper = new Mock<IMapper>();

            _AtivoService = new AtivoService(_AtivoRepositoryMock.Object, _mockMapper.Object);
        }

        [Test]
        public async Task Add_WhenCalled_ShouldReturnAtivo()
        {
            var ativo = AtivoServiceFixture.CreateAtivoEntity();
            var ativoDTO = AtivoServiceFixture.CreateAtivoDto();

            _mockMapper.Setup(x => x.Map<AtivoEntity>(ativoDTO)).Returns(ativo);

            _AtivoRepositoryMock.Setup(x => x.Add(ativo)).ReturnsAsync(ativo);

            var response = await _AtivoService.Add(ativoDTO);

            Assert.IsNotNull(response);
            Assert.AreEqual(ativo.Codigo, response.Codigo);
        }

        [Test]
        public async Task Delete_WhenCalled_ShouldReturnVoid()
        {
            var ativo = AtivoServiceFixture.CreateAtivoEntity();

            _AtivoRepositoryMock.Setup(x => x.GetById(ativo.Id)).ReturnsAsync(ativo);

            await _AtivoService.Delete(ativo.Id);

            _AtivoRepositoryMock.Verify(x => x.Delete(ativo), Times.Once);
        }

        [Test]
        public async Task GetAll_WhenCalled_ShouldReturnListAtivo()
        {
            var ativo = AtivoServiceFixture.CreateAtivoEntity();
            var ativoList = new List<AtivoEntity> { ativo };

            _AtivoRepositoryMock.Setup(x => x.GetAll()).ReturnsAsync(ativoList);

            var response = await _AtivoService.GetAll();

            Assert.IsNotNull(response);
            Assert.AreEqual(ativoList.Count, response.Count);
        }

        [Test]
        public async Task GetById_WhenAtivoExists_ShouldReturnAtivo()
        {
            var ativo = AtivoServiceFixture.CreateAtivoEntity();

            _AtivoRepositoryMock.Setup(x => x.GetById(ativo.Id)).ReturnsAsync(ativo);

            var response = await _AtivoService.GetById(ativo.Id);

            Assert.IsNotNull(response);
            Assert.AreEqual(ativo.Codigo, response.Codigo);
        }

        [Test]
        public async Task Update_WhenAtivoExists_ShouldReturnAtivo()
        {
            var ativo = AtivoServiceFixture.CreateAtivoEntity();
            ativo.Id = 1;
            var ativoDTO = AtivoServiceFixture.CreateAtivoDto();
            var id = 2;

            _AtivoRepositoryMock.Setup(x => x.GetById(id)).ReturnsAsync(ativo);

            _mockMapper.Setup(x => x.Map<CadastrarAtivoDTO, AtivoEntity>(It.IsAny<CadastrarAtivoDTO>(), It.IsAny<AtivoEntity>()))
                .Callback<CadastrarAtivoDTO, AtivoEntity>((dto, ativoEntity) =>
                {
                    ativo.Nome = dto.Nome;
                    ativo.Codigo = dto.Codigo;
                    ativo.TipoAtivo = dto.TipoAtivo;
                })
                .Returns(() => ativo);

            _AtivoRepositoryMock.Setup(x => x.Update(ativo)).ReturnsAsync(ativo);

            var ativoAtualizado = await _AtivoService.Update(id, ativoDTO);
            Assert.Multiple(() =>
            {
                _AtivoRepositoryMock.Verify(x => x.GetById(id), Times.Once);
                Assert.That(ativoAtualizado.Codigo, Is.EqualTo(ativoDTO.Codigo));
                Assert.That(ativoAtualizado.Nome, Is.EqualTo(ativoDTO.Nome));
                Assert.That(ativoAtualizado.TipoAtivo, Is.EqualTo(ativoDTO.TipoAtivo));
            });

        }
    }
}
