
using Core.Domain.Entities;

namespace Core.Domain.Dto
{
    public class AssetTypeDto
    { 
        public int Id { get; set; }
        public string Description { get; set; } = string.Empty;

        public static implicit operator AssetTypeDto(AssetType assetType)
        {
            return new()
            {
                Description = assetType.Description,
                Id = assetType.Id
            };
        }
    }
}
