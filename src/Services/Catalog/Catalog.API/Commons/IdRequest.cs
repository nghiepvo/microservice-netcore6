namespace Catalog.API.Commons;

public class IdRequest<T> where T:class
{
    public T Id { get; set; } = default!;
}