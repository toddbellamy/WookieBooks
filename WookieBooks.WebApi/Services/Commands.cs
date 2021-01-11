using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WookieBooks.WebApi.Services
{
    public static class Commands
    {
        public static class V1
        {
            public class Create
            {
                public Guid Id { get; set; }
                public Guid AuthorId { get; set; }

                public string Title { get; set; }

                public decimal Price { get; set; }

                public string AuthorFirstName { get; set; }
                public string AuthorLastName { get; set; }
            }

            public class UpdatePrice
            {
                public Guid Id { get; set; }
                public decimal Value { get; set; }
            }


            public class UpdateTitle
            {
                public Guid Id { get; set; }
                public string Text { get; set; }
            }

            public class UpdateDescription
            {
                public Guid Id { get; set; }
                public string Text { get; set; }
            }

            public class CreateAuthor
            {
                public Guid Id { get; set; }
                public string FirstName { get; set; }

                public string LastName { get; set; }
            }

            public class UpdateAuthor
            {
                public Guid Id { get; set; }
                public string FirstName { get; set; }

                public string LastName { get; set; }
            }

            public class UpdateCoverImage
            {
                public Guid Id { get; set; }
                //public string FileName { get; set; }

                //public byte[] ImageData { get; set; }

                public IFormFile ImageFile { get; set; }
            }
        }
    }
}
