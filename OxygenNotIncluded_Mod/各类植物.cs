using Klei.AI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TUNING;
using UnityEngine;
using static ResearchTypes;

namespace OxygenNotIncluded_Mod
{
    [HarmonyLib.HarmonyPatch(typeof(EntityTemplates), "ExtendEntityToBasicPlant")]
    internal class 植物生长速度
    {
        [HarmonyLib.HarmonyPrefix]
        static bool Prefix()
        {
            CROPS.CROP_TYPES = new List<Crop.CropVal>
            {
                new Crop.CropVal("BasicPlantFood", 1800f, 1, true),
                new Crop.CropVal(PrickleFruitConfig.ID, 1800f, 2, true),
                new Crop.CropVal(SwampFruitConfig.ID, 2560f, 1, true),
                new Crop.CropVal(MushroomConfig.ID, 2500f, 1, true),
                new Crop.CropVal("ColdWheatSeed", 3600f, 18, true),
                new Crop.CropVal(SpiceNutConfig.ID, 2000f, 4, true),
                new Crop.CropVal(BasicFabricConfig.ID, 1200f, 1, true),
                new Crop.CropVal(SwampLilyFlowerConfig.ID, 7200f, 2, true),
                new Crop.CropVal("GasGrassHarvested", 2400f, 1, true),
                new Crop.CropVal("WoodLog", 2700f, 300, true),
                new Crop.CropVal("Lettuce", 3000f, 12, true),
                new Crop.CropVal("BeanPlantSeed", 5200f, 12, true),
                new Crop.CropVal("OxyfernSeed", 3600f, 1, true),
                new Crop.CropVal("PlantMeat", 8000f, 10, true),
                new Crop.CropVal("WormBasicFruit", 2400f, 1, true),
                new Crop.CropVal("WormSuperFruit", 4800f, 8, true),
                new Crop.CropVal(SimHashes.Salt.ToString(), 3600f, 65, true),
                new Crop.CropVal(SimHashes.Water.ToString(), 6000f, 350, true)
            };

            return true;
        }
    }

    [HarmonyLib.HarmonyPatch(typeof(CreatureCalorieMonitor.Def), "GetDescriptors")]
    internal class 植物生长速度1
    {
        [HarmonyLib.HarmonyPrefix]
        static bool Prefix()
        {
            CROPS.CROP_TYPES = new List<Crop.CropVal>
            {
                new Crop.CropVal("BasicPlantFood", 1800f, 1, true),
                new Crop.CropVal(PrickleFruitConfig.ID, 1800f, 2, true),
                new Crop.CropVal(SwampFruitConfig.ID, 2560f, 1, true),
                new Crop.CropVal(MushroomConfig.ID, 2500f, 1, true),
                new Crop.CropVal("ColdWheatSeed", 3600f, 18, true),
                new Crop.CropVal(SpiceNutConfig.ID, 2000f, 4, true),
                new Crop.CropVal(BasicFabricConfig.ID, 1200f, 1, true),
                new Crop.CropVal(SwampLilyFlowerConfig.ID, 7200f, 2, true),
                new Crop.CropVal("GasGrassHarvested", 2400f, 1, true),
                new Crop.CropVal("WoodLog", 2700f, 300, true),
                new Crop.CropVal("Lettuce", 3000f, 12, true),
                new Crop.CropVal("BeanPlantSeed", 5200f, 12, true),
                new Crop.CropVal("OxyfernSeed", 3600f, 1, true),
                new Crop.CropVal("PlantMeat", 8000f, 10, true),
                new Crop.CropVal("WormBasicFruit", 2400f, 1, true),
                new Crop.CropVal("WormSuperFruit", 4800f, 8, true),
                new Crop.CropVal(SimHashes.Salt.ToString(), 3600f, 65, true),
                new Crop.CropVal(SimHashes.Water.ToString(), 6000f, 350, true)
            };

            return true;
        }
    }

