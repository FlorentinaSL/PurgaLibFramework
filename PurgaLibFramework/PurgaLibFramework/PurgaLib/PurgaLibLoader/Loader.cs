using System;
using LabApi.Features.Console;
using LabApi.Loader.Features.Plugins;
using PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibLoader.PurgaLib_Loader.LoaderEvent;

namespace PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibLoader
{
    public class Loader : Plugin<Config>
    {
        private PurgaLoader _purgaLoader;
        public static Loader Instance { get; private set; }
        public override string Name { get; } = "PurgaLibLoader";
        public override string Description { get; } = "The loader of PurgaLibAPI";
        public override string Author { get; } = "PurgaLibTeam";
        public override Version Version { get; } = new Version(0, 0, 4);
        public override Version RequiredApiVersion { get; } = new Version(1,0,0,0);
        
        public override void Enable()
        {
            Instance = this;
            
            Logger.Raw($"PurgaLibAPI Version: {Version}", ConsoleColor.Red);
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
            Logger.Raw("Bye bye from PurgaLibAPI", ConsoleColor.Cyan);
            _purgaLoader.UnloadPlugins();
        }
    }
}