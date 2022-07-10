using System;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;

namespace SinsMod.Skies
{
    public class WrathScreenShaderData : ScreenShaderData
    {
        Mod mod = ModLoader.GetMod("SinsMod");
        private int WrathIndex;
        public WrathScreenShaderData(string passName) : base(passName)
        {
        }
        private void UpdatePIndex()
        {
            int num = mod.NPCType("Wrath");
            int num2 = mod.NPCType("Wrath");
            if (WrathIndex >= 0 && Main.npc[WrathIndex].active && (Main.npc[WrathIndex].type == num || Main.npc[WrathIndex].type == num2))
            {
                return;
            }
            WrathIndex = -1;
            for (int i = 0; i < Main.npc.Length; i++)
            {
                if (Main.npc[i].active && (Main.npc[i].type == num || Main.npc[i].type == num2))
                {
                    WrathIndex = i;
                    return;
                }
            }
        }
        public override void Apply()
        {
            UpdatePIndex();
            if (WrathIndex != -1)
            {
                UseTargetPosition(Main.npc[WrathIndex].Center);
            }
            base.Apply();
        }
    }
}