﻿using AVFXLib.AVFX;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AVFXLib.Models
{
    public class AVFXTextureColor1 : Base
    {
        public LiteralBool Enabled = new LiteralBool("enabled", "bEna");
        public LiteralBool ColorToAlpha = new LiteralBool("colorToAlpha", "bC2A");
        public LiteralBool UseScreenCopy = new LiteralBool("useScreenCopy", "bUSC");
        public LiteralBool PreviousFrameCopy = new LiteralBool("previousFrameCopy", "bPFC");
        public LiteralInt UvSetIdx = new LiteralInt("uvSetIdx", "UvSN");
        public LiteralEnum TextureFilter = new LiteralEnum(new TextureFilterType(), "textureFilter", "TFT");
        public LiteralEnum TextureBorderU = new LiteralEnum(new TextureBorderType(), "textureBorderU", "TBUT");
        public LiteralEnum TextureBorderV = new LiteralEnum(new TextureBorderType(), "textureBorderV", "TBVT");
        public LiteralEnum TextureCalculateColor = new LiteralEnum(new TextureCalculateColor(), "textureCalculateColor", "TCCT");
        public LiteralEnum TextureCalculateAlpha = new LiteralEnum(new TextureCalculateAlpha(), "textureCalculateAlpha", "TCAT");
        public LiteralInt TextureIdx = new LiteralInt("textureIdx", "TxNo");
        public LiteralInt MaskTextureIdx = new LiteralInt("maskTextureIdx", "TLst");

        List<Base> Attributes;

        public AVFXTextureColor1(string jsonPath) : base(jsonPath, "TC1")
        {
            Attributes = new List<Base>(new Base[]{
                Enabled,
                ColorToAlpha,
                UseScreenCopy,
                PreviousFrameCopy,
                UvSetIdx,
                TextureFilter,
                TextureBorderU,
                TextureBorderV,
                TextureCalculateColor,
                TextureCalculateAlpha,
                TextureIdx,
                MaskTextureIdx
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
            AVFXNode dataAvfx = new AVFXNode("TC1");
            PutAVFX(dataAvfx, Attributes);
            return dataAvfx;
        }

        public override void Print(int level)
        {
            Console.WriteLine("{0}------- TC1 --------", new String('\t', level));
            Output(Attributes, level);
        }
    }
}