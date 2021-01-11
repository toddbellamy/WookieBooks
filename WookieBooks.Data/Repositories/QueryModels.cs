using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WookieBooks.Domain.Interfaces;

namespace WookieBooks.Data
{
    public class QueryModels
    {
        public class GetBooks : IGetBooks
        {
            public int Page { get; set; }
            public int PageSize { get; set; }
        }

        public class GetAuthorBooks : IGetAuthorBooks
        {
            public Guid AuthorId { get; set; }
            public int Page { get; set; }
            public int PageSize { get; set; }
        }

        public class GetBook : IGetBook
        {
            public Guid BookId { get; set; }
        }
    }
}

