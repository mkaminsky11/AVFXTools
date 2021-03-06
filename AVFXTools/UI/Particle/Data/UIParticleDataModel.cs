﻿using AVFXLib.Models;
using AVFXTools.Main;
using ImGuiNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AVFXTools.UI
{
    public class UIParticleDataModel : UIBase
    {
        public AVFXParticleDataModel Data;
        //==========================

        public UIParticleDataModel(AVFXParticleDataModel data)
        {
            Data = data;
            Init();
        }
        public override void Init()
        {
            base.Init();
            //=======================
            Attributes.Add(new UIInt("Model Number Random", Data.ModelNumberRandomValue));
            Attributes.Add(new UICombo<RandomType>("Model Number Random Type", Data.ModelNumberRandomType));
            Attributes.Add(new UIInt("Model Number Random Interval", Data.ModelNumberRandomInterval));
            Attributes.Add(new UICombo<FresnelType>("Fresnel Type", Data.FresnelType));
            Attributes.Add(new UICombo<DirectionalLightType>("Directional Light Type", Data.DirectionalLightType));
            Attributes.Add(new UICombo<PointLightType>("Point Light Type", Data.PointLightType));
            Attributes.Add(new UICheckbox("Is Lightning", Data.IsLightning));
            Attributes.Add(new UICheckbox("Is Morph", Data.IsMorph));
            Attributes.Add(new UIInt("Model Index", Data.ModelIdx));
            Attributes.Add(new UICurve(Data.Morph, "Morph"));
            Attributes.Add(new UICurve(Data.FresnelCurve, "Fresnel Curve"));
            Attributes.Add(new UICurve3Axis(Data.FresnelRotation, "Fresnel Rotation"));
            Attributes.Add(new UICurveColor(Data.ColorBegin, "Color Begin"));
            Attributes.Add(new UICurveColor(Data.ColorEnd, "Color End"));
        }

        public override void Draw(string parentId)
        {
            string id = parentId + "/Data";
            if (ImGui.TreeNode("Data" + id))
            {
                DrawAttrs(id);
                ImGui.TreePop();
            }
        }
    }
}
