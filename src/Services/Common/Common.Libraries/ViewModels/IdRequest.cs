namespace Common.Libraries.ViewModels;

public class IdRequest<T> where T : notnull
{
    public T Id { get; set; } = default!;
}