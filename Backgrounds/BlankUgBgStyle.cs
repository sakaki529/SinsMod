using Terraria;
using Terraria.ModLoader;

namespace SinsMod.Backgrounds
{
    public class BlankUgBgStyle : ModUgBgStyle
    {
        public override bool ChooseBgStyle()
        {
            return false;
        }
        public override void FillTextureArray(int[] textureSlots)
        {
            textureSlots[0] = mod.GetBackgroundSlot("Extra/Placeholder/BlankTex");
            textureSlots[1] = mod.GetBackgroundSlot("Extra/Placeholder/BlankTex");
            textureSlots[2] = mod.GetBackgroundSlot("Extra/Placeholder/BlankTex");
            textureSlots[3] = mod.GetBackgroundSlot("Extra/Placeholder/BlankTex");
        }
    }
}