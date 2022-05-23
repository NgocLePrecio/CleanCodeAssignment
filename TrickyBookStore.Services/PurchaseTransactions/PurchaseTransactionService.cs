using System;
using System.Collections.Generic;
using TrickyBookStore.Models;
using TrickyBookStore.Services.Books;
using System.Linq;

namespace TrickyBookStore.Services.PurchaseTransactions
{
    public class PurchaseTransactionService : IPurchaseTransactionService
    {
        IBookService BookService { get; }

        public PurchaseTransactionService(IBookService bookService)
        {
            BookService = bookService;
        }

        public IList<PurchaseTransaction> GetPurchaseTransactions(long customerId, int month, int year)
        {
            IEnumerable<PurchaseTransaction> purchaseTransactions = Store.PurchaseTransactions.Data;

            return purchaseTransactions.Where(p => p.CustomerId == customerId && 
                (p.CreatedDate.Month == month) && (p.CreatedDate.Year == year)).OrderBy(x => x.CreatedDate).ToList();
        }
    }
}
