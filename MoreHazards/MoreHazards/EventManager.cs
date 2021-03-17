using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exiled.API.Features;
using Exiled.Events.EventArgs;
using MEC;
using Handlers = Exiled.Events.Handlers;

namespace MoreHazards
{
    public class EventManager : IDisposable
    {
        public EventManager()
        {
            Handlers.Server.RoundStarted += OnRoundStart;
            Handlers.Server.RoundEnded += OnRoundEnd;
        }
        public virtual void Dispose()
        {
            Handlers.Server.RoundStarted -= OnRoundStart;
            Handlers.Server.RoundEnded -= OnRoundEnd;
        }

        public virtual void OnRoundStart()
        { }
        public virtual void OnRoundEnd(RoundEndedEventArgs ev)
        { }
    }
}
