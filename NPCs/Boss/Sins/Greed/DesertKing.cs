using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.NPCs.Boss.Sins.Greed
{
    [AutoloadBossHead]
    public class DesertKing : ModNPC
    {
        private bool Esc;
        private bool spawnedHands;
        private int[] Count = new int[2];
        public override string BossHeadTexture => "SinsMod/NPCs/Boss/Sins/Greed/Greed_Head_Boss";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ominous, the Ancient Desert King");
        }
        public override void SetDefaults()
        {
            npc.width = 34;
            npc.height = 92;
            npc.lifeMax = 2600;
            npc.damage = 25;
            npc.defense = 12;
            npc.knockBackResist = 0f;
            npc.aiStyle = -1;
            npc.boss = true;
            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.netAlways = true;
            npc.HitSound = SoundID.NPCHit2;
            npc.DeathSound = SoundID.NPCDeath44;
            npc.npcSlots = 5f;
            music = MusicID.Sandstorm;
            npc.scale = 1.2f;
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = (int)(npc.lifeMax * 0.8f * bossLifeScale);
            npc.damage = 50;
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            Texture2D texture = Main.npcTexture[npc.type];
            Texture2D glowMask = mod.GetTexture("Glow/NPC/DesertKing_Glow");
            SpriteEffects effects = (npc.spriteDirection == 1) ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            spriteBatch.Draw(texture, npc.Center - Main.screenPosition, new Rectangle?(npc.frame), npc.GetAlpha(drawColor), npc.rotation, npc.frame.Size() / 2f, npc.scale, effects, 0f);
            spriteBatch.Draw(glowMask, npc.Center - Main.screenPosition, new Rectangle?(npc.frame), npc.GetAlpha(Color.Lerp(drawColor, Color.White, (float)Math.Cos(6.28318548f * (Main.LocalPlayer.miscCounter / 100f)) * 0.5f + 0.75f/*0.75 = white*/)), npc.rotation, npc.frame.Size() / 2f, npc.scale, effects, 0f);
            return false;
        }
        public override void HitEffect(int hitDirection, double damage)
        {
            if (npc.life <= 0)
            {
                float num = Main.rand.Next(5, 101) / 100;
                Gore.NewGore(npc.position, new Vector2(-16f * num * Main.rand.NextFloat(0.8f, 1.0f), 0), mod.GetGoreSlot("Gores/Boss/DesertKing1"), npc.scale);
                Gore.NewGore(npc.position, new Vector2(16f * num * Main.rand.NextFloat(0.8f, 1.0f), 0), mod.GetGoreSlot("Gores/Boss/DesertKing2"), npc.scale);
            }
        }
        public override void AI()
        {
            bool LC = SinsWorld.LimitCut;
            if (!spawnedHands && SinsWorld.LimitCut)
            {
                int nPC = NPC.NewNPC((int)(npc.position.X + (npc.width / 2)), (int)npc.position.Y + npc.height / 2, mod.NPCType("DesertKingHand"), npc.whoAmI, 0f, 0f, 0f, 0f, 255);
                Main.npc[nPC].ai[0] = -1f;
                Main.npc[nPC].ai[1] = npc.whoAmI;
                Main.npc[nPC].target = npc.target;
                Main.npc[nPC].netUpdate = true;
                nPC = NPC.NewNPC((int)(npc.position.X + (npc.width / 2)), (int)npc.position.Y + npc.height / 2, mod.NPCType("DesertKingHand"), npc.whoAmI, 0f, 0f, 0f, 0f, 255);
                Main.npc[nPC].ai[0] = 1f;
                Main.npc[nPC].ai[1] = npc.whoAmI;
                Main.npc[nPC].ai[3] = 150f;
                Main.npc[nPC].target = npc.target;
                Main.npc[nPC].netUpdate = true;
                spawnedHands = true;
            }
            Player player = Main.player[npc.target];
            npc.GetGlobalNPC<SinsNPC>().defenceMode = NPC.AnyNPCs(mod.NPCType("DesertKingHand"));
            if (npc.GetGlobalNPC<SinsNPC>().defenceMode)
            {
                for (int i = 0; i < npc.buffImmune.Length; i++)
                {
                    npc.buffImmune[i] = true;
                }
            }
            //npc.GetGlobalNPC<SinsNPC>().damageMult = NPC.AnyNPCs(mod.NPCType("DesertKingHand")) ? 1E-09f : 1f;
            npc.spriteDirection = 0;
            if (npc.ai[1] != 1)
            {
                npc.rotation = npc.velocity.X * 0.05f;
            }
            else
            {
                ChargeAI(player);
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
                npc.alpha += 2;
                if (npc.alpha >= 255)
                {
                    npc.active = false;
                }
            }
            if (npc.ai[1] == 1)
            {
                return;
            }
            npc.ai[2]++;
            int random = (int)MathHelper.Clamp(1000 - 500 * (1.0f - npc.life / npc.lifeMax), 500, 1000);
            if ((npc.ai[2] > 480 && (Main.expertMode || npc.life < npc.lifeMax * 0.6f) && Main.rand.Next(random) == 0) || npc.ai[2] > 1200)
            {
                npc.ai[1] = 1;
                npc.ai[2] = 0;
            }
            float num = 0.09f;
            float num2 = 12f;
            float num3 = 0.12f;
            float num4 = 15f;
            if (LC)
            {
                num = 0.15f;
                num2 = 18f;
                num3 = 0.18f;
                num4 = 24f;
            }
            if (npc.Center.Y > player.Center.Y - 140f)
            {
                if (npc.velocity.Y > 0f)
                {
                    npc.velocity.Y = npc.velocity.Y * 0.98f;
                }
                npc.velocity.Y = npc.velocity.Y - num;
                if (npc.velocity.Y > num2)
                {
                    npc.velocity.Y = num2;
                }
            }
            else if (npc.Center.Y < player.Center.Y - 240)
            {
                if (npc.velocity.Y < 0f)
                {
                    npc.velocity.Y = npc.velocity.Y * 0.98f;
                }
                npc.velocity.Y = npc.velocity.Y + num;
                if (npc.velocity.Y < -num2)
                {
                    npc.velocity.Y = -num2;
                }
            }
            if (npc.position.X + (npc.width / 2) > player.position.X + (player.width / 2) + 100)
            {
                if (npc.velocity.X > 0f)
                {
                    npc.velocity.X = npc.velocity.X * 0.98f;
                }
                npc.velocity.X = npc.velocity.X - num3;
                if (npc.velocity.X > num4)
                {
                    npc.velocity.X = num4;
                }
            }
            if (npc.position.X + (npc.width / 2) < player.position.X + (player.width / 2) - 100)
            {
                if (npc.velocity.X < 0f)
                {
                    npc.velocity.X = npc.velocity.X * 0.98f;
                }
                npc.velocity.X = npc.velocity.X + num3;
                if (npc.velocity.X < -num4)
                {
                    npc.velocity.X = -num4;
                }
            }
            bool cantHit = !Collision.CanHit(npc.position, npc.width / 2, -36, Main.player[npc.target].position, Main.player[npc.target].width, Main.player[npc.target].height) && npc.life < (int)(npc.lifeMax * 0.3f) && Main.expertMode;
            if (!npc.GetGlobalNPC<SinsNPC>().defenceMode)
            {
                npc.localAI[0]++;
                if (npc.localAI[0] >= (cantHit || npc.life < (int)(npc.lifeMax * 0.85f) ? 20 : 40) && Main.rand.Next(cantHit ? 20 : 120) == 0)
                {
                    Vector2 vector = player.Center - npc.Center;
                    vector.Normalize();
                    vector *= Main.rand.NextFloat(4.5f, 7f);
                    int num5 = Projectile.NewProjectile(npc.Center.X + Main.rand.Next(-64, 64), npc.Center.Y - Main.rand.Next(82, 102), vector.X, vector.Y, ProjectileID.TopazBolt, 10, 0f, Main.myPlayer, 0, 0f);
                    Main.projectile[num5].friendly = false;
                    Main.projectile[num5].hostile = true;
                    Main.projectile[num5].tileCollide = false;
                    Main.projectile[num5].timeLeft = 360;
                    npc.localAI[0] = 0;
                }
            }
            if (npc.life < (int)(npc.lifeMax * 0.7f))
            {
                npc.localAI[1]++;
                if (npc.localAI[1] >= (cantHit || npc.life < (int)(npc.lifeMax * 0.55f) ? 20 : 40) && Main.rand.Next(cantHit ? 20 : 120) == 0)
                {
                    Vector2 vector = player.Center - npc.Center;
                    vector.Normalize();
                    vector *= Main.rand.NextFloat(5f, 8f);
                    int num6 = Projectile.NewProjectile(npc.Center.X + Main.rand.Next(-64, 64), npc.Center.Y - Main.rand.Next(82, 102), vector.X, vector.Y, ProjectileID.AmethystBolt, 15, 0f, Main.myPlayer, 0, 0f);
                    Main.projectile[num6].friendly = false;
                    Main.projectile[num6].hostile = true;
                    Main.projectile[num6].tileCollide = false;
                    Main.projectile[num6].timeLeft = 360;
                    npc.localAI[1] = 0;
                }
            }
            if (npc.life < (int)(npc.lifeMax * 0.4f) && Main.expertMode)
            {
                npc.localAI[2]--;
                bool flag = Main.rand.Next(240) == 0;
                if (flag && npc.localAI[2] <= 0)
                {
                    int num7 = 0;
                    int num8 = 0;
                    num8++;
                    npc.velocity.X *= 0.92f;
                    npc.velocity.Y *= 0.92f;
                    if (Main.netMode != 1 && SinsUtility.CountProjectiles(ProjectileID.SandnadoHostileMark) < (npc.life < (int)(npc.lifeMax * 0.1f) ? 3 : 2))
                    {
                        List<Point> list = new List<Point>();
                        Vector2 vector = Main.player[npc.target].Center + new Vector2(Main.player[npc.target].velocity.X * 30f, 0f);
                        Point point = vector.ToTileCoordinates();

                        while (num7 < 1000 && list.Count < 1)
                        {
                            bool flag2 = false;
                            int num9 = Main.rand.Next(point.X - 30, point.X + 30 + 1);
                            foreach (Point current in list)
                            {
                                if (Math.Abs(current.X - num9) < 10)
                                {
                                    flag2 = true;
                                    break;
                                }
                            }
                            if (!flag2)
                            {
                                int startY = point.Y - 20;
                                Collision.ExpandVertically(num9, startY, out int num10, out int num11, 1, 51);
                                if (StrayMethods.CanSpawnSandstormHostile(new Vector2(num9, num11 - 15) * 16f, 15, 15))
                                {
                                    list.Add(new Point(num9, num11 - 15));
                                }
                            }
                            num7++;
                        }
                        foreach (Point current in list)
                        {
                            Projectile.NewProjectile(current.X * 16, current.Y * 16, 0f, 0f, ProjectileID.SandnadoHostileMark, 12, 0f, Main.myPlayer, 0f, 0f);
                        }
                    }
                    if (num8 > 90)
                    {
                        num7 = 0;
                        num8 = 0;
                        flag = false;
                        npc.localAI[2] = Main.rand.Next(120, 300);
                    }
                }
            }
        }
        private void ChargeAI(Player player)
        {
            bool LC = SinsWorld.LimitCut;
            if (Count[0] == 0f)
            {
                npc.velocity *= 0.99f;
                Count[1] += 1;
                float num5 = Count[1] / 100f;
                num5 = 0.1f + num5 * 0.4f;
                npc.rotation += num5 * npc.direction;
                int min = Main.expertMode ? 80 : 90;
                int max = Main.expertMode ? 100 : 120;
                if (LC)
                {
                    min = 70;
                    max = 90;
                }
                if (Count[1] >= Main.rand.Next(min, max + 1))
                {
                    npc.netUpdate = true;
                    Count[0] = 1;
                    Count[1] = 0;
                }
                return;
            }
            if (Count[0] == 1)
            {
                float num = Main.expertMode ? 20f : 16f;
                if (LC)
                {
                    num = 24f;
                }
                Vector2 vector = new Vector2(npc.position.X + npc.width * 0.5f, npc.position.Y + npc.height * 0.5f);
                float num2 = Main.player[npc.target].position.X + (Main.player[npc.target].width / 2) - vector.X;
                float num3 = Main.player[npc.target].position.Y + (Main.player[npc.target].height / 2) - vector.Y;
                float num4 = (float)Math.Sqrt(num2 * num2 + num3 * num3);
                num4 = num / num4;
                num2 *= num4;
                num3 *= num4;
                npc.velocity.X = num2;
                npc.velocity.Y = num3;
                npc.rotation = (float)Math.Atan2(npc.velocity.Y, npc.velocity.X) + 1.57f;
                Count[0] = 2;
                Count[1] = 0;
                npc.netUpdate = true;
                Main.PlaySound(SoundID.Roar, (int)npc.Center.X, (int)npc.Center.Y, 0, 1f, 0f);
                return;
            }
            else
            {
                npc.velocity *= 0.99f;
                Count[1] += 1;
                if (Count[1] > 60)
                {
                    npc.ai[1] = 0;
                    npc.netUpdate = true;
                    Count[0] = 0;
                    Count[1] = 0;
                    npc.velocity.X = 0f;
                    npc.velocity.Y = 0f;
                    return;
                }
            }
        }
        public override bool CheckDead()
        {
            NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y + 48, mod.NPCType("Greed"), npc.whoAmI);
            return true;
        }
        /*public override bool CheckActive()
        {
            return false;
        }*/
        public override bool PreNPCLoot()
        {
            return false;
        }
    }
}