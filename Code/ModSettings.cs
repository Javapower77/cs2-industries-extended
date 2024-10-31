using System;
using System.Collections.Generic;
using System.Linq;
using Colossal.IO.AssetDatabase;
using Game;
using Game.Input;
using Game.Modding;
using Game.Prefabs;
using Game.SceneFlow;
using Game.Settings;
using Game.UI.InGame;
using Game.UI.Widgets;
using Unity.Entities;
using UnityEngine.Device;

namespace IndustriesExtendedDLC
{
    [FileLocation(nameof(IndustriesExtendedDLC))]
    [SettingsUITabOrder(GeneralTab, KeybindingsTab, AboutTab)]
    [SettingsUIGroupOrder(ExtractorsSection, ToolsSection, AboutSection)]
    [SettingsUIShowGroupName(ExtractorsSection, ToolsSection, AboutSection)]
    public partial class ModSettings : ModSetting
    {
        internal const string SETTINGS_ASSET_NAME = "Industries Extended DLC General Settings";
        internal static ModSettings Instance { get; private set; }

        // Default values from the game
        private static readonly VanillaData VanillaData = new();

        // TABs from the Settings UI
        internal const string GeneralTab = "General";        
        internal const string AboutTab = "About";

        // Sections from the Settings UI
        internal const string ExtractorsSection = "Extractors";
        internal const string AboutSection = "About";

        [SettingsUISlider(min = 0f, max = 100000f, step = 100f, unit = Game.UI.Unit.kFloatSingleFraction)]
        [SettingsUISection(GeneralTab, ExtractorsSection)]
        public float ExtractorProductionEfficiency { get; set; }

        [SettingsUISlider(min = 0f, max = 1f, step = 0.1f, unit = Game.UI.Unit.kFloatSingleFraction)]
        [SettingsUISection(GeneralTab, ExtractorsSection)]
        public float ExtractorCompanyExportMultiplier { get; set; }

        [SettingsUIButton]
        [SettingsUISection(GeneralTab, ExtractorsSection)]
        public bool Reset { set { SetDefaults(); } }

        [SettingsUISection(AboutTab, AboutSection)]
        public string ModVersion => Mod.Version;

        [SettingsUISection(AboutTab, AboutSection)]
        public string AuthorMod => Mod.Author;

        [SettingsUISection(AboutTab, AboutSection)]
        public string InformationalVersion => Mod.InformationalVersion;

        [SettingsUISection(AboutTab, AboutSection)]
        public bool OpenRepositoryAtVersion
        {
            set
            {
                try
                {
                    Application.OpenURL($"https://github.com/Javapower77/cs2-industries-extended-dlc/commit/{Mod.InformationalVersion.Split('+')[1]}");
                }
                catch (Exception e)
                {
                    UnityEngine.Debug.LogException(e);
                }
            }
        }

        [SettingsUISection(AboutTab, AboutSection)]
        public bool OpenRepositoryRoadmap
        {
            set
            {
                try
                {
                    Application.OpenURL($"https://github.com/users/Javapower77/projects/2/views/5");
                }
                catch (Exception e)
                {
                    UnityEngine.Debug.LogException(e);
                }
            }
        }
   
        [SettingsUISection(AboutTab, AboutSection)]
        [SettingsUIMultilineText("coui://javapower-industriesextended/discord-icon-white.png")]
        public string DiscordServers => string.Empty;

        [SettingsUISection(AboutTab, AboutSection)]
        public bool OpenCS2ModdingDiscord
        {
            set
            {
                try
                {
                    Application.OpenURL($"https://discord.gg/HTav7ARPs2");
                }
                catch (Exception e)
                {
                    UnityEngine.Debug.LogException(e);
                }
            }
        }

        [SettingsUISection(AboutTab, AboutSection)]
        public bool OpenAuthorDiscord
        {
            set
            {
                try
                {
                    Application.OpenURL($"https://discord.gg/VxDJTMzf");
                }
                catch (Exception e)
                {
                    UnityEngine.Debug.LogException(e);
                }
            }
        }

        public ModSettings(IMod mod, bool asDefault) : base(mod)
        {
            Instance = this;
        }

        public override void SetDefaults()
        {
            ExtractorProductionEfficiency = VanillaData.ExtractorProductionEfficiency;
            ExtractorCompanyExportMultiplier = VanillaData.ExtractorCompanyExportMultiplier;
        }

        /*
        public override void Apply()
        {
            OverlaySystem toolOverlaySystem = World.DefaultGameObjectInjectionWorld.GetExistingSystemManaged<OverlaySystem>();
            toolOverlaySystem.ApplyOverlayParams(new OverlayParameterData()
            {
                extractorCompanyExportMultiplier = ExtractorCompanyExportMultiplier,
                extractorProductionEfficiency = ExtractorProductionEfficiency
            });
            base.Apply();
        }
        */

    }
}

