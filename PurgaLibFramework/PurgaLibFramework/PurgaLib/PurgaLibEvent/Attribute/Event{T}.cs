using System;
using System.Collections.Generic;
using MEC;
using PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibAPI.Features.Server;

namespace PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibEvent.Attribute
{
    public sealed class Event<T> : IEvent
    {
        private record Registration(Action<T> Handler, int Priority);
        private record AsyncRegistration(Func<T, IEnumerator<float>> Handler, int Priority);

        private readonly List<Registration> _syncHandlers = new();
        private readonly List<AsyncRegistration> _asyncHandlers = new();
        private readonly object _lock = new();

        // ------------------- Add/Remove -------------------
        public void Add(Action<T> handler, int priority = 0)
        {
            if (handler == null) return;
            lock (_lock)
            {
                _syncHandlers.Add(new Registration(handler, priority));
                _syncHandlers.Sort((a, b) => b.Priority - a.Priority); // higher priority first
            }
        }

        public void Remove(Action<T> handler)
        {
            if (handler == null) return;
            lock (_lock)
                _syncHandlers.RemoveAll(h => h.Handler == handler);
        }

        public void AddAsync(Func<T, IEnumerator<float>> handler, int priority = 0)
        {
            if (handler == null) return;
            lock (_lock)
            {
                _asyncHandlers.Add(new AsyncRegistration(handler, priority));
                _asyncHandlers.Sort((a, b) => b.Priority - a.Priority); // higher priority first
            }
        }

        public void RemoveAsync(Func<T, IEnumerator<float>> handler)
        {
            if (handler == null) return;
            lock (_lock)
                _asyncHandlers.RemoveAll(h => h.Handler == handler);
        }

        // ------------------- Invoke -------------------
        public void Invoke(T args)
        {
            Registration[] syncSnapshot;
            AsyncRegistration[] asyncSnapshot;
            lock (_lock)
            {
                syncSnapshot = _syncHandlers.ToArray();
                asyncSnapshot = _asyncHandlers.ToArray();
            }

            // Sincrono
            foreach (var h in syncSnapshot)
            {
                try { h.Handler(args); }
                catch (Exception ex) { Log.Error($"[Event] Error in handler {h.Handler.Method.Name}: {ex}"); }
            }

            // Asincrono
            foreach (var h in asyncSnapshot)
            {
                try { Timing.RunCoroutine(h.Handler(args)); }
                catch (Exception ex) { Log.Error($"[Event] Error in async handler {h.Handler.Method.Name}: {ex}"); }
            }
        }

        // ------------------- Operatori + / - -------------------
        public static Event<T> operator +(Event<T> ev, Action<T> handler)
        {
            ev.Add(handler);
            return ev;
        }

        public static Event<T> operator -(Event<T> ev, Action<T> handler)
        {
            ev.Remove(handler);
            return ev;
        }

        public static Event<T> operator +(Event<T> ev, Func<T, IEnumerator<float>> handler)
        {
            ev.AddAsync(handler);
            return ev;
        }

        public static Event<T> operator -(Event<T> ev, Func<T, IEnumerator<float>> handler)
        {
            ev.RemoveAsync(handler);
            return ev;
        }

        // ------------------- IEvent generico -------------------
        public void Add(Action<object> handler)
        {
            if (handler == null) return;

            Action<T> wrapped = arg =>
            {
                try { handler(arg!); }
                catch (Exception ex) { Log.Error($"[Event] Error in generic Add handler: {ex}"); }
            };

            Add(wrapped);
        }


        public void Remove(Action<object> handler)
        {
            if (handler == null) return;

            Action<T> wrapped = arg =>
            {
                try { handler(arg!); }
                catch { /* ignore */ }
            };

            Remove(wrapped); 
        }


        public void Invoke(object args)
        {
            if (args is T typed)
                Invoke(typed);
            else
                Log.Error($"[Event] Invalid args type: expected {typeof(T)}, got {args?.GetType()}");
        }
    }
}
