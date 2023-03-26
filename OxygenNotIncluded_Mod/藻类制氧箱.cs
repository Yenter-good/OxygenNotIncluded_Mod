using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HarmonyLib;
using UnityEngine;

namespace OxygenNotIncluded_Mod
{
    [HarmonyPatch(typeof(AlgaeHabitatConfig), "ConfigureBuildingTemplate")]
    internal class 藻类制氧箱
    {
        [HarmonyPrefix]
        private static bool Prefix(GameObject go)
        {
            Storage storage = go.AddOrGet<Storage>();
            storage.showInUI = true;
            List<Tag> storageFilters = new List<Tag>
            {
                SimHashes.DirtyWater.CreateTag()
            };
            Tag tag = SimHashes.Algae.CreateTag();
            Tag tag2 = SimHashes.Water.CreateTag();
            Storage storage2 = go.AddComponent<Storage>();
            storage2.capacityKg = 360f;
            storage2.showInUI = true;
            storage2.SetDefaultStoredItemModifiers(new List<Storage.StoredItemModifier>
            {
                Storage.StoredItemModifier.Hide,
                Storage.StoredItemModifier.Seal
            });
            storage2.allowItemRemoval = false;
            storage2.storageFilters = storageFilters;
            ManualDeliveryKG manualDeliveryKG = go.AddOrGet<ManualDeliveryKG>();
            manualDeliveryKG.SetStorage(storage);
            manualDeliveryKG.RequestedItemTag = tag;
            manualDeliveryKG.capacity = 90f;
            manualDeliveryKG.refillMass = 18f;
            manualDeliveryKG.choreTypeIDHash = Db.Get().ChoreTypes.FetchCritical.IdHash;
            ManualDeliveryKG manualDeliveryKG2 = go.AddComponent<ManualDeliveryKG>();
            manualDeliveryKG2.SetStorage(storage);
            manualDeliveryKG2.RequestedItemTag = tag2;
            manualDeliveryKG2.capacity = 360f;
            manualDeliveryKG2.refillMass = 72f;
            manualDeliveryKG2.choreTypeIDHash = Db.Get().ChoreTypes.FetchCritical.IdHash;
            KAnimFile[] overrideAnims = new KAnimFile[]
            {
            Assets.GetAnim("anim_interacts_algae_terarrium_kanim")
            };
            AlgaeHabitatEmpty algaeHabitatEmpty = go.AddOrGet<AlgaeHabitatEmpty>();
            algaeHabitatEmpty.workTime = 5f;
            algaeHabitatEmpty.overrideAnims = overrideAnims;
            algaeHabitatEmpty.workLayer = Grid.SceneLayer.BuildingFront;
            AlgaeHabitat algaeHabitat = go.AddOrGet<AlgaeHabitat>();
            algaeHabitat.lightBonusMultiplier = 1.1f;
            algaeHabitat.pressureSampleOffset = new CellOffset(0, 1);
            ElementConverter elementConverter = go.AddComponent<ElementConverter>();
            elementConverter.consumedElements = new ElementConverter.ConsumedElement[]
            {
                new ElementConverter.ConsumedElement(tag, 0.01f, true),
                new ElementConverter.ConsumedElement(tag2, 0.03f, true)
            };
            elementConverter.outputElements = new ElementConverter.OutputElement[]
            {
                new ElementConverter.OutputElement(0.5f, SimHashes.Oxygen, 296.15f, false, false, 0f, 1f, 1f, byte.MaxValue, 0, true)
            };
           
            ElementConsumer elementConsumer = go.AddOrGet<ElementConsumer>();
            elementConsumer.elementToConsume = SimHashes.CarbonDioxide;
            elementConsumer.consumptionRate = 0.03f;
            elementConsumer.consumptionRadius = 9;
            elementConsumer.showInStatusPanel = true;
            elementConsumer.sampleCellOffset = new Vector3(0f, 1f, 0f);
            elementConsumer.isRequired = false;
            PassiveElementConsumer passiveElementConsumer = go.AddComponent<PassiveElementConsumer>();
            passiveElementConsumer.elementToConsume = SimHashes.Water;
            passiveElementConsumer.consumptionRate = 0.12f;
            passiveElementConsumer.consumptionRadius = 1;
            passiveElementConsumer.showDescriptor = false;
            passiveElementConsumer.storeOnConsume = true;
            passiveElementConsumer.capacityKG = 360f;
            passiveElementConsumer.showInStatusPanel = false;
            go.AddOrGet<KBatchedAnimController>().randomiseLoopedOffset = true;
            go.AddOrGet<AnimTileable>();
            Prioritizable.AddRef(go);

            return false;
        }
    }
}
