using Core.Common.Interfaces;
using Core.Domain.Classes;
using Core.Domain.Common;
using Core.Domain.Entities;
using MediatR;
using System.Net;

namespace Core.UseCase.V1.OrderOperations.Commands.Delete
{
    public class DeleteOrderCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }
    }

    public class DeleteOrderCommandHandler(IRepositoryEF repository) : IRequestHandler<DeleteOrderCommand, Response<string>>
    {
        public async Task<Response<string>> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            var response = new Response<string>();
            var order = await repository.FindAsync<Order>(o => o.Id == request.Id);

            if (order == null)
            {
                response.AddNotification("#404", nameof(request.Id), string.Format(ErrorMessage.NOT_FOUND_GET_BY_ID, request.Id, nameof(Order)));
                response.StatusCode = HttpStatusCode.NotFound;
                return response;
            }

            repository.Delete(order);
            await repository.SaveChangesAsync();

            response.Content = $"Order with Id {request.Id} deleted successfully.";
            response.StatusCode = HttpStatusCode.OK;
            return response;
        }
    }

}
