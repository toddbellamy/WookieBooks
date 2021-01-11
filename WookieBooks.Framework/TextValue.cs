using System;


namespace WookieBooks.DomainFramework
{
    public record TextValue : Value<TextValue> 
    {
        public string Value { get; protected set; } = "";

        protected TextValue() { }

        public TextValue(string text) => Value = text ?? "";

        public override string ToString()
        {
            return Value;
        }

        protected static void CheckValidity(string value, int maxLength)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Text value may not be empty.", nameof(value));

            if (value.Length > maxLength)
                throw new ArgumentOutOfRangeException(
                    $"Text may not be longer that {maxLength} characters",
                    nameof(value));
        }
    }
}
