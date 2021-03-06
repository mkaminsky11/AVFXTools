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
    public class UIParticleDataLaser : UIBase
    {
        public AVFXParticleDataLaser Data;
        //==========================

        public UIParticleDataLaser(AVFXParticleDataLaser data)
        {
            Data = data;
            Init();
        }
        public override void Init()
        {
            base.Init();
            //=======================
            Attributes.Add(new UICurve(Data.Width, "Width"));
            Attributes.Add(new UICurve(Data.Length, "Length"));
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
