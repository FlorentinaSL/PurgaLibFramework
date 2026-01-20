using System;
using HarmonyLib;
using LabApi.Loader.Features.Plugins;
using LabApi.Loader.Features.Plugins.Enums;
using PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibAPI.Features.Server;
using PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibEvent.Attribute;
using PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibEvent.Events.Handler;
using PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibLoader.PurgaLib_Loader.LoaderEvent;

namespace PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibLoader
{
    public class Loader : Plugin<Config>
    {
        private PurgaLoader _purgaLoader;
        public static Loader Instance { get; private set; }
        public Patcher Patcher { get; private set; }
        public override string Name { get; } = "PurgaLibLoader";
        public override string Description { get; } = "The loader of PurgaLib";
        public override string Author { get; } = "Florentina";
        public override Version Version { get; } = new Version(1, 8, 0);
        public override Version RequiredApiVersion { get; } = new Version(1, 0, 0, 0);
        public override LoadPriority Priority { get; } = LoadPriority.Lowest;

        public override void Enable()
        {
            Instance = this;
            PurgaUpdater.Initialize();
            PurgaUpdater.Instance.CheckUpdate();
            Harmony.DEBUG = true;
            
            Patch();
            PlayerHandler.Joined += PlayerHandler.OnJoined;
            PlayerHandler.Left += PlayerHandler.OnLeft;
            PlayerHandler.Verified += PlayerHandler.OnVerified;
            PlayerHandler.Hurting += PlayerHandler.OnHurting;
            PlayerHandler.Dying += PlayerHandler.OnDying;
            PlayerHandler.Died += PlayerHandler.OnDied;
            PlayerHandler.ChangingRole += PlayerHandler.OnChangingRole;
            PlayerHandler.ChangedRole += PlayerHandler.OnChangedRole;
            PlayerHandler.Spawning += PlayerHandler.OnSpawning;
            PlayerHandler.Spawned += PlayerHandler.OnSpawned;
            PlayerHandler.Banned += PlayerHandler.OnBanned;
            PlayerHandler.Kicked += PlayerHandler.OnKicked;
            PlayerHandler.Upgrading += PlayerHandler.OnUpgrading;
            PlayerHandler.ItemPickedUp += PlayerHandler.OnItemPickedUp;
            
            RoundHandler.Starting += RoundHandler.OnStarting;
            RoundHandler.Started += RoundHandler.OnStarted;
            RoundHandler.Ended += RoundHandler.OnEnded;
            RoundHandler.Restarting += RoundHandler.OnRestarting;
            
            DoorHandler.Interacting += DoorHandler.OnInteracting;
            ElevatorHandler.Interacting += ElevatorHandler.OnInteracting;
            //TeslaHandler.InteractingTesla += TeslaHandler.OnInteractingTesla;
            
            Log.SendRaw($"PurgaLibAPI Version {PurgaLibAPI.Version.version}", ConsoleColor.DarkRed);
            Log.SendRaw($"PurgaLib Version: {Version}", ConsoleColor.Red);
            Log.SendRaw(@" 
Welcome to:

██████╗ ██╗   ██╗██████╗  ██████╗  █████╗ ██╗     ██╗██████╗ 
██╔══██╗██║   ██║██╔══██╗██╔════╝ ██╔══██╗██║     ██║██╔══██╗
██████╔╝██║   ██║██████╔╝██║  ███╗███████║██║     ██║██████╔╝
██╔═══╝ ██║   ██║██╔══██╗██║   ██║██╔══██║██║     ██║██╔══██╗
██║     ╚██████╔╝██║  ██║╚██████╔╝██║  ██║███████╗██║██████╔╝
╚═╝      ╚═════╝ ╚═╝  ╚═╝ ╚═════╝ ╚═╝  ╚═╝╚══════╝╚═╝╚═════╝ [*] by Florentina
", ConsoleColor.DarkMagenta);
            
            _purgaLoader = new PurgaLoader();
            _purgaLoader.LoadPlugins();
        }
        

        public override void Disable()
        {
            Unpatch();
            
            PlayerHandler.Joined -= PlayerHandler.OnJoined;
            PlayerHandler.Left -= PlayerHandler.OnLeft;
            PlayerHandler.Verified -= PlayerHandler.OnVerified;
            PlayerHandler.Hurting -= PlayerHandler.OnHurting;
            PlayerHandler.Dying -= PlayerHandler.OnDying;
            PlayerHandler.Died -= PlayerHandler.OnDied;
            PlayerHandler.ChangingRole -= PlayerHandler.OnChangingRole;
            PlayerHandler.ChangedRole -= PlayerHandler.OnChangedRole;
            PlayerHandler.Spawning -= PlayerHandler.OnSpawning;
            PlayerHandler.Spawned -= PlayerHandler.OnSpawned;
            PlayerHandler.Banned -= PlayerHandler.OnBanned;
            PlayerHandler.Kicked -= PlayerHandler.OnKicked;
            PlayerHandler.Upgrading -= PlayerHandler.OnUpgrading;
            PlayerHandler.ItemPickedUp -= PlayerHandler.OnItemPickedUp;
            
            RoundHandler.Starting -= RoundHandler.OnStarting;
            RoundHandler.Started -= RoundHandler.OnStarted;
            RoundHandler.Ended -= RoundHandler.OnEnded;
            RoundHandler.Restarting += RoundHandler.OnRestarting;
            
            DoorHandler.Interacting += DoorHandler.OnInteracting;
            ElevatorHandler.Interacting += ElevatorHandler.OnInteracting;
            TeslaHandler.InteractingTesla += TeslaHandler.OnInteractingTesla;
            
            Instance = null;
            Log.SendRaw("Bye bye from PurgaLib", ConsoleColor.Cyan);
            _purgaLoader?.UnloadPlugins();
        }
        
        public void Patch()
        {
            try
            {
                Patcher = new Patcher();
                Patcher.PatchAll(!Config.UseDynamicPatch, out int failedPatches);
                if (failedPatches == 0)
                {
                    Log.SendRaw("PurgaLib Successfully Patched", ConsoleColor.Green);
                }
                else
                {
                    Log.SendRaw("PurgaLib Failed To Patch", ConsoleColor.Red);
                }
            }
            catch (Exception e)
            {
                Log.Error($"Patching fail\n{e}");
                throw;
            }
        }
    
        public void Unpatch()
        {
            try
            {
                Patcher.UnpatchAll();
                Patcher = null;
            }
            catch (Exception e)
            {
                Log.Error(e.ToString());
                throw;
            }
        }
    }
    
}
