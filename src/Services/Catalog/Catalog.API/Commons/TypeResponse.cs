using System.Collections.ObjectModel;

namespace Catalog.API.Commons;

public class TypeResponse<T> where T : notnull
{
    public T Body {get; set;} = default!;
}