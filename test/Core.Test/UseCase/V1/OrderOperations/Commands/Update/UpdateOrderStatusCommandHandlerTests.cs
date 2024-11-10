using Core.Domain.Entities;
using Core.UseCase.V1.OrderOperations.Commands.Update;
using System.Net;
using Core.Common.Interfaces;
using System.Linq.Expressions;

namespace Core.Test.UseCase.V1.OrderOperations.Commands.Update
{
    public class UpdateOrderStatusCommandHandlerTests
    {
        private readonly Mock<IRepositoryEF> _mockRepository;
        private readonly UpdateOrderStatusCommandHandler _handler;

        public UpdateOrderStatusCommandHandlerTests()
        {
            _mockRepository = new Mock<IRepositoryEF>();
            _handler = new UpdateOrderStatusCommandHandler(_mockRepository.Object);
        }

        [Fact]
        public async Task Handle_Should_ReturnNotFoundResponse_When_OrderDoesNotExist()
        {
            // Arrange
            var command = new UpdateOrderStatusCommand { Id = 1, State = 2 };
            _mockRepository.Setup(repo => repo.FindAsync(It.IsAny<Expression<Func<Order, bool>>>()))
                           .ReturnsAsync((Order?)null);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
            Assert.Contains("not found", result.Notifications.First().Message.ToLower());
        }

        [Fact]
        public async Task Handle_Should_UpdateOrderStatus_When_OrderExists()
        {
            // Arrange
            var command = new UpdateOrderStatusCommand { Id = 1, State = 2 };
            var order = new Order { Id = 1, Status = 1 };
            _mockRepository.Setup(repo => repo.FindAsync(It.IsAny<Expression<Func<Order, bool>>>()))
                           .ReturnsAsync(order);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Equal(command.State, order.Status);
            _mockRepository.Verify(repo => repo.Update(order), Times.Once);
            _mockRepository.Verify(repo => repo.SaveChangesAsync(), Times.Once);
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        }
    }
}

