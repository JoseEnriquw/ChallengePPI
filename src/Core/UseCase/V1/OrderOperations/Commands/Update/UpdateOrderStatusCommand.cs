using Core.Common.Interfaces;
using Core.Domain.Classes;
using Core.Domain.Common;
using Core.Domain.Dto;
using Core.Domain.Entities;
using MediatR;
using System.Net;

namespace Core.UseCase.V1.OrderOperations.Commands.Update
{
    public class UpdateOrderStatusCommand : IRequest<Response<OrderDto>>
    {
        public int Id { get; set; }
        public int State { get; set; }
    }

    public class UpdateOrderStatusCommandHandler(IRepositoryEF repository) : IRequestHandler<UpdateOrderStatusCommand, Response<OrderDto>>
    {
        public async Task<Response<OrderDto>> Handle(UpdateOrderStatusCommand request, CancellationToken cancellationToken)
        {
            var order = await repository.FindAsync<Order>(o => o.Id == request.Id);
            var response = new Response<OrderDto>();
            if (order == null)
            {
                response.AddNotification("#404", nameof(request.Id), string.Format(ErrorMessage.NOT_FOUND_GET_BY_ID, request.Id, nameof(Order)));
                response.StatusCode = HttpStatusCode.NotFound;
                return response;
            }

            order.Status = request.State;
            repository.Update(order);
            await repository.SaveChangesAsync();

            response.Content = order;
            response.StatusCode = HttpStatusCode.OK;
            return response;
        }
    }

}
