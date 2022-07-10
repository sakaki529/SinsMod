using Microsoft.Xna.Framework;
using Terraria.Graphics.Effects;
using Terraria.ModLoader;

namespace SinsMod.Backgrounds
{
    public class FogHandler : ModWorld
    {
        readonly ScreenFog Fog = new ScreenFog(false);
        public static OverlayManager Scene = new OverlayManager();
        public override void PostDrawTiles()
        {
            Color DefaultFog = new Color(62, 68, 100);
            Fog.Update(mod.GetTexture("Backgrounds/FogTex"));
            Fog.Draw(mod.GetTexture("Backgrounds/FogTex"), false, Color.White, true);
        }
    }
}