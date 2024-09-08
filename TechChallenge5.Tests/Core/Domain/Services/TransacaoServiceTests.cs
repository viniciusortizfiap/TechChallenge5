using AutoMapper;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechChallenge5.Data.Repositories;
using TechChallenge5.Domain.DTO.Transacao;
using TechChallenge5.Domain.Entities;
using TechChallenge5.Domain.Exceptions;
using TechChallenge5.Domain.Interfaces.Repositories;
using TechChallenge5.Domain.Interfaces.Services;
using TechChallenge5.Domain.Services;
using TechChallenge5.Tests.Fixtures.Domain.Services;

namespace TechChallenge5.Tests.Core.Domain.Services
{
    public class TransacaoServiceTests
    {
        private readonly ITransacaoService _portifolioService;
        private readonly Mock<ITransacaoRepository> _mockTransacaoRepository;
        private readonly Mock<IAtivoRepository> _mockAtivoRepository;
        private readonly Mock<IPortifolioRepository> _mockPortifolioRepository;
        private readonly Mock<IMapper> _mockMapper;

        public TransacaoServiceTests()
        {
            _mockMapper = new Mock<IMapper>();
            _mockTransacaoRepository = new Mock<ITransacaoRepository>();
            _mockAtivoRepository = new Mock<IAtivoRepository>();
            _mockPortifolioRepository = new Mock<IPortifolioRepository>();

            _portifolioService = new TransacaoService(
                _mockTransacaoRepository.Object,
                _mockAtivoRepository.Object,
                _mockPortifolioRepository.Object,
                _mockMapper.Object
            );
        }

        [Test]
        public async Task Atualizar_WhenTransacaoDoesNotExist_ShouldReturnNull()
        {
            // Arrange
            var input = TransacaoServiceFixture.BuscarTransacaoInputDto();

            _mockTransacaoRepository.Setup(p => p.GetByIdWithAtivoAndPortifolio(It.IsAny<int>()))
                .ReturnsAsync((TransacaoEntity)null);

            // Act
            var result = await _portifolioService.Atualizar(1, input);

            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public async Task Atualizar_WhenTransacaoExists_ShouldReturnUpdatedEntity()
        {
            // Arrange
            var input = TransacaoServiceFixture.BuscarTransacaoInputDto();

            var existingEntity = new TransacaoEntity(1, 1, "Ativo A", 100, 10);
            _mockTransacaoRepository.Setup(p => p.GetByIdWithAtivoAndPortifolio(It.IsAny<int>()))
                .ReturnsAsync(existingEntity);

            var updatedEntity = new TransacaoEntity(1, 1, "Ativo A", 200, 20);

            _mockMapper.Setup(m => m.Map<TransacaoEntity>(input))
                .Returns(updatedEntity);

            _mockTransacaoRepository.Setup(p => p.Update(It.IsAny<TransacaoEntity>()))
                .ReturnsAsync(updatedEntity);

            // Act
            var result = await _portifolioService.Atualizar(1, input);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.IsNotNull(result);
                Assert.That(result.Quantidade, Is.EqualTo(updatedEntity.Quantidade));
            });
        }

