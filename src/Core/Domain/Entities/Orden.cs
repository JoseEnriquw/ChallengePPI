
namespace Core.Domain.Entities
{
    public class Order
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
        public virtual Asset Asset { get; set; } = null!;
    }
}


