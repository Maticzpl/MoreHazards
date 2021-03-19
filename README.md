
[![Github release](https://img.shields.io/github/tag/Maticzpl/MoreHazards.svg?colorB=2146bf&label=Version)](https://github.com/Maticzpl/MoreHazards/releases)
[![Github commits](https://img.shields.io/github/last-commit/Maticzpl/MoreHazards?color=green&label=Last%20Commit)](https://github.com/Maticzpl/MoreHazards/commits/main)
[![Github downloads](https://img.shields.io/github/downloads/Maticzpl/MoreHazards/total?label=Downloads)](https://github.com/Maticzpl/MoreHazards/releases)

# MoreHazards
This plugin adds in mainly passive / enveriomental hazards.  
**More Hazards is in a very early stage of development.**
### Tesla Gates
The tesla gate module adds in:
- Options for roles that wont trigger tesla gates  
- Disabling random teslas (simmilar to how some teslas are disabled in SCP CB)
### Elevator Malfunctions
- Choose what elevators can break and how frequently
- Blackout the rooms the elevators are in
- Custom CASSIE message on elevator breakdown
### Doors 
- Randomly close doors around random players
- Random breakdowns of the whole door system
## Default Config
**The default config has most of those features disabled by default**.
Make sure you adjust the config to your preference.
```yaml
more_hazards:
# Is plugin enabled
  is_enabled: true
  # Tesla Gates Module - Disable Tesla Gates for specific roles, Disable random teslas completly
  tesla:
    enabled: true
    # Roles ignored by tesla. Available: ChaosInsurgency ClassD FacilityGuard NtfCadet NtfCommander NtfLieutenant NtfScientist Scientist Scp049 Scp0492 Scp096 Scp106 Scp173 Tutorial Scp93953 Scp93989
    ignored_roles:
    - NtfCommander
    - NtfLieutenant
    # Tesla Gate module
    disabling_teslas: false
    # Minimum number of tesla gates on the map for them to be randomly disabled.
    min_tesla_gates: 3
    # Maximum number of disabled tesla gates.
    max_disabled_tesla_gates: 1
    # Chance of a tesla gate being disabled.
    tesla_gate_disable_chance: 33
  # Elevators Module - Once in a while an elevator can breakdown.
  elevators:
  # EVENT: Elevators will sometimes breakdown and stop working
    enabled: false
    # List of all the elevators that can break down. AVAILABLE: LczA LczB Nuke Scp049 GateA GateB
    breakable_elevators:
    - LczA
    - LczB
    # Timing of the elevator break event
    random_event_timing:
    # Min Duration of the event
      min_duration: 10
      # Max Duration of the event
      max_duration: 30
      # Min Cooldown between calling this event
      min_cooldown: 120
      # Max Cooldown between calling this event
      max_cooldown: 300
    # Chance of breaking down per elevator
    chance_per_elevator: 40
    # Will cause the room the elevator broke in to have disabled lights
    blackout_room: true
    # Cassie will play your message on elevator break if true
    enable_cassie_message: false
    cassie_message:
    # Cassie Message Text
      text: ''
      # Cassie Message Glitch Chance (0-100)
      glitches: 10
      # Cassie Message Jam Chance (0-100)
      jams: 10
  # Door Module - Will close a random door near random players
  door_malfunction:
    enabled: false
    # If a player controls SCP 079 this part of the plugin will be inactive.
    disable_if079_exists: true
    # Timing of the door malfunction event
    random_door_malfunction_timing:
    # Min Delay between calling this event
      min_delay: 5
      # Max Delay between calling this event
      max_delay: 15
    # Chance of a door closing per player
    per_player_chance: 60
    # Roles that the doors wont close around.  Available: ChaosInsurgency ClassD FacilityGuard NtfCadet NtfCommander NtfLieutenant NtfScientist Scientist Scp049 Scp0492 Scp096 Scp106 Scp173 Tutorial Scp93953 Scp93989
    ignored_roles:
    - ChaosInsurgency
    - Scp93953
    - Scp93989
    - Scp049
    - Scp106
    - Scp0492
    - Scp173
    - Scp096
  # Door system breakdown module - a rare event that will lock all doors in facility for a few seconds.
  door_system_breakdown:
    enabled: false
    # If true all doors will close just before getting locked.
    close_before_locking: false
    # The cooldown and duration of this event.
    full_door_system_breakdown_timing:
    # Min Duration of the event
      min_duration: 5
      # Max Duration of the event
      max_duration: 15
      # Min Cooldown between calling this event
      min_cooldown: 140
      # Max Cooldown between calling this event
      max_cooldown: 300
    # Is the mesage below enabled
    use_cassie_message: false
    # The message that will play on door system breakdown
    cassie_message_on_breakdown:
    # Cassie Message Text
      text: ''
      # Cassie Message Glitch Chance (0-100)
      glitches: 10
      # Cassie Message Jam Chance (0-100)
      jams: 10
  # Show debug messages in console.
  debug: false
```

