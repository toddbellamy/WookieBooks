using System;
using Xunit;
using WookieBooks.Domain.Books;
using WookieBooks.DomainFramework;
using WookieBooks.Data;
using WookieBooks.Data.Repositories;
using WookieBooks.WebApi.Services;
using static WookieBooks.WebApi.Services.Commands;
using WookieBooks.WebApi.Infrastructure;
using System.Linq;

namespace WookieBooks.Tests
{
    public class BookCommandTests : CommandTestsBase
    {
        private Book _testBook;

        private void SeedData()
        {
            using (var commandContext = new CommandContext(ContextOptions))
            {
                _testBook = new Book(
                    new EntityId(Guid.NewGuid()),
                    new Author(new EntityId(Guid.NewGuid()), "Mary", "Wrytes"),
                    "A Wookie Guide to the Galaxy",
                    3.99m);
                commandContext.Books.Add(_testBook);
                commandContext.SaveChanges();
            }
        }

        public BookCommandTests() : base()
        {
            SeedData();
        }

        [Fact]
        public async void CanCreateBook()
        {
            using(var commandContext = new CommandContext(ContextOptions))
            {
                var bookCommandRepo = new BookCommandRepository(commandContext);
                var bookAppService = new BooksApplicationService(bookCommandRepo);

                var createRequest = new V1.Create
                {
                    Title = "A Wookie Life",
                    AuthorFirstName = "Logharra",
                    AuthorLastName = "Wook",
                    AuthorId = Guid.NewGuid(),
                    Id = Guid.NewGuid(),
                    Price = 5.99m
                };

                await RequestHandler.HandleCommand(createRequest, bookAppService.Handle, logger);

                var actual =
                    commandContext.Books
                    .Where(b => b.BookId == createRequest.Id)
                    .SingleOrDefault();

                Assert.NotNull(actual);
                Assert.Equal(actual.Title.Value, createRequest.Title);
            }
        }

        [Fact]
        public async void CanUpdateTitle()
        {
            using (var commandContext = new CommandContext(ContextOptions))
            {
                var bookCommandRepo = new BookCommandRepository(commandContext);
                var bookAppService = new BooksApplicationService(bookCommandRepo);

                var updateRequest = new V1.UpdateTitle
                {
                    Text = "Nu Updated Title",
                    Id = _testBook.BookId,
                };

                await RequestHandler.HandleCommand(updateRequest, bookAppService.Handle, logger);

                var actual =
                    commandContext.Books
                    .Where(b => b.BookId == updateRequest.Id)
                    .SingleOrDefault();

                Assert.NotNull(actual);
                Assert.Equal(actual.Title, updateRequest.Text);
            }
        }


        [Fact]
        public async void CanUpdateDescription()
        {
            using (var commandContext = new CommandContext(ContextOptions))
            {
                var bookCommandRepo = new BookCommandRepository(commandContext);
                var bookAppService = new BooksApplicationService(bookCommandRepo);

                var updateRequest = new V1.UpdateDescription
                { 
                    Text = "Noo Description of Book",
                    Id = _testBook.BookId,
                };

                await RequestHandler.HandleCommand(updateRequest, bookAppService.Handle, logger);

                var actual =
                    commandContext.Books
                    .Where(b => b.BookId == updateRequest.Id)
                    .SingleOrDefault();

                Assert.NotNull(actual);
                Assert.Equal(actual.Description, updateRequest.Text);
            }
        }


        [Fact]
        public async void CanUpdatePrice()
        {
            using (var commandContext = new CommandContext(ContextOptions))
            {
                var bookCommandRepo = new BookCommandRepository(commandContext);
                var bookAppService = new BooksApplicationService(bookCommandRepo);

                var updateRequest = new V1.UpdatePrice
                {
                    Value = _testBook.Price + 1.0m,
                    Id = _testBook.BookId,
                };

                await RequestHandler.HandleCommand(updateRequest, bookAppService.Handle, logger);

                var actual =
                    commandContext.Books
                    .Where(b => b.BookId == updateRequest.Id)
                    .SingleOrDefault();

                Assert.NotNull(actual);
                Assert.Equal(actual.Price.Value, updateRequest.Value);
            }
        }
    }
}