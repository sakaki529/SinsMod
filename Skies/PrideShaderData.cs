using System;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;

namespace SinsMod.Skies
{
    public class PrideScreenShaderData : ScreenShaderData
    {
        Mod mod = ModLoader.GetMod("SinsMod");
        private int PrideIndex;
        public PrideScreenShaderData(string passName) : base(passName)
        {
        }
        private void UpdatePIndex()
        {
            int num = mod.NPCType("Pride");
            int num2 = mod.NPCType("Pride");
            if (PrideIndex >= 0 && Main.npc[PrideIndex].active && (Main.npc[PrideIndex].type == num || Main.npc[PrideIndex].type == num2))
            {
                return;
            }
            PrideIndex = -1;
            for (int i = 0; i < Main.npc.Length; i++)
            {
                if (Main.npc[i].active && (Main.npc[i].type == num || Main.npc[i].type == num2))
                {
                    PrideIndex = i;
                    return;
                }
            }
        }
        public override void Apply()
        {
            UpdatePIndex();
            if (PrideIndex != -1)
            {
                UseTargetPosition(Main.npc[PrideIndex].Center);
            }
            base.Apply();
        }
    }
}