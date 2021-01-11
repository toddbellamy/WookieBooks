using WookieBooks.DomainFramework;

namespace WookieBooks.Domain.Books
{
    public record BookDescription : TextValue
    {
        public static int MaxLength { get; } = 100;

        public static implicit operator string(BookDescription text)
            => text == null ? "" : text.Value;

        public static implicit operator BookDescription(string text)
        {
            CheckValidity(text, MaxLength);
            return new BookDescription(text);
        }

        protected BookDescription() { }

        private BookDescription(string text) 
            => Value = text ?? "";

    }
}
