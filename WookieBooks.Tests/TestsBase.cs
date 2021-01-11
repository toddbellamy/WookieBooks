using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using WookieBooks.Data;

namespace WookieBooks.Tests
{
    public class CommandTestsBase
    {
        private static InMemoryDatabaseRoot _databaseRoot;
        protected static InMemoryDatabaseRoot DatabaseRoot
        {
            get
            {
                if (_databaseRoot == null)
                    _databaseRoot = new InMemoryDatabaseRoot();

                return _databaseRoot;
            }
        }

        protected DbContextOptions<CommandContext> ContextOptions { get; }

        protected ILogger<CommandTestsBase> logger { get;  }

        public CommandTestsBase()
        {
            using ILoggerFactory loggerFactory =
                LoggerFactory.Create(builder =>
                    builder.AddSimpleConsole(options =>
                    {
                        //options.IncludeScopes = true;
                        options.SingleLine = true;
                        options.TimestampFormat = "hh:mm:ss ";
                    }));

            logger = loggerFactory.CreateLogger<CommandTestsBase>();
            
            ContextOptions = new DbContextOptionsBuilder<CommandContext>()
                .UseInMemoryDatabase("WookieBooksDatabase", DatabaseRoot)
                .Options;
        }
    }
}
