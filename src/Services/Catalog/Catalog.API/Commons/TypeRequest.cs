using System.Collections.ObjectModel;

namespace Catalog.API.Commons;

public class TypeRequest<T> where T : notnull
{
    public T Payload {get; set;} = default!;
}