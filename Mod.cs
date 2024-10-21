using Colossal.Logging;
using Colossal.UI;
using Game;
using Game.Modding;
using Game.SceneFlow;
using System.IO;

namespace BoostedManufacturingBuidingsAssetPack
{
    public class Mod : IMod
    {
        public static string Name = "Boosted Manufacturing Buildings";
        public static string Version = "1.7.0";
        public static string Author = "Javapower";
        public static string uiHostName = "javapower-boostedfactories";

        public void OnLoad(UpdateSystem updateSystem)
        {
            GameManager.instance.modManager.TryGetExecutableAsset(this, out var asset);
            UIManager.defaultUISystem.AddHostLocation(uiHostName, Path.Combine(Path.GetDirectoryName(asset.path), "thumbs"), false);
        }

        public void OnDispose()
        {
            UIManager.defaultUISystem.RemoveHostLocation(uiHostName);
        }
    }
}