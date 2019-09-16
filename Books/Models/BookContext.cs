using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Books.Models
{
    public class BookContext : DbContext
    {
        public BookContext() : base("BookStore") { }
        public DbSet<Book> Books { get; set; }

    }
}