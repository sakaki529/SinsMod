using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Graphics.Shaders;

namespace SinsMod.Skies
{
    public class DistortionScreenShaderData : ScreenShaderData
    {
        public DistortionScreenShaderData(string passName) : base(passName)
        {
        }
        public override void Apply()
        {
            Vector3 vector = Main.bgColor.ToVector3();
            vector *= 0.4f;
            UseOpacity(Math.Max(vector.X, Math.Max(vector.Y, vector.Z)));
            base.Apply();
        }
    }
}