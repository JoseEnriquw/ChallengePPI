using Core.Common.Interfaces;
using Core.Domain.Entities;
using Core.UseCase.V1.OrderOperations.Commands.Create;
using System.Linq.Expressions;
using System.Net;

namespace Core.Test.UseCase.V1.OrderOperations.Commands.Create
{
    public class CreateOrderCommandHandlerTests
    {
        private readonly Mock<IRepositoryEF> _mockRepository;
        private readonly CreateOrderCommandHandler _handler;

        public CreateOrderCommandHandlerTests()
        {
            _mockRepository = new Mock<IRepositoryEF>();
            _handler = new CreateOrderCommandHandler(_mockRepository.Object);
        }

        [Fact]
        public async Task Handle_Should_ReturnBadRequest_When_AssetDoesNotExist()
        {
            // Arrange
            var command = new CreateOrderCommand { AssetId = 1 };
            _mockRepository.Setup(repo => repo.FindAsync(It.IsAny<Expression<Func<Asset, bool>>>()))
                           .ReturnsAsync((Asset?)null);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
            Assert.Contains("not found", result.Notifications.First().Message.ToLower());
        }

        [Fact]
        public async Task Handle_Should_CreateOrder_When_AssetExists_WithAssetTypeId1()
        {
            // Arrange
            var command = new CreateOrderCommand
            {
                AccountId = 1,
                AssetId = 1,
                Quantity = 10,
                Price = 100,
                Operation = 'C'
            };

            var asset = new Asset
            {
                Id = 1,
                AssetTypeId = 1,
                UnitPrice = 50
            };

            _mockRepository.Setup(repo => repo.FindAsync(It.IsAny<Expression<Func<Asset, bool>>>()))
                           .ReturnsAsync(asset);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            _mockRepository.Verify(repo => repo.Insert(It.IsAny<Order>()), Times.Once);
            _mockRepository.Verify(repo => repo.SaveChangesAsync(), Times.Once);
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
            Assert.NotNull(result.Content);
        }

        [Fact]
        public async Task Handle_Should_CreateOrder_When_AssetExists_WithAssetTypeId2()
        {
            // Arrange
            var command = new CreateOrderCommand
            {
                AccountId = 1,
                AssetId = 2,
                Quantity = 5,
                Price = 200,
                Operation = 'V'
            };

            var asset = new Asset
            {
                Id = 2,
                AssetTypeId = 2
            };

            _mockRepository.Setup(repo => repo.FindAsync(It.IsAny<Expression<Func<Asset, bool>>>()))
                           .ReturnsAsync(asset);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            _mockRepository.Verify(repo => repo.Insert(It.IsAny<Order>()), Times.Once);
            _mockRepository.Verify(repo => repo.SaveChangesAsync(), Times.Once);
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
            Assert.NotNull(result.Content);
        }

        [Fact]
        public async Task Handle_Should_CreateOrder_When_AssetExists_WithAssetTypeId3()
        {
            // Arrange
            var command = new CreateOrderCommand
            {
                AccountId = 1,
                AssetId = 3,
                Quantity = 3,
                Price = 300,
                Operation = 'V'
            };

            var asset = new Asset
            {
                Id = 3,
                AssetTypeId = 3
            };

            _mockRepository.Setup(repo => repo.FindAsync(It.IsAny<Expression<Func<Asset, bool>>>()))
                           .ReturnsAsync(asset);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            _mockRepository.Verify(repo => repo.Insert(It.IsAny<Order>()), Times.Once);
            _mockRepository.Verify(repo => repo.SaveChangesAsync(), Times.Once);
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
            Assert.NotNull(result.Content);
        }
    }
}