    [HarmonyLib.HarmonyPatch(typeof(PrickleFlowerConfig), "CreatePrefab")]
    internal class 毛刺花
    {
        [HarmonyLib.HarmonyPrefix]
        static bool Prefix(ref GameObject __result)
        {
            string id = "PrickleFlower";
            string name = STRINGS.CREATURES.SPECIES.PRICKLEFLOWER.NAME;
            string desc = STRINGS.CREATURES.SPECIES.PRICKLEFLOWER.DESC;
            float mass = 1f;
            EffectorValues tier = DECOR.BONUS.TIER1;
            GameObject gameObject = EntityTemplates.CreatePlacedEntity(id, name, desc, mass, Assets.GetAnim("bristleblossom_kanim"), "idle_empty", Grid.SceneLayer.BuildingFront, 1, 2, tier, default(EffectorValues), SimHashes.Creature, null, 293f);
            EntityTemplates.ExtendEntityToBasicPlant(gameObject, 118.15f, 178.15f, 503.15f, 598.15f, new SimHashes[]
            {
            SimHashes.Oxygen,
            SimHashes.ContaminatedOxygen,
            SimHashes.CarbonDioxide
            }, true, 0f, 0.15f, PrickleFruitConfig.ID, true, true, true, true, 2400f, 0f, 4600f, "PrickleFlowerOriginal", STRINGS.CREATURES.SPECIES.PRICKLEFLOWER.NAME);
            EntityTemplates.ExtendPlantToIrrigated(gameObject, new PlantElementAbsorber.ConsumeInfo[]
            {
            new PlantElementAbsorber.ConsumeInfo
            {
                tag = GameTags.Water,
                massConsumptionRate = 0.01f
            }
            });
            gameObject.AddOrGet<StandardCropPlant>();
            Modifiers component = gameObject.GetComponent<Modifiers>();
            Db.Get().traits.Get(component.initialTraits[0]).Add(new AttributeModifier(Db.Get().PlantAttributes.MinLightLux.Id, 200f, STRINGS.CREATURES.SPECIES.PRICKLEFLOWER.NAME, false, false, true));
            component.initialAttributes.Add(Db.Get().PlantAttributes.MinLightLux.Id);
            gameObject.AddOrGet<BlightVulnerable>();
            EntityTemplates.CreateAndRegisterPreviewForPlant(EntityTemplates.CreateAndRegisterSeedForPlant(gameObject, SeedProducer.ProductionType.Harvest, "PrickleFlowerSeed", STRINGS.CREATURES.SPECIES.SEEDS.PRICKLEFLOWER.NAME, STRINGS.CREATURES.SPECIES.SEEDS.PRICKLEFLOWER.DESC, Assets.GetAnim("seed_bristleblossom_kanim"), "object", 0, new List<Tag>
            {
                GameTags.CropSeed
            }, SingleEntityReceptacle.ReceptacleDirection.Top, default(Tag), 2, STRINGS.CREATURES.SPECIES.PRICKLEFLOWER.DOMESTICATEDDESC, EntityTemplates.CollisionShape.CIRCLE, 0.25f, 0.25f, null, "", false), "PrickleFlower_preview", Assets.GetAnim("bristleblossom_kanim"), "place", 1, 2);
            SoundEventVolumeCache.instance.AddVolume("bristleblossom_kanim", "PrickleFlower_harvest", NOISE_POLLUTION.CREATURES.TIER3);
            SoundEventVolumeCache.instance.AddVolume("bristleblossom_kanim", "PrickleFlower_grow", NOISE_POLLUTION.CREATURES.TIER3);

            __result = gameObject;

            return false;
        }
    }

    [HarmonyLib.HarmonyPatch(typeof(SpiceVineConfig), "CreatePrefab")]
    internal class 火藤椒
    {
        [HarmonyLib.HarmonyPrefix]
        static bool Prefix(ref GameObject __result)
        {
            GameObject gameObject = EntityTemplates.CreatePlacedEntity("SpiceVine", STRINGS.CREATURES.SPECIES.SPICE_VINE.NAME, STRINGS.CREATURES.SPECIES.SPICE_VINE.DESC, 2f, decor: DECOR.BONUS.TIER1, anim: Assets.GetAnim("vinespicenut_kanim"), initialAnim: "idle_empty", sceneLayer: Grid.SceneLayer.BuildingFront, width: 1, height: 3, noise: default(EffectorValues), element: SimHashes.Creature, additionalTags: new List<Tag> { GameTags.Hanging }, defaultTemperature: 320f);
            EntityTemplates.MakeHangingOffsets(gameObject, 1, 3);
            EntityTemplates.ExtendEntityToBasicPlant(gameObject, 108.15f, 118.15f, 558.15f, 598.15f, null, pressure_sensitive: true, 0f, 0.15f, SpiceNutConfig.ID, can_drown: true, can_tinker: true, require_solid_tile: true, should_grow_old: true, 2400f, 0f, 9800f, "SpiceVineOriginal", STRINGS.CREATURES.SPECIES.SPICE_VINE.NAME);
            Tag tag = ElementLoader.FindElementByHash(SimHashes.Water).tag;
            PlantElementAbsorber.ConsumeInfo[] array = new PlantElementAbsorber.ConsumeInfo[1];
            PlantElementAbsorber.ConsumeInfo consumeInfo = new PlantElementAbsorber.ConsumeInfo
            {
                tag = tag,
                massConsumptionRate = 0.001f
            };
            array[0] = consumeInfo;
            EntityTemplates.ExtendPlantToIrrigated(gameObject, array);
            PlantElementAbsorber.ConsumeInfo[] array2 = new PlantElementAbsorber.ConsumeInfo[1];
            gameObject.GetComponent<UprootedMonitor>().monitorCells = new CellOffset[1]
            {
            new CellOffset(0, 1)
            };
            gameObject.AddOrGet<StandardCropPlant>();
            EntityTemplates.MakeHangingOffsets(EntityTemplates.CreateAndRegisterPreviewForPlant(EntityTemplates.CreateAndRegisterSeedForPlant(gameObject, SeedProducer.ProductionType.Harvest, "SpiceVineSeed", STRINGS.CREATURES.SPECIES.SEEDS.SPICE_VINE.NAME, STRINGS.CREATURES.SPECIES.SEEDS.SPICE_VINE.DESC, Assets.GetAnim("seed_spicenut_kanim"), "object", 1, new List<Tag> { GameTags.CropSeed }, SingleEntityReceptacle.ReceptacleDirection.Bottom, default(Tag), 4, STRINGS.CREATURES.SPECIES.SPICE_VINE.DOMESTICATEDDESC, EntityTemplates.CollisionShape.CIRCLE, 0.3f, 0.3f), "SpiceVine_preview", Assets.GetAnim("vinespicenut_kanim"), "place", 1, 3), 1, 3);
            __result = gameObject;
            return false;
        }
    }

