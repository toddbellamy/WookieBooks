using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.Linq;
using WookieBooks.Data;
using WookieBooks.Data.Repositories;
using WookieBooks.Domain.Interfaces;
using WookieBooks.DomainFramework;
using WookieBooks.WebApi.Services;

namespace WookieBooks.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            CommandContext.RegisterAsService(services);
            QueryContext.RegisterAsService(services);

            services.AddScoped<IBookCommandRepository, BookCommandRepository>();
            services.AddScoped<IBookQueryRepository, BookQueryRepository>();
            services.AddScoped<IApplicationService, BooksApplicationService>();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WookieBooks.WebApi", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WookieBooks.WebApi v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            EnsureDatabase(app);
        }

        private void EnsureDatabase(IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope();
            var context = serviceScope.ServiceProvider.GetRequiredService<CommandContext>();
            context.EnsureDatabase();

            context.Authors.Add(AuthorEntityTypeConfiguration.DefaultAuthor);
            context.SaveChanges();
            context.Books.AddRange(BookEntityTypeConfiguration.DefaultBooks);
            context.SaveChanges();

            var books = context.Books.Include(b => b.Author).Select(b => b);
            Console.WriteLine(books.Count());
        }
    }
}
