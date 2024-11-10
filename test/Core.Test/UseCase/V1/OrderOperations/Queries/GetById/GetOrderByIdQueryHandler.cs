using AutoFixture;
using Core.Common.Interfaces;
using Core.Domain.Entities;
using Core.UseCase.V1.OrderOperations.Queries.GetById;
using System.Net;

namespace Core.Test.UseCase.V1.OrderOperations.Queries.GetById
{
    public class GetOrderByIdQueryHandlerTests
    {
        private readonly Mock<IRepositoryEF> _mockRepository;
        private readonly GetOrderByIdQueryHandler _handler;

        public GetOrderByIdQueryHandlerTests()
        {
            _mockRepository = new Mock<IRepositoryEF>();
            _handler = new GetOrderByIdQueryHandler(_mockRepository.Object);
        }

        [Fact]
        public async Task Handle_Should_ReturnNotFound_When_OrderDoesNotExist()
        {
            // Arrange
            var command = new GetOrderById { Id = 1 };
            _mockRepository.Setup(repo => repo.GetOrderByIdAsync(It.IsAny<int>()))
                           .ReturnsAsync((Order?)null);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
            Assert.Contains("not found", result.Notifications.First().Message.ToLower());
        }

        [Fact]
        public async Task Handle_Should_ReturnOrder_When_OrderExists()
        {
            // Arrange
            var command = new GetOrderById { Id = 1 };

            var assetType= new Fixture().Build<AssetType>().Without(x=> x.Assets).Create();

            var asset= new Fixture().Build<Asset>()
                .Without(x=> x.Orders)
                .With(x=> x.AssetType,assetType)
                .Create();

            var order = new Fixture().Build<Order>()
                .With(x=> x.Id, 1)
                .With(x=> x.Asset,asset)
                .Create();

            _mockRepository.Setup(repo => repo.GetOrderByIdAsync(It.IsAny<int>()))
                           .ReturnsAsync(order);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
            Assert.NotNull(result.Content);
            Assert.Equal(order.Id, result.Content.Id);
        }
    }
}
