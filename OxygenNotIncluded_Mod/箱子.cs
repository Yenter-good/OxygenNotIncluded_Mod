using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OxygenNotIncluded_Mod
{
    [HarmonyLib.HarmonyPatch(typeof(Storage), "OnPrefabInit")]
    internal class 箱子
    {
        [HarmonyLib.HarmonyPrefix]
        static bool Prefix(Storage __instance)
        {
            __instance.capacityKg = 3000000f;

            return true;
        }
    }
}
