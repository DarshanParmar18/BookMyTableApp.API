using Serilog;

namespace BookMyTableApp.API
{
    public class RequestResponseLoggingMiddleware
    {
        private readonly RequestDelegate _next;

        public RequestResponseLoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            Log.Information($"Request: {context.Request.Method} {context.Request.Path}");

            var originalBodyStream = context.Response.Body;

            using (var responseBody = new MemoryStream())
            { 
                context.Response.Body = responseBody;

                await _next(context);

                var response = await FormatResponse(context.Response);
                Log.Information($"Response: {response}");  

                responseBody.Seek(0, SeekOrigin.Begin);
                await responseBody.CopyToAsync(originalBodyStream);
            }
        }

        private async Task<string> FormatResponse(HttpResponse response)
        {
            response.Body.Seek(0, SeekOrigin.Begin);
            var text = await new StreamReader(response.Body).ReadToEndAsync();
            response.Body.Seek(0,SeekOrigin.Begin);
            return $"{response.StatusCode}: {text}";
        }
    }
}
