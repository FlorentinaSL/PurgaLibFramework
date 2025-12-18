<!DOCTYPE html>
<html lang="en">
<head>
<meta charset="UTF-8">
<meta name="viewport" content="width=device-width, initial-scale=1.0">
<link rel="stylesheet" href="style.css">
</head>
<body>
<div class="container">

<h1 align="center">🔥 PurgaLib Framework</h1>

<div align="center">
<img src="https://img.shields.io/badge/Status-ALPHA-orange?style=for-the-badge&logo=testinglibrary"/>
<img src="https://img.shields.io/badge/Platform-SCP:_Secret_Laboratory-black?style=for-the-badge"/>
<img src="https://img.shields.io/badge/Language-C%23-239120?style=for-the-badge&logo=csharp&logoColor=white"/>
<img src="https://img.shields.io/badge/Build-Passing-brightgreen?style=for-the-badge&logo=githubactions"/>
</div>

<hr>

<h2>🌟 About</h2>
<p>PurgaLib is a lightweight, modular, developer-friendly framework for <strong>SCP: Secret Laboratory</strong>.<br>
Designed to be fast, clean, and event-driven.</p>

<hr>

<h2>🛠️ Plugin Creation Tutorial</h2>

<div class="step">
<h3>1️⃣ Add PurgaLibFramework.dll</h3>
<p>Include <code>PurgaLibFramework.dll</code> in your C# plugin project.<br>
Place it in <strong>LabAPI/plugins/Global</strong>.</p>
</div>

<div class="step">
<h3>2️⃣ Create Config.cs</h3>
<pre><code class="language-csharp">
using PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibEvent.Events.PluginManager;

namespace ExamplePurgaLib
{
    public class Config : IPurgaConfig
    {
        public bool Enabled { get; set; } = true;
    }
}
</code></pre>
</div>

<div class="step">
<h3>3️⃣ Create Plugin.cs</h3>
<pre><code class="language-csharp">
using System;
using PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLib_API.Server;
using PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibEvent.Events.PluginManager;

namespace ExamplePurgaLib
{
    public class Main : Plugin<Config>
    {
        public override string Name { get; } = "Example Plugin";
        public override string Author { get; } = "Florentina<3";
        public override string Description { get; } = "Test plugin for PurgaLib";
        public override Version Version { get; } = new Version(0,0,1);
        public override Version RequiredPurgaLibVersion { get; } = new Version(0,0,1);
        
        protected override void OnEnabled()
        {
            Log.Info("Hi from Plugin PurgaLib!");
        }

        protected override void OnDisabled()
        {
            Log.Info("Bye from Plugin PurgaLib!");
        }
    }
}
</code></pre>
</div>

<div class="step">
<h3>4️⃣ Add Your Logic</h3>
<p>Implement events, commands, or any custom features for your plugin. Use PurgaLib's API for interacting with the server.</p>
</div>

<div class="step notes">
<h3>💡 Notes</h3>
<ul>
<li>✔ All core events are implemented</li>
<li>✔ For personal or server use only</li>
<li>❌ Do not rebrand or redistribute</li>
</ul>
</div>

</div> <!-- container -->
</body>
</html>
