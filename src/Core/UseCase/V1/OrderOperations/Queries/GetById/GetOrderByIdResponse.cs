using Core.Domain.Dto;
using Core.Domain.Entities;

namespace Core.UseCase.V1.OrderOperations.Queries.GetById
{
    public class GetOrderByIdResponse : OrderDto
    {
        public AssetDto Asset { get; set; } = null!;

        public static implicit operator GetOrderByIdResponse(Order order)
        {
            return new()
            {
                Id = order.Id,
                Operation = order.Operation,
                Price = order.Price,
                Quantity=order.Quantity,
                AssetId = order.AssetId,
                AccountId = order.AccountId,
                AssetName = order.AssetName,
                Status = order.Status,
                TotalAmount = order.TotalAmount,
                Asset=order.Asset,
            };
        }
    }
}
