using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandSystem.Commands.Prefs;
using Exiled.API.Interfaces;

namespace MoreHazards
{
    public class Config : IConfig
    {
        [Description("Is plugin enabled")]
        public bool IsEnabled { get; set; } = true;


        [Description("Roles ignored by tesla. Available: ChaosInsurgency ClassD FacilityGuard NtfCadet NtfCommander NtfLieutenant NtfScientist Scientist Scp049 Scp0492 Scp096 Scp106 Scp173 Tutorial Scp93953 Scp93989")]
        public string[] IgnoredRoles { get; set; }
        
        [Description("Disable Random Tesla Gates")]
        public bool DisabledTeslas { get; set; } = false;

        [Description("Minimum number of tesla gates on the map for them to be randomly disabled.")]
        public short MinTeslaGates { get; set; } = 3;

        [Description("Maximum number of disabled tesla gates.")]
        public short MaxDisabledTeslaGates { get; set; } = 1;

        [Description("Chance of a tesla gate being disabled.")]
        public short TeslaGateDisableChance { get; set; } = 33;
        

        [Description("Chance of a tesla gate being disabled.")]
        public short ElevatorMalfunctions { get; set; } = 33;


        [Description("Show debug messages in console.")]
        public bool Debug { get; set; } = false;
    }
}
