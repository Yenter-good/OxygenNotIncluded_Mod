using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace OxygenNotIncluded_Mod
{
    [HarmonyLib.HarmonyPatch(typeof(LiquidPumpConfig), "DoPostConfigureComplete")]
    internal class 抽水机
    {
        static void Postfix(GameObject go)
        {
            go.GetComponent<Storage>().capacityKg = 40f;

            ElementConsumer elementConsumer = go.GetComponent<ElementConsumer>();
            elementConsumer.configuration = ElementConsumer.Configuration.AllLiquid;
            elementConsumer.consumptionRate = 20f;
            elementConsumer.storeOnConsume = true;
            elementConsumer.showInStatusPanel = false;
            elementConsumer.consumptionRadius = 8;
        }
    }

    [HarmonyLib.HarmonyPatch(typeof(Game), "OnPrefabInit")]
    internal class 水管
    {
        [HarmonyLib.HarmonyPostfix]
        static void Postfix(Game __instance)
        {
            var flow = Traverse.Create(__instance).Field("liquidConduitFlow").GetValue<ConduitFlow>();
            var traverse = Traverse.Create(flow);
            traverse.Field("MaxMass").SetValue(20f);
        }
    }

    [HarmonyPatch(typeof(LiquidVentConfig), "ConfigureBuildingTemplate")]
    internal class 排水
    {
        [HarmonyLib.HarmonyPostfix]
        static void Postfix(GameObject go)
        {
            Vent vent = go.GetComponent<Vent>();
            vent.overpressureMass = 10000f;
        }
    }

    [HarmonyPatch(typeof(GasPumpConfig), "DoPostConfigureComplete")]
    internal class 抽气机
    {
        [HarmonyLib.HarmonyPostfix]
        static void Postfix(GameObject go)
        {
            go.GetComponent<Storage>().capacityKg = 4f;
            ElementConsumer elementConsumer = go.GetComponent<ElementConsumer>();
            elementConsumer.consumptionRate = 2f;
            elementConsumer.consumptionRadius = 8;
        }
    }
    [HarmonyPatch(typeof(Game), "OnPrefabInit")]
    internal class 气管
    {
        [HarmonyLib.HarmonyPostfix]
        static void Postfix(Game __instance)
        {
            var flow = Traverse.Create(__instance).Field("gasConduitFlow").GetValue<ConduitFlow>();
            var traverse = Traverse.Create(flow);
            traverse.Field("MaxMass").SetValue(2f);
        }
    }
    [HarmonyPatch(typeof(GasVentConfig), "ConfigureBuildingTemplate")]
    internal class 排气
    {
        [HarmonyLib.HarmonyPostfix]
        static void Postfix(GameObject go)
        {
            Vent vent = go.AddOrGet<Vent>();
            vent.conduitType = ConduitType.Gas;
            vent.overpressureMass = 10f;
        }
    }
}
