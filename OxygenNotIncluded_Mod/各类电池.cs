using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static STRINGS.BUILDINGS.PREFABS;

namespace OxygenNotIncluded_Mod
{
    [HarmonyLib.HarmonyPatch(typeof(BatteryConfig), "DoPostConfigureComplete")]
    internal class 小电池
    {
        [HarmonyLib.HarmonyPostfix]
        static void Postfix(GameObject go)
        {
            Battery battery = go.GetComponent<Battery>();
            battery.capacity = 90000f;
            battery.joulesLostPerSecond = 0;
        }
    }

    [HarmonyLib.HarmonyPatch(typeof(BatteryMediumConfig), "DoPostConfigureComplete")]
    internal class 大电池
    {
        [HarmonyLib.HarmonyPostfix]
        static void Postfix(GameObject go)
        {
            Battery battery = go.GetComponent<Battery>();
            battery.capacity = 900000f;
            battery.joulesLostPerSecond = 0f;
        }
    }

    [HarmonyLib.HarmonyPatch(typeof(BatterySmartConfig), "DoPostConfigureComplete")]
    internal class 智能电池
    {
        [HarmonyLib.HarmonyPostfix]
        static void Postfix(GameObject go)
        {
            BatterySmart battery = go.GetComponent<BatterySmart>();
            battery.capacity = 9000000f;
            battery.joulesLostPerSecond = 0;
        }
    }
}
