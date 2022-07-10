using System;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;

namespace SinsMod.Skies
{
    public class SinsScreenShaderData : ScreenShaderData
    {
        Mod mod = ModLoader.GetMod("SinsMod");
        private int SinsIndex;
        public SinsScreenShaderData(string passName) : base(passName)
        {
        }
        private void UpdatePIndex()
        {
            int num = mod.NPCType("Eden");
            int num2 = mod.NPCType("OriginWhite");
            int num3 = mod.NPCType("OriginBlack");
            if (SinsIndex >= 0 && Main.npc[SinsIndex].active && (Main.npc[SinsIndex].type == num || Main.npc[SinsIndex].type == num2 || Main.npc[SinsIndex].type == num3))
            {
                return;
            }
            SinsIndex = -1;
            for (int i = 0; i < Main.npc.Length; i++)
            {
                if (Main.npc[i].active && (Main.npc[i].type == num || Main.npc[i].type == num2 || Main.npc[i].type == num3))
                {
                    SinsIndex = i;
                    return;
                }
            }
        }
        public override void Apply()
        {
            UpdatePIndex();
            if (SinsIndex != -1)
            {
                UseTargetPosition(Main.npc[SinsIndex].Center);
            }
            base.Apply();
        }
    }
}