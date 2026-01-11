using System;
using System.Collections.Generic;

namespace PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibAPI.Core
{
    public class StaticActor 
    {
        private static readonly Dictionary<Type, PActor> actors = new();

        public static T Get<T>() where T : PActor, new()
        {
            var type = typeof(T);

            if (!actors.TryGetValue(type, out var actor))
            {
                actor = new T();
                actors[type] = actor;
            }

            return (T)actor;
        }

        public static void Destroy<T>() where T : PActor
        {
            var type = typeof(T);

            if (!actors.TryGetValue(type, out var actor))
                return;

            actor.DestroyInternal();
            actors.Remove(type);
        }
    }
}
