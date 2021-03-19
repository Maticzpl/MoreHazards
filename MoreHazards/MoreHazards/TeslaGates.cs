using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Exiled.API.Enums;
using Exiled.API.Extensions;
using Exiled.API.Features;
using Exiled.Events.EventArgs;
using HarmonyLib;
using Respawning;
using UnityEngine;
using Map = Exiled.API.Features.Map;
using Player = Exiled.Events.Handlers.Player;

namespace MoreHazards
{
    public class TeslaGateManager : LogicManager
    {
        private Dictionary<Vector3,bool> TeslaStates = new Dictionary<Vector3, bool>();
        public List<RoleType> IgnoredByTesla { get; } = new List<RoleType>();

        private readonly TeslaConfig Config = MoreHazards.Instance.Config.Tesla;
        public void LoadRolesFromConfig(Config config)
        {
            IgnoredByTesla.Clear();
            foreach (var role in config.Tesla.IgnoredRoles)
            {
                Log.Debug($"Tesla ignore role: {role}", MoreHazards.Instance.Config.Debug);
                IgnoredByTesla.Add(role);
            }
        }

        public void SetTeslaEnabled(TeslaGate tesla,bool state)
        {
            bool exists = TeslaStates.ContainsKey(tesla.gameObject.transform.position);

            if (exists)
                TeslaStates[tesla.gameObject.transform.position] = state;
            else
                TeslaStates.Add(tesla.gameObject.transform.position, state);
        }

        public void DisableRandomGates(short ChancePerGate, short MaxDisabledGates, short GatesRequired)
        {
            if (Map.TeslaGates.Count < GatesRequired)
                return;

            short DisabledGates = 0;
            foreach (var tesla in Map.TeslaGates)
            {
                if (DisabledGates == MaxDisabledGates)
                    break;

                if (RandomGenerator.GetInt16(0, 100) > ChancePerGate)
                    continue;

                DisabledGates++;
                SetTeslaEnabled(tesla, false);
            }
            
            Log.Debug($"Disabled {DisabledGates} tesla gates", MoreHazards.Instance.Config.Debug);

        }

        public TeslaGateManager()
        {
            Player.TriggeringTesla += OnTeslaTrigger;
            LoadRolesFromConfig(MoreHazards.Instance.Config);
        }

        public override void Dispose()
        {
            base.Dispose();
            Player.TriggeringTesla -= OnTeslaTrigger;
        }

        public override void OnRoundStart()
        {
            DisableRandomGates(
                Config.TeslaGateDisableChance,
                Config.MaxDisabledTeslaGates,
                Config.MinTeslaGates
                );
        }
        public override void OnRoundEnd(RoundEndedEventArgs ev)
        {
            TeslaStates = new Dictionary<Vector3, bool>();
            IgnoredByTesla.Clear();
        }

        public void OnTeslaTrigger(TriggeringTeslaEventArgs ev)
        {
            //If module is disabled
            if (!Config.Enabled)
                return;


            if (IgnoredByTesla.Contains(ev.Player.Role))
            {
                ev.IsTriggerable = false;
                Log.Debug($"Tesla ignored by player role", MoreHazards.Instance.Config.Debug);
                return;
            }
            
            if (!Config.DisablingTeslas)
                return;

            foreach (var tesla in TeslaStates)
            {
                //if tesla is active proceed normally
                if (tesla.Value)
                    continue;


                //Hopefully there is a better way to check that lmao
                if (Vector3.Distance(ev.Player.Position, tesla.Key) < 7)
                {
                    ev.IsTriggerable = false;
                    return;
                }
            }
        }

    }
}
