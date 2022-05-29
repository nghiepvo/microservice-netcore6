namespace Common.Libraries.ViewModels;

public class TypeRequest<T> where T : notnull
{
    public T Payload {get; set;} = default!;
}