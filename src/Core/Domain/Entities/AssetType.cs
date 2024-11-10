
namespace Core.Domain.Entities
{
    public class AssetType
    {
        public AssetType()
        {
            Assets = [];
        } 

        public int Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public virtual ICollection<Asset> Assets { get; set; }
    }
}
