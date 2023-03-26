using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static STRINGS.BUILDINGS.PREFABS;

namespace OxygenNotIncluded_Mod
{
    [HarmonyLib.HarmonyPatch(typeof(HydroponicFarmConfig), "ConfigureBuildingTemplate")]
    internal class 液培砖
    {
        [HarmonyLib.HarmonyPostfix]
        static void Postfix(GameObject go)
        {
            ConduitConsumer conduitConsumer = go.GetComponent<ConduitConsumer>();
            conduitConsumer.consumptionRate = 0.1f;
        }
    }
}
