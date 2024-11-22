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
using Game.Settings;
using System.IO;
using Game.Debug;
using Colossal.IO.AssetDatabase;
using System.Reflection;
using Game.Areas;
using Game.Serialization;
using IndustriesExtended.Systems;
using System.Linq;
using static IndustriesExtended.ModSettings;


namespace IndustriesExtended
{
    public class Mod : IMod
    {
        // some extra info about this mod to show in the UI and/or the components in this mod code
        public const string MOD_NAME = "Industries Extended";        
        public static string uiHostName = "javapower-industriesextended";
        public static readonly string Id = "IndustriesExtended";
        public static string Author = "Javapower";
        public static string Version => Assembly.GetExecutingAssembly().GetName().Version.ToString(4);
        public static string InformationalVersion => Assembly.GetExecutingAssembly().GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion;
        internal static ModSettings Settings { get; private set; }

        public string ModPath { get; set; }

        // This is something for the feature if this mod is incompatible with other mod in order to fix
        // ---
        // public static bool IsTLEEnabled => _isTLEEnabled ??= GameManager.instance.modManager.ListModsEnabled().Any(x => x.StartsWith("C2VM.CommonLibraries.LaneSystem"));
        // public static bool IsRBEnabled => _isRBEnabled ??= GameManager.instance.modManager.ListModsEnabled().Any(x => x.StartsWith("RoadBuilder"));
        // private static bool? _isTLEEnabled;
        // private static bool? _isRBEnabled;

        public void OnLoad(UpdateSystem updateSystem)
        {
            // Log entry for debugging purposes
                Logger.Info($"{nameof(OnLoad)}, version: {InformationalVersion}");

            // Register Key Binding and Settings UI
            Logger.Info("Registring Settings options in UI and keybindings");
            Settings = new ModSettings(this, false);
            Settings.RegisterKeyBindings();
            Settings.RegisterInOptionsUI();
            // Load all dictonary in English to apply in the objects of the mod
            GameManager.instance.localizationManager.AddSource("en-US", new LocaleEN(Settings));
            // Load the settings for the current mod
            AssetDatabase.global.LoadSettings(nameof(IndustriesExtended), Settings, new ModSettings(this, false));

            // Try to fetch the mod asset from the mod manager
            if (GameManager.instance.modManager.TryGetExecutableAsset(this, out var asset))
            {
                ModPath = Path.GetDirectoryName(asset.path);
                // Set the thumbnails location for the assets inside the mod
                UIManager.defaultUISystem.AddHostLocation(uiHostName, Path.Combine(Path.GetDirectoryName(asset.path), "thumbs"), false);
                Logger.Info($"Current mod asset at {asset.path}");
            }

            Settings.ApplyAndSave();


            updateSystem.UpdateAt<TestFieldsUISystem>(SystemUpdatePhase.UIUpdate);
            //updateSystem.UpdateAt<SceneExplorerUISystem>(SystemUpdatePhase.UIUpdate);
            //updateSystem.UpdateAt<TestQuery>(SystemUpdatePhase.GameSimulation);
        }

        public void OnDispose()
        {
            UIManager.defaultUISystem.RemoveHostLocation(uiHostName);
            Logger.Info(nameof(OnDispose));
            Settings?.UnregisterInOptionsUI();
            Settings = null;
        }
    }
}