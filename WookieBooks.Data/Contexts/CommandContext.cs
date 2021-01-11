using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore.Storage;
using System.Diagnostics;

namespace WookieBooks.Data
{
    public class CommandContext : DbContext
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

        private readonly ILoggerFactory _loggerFactory;

        public DbSet<Domain.Books.Book> Books { get; set; }
        public DbSet<Domain.Books.Author> Authors { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if(_loggerFactory != null)
                optionsBuilder.UseLoggerFactory(_loggerFactory);
            
            if (Debugger.IsAttached)
                optionsBuilder.EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BookEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new AuthorEntityTypeConfiguration());
        }

        public CommandContext(DbContextOptions<CommandContext> options, ILoggerFactory loggerFactory)
            : base(options)
        {
            _loggerFactory = loggerFactory;            
        }
        public CommandContext(DbContextOptions<CommandContext> options)
            : base(options)
        {
        }

        protected CommandContext(DbContextOptions options, ILoggerFactory loggerFactory)
            : base(options)
        {
            _loggerFactory = loggerFactory;
        }

        public static void RegisterAsService(IServiceCollection services)
        {
            services.AddDbContext<CommandContext>(options => options.UseInMemoryDatabase("WookieBooksDatabase", DatabaseRoot));
        }

        public void EnsureDatabase()
        {
            this.Database.EnsureCreated();
        }
    }
}
