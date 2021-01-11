using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using WookieBooks.Domain.Books;
using WookieBooks.DomainFramework;

namespace WookieBooks.Data
{
    public class AuthorEntityTypeConfiguration : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            builder.HasKey(m => m.AuthorId);
            builder.OwnsOne(m => m.Id);
            builder.OwnsOne(m => m.FirstName);
            builder.OwnsOne(m => m.LastName);
        }

        public static Author DefaultAuthor
        {
            get
            {
                return defaultAuthor;
            }
        }

        private static Author defaultAuthor =
            new Author(
                new EntityId(new Guid("14a3df1e-8b83-45e5-aae9-6af3cd6b1cf1")),
                "Jane",
                "Wordsworth");

    }
}
