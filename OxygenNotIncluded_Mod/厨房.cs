using STRINGS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TUNING;
using UnityEngine;

namespace OxygenNotIncluded_Mod
{
    [HarmonyLib.HarmonyPatch(typeof(CookingStationConfig), "ConfigureBuildingTemplate")]
    internal class 炉子
    {
        [HarmonyLib.HarmonyPrefix]
        static bool Prefix(GameObject go, Tag prefab_tag)
        {
            go.AddOrGet<BuildingComplete>().isManuallyOperated = true;
            CookingStation cookingStation = go.AddOrGet<CookingStation>();
            cookingStation.heatedTemperature = 368.15f;
            go.AddOrGet<FabricatorIngredientStatusManager>();
            go.AddOrGet<CopyBuildingSettings>();
            go.AddOrGet<ComplexFabricatorWorkable>().overrideAnims = new KAnimFile[1] { Assets.GetAnim("anim_interacts_cookstation_kanim") };
            cookingStation.sideScreenStyle = ComplexFabricatorSideScreen.StyleSetting.ListQueueHybrid;
            Prioritizable.AddRef(go);
            go.AddOrGet<DropAllWorkable>();
            ConfigureRecipes();
            go.AddOrGetDef<PoweredController.Def>();
            BuildingTemplates.CreateComplexFabricatorStorage(go, cookingStation);
            go.GetComponent<KPrefabID>().AddTag(RoomConstraints.ConstraintTags.CookTop);

            return false;
        }

