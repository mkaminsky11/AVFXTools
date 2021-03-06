﻿using AVFXLib.Models;
using AVFXTools.FFXIV;
using AVFXTools.ApplicationBase;
using System.Numerics;
using Veldrid;

namespace AVFXTools.Main
{
    public class Core
    {
        public AVFXBase B;

        public MainViewer Main;
        public GraphicsDevice GD;
        public ResourceFactory Factory;
        public CommandList CL;
        public Swapchain Chain;
        public Camera Cam;

        public TimelineItem[] Timelines;
        public EmitterItem[] Emitters;
        public ParticleItem[] Particles;
        public ModelItem[] Models;
        public BinderItem[] Binders;
        public Textures Tex;
        public WepModelItem Wep;

        public Core(AVFXBase avfx, ResourceGetter getter, WepModel model, MainViewer viewer, GraphicsDevice gd, ResourceFactory factory, CommandList cl, Swapchain swapChain, Camera camera)
        {
            B = avfx;
            Main = viewer;
            GD = gd;
            Factory = factory;
            CL = cl;
            Chain = swapChain;
            Cam = camera;
            // ==================
            Tex = new Textures(avfx.Textures, getter, this);
            if (model != null && model.Valid)
            {
                Wep = new WepModelItem(model, this);
            }
            Binders = BinderItem.GetArray(avfx.Binders, model);
            Models = ModelItem.GetArray(avfx.Models);
            Particles = ParticleItem.GetArray(avfx.Particles, this);
            Emitters = EmitterItem.GetArray(avfx.Emitters, this);
            Timelines = TimelineItem.GetArray(avfx.Timelines, this);

            // start, I guess
            foreach(var T in Timelines)
            {
                T.Start();
            }
        }

        public void AddParticleInstance(int idx, GenericInstance parent, Matrix4x4 startTransform, EmitterCreateStruct createData, int num=1)
        {
            Particles[idx].AddInstance(parent, startTransform, createData, num);
        }

        public void AddEmitterInstance(int idx, GenericInstance parent, Matrix4x4 startTransform, EmitterCreateStruct createData, int num=1)
        {
            Emitters[idx].AddInstance(parent, startTransform, createData, num);
        }



        public void Update(float dT)
        {
            float dTime = dT * 20; // SCALING OR SOMETHING
            foreach (EmitterItem e in Emitters)
            {
                e.Update(dTime);
            }
            foreach (ParticleItem p in Particles)
            {
                p.Update(dTime);
            }
        }

        public void Draw()
        {
            if (Wep != null)
            {
                Wep.Draw();
            }
            foreach(ParticleItem p in Particles)
            {
                p.Draw();
            }
        }
    }
}
