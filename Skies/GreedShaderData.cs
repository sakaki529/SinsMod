using System;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;

namespace SinsMod.Skies
{
    public class GreedScreenShaderData : ScreenShaderData
    {
        Mod mod = ModLoader.GetMod("SinsMod");
        private int GreedIndex;
        public GreedScreenShaderData(string passName) : base(passName)
        {
        }
        private void UpdatePIndex()
        {
            int num = mod.NPCType("Greed");
            int num2 = mod.NPCType("Greed");
            if (GreedIndex >= 0 && Main.npc[GreedIndex].active && (Main.npc[GreedIndex].type == num || Main.npc[GreedIndex].type == num2))
            {
                return;
            }
            GreedIndex = -1;
            for (int i = 0; i < Main.npc.Length; i++)
            {
                if (Main.npc[i].active && (Main.npc[i].type == num || Main.npc[i].type == num2))
                {
                    GreedIndex = i;
                    return;
                }
            }
        }
        public override void Apply()
        {
            UpdatePIndex();
            if (GreedIndex != -1)
            {
                UseTargetPosition(Main.npc[GreedIndex].Center);
            }
            base.Apply();
        }
    }
}