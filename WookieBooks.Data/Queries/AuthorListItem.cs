using WookieBooks.Domain.Interfaces;

namespace WookieBooks.Data
{
    public class AuthorListItem : IAuthorListItem
    {
        public string AuthorId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
