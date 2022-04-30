using MongoDB.Entities;
using Catalog.API.Infrastructures.MongoDB.MasterData;

namespace Catalog.API.Infrastructures.MongoDB.Migrations;

public class _0001_Inital_Product : IMigration
{
    public async Task UpgradeAsync()
    {
        await ProductData.Products.SaveAsync();
    }
}