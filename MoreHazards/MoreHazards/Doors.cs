using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exiled.API.Enums;
using Exiled.API.Extensions;
using Exiled.API.Features;
using Exiled.Events.EventArgs;
using Interactables.Interobjects;
using Interactables.Interobjects.DoorUtils;
using MEC;
using Warhead = Exiled.Events.Handlers.Warhead;

namespace MoreHazards
{
    class DoorLogicManager : LogicManager
    {
        private static CoroutineHandle RandomMalfunctionHandle;
        private static CoroutineHandle FullBreakdownHandle;
        private static readonly DoorConfig MalfunctionConfig = MoreHazards.Instance.Config.DoorMalfunction;
        private static readonly DoorSystemBreakdownConfig BreakdownConfig = MoreHazards.Instance.Config.DoorSystemBreakdown;

        public DoorLogicManager()
        {
            Warhead.Detonated += TerminateCoroutines;
        }

        public override void Dispose()
        {
            base.Dispose();
            Warhead.Detonated -= TerminateCoroutines;
        }

        public override void OnRoundStart()
        {
            if(MalfunctionConfig.Enabled)
                RandomMalfunctionHandle = Timing.RunCoroutine(RandomDoorMalfunction());

            if (BreakdownConfig.Enabled)
                FullBreakdownHandle = Timing.RunCoroutine(FullDoorBreakdown());
        }
        public override void OnRoundEnd(RoundEndedEventArgs ev)
        {
            TerminateCoroutines();
        }
        public void TerminateCoroutines()
        {
            Timing.KillCoroutines(RandomMalfunctionHandle);

            Timing.KillCoroutines(FullBreakdownHandle);
        }

        public IEnumerator<float> RandomDoorMalfunction()
        {
            while (Round.IsStarted)
            {
                yield return Timing.WaitForSeconds(MalfunctionConfig.RandomDoorMalfunctionTiming.GetInterval());
                foreach (var player in Player.List)
                {
                    if (MalfunctionConfig.IgnoredRoles.Contains(player.Role))
                        continue;
                    
                    if (UnityEngine.Random.Range(0, 100) > MalfunctionConfig.PerPlayerChance)
                        continue;

                    //random door of the room the player is inside
                    var door = CollectionUtils<DoorVariant>.GetRandomElement((Map.FindParentRoom(player.GameObject).Doors));
                    
                    door.NetworkTargetState = false;
                    Log.Debug("Door closed on player:"+player.Nickname);
                }
            }
        }

        public IEnumerator<float> FullDoorBreakdown()
        {
            while (Round.IsStarted)
            {
                yield return Timing.WaitForSeconds(BreakdownConfig.FullDoorSystemBreakdownTiming.GetCooldown());

                foreach (var door in Map.Doors)
                {
                    if (BreakdownConfig.CloseBeforeLocking)
                        door.NetworkTargetState = false;

                    door.ServerChangeLock(DoorLockReason.SpecialDoorFeature,true);
                }

                Log.Debug("Door system breakdown",MoreHazards.Instance.Config.Debug);

                if (BreakdownConfig.UseCassieMessage)
                    BreakdownConfig.CassieMessageOnBreakdown.Speak();

                yield return Timing.WaitForSeconds(BreakdownConfig.FullDoorSystemBreakdownTiming.GetDuration());

                foreach (var door in Map.Doors)
                {
                    door.ServerChangeLock(DoorLockReason.SpecialDoorFeature, false);
                }
            }
        }
    }
}
