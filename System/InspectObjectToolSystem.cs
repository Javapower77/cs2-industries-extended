using Game.Prefabs;
using Game.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Entities;

namespace BoostedManufacturingBuidingsAssetPack.System
{
    public struct InspectedObject : IComponentData
    {
        public Entity entity;
        public bool entityChanged;
        public bool isDirty;
    }

    public partial class InspectObjectToolSystem : ToolBaseSystem
    {
        public override string toolID => "Object Inspector Tool";

        protected override void OnCreate()
        {
            base.OnCreate();
            EntityManager.AddComponent<InspectedObject>(SystemHandle);
        }


        public override PrefabBase GetPrefab()
        {
            return null;
        }

        public override bool TrySetPrefab(PrefabBase prefab)
        {
            return false;
        }
    }

}
