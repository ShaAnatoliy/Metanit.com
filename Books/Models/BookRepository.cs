using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Books.Models
{
    public class BookRepository : IDisposable, IBookRepository
    {
        private BookContext db = new BookContext();

        public IEnumerable<Book> GetAll()
        {
            return db.Books;
        }
        public Book GetByID(int id)
        {
            return db.Books.FirstOrDefault(p => p.Id == id);
        }
        public void Add(Book book)
        {
            db.Books.Add(book);
            db.SaveChanges();
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