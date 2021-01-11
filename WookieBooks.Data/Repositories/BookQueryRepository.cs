using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WookieBooks.Domain.Books;
using WookieBooks.Domain.Interfaces;

namespace WookieBooks.Data
{
    public class BookQueryRepository : QueryRepository, IBookQueryRepository
    {
        public BookQueryRepository(QueryContext context)
            : base(context)
        { }

        private Expression<Func<Book, BookListItem>> bookItemSelector =
            b => new BookListItem
            {
                BookId = b.Id.ToString(),
                Title = b.Title.ToString(),
                AuthorFullName = $"{b.Author.FirstName.Value} {b.Author.LastName.Value}"
            };


        private Expression<Func<Book, BookDetail>> bookDetailSelector =
            b => new BookDetail
            {
                BookId = b.Id.ToString(),
                Title = b.Title.ToString(),
                AuthorFullName = $"{b.Author.FirstName.Value} {b.Author.LastName.Value}",
                Description = b.Description,
                CoverImageFileName = b.CoverImage.FileName,
                CoverImageData = b.CoverImage.ImageData
            };

        public async Task<IEnumerable<IBookListItem>> Query(IGetBooks query)
        {
            return await context.Books
                .Select(bookItemSelector)
                .Skip(query.Page - 1)
                .Take(query.PageSize)
                .ToListAsync();
        }

        public async Task<IBookDetail> Query(IGetBook query)
        {
            return await context.Books
                .Where(b => b.BookId == query.BookId)
                .Select(bookDetailSelector)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<IBookListItem>> Query(IGetAuthorBooks query)
        {
            return await context.Books
                .Where(b => b.AuthorId == query.AuthorId)
                .Select(bookItemSelector)
                .ToListAsync();
        }

        public async Task<IEnumerable<IAuthorListItem>> Query(IGetAuthors query)
        {
            return await context.Authors
                .Select(a => new AuthorListItem { AuthorId = a.Id.ToString(), FirstName = a.FirstName, LastName = a.LastName })
                .Skip(query.Page - 1)
                .Take(query.PageSize)
                .ToListAsync();
        }

        public async Task<ICoverImageDetail> Query(IGetCoverImage query)
        {
            return await context.Books
                .Where(b => b.BookId == query.BookId)
                .Select(b => new CoverImageDetail
                {
                    FileName = b.CoverImage.FileName,
                    ImageData = b.CoverImage.ImageData
                })
                .FirstOrDefaultAsync();
        }
    }
}
