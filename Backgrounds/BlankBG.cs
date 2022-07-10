using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Backgrounds
{
    public class BlankBG : ModSurfaceBgStyle
    {
        public override bool ChooseBgStyle()
        {
            //return !Main.gameMenu && NPC.AnyNPCs(mod.NPCType(""));
            return false;
        }
        public override void ModifyFarFades(float[] fades, float transitionSpeed)
        {
            for (int i = 0; i < fades.Length; i++)
            {
                if (i == Slot)
                {
                    fades[i] += transitionSpeed;
                    if (fades[i] > 1f)
                    {
                        fades[i] = 1f;
                    }
                }
                else
                {
                    fades[i] -= transitionSpeed;
                    if (fades[i] < 0f)
                    {
                        fades[i] = 0f;
                    }
                }
            }
        }
        public override int ChooseFarTexture()
        {
            return mod.GetBackgroundSlot("Extra/Placeholder/BlankTex");
        }
        public override int ChooseMiddleTexture()
        {
            return mod.GetBackgroundSlot("Extra/Placeholder/BlankTex");
        }
        public override int ChooseCloseTexture(ref float scale, ref double parallax, ref float a, ref float b)
        {
            return mod.GetBackgroundSlot("Extra/Placeholder/BlankTex");
        }
    }
}