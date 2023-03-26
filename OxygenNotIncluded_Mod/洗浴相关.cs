using rail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TUNING;
using UnityEngine;
using static ResearchTypes;
using static STRINGS.BUILDINGS.PREFABS;

namespace OxygenNotIncluded_Mod
{
    [HarmonyLib.HarmonyPatch(typeof(OuthouseConfig), "ConfigureBuildingTemplate")]
    internal class 旱厕
    {
        [HarmonyLib.HarmonyPostfix]
        static void Postfix(GameObject go)
        {
            Toilet toilet = go.GetComponent<Toilet>();
            toilet.maxFlushes = 60;
            toilet.dirtUsedPerFlush = 1f;
            toilet.solidWastePerUse = new Toilet.SpawnInfo(SimHashes.OxyRock, 1f, 0f);
            toilet.solidWasteTemperature = 296.15f;
            toilet.diseaseId = "FoodPoisoning";
            toilet.diseasePerFlush = 0;
            toilet.diseaseOnDupePerFlush = 0;

            ToiletWorkableClean toiletWorkableClean = go.GetComponent<ToiletWorkableClean>();
            toiletWorkableClean.workTime = 10f;
        }
    }

    [HarmonyLib.HarmonyPatch(typeof(FlushToiletConfig), "ConfigureBuildingTemplate")]
    internal class 抽水马桶
    {
        [HarmonyLib.HarmonyPostfix]
        static void Postfix(GameObject go)
        {
            FlushToilet flushToilet = go.GetComponent<FlushToilet>();
            flushToilet.massConsumedPerUse = 5f;
            flushToilet.massEmittedPerUse = 11.7f;
            flushToilet.newPeeTemperature = 296.15f;
            flushToilet.diseaseId = "FoodPoisoning";
            flushToilet.diseasePerFlush = 0;
            flushToilet.diseaseOnDupePerFlush = 0;
        }
    }

    [HarmonyLib.HarmonyPatch(typeof(ShowerConfig), "ConfigureBuildingTemplate")]
    internal class 淋浴房
    {
        [HarmonyLib.HarmonyPostfix]
        static void Postfix(GameObject go)
        {
            Shower shower = go.GetComponent<Shower>();
            shower.workTime = 5f;
            shower.outputTargetElement = SimHashes.DirtyWater;
            shower.fractionalDiseaseRemoval = 1f;
            shower.absoluteDiseaseRemoval = -200000;
        }
    }

    [HarmonyLib.HarmonyPatch(typeof(ShowerConfig), "CreateBuildingDef")]
    internal class 淋浴房1
    {
        [HarmonyLib.HarmonyPrefix]
        static bool Prefix(ref BuildingDef __result)
        {
            __result = BuildingTemplates.CreateBuildingDef("Shower", 2, 3, "shower_kanim", 30, 30f, BUILDINGS.CONSTRUCTION_MASS_KG.TIER4, MATERIALS.RAW_METALS, 1600f, BuildLocationRule.OnFloor, noise: NOISE_POLLUTION.NOISY.TIER3, decor: BUILDINGS.DECOR.BONUS.TIER1);
            __result.Overheatable = false;
            __result.ExhaustKilowattsWhenActive = 0.25f;
            __result.InputConduitType = ConduitType.Liquid;
            __result.OutputConduitType = ConduitType.Liquid;
            __result.ViewMode = OverlayModes.LiquidConduits.ID;
            __result.AudioCategory = "Metal";
            __result.UtilityInputOffset = new CellOffset(0, 0);
            __result.UtilityOutputOffset = new CellOffset(1, 1);

            return false;
        }
    }
}
