using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IO;

namespace API.Middleware
{
    public class RequestLoggerMiddleware
    {
        private readonly ILogger<RequestLoggerMiddleware> _logger;
        private readonly RequestDelegate _next;

        public RequestLoggerMiddleware(RequestDelegate next, ILogger<RequestLoggerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            var originalBodyStream = context.Response.Body;

            var request = await GetRequestAsTextAsync(context.Request);

            _logger.LogInformation(request);

            await _next(context);

        }

        private async Task<string> GetRequestAsTextAsync(HttpRequest request)
        {
            var body = request.Body;

            request.EnableBuffering();

            var buffer = new byte[Convert.ToInt32(request.ContentLength)];

            await request.Body.ReadAsync(buffer, 0, buffer.Length);

            var bodyAsText = Encoding.UTF8.GetString(buffer);

            request.Body.Position = 0;

            string query = "";
            foreach (var item in request.Query.Keys)
            {
                query += "Key: " + item.ToString() + " Value: " + request.Query[item].ToString() + "\n";
            }
            string headers = "";
            foreach (var item in request.Headers.Keys)
            {
                headers += "\n" + item.ToString() + " : " + request.Headers[item].ToString();
            }

            return $"{"\n"}Headers: {headers}{"\n"}BODY: {bodyAsText} \nMethod: {request.Method} {"\n"}Query: {query}";

        }
    }
}