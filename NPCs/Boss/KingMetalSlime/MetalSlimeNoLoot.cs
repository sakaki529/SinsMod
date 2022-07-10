using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.NPCs.KingMetalSlime
{
    public class MetalSlimeNoLoot : ModNPC
    {
        public override string Texture => "SinsMod/NPCs/NormalNPCs/MetalSlime";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Metal Slime");
            Main.npcFrameCount[npc.type] = 2;
            animationType = 1;
        }
        public override void SetDefaults()
        {
            npc.width = 32;
            npc.height = 24;
            npc.lifeMax = 10;
            npc.damage = 22;
            npc.knockBackResist = 0.2f;
            npc.aiStyle = 1;
            npc.npcSlots = 0.5f;
            npc.HitSound = SoundID.NPCHit4;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.value = Item.sellPrice(0, 0, 1, 50);
            for (int i = 0; i < npc.buffImmune.Length; i++)
            {
                npc.buffImmune[i] = true;
            }
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = 15;
            npc.damage = 33;
            npc.value = Item.buyPrice(0, 0, 3, 0);
        }
        public override bool StrikeNPC(ref double damage, int defense, ref float knockback, int hitDirection, ref bool crit)
        {
            damage = 1;
            if(crit)
            {
                damage = 2;
            }
            return false;
        }
        public override void HitEffect(int hitDirection, double damage)
        {
            if (npc.life <= 0)
            {
                for (int k = 0; k < 20; k++)
                {
                    Dust.NewDust(npc.position, npc.width, npc.height, 11, 1.5f * hitDirection, 1f, 0, default(Color), 0.7f);
                    Dust.NewDust(npc.position, npc.width, npc.height, 11, -1.5f * hitDirection, -1f, 0, default(Color), 0.7f);
                }
            }
        }
        public override void AI()
        {
            if(npc.wet == true)
            {
                npc.velocity.Y = +50;
            }
        }
        public override bool PreNPCLoot()
        {
            return false;
        }
    }
}