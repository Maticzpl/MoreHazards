using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exiled.API.Extensions;
using Exiled.API.Features;
using Exiled.Events.EventArgs;
using MEC;

namespace MoreHazards
{
    public class ElevatorEventManager
    {
        private static CoroutineHandle CoroutineHandle;
        private static readonly ElevatorsConfig Config = MoreHazards.Instance.Config.Elevators;
        public static void OnRoundStart()
        {
            if (!Config.Enabled)
                return;

            CoroutineHandle = Timing.RunCoroutine(Timer());
        }
        public static void OnRoundEnd(RoundEndedEventArgs ev)
        {
            Timing.KillCoroutines(CoroutineHandle);
        }

        private static IEnumerator<float> Timer()
        {
            yield return Timing.WaitForSeconds(Config.RandomEventTiming.GetCooldown());


            int duration = Config.RandomEventTiming.GetDuration();
            LiftBreakdown(duration);
            yield return Timing.WaitForSeconds(duration);
            FixLifts();
        }
        public static void LiftBreakdown(int duration)
        {
            foreach (var lift in Map.Lifts)
            {
                if (!Config.BreakableElevators.Contains(lift.Type()))
                    continue;

                var rng = RandomGenerator.GetInt16(0, 100);

                if (rng > Config.ChancePerElevator)
                    continue;

                lift.operative = false;

                if (Config.EnableCassieMessage)
                    Config.CassieMessage.Speak();

                if (Config.BlackoutRoom)
                {
                    //do blackout here
                }
            }
        }

        public static void FixLifts()
        {
            foreach (var lift in Map.Lifts)
            {
                lift.operative = true;
            }
        }
        //lift.Type()

    }
}
