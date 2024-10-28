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
using IndustriesExtendedDLC.System;
using System.Linq;


namespace IndustriesExtendedDLC.Code
{
    public class Mod : IMod
    {
        // some extra info about this mod to show in the UI and/or the components in this mod code
        public const string MOD_NAME = "Industries Extended DLC";
        public const string Author = "Javapower";
        public static string uiHostName = "javapower-industriesextended";
        public static readonly string Id = "IndustriesExtendedDLC";
        public static string Version => Assembly.GetExecutingAssembly().GetName().Version.ToString(4);
        public static string InformationalVersion => Assembly.GetExecutingAssembly().GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion;

        internal ModSettings Settings { get; private set; }

        // Mods Settings Folder
        public static string SettingsFolder = Path.Combine(EnvPath.kUserDataPath, "ModsSettings", nameof(IndustriesExtendedDLC));

        public static Mod Instance { get; private set; }
        public static ExecutableAsset modAsset { get; private set; }


        // This is something for the feature if this mod is incompatible with other mod in order to fix
        // ---
        // public static bool IsTLEEnabled => _isTLEEnabled ??= GameManager.instance.modManager.ListModsEnabled().Any(x => x.StartsWith("C2VM.CommonLibraries.LaneSystem"));
        // public static bool IsRBEnabled => _isRBEnabled ??= GameManager.instance.modManager.ListModsEnabled().Any(x => x.StartsWith("RoadBuilder"));
        // private static bool? _isTLEEnabled;
        // private static bool? _isRBEnabled;



        public void OnLoad(UpdateSystem updateSystem)
        {
            // Log entry for debugging purposes
            log.Info(nameof(OnLoad));

            Settings = new ModSettings(this, false);
            Settings.RegisterKeyBindings();
            Settings.RegisterInOptionsUI();

            // Try to fetch the mod asset from the mod manager
            if (GameManager.instance.modManager.TryGetExecutableAsset(this, out var asset))
            {
                log.Info($"Current mod asset at {asset.path}");
                modAsset = asset;
            }

            AssetDatabase.global.LoadSettings(nameof(IndustriesExtendedDLC), _settings, new Settings(this));
            _settings.ApplyLoadedSettings();

            UIManager.defaultUISystem.AddHostLocation(uiHostName, Path.Combine(Path.GetDirectoryName(asset.path), "thumbs"), false);

            updateSystem.UpdateAt<SceneExplorerUISystem>(SystemUpdatePhase.UIUpdate);
            updateSystem.UpdateAt<TestQuery>(SystemUpdatePhase.GameSimulation);

        }

        public void OnDispose()
        {
            UIManager.defaultUISystem.RemoveHostLocation(uiHostName);
            log.Info(nameof(OnDispose));

            if (_settings != null)
            {
                _settings.UnregisterInOptionsUI();
                _settings = null;
            }
        }
    }
}