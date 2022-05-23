using System;
using System.Collections.Generic;
using System.Text;

namespace TrickyBookStore.Models
{
    public class Subscription
    {
        public int Id { get; set; }
        public SubscriptionTypes SubscriptionType { get; set; }
        public int Priority { get; set; }
        public int? BookCategoryId { get; set; }
        public Dictionary<string, double> PriceDetails { get; set; }
    }
}
