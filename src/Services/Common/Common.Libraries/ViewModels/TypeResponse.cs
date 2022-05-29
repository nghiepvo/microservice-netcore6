namespace Common.Libraries.ViewModels;

public class TypeResponse<T> where T : notnull
{
    public T Body {get; set;} = default!;
}