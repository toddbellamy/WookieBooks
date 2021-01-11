using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using WookieBooks.Domain.Books;
using WookieBooks.DomainFramework;

namespace WookieBooks.Data
{
    public class BookEntityTypeConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasKey(m => m.BookId);
            builder.OwnsOne(m => m.Id);
            builder.OwnsOne(m => m.Title);
            builder.OwnsOne(m => m.Description);
            builder.OwnsOne(m => m.AuthorId);
            builder.OwnsOne(m => m.Price);
            builder.OwnsOne(m => m.CoverImage);
        }

        private static List<Book> defaultBooks =
            new List<Book>
            {
                new Book(
                    new EntityId(new Guid("1fa85f64-5717-4562-b3fc-2c963f66afa1")),
                    AuthorEntityTypeConfiguration.DefaultAuthor,
                    "C# for Wookies",
                    19.99m),
                new Book(
                    new EntityId(new System.Guid("2fa85f64-5717-4562-b3fc-2c963f66afa2")),
                    AuthorEntityTypeConfiguration.DefaultAuthor,
                    "A Wookie's Guide to Fur Braiding",
                    18.88m)
            };
        public static IEnumerable<Book> DefaultBooks
        {
            get { return defaultBooks; }
        }
    }
}
