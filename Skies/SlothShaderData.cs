using System;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;

namespace SinsMod.Skies
{
    public class SlothScreenShaderData : ScreenShaderData
    {
        Mod mod = ModLoader.GetMod("SinsMod");
        private int SlothIndex;
        public SlothScreenShaderData(string passName) : base(passName)
        {
        }
        private void UpdatePIndex()
        {
            int num = mod.NPCType("Sloth");
            int num2 = mod.NPCType("Sloth");
            if (SlothIndex >= 0 && Main.npc[SlothIndex].active && (Main.npc[SlothIndex].type == num || Main.npc[SlothIndex].type == num2))
            {
                return;
            }
            SlothIndex = -1;
            for (int i = 0; i < Main.npc.Length; i++)
            {
                if (Main.npc[i].active && (Main.npc[i].type == num || Main.npc[i].type == num2))
                {
                    SlothIndex = i;
                    return;
                }
            }
        }
        public override void Apply()
        {
            UpdatePIndex();
            if (SlothIndex != -1)
            {
                UseTargetPosition(Main.npc[SlothIndex].Center);
            }
            base.Apply();
        }
    }
}