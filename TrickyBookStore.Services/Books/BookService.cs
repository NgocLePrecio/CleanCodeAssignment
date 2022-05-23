using System;
using System.Collections.Generic;
using TrickyBookStore.Models;
using System.Linq;

namespace TrickyBookStore.Services.Books
{
    public class BookService : IBookService
    {
        public IList<Book> GetBooks(params long[] ids)
        {
            List<Book> books = new List<Book>();

            foreach (long id in ids)
            {
                books.Add(Store.Books.Data.FirstOrDefault(b => b.Id == id));
            }
            return books;
        }
    }
}
