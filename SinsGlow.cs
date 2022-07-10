using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace SinsMod
{
    public class SinsGlow
    {
        internal static short SetStaticDefaultsGlowMask(ModItem modItem)
        {
            Mod mod = ModLoader.GetMod("SinsMod");
            if (!Main.dedServ)
            {
                Texture2D[] glowMasks = new Texture2D[Main.glowMaskTexture.Length + 1];
                for (int i = 0; i < Main.glowMaskTexture.Length; i++)
                {
                    glowMasks[i] = Main.glowMaskTexture[i];
                }
                glowMasks[glowMasks.Length - 1] = mod.GetTexture("Glow/Item/" + modItem.GetType().Name + "_Glow");
                Main.glowMaskTexture = glowMasks;
                return (short)(glowMasks.Length - 1);
            }
            else return 0;
        }
        public static float DrawOpacity(Player drawPlayer)
        {
            return ShadowOpacity(drawPlayer) * ImmuneOpacity(drawPlayer);
        }
        public static float ShadowOpacity(Player drawPlayer)
        {
            float num = 1f;
            float num2 = drawPlayer.stealth;
            if (num2 < 0.03)
            {
                num2 = 0.03f;
            }
            float num3 = (1f + num2 * 10f) / 11f;
            if (num2 < 0f)
            {
                num2 = 0f;
            }
            if (num2 >= 1f - drawPlayer.shadow && drawPlayer.shadow > 0f)
            {
                num2 = drawPlayer.shadow * 0.5f;
            }
            num = num3;
            return num * (1f - drawPlayer.shadow);
        }
        public static float ImmuneOpacity(Player drawPlayer)
        {
            return (byte.MaxValue - drawPlayer.immuneAlpha) / (float)byte.MaxValue;
        }
    }
}