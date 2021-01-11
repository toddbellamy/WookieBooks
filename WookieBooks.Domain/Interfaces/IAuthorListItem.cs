using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WookieBooks.Domain.Interfaces
{
    public interface IAuthorListItem
    {
        public string AuthorId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

    }
}