        [Test]
        public async Task BuscarPorId_WhenTransacaoExists_ShouldReturnEntity()
        {
            // Arrange
            var entity = new TransacaoEntity(1, 1, "Ativo A", 100, 10);
            _mockTransacaoRepository.Setup(p => p.GetByIdWithAtivoAndPortifolio(It.IsAny<int>()))
                .ReturnsAsync(entity);

            // Act
            var result = await _portifolioService.BuscarPorId(1);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.IsNotNull(result);
                Assert.That(result.Id, Is.EqualTo(entity.Id));
            });
        }

        [Test]
        public async Task BuscarPorId_WhenTransacaoDoesNotExist_ShouldReturnNull()
        {
            // Arrange
            _mockTransacaoRepository.Setup(p => p.GetByIdWithAtivoAndPortifolio(It.IsAny<int>()))
                .ReturnsAsync((TransacaoEntity)null);

            // Act
            var result = await _portifolioService.BuscarPorId(1);

            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public async Task Deletar_WhenTransacaoExists_ShouldReturnDeletedEntity()
        {
            // Arrange
            var entity = new TransacaoEntity(1, 1, "Ativo A", 100, 10);
            _mockTransacaoRepository.Setup(p => p.GetById(It.IsAny<int>()))
                .ReturnsAsync(entity);

            _mockTransacaoRepository.Setup(p => p.Delete(It.IsAny<TransacaoEntity>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _portifolioService.Deletar(1);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.IsNotNull(result);
                Assert.That(result.Id, Is.EqualTo(entity.Id));
            });
        }

        [Test]
        public async Task Deletar_WhenTransacaoDoesNotExist_ShouldReturnNull()
        {
            // Arrange
            _mockTransacaoRepository.Setup(p => p.GetById(It.IsAny<int>()))
                .ReturnsAsync((TransacaoEntity)null);

            // Act
            var result = await _portifolioService.Deletar(1);

            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public async Task ListarTodos_WhenEntitiesExist_ShouldReturnAllEntities()
        {
            // Arrange
            var entities = new List<TransacaoEntity>
    {
        new TransacaoEntity(1, 1, "Ativo A", 100, 10),
        new TransacaoEntity(2, 2, "Ativo B", 200, 20)
    };

            _mockTransacaoRepository.Setup(p => p.GetAllWithAtivoAndPortifolio())
                .ReturnsAsync(entities);

            // Act
            var result = await _portifolioService.ListarTodos();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result.Count(), Is.EqualTo(2));
                Assert.That(result.First().Id, Is.EqualTo(entities.First().Id));
            });
        }

        [Test]
        public async Task ListarTodos_WhenNoEntitiesExist_ShouldReturnEmptyList()
        {
            // Arrange
            _mockTransacaoRepository.Setup(p => p.GetAllWithAtivoAndPortifolio())
                .ReturnsAsync(new List<TransacaoEntity>());

            // Act
            var result = await _portifolioService.ListarTodos();

            // Assert
            Assert.That(result, Is.Empty);
        }

        [Test]
        public void Criar_WhenAtivoDoesNotExist_ShouldThrowNotFoundException()
        {
            var dut = TransacaoServiceFixture.BuscarTransacaoInputDto();

            _mockAtivoRepository.Setup(p => p.GetById(It.IsAny<int>())).ReturnsAsync((AtivoEntity)null);

            var error = Assert.ThrowsAsync<NotFoundException>(() => _portifolioService.Criar(dut));

            Assert.Multiple(() =>
            {
                Assert.That(error.Message, Is.EqualTo($"Ativo não localizado com id: {dut.AtivoId}"));
            });
        }

        [Test]
        public void Criar_WhenPortifolioDoesNotExist_ShouldThrowNotFoundException()
        {
            var dut = TransacaoServiceFixture.BuscarTransacaoInputDto();

            _mockAtivoRepository.Setup(p => p.GetById(It.IsAny<int>())).ReturnsAsync(
                new AtivoEntity("A", "B", "C")
            );

            _mockPortifolioRepository.Setup(p => p.GetById(It.IsAny<int>())).ReturnsAsync((PortifolioEntity)null);

            var error = Assert.ThrowsAsync<NotFoundException>(() => _portifolioService.Criar(dut));

            Assert.Multiple(() =>
            {
                Assert.That(error.Message, Is.EqualTo($"Portifolio não localizado com id: {dut.PortifolioId}"));
            });
        }

        [Test]
        public async Task Criar_WhenCorrectDAta_ShouldReturnEntity()
        {
            var dut = TransacaoServiceFixture.BuscarTransacaoInputDto();

            _mockAtivoRepository.Setup(p => p.GetById(It.IsAny<int>())).ReturnsAsync(
                new AtivoEntity("A", "B", "C")
            );

            _mockPortifolioRepository.Setup(p => p.GetById(It.IsAny<int>())).ReturnsAsync(
                new PortifolioEntity(1, "A", "B")
            );

            var entity = new TransacaoEntity(1, 2, "A", 1, 1);

            // Mock the mapper to return the expected entity when called with the input DTO
            _mockMapper.Setup(m => m.Map<TransacaoEntity>(It.IsAny<TransacaoInputDto>()))
                .Returns(entity); // Return the expected mapped entity

            _mockTransacaoRepository.Setup(p => p.Add(It.IsAny<TransacaoEntity>()))
                .ReturnsAsync(entity);

            var response = await _portifolioService.Criar(dut);
            Assert.Multiple(() =>
            {
                Assert.That(response.Quantidade, Is.EqualTo(dut.Quantidade));
            });
        }


    }
}
