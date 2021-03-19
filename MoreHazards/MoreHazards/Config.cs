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

        [Description("Tesla Gates Module - Disable Tesla Gates for specific roles, Disable random teslas completly")]
        public TeslaConfig Tesla { get; set; } = new TeslaConfig();

        [Description("Elevators Module - Once in a while an elevator can breakdown.")]
        public ElevatorsConfig Elevators { get; set; } = new ElevatorsConfig();

        [Description("Door Module - Will close a random door near random players")]
        public DoorConfig DoorMalfunction { get; set; } = new DoorConfig();

        [Description("Door system breakdown module - a rare event that will lock all doors in facility for a few seconds.")]
        public DoorSystemBreakdownConfig DoorSystemBreakdown { get; set; } = new DoorSystemBreakdownConfig();


        [Description("Show debug messages in console.")]
        public bool Debug { get; set; } = false;
    }

    
    public class ElevatorsConfig
    {
        [Description("EVENT: Elevators will sometimes breakdown and stop working")]
        public bool Enabled { get; set; } = false;

        [Description("List of all the elevators that can break down. AVAILABLE: LczA LczB Nuke Scp049 GateA GateB")]
        public List<ElevatorType> BreakableElevators { get; set; } = new List<ElevatorType>( new []{ElevatorType.LczA, ElevatorType.LczB});

        [Description("Timing of the elevator break event")]
        public RandomTiming RandomEventTiming { get; set; } = new RandomTiming(10,30,120,300);

        [Description("Chance of breaking down per elevator")]
        public int ChancePerElevator { get; set; } = 40;
        
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

    public class DoorConfig
    {
        public bool Enabled { get; set; } = false;

        [Description("If a player controls SCP 079 this part of the plugin will be inactive.")]
        public bool DisableIf079Exists { get; set; }  = true;

        [Description("Timing of the door malfunction event")]
        public RandomInterval RandomDoorMalfunctionTiming { get; set; } = new RandomInterval(5,15);

        [Description("Chance of a door closing per player")]
        public int PerPlayerChance { get; set; } = 60;

        [Description("Roles that the doors wont close around.  Available: ChaosInsurgency ClassD FacilityGuard NtfCadet NtfCommander NtfLieutenant NtfScientist Scientist Scp049 Scp0492 Scp096 Scp106 Scp173 Tutorial Scp93953 Scp93989")]
        public List<RoleType> IgnoredRoles { get; set; } = new List<RoleType>(new[] { RoleType.ChaosInsurgency,RoleType.Scp93953, RoleType.Scp93989, RoleType.Scp049, RoleType.Scp106, RoleType.Scp0492, RoleType.Scp173, RoleType.Scp096 });
        
    }

    public class DoorSystemBreakdownConfig
    {
        public bool Enabled { get; set; } = false;

        [Description("If true all doors will close just before getting locked.")]
        public bool CloseBeforeLocking { get; set; } = false;

        [Description("The cooldown and duration of this event.")]
        public RandomTiming FullDoorSystemBreakdownTiming { get; set; } = new RandomTiming(5, 15, 140, 300);

        [Description("Is the mesage below enabled")]
        public bool UseCassieMessage { get; set; } = false;

        [Description("The message that will play on door system breakdown")]
        public CassieAnnouncement CassieMessageOnBreakdown { get; set; } = new CassieAnnouncement();
    }
}