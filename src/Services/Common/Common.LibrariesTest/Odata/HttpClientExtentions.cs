using System.Net.Http.Headers;
using System.Text.Json;
using Newtonsoft.Json.Linq;

namespace Common.LibrariesTest.Odata;
public static class HttpClientExtentions
{
    /// <summary>
    /// make a SEND ODATA request using a request dto and get back a response dto.
    /// </summary>
    /// <typeparam name="TRequest">type of the requet dto</typeparam>
    /// <typeparam name="TResponse">type of the response dto</typeparam>
    /// <param name="requestUri">the route url to post to</param>
    /// <exception cref="InvalidOperationException">thrown when the response body cannot be deserialized in to specified response dto type</exception>
    public static async Task<(HttpResponseMessage? response, TResponse? result, int odataCount, string odataNextLink)> SENDODataAsync<TResponse>
        (this HttpClient client, string requestUri)
    {
        var req = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri(
                client.BaseAddress!.ToString().TrimEnd('/') +
                (requestUri.StartsWith('/') ? requestUri : "/" + requestUri))
        };

        req.Headers.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("application/json;odata.metadata=none"));

        var res = await client.SendAsync(req);

        TResponse? body;
        var count = 0;
        var nextLink = "";

        try
        {
            var jTokenParse = JToken.Parse(await res.Content.ReadAsStringAsync());

            body = jTokenParse["value"].ToObject<TResponse>();

            count = jTokenParse["@odata.count"].ToObject<int>();

            nextLink = jTokenParse["@odata.nextLink"].ToObject<string>();
        }
        catch (JsonException)
        {
            var reason = $"[{res.StatusCode}] {await res.Content.ReadAsStringAsync()}";
            throw new InvalidOperationException(
                $"Unable to deserialize the response body as [{typeof(TResponse).FullName}]. Reason: {reason}");
        }

        return (res, body, count, nextLink);
    }
}