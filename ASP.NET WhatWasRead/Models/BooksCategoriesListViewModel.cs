using Domain.Concrete.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASP.NET_WhatWasRead.Models
{
    public class BooksListViewModel
    {
        public IEnumerable<Book> Books { get; set; }
        public PagingInfo PagingInfo { get; set; }
        public string CurrentCategory { get; set; }
        public string CurrentTag { get; set; }

    }
}