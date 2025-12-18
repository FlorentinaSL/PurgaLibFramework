<h2 align="center">🛠️ Plugin Creation Tutorial</h2>

<p align="center">
Follow these steps to create a plugin using <strong>PurgaLib Framework</strong> v0.0.1.
</p>

<div style="background-color:#1f1f1f; border-left:5px solid #ff6f00; border-radius:8px; padding:20px; margin:20px 0;">
  <h3>1️⃣ Add PurgaLibFramework.dll <span style="background:#ff6f00; color:#fff; padding:2px 8px; border-radius:4px; font-size:0.9em;">Step</span></h3>
  <p>Include <code>PurgaLib.dll</code> in your C# plugin project.<br>
  Place it in <strong>LabAPI/plugins/Global</strong>.</p>
</div>

<div style="background-color:#1f1f1f; border-left:5px solid #00bcd4; border-radius:8px; padding:20px; margin:20px 0;">
  <h3>2️⃣ Create <code>Config.cs</code> <span style="background:#00bcd4; color:#fff; padding:2px 8px; border-radius:4px; font-size:0.9em;">Step</span></h3>
  <pre style="background-color:#262626; color:#fff; padding:15px; border-radius:5px; overflow-x:auto;">
using PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibEvent.Events.PluginManager;

namespace ExamplePurgaLib
{
    public class Config : IPurgaConfig
    {
        public bool Enabled { get; set; } = true;
    }
}
  </pre>
</div>

<div style="background-color:#1f1f1f; border-left:5px solid #4caf50; border-radius:8px; padding:20px; margin:20px 0;">
  <h3>3️⃣ Create <code>Plugin.cs</code> <span style="background:#4caf50; color:#fff; padding:2px 8px; border-radius:4px; font-size:0.9em;">Step</span></h3>
  <pre style="background-color:#262626; color:#fff; padding:15px; border-radius:5px; overflow-x:auto;">
using System;
using PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLib_API.Server;
using PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibEvent.Events.PluginManager;

namespace ExamplePurgaLib
{
    public class Main : Plugin&lt;Config&gt;
    {
        public override string Name { get; } = "Example Plugin";
        public override string Author { get; } = "Florentina&lt;3";
        public override string Description { get; } = "Test";
        public override Version Version { get; } = new Version(0,0,1);
        public override Version RequiredPurgaLibVersion { get; } = new Version(0,0,1);
        
        protected override void OnEnabled()
        {
            Log.Info("Hi from Plugin PurgaLib");
        }

        protected override void OnDisabled()
        {
            Log.Info("Bye from Plugin PurgaLib");
        }
    }
}
  </pre>
</div>

<div style="background-color:#1f1f1f; border-left:5px solid #ff5722; border-radius:8px; padding:20px; margin:20px 0;">
  <h3>4️⃣ Add Your Logic <span style="background:#ff5722; color:#fff; padding:2px 8px; border-radius:4px; font-size:0.9em;">Step</span></h3>
  <p>Implement events, commands, or any custom features for your plugin.<br>
  Use PurgaLib's API for interacting with the server.</p>
</div>

<div style="background-color:#1f1f1f; border-left:5px solid #9c27b0; border-radius:8px; padding:20px; margin:20px 0;">
  <h3>💡 Notes <span style="background:#9c27b0; color:#fff; padding:2px 8px; border-radius:4px; font-size:0.9em;">Important</span></h3>
  <ul>
    <li>✔ All core events are implemented</li>
    <li>✔ For personal or server use only</li>
    <li>❌ Do not rebrand or redistribute</li>
  </ul>
</div>
