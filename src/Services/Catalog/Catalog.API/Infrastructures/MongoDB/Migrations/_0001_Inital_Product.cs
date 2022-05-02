using MongoDB.Entities;
using Catalog.API.Infrastructures.MongoDB.MasterData;
using System.Diagnostics.CodeAnalysis;

namespace Catalog.API.Infrastructures.MongoDB.Migrations;
[ExcludeFromCodeCoverage]
public class _0001_Inital_Product : IMigration
{
    public async Task UpgradeAsync()
    {
        await ProductData.Products.SaveAsync();
    }
}