using Colossal.Logging;
using Colossal.UI;
using Game;
using Game.Modding;
using Game.SceneFlow;
using Game.Economy;
using Game.Buildings;
using Game.City;
using Game.Common;
using Game.Companies;
using Game.Prefabs;
using Game.Simulation;
using Game.UI;
using System.IO;
using Game.Debug;
using Colossal.IO.AssetDatabase;
using Game.Settings;
using Game.Objects;
using System;
using Unity.Collections;
using Unity.Entities;
using Game.Tools;
using Colossal.Serialization.Entities;

namespace BoostedManufacturingBuidingsAssetPack
{
    public partial class BoostedForestry : GameSystemBase
    {
        public static ILog log = LogManager.GetLogger($"{nameof(BoostedManufacturingBuidingsAssetPack)}.{nameof(Mod)}").SetShowsErrorsInUI(false);


        protected override void OnCreate()
        {
            base.OnCreate();
            int x = 0;
        }
        protected override void OnUpdate()
        {
            int z = 0;
        }

        protected override void OnGameLoadingComplete(Purpose purpose, GameMode mode)
        {
            base.OnGameLoadingComplete(purpose, mode);
            int y = 0;
        }

        protected override void OnGameLoaded(Context serializationContext)
        {
            base.OnGameLoaded(serializationContext);

            int z = 0;
        }
    }
}
