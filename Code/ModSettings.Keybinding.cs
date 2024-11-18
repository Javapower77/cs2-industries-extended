using System;
using System.Collections.Generic;
using System.Linq;
using Colossal.IO.AssetDatabase;
using Game;
using Game.Input;
using Game.Modding;
using Game.SceneFlow;
using Game.Settings;
using Game.UI.Widgets;
using Unity.Entities;
using UnityEngine.Device;

namespace IndustriesExtended
{
    [SettingsUIKeyboardAction(KeyBindAction.ToggleToolAction, customUsages: new[] { Usages.kDefaultUsage, Usages.kToolUsage })]
   

    public partial class ModSettings : ModSetting
    {
        // Sections from the Settings UI
        internal const string ToolsSection = "Tools";

        // Sections from the Settings UI
        internal const string KeybindingsTab = "Keybindings";

        private string _switchToolModeKeybindingName = string.Empty;

        [SettingsUIHidden]
        internal string SwitchToolModeKeybind => _switchToolModeKeybindingName;

        [SettingsUIKeyboardBinding(BindingKeyboard.E, KeyBindAction.ToggleToolAction, ctrl: true)]
        [SettingsUISection(KeybindingsTab, ToolsSection)]
        public ProxyBinding ToggleSceneExplorerTool { get; set; }


        internal static class KeyBindAction
        {
            internal const string ToggleToolAction = "ToggleToolAction";
        }


    }
}
