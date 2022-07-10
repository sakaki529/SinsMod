using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace SinsMod.Waters
{
    public class DistortionWaterStyle : ModWaterStyle
    {
        public override bool ChooseWaterStyle() => Main.bgStyle == mod.GetSurfaceBgStyleSlot("DistortionSurfaceBgStyle");
        public override int ChooseWaterfallStyle() => mod.GetWaterfallStyleSlot("DistortionWaterfallStyle");
        public override int GetSplashDust() => mod.DustType("DistortionWaterSplash");
        public override int GetDropletGore() => mod.GetGoreSlot("Gores/DistortionDroplet");
        public override void LightColorMultiplier(ref float r, ref float g, ref float b)
        {
            r = 1f;
            g = 1f;
            b = 1f;
        }
        public override Color BiomeHairColor()  => new Color(220, 70, 250);
    }
}