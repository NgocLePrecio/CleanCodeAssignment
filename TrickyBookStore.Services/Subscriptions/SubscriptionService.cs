using System;
using System.Collections.Generic;
using TrickyBookStore.Models;
using TrickyBookStore.Services.Store;
using System.Linq;
namespace TrickyBookStore.Services.Subscriptions
{
    public class SubscriptionService : ISubscriptionService
    {
        public IList<Subscription> GetSubscriptions(params int[] ids)
        {
            IList<Subscription> subscriptions = new List<Subscription>();

            foreach (int id in ids)
            {
                subscriptions.Add(Store.Subscriptions.Data.FirstOrDefault(s => s.Id == id));
            }

            return subscriptions;
        }
    }
}
