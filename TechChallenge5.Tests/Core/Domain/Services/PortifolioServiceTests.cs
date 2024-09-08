using AutoMapper;
using Moq;
using TechChallenge5.Domain.DTO.Portfolio;
using TechChallenge5.Domain.Entities;
using TechChallenge5.Domain.Interfaces.Repositories;
using TechChallenge5.Domain.Interfaces.Services;
using TechChallenge5.Domain.Services;

namespace TechChallenge5.Tests.Core.Domain.Services
{
    public class PortifolioServiceTests
    {
        private IPortifolioService _portifolioService;
        private Mock<IPortifolioRepository> _portifolioRepositoryMock;
        private Mock<ITransacaoRepository> _mockTransacaoRepository;
        private Mock<IMapper> _mockMapper;

        public PortifolioServiceTests()
        {
            _mockMapper = new Mock<IMapper>();
            _portifolioRepositoryMock = new Mock<IPortifolioRepository>();
            _mockTransacaoRepository = new Mock<ITransacaoRepository>();
            _portifolioService = new PortifolioService(_portifolioRepositoryMock.Object, _mockTransacaoRepository.Object, _mockMapper.Object);
        }

        [Test]
        public async Task Should_Return_Portifolio()
        {
            // Arrange
            var portifolio = new PortifolioEntity(1, "Portifolio 1", "Portifolio 1 Description");

            _portifolioRepositoryMock.Setup(x => x.GetById(It.IsAny<int>())).ReturnsAsync(portifolio);

            // Act
            var result = await _portifolioService.GetById(1, 1);

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(portifolio.Nome, result.Nome);
            Assert.AreEqual(portifolio.Descricao, result.Descricao);
        }

        [Test]
        public async Task Should_Create_Portifolio()
        {
            // Arrange
            var portifolio = new PortifolioEntity(1, "Portifolio 1", "Portifolio 1 Description");
            var portifolioDTO = new CadastrarPortifolioDTO
            {
                Nome = "Portifolio 1",
                Descricao = "Portifolio 1 Description"
            };
            int usuarioId = 1;

            _mockMapper.Setup(x => x.Map<PortifolioEntity>(It.IsAny<CadastrarPortifolioDTO>())).Returns(portifolio);
            _portifolioRepositoryMock.Setup(x => x.Add(It.IsAny<PortifolioEntity>())).ReturnsAsync(portifolio);

            // Act
            var result = await _portifolioService.Add(usuarioId, portifolioDTO);

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(portifolio.Id, result.Id);
            Assert.AreEqual(portifolio.Nome, result.Nome);
            Assert.AreEqual(portifolio.Descricao, result.Descricao);
        }

        [Test]
        public async Task Should_Update_Portifolio()
        {
            // Arrange
            var portifolio = new PortifolioEntity(1, "Portifolio 1", "Portifolio 1 Description");
            var portifolioDTO = new AtualizarPortifolioDTO
            {
                Nome = "Portifolio 1",
                Descricao = "Portifolio 1 Description"
            };

            _mockMapper.Setup(x => x.Map<PortifolioEntity>(It.IsAny<AtualizarPortifolioDTO>())).Returns(portifolio);
            _portifolioRepositoryMock.Setup(x => x.Update(It.IsAny<PortifolioEntity>())).ReturnsAsync(portifolio);

            // Act
            var result = await _portifolioService.Update(1, portifolioDTO);

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(portifolio.Id, result.Id);
            Assert.AreEqual(portifolio.Nome, result.Nome);
            Assert.AreEqual(portifolio.Descricao, result.Descricao);
        }
        [Test]
        public async Task Should_Delete_Portifolio()
        {
            // Arrange
            var portifolio = new PortifolioEntity(1, "Portifolio 1", "Portifolio 1 Description");

            _portifolioRepositoryMock.Setup(x => x.GetById(It.IsAny<int>())).ReturnsAsync(portifolio);

            // Act
            await _portifolioService.Delete(1);

            // Assert
            _portifolioRepositoryMock.Verify(x => x.Delete(It.IsAny<PortifolioEntity>()), Times.Once);
        }
        [Test]
        public async Task Should_Return_All_Portifolios()
        {
            // Arrange
            var portifolio = new PortifolioEntity(1, "Portifolio 1", "Portifolio 1 Description");
            var portifolios = new List<PortifolioEntity> { portifolio };
            var usuarioId = 1;

            _portifolioRepositoryMock.Setup(x => x.GetAllWithUser(It.IsAny<int>())).ReturnsAsync(portifolios);

            // Act
            var result = await _portifolioService.GetAll(usuarioId);

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(portifolios.Count, result.Count);
            Assert.AreEqual(portifolios[0].Id, result[0].Id);
            Assert.AreEqual(portifolios[0].Nome, result[0].Nome);
            Assert.AreEqual(portifolios[0].Descricao, result[0].Descricao);

        }
    }
}
