using System;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;

namespace SinsMod.Skies
{
    public class LustScreenShaderData : ScreenShaderData
    {
        Mod mod = ModLoader.GetMod("SinsMod");
        private int LustIndex;
        public LustScreenShaderData(string passName) : base(passName)
        {
        }
        private void UpdatePIndex()
        {
            int num = mod.NPCType("Lust");
            int num2 = mod.NPCType("Lust");
            if (LustIndex >= 0 && Main.npc[LustIndex].active && (Main.npc[LustIndex].type == num || Main.npc[LustIndex].type == num2))
            {
                return;
            }
            LustIndex = -1;
            for (int i = 0; i < Main.npc.Length; i++)
            {
                if (Main.npc[i].active && (Main.npc[i].type == num || Main.npc[i].type == num2))
                {
                    LustIndex = i;
                    return;
                }
            }
        }
        public override void Apply()
        {
            UpdatePIndex();
            if (LustIndex != -1)
            {
                UseTargetPosition(Main.npc[LustIndex].Center);
            }
            base.Apply();
        }
    }
}