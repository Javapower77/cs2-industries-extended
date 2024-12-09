using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using Colossal.Entities;
using Colossal.Logging;
using Colossal.PSI.Environment;
using Colossal.Serialization.Entities;
using Colossal.UI.Binding;
using Game;
using Game.Common;
using Game.Input;
using Game.Objects;
using Game.Prefabs;
using Game.Prefabs.Climate;
using Game.Rendering;
using Game.Simulation;
using Game.Tools;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using UnityEngine;
using IndustriesExtended.Extensions;

namespace IndustriesExtended.Systems
{
    public partial class TestFieldsUISystem : ExtendedInfoSectionBase
    {
        protected override string group => Mod.Id;

        public override void OnWriteProperties(IJsonWriter writer)
        {

        }

        protected override void OnProcess()
        {

        }

        protected override void Reset()
        {

        }
        protected override void OnCreate()
        {
            base.OnCreate();
            //m_InfoUISystem.AddMiddleSection(this);
            LogUtil.Info($"{nameof(TestFieldsUISystem)}.{nameof(OnCreate)}");
        }

        protected override void OnUpdate()
        {
            base.visible = true;
        }
    }
}
