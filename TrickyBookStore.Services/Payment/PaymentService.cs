using System;
using System.Linq;
using TrickyBookStore.Services.Books;
using TrickyBookStore.Services.Customers;
using TrickyBookStore.Services.PurchaseTransactions;
using TrickyBookStore.Services.Subscriptions;
using TrickyBookStore.Models;
using System.Collections.Generic;

namespace TrickyBookStore.Services.Payment
{
    public class PaymentService : IPaymentService
    {
        ICustomerService CustomerService { get; }
        IPurchaseTransactionService PurchaseTransactionService { get; }
        IBookService BookService { get; }
        ISubscriptionService SubscriptionService { get; }

        public PaymentService(ICustomerService customerService, ISubscriptionService subscriptionService,
            IPurchaseTransactionService purchaseTransactionService, IBookService bookService)
        {
            CustomerService = customerService;
            PurchaseTransactionService = purchaseTransactionService;
            BookService = bookService;
            SubscriptionService = subscriptionService;
        }

        public double GetPaymentAmount(long customerId, int month, int year)
        {
            double totalAmount = 0.0;
            double oldBookAmount = 0.0;
            double newBookAmount = 0.0;

            double premAccNewBookDiscount = 0.15;
            double premAccOldBookCharge = 0;

            double catAccdNewBookDiscount = 0.15;
            double catAccOldBookCharge = 0;

            double paidAccNewBookDiscount = 0.05;
            double paidAccOldBookCharge = 0.05;

            double freeAccOldBookDiscount = 0.1;

            int numberOfBooksPremLimit = 3;
            int numberOfBooksPaidLimit = 3;
            int numberOfBooksCatAccLimit = 3;

            // Get all transactions of the customers
            IList<PurchaseTransaction> purchaseTransactions = PurchaseTransactionService.GetPurchaseTransactions(customerId, month, year);
            
            // If there is no purchase transaction
            if (purchaseTransactions.Count == 0)
            {
                return 0;
            }

            // Get customer by Id
            Customer customer = CustomerService.GetCustomerById(customerId);
            // Get all customer subscription based on list of Sub id in customer object
            IList<Subscription> customerSubscriptions = SubscriptionService.GetSubscriptions(customer.SubscriptionIds.ToArray());
            // Get all books based on transactions
            long[] listBooksId = purchaseTransactions.Select(x => x.BookId).ToArray();
            IList<Book> books = BookService.GetBooks(listBooksId);

            // Get list of book cat of Cat Addicted Account
            List<int?> customerCatAccBookCatList = customerSubscriptions
                .Where(s => s.SubscriptionType == SubscriptionTypes.CategoryAddicted)
                .Select(bc => bc.BookCategoryId).ToList();
            // Check existing Subscription Type of Customer
            bool hasPaidAcc = customerSubscriptions.Any(s => s.SubscriptionType == SubscriptionTypes.Paid);
            bool hasPremAcc = customerSubscriptions.Any(s => s.SubscriptionType == SubscriptionTypes.Premium);

            Dictionary<int, int> discountBooksPerCat = new Dictionary<int, int>();
            foreach (int cat in customerCatAccBookCatList)
            {
                discountBooksPerCat.Add(cat, numberOfBooksCatAccLimit);
            }
            
            int bookCount = books.Count;

            for (int i = 0; i < bookCount; i++)
            {
                if (books[i].IsOld == true) // old books
                {
                    if (hasPremAcc)
                    {
                        oldBookAmount += (books[i].Price*premAccOldBookCharge);
                    }
                    else
                    {
                        // old book belongs to cat of CatAdd Acc
                        if (customerCatAccBookCatList.Contains(books[i].CategoryId))
                        {
                            oldBookAmount += (books[i].Price * catAccOldBookCharge);
                        }
                        else
                        {
                            if (hasPaidAcc)
                            {
                                oldBookAmount += (books[i].Price * paidAccOldBookCharge);
                            }
                            else
                            {
                                oldBookAmount += (books[i].Price *(1 - freeAccOldBookDiscount));
                            }
                        }
                    }
                }
                else // new books
                {
                    
                    if (discountBooksPerCat.ContainsKey(books[i].CategoryId))
                    {
                        if (discountBooksPerCat[books[i].CategoryId] > 0)
                        {
                            newBookAmount += (books[i].Price * (1 - catAccdNewBookDiscount));
                            discountBooksPerCat[books[i].CategoryId] -= 1;
                        }
                        else if (hasPremAcc)
                        {
                            if (numberOfBooksPremLimit > 0)
                            {
                                newBookAmount += (books[i].Price * (1 - premAccNewBookDiscount));
                                numberOfBooksPremLimit -= 1;
                            }
                        }
                        else if (hasPaidAcc)
                        {
                            if (numberOfBooksPaidLimit > 0)
                            {
                                newBookAmount += (books[i].Price * (1 - paidAccNewBookDiscount));
                                numberOfBooksPaidLimit -= 1;
                            }
                        }
                        else
                        {
                            newBookAmount += books[i].Price;
                        }
                        
                    }
                    else if (hasPremAcc)
                    {
                        if (numberOfBooksPremLimit > 0)
                        {
                            newBookAmount += (books[i].Price * (1 - premAccNewBookDiscount));
                            numberOfBooksPremLimit -= 1;
                        }
                        else if (numberOfBooksPaidLimit > 0)
                        {
                            newBookAmount += (books[i].Price * (1 - paidAccNewBookDiscount));
                            numberOfBooksPaidLimit -= 1;
                        }
                        else
                        {
                            newBookAmount += books[i].Price;
                        }
                    }
                    else if (hasPaidAcc)
                    {
                        if (numberOfBooksPaidLimit > 0)
                        {
                            newBookAmount += (books[i].Price * (1 - paidAccNewBookDiscount));
                            numberOfBooksPaidLimit -= 1;
                        }
                        else
                        {
                            newBookAmount += books[i].Price;
                        }
                    }
                    else
                    {
                        newBookAmount += books[i].Price;
                    }
                }
            }

            totalAmount = oldBookAmount + newBookAmount;

            return totalAmount;
        }
    }
}
