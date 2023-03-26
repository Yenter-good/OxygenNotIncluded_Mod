﻿using STRINGS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace OxygenNotIncluded_Mod
{
    [HarmonyLib.HarmonyPatch(typeof(RockCrusherConfig), "ConfigureBuildingTemplate")]
    internal class 碎石机
    {
        [HarmonyLib.HarmonyPrefix]
        static bool Prefix(GameObject go)
        {
            go.AddOrGet<DropAllWorkable>();
            go.AddOrGet<BuildingComplete>().isManuallyOperated = true;
            ComplexFabricator complexFabricator = go.AddOrGet<ComplexFabricator>();
            complexFabricator.sideScreenStyle = ComplexFabricatorSideScreen.StyleSetting.ListQueueHybrid;
            complexFabricator.duplicantOperated = true;
            go.AddOrGet<FabricatorIngredientStatusManager>();
            go.AddOrGet<CopyBuildingSettings>();
            ComplexFabricatorWorkable complexFabricatorWorkable = go.AddOrGet<ComplexFabricatorWorkable>();
            BuildingTemplates.CreateComplexFabricatorStorage(go, complexFabricator);
            complexFabricatorWorkable.overrideAnims = new KAnimFile[1] { Assets.GetAnim("anim_interacts_rockrefinery_kanim") };
            complexFabricatorWorkable.workingPstComplete = new HashedString[1] { "working_pst_complete" };
            Tag tag = SimHashes.Sand.CreateTag();
            foreach (Element item in ElementLoader.elements.FindAll((Element e) => e.HasTag(GameTags.Crushable)))
            {
                ComplexRecipe.RecipeElement[] array = new ComplexRecipe.RecipeElement[1]
                {
                new ComplexRecipe.RecipeElement(item.tag, 100f)
                };
                ComplexRecipe.RecipeElement[] array2 = new ComplexRecipe.RecipeElement[1]
                {
                new ComplexRecipe.RecipeElement(tag, 100f)
                };
                string obsolete_id = ComplexRecipeManager.MakeObsoleteRecipeID("RockCrusher", item.tag);
                string text = ComplexRecipeManager.MakeRecipeID("RockCrusher", array, array2);
                new ComplexRecipe(text, array, array2)
                {
                    time = 10f,
                    description = string.Format(STRINGS.BUILDINGS.PREFABS.ROCKCRUSHER.RECIPE_DESCRIPTION, item.name, tag.ProperName()),
                    nameDisplay = ComplexRecipe.RecipeNameDisplay.IngredientToResult,
                    fabricators = new List<Tag> { TagManager.Create("RockCrusher") }
                };
                ComplexRecipeManager.Get().AddObsoleteIDMapping(obsolete_id, text);
            }

            foreach (Element item2 in ElementLoader.elements.FindAll((Element e) => e.IsSolid && e.HasTag(GameTags.Metal)))
            {
                if (!item2.HasTag(GameTags.Noncrushable))
                {
                    Element lowTempTransition = item2.highTempTransition.lowTempTransition;
                    if (lowTempTransition != item2)
                    {
                        ComplexRecipe.RecipeElement[] array3 = new ComplexRecipe.RecipeElement[1]
                        {
                            new ComplexRecipe.RecipeElement(item2.tag, 100f)
                        };
                        ComplexRecipe.RecipeElement[] array4 = new ComplexRecipe.RecipeElement[1]
                        {
                            new ComplexRecipe.RecipeElement(lowTempTransition.tag, 150f)
                        };
                        string obsolete_id2 = ComplexRecipeManager.MakeObsoleteRecipeID("RockCrusher", lowTempTransition.tag);
                        string text2 = ComplexRecipeManager.MakeRecipeID("RockCrusher", array3, array4);
                        new ComplexRecipe(text2, array3, array4)
                        {
                            time = 10f,
                            description = string.Format(STRINGS.BUILDINGS.PREFABS.ROCKCRUSHER.METAL_RECIPE_DESCRIPTION, lowTempTransition.name, item2.name),
                            nameDisplay = ComplexRecipe.RecipeNameDisplay.IngredientToResult,
                            fabricators = new List<Tag> { TagManager.Create("RockCrusher") }
                        };
                        ComplexRecipeManager.Get().AddObsoleteIDMapping(obsolete_id2, text2);
                    }
                }
            }

            Element element = ElementLoader.FindElementByHash(SimHashes.Lime);
            ComplexRecipe.RecipeElement[] array5 = new ComplexRecipe.RecipeElement[1]
            {
            new ComplexRecipe.RecipeElement("EggShell", 5f)
            };
            ComplexRecipe.RecipeElement[] array6 = new ComplexRecipe.RecipeElement[1]
            {
            new ComplexRecipe.RecipeElement(ElementLoader.FindElementByHash(SimHashes.Lime).tag, 5f, ComplexRecipe.RecipeElement.TemperatureOperation.AverageTemperature)
            };
            string obsolete_id3 = ComplexRecipeManager.MakeObsoleteRecipeID("RockCrusher", element.tag);
            string text3 = ComplexRecipeManager.MakeRecipeID("RockCrusher", array5, array6);
            new ComplexRecipe(text3, array5, array6)
            {
                time = 10f,
                description = string.Format(STRINGS.BUILDINGS.PREFABS.ROCKCRUSHER.LIME_RECIPE_DESCRIPTION, SimHashes.Lime.CreateTag().ProperName(), MISC.TAGS.EGGSHELL),
                nameDisplay = ComplexRecipe.RecipeNameDisplay.IngredientToResult,
                fabricators = new List<Tag> { TagManager.Create("RockCrusher") }
            };
            ComplexRecipeManager.Get().AddObsoleteIDMapping(obsolete_id3, text3);
            Element element2 = ElementLoader.FindElementByHash(SimHashes.Lime);
            ComplexRecipe.RecipeElement[] array7 = new ComplexRecipe.RecipeElement[1]
            {
            new ComplexRecipe.RecipeElement("BabyCrabShell", 1f)
            };
            ComplexRecipe.RecipeElement[] array8 = new ComplexRecipe.RecipeElement[1]
            {
            new ComplexRecipe.RecipeElement(element2.tag, 5f, ComplexRecipe.RecipeElement.TemperatureOperation.AverageTemperature)
            };
            new ComplexRecipe(ComplexRecipeManager.MakeRecipeID("RockCrusher", array7, array8), array7, array8)
            {
                time = 10f,
                description = string.Format(STRINGS.BUILDINGS.PREFABS.ROCKCRUSHER.LIME_RECIPE_DESCRIPTION, SimHashes.Lime.CreateTag().ProperName(), ITEMS.INDUSTRIAL_PRODUCTS.CRAB_SHELL.NAME),
                nameDisplay = ComplexRecipe.RecipeNameDisplay.IngredientToResult,
                fabricators = new List<Tag> { TagManager.Create("RockCrusher") }
            };
            Element element3 = ElementLoader.FindElementByHash(SimHashes.Lime);
            ComplexRecipe.RecipeElement[] array9 = new ComplexRecipe.RecipeElement[1]
            {
            new ComplexRecipe.RecipeElement("CrabShell", 1f)
            };
            ComplexRecipe.RecipeElement[] array10 = new ComplexRecipe.RecipeElement[1]
            {
            new ComplexRecipe.RecipeElement(element3.tag, 10f, ComplexRecipe.RecipeElement.TemperatureOperation.AverageTemperature)
            };
            new ComplexRecipe(ComplexRecipeManager.MakeRecipeID("RockCrusher", array9, array10), array9, array10)
            {
                time = 10f,
                description = string.Format(STRINGS.BUILDINGS.PREFABS.ROCKCRUSHER.LIME_RECIPE_DESCRIPTION, SimHashes.Lime.CreateTag().ProperName(), ITEMS.INDUSTRIAL_PRODUCTS.CRAB_SHELL.NAME),
                nameDisplay = ComplexRecipe.RecipeNameDisplay.IngredientToResult,
                fabricators = new List<Tag> { TagManager.Create("RockCrusher") }
            };
            ComplexRecipe.RecipeElement[] array11 = new ComplexRecipe.RecipeElement[1]
            {
            new ComplexRecipe.RecipeElement("BabyCrabWoodShell", 1f)
            };
            ComplexRecipe.RecipeElement[] array12 = new ComplexRecipe.RecipeElement[1]
            {
            new ComplexRecipe.RecipeElement("WoodLog", 10f, ComplexRecipe.RecipeElement.TemperatureOperation.AverageTemperature)
            };
            new ComplexRecipe(ComplexRecipeManager.MakeRecipeID("RockCrusher", array11, array12), array11, array12)
            {
                time = 10f,
                description = string.Format(STRINGS.BUILDINGS.PREFABS.ROCKCRUSHER.LIME_RECIPE_DESCRIPTION, WoodLogConfig.TAG.ProperName(), ITEMS.INDUSTRIAL_PRODUCTS.BABY_CRAB_SHELL.VARIANT_WOOD.NAME),
                nameDisplay = ComplexRecipe.RecipeNameDisplay.IngredientToResult,
                fabricators = new List<Tag> { TagManager.Create("RockCrusher") }
            };
            float num = 5f;
            ComplexRecipe.RecipeElement[] array13 = new ComplexRecipe.RecipeElement[1]
            {
            new ComplexRecipe.RecipeElement("CrabWoodShell", num)
            };
            ComplexRecipe.RecipeElement[] array14 = new ComplexRecipe.RecipeElement[1]
            {
            new ComplexRecipe.RecipeElement("WoodLog", 100f * num, ComplexRecipe.RecipeElement.TemperatureOperation.AverageTemperature)
            };
            new ComplexRecipe(ComplexRecipeManager.MakeRecipeID("RockCrusher", array13, array14), array13, array14)
            {
                time = 10f,
                description = string.Format(STRINGS.BUILDINGS.PREFABS.ROCKCRUSHER.LIME_RECIPE_DESCRIPTION, WoodLogConfig.TAG.ProperName(), ITEMS.INDUSTRIAL_PRODUCTS.CRAB_SHELL.VARIANT_WOOD.NAME),
                nameDisplay = ComplexRecipe.RecipeNameDisplay.IngredientToResult,
                fabricators = new List<Tag> { TagManager.Create("RockCrusher") }
            };
            ComplexRecipe.RecipeElement[] array15 = new ComplexRecipe.RecipeElement[1]
            {
            new ComplexRecipe.RecipeElement(ElementLoader.FindElementByHash(SimHashes.Fossil).tag, 100f)
            };
            ComplexRecipe.RecipeElement[] array16 = new ComplexRecipe.RecipeElement[2]
            {
            new ComplexRecipe.RecipeElement(ElementLoader.FindElementByHash(SimHashes.Lime).tag, 5f, ComplexRecipe.RecipeElement.TemperatureOperation.AverageTemperature),
            new ComplexRecipe.RecipeElement(ElementLoader.FindElementByHash(SimHashes.SedimentaryRock).tag, 95f, ComplexRecipe.RecipeElement.TemperatureOperation.AverageTemperature)
            };
            new ComplexRecipe(ComplexRecipeManager.MakeRecipeID("RockCrusher", array15, array16), array15, array16)
            {
                time = 10f,
                description = string.Format(STRINGS.BUILDINGS.PREFABS.ROCKCRUSHER.LIME_FROM_LIMESTONE_RECIPE_DESCRIPTION, SimHashes.Fossil.CreateTag().ProperName(), SimHashes.SedimentaryRock.CreateTag().ProperName(), SimHashes.Lime.CreateTag().ProperName()),
                nameDisplay = ComplexRecipe.RecipeNameDisplay.IngredientToResult,
                fabricators = new List<Tag> { TagManager.Create("RockCrusher") }
            };
            float num2 = 5E-05f;
            ComplexRecipe.RecipeElement[] array17 = new ComplexRecipe.RecipeElement[1]
            {
            new ComplexRecipe.RecipeElement(SimHashes.Salt.CreateTag(), 100f)
            };
            ComplexRecipe.RecipeElement[] array18 = new ComplexRecipe.RecipeElement[2]
            {
            new ComplexRecipe.RecipeElement(TableSaltConfig.ID.ToTag(), 100f * num2, ComplexRecipe.RecipeElement.TemperatureOperation.AverageTemperature),
            new ComplexRecipe.RecipeElement(SimHashes.Sand.CreateTag(), 100f * (1f - num2), ComplexRecipe.RecipeElement.TemperatureOperation.AverageTemperature)
            };
            new ComplexRecipe(ComplexRecipeManager.MakeRecipeID("RockCrusher", array17, array18), array17, array18)
            {
                time = 10f,
                description = string.Format(STRINGS.BUILDINGS.PREFABS.ROCKCRUSHER.RECIPE_DESCRIPTION, SimHashes.Salt.CreateTag().ProperName(), ITEMS.INDUSTRIAL_PRODUCTS.TABLE_SALT.NAME),
                nameDisplay = ComplexRecipe.RecipeNameDisplay.IngredientToResult,
                fabricators = new List<Tag> { TagManager.Create("RockCrusher") }
            };
            if (ElementLoader.FindElementByHash(SimHashes.Graphite) != null)
            {
                float num3 = 0.9f;
                ComplexRecipe.RecipeElement[] array19 = new ComplexRecipe.RecipeElement[1]
                {
                new ComplexRecipe.RecipeElement(SimHashes.Fullerene.CreateTag(), 100f)
                };
                ComplexRecipe.RecipeElement[] array20 = new ComplexRecipe.RecipeElement[2]
                {
                new ComplexRecipe.RecipeElement(SimHashes.Graphite.CreateTag(), 100f * num3, ComplexRecipe.RecipeElement.TemperatureOperation.AverageTemperature),
                new ComplexRecipe.RecipeElement(SimHashes.Sand.CreateTag(), 100f * (1f - num3), ComplexRecipe.RecipeElement.TemperatureOperation.AverageTemperature)
                };
                new ComplexRecipe(ComplexRecipeManager.MakeRecipeID("RockCrusher", array19, array20), array19, array20)
                {
                    time = 10f,
                    description = string.Format(STRINGS.BUILDINGS.PREFABS.ROCKCRUSHER.RECIPE_DESCRIPTION, SimHashes.Fullerene.CreateTag().ProperName(), SimHashes.Graphite.CreateTag().ProperName()),
                    nameDisplay = ComplexRecipe.RecipeNameDisplay.IngredientToResult,
                    fabricators = new List<Tag> { TagManager.Create("RockCrusher") }
                };
            }

            Prioritizable.AddRef(go);

            return false;
        }
    }


}
