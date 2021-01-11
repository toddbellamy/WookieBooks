using WookieBooks.DomainFramework;

namespace WookieBooks.Domain.Books
{
    public record BookTitle : TextValue
    {
        public static int MaxLength { get; } = 50;

        public static implicit operator string(BookTitle text)
            => text.Value;

        public static implicit operator BookTitle(string text)
        {
            CheckValidity(text, MaxLength);
            return new BookTitle(text);
        }

        public override string ToString()
        {
            return Value;
        }

        protected BookTitle() { }

        private BookTitle(string text) 
            => Value = text ?? "";
    }
}
