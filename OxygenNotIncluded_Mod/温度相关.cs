using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using YamlDotNet.Core.Tokens;

namespace OxygenNotIncluded_Mod
{
    [HarmonyLib.HarmonyPatch(typeof(AirConditioner), "UpdateState")]
    internal class 空调不产热
    {
        [HarmonyLib.HarmonyPrefix]
        static bool Prefix(float dt, AirConditioner __instance, ref Storage ___storage, ref bool ___isLiquidConditioner, ref float ___temperatureDelta, ref int ___cooledAirOutputCell, ref Operational ___operational)
        {
            List<GameObject> items = ___storage.items;
            for (int i = 0; i < items.Count; i++)
            {
                PrimaryElement component = items[i].GetComponent<PrimaryElement>();
                if (component.Mass > 0f && (!___isLiquidConditioner || !component.Element.IsGas) && (___isLiquidConditioner || !component.Element.IsLiquid))
                {
                    float num = component.Temperature + ___temperatureDelta;

                    float num2 = (___isLiquidConditioner ? Game.Instance.liquidConduitFlow : Game.Instance.gasConduitFlow).AddElement(___cooledAirOutputCell, component.ElementID, component.Mass, num, component.DiseaseIdx, component.DiseaseCount);
                    component.KeepZeroMassObject = true;
                    int num4 = component.DiseaseCount;
                    component.Mass -= num2;
                    component.ModifyDiseaseCount(-num4, "AirConditioner.UpdateState");

                    component.Temperature = num;

                    break;
                }
            }

            ___operational.SetActive(true, false);
            Traverse.Create(__instance).Method("UpdateStatus").GetValue();

            return false;
        }
    }

    [HarmonyPatch(typeof(LiquidConditionerConfig), "ConfigureBuildingTemplate")]
    internal class 液体冷却
    {
        [HarmonyLib.HarmonyPostfix]
        static void Postfix(GameObject go)
        {
            ConduitConsumer conduitConsumer = go.GetComponent<ConduitConsumer>();
            conduitConsumer.consumptionRate = 20f;
        }
    }

    [HarmonyPatch(typeof(AirConditionerConfig), "ConfigureBuildingTemplate")]
    internal class 空气冷却
    {
        [HarmonyPostfix]
        static void Postfix(GameObject go)
        {
            ConduitConsumer conduitConsumer = go.GetComponent<ConduitConsumer>();
            conduitConsumer.consumptionRate = 2f;
        }
    }
}
