using System;
using WookieBooks.DomainFramework;

namespace WookieBooks.Domain.Books
{
    public class Book : AggregateRoot<EntityId>
    {   
        public Guid BookId { get; private set; }
        public BookTitle Title { get; private set; }

        public MoneyValue Price { get; private set; }
        public BookDescription Description { get; private set; }

        public EntityId AuthorId { get; private set; }

        private Author author;
        public Author Author { 
            get { return author; }

            set
            {
                Author.CheckValidity(value.FirstName, value.LastName);
                author = value;
            } 
        }   

        public ImageFile CoverImage { get; private set; }

        private Book() { }

        public Book(EntityId id, EntityId authorId, string title, decimal price) 
        {
            Apply(new Events.BookCreated
            {
                Id = id,
                Title = title,
                AuthorId = authorId,
                Price = price
            });
        }

        public Book(EntityId id, Author author, string title, decimal price)
            :this(id, author.Id, title, price)
        {
            Author = author;
        }

        public void UpdateAuthor(Author author)
        {
            Apply(new Events.AuthorUpdated
            {
                Id = author.AuthorId,
                LastName = author.LastName, 
                FirstName = author.FirstName
            });
        }

        public void UpdateTitle(BookTitle title) =>
          Apply(new Events.BookTitleUpdated
          {
              Id = Id,
              Title = title
          });

        public void UpdateDescription(BookDescription text) =>
            Apply(new Events.BookDescriptionUpdated
            {
                Id = Id,
                Description = text
            });

        public void UpdatePrice(MoneyValue price) =>
            Apply(new Events.BookPriceUpdated
            {
                Id = Id,
                Price = price
            });

        public void UpdateCoverImage(ImageFile coverImage) =>
            Apply(new Events.CoverImageUpdated
            {
                Id = Id,
                CoverImage = coverImage,

            });

        protected override void EnsureValidState()
        {
            var valid =
                Id != null && Title != null && Author != null;
        }

        protected override void When(object @event)
        {
            switch(@event)
            {
                case Events.BookCreated e:
                    Id = new BookId(e.Id);
                    BookId = e.Id;
                    Title = e.Title;
                    AuthorId = new EntityId(e.AuthorId);
                    Price = e.Price;
                    break;
                case Events.BookTitleUpdated e:
                    Id = new BookId(e.Id);
                    Title = e.Title;
                    break;
                case Events.BookDescriptionUpdated e:
                    Id = new BookId(e.Id);
                    Description = e.Description;
                    break;
                case Events.AuthorUpdated e:
                    AuthorId = new EntityId(e.Id);
                    Author = new Author(AuthorId, e.FirstName, e.LastName);
                    break;
                case Events.BookPriceUpdated e:
                    Id = new BookId(e.Id);
                    Price = e.Price;
                    break;
                case Events.CoverImageUpdated e:
                    Id = new BookId(e.Id);
                    CoverImage = e.CoverImage;
                    break;
            }
        }
    }
}
