using System;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;

namespace SinsMod.Skies
{
    public class GluttonyScreenShaderData : ScreenShaderData
    {
        Mod mod = ModLoader.GetMod("SinsMod");
        private int GluttonyIndex;
        public GluttonyScreenShaderData(string passName) : base(passName)
        {
        }
        private void UpdatePIndex()
        {
            int num = mod.NPCType("Gluttony");
            int num2 = mod.NPCType("Gluttony");
            if (GluttonyIndex >= 0 && Main.npc[GluttonyIndex].active && (Main.npc[GluttonyIndex].type == num || Main.npc[GluttonyIndex].type == num2))
            {
                return;
            }
            GluttonyIndex = -1;
            for (int i = 0; i < Main.npc.Length; i++)
            {
                if (Main.npc[i].active && (Main.npc[i].type == num || Main.npc[i].type == num2))
                {
                    GluttonyIndex = i;
                    return;
                }
            }
        }
        public override void Apply()
        {
            UpdatePIndex();
            if (GluttonyIndex != -1)
            {
                UseTargetPosition(Main.npc[GluttonyIndex].Center);
            }
            base.Apply();
        }
    }
}