using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static STRINGS.BUILDINGS.PREFABS;

namespace OxygenNotIncluded_Mod
{
    [HarmonyLib.HarmonyPatch(typeof(DoorConfig), "DoPostConfigureComplete")]
    internal class 普通门
    {
        [HarmonyLib.HarmonyPostfix]
        static void Postfix(GameObject go)
        {
            Door door = go.GetComponent<Door>();
            door.unpoweredAnimSpeed = 3f;
            go.GetComponent<Workable>().workTime = 1f;
        }
    }

    [HarmonyLib.HarmonyPatch(typeof(ManualPressureDoorConfig), "ConfigureBuildingTemplate")]
    internal class 手动气闸
    {
        [HarmonyLib.HarmonyPostfix]
        static void Postfix(GameObject go)
        {
            Door door = go.GetComponent<Door>();
            door.unpoweredAnimSpeed = 3f;
            go.GetComponent<Workable>().workTime = 2f;
        }
    }

}
