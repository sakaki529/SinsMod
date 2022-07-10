using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.NPCs.KingMetalSlime
{
    public class BigMetalSlimeNoLoot : ModNPC
    {
        public override string Texture => "SinsMod/NPCs/NormalNPCs/BigMetalSlime";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Metal Slime");
            Main.npcFrameCount[npc.type] = 4;
            animationType = 244;
        }
        public override void SetDefaults()
        {
            npc.width = 70;
            npc.height = 46;
            npc.lifeMax = 18;
            npc.damage = 50;
            npc.knockBackResist = 0.1f;
            npc.aiStyle = 1;
            npc.npcSlots = 0.5f;
            npc.HitSound = SoundID.NPCHit4;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.value = Item.sellPrice(0, 0, 4, 0);
            for (int i = 0; i < npc.buffImmune.Length; i++)
            {
                npc.buffImmune[i] = true;
            }
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = 27;
            npc.damage = 75;
            npc.value = Item.buyPrice(0, 0, 8, 0);
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
                    Dust.NewDust(npc.position, npc.width, npc.height, 11, -2f * hitDirection, -1.0f, 0, default(Color), 1f);
                    Dust.NewDust(npc.position, npc.width, npc.height, 11, 2f * hitDirection, -1.0f, 0, default(Color), 1f);
                }
                Dust.NewDust(npc.position, npc.width, npc.height, 11, -2f * hitDirection, -1.0f, 0, default(Color), 1f);
                Dust.NewDust(npc.position, npc.width, npc.height, 11, -2f * hitDirection, -1.0f, 0, default(Color), 1f);
                Dust.NewDust(npc.position, npc.width, npc.height, 11, 2f * hitDirection, -1.0f, 0, default(Color), 1f);
                Dust.NewDust(npc.position, npc.width, npc.height, 1, 2f * hitDirection, -1.0f, 0, default(Color), 1f);
            }
        }
        public override void AI()
        {
            if (npc.wet == true)
            {
                npc.velocity.Y = +60;
            }
        }
        public override bool PreNPCLoot()
        {
            return false;
        }
    }
}