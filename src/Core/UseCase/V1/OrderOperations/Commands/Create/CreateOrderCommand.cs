using Core.Common.Interfaces;
using Core.Domain.Classes;
using Core.Domain.Common;
using Core.Domain.Dto;
using Core.Domain.Entities;
using Core.Domain.Enums;
using MediatR;
using System.Net;

namespace Core.UseCase.V1.OrderOperations.Commands.Create
{
    public class CreateOrderCommand : IRequest<Response<OrderDto>>
    {
        public int AccountId { get; set; }
        public int AssetId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public char Operation { get; set; }
    }

    public class CreateOrderCommandHandler(IRepositoryEF repository) : IRequestHandler<CreateOrderCommand, Response<OrderDto>>
    {
        public async Task<Response<OrderDto>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var asset = await repository.FindAsync<Asset>(x=> x.Id==request.AssetId);
            var response= new Response<OrderDto>();
            if (asset == null)
            {
                response.AddNotification("#123", nameof(request.AssetId), string.Format(ErrorMessage.NOT_FOUND_GET_BY_ID, request.AssetId, nameof(Asset)));
                response.StatusCode = HttpStatusCode.BadRequest;
                return response;
            }

            Order order = new()
            {
                AccountId = request.AccountId,
                AssetId = request.AssetId,
                Quantity = request.Quantity,
                Price = asset.AssetTypeId == 1 ? 0 : request.Price,
                Operation = request.Operation,
                Status = (int)OrderStatusEnum.InProcess
            };

            decimal amount=0;
            switch (asset.AssetTypeId)
            {
                case 1:
                    amount = order.Quantity * asset.UnitPrice;
                    var commission = 0.006m * amount;
                    var tax = 0.21m * commission;
                    order.TotalAmount = amount + commission + tax;
                    break;
                case 2:
                    amount = order.Quantity * order.Price;
                    commission = 0.002m * amount;
                    tax = 0.21m * commission;
                    order.TotalAmount = amount + commission + tax;
                    break;
                case 3: 
                    order.TotalAmount = order.Quantity * order.Price;
                    break;
            }

            repository.Insert(order);
            await repository.SaveChangesAsync();

            response.StatusCode = HttpStatusCode.OK;
            response.Content = order;
            return response;
        }
    }

}
