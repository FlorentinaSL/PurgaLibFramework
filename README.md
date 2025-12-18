# 🔥 PurgaLib Framework

![ALPHA](https://img.shields.io/badge/Status-ALPHA-orange?style=for-the-badge&logo=testinglibrary)
![Platform](https://img.shields.io/badge/Platform-SCP:_Secret_Laboratory-black?style=for-the-badge)
![C#](https://img.shields.io/badge/Language-C%23-239120?style=for-the-badge&logo=csharp&logoColor=white)
![Build](https://img.shields.io/badge/Build-Passing-brightgreen?style=for-the-badge&logo=githubactions)

---

## 🌟 About

PurgaLib is a lightweight, modular, developer-friendly framework for **SCP: Secret Laboratory**.  
Designed to be fast, clean, and event-driven.

---

## 🛠️ Plugin Creation Tutorial

Follow these steps to create a plugin using **PurgaLib Framework v0.0.1**:

> ### 1️⃣ Add PurgaLibFramework.dll
Include `PurgaLibFramework.dll` in your C# plugin project.  
Place it in **LabAPI/plugins/Global**.

> ### 2️⃣ Create `Config.cs`
```csharp
using PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibEvent.Events.PluginManager;

namespace ExamplePurgaLib
{
    public class Config : IPurgaConfig
    {
        public bool Enabled { get; set; } = true;
    }
}
```
> ### 3️⃣ Create `Plugin.cs`
```csharp
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

```
<link rel="stylesheet" href="style.css">


