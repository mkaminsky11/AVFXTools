﻿using AVFXLib.AVFX;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AVFXLib.Models
{
    public class AVFXTextureReflection : Base
    {
        public LiteralBool Enabled = new LiteralBool("enabled", "bEna");
        public LiteralBool UseScreenCopy = new LiteralBool("useScreenCopy", "bUSC");
        public LiteralEnum TextureFilter = new LiteralEnum(new TextureFilterType(), "textureFilter", "TFT");
        public LiteralEnum TextureCalculateColor = new LiteralEnum(new TextureCalculateColor(), "textureCalculateColor", "TCCT");
        public LiteralInt TextureIdx = new LiteralInt("textureIdx", "TxNo");
        public AVFXCurve RPow = new AVFXCurve("reflectionPower", "RPow");

        List<Base> Attributes;

        public AVFXTextureReflection(string jsonPath) : base(jsonPath, "TR")
        {
            Attributes = new List<Base>(new Base[]{
                Enabled,
                UseScreenCopy,
                TextureFilter,
                TextureCalculateColor,
                TextureIdx,
                RPow
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
            AVFXNode dataAvfx = new AVFXNode("TR");
            PutAVFX(dataAvfx, Attributes);
            return dataAvfx;
        }

        public override void Print(int level)
        {
            Console.WriteLine("{0}------- TR --------", new String('\t', level));
            Output(Attributes, level);
        }
    }
}