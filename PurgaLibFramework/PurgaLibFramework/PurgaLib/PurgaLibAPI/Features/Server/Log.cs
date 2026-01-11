using System;
using System.Threading;
using LabApi.Features.Console;

namespace PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibAPI.Features.Server
{
    public static class Log 
    {
        private static readonly AsyncLocal<string> CurrentPlugin = new();

        internal static void SetPlugin(string pluginName)
        {
            CurrentPlugin.Value = pluginName;
        }

        internal static void ClearPlugin()
        {
            CurrentPlugin.Value = null;
        }

        private static string Prefix => CurrentPlugin.Value != null ? $"[{CurrentPlugin.Value}] " : "";

        public static void Info(string message)
        {
            Logger.Info($"{Prefix}{message}");
        }

        public static void Success(string message)
        {
            Logger.Raw($"{Prefix}{message}", ConsoleColor.Green);
        }

        public static void Warn(string message)
        {
            Logger.Warn($"{Prefix}{message}");
        }

        public static void Error(string message)
        {
            Logger.Error($"{Prefix}{message}");
        }

        public static void Debug(string message)
        {
            Logger.Debug($"{Prefix}{message}");
        }
    }
}