    [HarmonyLib.HarmonyPatch(typeof(SwampLilyConfig), "CreatePrefab")]
    internal class 芳香百合
    {
        [HarmonyLib.HarmonyPrefix]
        static bool Prefix(ref GameObject __result)
        {
            GameObject gameObject = EntityTemplates.CreatePlacedEntity("SwampLily", STRINGS.CREATURES.SPECIES.SWAMPLILY.NAME, STRINGS.CREATURES.SPECIES.SWAMPLILY.DESC, 1f, decor: DECOR.BONUS.TIER1, anim: Assets.GetAnim("swamplily_kanim"), initialAnim: "idle_empty", sceneLayer: Grid.SceneLayer.BuildingBack, width: 1, height: 2, noise: default(EffectorValues), element: SimHashes.Creature, additionalTags: null, defaultTemperature: 328.15f);
            EntityTemplates.ExtendEntityToBasicPlant(gameObject, 108.15f, 118.15f, 558.15f, 598.15f, null, pressure_sensitive: true, 0f, 0.15f, SwampLilyFlowerConfig.ID, can_drown: true, can_tinker: true, require_solid_tile: true, should_grow_old: true, 2400f, 0f, 4600f, "SwampLily" + "Original", STRINGS.CREATURES.SPECIES.SWAMPLILY.NAME);
            gameObject.AddOrGet<StandardCropPlant>();
            EntityTemplates.CreateAndRegisterPreviewForPlant(EntityTemplates.CreateAndRegisterSeedForPlant(gameObject, SeedProducer.ProductionType.Harvest, "SwampLilySeed", STRINGS.CREATURES.SPECIES.SEEDS.SWAMPLILY.NAME, STRINGS.CREATURES.SPECIES.SEEDS.SWAMPLILY.DESC, Assets.GetAnim("seed_swampLily_kanim"), "object", 1, new List<Tag> { GameTags.CropSeed }, SingleEntityReceptacle.ReceptacleDirection.Top, default(Tag), 21, STRINGS.CREATURES.SPECIES.SWAMPLILY.DOMESTICATEDDESC, EntityTemplates.CollisionShape.CIRCLE, 0.3f, 0.3f), "SwampLily" + "_preview", Assets.GetAnim("swamplily_kanim"), "place", 1, 2);
            SoundEventVolumeCache.instance.AddVolume("swamplily_kanim", "SwampLily_grow", NOISE_POLLUTION.CREATURES.TIER3);
            SoundEventVolumeCache.instance.AddVolume("swamplily_kanim", "SwampLily_harvest", NOISE_POLLUTION.CREATURES.TIER3);
            SoundEventVolumeCache.instance.AddVolume("swamplily_kanim", "SwampLily_death", NOISE_POLLUTION.CREATURES.TIER3);
            SoundEventVolumeCache.instance.AddVolume("swamplily_kanim", "SwampLily_death_bloom", NOISE_POLLUTION.CREATURES.TIER3);
            GeneratedBuildings.RegisterWithOverlay(OverlayScreen.HarvestableIDs, "SwampLily");
            __result = gameObject;

            return false;
        }
    }
}
