using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace DepInject.Models
{
    public class BookContext : DbContext
    {
        public BookContext() : base("BookStore") { }
        public DbSet<Book> Books { get; set; }
    }

    public class BookRepository : IDisposable
    {
        private BookContext db = new BookContext();

        public void Save(Book b)
        {
            db.Books.Add(b);
            db.SaveChanges();
        }
        public IEnumerable<Book> List()
        {
            return db.Books;
        }
        public Book Get(int id)
        {
            return db.Books.Find(id);
        }

        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (db != null)
                {
                    db.Dispose();
                    db = null;
                }
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}