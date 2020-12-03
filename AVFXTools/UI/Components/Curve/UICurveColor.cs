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
    public class UICurveColor : UIBase
    {
        public AVFXCurveColor Curve;
        public bool Assigned = false;
        public string Name;
        //=========================

        public UICurveColor(AVFXCurveColor curve, string name)
        {
            Curve = curve;
            Name = name;
            if (!curve.Assigned) { Assigned = false; return; }
            Assigned = true;
            // ======================
            Attributes.Add(new UICurve(Curve.RGB, "RGB", color:true));
            Attributes.Add(new UICurve(Curve.A, "Alpha"));
            Attributes.Add(new UICurve(Curve.SclR, "Scale R"));
            Attributes.Add(new UICurve(Curve.SclG, "Scale G"));
            Attributes.Add(new UICurve(Curve.SclB, "Scale B"));
            Attributes.Add(new UICurve(Curve.SclA, "Scale Alpha"));
            Attributes.Add(new UICurve(Curve.Bri, "Brightness"));
            Attributes.Add(new UICurve(Curve.RanR, "Random R"));
            Attributes.Add(new UICurve(Curve.RanG, "Random G"));
            Attributes.Add(new UICurve(Curve.RanB, "Random B"));
            Attributes.Add(new UICurve(Curve.RanA, "Random Alpha"));
            Attributes.Add(new UICurve(Curve.RBri, "Random Brightness"));
        }

        public override void Draw(string parentId)
        {
            string id = parentId + "/" + Name;
            // === UNASSIGNED ===
            if (!Assigned)
            {
                if (ImGui.Button("+ " + Name + id))
                {
                    // TODO
                }
                return;
            }
            // ==== ASSIGNED ===
            if (ImGui.TreeNode(Name + id))
            {
                if (UIUtils.RemoveButton("Delete" + id))
                {
                    // TODO
                }
                DrawAttrs(id);
                ImGui.TreePop();
            }
        }
    }
}
