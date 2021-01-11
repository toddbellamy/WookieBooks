using System.Threading.Tasks;
using WookieBooks.Domain.Books;
using WookieBooks.DomainFramework;

namespace WookieBooks.Domain.Interfaces
{
    public interface IBookCommandRepository
    {
        ValueTask<Book> Load(EntityId id);

        ValueTask Add(Book book);

        ValueTask<bool> Exists(EntityId id);

        ValueTask Commit();

        ValueTask<bool> AuthorExists(EntityId id);

        ValueTask AddAuthor(Author author);

        ValueTask<Author> LoadAuthor(EntityId id);

    }
}
