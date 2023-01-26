using CarsCatalogAPI.Data;
using CarsCatalogAPI.Models;

namespace CarsCatalogAPI.Middlewares
{
    public class RequestResponseLoggerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly bool _isRequestResponseLoggingEnabled;

        public RequestResponseLoggerMiddleware(RequestDelegate next, IConfiguration config)
        {
            _next = next;
            _isRequestResponseLoggingEnabled = config.GetValue("EnableRequestResponseLogging", false);
        }

        public async Task InvokeAsync(HttpContext httpContext, CarsDBContext carsDbContext)
        {
            if (_isRequestResponseLoggingEnabled)
            {
                Request request = new Request {
                    Method = httpContext.Request.Method,
                    Path = httpContext.Request.Path,
                    QueryString = httpContext.Request.QueryString.ToString(),
                    Headers = FormatHeaders(httpContext.Request.Headers),
                    Schema = httpContext.Request.Scheme,
                    Host = httpContext.Request.Host.ToString(),
                    Body = await ReadBodyFromRequest(httpContext.Request)
                };

                RequestInfo requestInfo = new RequestInfo { Request = request };

                carsDbContext.RequestInfos.Add(requestInfo);
                carsDbContext.SaveChanges();

                Console.WriteLine($"HTTP request information:\n" +
                    $"\tMethod: {request.Method}\n" +
                    $"\tPath: {request.Path}\n" +
                    $"\tQueryString: {request.QueryString}\n" +
                    $"\tHeaders: {request.Headers}\n" +
                    $"\tSchema: {request.Schema}\n" +
                    $"\tHost: {request.Host}\n" +
                    $"\tBody: {request.Body}");

                var originalResponseBody = httpContext.Response.Body;
                using var newResponseBody = new MemoryStream();
                httpContext.Response.Body = newResponseBody;

                await _next(httpContext);

                newResponseBody.Seek(0, SeekOrigin.Begin);
                var responseBodyText = await new StreamReader(httpContext.Response.Body).ReadToEndAsync();

                Response response = new Response
                {
                    StatusCode = httpContext.Response.StatusCode.ToString(),
                    ContentType = httpContext.Response.ContentType,
                    Headers = FormatHeaders(httpContext.Response.Headers),
                    Body = responseBodyText
                };

                requestInfo.Response = response;
                carsDbContext.Update(requestInfo);
                carsDbContext.SaveChanges();

                Console.WriteLine($"HTTP response information:\n" +
                    $"\tStatusCode: {response.StatusCode}\n" +
                    $"\tContentType: {response.ContentType}\n" +
                    $"\tHeaders: {response.Headers}\n" +
                    $"\tBody: {response.Body}");

                newResponseBody.Seek(0, SeekOrigin.Begin);
                await newResponseBody.CopyToAsync(originalResponseBody);
            }
            else
            {
                await _next(httpContext);
            }
        }

        private static string FormatHeaders(IHeaderDictionary headers) => string.Join(", ", headers.Select(kvp => $"{{{kvp.Key}: {string.Join(", ", kvp.Value)}}}"));

        private static async Task<string> ReadBodyFromRequest(HttpRequest request)
        {
            request.EnableBuffering();

            using var streamReader = new StreamReader(request.Body, leaveOpen: true);
            var requestBody = await streamReader.ReadToEndAsync();

            request.Body.Position = 0;
            return requestBody;
        }
    }
}
