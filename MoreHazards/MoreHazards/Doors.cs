using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.Events.EventArgs;
using MEC;
using Warhead = Exiled.Events.Handlers.Warhead;

namespace MoreHazards
{
    class DoorEventManager : EventManager
    {
        private static CoroutineHandle CoroutineHandle;
        private static readonly DoorConfig Config = MoreHazards.Instance.Config.Doors;

        public DoorEventManager()
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
                yield return Timing.WaitForSeconds(1);
            }
        }
    }
}
