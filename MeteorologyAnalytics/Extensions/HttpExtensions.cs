using System.Text.Json;
using Microsoft.AspNetCore.Http;

namespace MeteorologyAnalytics.Extensions;

public static class HttpExtensions
{
    public static void AddPaginationHeader<T>(
        this HttpResponse response,
        T header)
    {
        var jsonOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        var json = JsonSerializer.Serialize(header, jsonOptions);

        response.Headers["Pagination"] = json;

        if (!response.Headers.ContainsKey("Access-Control-Expose-Headers"))
        {
            response.Headers.Add("Access-Control-Expose-Headers", "Pagination");
        }
    }
}