using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WookieBooks.Domain.Interfaces;

namespace WookieBooks.Data
{
    public class CoverImageDetail : ICoverImageDetail
    {
        public string FileName { get; set; }
        public byte[] ImageData { get; set; }

    }
}
