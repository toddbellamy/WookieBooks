using WookieBooks.Domain.Interfaces;

namespace WookieBooks.Data
{
    public record BookDetail : IBookDetail
    {
        public string BookId { get; set; }
        public string Title { get; set; }

        public string AuthorFullName { get; set; }

        public string Description { get; set; }

        public string CoverImageFileName { get; set; }

        public byte[] CoverImageData { get; set; }
    }
}
