using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TUNING;
using UnityEngine;

namespace OxygenNotIncluded_Mod
{
    [HarmonyLib.HarmonyPatch(typeof(GeneratorConfig), "CreateBuildingDef")]
    internal class 煤炭发电机
    {
        [HarmonyLib.HarmonyPostfix]
        static void Postfix(BuildingDef __result)
        {
            __result.GeneratorWattageRating = 9999;
            __result.ExhaustKilowattsWhenActive = 0;
            __result.SelfHeatKilowattsWhenActive = 0;
        }
    }

    [HarmonyLib.HarmonyPatch(typeof(GeneratorConfig), "ConfigureBuildingTemplate")]
    internal class 煤炭发电机1
    {
        [HarmonyLib.HarmonyPostfix]
        static void Postfix(GameObject go)
        {
            var generator = go.GetComponent<EnergyGenerator>();
            generator.formula = EnergyGenerator.CreateSimpleFormula(SimHashes.Carbon.CreateTag(), 0.1f, 600f, SimHashes.CarbonDioxide, 0.002f, false, new CellOffset(1, 2), 296.15f);
        }
    }

    [HarmonyLib.HarmonyPatch(typeof(ManualGeneratorConfig), "CreateBuildingDef")]
    internal class 人力发电机
    {
        static void Postfix(BuildingDef __result)
        {
            __result.GeneratorWattageRating = 4000f;
        }
    }
}
