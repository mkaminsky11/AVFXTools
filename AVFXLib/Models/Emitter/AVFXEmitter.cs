﻿using AVFXLib.AVFX;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AVFXLib.Models
{
    public class AVFXEmitter : Base
    {
        public const string NAME = "Emit";

        public LiteralString Sound = new LiteralString("sound", "SdNm");
        public LiteralInt SoundNumber = new LiteralInt("soundNumber", "SdNo");
        public LiteralInt LoopStart = new LiteralInt("loopStart", "LpSt");
        public LiteralInt LoopEnd = new LiteralInt("loopEnd", "LpEd");
        public LiteralInt ChildLimit = new LiteralInt("childLimit", "ClCn");
        public LiteralInt EffectorIdx = new LiteralInt("effectorIdx", "EfNo");
        public LiteralBool AnyDirection = new LiteralBool("anyDirection", "bAD", size:1);
        public LiteralEnum<EmitterType> EmitterVariety = new LiteralEnum<EmitterType>("emitterType", "EVT");
        public LiteralEnum<RotationDirectionBase> RotationDirectionBase = new LiteralEnum<RotationDirectionBase>("rotationDirectionBase", "RBDT");
        public LiteralEnum<CoordComputeOrder> CoordComputeOrder = new LiteralEnum<CoordComputeOrder>("coordComputeOrder", "CCOT");
        public LiteralEnum<RotationOrder> RotationOrder = new LiteralEnum<RotationOrder>("rotationOrder", "ROT");
        public LiteralInt ParticleCount = new LiteralInt("particleCount", "PrCn");
        public LiteralInt EmitterCount = new LiteralInt("emitterCount", "EmCn");
        public AVFXLife Life = new AVFXLife("life");

        public AVFXCurve CreateCount = new AVFXCurve("createCount", "CrC");
        public AVFXCurve CreateCountRandom = new AVFXCurve("createCountRandom", "CrCR");
        public AVFXCurve CreateInterval = new AVFXCurve("createInterval", "CrI");
        public AVFXCurve CreateIntervalRandom = new AVFXCurve("createIntervalRandom", "CrIR");

        public AVFXCurve AirResistance = new AVFXCurve("airResistance", "ARs");

        public AVFXCurveColor Color = new AVFXCurveColor("color");
        public AVFXCurve3Axis Position = new AVFXCurve3Axis("position", "Pos");
        public AVFXCurve3Axis Rotation = new AVFXCurve3Axis("rotation", "Rot");
        public AVFXCurve3Axis Scale = new AVFXCurve3Axis("scale", "Scl");


        // ItPr
        public List<AVFXEmitterItem> ItEms = new List<AVFXEmitterItem>();
        public List<AVFXEmitterIteration> ItPrs = new List<AVFXEmitterIteration>();

        // Data
        public EmitterType Type;
        public AVFXEmitterData Data;

        List<Base> Attributes;

        public AVFXEmitter() : base("emitter", NAME)
        {
            Assigned = true;
            Attributes = new List<Base>(new Base[]{
                Sound,
                SoundNumber,
                LoopStart,
                LoopEnd,
                ChildLimit,
                EffectorIdx,
                AnyDirection,
                EmitterVariety,
                RotationDirectionBase,
                CoordComputeOrder,
                RotationOrder,
                ParticleCount,
                EmitterCount,
                Life,

                CreateCount,
                CreateCountRandom,
                CreateInterval,
                CreateIntervalRandom,

                AirResistance,

                Color,
                Position,
                Rotation,
                Scale,
            });
        }

        public override void read(JObject elem)
        {
            Assigned = true;
            ReadJSON(Attributes, elem);
            Type = EmitterVariety.Value;

            // ITEM
            //=======================//
            JArray itemElems = (JArray)elem.GetValue("items");
            foreach (JToken i in itemElems)
            {
                AVFXEmitterItem ItEm = new AVFXEmitterItem();
                ItEm.read((JObject)i);
                ItEms.Add(ItEm);
            }

            // ITPR
            //=======================//
            JArray itprElems = (JArray)elem.GetValue("itPr");
            foreach (JToken i in itprElems)
            {
                AVFXEmitterIteration ItPr = new AVFXEmitterIteration();
                ItPr.read((JObject)i);
                ItPrs.Add(ItPr);
            }

            // Data
            //========================//
            SetType(Type);
            ReadJSON(Data, elem);
        }

        public override void read(AVFXNode node)
        {
            Assigned = true;
            ReadAVFX(Attributes, node);
            Type = EmitterVariety.Value;

            foreach (AVFXNode item in node.Children){
                switch (item.Name){
                    // ItEm =================================
                    case AVFXEmitterItem.NAME:
                        AVFXEmitterItem ItEm = new AVFXEmitterItem();
                        ItEm.read(item);
                        ItEms.Add(ItEm);
                        break;

                    // ITPR =================================
                    case AVFXEmitterIteration.NAME:
                        AVFXEmitterIteration ItPr = new AVFXEmitterIteration();
                        ItPr.read(item);
                        ItPrs.Add(ItPr);
                        break;

                    // DATA ==================================
                    case AVFXEmitterData.NAME:
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

            // ItEm ========
            JArray itemArray = new JArray();
            foreach (AVFXEmitterItem item in ItEms)
            {
                itemArray.Add(item.toJSON());
            }
            elem["items"] = itemArray;

            // ItPr ========
            JArray itPrArray = new JArray();
            foreach (AVFXEmitterIteration itpr in ItPrs)
            {
                itPrArray.Add(itpr.toJSON());
            }
            elem["itPr"] = itPrArray;

            PutJSON(elem, Data);
            return elem;
        }

        public override AVFXNode toAVFX()
        {
            AVFXNode emitAvfx = new AVFXNode("Emit");

            PutAVFX(emitAvfx, Attributes);

            /*
            // ITEM
            //=======================//
            foreach (AVFXEmitterItem itemElem in ItEms)
            {
                PutAVFX(emitAvfx, itemElem);
            }

            // ITPR
            //=======================//
            foreach (AVFXEmitterIteration itprElem in ItPrs)
            {
                PutAVFX(emitAvfx, itprElem);
            }*/

            if(ItEms.Count > 0) // only care about the last one, since the others should all be determined by it
            {
                var lastItEm = ItEms[ItEms.Count - 1];
                var subItemCount = lastItEm.Items.Count;
                for(int i = 1; i <= subItemCount; i++)
                {
                    emitAvfx.Children.Add(lastItEm.toAVFXRange(i));
                }
            }

            if (ItPrs.Count > 0)
            {
                var lastItPr = ItPrs[ItPrs.Count - 1];
                var subItemCount = lastItPr.Items.Count;
                for (int i = 1; i <= subItemCount; i++)
                {
                    emitAvfx.Children.Add(lastItPr.toAVFXRange(i));
                }
            }

            PutAVFX(emitAvfx, Data);

            return emitAvfx;
        }

        public override void Print(int level)
        {
            Console.WriteLine("{0}------- EMIT --------", new String('\t', level));
            Output(Attributes, level);

            // ITEM
            //=======================//
            foreach (AVFXEmitterItem itemElem in ItPrs)
            {
                Output(itemElem, level);
            }

            // ITPR
            //=======================//
            foreach (AVFXEmitterIteration itprElem in ItPrs)
            {
                Output(itprElem, level);
            }

            Output(Data, level);
        }

        public void SetType(EmitterType type)
        {
            switch (type)
            {
                case EmitterType.Point: // no data here :)
                    break;
                case EmitterType.Cone:
                    throw new System.InvalidOperationException("Cone Emitter!");
                    break;
                case EmitterType.ConeModel:
                    throw new System.InvalidOperationException("Cone Model Emitter!");
                    break;
                case EmitterType.SphereModel:
                    Data = new AVFXEmitterDataSphereModel("data");
                    break;
                case EmitterType.CylinderModel:
                    Data = new AVFXEmitterDataCylinderModel("data");
                    break;
                case EmitterType.Model:
                    Data = new AVFXEmitterDataModel("data");
                    break;
            }
        }
    }
}
