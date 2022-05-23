using System.Collections.Generic;
using TrickyBookStore.Models;

namespace TrickyBookStore.Services.Subscriptions
{
    public interface ISubscriptionService
    {
        IList<Subscription> GetSubscriptions(params int[] ids);
    }
}
