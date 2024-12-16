using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using Colossal.Entities;
using Colossal.Logging;
using Colossal.PSI.Environment;
using Colossal.Serialization.Entities;
using Colossal.UI.Binding;
using Game;
using Game.Common;
using Game.Input;
using Game.Objects;
using Game.Prefabs;
using Game.Prefabs.Climate;
using Game.Rendering;
using Game.Simulation;
using Game.Tools;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using UnityEngine;
using IndustriesExtended.Extensions;
using System.Diagnostics;
using Game.Buildings;
using Game.Economy;
using Game.Zones;
using static Game.Buildings.LocalEffectSystem;
using Colossal.Collections;
using Game.City;
using Game.Companies;
using System.Runtime.CompilerServices;
using Unity.Mathematics;
using Game.Debug;
using Game.Reflection;

namespace IndustriesExtended.Systems
{
    public partial class StorageFieldsUISystem : ExtendedInfoSectionBase
    {
        protected override string group => Mod.Id;
        private ToolSystem toolSystem;
        private DefaultToolSystem defaultToolSystem;
        private ValueBindingHelper<string> m_CurrentFactoryPrefab;
        private ValueBindingHelper<bool> m_IsBoostedFactory;
        private ValueBindingHelper<int> m_GoodProduction;
        private ValueBindingHelper<int> m_ResourceIndex;
        private PrefabBase _currentPrefab;
        private PrefabSystem _prefabSystem;
        private SimulationSystem m_SimulationSystem;
        public override void OnWriteProperties(IJsonWriter writer)
        {

        }

        protected override void OnProcess()
        {

        }

        protected override void Reset()
        {

        }
        protected override void OnCreate()
        {
            base.OnCreate();
            m_InfoUISystem.AddMiddleSection(this);
            LogUtil.Info($"{nameof(StorageFieldsUISystem)}.{nameof(OnCreate)}");

            m_CurrentFactoryPrefab = CreateBinding("CurrentFactoryPrefab", "");
            m_IsBoostedFactory = CreateBinding("IsBoostedFactory", false);
            m_GoodProduction = CreateBinding("GoodProduction", 0);
            m_ResourceIndex = CreateBinding("ResourceIndex", 0);

            _prefabSystem = base.World.GetOrCreateSystemManaged<PrefabSystem>();
            this.toolSystem = World.GetExistingSystemManaged<ToolSystem>();
            this.defaultToolSystem = World.GetExistingSystemManaged<DefaultToolSystem>();
        }

        protected override void OnUpdate()
        {
            base.OnUpdate();

            Entity selected = this.getSelected();
            m_IsBoostedFactory.Value = false;

            if (selected != Entity.Null && EntityManager.HasComponent<Building>(selected))
            {
                PrefabRef refData;
                base.EntityManager.TryGetComponent(selected, out refData);
                PrefabBase prefab = _prefabSystem.GetPrefab<PrefabBase>(refData);
                m_CurrentFactoryPrefab.Value = prefab.name;
                if (prefab.name == "Convenience Food Factory")
                {
                    m_IsBoostedFactory.Value = true;
                }

                EntityManager.TryGetBuffer(selected, false, out DynamicBuffer<Renter> _renterBuilding);
                EntityManager.TryGetComponent(selected, out PrefabRef _prefabref);
                EntityManager.TryGetComponent(selected, out SignatureBuildingData _signatureBuildingData);
                EntityManager.TryGetComponent(selected, out WarehouseData _warehouseData);

                if ((m_IsBoostedFactory.Value == true) && (EntityManager.TryGetBuffer(_renterBuilding[0].m_Renter, false, out DynamicBuffer<Game.Economy.Resources> _resources)))
                {
                    for (int i = 0; i < _resources.Length; i++)
                    {
                        if (_resources[i].m_Resource == Game.Economy.Resource.ConvenienceFood)
                        {
                            m_GoodProduction.Value = _resources[i].m_Amount;
                        }
                    }
                }

                m_ResourceIndex.Value = EconomyUtils.GetResourceIndex(Game.Economy.Resource.ConvenienceFood);

                m_SimulationSystem = base.World.GetOrCreateSystemManaged<SimulationSystem>();
                World.GetExistingSystemManaged<TestIndustrialStuff>().GetStuff();
            }

            base.visible = true;
        }

        private Entity getSelected()
        {
            return this.toolSystem.selected;
        }
    }
}
