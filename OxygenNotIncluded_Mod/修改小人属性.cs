using Database;
using HarmonyLib;
using STRINGS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TUNING;
using UnityEngine;

namespace OxygenNotIncluded_Mod
{

    [HarmonyLib.HarmonyPatch(typeof(MinionStartingStats), "GenerateAptitudes")]
    internal class 小人技能
    {
        [HarmonyLib.HarmonyPrefix]
        static bool Prefix(string guaranteedAptitudeID, MinionStartingStats __instance)
        {
            int num = UnityEngine.Random.Range(3, 4);
            List<SkillGroup> list = new List<SkillGroup>(Db.Get().SkillGroups.resources);
            list.Shuffle<SkillGroup>();
            if (guaranteedAptitudeID != null)
            {
                __instance.skillAptitudes.Add(Db.Get().SkillGroups.Get(guaranteedAptitudeID), 5);
                list.Remove(Db.Get().SkillGroups.Get(guaranteedAptitudeID));
                num--;
            }
            for (int i = 0; i < num; i++)
            {
                __instance.skillAptitudes.Add(list[i], 5);
            }

            return false;
        }
    }

    [HarmonyLib.HarmonyPatch(typeof(AttributeConverters), HarmonyLib.MethodType.Constructor)]
    internal class 小人属性
    {
        [HarmonyLib.HarmonyPostfix]
        static void Postfix(AttributeConverters __instance)
        {
            __instance.CarryAmountFromStrength.multiplier = 400;
            __instance.HarvestSpeed.multiplier = 0.5f;
            __instance.ArtSpeed.multiplier = 1;
        }
    }

    [HarmonyPatch(typeof(MinionStartingStats), "ApplyAptitudes")]
    internal class 每学习一个技能的士气加值
    {
        [HarmonyLib.HarmonyPrefix]
        public static bool Prefix(GameObject go)
        {
            MinionResume component = go.GetComponent<MinionResume>();
            foreach (SkillGroup skill in new List<SkillGroup>(Db.Get().SkillGroups.resources))
            {
                component.SetAptitude(skill.Id, 15f);
            }
            return false;
        }
    }
}
