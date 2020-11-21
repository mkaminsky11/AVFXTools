﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using AVFXLib.Models;
using AVFXTools.Main;
using ImGuiNET;
using Veldrid;

namespace AVFXTools.UI
{
    public class ImgUIMain
    {
        public AVFXBase AVFX;
        public MainViewer Main;
        public ImGuiRenderer I;
        public GraphicsDevice GD;
        public CommandList CL;

        UIBaseParameters Params;
        public List<UITimeline> Timelines = new List<UITimeline>();
        public List<UIParticle> Particles = new List<UIParticle>();
        public List<UIEmitter> Emitters = new List<UIEmitter>();
        public List<UIBinder> Binders = new List<UIBinder>();

        public ImgUIControls Controls;

        public ImgUIMain(AVFXBase avfx, MainViewer main, ImGuiRenderer imgui, GraphicsDevice gd, CommandList cl)
        {
            AVFX = avfx;
            Main = main;
            I = imgui;
            GD = gd;
            CL = cl;

            Controls = new ImgUIControls(this);

            Params = new UIBaseParameters(AVFX);
            foreach (var timeline in AVFX.Timelines)
            {
                Timelines.Add(new UITimeline(timeline));
            }
            foreach (var particle in AVFX.Particles)
            {
                Particles.Add(new UIParticle(particle));
            }
            foreach(var emitter in AVFX.Emitters)
            {
                Emitters.Add(new UIEmitter(emitter));
            }
            foreach(var binder in AVFX.Binders)
            {
                Binders.Add(new UIBinder(binder));
            }

            ImGui.SetNextWindowPos(new Vector2(10, 10));
            ImGui.SetNextWindowSize(new Vector2(400, 600));
        }

        public void Draw()
        {
            // ================================
            ImGui.Begin("AVFX");
            //================================
            ImGui.PushStyleColor(ImGuiCol.Button, new Vector4(0.8f, 0, 0, 1));
            if (ImGui.Button("UPDATE"))
            {
                Main.refreshGraphics();
            }
            ImGui.PopStyleColor();
            //================================
            if (ImGui.BeginTabBar("##MainTabs", ImGuiTabBarFlags.NoCloseWithMiddleMouseButton | ImGuiTabBarFlags.TabListPopupButton)) {
                if (ImGui.BeginTabItem("Parameters"))
                {
                    DrawParameters();
                    ImGui.EndTabItem();
                }
                if (ImGui.BeginTabItem("Scheduler"))
                {
                    DrawScheduler();
                    ImGui.EndTabItem();
                }
                if (ImGui.BeginTabItem("Timelines"))
                {
                    DrawTimelines();
                    ImGui.EndTabItem();
                }
                if (ImGui.BeginTabItem("Emitters"))
                {
                    DrawEmitters();
                    ImGui.EndTabItem();
                }
                if (ImGui.BeginTabItem("Particles"))
                {
                    DrawParticles();
                    ImGui.EndTabItem();
                }
                if (ImGui.BeginTabItem("Effectors"))
                {
                    DrawEffectors();
                    ImGui.EndTabItem();
                }
                if (ImGui.BeginTabItem("Binders"))
                {
                    DrawBinders();
                    ImGui.EndTabItem();
                }
                if (ImGui.BeginTabItem("Textures"))
                {
                    DrawTextures();
                    ImGui.EndTabItem();
                }
                if (ImGui.BeginTabItem("Models"))
                {
                    DrawModels();
                    ImGui.EndTabItem();
                }
                ImGui.EndTabBar();
            }

            Controls.Draw();

            I.Render(GD, CL);
        }

        // ======== PARAMETERS ========
        public void DrawParameters() {
            Params.Draw("##params");
        }
        public void DrawScheduler() { }
        // ======== TIMELINES =======
        public void DrawTimelines() {
            int idx = 0;
            foreach (var timeline in Timelines)
            {
                if (ImGui.CollapsingHeader("Timeline #" + idx))
                {
                    timeline.Draw("##timeline-" + idx);
                }
                idx++;
            }
        }
        // ====== EMITTERS ========
        public void DrawEmitters() {
            int idx = 0;
            foreach (var emitter in Emitters)
            {
                if (ImGui.CollapsingHeader("Emitter #" + idx + "(" + emitter.Emitter.EmitterType.Value + ")"))
                {
                    emitter.Draw("##emitter-" + idx);
                }
                idx++;
            }
        }
        // ====== PARTICLES ===========
        public void DrawParticles() {
            int idx = 0;
            foreach(var particle in Particles)
            {
                if(ImGui.CollapsingHeader("Particle #" + idx + "(" + particle.Particle.ParticleType.Value + ")"))
                {
                    particle.Draw("##particle-" + idx);
                }
                idx++;
            }
        }

        public void DrawEffectors() { }

        // ====== BINDERS =========
        public void DrawBinders() {
            int idx = 0;
            foreach (var binder in Binders)
            {
                if (ImGui.CollapsingHeader("Binder #" + idx + "(" + binder.Binder.BType.Value + ")"))
                {
                    binder.Draw("##binder-" + idx);
                }
                idx++;
            }
        }
        public void DrawTextures() { }
        public void DrawModels() { }
    }
}
