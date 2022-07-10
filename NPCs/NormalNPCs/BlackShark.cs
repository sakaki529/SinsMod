using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.NPCs.NormalNPCs
{
    public class BlackShark : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Black Shark");
            Main.npcFrameCount[npc.type] = 4;
            animationType = 545;
        }
        public override void SetDefaults()
        {
            npc.width = 100;
            npc.height = 24;
            npc.lifeMax = 64500;
            npc.damage = 254;
            npc.defense = 100;
            npc.aiStyle = -1;
            //npc.aiStyle = 103;
            npc.knockBackResist = 0f;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.lavaImmune = true;
            npc.behindTiles = true;
            npc.netAlways = true;
            npc.HitSound = SoundID.DD2_WitherBeastCrystalImpact;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.value = Item.sellPrice(0, 2, 50, 0);
            npc.alpha = 255;
            for (int i = 0; i < npc.buffImmune.Length; i++)
            {
                npc.buffImmune[i] = true;
            }
            banner = npc.type;
            bannerItem = mod.ItemType("BlackSharkBanner");
            npc.GetGlobalNPC<SinsNPC>().drawCenter = true;
        }
        public override void HitEffect(int hitDirection, double damage)
        {
            int num = 0;
            if (npc.life > 0)
            {
                int num2 = 0;
                while (num2 < damage / npc.lifeMax * 150.0)
                {
                    Dust.NewDust(npc.position, npc.width, npc.height, 5, hitDirection, -1f, 0, default(Color), 1f);
                    num = num2;
                    num2 = num + 1;
                }
                return;
            }
            for (int num3 = 0; num3 < 75; num3 = num + 1)
            {
                Dust.NewDust(npc.position, npc.width, npc.height, 5, 2 * hitDirection, -2f, 0, default(Color), 1f);
                num = num3;
            }
            Gore.NewGore(npc.position, npc.velocity * 0.8f, mod.GetGoreSlot("Gores/NPC/BlackShark1"), npc.scale);
            Gore.NewGore(new Vector2(npc.position.X + 14f, npc.position.Y), npc.velocity * 0.8f, mod.GetGoreSlot("Gores/NPC/BlackShark2"), npc.scale);
            Gore.NewGore(new Vector2(npc.position.X + 14f, npc.position.Y), npc.velocity * 0.8f, mod.GetGoreSlot("Gores/NPC/BlackShark3"), npc.scale);
            Gore.NewGore(new Vector2(npc.position.X + 14f, npc.position.Y), npc.velocity * 0.8f, mod.GetGoreSlot("Gores/NPC/BlackShark4"), npc.scale);
        }
        public override void AI()
        {
            bool expertMode = Main.expertMode;
            float num = expertMode ? (0.6f * Main.damageMultiplier) : 1f;
            bool flag3 = npc.ai[0] > 4f;
            bool flag4 = npc.ai[0] > 9f;
            bool flag5 = npc.ai[3] < 10f;
            int num2 = expertMode ? 40 : 60;
            float num3 = expertMode ? 0.55f : 0.45f;
            float scaleFactor = expertMode ? 8.5f : 7.5f;
            if (flag4)
            {
                num3 = 0.7f;
                scaleFactor = 12f;
                num2 = 30;
            }
            else
            {
                if (flag3 & flag5)
                {
                    num3 = expertMode ? 0.6f : 0.5f;
                    scaleFactor = expertMode ? 10f : 8f;
                    num2 = expertMode ? 40 : 20;
                }
                else
                {
                    if (flag5 && !flag3 && !flag4)
                    {
                        num2 = 30;
                    }
                }
            }
            int num4 = expertMode ? 28 : 30;
            float num5 = expertMode ? 17f : 16f;
            if (flag4)
            {
                num4 = 25;
                num5 = 27f;
            }
            else
            {
                if (flag5 & flag3)
                {
                    num4 = expertMode ? 27 : 30;
                    if (expertMode)
                    {
                        num5 = 21f;
                    }
                }
            }
            int num9 = 90;
            int num16 = 75;
            Vector2 vector = npc.Center;
            Player player = Main.player[npc.target];
            if (npc.target < 0 || npc.target == 255 || player.dead || !player.active)
            {
                npc.TargetClosest(true);
                player = Main.player[npc.target];
                npc.netUpdate = true;
            }
            if (player.dead || Vector2.Distance(player.Center, vector) > 5600f)
            {
                npc.velocity.Y = npc.velocity.Y + 0.4f;
                if (npc.timeLeft > 10)
                {
                    npc.timeLeft = 10;
                }
                if (npc.ai[0] > 4f)
                {
                    npc.ai[0] = 5f;
                }
                else
                {
                    npc.ai[0] = 0f;
                }
                npc.ai[2] = 0f;
            }
            //if (player.position.Y < 800f || (double)player.position.Y > Main.worldSurface * 16.0 || (player.position.X > 6400f && player.position.X < (float)(Main.maxTilesX * 16 - 6400)))
            {
                //num2 = 20;
                npc.ai[3] = 0f;
                //num5 += 6f;
            }
            if (npc.localAI[0] == 0f)
            {
                npc.localAI[0] = 1f;
                npc.alpha = 255;
                npc.rotation = 0f;
                if (Main.netMode != 1)
                {
                    npc.ai[0] = -1f;
                    npc.netUpdate = true;
                }
            }
            float num17 = (float)Math.Atan2(player.Center.Y - vector.Y, player.Center.X - vector.X);
            if (npc.spriteDirection == -1)
            {
                num17 += 3.14159274f;
            }
            if (num17 < 0f)
            {
                num17 += 6.28318548f;
            }
            if (num17 > 6.28318548f)
            {
                num17 -= 6.28318548f;
            }
            if (npc.ai[0] == -1f)
            {
                num17 = 0f;
            }
            if (npc.ai[0] == 3f)
            {
                num17 = 0f;
            }
            if (npc.ai[0] == 4f)
            {
                num17 = 0f;
            }
            if (npc.ai[0] == 8f)
            {
                num17 = 0f;
            }
            float num18 = 0.04f;
            if (npc.ai[0] == 1f || npc.ai[0] == 6f)
            {
                num18 = 0f;
            }
            if (npc.ai[0] == 7f)
            {
                num18 = 0f;
            }
            if (npc.ai[0] == 3f)
            {
                num18 = 0.01f;
            }
            if (npc.ai[0] == 4f)
            {
                num18 = 0.01f;
            }
            if (npc.ai[0] == 8f)
            {
                num18 = 0.01f;
            }
            if (npc.rotation < num17)
            {
                if ((num17 - npc.rotation) > 3.1415926535897931)
                {
                    npc.rotation -= num18;
                }
                else
                {
                    npc.rotation += num18;
                }
            }
            if (npc.rotation > num17)
            {
                if ((npc.rotation - num17) > 3.1415926535897931)
                {
                    npc.rotation += num18;
                }
                else
                {
                    npc.rotation -= num18;
                }
            }
            if (npc.rotation > num17 - num18 && npc.rotation < num17 + num18)
            {
                npc.rotation = num17;
            }
            if (npc.rotation < 0f)
            {
                npc.rotation += 6.28318548f;
            }
            if (npc.rotation > 6.28318548f)
            {
                npc.rotation -= 6.28318548f;
            }
            if (npc.rotation > num17 - num18 && npc.rotation < num17 + num18)
            {
                npc.rotation = num17;
            }
            if (npc.ai[0] != -1f && npc.ai[0] < 9f)
            {
                if (Collision.SolidCollision(npc.position, npc.width, npc.height))
                {
                    npc.alpha += 15;
                }
                else
                {
                    npc.alpha -= 15;
                }
                if (npc.alpha < 0)
                {
                    npc.alpha = 0;
                }
                if (npc.alpha > 150)
                {
                    npc.alpha = 150;
                }
            }
            if (npc.ai[0] == -1f)
            {
                npc.velocity *= 0.98f;
                int num19 = Math.Sign(player.Center.X - vector.X);
                if (num19 != 0)
                {
                    npc.direction = num19;
                    npc.spriteDirection = npc.direction;
                }
                if (npc.ai[2] > 20f)
                {
                    npc.velocity.Y = -2f;
                    npc.alpha -= 5;
                    if (Collision.SolidCollision(npc.position, npc.width, npc.height))
                    {
                        npc.alpha += 15;
                    }
                    if (npc.alpha < 0)
                    {
                        npc.alpha = 0;
                    }
                    if (npc.alpha > 150)
                    {
                        npc.alpha = 150;
                    }
                }
                if (npc.ai[2] == num9 - 30)
                {
                    int num20 = 36;
                    for (int i = 0; i < num20; i++)
                    {
                        Vector2 expr_80F = (Vector2.Normalize(npc.velocity) * new Vector2(npc.width / 2f, npc.height) * 0.75f * 0.5f).RotatedBy((i - (num20 / 2 - 1)) * 6.28318548f / num20, default(Vector2)) + npc.Center;
                        Vector2 vector2 = expr_80F - npc.Center;
                        int num21 = Dust.NewDust(expr_80F + vector2, 0, 0, 175, vector2.X * 2f, vector2.Y * 2f, 100, default(Color), 1.4f);
                        Main.dust[num21].noGravity = true;
                        Main.dust[num21].noLight = true;
                        Main.dust[num21].velocity = Vector2.Normalize(vector2) * 3f;
                    }
                    //Main.PlaySound(29, (int)vector.X, (int)vector.Y, 20, 1f, 0f);
                }
                npc.ai[2] += 1f;
                if (npc.ai[2] >= num16)
                {
                    npc.ai[0] = 0f;
                    npc.ai[1] = 0f;
                    npc.ai[2] = 0f;
                    npc.netUpdate = true;
                    return;
                }
            }
            else
            {
                if (npc.ai[0] == 0f && !player.dead)
                {
                    if (npc.ai[1] == 0f)
                    {
                        npc.ai[1] = 300 * Math.Sign((vector - player.Center).X);
                    }
                    Vector2 vector3 = Vector2.Normalize(player.Center + new Vector2(npc.ai[1], -200f) - vector - npc.velocity) * scaleFactor;
                    if (npc.velocity.X < vector3.X)
                    {
                        npc.velocity.X = npc.velocity.X + num3;
                        if (npc.velocity.X < 0f && vector3.X > 0f)
                        {
                            npc.velocity.X = npc.velocity.X + num3;
                        }
                    }
                    else
                    {
                        if (npc.velocity.X > vector3.X)
                        {
                            npc.velocity.X = npc.velocity.X - num3;
                            if (npc.velocity.X > 0f && vector3.X < 0f)
                            {
                                npc.velocity.X = npc.velocity.X - num3;
                            }
                        }
                    }
                    if (npc.velocity.Y < vector3.Y)
                    {
                        npc.velocity.Y = npc.velocity.Y + num3;
                        if (npc.velocity.Y < 0f && vector3.Y > 0f)
                        {
                            npc.velocity.Y = npc.velocity.Y + num3;
                        }
                    }
                    else
                    {
                        if (npc.velocity.Y > vector3.Y)
                        {
                            npc.velocity.Y = npc.velocity.Y - num3;
                            if (npc.velocity.Y > 0f && vector3.Y < 0f)
                            {
                                npc.velocity.Y = npc.velocity.Y - num3;
                            }
                        }
                    }
                    int num22 = Math.Sign(player.Center.X - vector.X);
                    if (num22 != 0)
                    {
                        if (npc.ai[2] == 0f && num22 != npc.direction)
                        {
                            npc.rotation += 3.14159274f;
                        }
                        npc.direction = num22;
                        if (npc.spriteDirection != npc.direction)
                        {
                            npc.rotation += 3.14159274f;
                        }
                        npc.spriteDirection = npc.direction;
                    }
                    npc.ai[2] += 1f;
                    if (npc.ai[2] >= num2)
                    {
                        npc.ai[0] = 1f;
                        npc.ai[1] = 0f;
                        npc.ai[2] = 0f;
                        npc.velocity = Vector2.Normalize(player.Center - vector) * num5;
                        npc.rotation = (float)Math.Atan2(npc.velocity.Y, npc.velocity.X);
                        if (num22 != 0)
                        {
                            npc.direction = num22;
                            if (npc.spriteDirection == -1)
                            {
                                npc.rotation += 3.14159274f;
                            }
                            npc.spriteDirection = npc.direction;
                        }
                        npc.netUpdate = true;
                        return;
                    }
                }
                else
                {
                    if (npc.ai[0] == 1f)
                    {
                        int num24 = 7;
                        for (int j = 0; j < num24; j++)
                        {
                            Vector2 arg_E1C_0 = (Vector2.Normalize(npc.velocity) * new Vector2((npc.width + 50) / 2f, npc.height) * 0.75f).RotatedBy((j - (num24 / 2 - 1)) * 3.1415926535897931 / (float)num24, default(Vector2)) + vector;
                            Vector2 vector4 = ((float)(Main.rand.NextDouble() * 3.1415927410125732) - 1.57079637f).ToRotationVector2() * Main.rand.Next(3, 8);
                            int num25 = Dust.NewDust(arg_E1C_0 + vector4, 0, 0, 175, vector4.X * 2f, vector4.Y * 2f, 100, default(Color), 1.4f);
                            Main.dust[num25].noGravity = true;
                            Main.dust[num25].noLight = true;
                            Main.dust[num25].velocity /= 4f;
                            Main.dust[num25].velocity -= npc.velocity;
                        }
                        npc.ai[2] += 1f;
                        if (npc.ai[2] >= num4)
                        {
                            npc.ai[0] = 0f;
                            npc.ai[1] = 0f;
                            npc.ai[2] = 0f;
                            npc.netUpdate = true;
                            return;
                        }
                    }
                }
            }
        }
        public override void NPCLoot()
        {
            int num = Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.SharkFin, Main.rand.Next(5, 13));
            Main.item[num].color = new Color(20, 20, 20, 255);
            NetMessage.SendData(88, -1, -1, null, num, 1f, 0f, 0f, 0, 0, 0);
        }
    }
}