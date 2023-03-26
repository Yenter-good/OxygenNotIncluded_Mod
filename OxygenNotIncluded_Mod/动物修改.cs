using HarmonyLib;
using Klei.AI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateClasses;
using TUNING;
using UnityEngine;
using static ResearchTypes;
using static STRINGS.CREATURES.FERTILITY_MODIFIERS;

namespace OxygenNotIncluded_Mod
{
    [HarmonyLib.HarmonyPatch(typeof(BaseHatchConfig), "SetupDiet")]
    internal class 哈奇
    {
        [HarmonyLib.HarmonyPostfix]
        static void Postfix(GameObject __result)
        {
            CreatureCalorieMonitor.Def def = __result.AddOrGetDef<CreatureCalorieMonitor.Def>();
            def.minPoopSizeInCalories *= 50;
        }
    }

    [HarmonyLib.HarmonyPatch(typeof(PuftConfig), "CreatePuft")]
    internal class 喷浮飞鱼
    {
        [HarmonyLib.HarmonyPrefix]
        static bool Prefix(ref GameObject __result, string id, string name, string desc, string anim_file, bool is_baby)
        {
            GameObject prefab = BasePuftConfig.BasePuft(id, name, STRINGS.CREATURES.SPECIES.PUFT.DESC, "PuftBaseTrait", anim_file, is_baby, null, 288.15f, 328.15f);
            EntityTemplates.ExtendEntityToWildCreature(prefab, PuftTuning.PEN_SIZE_PER_CREATURE);
            Trait trait = Db.Get().CreateTrait("PuftBaseTrait", name, name, null, false, null, true, true);
            trait.Add(new AttributeModifier(Db.Get().Amounts.Calories.maxAttribute.Id, PuftTuning.STANDARD_STOMACH_SIZE, name, false, false, true));
            trait.Add(new AttributeModifier(Db.Get().Amounts.Calories.deltaAttribute.Id, -PuftTuning.STANDARD_CALORIES_PER_CYCLE / 60000f, name, false, false, true));
            trait.Add(new AttributeModifier(Db.Get().Amounts.HitPoints.maxAttribute.Id, 25f, name, false, false, true));
            trait.Add(new AttributeModifier(Db.Get().Amounts.Age.maxAttribute.Id, 75f, name, false, false, true));
            GameObject gameObject = BasePuftConfig.SetupDiet(prefab, SimHashes.ContaminatedOxygen.CreateTag(), SimHashes.OxyRock.CreateTag(), PuftTuning.STANDARD_CALORIES_PER_CYCLE / 500f, TUNING.CREATURES.CONVERSION_EFFICIENCY.GOOD_2 * 500, "SlimeLung", 1000f, 15f);
            gameObject.AddTag(GameTags.OriginalCreature);

            __result = gameObject;
            return false;
        }
    }

    [HarmonyLib.HarmonyPatch(typeof(GlomConfig), "CreatePrefab")]
    internal class 疫病章鱼
    {
        [HarmonyLib.HarmonyPostfix]
        static void Postfix(GameObject __result)
        {
            ElementDropperMonitor.Def def = __result.AddOrGetDef<ElementDropperMonitor.Def>();
            def.dirtyEmitElement = SimHashes.OxyRock;
            def.dirtyProbabilityPercent = 25f;
            def.dirtyCellToTargetMass = 1f;
            def.dirtyMassPerDirty = 0.2f;
            def.dirtyMassReleaseOnDeath = 3f;
        }
    }

    [HarmonyLib.HarmonyPatch(typeof(BasePacuConfig), "CreatePrefab")]
    internal class 帕库鱼
    {
        [HarmonyLib.HarmonyPostfix]
        static void Postfix(GameObject __result)
        {
            __result.AddOrGetDef<LureableMonitor.Def>().lures = new Tag[]
            {
                GameTags.Algae
            };

            Tag tag = SimHashes.Water.CreateTag();
            HashSet<Tag> hashSet = new HashSet<Tag>();
            hashSet.Add(SimHashes.Algae.CreateTag());
            hashSet.Add(SimHashes.Sand.CreateTag());
            hashSet.Add(SimHashes.SandStone.CreateTag());
            hashSet.Add(SimHashes.ToxicSand.CreateTag());
            hashSet.Add(SimHashes.Dirt.CreateTag());
            List<Diet.Info> list = new List<Diet.Info>();
            list.Add(new Diet.Info(hashSet, tag, PacuTuning.STANDARD_CALORIES_PER_CYCLE / 140f, TUNING.CREATURES.CONVERSION_EFFICIENCY.NORMAL * 500, null, 0f, false, false));
            list.AddRange(BasePacuConfig.SeedDiet(tag, PacuTuning.STANDARD_CALORIES_PER_CYCLE * 4f, TUNING.CREATURES.CONVERSION_EFFICIENCY.NORMAL * 500));
            Diet diet = new Diet(list.ToArray());
            CreatureCalorieMonitor.Def def2 = __result.AddOrGetDef<CreatureCalorieMonitor.Def>();
            def2.diet = diet;
            def2.minPoopSizeInCalories = PacuTuning.STANDARD_CALORIES_PER_CYCLE / 140 * 25f;
        }
    }

    [HarmonyLib.HarmonyPatch(typeof(LightBugConfig), "CreateLightBug")]
    internal class 发光虫
    {
        [HarmonyLib.HarmonyPostfix]
        static void Postfix(GameObject __result)
        {
            var consumed_tags = new HashSet<Tag>
            {
                SimHashes.Sand.CreateTag(),
                SimHashes.SandStone.CreateTag(),
                SimHashes.ToxicSand.CreateTag(),
                TagManager.Create(PrickleFruitConfig.ID),
                TagManager.Create("GrilledPrickleFruit"),
                SimHashes.Phosphorite.CreateTag()
            };

            Diet diet = new Diet(new Diet.Info[]
            {
                  new Diet.Info(consumed_tags, SimHashes.Copper.CreateTag(), LightBugTuning.STANDARD_CALORIES_PER_CYCLE*100, 1f, null, 0f, false, false)
            });
            __result.AddOrGetDef<CreatureCalorieMonitor.Def>().diet = diet;
            __result.AddOrGetDef<SolidConsumerMonitor.Def>().diet = diet;
        }
    }
}
