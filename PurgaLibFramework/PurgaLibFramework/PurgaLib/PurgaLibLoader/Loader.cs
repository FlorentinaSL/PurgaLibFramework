using System;
using LabApi.Features.Console;
using LabApi.Loader.Features.Plugins;
using LabApi.Loader.Features.Plugins.Enums;
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
        public override Version Version { get; } = new Version(0, 1, 4);
        public override Version RequiredApiVersion { get; } = new Version(1,0,0,0);
        public override LoadPriority Priority { get; } = LoadPriority.Lowest;

        public override void Enable()
        {
            DoorHandler.RegisterLabApi();
            ElevatorHandler.RegisterLabApi();
            PlayerHandler.RegisterLabApi();
            RoundHandler.RegisterLabApi();
            TeslaHandler.RegisterLabApi();
            
            CustomRoleHandler.RegisterEvents();
            CustomItemHandler.RegisterEvents();
            
            Instance = this;
            
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