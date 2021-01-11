
using WookieBooks.Domain.Interfaces;

namespace WookieBooks.Data
{
    public record BookListItem : IBookListItem
    {
        public string BookId { get; set; }
        public string Title { get; set; }

        public string AuthorFullName { get; set; }
    }
}
