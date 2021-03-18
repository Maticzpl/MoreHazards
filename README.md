
[![Github release](https://img.shields.io/github/tag/Maticzpl/MoreHazards.svg?colorB=2146bf&label=Version)](https://github.com/Maticzpl/MoreHazards/releases)
[![Github commits](https://img.shields.io/github/last-commit/Maticzpl/MoreHazards?color=green&label=Last%20Commit)](https://github.com/Maticzpl/MoreHazards/commits/main)
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
  # Elevator Breakdown Module
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
    chance_per_elevator: 30
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
  # Door Module - Once in a while a random door will close.
  doors:
    enabled: false
    # Timing of the door malfunction event
    random_event_timing:
    # Min Delay between calling this event
      min_delay: 10
      # Max Delay between calling this event
      max_delay: 30
    # Chance of a door closing per player
    per_player_chance: 20
  # Show debug messages in console.
  debug: false
```

