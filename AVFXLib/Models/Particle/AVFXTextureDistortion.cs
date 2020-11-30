﻿using AVFXLib.AVFX;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AVFXLib.Models
{
    public class AVFXTextureDistortion : Base
    {
        public LiteralBool Enabled = new LiteralBool("enabled", "bEna");
        public LiteralBool TargetUV1 = new LiteralBool("targetUV1", "bT1");
        public LiteralBool TargetUV2 = new LiteralBool("targetUV2", "bT2");
        public LiteralBool TargetUV3 = new LiteralBool("targetUV3", "bT3");
        public LiteralBool TargetUV4 = new LiteralBool("targetUV4", "bT4");
        public LiteralInt UvSetIdx = new LiteralInt("uvSetIdx", "UvSN");
        public LiteralEnum TextureFilter = new LiteralEnum(new TextureFilterType(), "textureFilter", "TFT");
        public LiteralEnum TextureBorderU = new LiteralEnum(new TextureBorderType(), "textureBorderU", "TBUT");
        public LiteralEnum TextureBorderV = new LiteralEnum(new TextureBorderType(), "textureBorderV", "TBVT");
        public LiteralInt TextureIdx = new LiteralInt("textureIdx", "TxNo");
        public AVFXCurve DPow = new AVFXCurve("distortPower", "DPow");

        List<Base> Attributes;

        public AVFXTextureDistortion(string jsonPath) : base(jsonPath, "TD")
        {
            Attributes = new List<Base>(new Base[]{
                Enabled,
                TargetUV1,
                TargetUV2,
                TargetUV3,
                TargetUV4,
                UvSetIdx,
                TextureFilter,
                TextureBorderU,
                TextureBorderV,
                TextureIdx,
                DPow
            });
        }

        public override void read(JObject elem)
        {
            Assigned = true;
            ReadJSON(Attributes, elem);
        }

        public override void read(AVFXNode node)
        {
            Assigned = true;
            ReadAVFX(Attributes, node);
        }

        public override JToken toJSON()
        {
            JObject elem = new JObject();
            PutJSON(elem, Attributes);
            return elem;
        }

        public override AVFXNode toAVFX()
        {
            AVFXNode dataAvfx = new AVFXNode("TD");
            PutAVFX(dataAvfx, Attributes);
            return dataAvfx;
        }

        public override void Print(int level)
        {
            Console.WriteLine("{0}------- TD --------", new String('\t', level));
            Output(Attributes, level);
        }
    }
}