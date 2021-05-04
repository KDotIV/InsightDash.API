using System;
using System.Collections.Generic;

namespace InsightDash.API
{
    public class Helpers
    {
        private static Random rand = new Random();
        private static string GetRandom(IList<string> items)
        {
            return items[rand.Next(items.Count)];
        }
        internal static string MakeUniqueCustomerName(List<string> names)
        {
            var maxNames = bizPrefix.Count * bizSuffix.Count;
            if(names.Count >= maxNames)
            {
                throw new System.InvalidOperationException("Maximum number of unique names exceeded");
            }
            var prefix = GetRandom(bizPrefix);
            var suffix = GetRandom(bizSuffix);
            var bizName = prefix + suffix;

            if(names.Contains(bizName))
            {
                MakeUniqueCustomerName(names);
            }
            return bizName;
        }
        internal static string MakeCustomerEmail(string customerName)
        {
            return $"contact@{customerName.ToLower()}.com";
        }
        internal static string GetRandomState()
        {
            return GetRandom(usStates);
        }
        internal static decimal GetRandomOrderTotal()
        {
            return rand.Next(100, 5000);
        }
        internal static DateTime GetRandomOrderPlaced()
        {
            var end = DateTime.Now;
            var startDate = end.AddDays(-90);

            TimeSpan possibleSpan = end - startDate;
            TimeSpan newSpan = new TimeSpan(0, rand.Next(0, (int)possibleSpan.TotalMinutes), 0);

            return startDate + newSpan;
        }
        internal static DateTime? GetRandomOrderCompleted(DateTime orderPlaced)
        {
            var now = DateTime.Now;
            var minLeadTime = TimeSpan.FromDays(7);
            var timePassed = now - orderPlaced;

            if(timePassed < minLeadTime)
            {
                return null;
            }

            return orderPlaced.AddDays(rand.Next(7, 14));
        }
        private static readonly List<string> usStates = new List<string>()
        {
            "AK", "AL", "AR", "AS", "AZ", "CA", "CO", "CT",
            "DC", "DE", "FL", "GA", "GU", "HI", "IA", "ID",
            "IL", "IN", "KS", "KY", "LA", "MA", "MD", "ME",
            "MI", "MN", "MO", "MP", "MS", "MT", "NC", "ND",
            "NE", "NH", "NJ", "NM", "NV", "NY", "OH", "OK",
            "OR", "PA", "PR", "RI", "SC", "SD", "TN", "TX",
            "UM", "UT", "VA", "VI", "VT", "WA", "WI", "WV", "WY"
        };
        private static readonly List<string> bizPrefix = new List<string>()
        {
            "ABC",
            "XYZ",
            "MainSt",
            "Sales",
            "Enterprise",
            "Ready",
            "Quick",
            "Budget"
        };
        private static readonly List<string> bizSuffix = new List<string>()
        {
            "Corporation",
            "Corpo",
            "Co",
            "Logistics",
            "Transit",
            "Goods",
            "Foods",
            "Automotives"
        };
    }
}