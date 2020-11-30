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
    public class UITextureDistortion : UIBase
    {
        public AVFXTextureDistortion Tex;
        public string Name;
        public bool Assigned;
        //============================

        public UITextureDistortion(AVFXTextureDistortion tex)
        {
            Tex = tex;
            if (!tex.Assigned) return;
            Assigned = true;
            //====================
            Attributes.Add(new UICheckbox("Enabled", Tex.Enabled));
            Attributes.Add(new UICheckbox("Distort UV1", Tex.TargetUV1));
            Attributes.Add(new UICheckbox("Distort UV2", Tex.TargetUV2));
            Attributes.Add(new UICheckbox("Distort UV3", Tex.TargetUV3));
            Attributes.Add(new UICheckbox("Distort UV4", Tex.TargetUV4));
            Attributes.Add(new UIInt("UV Set Index", Tex.UvSetIdx));
            Attributes.Add(new UIInt("Texture Index", Tex.TextureIdx));
            Attributes.Add(new UICombo<TextureFilterType>("Texture Filter", Tex.TextureFilter));
            Attributes.Add(new UICombo<TextureBorderType>("Texture Border U", Tex.TextureBorderU));
            Attributes.Add(new UICombo<TextureBorderType>("Texture Border V", Tex.TextureBorderV));
            Attributes.Add(new UICurve(Tex.DPow, "Power"));
        }

        public override void Draw(string parentId)
        {
            string id = parentId + "/TD";
            if (!Assigned) return;
            if (ImGui.TreeNode("Distortion" + id))
            {
                DrawAttrs(id);
                ImGui.TreePop();
            }
        }
    }
}