using System;
using WookieBooks.Domain.Interfaces;

namespace WookieBooks.Data.Queries
{
    public class GetBook : IGetBook
    {
        public Guid BookId { get; set; }
    }

    public class GetBooks : IGetBooks
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
    }

    public class GetAuthorBooks : IGetAuthorBooks
    {
        public Guid AuthorId { get; set; }
    }

    public class GetAuthors : IGetAuthors
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
    }

    public class GetCoverImage : IGetCoverImage
    {
        public Guid BookId { get; set; }
    }

}
