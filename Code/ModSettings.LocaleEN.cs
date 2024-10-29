using Colossal;
using Game.Modding;
using Game.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndustriesExtendedDLC
{
    public partial class ModSettings : ModSetting
    {
        public class LocaleEN : IDictionarySource
        {
            private readonly ModSettings _setting;
            private Dictionary<string, string> _translations;

            public LocaleEN(ModSettings setting)
            {
                _setting = setting;
                _translations = Load();
            }
            public IEnumerable<KeyValuePair<string, string>> ReadEntries(IList<IDictionaryEntryError> errors, Dictionary<string, int> indexCounts)
            {
                return _translations;
            }

            public static string GetToolTooltipLocaleID(string tool, string value)
            {
                return $"{Mod.MOD_NAME}.Tooltip.Tools[{tool}][{value}]";
            }

            public static string GetLanguageNameLocaleID()
            {
                return $"{Mod.MOD_NAME}.Language.DisplayName";
            }

            public Dictionary<string, string> Load(bool dumpTranslations = false)
            {
                return new Dictionary<string, string>
                {
                    { _setting.GetSettingsLocaleID(), "Industries Extended" },
                    { GetLanguageNameLocaleID(), "English"},
                    { _setting.GetOptionTabLocaleID(ModSettings.GeneralTab), "General" },
                    { _setting.GetOptionTabLocaleID(ModSettings.KeybindingsTab), "Key Bindings" },
                    { _setting.GetOptionTabLocaleID(ModSettings.AboutTab), "About" },
                    // Groups
                    { _setting.GetOptionGroupLocaleID(ModSettings.ExtractorsSection), "Natural Resource Extractors" },
                    { _setting.GetOptionGroupLocaleID(ModSettings.ToolsSection), "Tools" },
                    { _setting.GetOptionGroupLocaleID(ModSettings.AboutSection), "Mod Info" },
                    //Keybindings
                    { _setting.GetBindingMapLocaleID(), "Industries Extended DLC Mod" },
                    { _setting.GetBindingKeyLocaleID(ModSettings.KeyBindAction.ToggleToolAction), "Apply Tool" },
                    //Labels
                    { _setting.GetOptionLabelLocaleID(nameof(ModSettings.ExtractorProductionEfficiency)), "Production efficiency" },
                    { _setting.GetOptionDescLocaleID(nameof(ModSettings.ExtractorProductionEfficiency)), "Change the efficiency when harvesting natural resources. It will produce more in less time. The vanilla value is 8. This will apply to all natural resouces in the game." },
                    { _setting.GetOptionLabelLocaleID(nameof(ModSettings.ExtractorCompanyExportMultiplier)), "Export Multiplier" },
                    { _setting.GetOptionDescLocaleID(nameof(ModSettings.ExtractorCompanyExportMultiplier)), "Change the export multiplier coeficient to get more money when exporting the natural resources extracted in the industry areas." },
                    { _setting.GetOptionLabelLocaleID(nameof(ModSettings.ToggleSceneExplorerTool)), "Exploration" },
                    { _setting.GetOptionDescLocaleID(nameof(ModSettings.ToggleSceneExplorerTool)), "Set the key to activate in game the scene exploration in orden to select an object to view." },
                    { _setting.GetOptionLabelLocaleID(nameof(ModSettings.OpenRepositoryAtVersion)), "Open GitHub mod repository" },
                    { _setting.GetOptionDescLocaleID(nameof(ModSettings.OpenRepositoryAtVersion)), "Open the github repository of this mod." },
                    { _setting.GetOptionLabelLocaleID(nameof(ModSettings.OpenRepositoryRoadmap)), "Open Roadmap" },
                    { _setting.GetOptionDescLocaleID(nameof(ModSettings.OpenRepositoryRoadmap)), "Open the status board of the tasks involved in the devolpming of this mod." },
                    { _setting.GetOptionLabelLocaleID(nameof(ModSettings.ModVersion)), "Version" },
                    { _setting.GetOptionDescLocaleID(nameof(ModSettings.ModVersion)), "Mod current version." },
                    { _setting.GetOptionLabelLocaleID(nameof(ModSettings.AuthorMod)), "Mod author" },
                    { _setting.GetOptionDescLocaleID(nameof(ModSettings.AuthorMod)), "Name of the author of this mod." },
                    { _setting.GetOptionLabelLocaleID(nameof(ModSettings.InformationalVersion)), "Informational Version" },
                    { _setting.GetOptionDescLocaleID(nameof(ModSettings.InformationalVersion)), "Mod version with the commit ID from GitHub." },
                };
            }

            public void Unload() { }

            public override string ToString()
            {
                return "IndustriesExtendedDLC.Locale.en-US";
            }
        }
    }
}
