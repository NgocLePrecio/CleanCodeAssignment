using System.Collections.Generic;
using TrickyBookStore.Models;

namespace TrickyBookStore.Services.Store
{
    public static class Customers
    {
        public static readonly IEnumerable<Customer> Data = new List<Customer>
        {
            new Customer{ Id = 1, SubscriptionIds = new List<int> { 1 } },
            new Customer{ Id = 2, SubscriptionIds = new List<int> { } },
            new Customer{ Id = 3, SubscriptionIds = new List<int> { 3 } },
            new Customer{ Id = 4, SubscriptionIds = new List<int> { 6 } },
            new Customer{ Id = 5, SubscriptionIds = new List<int> { 1, 5, 4 } },
            new Customer{ Id = 6, SubscriptionIds = new List<int> { 1, 3, 5, 4 } },
            new Customer{ Id = 7, SubscriptionIds = new List<int> { 4 } },
            new Customer{ Id = 8, SubscriptionIds = new List<int> { 1,3,4,5 } },
        };
    }
}
