﻿using AVFXLib.Models;
using AVFXTools.Main;
using ImGuiNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace AVFXTools.UI
{
    public class UICurve3Axis : UIBase
    {
        public AVFXCurve3Axis Curve;
        public bool Assigned = false;
        public string Name;
        //=========================
        public UICurve X;
        public UICurve Y;
        public UICurve Z;
        public UICurve RX;
        public UICurve RY;
        public UICurve RZ;

        public UICurve3Axis(AVFXCurve3Axis curve, string name)
        {
            Curve = curve;
            if (!curve.Assigned) return;
            Assigned = true;
            Name = name;
            // ======================
            Attributes.Add(new UICombo<AxisConnect>("Axis Connect", Curve.AxisConnectType));
            Attributes.Add(new UICombo<RandomType>("Axis Connect Random", Curve.AxisConnectRandomType));
            Attributes.Add(new UICurve(Curve.X, "X"));
            Attributes.Add(new UICurve(Curve.Y, "Y"));
            Attributes.Add(new UICurve(Curve.Z, "Z"));
            Attributes.Add(new UICurve(Curve.RX, "RX"));
            Attributes.Add(new UICurve(Curve.RY, "RY"));
            Attributes.Add(new UICurve(Curve.RZ, "RZ"));
        }

        public override void Draw(string parentId)
        {
            string id = parentId + "/" + Name;
            if (!Assigned) return;
            if (ImGui.TreeNode(Name + id))
            {
                DrawAttrs(id);
                ImGui.TreePop();
            }
        }
    }
}