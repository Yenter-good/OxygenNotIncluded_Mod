using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OxygenNotIncluded_Mod
{
    [HarmonyLib.HarmonyPatch(typeof(GeyserGenericConfig), "GenerateConfigs")]
    internal class 喷泉修改
    {
        [HarmonyLib.HarmonyPostfix]
        static void Postfix(List<GeyserGenericConfig.GeyserPrefabParams> __result)
        {
            var steam = __result.Find(p => p.anim == "geyser_gas_steam_kanim");
            var steamType = steam.geyserType;
            steamType.temperature = 296.15f;
            steamType.minRatePerCycle = 2000;
            steamType.maxRatePerCycle = 3000;
            steamType.minIterationLength = 2000;
            steamType.maxIterationLength = 3000;
            steamType.minIterationPercent = 0.9f;
            steamType.maxIterationPercent = 1f;
            steamType.minYearPercent = 0.9f;
            steamType.maxYearPercent = 1f;
            steamType.geyserTemperature = 296.15f;

            var hotsteam = __result.Find(p => p.anim == "geyser_gas_steam_hot_kanim");
            var hotsteamType = hotsteam.geyserType;
            hotsteamType.temperature = 296.15f;
            hotsteamType.minRatePerCycle = 2000;
            hotsteamType.maxRatePerCycle = 3000;
            hotsteamType.minIterationLength = 2000;
            hotsteamType.maxIterationLength = 3000;
            hotsteamType.minIterationPercent = 0.9f;
            hotsteamType.maxIterationPercent = 1f;
            hotsteamType.minYearPercent = 0.9f;
            hotsteamType.maxYearPercent = 1f;
            hotsteamType.geyserTemperature = 296.15f;

            var hotwatersteam = __result.Find(p => p.anim == "geyser_liquid_water_hot_kanim");
            var hotwatersteamType = hotsteam.geyserType;
            hotwatersteamType.temperature = 296.15f;
            hotwatersteamType.minRatePerCycle = 2000;
            hotwatersteamType.maxRatePerCycle = 3000;
            hotwatersteamType.minIterationLength = 2000;
            hotwatersteamType.maxIterationLength = 3000;
            hotwatersteamType.minIterationPercent = 0.9f;
            hotwatersteamType.maxIterationPercent = 1f;
            hotwatersteamType.minYearPercent = 0.9f;
            hotwatersteamType.maxYearPercent = 1f;
            hotwatersteamType.geyserTemperature = 296.15f;
        }
    }
}
