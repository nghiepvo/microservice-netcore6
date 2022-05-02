using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace FastEndpoints;
public static class HttpClientExtentions
{
    /// <summary>
    /// make a SEND request using a request dto and get back a response dto.
    /// </summary>
    /// <typeparam name="TRequest">type of the requet dto</typeparam>
    /// <typeparam name="TResponse">type of the response dto</typeparam>
    /// <param name="requestUri">the route url to post to</param>
    /// <param name="method">provide a HttpMethod</param>
    /// <param name="request">the request dto</param>
    /// <exception cref="InvalidOperationException">thrown when the response body cannot be deserialized in to specified response dto type</exception>
    public static async Task<(HttpResponseMessage? response, TResponse? result)> SENDAsync<TRequest, TResponse>
        (this HttpClient client, string requestUri, HttpMethod method, TRequest request)
    {
        var res = await client.SendAsync(
            new HttpRequestMessage
            {
                Method = method,
                RequestUri = new Uri(
                    client.BaseAddress!.ToString().TrimEnd('/') +
                    (requestUri.StartsWith('/') ? requestUri : "/" + requestUri)),
                Content = new StringContent(JsonSerializer.Serialize(request, new JsonSerializerOptions()), Encoding.UTF8, "application/json")
            });

        if (typeof(TResponse) == typeof(EmptyResponse))
            return (res, default(TResponse));

        TResponse? body;

        try
        {
            body = await res.Content.ReadFromJsonAsync<TResponse>(new JsonSerializerOptions { PropertyNameCaseInsensitive = true});
        }
        catch (JsonException)
        {
            var reason = $"[{res.StatusCode}] {await res.Content.ReadAsStringAsync()}";
            throw new InvalidOperationException(
                $"Unable to deserialize the response body as [{typeof(TResponse).FullName}]. Reason: {reason}");
        }

        return (res, body);
    }

    /// <summary>
    /// make a DELETE request to an endpoint using auto route discovery using a request dto and get back a response dto.
    /// </summary>
    /// <typeparam name="TEndpoint">the type of the endpoint</typeparam>
    /// <typeparam name="TRequest">the type of the request dto</typeparam>
    /// <typeparam name="TResponse">the type of the response dto</typeparam>
    /// <param name="request">the request dto</param>
    public static Task<(HttpResponseMessage? response, TResponse? result)> DELETEAsync<TEndpoint, TRequest, TResponse>(this HttpClient client, TRequest request) where TEndpoint : BaseEndpoint
        => SENDAsync<TRequest, TResponse>(client, IEndpoint.TestURLFor<TEndpoint>(), HttpMethod.Delete, request);

}