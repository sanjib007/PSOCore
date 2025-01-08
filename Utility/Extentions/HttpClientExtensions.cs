using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Text;

namespace Utility.Extentions
{
    public static class HttpClientExtensions
    {
        public static async Task<TResponse> PostDataAsJsonAsync<TResponse>(this HttpClient httpClient, string url, object data)
        {
            try
            {
                var jsonContent = JsonConvert.SerializeObject(data);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                var response = await httpClient.PostAsync(url, content);
                var jsonResponse = await response.Content.ReadAsStringAsync();

                //response.EnsureSuccessStatusCode();

                var responseObject = JsonConvert.DeserializeObject<TResponse>(jsonResponse);

                return responseObject;
            }
            catch (Exception ex)
            {

                throw;
            }
       
        }

        public static async Task<TResponse> GetDataAsJsonAsync<TResponse>(this HttpClient httpClient, string url)
        {
            var response = await httpClient.GetAsync(url);
            var jsonResponse = await response.Content.ReadAsStringAsync();

            //response.EnsureSuccessStatusCode();

            var responseObject = JsonConvert.DeserializeObject<TResponse>(jsonResponse);

            return responseObject;
        }

        public static string BodyToString(this HttpRequest request)
        {
            var returnValue = string.Empty;
            request.EnableBuffering();
            //ensure we read from the begining of the stream - in case a reader failed to read to end before us.
            request.Body.Position = 0;
            //use the leaveOpen parameter as true so further reading and processing of the request body can be done down the pipeline
            using (var stream = new StreamReader(request.Body, Encoding.UTF8, true, 1024, leaveOpen: true))
            {
                returnValue = stream.ReadToEnd();
            }
            //reset position to ensure other readers have a clear view of the stream 
            request.Body.Position = 0;
            return returnValue;
        }


        public static async Task<string> ToHttpLog(this HttpRequest request)
        {
            var sb = new StringBuilder();
            request.EnableBuffering();
            var line1 = $"{request.Method} {request.Scheme}://{request.Host}{request.Path} {request.Protocol}";
            sb.AppendLine(line1);

            foreach (var (key, value) in request.Headers)
            {
                var header = $"{key}: {value}";
                sb.AppendLine(header);
            }
            sb.AppendLine();


            // Reset the request body stream position to the start so we can read it
            request.Body.Position = 0;
            var buffer = new byte[Convert.ToInt32(request.ContentLength)];
            // Leave the body open so the next middleware can read it.
            using (var reader = new StreamReader(
                request.Body,
                encoding: Encoding.UTF8,
                detectEncodingFromByteOrderMarks: false,
                bufferSize: buffer.Length,
                leaveOpen: true))
            {
                var body = await reader.ReadToEndAsync();
                if (!string.IsNullOrWhiteSpace(body))
                    sb.AppendLine(body);
                // Do some processing with body…
            }
            //reset position to ensure other readers have a clear view of the stream 
            request.Body.Position = 0;
            return sb.ToString();
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




        public static async Task<string> ToHttpLogQueryStr(this HttpRequest request)
        {
            var sb = new StringBuilder();
            request.EnableBuffering();

            var queryString = request.QueryString.HasValue ? request.QueryString.Value : string.Empty;
            var line1 = $"{request.Method} {request.Scheme}://{request.Host}{request.Path}{queryString} {request.Protocol}";
            sb.AppendLine(line1);

            foreach (var (key, value) in request.Headers)
            {
                var header = $"{key}: {value}";
                sb.AppendLine(header);
            }
            sb.AppendLine();

            // Reset the request body stream position to the start so we can read it
            request.Body.Position = 0;
            var bufferSize = Math.Max(1024, (int)(request.ContentLength ?? 0));
            var buffer = new byte[bufferSize];
            // Leave the body open so the next middleware can read it.
            using (var reader = new StreamReader(
                request.Body,
                encoding: Encoding.UTF8,
                detectEncodingFromByteOrderMarks: false,
                bufferSize: bufferSize,
                leaveOpen: true))
            {
                var body = await reader.ReadToEndAsync();
                if (!string.IsNullOrWhiteSpace(body))
                    sb.AppendLine(body);
                // Do some processing with the body…
            }
            // Reset position to ensure other readers have a clear view of the stream
            request.Body.Position = 0;
            return sb.ToString();
        }


    }

}
