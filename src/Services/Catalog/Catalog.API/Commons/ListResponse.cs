using System.Collections.ObjectModel;

namespace Catalog.API.Commons;

public class ListResponse<T> where T : class
{
    public IEnumerable<T> Data { get; set; } = new Collection<T>();
    public int Total { get; set; }
}