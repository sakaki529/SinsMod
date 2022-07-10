using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.NPCs.Boss.KingMetalSlime
{
    public class SpikedMetalSlime : ModNPC
    {
        public float spikeTimer = 60f;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Spiked Metal Slime");
            Main.npcFrameCount[npc.type] = 2;
            animationType = NPCID.SlimeSpiked;
        }
        public override void SetDefaults()
        {
            npc.width = 40;
            npc.height = 30;
            npc.lifeMax = 10;
            npc.damage = 30;
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
            npc.damage = 45;
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
            if (spikeTimer > 0f)
            {
                spikeTimer -= 1f;
            }
            Vector2 vector = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
            float num = Main.player[npc.target].position.X + (float)Main.player[npc.target].width * 0.5f - vector.X;
            float num2 = Main.player[npc.target].position.Y - vector.Y;
            float num3 = (float)Math.Sqrt((double)(num * num + num2 * num2));
            if (Main.expertMode && num3 < 120f && Collision.CanHit(npc.position, npc.width, npc.height, Main.player[npc.target].position, Main.player[npc.target].width, Main.player[npc.target].height) && npc.velocity.Y == 0f)
            {
                npc.ai[0] = -40f;
                if (npc.velocity.Y == 0f)
                {
                    npc.velocity.X = npc.velocity.X * 0.9f;
                }
                if (Main.netMode != 1 && spikeTimer == 0f)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        Vector2 vector2 = new Vector2((float)(i - 2), -4f);
                        vector2.X *= 1f + (float)Main.rand.Next(-50, 51) * 0.005f;
                        vector2.Y *= 1f + (float)Main.rand.Next(-50, 51) * 0.005f;
                        vector2.Normalize();
                        vector2 *= 4f + (float)Main.rand.Next(-50, 51) * 0.01f;
                        Projectile.NewProjectile(vector.X, vector.Y, vector2.X, vector2.Y, mod.ProjectileType("MetalSpike"), 26, 0f, Main.myPlayer, 0f, 0f);
                        spikeTimer = 30f;
                    }
                    return;
                }
            }
            else
            {
                if (num3 < 360f && Collision.CanHit(npc.position, npc.width, npc.height, Main.player[npc.target].position, Main.player[npc.target].width, Main.player[npc.target].height) && npc.velocity.Y == 0f)
                {
                    npc.ai[0] = -40f;
                    if (npc.velocity.Y == 0f)
                    {
                        npc.velocity.X = npc.velocity.X * 0.9f;
                    }
                    if (Main.netMode != 1 && spikeTimer == 0f)
                    {
                        num2 = Main.player[npc.target].position.Y - vector.Y - (float)Main.rand.Next(0, 200);
                        num3 = (float)Math.Sqrt((double)(num * num + num2 * num2));
                        num3 = 6.5f / num3;
                        num *= num3;
                        num2 *= num3;
                        spikeTimer = 50f;
                        Projectile.NewProjectile(vector.X, vector.Y, num, num2, mod.ProjectileType("MetalSpike"), 26, 0f, Main.myPlayer, 0f, 0f);
                    }
                }
            }
        }
        public override bool PreNPCLoot()
        {
            return false;
        }
    }
}