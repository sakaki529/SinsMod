using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace SinsMod.Waters
{
    public class MysticWaterStyle : ModWaterStyle
    {
        public override bool ChooseWaterStyle() => Main.bgStyle == mod.GetSurfaceBgStyleSlot("MysticSurfaceBgStyle");
        public override int ChooseWaterfallStyle() => mod.GetWaterfallStyleSlot("MysticWaterfallStyle");
        public override int GetSplashDust() => mod.DustType("MysticWaterSplash");
        public override int GetDropletGore() => mod.GetGoreSlot("Gores/MysticDroplet");
        public override void LightColorMultiplier(ref float r, ref float g, ref float b)
        {
            r = 1f;
            g = 1f;
            b = 1f;
        }
        public override Color BiomeHairColor()  => new Color(220, 70, 250);
    }
}