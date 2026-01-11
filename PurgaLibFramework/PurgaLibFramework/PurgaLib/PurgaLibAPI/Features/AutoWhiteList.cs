using System.Collections.Generic;
using System.Linq;
using PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibAPI.Core;
using PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibAPI.Features.Server;
using UnityEngine;

namespace PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibAPI.Features;

public sealed class AutoWhitelist : PActor
{
    private readonly HashSet<string> _whitelist = new();
    private bool _isActive;
    private const string KickReason = "Server is in whitelist mode";

    public void Enable()
    {
        if (_isActive) return;
        _isActive = true;
        KickNonWhitelistedPlayers();
        Log.Info("Server whitelist mode enabled");
    }

    public void Disable()
    {
        if (!_isActive) return;
        _isActive = false;
        Log.Info("Server whitelist mode disabled");
    }

    private void KickNonWhitelistedPlayers()
    {
        foreach (var player in Player.List.ToList())
        {
            if (!_whitelist.Contains(player.UserId))
            {
                player.Kick(KickReason);
                Log.Info($"Kicked {player.Nickname} ({player.UserId}) because server is whitelisted");
            }
        }
    }

    public void AddToWhitelist(string userId)
    {
        if (!_whitelist.Contains(userId))
        {
            _whitelist.Add(userId);
            Log.Info($"Added {userId} to whitelist");
        }
    }

    public void RemoveFromWhitelist(string userId)
    {
        if (_whitelist.Contains(userId))
        {
            _whitelist.Remove(userId);
            Log.Info($"Removed {userId} from whitelist");
        }
    }

    public bool IsWhitelisted(string userId) => _whitelist.Contains(userId);

    public override Transform Transform { get; } = null;
    public override bool IsAlive { get; } = false;
}

public static class WhitelistFeature
{
    private static readonly AutoWhitelist Core = StaticActor.Get<AutoWhitelist>();

    public static void Enable() => Core.Enable();
    public static void Disable() => Core.Disable();
    public static void AddUser(string userId) => Core.AddToWhitelist(userId);
    public static void RemoveUser(string userId) => Core.RemoveFromWhitelist(userId);
    public static bool IsUserWhitelisted(string userId) => Core.IsWhitelisted(userId);
}