using System;


namespace WookieBooks.DomainFramework
{
    public record Value<T> where T : Value<T>
    {
    }
}
