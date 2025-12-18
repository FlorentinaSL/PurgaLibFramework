<!DOCTYPE html>
<html lang="en">
<head>
<meta charset="UTF-8">
<meta name="viewport" content="width=device-width, initial-scale=1.0">
<title>PurgaLib Framework</title>
<style>
  body {
    font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
    background-color: #121212;
    color: #e0e0e0;
    margin: 0;
    padding: 0;
  }
  header {
    text-align: center;
    padding: 40px 20px;
    background: linear-gradient(90deg,#ff6f00,#ff8f00);
    color: #fff;
  }
  header h1 {
    margin: 0;
    font-size: 3em;
  }
  .badges img {
    margin: 5px;
  }
  main {
    max-width: 900px;
    margin: 40px auto;
    padding: 0 20px;
  }
  section {
    margin-bottom: 40px;
    padding: 25px;
    border-radius: 10px;
    background-color: #1f1f1f;
    box-shadow: 0 0 20px rgba(255,111,0,0.2);
  }
  section h2 {
    color: #ff6f00;
    text-align: center;
    margin-bottom: 20px;
    font-size: 2em;
  }
  pre {
    background-color: #262626;
    color: #fff;
    padding: 15px;
    border-radius: 5px;
    overflow-x: auto;
  }
  code {
    font-family: Consolas, monospace;
  }
  .step {
    background-color: #1f1f1f;
    border-left: 5px solid #ff6f00;
    padding: 20px;
    margin: 20px 0;
    border-radius: 8px;
    box-shadow: 0 0 10px rgba(255,111,0,0.3);
  }
  .step h3 {
    margin-top: 0;
  }
  .note {
    border-left: 5px solid #9c27b0;
    padding: 15px;
    margin: 20px 0;
    border-radius: 8px;
    background-color: #1f1f1f;
  }
  a {
    color: #ff6f00;
    text-decoration: none;
  }
  a:hover {
    text-decoration: underline;
  }
</style>
</head>
<body>

<header>
  <h1>🔥 PurgaLib Framework</h1>
  <div class="badges">
    <img src="https://img.shields.io/badge/Status-ALPHA-orange?style=for-the-badge&logo=testinglibrary" alt="ALPHA">
    <img src="https://img.shields.io/badge/Platform-SCP:_Secret_Laboratory-black?style=for-the-badge" alt="Platform">
    <img src="https://img.shields.io/badge/Language-C%23-239120?style=for-the-badge&logo=csharp&logoColor=white" alt="C#">
    <img src="https://img.shields.io/badge/Build-Passing-brightgreen?style=for-the-badge&logo=githubactions" alt="Build">
  </div>
</header>

<main>
<section>
  <h2>🌟 About</h2>
  <p align="center">
    PurgaLib is a lightweight, modular and developer-friendly framework for <strong>SCP: Secret Laboratory</strong>.<br>
    Designed to be fast, clean, and event-driven.
  </p>
</section>

<section>
  <h2>🛠️ Plugin Creation Tutorial</h2>

  <div class="step">
    <h3>1️⃣ Add PurgaLib.dll</h3>
    <p>Include <code>PurgaLib.dll</code> in your C# plugin project.<br>
    Place it in <strong>LabAPI/plugins/Global</strong>.</p>
  </div>

  <div class="step" style="border-left-color:#00bcd4; box-shadow: 0 0 10px rgba(0,188,212,0.3);">
    <h3>2️⃣ Create <code>Config.cs</code></h3>
    <pre><code>using PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibEvent.Events.PluginManager;

namespace ExamplePurgaLib
{
    public class Config : IPurgaConfig
    {
        public bool Enabled { get; set; } = true;
    }
}
    </code></pre>
  </div>

  <div class="step" style="border-left-color:#4caf50; box-shadow: 0 0 10px rgba(76,175,80,0.3);">
    <h3>3️⃣ Create <code>Plugin.cs</code></h3>
    <pre><code>using System;
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
    </code></pre>
  </div>

  <div class="step" style="border-left-color:#ff5722; box-shadow: 0 0 10px rgba(255,87,34,0.3);">
    <h3>4️⃣ Add Your Logic</h3>
    <p>Implement events, commands, or custom features for your plugin.<br>
    Use PurgaLib's API for server interaction.</p>
  </div>

  <div class="note">
    <h3>💡 Notes</h3>
    <ul>
      <li>✔ All core events are implemented</li>
      <li>✔ For personal or server use only</li>
      <li>❌ Do not rebrand or redistribute</li>
    </ul>
  </div>
</section>

<section>
  <h2>📜 License & Usage</h2>
  <p align="center">
    This project is protected by <strong>Copyright</strong>.<br>
    ✔ Personal or server use allowed<br>
    ❌ Rebranding or redistribution not allowed
  </p>
</section>

</main>
</body>
</html>
