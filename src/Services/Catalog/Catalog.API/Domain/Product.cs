using MongoDB.Entities;

namespace Catalog.API.Domain;

[Collection("Products")]
public class Product : Entity
{
    public string Name { get; set; } = default!;
    public string Category { get; set; } = default!;
    public string Summary { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string ImageFile { get; set; } = default!;
    public decimal Price { get; set; }
}