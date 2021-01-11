using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace WookieBooks.WebApi.Infrastructure
{
    public class RequestHandler
    {
        public static async Task<IActionResult> HandleCommand<T>(T request, Func<T, Task> handler, ILogger log)
        {
            try
            {
                log.LogDebug("Handling HTTP request of type {type}", typeof(T).Name);
                await handler(request);
                return new OkResult();
            }
            catch (Exception e)
            {
                log.LogError(e, "Error handling the command");
                return new BadRequestObjectResult(new
                {
                    error = e.Message,
                    stackTrace = e.StackTrace
                });
            }
        }

        public static async Task<IActionResult> HandleQuery<TModel>(Func<Task<TModel>> query, ILogger log)
        {
            try
            {
                return new OkObjectResult(await query());
            }
            catch (Exception e)
            {
                log.LogError(e, "Error handling the query");
                return new BadRequestObjectResult(new
                {
                    error = e.Message,
                    stackTrace = e.StackTrace
                });
            }
        }
    }
}
