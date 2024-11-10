using Core.Domain.Entities;
using Core.UseCase.V1.OrderOperations.Commands.Delete;
using System.Net;
using Core.Common.Interfaces;
using System.Linq.Expressions;

namespace Core.Test.UseCase.V1.OrderOperations.Commands.Delete
{

    public class DeleteOrderCommandHandlerTests
    {
        private readonly Mock<IRepositoryEF> _mockRepository;
        private readonly DeleteOrderCommandHandler _handler;

        public DeleteOrderCommandHandlerTests()
        {
            _mockRepository = new Mock<IRepositoryEF>();
            _handler = new DeleteOrderCommandHandler(_mockRepository.Object);
        }

        [Fact]
        public async Task Handle_Should_ReturnNotFoundResponse_When_OrderDoesNotExist()
        {
            // Arrange
            var command = new DeleteOrderCommand { Id = 1 };
            _mockRepository.Setup(repo => repo.FindAsync(It.IsAny<Expression<Func<Order, bool>>>()))
                           .ReturnsAsync((Order?)null);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
            Assert.Contains("not found", result.Notifications.First().Message.ToLower());
        }

        [Fact]
        public async Task Handle_Should_DeleteOrder_When_OrderExists()
        {
            // Arrange
            var command = new DeleteOrderCommand { Id = 1 };
            var order = new Order { Id = 1 };
            _mockRepository.Setup(repo => repo.FindAsync(It.IsAny<Expression<Func<Order, bool>>>()))
                           .ReturnsAsync(order);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            _mockRepository.Verify(repo => repo.Delete(order), Times.Once);
            _mockRepository.Verify(repo => repo.SaveChangesAsync(), Times.Once);
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
            Assert.Contains("deleted successfully", result.Content);
        }
    }
}

