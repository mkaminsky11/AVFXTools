﻿using AVFXLib.AVFX;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AVFXLib.Models
{
    public class AVFXBinder : Base
    {
        public const string NAME = "Bind";

        public LiteralBool StartToGlobalDirection = new LiteralBool("startToGlobalDirection", "bStG");
        public LiteralBool VfxScaleEnabled = new LiteralBool("vfxScaleEnabled", "bVSc");
        public LiteralFloat VfxScaleBias = new LiteralFloat("vfxScaleBias", "bVSb");
        public LiteralBool VfxScaleDepthOffset = new LiteralBool("vfxScaleDepthOffset", "bVSd");
        public LiteralBool VfxScaleInterpolation = new LiteralBool("vfxScaleInterpolation", "bVSi");
        public LiteralBool TransformScale = new LiteralBool("transformScale", "bTSc");
        public LiteralBool TransformScaleDepthOffset = new LiteralBool("transformScaleDepthOffset", "bTSd");
        public LiteralBool TransformScaleInterpolation = new LiteralBool("transformScaleInterpolation", "bTSi");
        public LiteralBool FollowingTargetOrientation = new LiteralBool("followingTargetOrientation", "bFTO");
        public LiteralBool DocumentScaleEnabled = new LiteralBool("documentScaleEnabled", "bDSE");
        public LiteralBool AdjustToScreenEnabled = new LiteralBool("adjustToScreenEnabled", "bATS");
        public LiteralInt Life = new LiteralInt("life", "Life");
        public LiteralEnum BinderRotationType = new LiteralEnum(new BinderRotation(), "rotationtype", "RoTp");
        public LiteralEnum BType = new LiteralEnum(new BinderType(), "binderType", "BnVr");
        public AVFXBinderProperty Prop = new AVFXBinderProperty("properties");

        public string Type;
        public AVFXBinderData Data;

        List<Base> Attributes;

        public AVFXBinder() : base("binder", NAME)
        {
            Attributes = new List<Base>(new Base[]{
                StartToGlobalDirection,
                VfxScaleEnabled,
                VfxScaleBias,
                VfxScaleDepthOffset,
                VfxScaleInterpolation,
                TransformScale,
                TransformScaleDepthOffset,
                TransformScaleInterpolation,
                FollowingTargetOrientation,
                DocumentScaleEnabled,
                AdjustToScreenEnabled,
                Life,
                BinderRotationType,
                BType,
                Prop
            });
        }

        public override void read(JObject elem)
        {
            Assigned = true;
            ReadJSON(Attributes, elem);
            Type = BType.Value;

            // Data
            //========================//
            SetType(Type);
            ReadJSON(Data, elem);
        }

        public override void read(AVFXNode node)
        {
            Assigned = true;
            ReadAVFX(Attributes, node);
            Type = BType.Value;

            foreach (AVFXNode item in node.Children)
            {
                switch (item.Name)
                {
                    // DATA =========================
                    case AVFXParticleData.NAME:
                        SetType(Type);
                        ReadAVFX(Data, node);
                        break;
                }
            }
        }

        public override JToken toJSON()
        {
            JObject elem = new JObject();

            PutJSON(elem, Attributes);
            PutJSON(elem, Data);

            return elem;
        }

        public override AVFXNode toAVFX()
        {
            AVFXNode bindAvfx = new AVFXNode("Bind");
            PutAVFX(bindAvfx, Attributes);
            PutAVFX(bindAvfx, Data);

            return bindAvfx;
        }

        public override void Print(int level)
        {
            Console.WriteLine("{0}------- BIND --------", new String('\t', level));
            Output(Attributes, level);
            Output(Data, level);
        }

        public void SetType(string type)
        {
            switch (type)
            {
                case "Point":
                    Data = new AVFXBinderDataPoint("data");
                    break;
                case "Linear":
                    throw new System.InvalidOperationException("Linear Binder Data!");
                    break;
                case "Spline":
                    throw new System.InvalidOperationException("Spline Particle Data!");
                    break;
                case "Camera":
                    throw new System.InvalidOperationException("Camera Particle Data!");
                    break;
            }
        }
    }
}