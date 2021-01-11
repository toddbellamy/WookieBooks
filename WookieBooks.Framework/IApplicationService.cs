using System.Threading.Tasks;

namespace WookieBooks.DomainFramework
{
    public interface IApplicationService
    {
        Task Handle(object command);
    }
}
