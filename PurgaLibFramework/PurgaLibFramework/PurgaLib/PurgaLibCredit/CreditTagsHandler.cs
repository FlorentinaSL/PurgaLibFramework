using LabApi.Features.Wrappers;
using PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibCredit.Features;

namespace PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibCredit
{
    public static class CreditVerifiedHandler
    {
        public static void Handle(Player player)
        {
            if (player == null)
                return;

            if (!CreditDatabase.TryGetRank(player.UserId, out var rank))
                return;

            string text = CreditRankFormatter.ToDisplay(rank);
            if (string.IsNullOrEmpty(text))
                return;

            var roles = player.ReferenceHub.serverRoles;
            roles.GlobalHidden = true;
            roles.SetText(text);
        }
    }
}
