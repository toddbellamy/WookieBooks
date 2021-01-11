using System;

namespace WookieBooks.DomainFramework
{
    public record MoneyValue : Value<MoneyValue>
    {
        public static implicit operator decimal(MoneyValue money)
            => money.Value;

        public static implicit operator MoneyValue(decimal value)
        {
            return new MoneyValue(value);
        }

        public decimal Value { get; protected set; } = 0.0m;

        protected MoneyValue() { }

        public MoneyValue(decimal value)
        {
            CheckValidity(value);

            Value = value;
        }

        protected static void CheckValidity(decimal value)
        {
            if (value <= 0.0m)
                throw new ArgumentException("Money value may not be negative.");
        }
    }
}
