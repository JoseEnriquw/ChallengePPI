using Core.Common.Interfaces;
using Core.Common.Models;
using Core.Domain.Classes;
using Core.Domain.Dto;
using MediatR;

namespace Core.UseCase.V1.OrderOperations.Queries.GetAll
{
    public class GetOrdersByFilters : IRequest<Response<PaginatedList<OrderDto>>>
    {
        public char? Operation { get; set; }
        public int? State { get; set; }
        public int Page { get; set; }
        public int Size { get; set; }
    }

    public class GetOrdersByFiltersHandler(IRepositoryEF repository) : IRequestHandler<GetOrdersByFilters, Response<PaginatedList<OrderDto>>>
    {
        public async Task<Response<PaginatedList<OrderDto>>> Handle(GetOrdersByFilters request, CancellationToken cancellationToken)
        {
            var orders = await repository.GetOrdersByFiltersAsync(request);

            return new()
            {
                Content = orders,
                StatusCode = System.Net.HttpStatusCode.OK
            };
        }
    }
}
