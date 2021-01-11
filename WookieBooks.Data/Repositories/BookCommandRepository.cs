using System.Threading.Tasks;
using WookieBooks.Domain.Books;
using WookieBooks.Domain.Interfaces;
using WookieBooks.DomainFramework;

namespace WookieBooks.Data.Repositories
{
    public class BookCommandRepository : CommandRepository, IBookCommandRepository
    {
        public BookCommandRepository(CommandContext context)
            :base(context)
        {}

        public async ValueTask Add(Book book)
            => await context.Books.AddAsync(book);

        public async ValueTask<bool> Exists(EntityId id)
            => await context.Books.FindAsync(id.Value) != null;


        public async ValueTask<Book> Load(EntityId id)
            => await context.Books.FindAsync(id.Value);

        public async ValueTask<bool> AuthorExists(EntityId id)
            => await context.Authors.FindAsync(id.Value) != null;

        public async ValueTask AddAuthor(Author author)
            => await context.Authors.AddAsync(author);

        public async ValueTask<Author> LoadAuthor(EntityId id)
            => await context.Authors.FindAsync(id.Value);

    }
}
