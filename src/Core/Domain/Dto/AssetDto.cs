
using Core.Domain.Entities;

namespace Core.Domain.Dto
{
    public class AssetDto
    {
        public int Id { get; set; }
        public string Ticker { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public decimal UnitPrice { get; set; }

        public AssetTypeDto AssetType { get; set; } = null!;

        public static implicit operator AssetDto(Asset asset)
        {
            return new()
            {
                Id = asset.Id,
                Ticker = asset.Ticker,
                Name = asset.Name,
                UnitPrice = asset.UnitPrice,
                AssetType = asset.AssetType
            };
        }
    }
}
