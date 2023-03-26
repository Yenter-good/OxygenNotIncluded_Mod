using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Wire;

namespace OxygenNotIncluded_Mod
{
    [HarmonyLib.HarmonyPatch(typeof(Wire), "GetMaxWattageAsFloat")]
    internal class 电线
    {
        [HarmonyLib.HarmonyPrefix]
        static bool Prefix(WattageRating rating, ref float __result)
        {
            switch (rating)
            {
                case WattageRating.Max500:
                    __result = 5000f;
                    break;
                case WattageRating.Max1000:
                    __result = 10000f;
                    break;
                case WattageRating.Max2000:
                    __result = 20000f;
                    break;
                case WattageRating.Max20000:
                    __result = 200000f;
                    break;
                case WattageRating.Max50000:
                    __result = 500000f;
                    break;
                default:
                    __result = 0;
                    break;
            }

            return false;
        }
    }
}