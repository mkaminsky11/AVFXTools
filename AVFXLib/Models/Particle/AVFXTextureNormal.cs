﻿using AVFXLib.AVFX;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AVFXLib.Models
{
    public class AVFXTextureNormal : Base
    {
        public LiteralBool Enabled = new LiteralBool("enabled", "bEna");
        public LiteralInt UvSetIdx = new LiteralInt("uvSetIdx", "UvSN");
        public LiteralEnum TextureFilter = new LiteralEnum(new TextureFilterType(), "textureFilter", "TFT");
        public LiteralEnum TextureBorderU = new LiteralEnum(new TextureBorderType(), "textureBorderU", "TBUT");
        public LiteralEnum TextureBorderV = new LiteralEnum(new TextureBorderType(), "textureBorderV", "TBVT");
        public LiteralInt TextureIdx = new LiteralInt("textureIdx", "TxNo");
        public AVFXCurve NPow = new AVFXCurve("normalPower", "NPow");

        List<Base> Attributes;

        public AVFXTextureNormal(string jsonPath) : base(jsonPath, "TN")
        {
            Attributes = new List<Base>(new Base[]{
                Enabled,
                UvSetIdx,
                TextureFilter,
                TextureBorderU,
                TextureBorderV,
                TextureIdx,
                NPow
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
            AVFXNode dataAvfx = new AVFXNode("TN");
            PutAVFX(dataAvfx, Attributes);
            return dataAvfx;
        }

        public override void Print(int level)
        {
            Console.WriteLine("{0}------- TN --------", new String('\t', level));
            Output(Attributes, level);
        }
    }
}