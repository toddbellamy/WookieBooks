using System;
using WookieBooks.DomainFramework;

namespace WookieBooks.Domain.Books
{
    public record BookId : EntityId
    {
        public BookId(Guid id) : base(id) { }

        private BookId() { }

    }
}
