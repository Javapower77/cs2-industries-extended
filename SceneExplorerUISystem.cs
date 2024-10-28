using System;
using System.Linq;
using IndustriesExtendedDLC.System;
using Colossal.Serialization.Entities;
using Colossal.UI;
using Game;
using Game.Input;
using Game.Rendering;
using Game.UI;
using Game.UI.Editor;
using Unity.Entities;
using UnityEngine;
using UnityEngine.InputSystem;
using static Colossal.IO.AssetDatabase.GeometryAsset;
using Object = UnityEngine.Object;

namespace IndustriesExtendedDLC
{
    public partial class SceneExplorerUISystem : UISystemBase
    {
        private ProxyAction _toggleExplorerAction;

        protected override void OnGamePreload(Purpose purpose, GameMode mode)
        {
            base.OnGamePreload(purpose, mode);
            if (mode == GameMode.Game)
            {
                ToggleInputActions(true, false);
            }
        }

        private void ToggleInputActions(bool activate, bool isEditor)
        {
            _toggleExplorerAction.shouldBeEnabled = activate;
        }

        protected override void OnCreate()
        {
            base.OnCreate();
            //Logging.Info($"OnCreate in {nameof(IndustriesExtendedDLC)}");

            _toggleExplorerAction = Mod._settings.GetAction(Settings.ToggleToolAction);
            _toggleExplorerAction.onInteraction += OnToggleSceneExplorerTool;
        }

        private void OnToggleSceneExplorerTool(ProxyAction proxyAction, InputActionPhase inputActionPhase)
        {
            if (inputActionPhase != InputActionPhase.Performed)
            {
                return;
            }

            World.GetExistingSystemManaged<TestQuery>().DoTesting();


        }
        
        protected override void OnDestroy()
        {
            _toggleExplorerAction.onInteraction -= OnToggleSceneExplorerTool;
            base.OnDestroy();
        }
    }
}
