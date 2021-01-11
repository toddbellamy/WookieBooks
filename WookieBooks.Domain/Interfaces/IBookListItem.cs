
namespace WookieBooks.Domain.Interfaces
{
    public interface IBookListItem
    {
        public string BookId { get; set; }
        public string Title { get; set; }

        public string AuthorFullName { get; set; }

    }
}
