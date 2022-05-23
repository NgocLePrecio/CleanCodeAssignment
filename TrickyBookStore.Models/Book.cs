using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// KeepIt
namespace TrickyBookStore.Models
{
    public class Book
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public double Price { get; set; }
        public bool IsOld { get; set; }
        public int CategoryId { get; set; }

        public BookCategory Category { get; set; }
    }
}
