using System.Collections.Generic;
using TrickyBookStore.Models;

// KeepIt
namespace TrickyBookStore.Services.Books
{
    public interface IBookService
    {
        IList<Book> GetBooks(params long[] ids);
    }
}
