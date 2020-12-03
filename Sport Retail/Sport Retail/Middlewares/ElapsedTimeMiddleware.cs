using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Sport_Retail.Middlewares
{
    public class ElapsedTimeMiddleware
    {
        private readonly ILogger _logger;
        private readonly RequestDelegate _next;

        public ElapsedTimeMiddleware(RequestDelegate next, ILogger<ElapsedTimeMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            var sw = new Stopwatch();
            sw.Start();
            await _next(context);

            _logger.LogInformation($"Executed in {sw.ElapsedMilliseconds}ms");
            Console.WriteLine($"Executed in {sw.ElapsedMilliseconds}ms");
        }
    }
}
