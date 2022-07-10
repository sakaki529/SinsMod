using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.NPCs.NormalNPCs
{
    public class NightmareProbe : ModNPC
    {
        private bool Esc;
        private int EscTimer;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Nightmare Probe");
        }
        public override void SetDefaults()
        {
            npc.width = 56;
            npc.height = 56;
            npc.lifeMax = 10000;
            npc.damage = 300;
            npc.knockBackResist = 0f;
            npc.aiStyle = -1;
            npc.npcSlots = 1f;
            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.HitSound = SoundID.NPCHit4;
            npc.DeathSound = SoundID.NPCDeath14;
            npc.value = Item.sellPrice(0, 2, 50, 0);
            for (int i = 0; i < npc.buffImmune.Length; i++)
            {
                npc.buffImmune[i] = true;
            }
            /*banner = mod.NPCType("NightmareProbe");
            bannerItem = mod.ItemType("NightmareProbeBanner");*/
            npc.GetGlobalNPC<SinsNPC>().drawCenter = true;
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = 16000;
        }
        public override bool StrikeNPC(ref double damage, int defense, ref float knockback, int hitDirection, ref bool crit)
        {
            damage /= 2;
            return true;
        }
        public override void HitEffect(int hitDirection, double damage)
        {
            if (npc.life <= 0)
            {
                int num = Gore.NewGore(new Vector2(npc.position.X, npc.position.Y), default(Vector2), Main.rand.Next(61, 64), npc.scale);
                Main.gore[num].velocity *= 0.4f;
                Gore gore = Main.gore[num];
                gore.velocity.X = gore.velocity.X + 1f;
                Gore gore2 = Main.gore[num];
                gore2.velocity.Y = gore2.velocity.Y + 1f;
                num = Gore.NewGore(new Vector2(npc.position.X, npc.position.Y), default(Vector2), Main.rand.Next(61, 64), npc.scale);
                Main.gore[num].velocity *= 0.4f;
                Gore gore3 = Main.gore[num];
                gore3.velocity.X = gore3.velocity.X - 1f;
                Gore gore4 = Main.gore[num];
                gore4.velocity.Y = gore4.velocity.Y + 1f;
                num = Gore.NewGore(new Vector2(npc.position.X, npc.position.Y), default(Vector2), Main.rand.Next(61, 64), npc.scale);
                Main.gore[num].velocity *= 0.4f;
                Gore gore5 = Main.gore[num];
                gore5.velocity.X = gore5.velocity.X + 1f;
                Gore gore6 = Main.gore[num];
                gore6.velocity.Y = gore6.velocity.Y - 1f;
                num = Gore.NewGore(new Vector2(npc.position.X, npc.position.Y), default(Vector2), Main.rand.Next(61, 64), npc.scale);
                Main.gore[num].velocity *= 0.4f;
                Gore gore7 = Main.gore[num];
                gore7.velocity.X = gore7.velocity.X - 1f;
                Gore gore8 = Main.gore[num];
                gore8.velocity.Y = gore8.velocity.Y - 1f;
            }
        }
        public override void AI()
        {
            Player player = Main.player[npc.target];
            Vector2 vector2 = new Vector2(npc.Center.X, npc.Center.Y);
            npc.rotation = (float)Math.Atan2(player.Center.Y - vector2.Y, player.Center.X - vector2.X) - 1.57f;
            float num = 6f * 3f;
            float num2 = 0.05f * 3f;
            Vector2 vector = new Vector2(npc.position.X + npc.width * 0.5f, npc.position.Y + npc.height * 0.5f);
            float num3 = player.position.X + (player.width / 2);
            float num4 = player.position.Y + (player.height / 2);
            num3 = (int)(num3 / 8f) * 8;
            num4 = (int)(num4 / 8f) * 8;
            vector.X = (int)(vector.X / 8f) * 8;
            vector.Y = (int)(vector.Y / 8f) * 8;
            num3 -= vector.X;
            num4 -= vector.Y;
            float num5 = (float)Math.Sqrt(num3 * num3 + num4 * num4);
            float num6 = num5;
            bool flag = false;
            if (num5 > 600f)
            {
                flag = true;
            }
            if (num5 == 0f)
            {
                num3 = npc.velocity.X;
                num4 = npc.velocity.Y;
            }
            else
            {
                num5 = num / num5;
                num3 *= num5;
                num4 *= num5;
            }
            if (num6 > 100f)
            {
                npc.ai[1] += 1f;
                if (npc.ai[1] > 0f)
                {
                    npc.velocity.Y = npc.velocity.Y + 0.036f;
                }
                else
                {
                    npc.velocity.Y = npc.velocity.Y - 0.036f;
                }
                if (npc.ai[1] < -100f || npc.ai[1] > 100f)
                {
                    npc.velocity.X = npc.velocity.X + 0.036f;
                }
                else
                {
                    npc.velocity.X = npc.velocity.X - 0.036f;
                }
                if (npc.ai[1] > 200f)
                {
                    npc.ai[1] = -200f;
                }
            }
            if (player.dead)
            {
                num3 = npc.direction * num / 2f;
                num4 = -num / 2f;
            }
            if (npc.velocity.X < num3)
            {
                npc.velocity.X = npc.velocity.X + num2;
            }
            else
            {
                if (npc.velocity.X > num3)
                {
                    npc.velocity.X = npc.velocity.X - num2;
                }
            }
            if (npc.velocity.Y < num4)
            {
                npc.velocity.Y = npc.velocity.Y + num2;
            }
            else
            {
                if (npc.velocity.Y > num4)
                {
                    npc.velocity.Y = npc.velocity.Y - num2;
                }
            }
            npc.localAI[0] += 1f;
            if (npc.justHit)
            {
                npc.localAI[0] = 0f;
            }
            if (Main.netMode != 1 && npc.localAI[0] >= 200f)
            {
                npc.localAI[0] = 0f;
                if (Collision.CanHit(npc.position, npc.width, npc.height, player.position, player.width, player.height) || !Collision.CanHit(npc.position, npc.width, npc.height, player.position, player.width, player.height))
                {
                    int damage = 120;
                    if (Main.expertMode)
                    {
                        damage = 150;
                    }
                    int proj= Projectile.NewProjectile(vector.X, vector.Y, num3, num4, mod.ProjectileType("NightmareLaser"), damage, 0f, Main.myPlayer, 0f, 0f);
                    Main.projectile[proj].hostile = true;
                    Main.projectile[proj].friendly = false;
                    Main.projectile[proj].ranged = false;
                }
            }
            int num7 = (int)npc.position.X + npc.width / 2;
            int num8 = (int)npc.position.Y + npc.height / 2;
            num7 /= 16;
            num8 /= 16;
            if (!WorldGen.SolidTile(num7, num8))
            {
                Lighting.AddLight((int)((npc.position.X + (npc.width / 2)) / 16f), (int)((npc.position.Y + (npc.height / 2)) / 16f), 0.2f, 0.2f, 0.2f);
            }
            if (num3 > 0f)
            {
                npc.spriteDirection = 1;
            }
            if (num3 < 0f)
            {
                npc.spriteDirection = -1;
            }
            float num9 = 0.7f;
            if (npc.collideX)
            {
                npc.netUpdate = true;
                npc.velocity.X = npc.oldVelocity.X * -num9;
                if (npc.direction == -1 && npc.velocity.X > 0f && npc.velocity.X < 4f)
                {
                    npc.velocity.X = 4f;
                }
                if (npc.direction == 1 && npc.velocity.X < 0f && npc.velocity.X > -4f)
                {
                    npc.velocity.X = -4f;
                }
            }
            if (npc.collideY)
            {
                npc.netUpdate = true;
                npc.velocity.Y = npc.oldVelocity.Y * -num9;
                if (npc.velocity.Y > 0f && npc.velocity.Y < 3.5)
                {
                    npc.velocity.Y = 4f;
                }
                if (npc.velocity.Y < 0f && npc.velocity.Y > -3.5)
                {
                    npc.velocity.Y = -4f;
                }
            }
            if (flag)
            {
                if ((npc.velocity.X > 0f && num3 > 0f) || (npc.velocity.X < 0f && num3 < 0f))
                {
                    if (Math.Abs(npc.velocity.X) < 12f)
                    {
                        npc.velocity.X = npc.velocity.X * 1.1f;
                    }
                }
                else
                {
                    npc.velocity.X = npc.velocity.X * 0.9f;
                }
            }
            if (((npc.velocity.X > 0f && npc.oldVelocity.X < 0f) || (npc.velocity.X < 0f && npc.oldVelocity.X > 0f) || (npc.velocity.Y > 0f && npc.oldVelocity.Y < 0f) || (npc.velocity.Y < 0f && npc.oldVelocity.Y > 0f)) && !npc.justHit)
            {
                npc.netUpdate = true;
            }
            if (npc.target < 0 || npc.target == 255 || player.dead || !player.active)
            {
                npc.TargetClosest(true);
            }
            npc.netUpdate = true;
            if (player.dead || !player.active)
            {
                npc.TargetClosest(false);
                player = Main.player[npc.target];
                if (player.dead || !player.active)
                {
                    Esc = true;
                }
            }
            if (Esc)
            {
                npc.velocity.Y--;
                EscTimer++;
                if (EscTimer > 60 || npc.Center.Y < Main.maxTilesY - 1200)
                {
                    npc.active = false;
                }
                return;
            }
        }
        public override void NPCLoot()
        {
            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("NightmareOre"), Main.rand.Next(0, 3));
        }
        /*public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (spawnInfo.playerSafe || !Main.hardMode || !Main.dayTime || Main.eclipse)
            {
                return 0f;
            }
            return SpawnCondition.OverworldDaySlime.Chance * 0.01f;
        }*/
    }
}