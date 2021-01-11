using System.Collections.Generic;
using System.Threading.Tasks;

namespace WookieBooks.Domain.Interfaces
{
    public interface IBookQueryRepository
    {
        public Task<IEnumerable<IBookListItem>> Query(IGetBooks query);

        public Task<IBookDetail> Query(IGetBook query);

        public Task<IEnumerable<IBookListItem>> Query(IGetAuthorBooks query);

        public Task<IEnumerable<IAuthorListItem>> Query(IGetAuthors query);

        public Task<ICoverImageDetail> Query(IGetCoverImage query);
    }
}
