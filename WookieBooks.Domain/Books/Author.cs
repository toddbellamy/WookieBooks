using System;
using WookieBooks.DomainFramework;

namespace WookieBooks.Domain.Books
{

    public class Author : Entity
    {
        protected Author() { }

        public Author(EntityId id, string firstName, string lastName)
        {
            Apply(new Events.AuthorCreated
            {
                Id = id,
                FirstName = firstName,
                LastName = lastName
            });
        }

        public Guid AuthorId;
        public Name FirstName { get; private set; }

        public Name LastName { get; private set; }

        public static void CheckValidity(string firstName, string lastName)
        {
            if (string.IsNullOrWhiteSpace(firstName))
                throw new ArgumentException("First Name may not be empty.", nameof(FirstName));

            if (string.IsNullOrWhiteSpace(lastName))
                throw new ArgumentException("Last Name may not be empty.", nameof(LastName));
        }

        protected override void When(object @event)
        {
            switch (@event)
            {
                case Events.AuthorCreated e:
                    Id = new EntityId(e.Id);
                    AuthorId = e.Id;
                    FirstName = (Name)e.FirstName;
                    LastName = (Name)e.LastName;
                    break;
            }
        }
    }
}
