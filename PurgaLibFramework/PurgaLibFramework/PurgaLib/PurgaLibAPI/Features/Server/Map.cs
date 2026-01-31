using PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibAPI.Core;
using UnityEngine;

namespace PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibAPI.Features.Server
{
    public sealed class Map : PActor
    {
        private static Light[] _allLights;
        private static void InitLights()
        {
            if (_allLights == null)
                _allLights = GameObject.FindObjectsOfType<Light>();
        }
        public static void TurnOffAllLights(float duration)
        {
            InitLights();
            foreach (var light in _allLights)
                light.enabled = false;
            
            if (duration > 0f)
            {
                MonoBehaviour dummy = new GameObject("TempLightTimer").AddComponent<MonoBehaviour>();
                dummy.StartCoroutine(TurnOnAfterDelay(dummy, duration));
            }
        }

        private static System.Collections.IEnumerator TurnOnAfterDelay(MonoBehaviour mb, float duration)
        {
            yield return new WaitForSeconds(duration);
            TurnOnAllLights();
            GameObject.Destroy(mb.gameObject);
        }
        
        public static void TurnOnAllLights()
        {
            InitLights();
            foreach (var light in _allLights)
                light.enabled = true;
        }
        
        public static void SetColorOfAllLights(Color color)
        {
            InitLights();
            foreach (var light in _allLights)
                light.color = color;
        }
        
        public static void ResetAllLights()
        {
            SetColorOfAllLights(Color.white);
        }
        
        public override bool IsAlive => true;
        public override Transform Transform => null;
        protected override void Tick() { }
    }
}
