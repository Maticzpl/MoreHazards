using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Exiled.API.Enums;
using Exiled.API.Extensions;
using Exiled.API.Features;
using Exiled.Events.EventArgs;
using MEC;
using Warhead = Exiled.Events.Handlers.Warhead;

namespace MoreHazards
{
    public class ElevatorLogicManager : LogicManager
    {
        private CoroutineHandle CoroutineHandle;
        private readonly ElevatorsConfig Config = MoreHazards.Instance.Config.Elevators;

        private readonly Dictionary<ElevatorType, (RoomType lower, RoomType upper)> ElevatorToRoomsLookup = 
            new Dictionary<ElevatorType, (RoomType lower, RoomType upper)>
            {
                [ElevatorType.LczA] = (RoomType.LczChkpA, RoomType.HczChkpA),
                [ElevatorType.LczB] = (RoomType.LczChkpB, RoomType.HczChkpB),
                [ElevatorType.Nuke] = (RoomType.HczNuke, RoomType.Unknown),
                [ElevatorType.Scp049] = (RoomType.Hcz049, RoomType.Unknown),
                [ElevatorType.GateA] = (RoomType.EzGateA, RoomType.Unknown),
                [ElevatorType.GateB] = (RoomType.EzGateB, RoomType.Unknown)
            };

        public ElevatorLogicManager()
        {
            Warhead.Detonated += OnDetonated;
        }

        public override void Dispose()
        {
            base.Dispose();
            Warhead.Detonated -= OnDetonated;
        }

        public override void OnRoundStart()
        {
            if (!Config.Enabled)
                return;

            CoroutineHandle = Timing.RunCoroutine(Timer());
        }
        public override void OnRoundEnd(RoundEndedEventArgs ev)
        {
            Timing.KillCoroutines(CoroutineHandle);
        }
        public void OnDetonated()
        {
            Timing.KillCoroutines(CoroutineHandle);
        }
        public IEnumerator<float> Timer()
        {
            while (Round.IsStarted)
            {
                yield return Timing.WaitForSeconds(Config.RandomEventTiming.GetCooldown());


                int duration = Config.RandomEventTiming.GetDuration();
                LiftBreakdown(duration);

                if (Config.EnableCassieMessage)
                    Config.CassieMessage.Speak();

                yield return Timing.WaitForSeconds(duration);
                FixLifts();
            }
        }
        public void LiftBreakdown(int duration)
        {
            foreach (var lift in Map.Lifts)
            {
                if (!Config.BreakableElevators.Contains(lift.Type()))
                    continue;

                
                var rng = UnityEngine.Random.Range(0, 100);

                if (rng > Config.ChancePerElevator)
                    continue;

                lift.operative = false;

                Debug.Log($"Elevator {lift.Type()} disabled for {duration}");

                if (Config.BlackoutRoom)
                {
                    var lower = ElevatorToRoomsLookup[lift.Type()].lower;
                    var upper = ElevatorToRoomsLookup[lift.Type()].upper;

                    Map.Rooms.First(r => r.Type == lower ).TurnOffLights(duration);

                    if (upper != RoomType.Unknown)
                        Map.Rooms.First(r => r.Type == upper).TurnOffLights(duration);
                }
            }
        }
        public void FixLifts()
        {
            foreach (var lift in Map.Lifts)
            {
                lift.operative = true;
            }

        }
    }
}
