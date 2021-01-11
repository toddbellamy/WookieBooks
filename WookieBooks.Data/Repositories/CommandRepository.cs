using System.Threading.Tasks;

namespace WookieBooks.Data
{
    public class CommandRepository
    {
        protected readonly CommandContext context;

        public CommandRepository(CommandContext context)
            => this.context = context;

        public virtual async ValueTask Commit()
            => await context.SaveChangesAsync();
    }
}
