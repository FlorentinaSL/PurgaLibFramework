using PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibCredit.Enums;

namespace PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibCredit.Features
{
    public static class CreditRankFormatter
    {
        public static string ToDisplay(CreditRank rank) => rank switch
        {
            CreditRank.Owner       => "PurgaLibFramework Owner",
            CreditRank.Developer   => "PurgaLibFramework Dev",
            CreditRank.Contributor => "PurgaLibFramework Contributor",
            _ => null
        };
    }
}
