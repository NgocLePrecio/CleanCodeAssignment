using System;
using TrickyBookStore.Models;
using TrickyBookStore.Services.Subscriptions;
using System.Linq;

namespace TrickyBookStore.Services.Customers
{
    public class CustomerService : ICustomerService
    {
        ISubscriptionService SubscriptionService { get; }

        public CustomerService(ISubscriptionService subscriptionService)
        {
            SubscriptionService = subscriptionService;
        }

        public Customer GetCustomerById(long id)
        {
            Customer customer = Store.Customers.Data.FirstOrDefault(c => c.Id == id);
            if (customer != null)
            {
                customer.Subscriptions = SubscriptionService.GetSubscriptions(customer.SubscriptionIds.ToArray());
            }
            return customer;
        }
    }
}
