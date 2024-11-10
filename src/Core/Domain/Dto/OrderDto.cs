using Core.Domain.Entities;

namespace Core.Domain.Dto
{
    public class OrderDto
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public string AssetName { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public char Operation { get; set; }
        public int Status { get; set; }
        public decimal TotalAmount { get; set; }
        public int AssetId { get; set; }

        public static implicit operator OrderDto(Order order)
        {
            return new()
            {
                Id = order.Id,
                AccountId = order.AccountId,
                AssetId = order.AssetId,
                Quantity = order.Quantity,
                Price = order.Price,
                Operation = order.Operation,
                AssetName = order.AssetName,
                Status = order.Status,
                TotalAmount = order.TotalAmount
            };
        }  
    }
}
