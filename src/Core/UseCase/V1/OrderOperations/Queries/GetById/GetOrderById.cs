using Core.Common.Interfaces;
using Core.Domain.Classes;
using Core.Domain.Common;
using Core.Domain.Entities;
using MediatR;
using System.Net;

namespace Core.UseCase.V1.OrderOperations.Queries.GetById
{
    public class GetOrderById : IRequest<Response<GetOrderByIdResponse>>
    {
        public int Id { get; set; }
    }

    public class GetOrderByIdQueryHandler(IRepositoryEF repository) : IRequestHandler<GetOrderById, Response<GetOrderByIdResponse>>
    {
        public async Task<Response<GetOrderByIdResponse>> Handle(GetOrderById request, CancellationToken cancellationToken)
        {
            var order = await repository.GetOrderByIdAsync(request.Id);
            var response= new Response<GetOrderByIdResponse>();
            if (order == null)
            {
                response.AddNotification("#404",nameof(request.Id),string.Format(ErrorMessage.NOT_FOUND_GET_BY_ID, request.Id, nameof(Order)));
                response.StatusCode = HttpStatusCode.NotFound;
                return response;
            }

            response.Content= order;
            response.StatusCode = HttpStatusCode.OK;
            return response;
        }
    }
}
