using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.NPCs.Boss.Sins.Lust
{
    [AutoloadBossHead]
    public class AsmodeusSerpentHead : ModNPC
    {
        private bool Esc;
        private int EscTimer;
        private bool TailSpawned;
        private bool fly;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Servant of Asmodeus");
        }
        public override void SetDefaults()
        {
            npc.width = 24;
            npc.height = 24;
            npc.aiStyle = 6;
            aiType = -1;
            if (Asmodeus.summonedSerpOut > 1)
            {
                npc.lifeMax = 500000;
                npc.damage = 150;
            }
            else
            {
                npc.lifeMax = 300000;
                npc.damage = 90;
            }
            npc.defense = 40;
            npc.knockBackResist = 0f;
            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.behindTiles = true;
            npc.netAlways = true;
            npc.chaseable = false;
            npc.HitSound = SoundID.NPCHit7;
            npc.DeathSound = SoundID.NPCDeath18;
            npc.npcSlots = 1f;
            for (int i = 0; i < npc.buffImmune.Length; i++)
            {
                npc.buffImmune[i] = true;
                npc.buffImmune[BuffID.Ichor] = false;
                npc.buffImmune[mod.BuffType("Chroma")] = false;
            }
            npc.GetGlobalNPC<SinsNPC>().drawCenter = true;
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = (int)(npc.lifeMax * 0.8f * bossLifeScale);
            if (Asmodeus.summonedSerpOut > 1)
            {
                npc.damage = 180;
            }
            else
            {
                npc.damage = 120;
            }
        }
        public override void AI()
        {
            bool LC = SinsWorld.LimitCut;
            npc.boss = true;
            npc.rotation = (float)Math.Atan2(npc.velocity.Y, npc.velocity.X) + 1.57f;
            Player player = Main.player[npc.target];
            if (npc.target < 0 || npc.target == 255 || player.dead || !player.active)
            {
                npc.TargetClosest(true);
            }
            npc.netUpdate = true;
            if (player.dead || !player.active || Main.dayTime)
            {
                npc.TargetClosest(false);
                player = Main.player[npc.target];
                if (player.dead || !player.active || Main.dayTime)
                {
                    Esc = true;
                }
            }
            if (Esc)
            {
                npc.velocity.Y++;
                EscTimer++;
                if (EscTimer > 120)
                {
                    npc.active = false;
                }
            }
            if (npc.ai[3] > 0f)
            {
                npc.realLife = (int)npc.ai[3];
            }
            npc.velocity.Length();
            if (Main.netMode != 1)
            {
                if (!TailSpawned && npc.ai[0] == 0f)
                {
                    int num = npc.whoAmI;
                    for (int i = 0; i < 19; i++)
                    {
                        int num2;
                        if (i < 18)
                        {
                            num2 = NPC.NewNPC((int)npc.position.X + npc.width / 2, (int)npc.position.Y + npc.height / 2, mod.NPCType("AsmodeusSerpentBody"), npc.whoAmI, 0f, 0f, 0f, 0f, 255);
                        }
                        else
                        {
                            num2 = NPC.NewNPC((int)npc.position.X + npc.width / 2, (int)npc.position.Y + npc.height / 2, mod.NPCType("AsmodeusSerpentTail"), npc.whoAmI, 0f, 0f, 0f, 0f, 255);
                        }
                        Main.npc[num2].realLife = npc.whoAmI;
                        Main.npc[num2].ai[2] = npc.whoAmI;
                        Main.npc[num2].ai[1] = num;
                        Main.npc[num].ai[0] = num2;
                        num = num2;
                    }
                    TailSpawned = true;
                }
                if (!npc.active && Main.netMode == 2)
                {
                    NetMessage.SendData(28, -1, -1, null, npc.whoAmI, -1f, 0f, 0f, 0, 0, 0);
                }
            }
            int num3 = (int)(npc.position.X / 16f) - 1;
            int num4 = (int)((npc.position.X + npc.width) / 16f) + 2;
            int num5 = (int)(npc.position.Y / 16f) - 1;
            int num6 = (int)((npc.position.Y + npc.height) / 16f) + 2;
            if (num3 < 0)
            {
                num3 = 0;
            }
            if (num4 > Main.maxTilesX)
            {
                num4 = Main.maxTilesX;
            }
            if (num5 < 0)
            {
                num5 = 0;
            }
            if (num6 > Main.maxTilesY)
            {
                num6 = Main.maxTilesY;
            }
            bool flag = fly;
            if (!flag)
            {
                for (int num7 = num3; num7 < num4; num7++)
                {
                    for (int num8 = num5; num8 < num6; num8++)
                    {
                        if (Main.tile[num7, num8] != null && ((Main.tile[num7, num8].nactive() && (Main.tileSolid[Main.tile[num7, num8].type] || (Main.tileSolidTop[Main.tile[num7, num8].type] && Main.tile[num7, num8].frameY == 0))) || Main.tile[num7, num8].liquid > 64))
                        {
                            Vector2 vector;
                            vector.X = num7 * 16;
                            vector.Y = num8 * 16;
                            if (npc.position.X + npc.width > vector.X && npc.position.X < vector.X + 16f && npc.position.Y + npc.height > vector.Y && npc.position.Y < vector.Y + 16f)
                            {
                                flag = true;
                                break;
                            }
                        }
                    }
                }
            }
            if (!flag)
            {
                npc.localAI[1] = 1f;
                Rectangle rectangle = new Rectangle((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height);
                int num9 = 320;
                int num10 = 160;
                bool flag2 = true;
                if (npc.position.Y > Main.player[npc.target].position.Y)
                {
                    for (int num11 = 0; num11 < Main.player.Length; num11++)
                    {
                        if (Main.player[num11].active)
                        {
                            Rectangle rectangle2 = new Rectangle((int)Main.player[num11].position.X - num9, (int)Main.player[num11].position.Y - num10, num9 * 2, num10 * 2);
                            if (rectangle.Intersects(rectangle2))
                            {
                                flag2 = false;
                                break;
                            }
                        }
                    }
                    if (flag2)
                    {
                        flag = true;
                    }
                }
            }
            else
            {
                npc.localAI[1] = 0f;
            }
            float num12 = 20f;
            float num13 = 0.15f;//speed
            float num14 = 0.1f;//turn speed
            float num15 = Main.expertMode ? 1.4f : 1.2f;//amount
            if (LC)
            {
                num15 = 1.6f;
                if (Asmodeus.summonedSerpOut > 1)
                {
                    num15 = 1.8f;
                }
            }
            float num16 = num13 * (num15 - npc.life / npc.lifeMax);
            float num17 = num14 * (num15 - npc.life / npc.lifeMax);
            float num18 = num16;
            float num19 = num17;
            Vector2 vector2 = new Vector2(npc.position.X + npc.width * 0.5f, npc.position.Y + npc.height * 0.5f);
            float num20 = Main.player[npc.target].position.X + (Main.player[npc.target].width / 2);
            float num21 = Main.player[npc.target].position.Y + (Main.player[npc.target].height / 2);
            num20 = (int)(num20 / 16f) * 16;
            num21 = (int)(num21 / 16f) * 16;
            vector2.X = (int)(vector2.X / 16f) * 16;
            vector2.Y = (int)(vector2.Y / 16f) * 16;
            num20 -= vector2.X;
            num21 -= vector2.Y;
            float num22 = (float)Math.Sqrt(num20 * num20 + num21 * num21);
            if (npc.ai[1] > 0f && npc.ai[1] < Main.npc.Length)
            {
                try
                {
                    vector2 = new Vector2(npc.position.X + npc.width * 0.5f, npc.position.Y + npc.height * 0.5f);
                    num20 = Main.npc[(int)npc.ai[1]].position.X + (Main.npc[(int)npc.ai[1]].width / 2) - vector2.X;
                    num21 = Main.npc[(int)npc.ai[1]].position.Y + (Main.npc[(int)npc.ai[1]].height / 2) - vector2.Y;
                }
                catch
                {
                }
                npc.rotation = (float)Math.Atan2(num21, num20) + 1.57f;
                num22 = (float)Math.Sqrt(num20 * num20 + num21 * num21);
                int width = npc.width;
                num22 = (num22 - width) / num22;
                num20 *= num22;
                num21 *= num22;
                npc.velocity = Vector2.Zero;
                npc.position.X = npc.position.X + num20;
                npc.position.Y = npc.position.Y + num21;
                return;
            }
            if (!flag)
            {
                npc.TargetClosest(true);
                npc.velocity.Y = npc.velocity.Y + 0.15f;
                if (npc.velocity.Y > num12)
                {
                    npc.velocity.Y = num12;
                }
                if (Math.Abs(npc.velocity.X) + Math.Abs(npc.velocity.Y) < num12 * 0.4)
                {
                    if (npc.velocity.X < 0f)
                    {
                        npc.velocity.X = npc.velocity.X - num18 * 1.1f;
                    }
                    else
                    {
                        npc.velocity.X = npc.velocity.X + num18 * 1.1f;
                    }
                }
                else
                {
                    if (npc.velocity.Y == num12)
                    {
                        if (npc.velocity.X < num20)
                        {
                            npc.velocity.X = npc.velocity.X + num18;
                        }
                        else
                        {
                            if (npc.velocity.X > num20)
                            {
                                npc.velocity.X = npc.velocity.X - num18;
                            }
                        }
                    }
                    else
                    {
                        if (npc.velocity.Y > 4f)
                        {
                            if (npc.velocity.X < 0f)
                            {
                                npc.velocity.X = npc.velocity.X + num18 * 0.9f;
                            }
                            else
                            {
                                npc.velocity.X = npc.velocity.X - num18 * 0.9f;
                            }
                        }
                    }
                }
            }
            else
            {
                if (!fly && npc.behindTiles && npc.soundDelay == 0)
                {
                    float num23 = num22 / 40f;
                    if (num23 < 10f)
                    {
                        num23 = 10f;
                    }
                    if (num23 > 20f)
                    {
                        num23 = 20f;
                    }
                    npc.soundDelay = (int)num23;
                    Main.PlaySound(SoundID.Roar, (int)npc.position.X, (int)npc.position.Y, 1, 1f, 0f);
                }
                num22 = (float)Math.Sqrt(num20 * num20 + num21 * num21);
                float num24 = Math.Abs(num20);
                float num25 = Math.Abs(num21);
                float num26 = num12 / num22;
                num20 *= num26;
                num21 *= num26;
                if (((npc.velocity.X > 0f && num20 > 0f) || (npc.velocity.X < 0f && num20 < 0f)) && ((npc.velocity.Y > 0f && num21 > 0f) || (npc.velocity.Y < 0f && num21 < 0f)))
                {
                    if (npc.velocity.X < num20)
                    {
                        npc.velocity.X = npc.velocity.X + num19;
                    }
                    else
                    {
                        if (npc.velocity.X > num20)
                        {
                            npc.velocity.X = npc.velocity.X - num19;
                        }
                    }
                    if (npc.velocity.Y < num21)
                    {
                        npc.velocity.Y = npc.velocity.Y + num19;
                    }
                    else
                    {
                        if (npc.velocity.Y > num21)
                        {
                            npc.velocity.Y = npc.velocity.Y - num19;
                        }
                    }
                }
                if ((npc.velocity.X > 0f && num20 > 0f) || (npc.velocity.X < 0f && num20 < 0f) || (npc.velocity.Y > 0f && num21 > 0f) || (npc.velocity.Y < 0f && num21 < 0f))
                {
                    if (npc.velocity.X < num20)
                    {
                        npc.velocity.X = npc.velocity.X + num18;
                    }
                    else
                    {
                        if (npc.velocity.X > num20)
                        {
                            npc.velocity.X = npc.velocity.X - num18;
                        }
                    }
                    if (npc.velocity.Y < num21)
                    {
                        npc.velocity.Y = npc.velocity.Y + num18;
                    }
                    else
                    {
                        if (npc.velocity.Y > num21)
                        {
                            npc.velocity.Y = npc.velocity.Y - num18;
                        }
                    }
                    if (Math.Abs(num21) < num12 * 0.2 && ((npc.velocity.X > 0f && num20 < 0f) || (npc.velocity.X < 0f && num20 > 0f)))
                    {
                        if (npc.velocity.Y > 0f)
                        {
                            npc.velocity.Y = npc.velocity.Y + num18 * 2f;
                        }
                        else
                        {
                            npc.velocity.Y = npc.velocity.Y - num18 * 2f;
                        }
                    }
                    if (Math.Abs(num20) < num12 * 0.2 && ((npc.velocity.Y > 0f && num21 < 0f) || (npc.velocity.Y < 0f && num21 > 0f)))
                    {
                        if (npc.velocity.X > 0f)
                        {
                            npc.velocity.X = npc.velocity.X + num18 * 2f;
                        }
                        else
                        {
                            npc.velocity.X = npc.velocity.X - num18 * 2f;
                        }
                    }
                }
                else
                {
                    if (num24 > num25)
                    {
                        if (npc.velocity.X < num20)
                        {
                            npc.velocity.X = npc.velocity.X + num18 * 1.1f;
                        }
                        else
                        {
                            if (npc.velocity.X > num20)
                            {
                                npc.velocity.X = npc.velocity.X - num18 * 1.1f;
                            }
                        }
                        if (Math.Abs(npc.velocity.X) + Math.Abs(npc.velocity.Y) < num12 * 0.5)
                        {
                            if (npc.velocity.Y > 0f)
                            {
                                npc.velocity.Y = npc.velocity.Y + num18;
                            }
                            else
                            {
                                npc.velocity.Y = npc.velocity.Y - num18;
                            }
                        }
                    }
                    else
                    {
                        if (npc.velocity.Y < num21)
                        {
                            npc.velocity.Y = npc.velocity.Y + num18 * 1.1f;
                        }
                        else
                        {
                            if (npc.velocity.Y > num21)
                            {
                                npc.velocity.Y = npc.velocity.Y - num18 * 1.1f;
                            }
                        }
                        if (Math.Abs(npc.velocity.X) + Math.Abs(npc.velocity.Y) < num12 * 0.5)
                        {
                            if (npc.velocity.X > 0f)
                            {
                                npc.velocity.X = npc.velocity.X + num18;
                            }
                            else
                            {
                                npc.velocity.X = npc.velocity.X - num18;
                            }
                        }
                    }
                }
            }
            npc.rotation = (float)Math.Atan2(npc.velocity.Y, npc.velocity.X) + 1.57f;
            if (flag)
            {
                if (npc.localAI[0] != 1f)
                {
                    npc.netUpdate = true;
                }
                npc.localAI[0] = 1f;
            }
            else
            {
                if (npc.localAI[0] != 0f)
                {
                    npc.netUpdate = true;
                }
                npc.localAI[0] = 0f;
            }
            if (((npc.velocity.X > 0f && npc.oldVelocity.X < 0f) || (npc.velocity.X < 0f && npc.oldVelocity.X > 0f) || (npc.velocity.Y > 0f && npc.oldVelocity.Y < 0f) || (npc.velocity.Y < 0f && npc.oldVelocity.Y > 0f)) && !npc.justHit)
            {
                npc.netUpdate = true;
            }
        }
        public override bool CheckActive()
        {
            return false;
        }
        public override bool PreNPCLoot()
        {
            return false;
        }
    }
}