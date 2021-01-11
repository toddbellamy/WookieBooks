using System;

namespace WookieBooks.Domain.Interfaces
{
    public interface IGetBook
    {
        public Guid BookId { get; set; }
    }

    public interface IGetBooks
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
    }

    public interface IGetAuthorBooks
    {
        public Guid AuthorId { get; set; }
    }

    public interface IGetAuthors
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
    }

    public interface IGetCoverImage
    {
        public Guid BookId { get; set; }
    }

}
