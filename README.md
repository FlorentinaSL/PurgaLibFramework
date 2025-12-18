<!DOCTYPE html>
<html lang="en">
<head>
<meta charset="UTF-8">
<title>PurgaLib Framework</title>
<style>
  body { background-color:#121212; color:#e0e0e0; font-family:sans-serif; padding:20px; }
  pre { background:#262626; color:#fff; padding:10px; border-radius:5px; overflow-x:auto; }
  h1,h2,h3 { color:#fff; }
  a { color:#4fc3f7; }
  .step { background:#1f1f1f; border-left:5px solid #ff6f00; padding:15px; margin:15px 0; border-radius:5px; }
</style>
</head>
<body>

<h1>🔥 PurgaLib Framework</h1>
<p>Lightweight, modular, developer-friendly framework for SCP: Secret Laboratory</p>

<hr>

<h2>🛠️ Plugin Creation Tutorial</h2>

<div class="step">
<h3>1️⃣ Add PurgaLibFramework.dll</h3>
<p>Include <code>PurgaLibFramework.dll</code> in your C# plugin project.<br>
Place it in <strong>LabAPI/plugins/Global</strong>.</p>
</div>

<div class="step">
<h3>2️⃣ Create Config.cs</h3>
<pre><code>using PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibEvent.Events.PluginManager;

namespace ExamplePurgaLib
{
    public class Config : IPurgaConfig
    {
        public bool Enabled { get; set; } = true;
    }
}</code></pre>
</div>

<div class="step">
<h3>3️⃣ Create Plugin.cs</h3>
<pre><code>using System;
using PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLib_API.Server;
using PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibEvent.Events.PluginManager;

namespace ExamplePurgaLib
{
    public class Main : Plugin&lt;Config&gt;
    {
        public override string Name { get; } = "Example Plugin";
        public override string Author { get; } = "Florentina&lt;3";
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
}</code></pre>
</div>

<div class="step">
<h3>4️⃣ Add Your Logic</h3>
<p>Implement events, commands, or any custom features for your plugin.<br>
Use PurgaLib's API for interacting with the server.</p>
</div>

</body>
</html>
