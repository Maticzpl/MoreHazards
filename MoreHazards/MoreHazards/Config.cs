using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandSystem.Commands.Prefs;
using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.API.Interfaces;

namespace MoreHazards
{
    public class Config : IConfig
    {
        [Description("Is plugin enabled")]
        public bool IsEnabled { get; set; } = true;

        [Description("Tesla Gates Module")]
        public TeslaConfig Tesla { get; set; } = new TeslaConfig();

        [Description("Elevators Module")]
        public ElevatorsConfig Elevators { get; set; } = new ElevatorsConfig();

        [Description("Show debug messages in console.")]
        public bool Debug { get; set; } = false;
    }

    
    public class ElevatorsConfig
    {
        [Description("Elevator Breakdown Module")]
        public bool Enabled { get; set; } = false;

        [Description("List of all the elevators that can break down")]
        public List<ElevatorType> BreakableElevators { get; set; } = new List<ElevatorType>( new []{ElevatorType.LczA, ElevatorType.LczB});

        [Description("Timing of the elevator break event")]
        public RandomTiming RandomEventTiming { get; set; } = new RandomTiming(10,30,120,300);

        [Description("Chance of breaking down per elevator")]
        public int ChancePerElevator { get; set; } = 30;
        
        [Description("Will cause the room the elevator broke in to have disabled lights")]
        public bool BlackoutRoom { get; set; } = true;

        [Description("Cassie will play your message on elevator break if true")]
        public bool EnableCassieMessage { get; set; } = false;

        public CassieAnnouncement CassieMessage { get; set; } = new CassieAnnouncement();
    }

    public class TeslaConfig
    {
        public bool Enabled { get; set; } = true;

        [Description("Roles ignored by tesla. Available: ChaosInsurgency ClassD FacilityGuard NtfCadet NtfCommander NtfLieutenant NtfScientist Scientist Scp049 Scp0492 Scp096 Scp106 Scp173 Tutorial Scp93953 Scp93989")]
        public List<RoleType> IgnoredRoles { get; set; } = new List<RoleType>(new[] {RoleType.NtfCommander,RoleType.NtfLieutenant});

        [Description("Tesla Gate module")]
        public bool DisablingTeslas { get; set; } = false;

        [Description("Minimum number of tesla gates on the map for them to be randomly disabled.")]
        public short MinTeslaGates { get; set; } = 3;

        [Description("Maximum number of disabled tesla gates.")]
        public short MaxDisabledTeslaGates { get; set; } = 1;

        [Description("Chance of a tesla gate being disabled.")]
        public short TeslaGateDisableChance { get; set; } = 33;
    }

}
