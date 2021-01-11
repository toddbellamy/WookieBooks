using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace WookieBooks.Data
{
    public class QueryContext : CommandContext
    {
        public QueryContext(DbContextOptions<CommandContext> options, ILoggerFactory loggerFactory)
            :base(options, loggerFactory)
        {}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            base.OnConfiguring(optionsBuilder);
        }

        public static new void RegisterAsService(IServiceCollection services)
        {
            services.AddDbContext<QueryContext>(options => options.UseInMemoryDatabase("WookieBooksDatabase", DatabaseRoot));
        }

        public override int SaveChanges()
        {
            throw new InvalidOperationException("Saving changes with query context is not permitted.");
        }
    }
}
