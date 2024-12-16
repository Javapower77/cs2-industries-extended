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
using Game.UI.InGame;
using Unity.Entities;
using System;


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
            LogUtil.Info($"{nameof(Mod)}.{nameof(OnLoad)}, version:{InformationalVersion}");

            try
            {
                // Register Key Binding and Settings UI
                LogUtil.Info("Registring Settings options in UI and keybindings");
                Settings = new ModSettings(this, false);
                Settings.RegisterKeyBindings();
                Settings.RegisterInOptionsUI();
                
                // Load all dictonary in English to apply in the objects of the mod
                GameManager.instance.localizationManager.AddSource("en-US", new LocaleEN(Settings));
                
                // Load the settings for the current mod
                AssetDatabase.global.LoadSettings(nameof(IndustriesExtended), Settings, new ModSettings(this, false));

                Settings.ApplyAndSave();

                // Try to fetch the mod asset from the mod manager
                if (GameManager.instance.modManager.TryGetExecutableAsset(this, out var asset))
                {
                    ModPath = Path.GetDirectoryName(asset.path);
                    // Set the thumbnails location for the assets inside the mod
                    UIManager.defaultUISystem.AddHostLocation(uiHostName, Path.Combine(Path.GetDirectoryName(asset.path), "thumbs"), false);
                    LogUtil.Info($"Current mod asset at {asset.path}");
                }
                else
                {
                    LogUtil.Error("Unable to get mod executable asset.");
                    return;
                }

                //updateSystem.UpdateAt<TestFieldsUISystem>(SystemUpdatePhase.UIUpdate);

                //World.DefaultGameObjectInjectionWorld.GetOrCreateSystemManaged<SelectedInfoUISystem>().AddMiddleSection(
                //    World.DefaultGameObjectInjectionWorld.GetOrCreateSystemManaged<TestFieldsUISystem>()
                //);


                updateSystem.UpdateAt<SceneExplorerUISystem>(SystemUpdatePhase.UIUpdate);
                updateSystem.UpdateAt<TestQuery>(SystemUpdatePhase.GameSimulation);
                updateSystem.UpdateAt<TestIndustrialStuff>(SystemUpdatePhase.GameSimulation);

                updateSystem.UpdateBefore<TestFieldsUISystem>(SystemUpdatePhase.Rendering);
                updateSystem.UpdateBefore<StorageFieldsUISystem>(SystemUpdatePhase.Rendering);
                
                

 //               World.DefaultGameObjectInjectionWorld.GetOrCreateSystemManaged<SelectedInfoUISystem>().AddMiddleSection(
 //                   World.DefaultGameObjectInjectionWorld.GetOrCreateSystemManaged<TestFieldsUISystem>()
 //                );
            }
            catch (Exception ex)
            {
                LogUtil.Exception(ex);
            }
        }

        public void OnDispose()
        {
            UIManager.defaultUISystem.RemoveHostLocation(uiHostName);
            LogUtil.Info($"{nameof(Mod)}.{nameof(OnDispose)}");
            Settings?.UnregisterInOptionsUI();
            Settings = null;
        }
    }
}