        private static void ConfigureRecipes()
        {
            ComplexRecipe.RecipeElement[] array = new ComplexRecipe.RecipeElement[1]
            {
            new ComplexRecipe.RecipeElement("BasicPlantFood", 3f)
            };
            ComplexRecipe.RecipeElement[] array2 = new ComplexRecipe.RecipeElement[1]
            {
            new ComplexRecipe.RecipeElement("PickledMeal", 1f, ComplexRecipe.RecipeElement.TemperatureOperation.Heated)
            };
            PickledMealConfig.recipe = new ComplexRecipe(ComplexRecipeManager.MakeRecipeID("CookingStation", array, array2), array, array2)
            {
                time = 10,
                description = ITEMS.FOOD.PICKLEDMEAL.RECIPEDESC,
                nameDisplay = ComplexRecipe.RecipeNameDisplay.Result,
                fabricators = new List<Tag> { "CookingStation" },
                sortOrder = 21
            };
            ComplexRecipe.RecipeElement[] array3 = new ComplexRecipe.RecipeElement[1]
            {
            new ComplexRecipe.RecipeElement("MushBar", 1f)
            };
            ComplexRecipe.RecipeElement[] array4 = new ComplexRecipe.RecipeElement[1]
            {
            new ComplexRecipe.RecipeElement("FriedMushBar".ToTag(), 1f, ComplexRecipe.RecipeElement.TemperatureOperation.Heated)
            };
            FriedMushBarConfig.recipe = new ComplexRecipe(ComplexRecipeManager.MakeRecipeID("CookingStation", array3, array4), array3, array4)
            {
                time = 15,
                description = ITEMS.FOOD.FRIEDMUSHBAR.RECIPEDESC,
                nameDisplay = ComplexRecipe.RecipeNameDisplay.Result,
                fabricators = new List<Tag> { "CookingStation" },
                sortOrder = 1
            };
            ComplexRecipe.RecipeElement[] array5 = new ComplexRecipe.RecipeElement[1]
            {
            new ComplexRecipe.RecipeElement(MushroomConfig.ID, 1f)
            };
            ComplexRecipe.RecipeElement[] array6 = new ComplexRecipe.RecipeElement[1]
            {
            new ComplexRecipe.RecipeElement("FriedMushroom", 1f, ComplexRecipe.RecipeElement.TemperatureOperation.Heated)
            };
            FriedMushroomConfig.recipe = new ComplexRecipe(ComplexRecipeManager.MakeRecipeID("CookingStation", array5, array6), array5, array6)
            {
                time = 15,
                description = ITEMS.FOOD.FRIEDMUSHROOM.RECIPEDESC,
                nameDisplay = ComplexRecipe.RecipeNameDisplay.Result,
                fabricators = new List<Tag> { "CookingStation" },
                sortOrder = 20
            };
            ComplexRecipe.RecipeElement[] array7 = new ComplexRecipe.RecipeElement[1]
            {
            new ComplexRecipe.RecipeElement("Meat", 2f)
            };
            ComplexRecipe.RecipeElement[] array8 = new ComplexRecipe.RecipeElement[1]
            {
            new ComplexRecipe.RecipeElement("CookedMeat", 5f, ComplexRecipe.RecipeElement.TemperatureOperation.Heated)
            };
            CookedMeatConfig.recipe = new ComplexRecipe(ComplexRecipeManager.MakeRecipeID("CookingStation", array7, array8), array7, array8)
            {
                time = 15,
                description = ITEMS.FOOD.COOKEDMEAT.RECIPEDESC,
                nameDisplay = ComplexRecipe.RecipeNameDisplay.Result,
                fabricators = new List<Tag> { "CookingStation" },
                sortOrder = 21
            };
            ComplexRecipe.RecipeElement[] array9 = new ComplexRecipe.RecipeElement[1]
            {
            new ComplexRecipe.RecipeElement("FishMeat", 1f)
            };
            ComplexRecipe.RecipeElement[] array10 = new ComplexRecipe.RecipeElement[1]
            {
            new ComplexRecipe.RecipeElement("CookedFish", 5f, ComplexRecipe.RecipeElement.TemperatureOperation.Heated)
            };
            CookedMeatConfig.recipe = new ComplexRecipe(ComplexRecipeManager.MakeRecipeID("CookingStation", array9, array10), array9, array10)
            {
                time = 15,
                description = ITEMS.FOOD.COOKEDMEAT.RECIPEDESC,
                nameDisplay = ComplexRecipe.RecipeNameDisplay.IngredientToResult,
                fabricators = new List<Tag> { "CookingStation" },
                sortOrder = 22
            };
            ComplexRecipe.RecipeElement[] array11 = new ComplexRecipe.RecipeElement[1]
            {
            new ComplexRecipe.RecipeElement("ShellfishMeat", 1f)
            };
            ComplexRecipe.RecipeElement[] array12 = new ComplexRecipe.RecipeElement[1]
            {
            new ComplexRecipe.RecipeElement("CookedFish", 5f, ComplexRecipe.RecipeElement.TemperatureOperation.Heated)
            };
            CookedMeatConfig.recipe = new ComplexRecipe(ComplexRecipeManager.MakeRecipeID("CookingStation", array11, array12), array11, array12)
            {
                time = 15,
                description = ITEMS.FOOD.COOKEDMEAT.RECIPEDESC,
                nameDisplay = ComplexRecipe.RecipeNameDisplay.IngredientToResult,
                fabricators = new List<Tag> { "CookingStation" },
                sortOrder = 22
            };
            ComplexRecipe.RecipeElement[] array13 = new ComplexRecipe.RecipeElement[1]
            {
            new ComplexRecipe.RecipeElement(PrickleFruitConfig.ID, 1f)
            };
            ComplexRecipe.RecipeElement[] array14 = new ComplexRecipe.RecipeElement[1]
            {
            new ComplexRecipe.RecipeElement("CookedMeat", 1f, ComplexRecipe.RecipeElement.TemperatureOperation.Heated)
            };
            GrilledPrickleFruitConfig.recipe = new ComplexRecipe(ComplexRecipeManager.MakeRecipeID("CookingStation", array13, array14), array13, array14)
            {
                time = 15,
                description = ITEMS.FOOD.GRILLEDPRICKLEFRUIT.RECIPEDESC,
                nameDisplay = ComplexRecipe.RecipeNameDisplay.Result,
                fabricators = new List<Tag> { "CookingStation" },
                sortOrder = 20
            };
            if (DlcManager.IsExpansion1Active())
            {
                ComplexRecipe.RecipeElement[] array15 = new ComplexRecipe.RecipeElement[1]
                {
                new ComplexRecipe.RecipeElement(SwampFruitConfig.ID, 1f)
                };
                ComplexRecipe.RecipeElement[] array16 = new ComplexRecipe.RecipeElement[1]
                {
                new ComplexRecipe.RecipeElement("SwampDelights", 1f, ComplexRecipe.RecipeElement.TemperatureOperation.Heated)
                };
                CookedEggConfig.recipe = new ComplexRecipe(ComplexRecipeManager.MakeRecipeID("CookingStation", array15, array16), array15, array16)
                {
                    time = 15,
                    description = ITEMS.FOOD.SWAMPDELIGHTS.RECIPEDESC,
                    nameDisplay = ComplexRecipe.RecipeNameDisplay.Result,
                    fabricators = new List<Tag> { "CookingStation" },
                    sortOrder = 20
                };
            }

            ComplexRecipe.RecipeElement[] array17 = new ComplexRecipe.RecipeElement[1]
            {
            new ComplexRecipe.RecipeElement("ColdWheatSeed", 3f)
            };
            ComplexRecipe.RecipeElement[] array18 = new ComplexRecipe.RecipeElement[1]
            {
            new ComplexRecipe.RecipeElement("ColdWheatBread", 5f, ComplexRecipe.RecipeElement.TemperatureOperation.Heated)
            };
            ColdWheatBreadConfig.recipe = new ComplexRecipe(ComplexRecipeManager.MakeRecipeID("CookingStation", array17, array18), array17, array18)
            {
                time = 15,
                description = ITEMS.FOOD.COLDWHEATBREAD.RECIPEDESC,
                nameDisplay = ComplexRecipe.RecipeNameDisplay.Result,
                fabricators = new List<Tag> { "CookingStation" },
                sortOrder = 50
            };
            ComplexRecipe.RecipeElement[] array19 = new ComplexRecipe.RecipeElement[1]
            {
            new ComplexRecipe.RecipeElement("RawEgg", 1f)
            };
            ComplexRecipe.RecipeElement[] array20 = new ComplexRecipe.RecipeElement[1]
            {
            new ComplexRecipe.RecipeElement("CookedEgg", 5f, ComplexRecipe.RecipeElement.TemperatureOperation.Heated)
            };
            CookedEggConfig.recipe = new ComplexRecipe(ComplexRecipeManager.MakeRecipeID("CookingStation", array19, array20), array19, array20)
            {
                time = 15,
                description = ITEMS.FOOD.COOKEDEGG.RECIPEDESC,
                nameDisplay = ComplexRecipe.RecipeNameDisplay.Result,
                fabricators = new List<Tag> { "CookingStation" },
                sortOrder = 1
            };
            if (DlcManager.IsExpansion1Active())
            {
                ComplexRecipe.RecipeElement[] array21 = new ComplexRecipe.RecipeElement[1]
                {
                new ComplexRecipe.RecipeElement("WormBasicFruit", 1f)
                };
                ComplexRecipe.RecipeElement[] array22 = new ComplexRecipe.RecipeElement[1]
                {
                new ComplexRecipe.RecipeElement("WormBasicFood", 1f, ComplexRecipe.RecipeElement.TemperatureOperation.Heated)
                };
                WormBasicFoodConfig.recipe = new ComplexRecipe(ComplexRecipeManager.MakeRecipeID("CookingStation", array21, array22), array21, array22)
                {
                    time = 15,
                    description = ITEMS.FOOD.WORMBASICFOOD.RECIPEDESC,
                    nameDisplay = ComplexRecipe.RecipeNameDisplay.Result,
                    fabricators = new List<Tag> { "CookingStation" },
                    sortOrder = 20
                };
            }

            if (DlcManager.IsExpansion1Active())
            {
                ComplexRecipe.RecipeElement[] array23 = new ComplexRecipe.RecipeElement[2]
                {
                new ComplexRecipe.RecipeElement("WormSuperFruit", 8f),
                new ComplexRecipe.RecipeElement("Sucrose".ToTag(), 4f)
                };
                ComplexRecipe.RecipeElement[] array24 = new ComplexRecipe.RecipeElement[1]
                {
                new ComplexRecipe.RecipeElement("WormSuperFood", 1f, ComplexRecipe.RecipeElement.TemperatureOperation.Heated)
                };
                WormSuperFoodConfig.recipe = new ComplexRecipe(ComplexRecipeManager.MakeRecipeID("CookingStation", array23, array24), array23, array24)
                {
                    time = 15,
                    description = ITEMS.FOOD.WORMSUPERFOOD.RECIPEDESC,
                    nameDisplay = ComplexRecipe.RecipeNameDisplay.Result,
                    fabricators = new List<Tag> { "CookingStation" },
                    sortOrder = 20
                };
            }
        }

    }


}
