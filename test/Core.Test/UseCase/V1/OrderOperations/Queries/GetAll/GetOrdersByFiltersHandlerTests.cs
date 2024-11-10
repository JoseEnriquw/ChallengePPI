using Core.Common.Interfaces;
using Core.Common.Models;
using Core.Domain.Dto;
using Core.UseCase.V1.OrderOperations.Queries.GetAll;

namespace Core.Test.UseCase.V1.OrderOperations.Queries.GetAll
{
    public class GetOrdersByFiltersHandlerTests
    {
        private readonly Mock<IRepositoryEF> _mockRepository;
        private readonly GetOrdersByFiltersHandler _handler;

        public GetOrdersByFiltersHandlerTests()
        {
            _mockRepository = new Mock<IRepositoryEF>();
            _handler = new GetOrdersByFiltersHandler(_mockRepository.Object);
        }

        [Fact]
        public async Task Handle_Should_ReturnOrders_When_ValidRequest()
        {
            // Arrange
            var command = new GetOrdersByFilters { Page = 1, Size = 10 };
            var orders = new PaginatedList<OrderDto>([new OrderDto { Id = 1, Quantity = 10 }],1,1,1);
            _mockRepository.Setup(repo => repo.GetOrdersByFiltersAsync(It.IsAny<GetOrdersByFilters>()))
                           .ReturnsAsync(orders);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Equal(System.Net.HttpStatusCode.OK, result.StatusCode);
            Assert.Equal(orders, result.Content);
        }
    }
}
