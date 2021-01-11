using System;
using WookieBooks.DomainFramework;

namespace WookieBooks.Domain.Books
{
    public static class Events
    {
        public class BookCreated
        {
            public Guid Id { get; set; }
            public Guid AuthorId { get; set; }
            public string Title { get; set; }

            public decimal Price { get; set; }
        }

        public class BookPriceUpdated
        {
            public Guid Id { get; set; }
            public decimal Price { get; set; }
        }

        public class BookTitleUpdated
        {
            public Guid Id { get; set; }
            public string Title { get; set; }
        }
        public class BookDescriptionUpdated
        {
            public Guid Id { get; set; }
            public string Description { get; set; }
        }

        public class AuthorCreated
        {
            public Guid Id { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
        }
        public class AuthorUpdated
        {
            public Guid Id { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
        }

        public class CoverImageUpdated
        {
            public Guid Id { get; set; }
            public ImageFile CoverImage { get; set; }
        }
    }
}
