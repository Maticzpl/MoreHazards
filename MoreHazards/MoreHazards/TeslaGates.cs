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

namespace MoreHazards
{
    public class TeslaGateManager
    {
        private static Dictionary<Vector3,bool> TeslaStates = new Dictionary<Vector3, bool>();
        public static List<RoleType> IgnoredByTesla { get; } = new List<RoleType>();

        private static readonly TeslaConfig Config = MoreHazards.Instance.Config.Tesla;
        public static void LoadRolesFromConfig(Config config)
        {
            IgnoredByTesla.Clear();
            foreach (var role in config.Tesla.IgnoredRoles)
            {
                IgnoredByTesla.Add(role);
            }
        }

        public static void SetTeslaEnabled(TeslaGate tesla,bool state)
        {
            bool exists = TeslaStates.ContainsKey(tesla.gameObject.transform.position);

            if (exists)
                TeslaStates[tesla.gameObject.transform.position] = state;
            else
                TeslaStates.Add(tesla.gameObject.transform.position, state);
        }

        public static void DisableRandomGates(short ChancePerGate, short MaxDisabledGates, short GatesRequired)
        {
            if (Map.TeslaGates.Count < GatesRequired)
                return;

            short DisabledGates = 0;
            foreach (var tesla in Map.TeslaGates)
            {
                if (DisabledGates == MaxDisabledGates)
                    return;

                if (RandomGenerator.GetInt16(0, 100) > ChancePerGate)
                    return;

                DisabledGates++;
                SetTeslaEnabled(tesla, false);
            }
            
        }

        public static void OnRoundStart()
        {

            DisableRandomGates(
                Config.TeslaGateDisableChance,
                Config.MaxDisabledTeslaGates,
                Config.MinTeslaGates
                );
        }
        public static void OnRoundEnd(RoundEndedEventArgs ev)
        {
            TeslaStates = new Dictionary<Vector3, bool>();
            IgnoredByTesla.Clear();
        }

        public static void OnTeslaTrigger(TriggeringTeslaEventArgs ev)
        {
            //If module is disabled
            if (!Config.Enabled)
                return;


            if (IgnoredByTesla.Contains(ev.Player.Role))
            {
                ev.IsTriggerable = false;
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
