using System;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;

namespace SinsMod.Skies
{
    public class NothingnessScreenShaderData : ScreenShaderData
    {
        Mod mod = ModLoader.GetMod("SinsMod");
        private int NothingnessIndex;
        public NothingnessScreenShaderData(string passName) : base(passName)
        {
        }
        private void UpdatePIndex()
        {
            int num = mod.NPCType("BlackCrystalCore");
            int num2 = mod.NPCType("WillOfMadness");
            int num3 = mod.NPCType("BlackCrystal");
            int num4 = mod.NPCType("BlackCrystalNoMove");
            if (NothingnessIndex >= 0 && Main.npc[NothingnessIndex].active && (Main.npc[NothingnessIndex].type == num || Main.npc[NothingnessIndex].type == num2 || Main.npc[NothingnessIndex].type == num3 || Main.npc[NothingnessIndex].type == num4))
            {
                return;
            }
            NothingnessIndex = -1;
            for (int i = 0; i < Main.npc.Length; i++)
            {
                if (Main.npc[i].active && (Main.npc[i].type == num || Main.npc[i].type == num2 || Main.npc[i].type == num3))
                {
                    NothingnessIndex = i;
                    return;
                }
            }
        }
        public override void Apply()
        {
            UpdatePIndex();
            if (NothingnessIndex != -1)
            {
                UseTargetPosition(Main.npc[NothingnessIndex].Center);
            }
            base.Apply();
        }
    }
}