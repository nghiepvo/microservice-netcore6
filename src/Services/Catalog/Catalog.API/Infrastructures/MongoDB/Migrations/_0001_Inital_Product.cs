using MongoDB.Entities;
using Catalog.API.Infrastructures.MongoDB.MasterData;
using System.Diagnostics.CodeAnalysis;

namespace Catalog.API.Infrastructures.MongoDB.Migrations;
[ExcludeFromCodeCoverage]
#pragma warning disable S101 // Rename class '_0001_Inital_Product' to match pascal case naming rules, trim underscores from the name.
public class _0001_Inital_Product : IMigration
#pragma warning restore S101 // Rename class '_0001_Inital_Product' to match pascal case naming rules, trim underscores from the name.
{
    public async Task UpgradeAsync()
    {
        await ProductData.Products.SaveAsync();
    }
}