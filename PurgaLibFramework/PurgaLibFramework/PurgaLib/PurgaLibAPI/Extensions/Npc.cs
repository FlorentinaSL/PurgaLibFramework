using System.Collections.Generic;
using MEC;
using Mirror;
using PlayerRoles;
using PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibAPI.Features;
using UnityEngine;

namespace PurgaLibFramework.PurgaLibFramework.PurgaLib.PurgaLibAPI.Extensions
{
    public class Npc : Player
    {
        private static int _nextconid = 1000;
        private static readonly List<Npc> Npcs = new();
        public new static IReadOnlyList<Npc> List => Npcs;

        private CoroutineHandle _followHandle;

        public static Npc Spawn(string name, RoleTypeId role, Vector3 position)
        {
            GameObject obj = Object.Instantiate(NetworkManager.singleton.playerPrefab);
            var fakeConn = new NetworkConnectionToClient(_nextconid++);
            NetworkServer.AddPlayerForConnection(fakeConn, obj);

            var hub = obj.GetComponent<ReferenceHub>();
            int id = _nextconid++;
            hub.nicknameSync.Network_myNickSync = name;
            hub.roleManager.InitializeNewRole(RoleTypeId.None, RoleChangeReason.None);
            hub.authManager.UserId = $"{id}";

            LabApi.Features.Wrappers.Player player = LabApi.Features.Wrappers.Player.Get(hub);
            player.SetRole(role);
            player.Position = position;

            var npc = new Npc(player);
            Npcs.Add(npc);
            return npc;
        }

        internal new LabApi.Features.Wrappers.Player Base { get; }
        protected Npc(LabApi.Features.Wrappers.Player player) : base(player)
        {
            Base = player;
        }

        public new string Name => Base.Nickname;
        public new RoleTypeId Role => Base.Role;
        public new Vector3 Position { get => Base.Position; set => Base.Position = value; }
        public override bool IsAlive => Base.IsAlive;

        public new void SetRole(RoleTypeId role) => Base.SetRole(role);
        public new void Kill(string reason = "NPC killed") { if (!Base.IsAlive) return; Base.Kill(reason); }
        public void Destroy(string reason = "NPC destroyed") { NetworkServer.Destroy(Base.GameObject); Npcs.Remove(this); }
        
        public void TeleportToPlayer(LabApi.Features.Wrappers.Player target)
        {
            if (target == null || !IsAlive) return;
            Position = target.Position + Vector3.back;
        }

        public void FollowPlayer(LabApi.Features.Wrappers.Player target, float speed = 5f, float updateRate = 0.05f, float smoothness = 0.1f)
        {
            if (_followHandle.IsRunning)
                Timing.KillCoroutines(_followHandle);

            _followHandle = Timing.RunCoroutine(FollowRoutine(target, speed, updateRate, smoothness));
        }

        private IEnumerator<float> FollowRoutine(LabApi.Features.Wrappers.Player target, float speed, float updateRate, float smoothness)
        {
            while (target != null && IsAlive)
            {
                Vector3 targetPos = target.Position + Vector3.back;
                Position = Vector3.Lerp(Position, targetPos, speed * smoothness * updateRate);

                yield return Timing.WaitForSeconds(updateRate);
            }
        }

        public void StopFollow()
        {
            if (_followHandle.IsRunning)
                Timing.KillCoroutines(_followHandle);
        }
    }
}
