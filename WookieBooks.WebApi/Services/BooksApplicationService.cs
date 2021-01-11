using System;
using System.Threading.Tasks;
using WookieBooks.DomainFramework;
using WookieBooks.Domain.Books;
using static WookieBooks.WebApi.Services.Commands;
using WookieBooks.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace WookieBooks.WebApi.Services
{
    public class BooksApplicationService : IApplicationService
    {
        private readonly IBookCommandRepository _repository;

        public BooksApplicationService(IBookCommandRepository repository)
        {
            _repository = repository;
        }

        public Task Handle(object command) =>
            command switch
            {
                V1.Create cmd =>
                    HandleCreate(cmd),
                V1.UpdateTitle cmd =>
                    HandleUpdate(
                        cmd.Id,
                        b => b.UpdateTitle(
                            (BookTitle)cmd.Text
                        )
                    ),
                V1.UpdatePrice cmd =>
                    HandleUpdate(
                        cmd.Id,
                        b => b.UpdatePrice(
                            (MoneyValue)cmd.Value
                        )
                    ),
                V1.UpdateDescription cmd =>
                    HandleUpdate(
                        cmd.Id, 
                        b => b.UpdateDescription(
                            (BookDescription)cmd.Text
                        )
                    ),
                V1.UpdateAuthor cmd =>
                    HandleUpdate(
                        cmd.Id,
                        b => b.UpdateAuthor(
                            new Author(new EntityId(cmd.Id), cmd.LastName, cmd.FirstName)
                            )
                        ),
                V1.UpdateCoverImage cmd => 
                    HandleUpdate(
                        cmd.Id,
                        b => b.UpdateCoverImage(
                            handleFormFile(cmd)
                            )
                        ),
                _ => Task.CompletedTask
            };

        private async Task HandleCreate(V1.Create cmd)
        {
            if (await _repository.Exists(cmd.Id.ToString()))
                throw new InvalidOperationException(
                    $"Entity with id {cmd.Id} already exists"
                );

            var author = await _repository.LoadAuthor(cmd.AuthorId.ToString());
            if (author == null)
            {
                author = new Author(new EntityId(cmd.AuthorId), cmd.AuthorFirstName, cmd.AuthorLastName);
            }

            var book =
                new Book(
                    new EntityId(cmd.Id),
                    new EntityId(cmd.AuthorId),
                    cmd.Title,
                    cmd.Price
                );

            book.Author = author;

            await _repository.Add(book);
            await _repository.Commit();
        }

        private async Task HandleUpdate(Guid id, Action<Book> operation)
        {
            var book = await _repository
                .Load(id.ToString());

            if (book == null)
                throw new InvalidOperationException(
                    $"Entity with id {id} cannot be found"
                );

            operation(book);

            await _repository.Commit();
        }

        private ImageFile handleFormFile(V1.UpdateCoverImage command)
        {
            IFormFile file = command.ImageFile;
            byte[] fileBytes = Array.Empty<byte>();

            using (var memStream = new MemoryStream())
            {
                file.CopyToAsync(memStream);
                fileBytes = memStream.ToArray();
            }

            return new ImageFile(file.FileName, fileBytes);
        }
    }
}
