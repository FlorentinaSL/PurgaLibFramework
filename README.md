<h1 align="center">🔥 PurgaLib Framework</h1>

<p align="center">
  <img src="https://img.shields.io/badge/Status-ALPHA-orange?style=for-the-badge&logo=testinglibrary" alt="ALPHA">
  <img src="https://img.shields.io/badge/Platform-SCP:_Secret_Laboratory-black?style=for-the-badge" alt="Platform">
  <img src="https://img.shields.io/badge/Language-C%23-239120?style=for-the-badge&logo=csharp&logoColor=white" alt="C#">
  <img src="https://img.shields.io/badge/Build-Passing-brightgreen?style=for-the-badge&logo=githubactions" alt="Build">
</p>

---

<h2 align="center">🌟 About</h2>
<p align="center">
PurgaLib is a lightweight, modular and developer-friendly framework for <strong>SCP: Secret Laboratory</strong>.<br>
Designed to be fast, clean, and event-driven.
</p>

---

<h2 align="center">🛠️ Plugin Creation Tutorial</h2>

<div style="background-color:#1f1f1f; border-left:5px solid #ff6f00; border-radius:8px; padding:20px; margin:20px 0;">
  <h3>1️⃣ Add PurgaLib.dll</h3>
  <p>Include <code>PurgaLib.dll</code> in your C# plugin project.<br>
  Place it in <strong>LabAPI/plugins/Global</strong>.</p>
</div>

<div style="background-color:#1f1f1f; border-left:5px solid #00bcd4; border-radius:8px; padding:20px; margin:20px 0;">
  <h3>2️⃣ Create <code>Config.cs</code></h3>
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
  <h3>3️⃣ Create <code>Plugin.cs</code></h3>
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
  <h3>4️⃣ Add Your Logic</h3>
  <p>Implement events, commands, or custom features for your plugin.<br>
  Use PurgaLib's API for server interaction.</p>
</div>

<div style="background-color:#1f1f1f; border-left:5px solid #9c27b0; border-radius:8px; padding:20px; margin:20px 0;">
  <h3>💡 Notes</h3>
  <ul>
    <li>✔ All core events are implemented</li>
    <li>✔ For personal or server use only</li>
    <li>❌ Do not rebrand or redistribute</li>
  </ul>
</div>

---

<h2 align="center">📜 License & Usage</h2>
<p align="center">
This project is protected by <strong>Copyright</strong>.<br>
✔ Personal or server use allowed<br>
❌ Rebranding or redistribution not allowed
</p>
