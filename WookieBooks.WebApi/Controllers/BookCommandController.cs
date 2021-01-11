using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WookieBooks.WebApi.Services;
using WookieBooks.WebApi.Infrastructure;
using WookieBooks.DomainFramework;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using Microsoft.Extensions.Logging;

namespace WookieBooks.WebApi.Controllers
{
    [ApiController]
    [Route("/books")]
    public class BookCommandController : ControllerBase
    {
        private readonly IApplicationService _appService;

        private readonly ILogger _logger;

        public BookCommandController(IApplicationService appService)
        {
            using ILoggerFactory loggerFactory =
                LoggerFactory.Create(builder =>
                    builder.AddSimpleConsole(options =>
                    {
                        //options.IncludeScopes = true;
                        options.SingleLine = true;
                        options.TimestampFormat = "hh:mm:ss ";
                    }));

            _logger = loggerFactory.CreateLogger<BookQueryController>();

            _appService = appService;
        }

        [HttpPost]
        public Task<IActionResult> Post(Commands.V1.Create request)
              => RequestHandler.HandleCommand(request, _appService.Handle, _logger);

        [Route("title")]
        [HttpPut]
        public Task<IActionResult> Put(Commands.V1.UpdateTitle request)
              => RequestHandler.HandleCommand(request, _appService.Handle, _logger);

        [Route("description")]
        [HttpPut]
        public Task<IActionResult> Put(Commands.V1.UpdateDescription request)
            => RequestHandler.HandleCommand(request, _appService.Handle, _logger);


        [Route("price")]
        [HttpPut]
        public Task<IActionResult> Put(Commands.V1.UpdatePrice request)
            => RequestHandler.HandleCommand(request, _appService.Handle, _logger);


        [Route("coverimage")]
        [HttpPut]
        public async Task<IActionResult> Put([FromForm] Commands.V1.UpdateCoverImage request)
        {
            return await RequestHandler.HandleCommand(request, _appService.Handle, _logger);
        }
    }
}
