using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WookieBooks.WebApi.Infrastructure;
using WookieBooks.Data.Queries;
using WookieBooks.Domain.Interfaces;
using System.IO;
using Microsoft.Extensions.Logging;

namespace WookieBooks.WebApi.Controllers
{
    [ApiController]
    [Route("/books")]
    public class BookQueryController : ControllerBase
    {
        private IBookQueryRepository repository;

        private static ILogger _logger;

        public BookQueryController(IBookQueryRepository repository)
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

            this.repository = repository;
        }

        [HttpGet]
        [Route("list")]
        public Task<IActionResult> Get([FromQuery] GetBooks request)
            => RequestHandler.HandleQuery(() => repository.Query(request), _logger);

        [HttpGet]
        [Route("{bookId}")]
        public Task<IActionResult> Get([FromRoute] GetBook request)
            => RequestHandler.HandleQuery(() => repository.Query(request), _logger);


        [HttpGet]
        [Route("/authors/list")]
        public Task<IActionResult> Get([FromQuery] GetAuthors request)
            => RequestHandler.HandleQuery(() => repository.Query(request), _logger);

        [HttpGet]
        [Route("coverimage/{bookId}")]
        public Task<IActionResult> Get([FromRoute] GetCoverImage request)
            => RequestHandler.HandleQuery(async () => handleImageFile(await repository.Query(request)), _logger);

        private FileContentResult handleImageFile(ICoverImageDetail coverImage)
        {
            var extension = Path.GetExtension(coverImage.FileName.ToLower());
            extension = extension == "jpg" ? "jpeg" : extension;
            return File(coverImage.ImageData, $"image/{extension}");
        }


    }
}
