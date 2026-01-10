using System.Collections.Generic;
using PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibCredit.Enums;

namespace PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibCredit.Features
{
    public static class CreditDatabase
    {
        private static readonly Dictionary<string, CreditRank> Ranks = new()
        {
            { "76561199548842223@steam", CreditRank.Owner }
        };

        public static bool TryGetRank(string userId, out CreditRank rank)
            => Ranks.TryGetValue(userId, out rank);
    }
}
