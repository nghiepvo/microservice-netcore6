namespace Catalog.API.Commons;

public class IdRequest<T> where T : notnull
{
    public T Id { get; set; } = default!;
}