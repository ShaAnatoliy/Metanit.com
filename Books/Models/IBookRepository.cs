using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Models
{
    public interface IBookRepository
    {
        IEnumerable<Book> GetAll();
        Book GetByID(int id);
        void Add(Book book);
    }

}
