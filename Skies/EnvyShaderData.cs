using System;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;

namespace SinsMod.Skies
{
    public class EnvyScreenShaderData : ScreenShaderData
    {
        Mod mod = ModLoader.GetMod("SinsMod");
        private int EnvyIndex;
        public EnvyScreenShaderData(string passName) : base(passName)
        {
        }
        private void UpdatePIndex()
        {
            int num = mod.NPCType("Envy");
            int num2 = mod.NPCType("Envy");
            if (EnvyIndex >= 0 && Main.npc[EnvyIndex].active && (Main.npc[EnvyIndex].type == num || Main.npc[EnvyIndex].type == num2))
            {
                return;
            }
            EnvyIndex = -1;
            for (int i = 0; i < Main.npc.Length; i++)
            {
                if (Main.npc[i].active && (Main.npc[i].type == num || Main.npc[i].type == num2))
                {
                    EnvyIndex = i;
                    return;
                }
            }
        }
        public override void Apply()
        {
            UpdatePIndex();
            if (EnvyIndex != -1)
            {
                UseTargetPosition(Main.npc[EnvyIndex].Center);
            }
            base.Apply();
        }
    }
}