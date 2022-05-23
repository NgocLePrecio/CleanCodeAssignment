using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject.Modules;
using TrickyBookStore.Models;
using TrickyBookStore.Services.Books;
using TrickyBookStore.Services.Customers;
using TrickyBookStore.Services.Payment;
using TrickyBookStore.Services.PurchaseTransactions;
using TrickyBookStore.Services.Subscriptions;

namespace TrickyBookStore.ConsoleApp 
{
    public class BookExportModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IPaymentService>().To<PaymentService>();
            Bind<IPurchaseTransactionService>().To<PurchaseTransactionService>();
            Bind<IBookService>().To<BookService>();
            Bind<ISubscriptionService>().To<SubscriptionService>();
            Bind<ICustomerService>().To<CustomerService>();
        }
    }
}
