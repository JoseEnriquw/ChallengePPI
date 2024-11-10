
namespace Core.Domain.Entities
{
    public class Asset
    {
        public Asset() 
        {
            Orders = [];
        }
        public int Id { get; set; }
        public string Ticker { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public int AssetTypeId { get; set; }
        public decimal UnitPrice { get; set; }

        public virtual AssetType AssetType { get; set; } = null!;
        public virtual ICollection<Order> Orders { get; set; }
    }
}
