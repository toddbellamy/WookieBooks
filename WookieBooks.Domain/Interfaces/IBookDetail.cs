
namespace WookieBooks.Domain.Interfaces
{
    public interface IBookDetail
    {
        public string BookId { get; set; }
        public string Title { get; set; }

        public string AuthorFullName { get; set; }

        public string Description { get; set; }

    }
}
