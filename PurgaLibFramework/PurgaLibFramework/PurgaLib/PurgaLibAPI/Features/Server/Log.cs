using System;
using System.Reflection;
using System.Threading;
using Discord;

namespace PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibAPI.Features.Server
{
    public static partial class Log
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
        public static void Info(object message) => Send($"[{Assembly.GetCallingAssembly().GetName().Name}] {message}",
            LogLevel.Info, ConsoleColor.Cyan);
        public static void Info(string message) => Send($"[{Assembly.GetCallingAssembly().GetName().Name}] {message}",
            LogLevel.Info, ConsoleColor.Cyan);

        public static void Warn(object message) => Send($"[{Assembly.GetCallingAssembly().GetName().Name}] {message}",
            LogLevel.Warn, ConsoleColor.Yellow);
        public static void Warn(string message) => Send($"[{Assembly.GetCallingAssembly().GetName().Name}] {message}",
            LogLevel.Warn, ConsoleColor.Yellow);

        public static void Error(object message) => Send($"[{Assembly.GetCallingAssembly().GetName().Name}] {message}",
            LogLevel.Error, ConsoleColor.DarkRed);
        public static void Error(string message) => Send($"[{Assembly.GetCallingAssembly().GetName().Name}] {message}",
            LogLevel.Error, ConsoleColor.DarkRed);

        public static void Debug(object message, bool debugEnabled = true)
        {
            if (!debugEnabled) return;
            Send($"[{Assembly.GetCallingAssembly().GetName().Name}] {message}", LogLevel.Debug, ConsoleColor.Magenta);
        }

        public static void Debug(string message, bool debugEnabled = true)
        {
            if (!debugEnabled) return;
            Send($"[{Assembly.GetCallingAssembly().GetName().Name}] {message}", LogLevel.Debug, ConsoleColor.Magenta);
        }
        
        public static void Success(object message) =>
            Send($"[{Assembly.GetCallingAssembly().GetName().Name}] {message}", LogLevel.Info, ConsoleColor.Green);
        public static void Success(string message) =>
            Send($"[{Assembly.GetCallingAssembly().GetName().Name}] {message}", LogLevel.Info, ConsoleColor.Green);

        public static void Send(string message, LogLevel level, ConsoleColor color = ConsoleColor.DarkGray)
        {
            SendRaw($"[{level.ToString().ToUpper()}] {message}", color);
        }
        public static void SendRaw(object message, ConsoleColor color) => ServerConsole.AddLog(message.ToString(), color);
    }
}
