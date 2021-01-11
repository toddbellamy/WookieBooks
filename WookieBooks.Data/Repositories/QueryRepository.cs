using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WookieBooks.Data
{
    public class QueryRepository
    {
        protected readonly QueryContext context;

        public QueryRepository(QueryContext context)
        {
            this.context = context;
        }
    }
}
