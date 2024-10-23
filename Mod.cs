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

namespace BoostedManufacturingBuidingsAssetPack
{
    public class Mod : IMod
    {
        public static string Name = "Boosted Manufacturing Buildings";
        public static string Version = "1.8.0";
        public static string Author = "Javapower";
        public static string uiHostName = "javapower-boostedfactories";

        // Static fields and properties
        public static Setting setting;
        public static readonly string Id = "BostedManufacturingBuildings";

        public static Mod Instance { get; private set; }
        public static ExecutableAsset modAsset { get; private set; }
        internal ILog Log { get; private set; }

        // Static logger instance with custom logger name and settings
        public static ILog log = LogManager.GetLogger($"{nameof(BoostedManufacturingBuidingsAssetPack)}.{nameof(Mod)}")
            .SetShowsErrorsInUI(false);

        public void OnLoad(UpdateSystem updateSystem)
        {
            // Log entry for debugging purposes
            log.Info(nameof(OnLoad));

            // Try to fetch the mod asset from the mod manager
            if (GameManager.instance.modManager.TryGetExecutableAsset(this, out var asset))
            {
                log.Info($"Current mod asset at {asset.path}");
                modAsset = asset;
            }

            UIManager.defaultUISystem.AddHostLocation(uiHostName, Path.Combine(Path.GetDirectoryName(asset.path), "thumbs"), false);

            updateSystem.UpdateAt<BoostedForestry>(SystemUpdatePhase.PrefabUpdate);
            updateSystem.UpdateAfter<BoostedForestry>(SystemUpdatePhase.ToolUpdate);

        }

        public void OnDispose()
        {
            UIManager.defaultUISystem.RemoveHostLocation(uiHostName);
            log.Info(nameof(OnDispose));
        }
    }
}