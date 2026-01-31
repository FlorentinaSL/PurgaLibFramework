using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibAPI.Features.Server;
using PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibEvent.Events.PluginManager;

namespace PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibLoader.PurgaLib_Loader.LoaderEvent
{
    public class PurgaLoader
    {
        public static readonly List<object> LoadedPlugins = new();

        private readonly string _purgaLibFolder = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "PurgaLib");

        private readonly string _pluginFolder;
        private readonly string _configRootFolder;

        public PurgaLoader()
        {
            _pluginFolder = Path.Combine(_purgaLibFolder, "Plugins");
            _configRootFolder = Path.Combine(_purgaLibFolder, "Config");

            Directory.CreateDirectory(_purgaLibFolder);
            Directory.CreateDirectory(_pluginFolder);
            Directory.CreateDirectory(_configRootFolder);

            Log.SendRaw("[PurgaLibFramework]: Loader Enabled.", ConsoleColor.Green);
        }

        public void LoadPlugins()
        {
            int serverPort = LabApi.Features.Wrappers.Server.Port;

            string globalPluginFolder = _pluginFolder;
            string globalConfigFolder = _configRootFolder;

            string serverPluginFolder = Path.Combine(_pluginFolder, serverPort.ToString());
            string serverConfigFolder = Path.Combine(_configRootFolder, serverPort.ToString());

            Directory.CreateDirectory(serverPluginFolder);
            Directory.CreateDirectory(serverConfigFolder);

            LoadFromFolder(globalPluginFolder, globalConfigFolder);
            LoadFromFolder(serverPluginFolder, serverConfigFolder);
        }

        private void LoadFromFolder(string pluginFolder, string configFolder)
        {
            var files = Directory.GetFiles(pluginFolder, "*.dll");

            foreach (var file in files)
            {
                try
                {
                    var raw = File.ReadAllBytes(file);
                    var assembly = Assembly.Load(raw);

                    var pluginTypes = assembly.GetTypes()
                        .Where(t => t.IsClass && !t.IsAbstract && IsPluginType(t));

                    foreach (var type in pluginTypes)
                    {
                        try
                        {
                            var pluginInstance = Activator.CreateInstance(type);
                            string pluginName = GetProperty(pluginInstance, "Name");

                            string pluginConfigFolder = Path.Combine(configFolder, pluginName);
                            Directory.CreateDirectory(pluginConfigFolder);

                            var requireProp = type.GetProperty("RequiredPurgaLibVersion");
                            if (requireProp == null || !typeof(Version).IsAssignableFrom(requireProp.PropertyType))
                                continue;

                            var requiredVersion = (Version)requireProp.GetValue(pluginInstance);
                            if (requiredVersion > new Version(2, 3, 0))
                                continue;

                            var configProperty = type.GetProperty("Config");
                            if (configProperty != null && typeof(IPurgaConfig).IsAssignableFrom(configProperty.PropertyType))
                            {
                                var configPath = Path.Combine(pluginConfigFolder, "config.yml");
                                var configValue = ConfigManager.LoadConfig(configProperty.PropertyType, configPath);
                                configProperty.SetValue(pluginInstance, configValue);
                            }

                            LoadedPlugins.Add(pluginInstance);

                            CommandLoader.RegisterCommands(pluginInstance);

                            bool enabled = true;
                            var enabledProp = configProperty?.PropertyType.GetProperty("Enabled");
                            if (enabledProp != null)
                            {
                                var cfg = configProperty.GetValue(pluginInstance);
                                if (enabledProp.GetValue(cfg) is bool b)
                                    enabled = b;
                            }

                            if (enabled)
                            {
                                var onEnabled = type.GetMethod("OnEnabled", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                                Log.SetPlugin(pluginName);
                                onEnabled?.Invoke(pluginInstance, null);
                                Log.ClearPlugin();
                            }

                            string pluginAuthor = GetProperty(pluginInstance, "Author");
                            string pluginVersion = GetProperty(pluginInstance, "Version");

                            Log.SendRaw($"[PurgaLibFramework] [{pluginName}] Loaded v{pluginVersion} by {pluginAuthor}", ConsoleColor.Cyan);
                        }
                        catch
                        {
                            // ignored
                        }
                    }
                }
                catch
                {
                    // ignored
                }
            }
        }

        private bool IsPluginType(Type type)
        {
            var baseType = type.BaseType;
            if (baseType == null) return false;
            if (!baseType.IsGenericType) return false;
            return baseType.GetGenericTypeDefinition() == typeof(Plugin<>);
        }

        private string GetProperty(object plugin, string propertyName)
        {
            var prop = plugin.GetType().GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance);
            var value = prop?.GetValue(plugin);
            return value?.ToString() ?? "(unknown)";
        }

        public void UnloadPlugins()
        {
            foreach (var plugin in LoadedPlugins)
            {
                try
                {
                    var type = plugin.GetType();
                    string pluginName = GetProperty(plugin, "Name");

                    var configProperty = type.GetProperty("Config");
                    if (configProperty != null)
                    {
                        var configValue = configProperty.GetValue(plugin);
                        var pluginConfigFolder = Path.Combine(_configRootFolder, pluginName);
                        Directory.CreateDirectory(pluginConfigFolder);

                        var configPath = Path.Combine(pluginConfigFolder, "config.yml");
                        ConfigManager.SaveConfig(configPath, configValue);
                    }

                    CommandLoader.UnregisterCommands(plugin);

                    var onDisabled = type.GetMethod("OnDisabled", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                    Log.SetPlugin(pluginName);
                    onDisabled?.Invoke(plugin, null);
                    Log.ClearPlugin();
                }
                catch
                {
                  //
                }
            }

            LoadedPlugins.Clear();
            Log.SendRaw("[PurgaLibFramework] All plugins disabled", ConsoleColor.DarkYellow);
        }

        public string PluginFolder => _pluginFolder;
        public string ConfigFolder => _configRootFolder;

        public void ListPlugins()
        {
            Log.SendRaw("[PurgaLibFramework] Plugins:", ConsoleColor.Cyan);
            foreach (var plugin in LoadedPlugins)
            {
                string pluginName = GetProperty(plugin, "Name");
                Log.SendRaw($" - {pluginName}", ConsoleColor.Cyan);
            }
        }
    }
}
