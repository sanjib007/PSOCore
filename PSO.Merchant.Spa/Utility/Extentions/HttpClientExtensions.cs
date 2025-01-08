using Newtonsoft.Json;
using System.Text;

public static class HttpClientExtensions
{

    public static async Task<TResponse> PostDataAsJsonAsync<TResponse>(this HttpClient httpClient, string url, object data)
    {

        var jsonContent = JsonConvert.SerializeObject(data);
        var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

        var response = await httpClient.PostAsync(url, content);
        var jsonResponse = await response.Content.ReadAsStringAsync();

        //response.EnsureSuccessStatusCode();

        var responseObject = JsonConvert.DeserializeObject<TResponse>(jsonResponse);

        return responseObject;

    }


    public static async Task<TResponse> GetDataAsJsonAsync<TResponse>(this HttpClient httpClient, string url)
    {
        var response = await httpClient.GetAsync(url);
        var jsonResponse = await response.Content.ReadAsStringAsync();

        //response.EnsureSuccessStatusCode();

        var responseObject = JsonConvert.DeserializeObject<TResponse>(jsonResponse);

        return responseObject;
    }




    public static async Task<string> ToHttpLog(this HttpRequestMessage request)
    {
        var sb = new StringBuilder();

        var line1 = $"{request.Method} {request.RequestUri} HTTP/{request.Version}";
        sb.AppendLine(line1);

        foreach (var (key, value) in request.Headers)
            foreach (var val in value)
            {
                var header = $"{key}: {val}";
                sb.AppendLine(header);
            }

        if (request.Content?.Headers != null)
        {
            foreach (var (key, value) in request.Content.Headers)
                foreach (var val in value)
                {
                    var header = $"{key}: {val}";
                    sb.AppendLine(header);
                }
        }
        sb.AppendLine();

        var body = await (request.Content?.ReadAsStringAsync() ?? Task.FromResult<string>(null));
        if (!string.IsNullOrWhiteSpace(body))
            sb.AppendLine(body);

        return sb.ToString();
    }

    public static async Task<string> ToHttpLog(this HttpResponseMessage response)
    {
        var sb = new StringBuilder();

        var statusCode = (int)response.StatusCode;
        var line1 = $"HTTP/{response.Version} {statusCode} {response.ReasonPhrase}";
        sb.AppendLine(line1);

        foreach (var (key, value) in response.Headers)
            foreach (var val in value)
            {
                var header = $"{key}: {val}";
                sb.AppendLine(header);
            }

        foreach (var (key, value) in response.Content.Headers)
            foreach (var val in value)
            {
                var header = $"{key}: {val}";
                sb.AppendLine(header);
            }
        sb.AppendLine();

        var body = await response.Content.ReadAsStringAsync();
        if (!string.IsNullOrWhiteSpace(body))
            sb.AppendLine(body);

        return sb.ToString();
    }


}
