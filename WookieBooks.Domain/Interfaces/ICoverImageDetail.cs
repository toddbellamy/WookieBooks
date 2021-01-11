﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WookieBooks.Domain.Interfaces
{
    public interface ICoverImageDetail
    {
        public string FileName { get; set; }
        public byte[] ImageData { get; set; }

    }
}
