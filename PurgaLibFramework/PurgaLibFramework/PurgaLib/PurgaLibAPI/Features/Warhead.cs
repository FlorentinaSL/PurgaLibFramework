using System.Collections.Generic;
using Interactables.Interobjects.DoorUtils;
using UnityEngine;
using Mirror;

namespace PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibAPI.Features
{
    public static class Warhead
    {
        public static AlphaWarheadController Controller => AlphaWarheadController.Singleton;
        
        private static AlphaWarheadOutsitePanel OutsitePanel =>
            Object.FindObjectOfType<AlphaWarheadOutsitePanel>();
        
        private static AlphaWarheadNukesitePanel SitePanel => AlphaWarheadOutsitePanel.nukeside;
        
        public static bool AutoDetonate
        {
            get => Controller._autoDetonate;
            set => Controller._autoDetonate = value;
        }
        
        public static bool LeverStatus
        {
            get => SitePanel.Networkenabled;
            set => SitePanel.Networkenabled = value;
        }
        
        
        public static bool IsInProgress => Controller.Info.InProgress;
        
        public static bool IsDetonated => Controller.AlreadyDetonated;
        
        public static float DetonationTimer
        {
            get => AlphaWarheadController.TimeUntilDetonation;
            set => Controller.ForceTime(value);
        }
        
        public static float RealDetonationTimer => Controller.CurScenario.TimeToDetonate;
        
        public static bool IsLocked
        {
            get => Controller.IsLocked;
            set => Controller.IsLocked = value;
        }
        
        public static int Kills
        {
            get => Controller.WarheadKills;
            set => Controller.WarheadKills = value;
        }
        
        public static bool CanBeStarted => !IsInProgress && !IsDetonated && Controller.CooldownEndTime <= NetworkTime.time;
        
        public static void Start()
        {
            Controller.InstantPrepare();
            Controller.StartDetonation(false);
        }
        
        public static void Stop() => Controller.CancelDetonation();
        
        public static void Detonate() => Controller.ForceTime(0f);
        
        public static void Shake() => Controller.RpcShake(false);
        
        public static void TriggerDoors(bool open) =>
            DoorEventOpenerExtension.TriggerAction(open 
                ? DoorEventOpenerExtension.OpenerEventType.WarheadStart 
                : DoorEventOpenerExtension.OpenerEventType.WarheadCancel);
        
        public static void CloseBlastDoors()
        {
            foreach (var door in BlastDoors)
                door._isOpen = false;
        }
        
        public static IReadOnlyCollection<BlastDoor> BlastDoors => BlastDoor.Instances;
        
        public static bool CanBeDetonated(Vector3 pos, bool includeOnlyLifts = false) =>
            AlphaWarheadController.CanBeDetonated(pos, includeOnlyLifts);
    }
}
