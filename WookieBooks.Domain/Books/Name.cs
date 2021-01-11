using System;

namespace WookieBooks.DomainFramework
{
    public record Name : TextValue
    {
        public static int MaxLength { get; } = 30;

        public static implicit operator string(Name text)
            => text.Value;

        public static implicit operator Name(string text)
        {
            CheckValidity(text, MaxLength);
            return new Name(text);
        }

        public override string ToString()
        {
            return this.Value;
        }

        protected Name() { }

        private Name(string text) => Value = text ?? "";

    }
}
