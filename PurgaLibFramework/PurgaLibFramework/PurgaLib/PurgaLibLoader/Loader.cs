using System;
using LabApi.Features.Console;
using LabApi.Loader.Features.Plugins;
using LabApi.Loader.Features.Plugins.Enums;
using PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibAPI.Features.Server;
using PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibCustomItems.Handlers;
using PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibCustomRoles.Handlers;
using PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibEvent.Events.Handler;
using PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibLoader.PurgaLib_Loader.LoaderEvent;

namespace PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibLoader
{
    public class Loader : Plugin<Config>
    {
        private PurgaLoader _purgaLoader;
        public static Loader Instance { get; private set; }
        public override string Name { get; } = "PurgaLibLoader";
        public override string Description { get; } = "The loader of PurgaLib";
        public override string Author { get; } = "Florentina";
        public override Version Version { get; } = new Version(1, 6, 0);
        public override Version RequiredApiVersion { get; } = new Version(1, 0, 0, 0);
        public override LoadPriority Priority { get; } = LoadPriority.Lowest;

        public override void Enable()
        {
            Instance = this;
            PurgaUpdater.Initialize();
            PurgaUpdater.Instance.CheckUpdate();
            
            DoorHandler.Initialize();
            Log.Success("[PurgaLib] DoorHandler registered successfully.");
            ElevatorHandler.Initialize();
            Log.Success("[PurgaLib] ElevatorHandler registered successfully.");
            PlayerHandler.Initialize();
            Log.Success("[PurgaLib] PlayerHandler registered successfully");
            RoundHandler.Initialize();
            Log.Success("[PurgaLib] RoundHandler registered successfully");
            TeslaHandler.Initialize();
            Log.Success("[PurgaLib] TeslaHandler registered successfully");
            CustomItemHandler.RegisterEvents();
            Log.Success("[PurgaLib] CustomItemHandler events registered.");
            CustomRoleHandler.RegisterEvents();
            Log.Success("[PurgaLib] CustomRoleHandler events registered.");
            
            Logger.Raw($"PurgaLibAPI Version {PurgaLibAPI.Version.version}", ConsoleColor.DarkRed);
            Logger.Raw($"PurgaLib Version: {Version}", ConsoleColor.Red);
            Logger.Raw(@" 
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
            Instance = null;
            Logger.Raw("Bye bye from PurgaLib", ConsoleColor.Cyan);
            _purgaLoader?.UnloadPlugins();
        }
    }
}
