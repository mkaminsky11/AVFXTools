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
    public class UIEffectorDataCameraQuake : UIBase
    {
        public AVFXEffectorDataCameraQuake Data;
        //==========================

        public UIEffectorDataCameraQuake(AVFXEffectorDataCameraQuake data)
        {
            Data = data;
            //=======================
            Attributes.Add(new UICurve(Data.Attenuation, "Attenuation"));
            Attributes.Add(new UICurve(Data.RadiusOut, "Radius Out"));
            Attributes.Add(new UICurve(Data.RadiusIn, "Radius In"));
            Attributes.Add(new UICurve3Axis(Data.Rotation, "Rotation"));
            Attributes.Add(new UICurve3Axis(Data.Position, "Position"));
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
