using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using CustomPlayerEffects;
using Discord;
using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.Loader;
using Exiled.Events;
using Exiled.Events.EventArgs;
using HarmonyLib;
using Handlers = Exiled.Events.Handlers;
using MEC;


namespace MoreHazards
{
    public class MoreHazards : Plugin<Config>
    {
        private static MoreHazards singleton = new MoreHazards();

        public override string Author { get; } = "Maticzpl";
        public override string Name { get; } = "More Hazards";
        public override Version Version { get; } = new Version(0, 1, 0);
        public override Version RequiredExiledVersion { get; } = new Version(2, 9, 0);

        private MoreHazards()
        {
        }

        private static List<LogicManager> LogicHandlers = new List<LogicManager>();
        public static MoreHazards Instance => singleton;

        public override PluginPriority Priority { get; } = PluginPriority.Medium;


        public override void OnEnabled()
        {
            base.OnEnabled();

            LogicHandlers.Add(new TeslaGateManager());
            LogicHandlers.Add(new ElevatorLogicManager());
            LogicHandlers.Add(new DoorLogicManager());
        }


        public override void OnDisabled()
        {
            base.OnDisabled();

            LogicHandlers.Clear();
        }

    }
}
