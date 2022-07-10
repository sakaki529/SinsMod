using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.NPCs.Boss.Sins.Greed
{
    public class DesertKingHand : ModNPC
    {
        private bool Esc;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Desert King Hand");
        }
        public override void SetDefaults()
        {
            npc.width = 52;
            npc.height = 52;
            npc.lifeMax = 500;
            npc.damage = 12;
            npc.defense = 8;
            npc.knockBackResist = 0f;
            npc.aiStyle = -1;
            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.netAlways = true;
            npc.HitSound = SoundID.NPCHit49;
            npc.DeathSound = SoundID.NPCDeath6;
            npc.npcSlots = 2f;
            npc.value = Item.buyPrice(0, 0, 0, 0);
            npc.scale = 0.8f;
            npc.GetGlobalNPC<SinsNPC>().drawCenter = true;
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = (int)(npc.lifeMax * 0.8f * bossLifeScale);
            npc.damage = 24;
        }
        public override void HitEffect(int hitDirection, double damage)
        {
            if (npc.life <= 0)
            {
                int num = Gore.NewGore(new Vector2(npc.position.X, npc.position.Y), default(Vector2), 99, npc.scale + 0.2f);
                Main.gore[num].velocity *= 0.4f;
                Gore gore = Main.gore[num];
                gore.velocity.X = gore.velocity.X + 1f;
                Gore gore2 = Main.gore[num];
                gore2.velocity.Y = gore2.velocity.Y + 1f;
                num = Gore.NewGore(new Vector2(npc.position.X, npc.position.Y), default(Vector2), 99, npc.scale + 0.2f);
                Main.gore[num].velocity *= 0.4f;
                Gore gore3 = Main.gore[num];
                gore3.velocity.X = gore3.velocity.X - 1f;
                Gore gore4 = Main.gore[num];
                gore4.velocity.Y = gore4.velocity.Y + 1f;
                num = Gore.NewGore(new Vector2(npc.position.X, npc.position.Y), default(Vector2), 99, npc.scale + 0.2f);
                Main.gore[num].velocity *= 0.4f;
                Gore gore5 = Main.gore[num];
                gore5.velocity.X = gore5.velocity.X + 1f;
                Gore gore6 = Main.gore[num];
                gore6.velocity.Y = gore6.velocity.Y - 1f;
                num = Gore.NewGore(new Vector2(npc.position.X, npc.position.Y), default(Vector2), 99, npc.scale + 0.2f);
                Main.gore[num].velocity *= 0.4f;
                Gore gore7 = Main.gore[num];
                gore7.velocity.X = gore7.velocity.X - 1f;
                Gore gore8 = Main.gore[num];
                gore8.velocity.Y = gore8.velocity.Y - 1f;
            }
        }
        public override void AI()
        {
            npc.boss = true;
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
            npc.spriteDirection = -(int)npc.ai[0];
            if (!Main.npc[(int)npc.ai[1]].active || Main.npc[(int)npc.ai[1]].type != mod.NPCType("DesertKing"))
            {
                npc.ai[2] += 10f;
                if (npc.ai[2] > 50f || Main.netMode != 2)
                {
                    npc.life = -1;
                    npc.HitEffect(0, 10.0);
                    npc.active = false;
                }
            }
            if (npc.ai[2] == 0f || npc.ai[2] == 3f)
            {
                if (Main.npc[(int)npc.ai[1]].ai[1] == 3f && npc.timeLeft > 10)
                {
                    npc.timeLeft = 10;
                }
                if (Main.npc[(int)npc.ai[1]].ai[1] != 0f)
                {
                    if (npc.position.Y > Main.npc[(int)npc.ai[1]].position.Y - 100f)
                    {
                        if (npc.velocity.Y > 0f)
                        {
                            npc.velocity.Y = npc.velocity.Y * 0.96f;
                        }
                        npc.velocity.Y = npc.velocity.Y - 0.07f;
                        if (npc.velocity.Y > 7f)
                        {
                            npc.velocity.Y = 7f;
                        }
                    }
                    else
                    {
                        if (npc.position.Y < Main.npc[(int)npc.ai[1]].position.Y - 100f)
                        {
                            if (npc.velocity.Y < 0f)
                            {
                                npc.velocity.Y = npc.velocity.Y * 0.96f;
                            }
                            npc.velocity.Y = npc.velocity.Y + 0.07f;
                            if (npc.velocity.Y < -7f)
                            {
                                npc.velocity.Y = -7f;
                            }
                        }
                    }
                    if (npc.position.X + (npc.width / 2) > Main.npc[(int)npc.ai[1]].position.X + (Main.npc[(int)npc.ai[1]].width / 2) - 120f * npc.ai[0])
                    {
                        if (npc.velocity.X > 0f)
                        {
                            npc.velocity.X = npc.velocity.X * 0.96f;
                        }
                        npc.velocity.X = npc.velocity.X - 0.1f;
                        if (npc.velocity.X > 8f)
                        {
                            npc.velocity.X = 8f;
                        }
                    }
                    if (npc.position.X + (npc.width / 2) < Main.npc[(int)npc.ai[1]].position.X + (Main.npc[(int)npc.ai[1]].width / 2) - 120f * npc.ai[0])
                    {
                        if (npc.velocity.X < 0f)
                        {
                            npc.velocity.X = npc.velocity.X * 0.96f;
                        }
                        npc.velocity.X = npc.velocity.X + 0.1f;
                        if (npc.velocity.X < -8f)
                        {
                            npc.velocity.X = -8f;
                        }
                    }
                }
                else
                {
                    npc.ai[3] += 1f;
                    if (Main.expertMode)
                    {
                        npc.ai[3] += 0.5f;
                    }
                    if (npc.ai[3] >= 300f)
                    {
                        npc.ai[2] += 1f;
                        npc.ai[3] = 0f;
                        npc.netUpdate = true;
                    }
                    if (Main.expertMode)
                    {
                        if (npc.position.Y > Main.npc[(int)npc.ai[1]].position.Y + 230f)
                        {
                            if (npc.velocity.Y > 0f)
                            {
                                npc.velocity.Y = npc.velocity.Y * 0.96f;
                            }
                            npc.velocity.Y = npc.velocity.Y - 0.05f;
                            if (npc.velocity.Y > 5f)
                            {
                                npc.velocity.Y = 5f;
                            }
                        }
                        else
                        {
                            if (npc.position.Y < Main.npc[(int)npc.ai[1]].position.Y + 230f)
                            {
                                if (npc.velocity.Y < 0f)
                                {
                                    npc.velocity.Y = npc.velocity.Y * 0.96f;
                                }
                                npc.velocity.Y = npc.velocity.Y + 0.05f;
                                if (npc.velocity.Y < -5f)
                                {
                                    npc.velocity.Y = -5f;
                                }
                            }
                        }
                        if (npc.position.X + (npc.width / 2) > Main.npc[(int)npc.ai[1]].position.X + (Main.npc[(int)npc.ai[1]].width / 2) - 200f * npc.ai[0])
                        {
                            if (npc.velocity.X > 0f)
                            {
                                npc.velocity.X = npc.velocity.X * 0.96f;
                            }
                            npc.velocity.X = npc.velocity.X - 0.08f;
                            if (npc.velocity.X > 8f)
                            {
                                npc.velocity.X = 8f;
                            }
                        }
                        if (npc.position.X + (npc.width / 2) < Main.npc[(int)npc.ai[1]].position.X + (Main.npc[(int)npc.ai[1]].width / 2) - 200f * npc.ai[0])
                        {
                            if (npc.velocity.X < 0f)
                            {
                                npc.velocity.X = npc.velocity.X * 0.96f;
                            }
                            npc.velocity.X = npc.velocity.X + 0.08f;
                            if (npc.velocity.X < -8f)
                            {
                                npc.velocity.X = -8f;
                            }
                        }
                    }
                    if (npc.position.Y > Main.npc[(int)npc.ai[1]].position.Y + 230f)
                    {
                        if (npc.velocity.Y > 0f)
                        {
                            npc.velocity.Y = npc.velocity.Y * 0.96f;
                        }
                        npc.velocity.Y = npc.velocity.Y - 0.05f;
                        if (npc.velocity.Y > 5f)
                        {
                            npc.velocity.Y = 5f;
                        }
                    }
                    else
                    {
                        if (npc.position.Y < Main.npc[(int)npc.ai[1]].position.Y + 230f)
                        {
                            if (npc.velocity.Y < 0f)
                            {
                                npc.velocity.Y = npc.velocity.Y * 0.96f;
                            }
                            npc.velocity.Y = npc.velocity.Y + 0.05f;
                            if (npc.velocity.Y < -5f)
                            {
                                npc.velocity.Y = -5f;
                            }
                        }
                    }
                    if (npc.position.X + (npc.width / 2) > Main.npc[(int)npc.ai[1]].position.X + (Main.npc[(int)npc.ai[1]].width / 2) - 200f * npc.ai[0])
                    {
                        if (npc.velocity.X > 0f)
                        {
                            npc.velocity.X = npc.velocity.X * 0.96f;
                        }
                        npc.velocity.X = npc.velocity.X - 0.08f;
                        if (npc.velocity.X > 8f)
                        {
                            npc.velocity.X = 8f;
                        }
                    }
                    if (npc.position.X + (npc.width / 2) < Main.npc[(int)npc.ai[1]].position.X + (Main.npc[(int)npc.ai[1]].width / 2) - 200f * npc.ai[0])
                    {
                        if (npc.velocity.X < 0f)
                        {
                            npc.velocity.X = npc.velocity.X * 0.96f;
                        }
                        npc.velocity.X = npc.velocity.X + 0.08f;
                        if (npc.velocity.X < -8f)
                        {
                            npc.velocity.X = -8f;
                        }
                    }
                }
                Vector2 vector22 = new Vector2(npc.position.X + npc.width * 0.5f, npc.position.Y + npc.height * 0.5f);
                float num182 = Main.npc[(int)npc.ai[1]].position.X + (Main.npc[(int)npc.ai[1]].width / 2) - 200f * npc.ai[0] - vector22.X;
                float num183 = Main.npc[(int)npc.ai[1]].position.Y + 230f - vector22.Y;
                float num184 = (float)Math.Sqrt(num182 * num182 + num183 * num183);
                npc.rotation = (float)Math.Atan2(num183, num182) + 1.57f;
                return;
            }
            if (npc.ai[2] == 1f)
            {
                Vector2 vector23 = new Vector2(npc.position.X + npc.width * 0.5f, npc.position.Y + npc.height * 0.5f);
                float num185 = Main.npc[(int)npc.ai[1]].position.X + (Main.npc[(int)npc.ai[1]].width / 2) - 200f * npc.ai[0] - vector23.X;
                float num186 = Main.npc[(int)npc.ai[1]].position.Y + 230f - vector23.Y;
                float num187 = (float)Math.Sqrt(num185 * num185 + num186 * num186);
                npc.rotation = (float)Math.Atan2(num186, num185) + 1.57f;
                npc.velocity.X = npc.velocity.X * 0.95f;
                npc.velocity.Y = npc.velocity.Y - 0.1f;
                if (Main.expertMode)
                {
                    npc.velocity.Y = npc.velocity.Y - 0.08f;
                    if (npc.velocity.Y < -13f)
                    {
                        npc.velocity.Y = -13f;
                    }
                }
                else
                {
                    if (npc.velocity.Y < -8f)
                    {
                        npc.velocity.Y = -8f;
                    }
                }
                if (npc.position.Y < Main.npc[(int)npc.ai[1]].position.Y - 200f)
                {
                    npc.TargetClosest(true);
                    npc.ai[2] = 2f;
                    vector23 = new Vector2(npc.position.X + npc.width * 0.5f, npc.position.Y + npc.height * 0.5f);
                    num185 = Main.player[npc.target].position.X + (Main.player[npc.target].width / 2) - vector23.X;
                    num186 = Main.player[npc.target].position.Y + (Main.player[npc.target].height / 2) - vector23.Y;
                    num187 = (float)Math.Sqrt(num185 * num185 + num186 * num186);
                    if (Main.expertMode)
                    {
                        num187 = 21f / num187;
                    }
                    else
                    {
                        num187 = 18f / num187;
                    }
                    npc.velocity.X = num185 * num187;
                    npc.velocity.Y = num186 * num187;
                    npc.netUpdate = true;
                    return;
                }
            }
            else
            {
                if (npc.ai[2] == 2f)
                {
                    if (npc.position.Y > Main.player[npc.target].position.Y || npc.velocity.Y < 0f)
                    {
                        npc.ai[2] = 3f;
                        return;
                    }
                }
                else
                {
                    if (npc.ai[2] == 4f)
                    {
                        Vector2 vector24 = new Vector2(npc.position.X + npc.width * 0.5f, npc.position.Y + npc.height * 0.5f);
                        float num188 = Main.npc[(int)npc.ai[1]].position.X + (Main.npc[(int)npc.ai[1]].width / 2) - 200f * npc.ai[0] - vector24.X;
                        float num189 = Main.npc[(int)npc.ai[1]].position.Y + 230f - vector24.Y;
                        float num190 = (float)Math.Sqrt(num188 * num188 + num189 * num189);
                        npc.rotation = (float)Math.Atan2(num189, num188) + 1.57f;
                        npc.velocity.Y = npc.velocity.Y * 0.95f;
                        npc.velocity.X = npc.velocity.X + 0.1f * -npc.ai[0];
                        if (Main.expertMode)
                        {
                            npc.velocity.X = npc.velocity.X + 0.08f * -npc.ai[0];
                            if (npc.velocity.X < -12f)
                            {
                                npc.velocity.X = -12f;
                            }
                            else
                            {
                                if (npc.velocity.X > 12f)
                                {
                                    npc.velocity.X = 12f;
                                }
                            }
                        }
                        else
                        {
                            if (npc.velocity.X < -8f)
                            {
                                npc.velocity.X = -8f;
                            }
                            else
                            {
                                if (npc.velocity.X > 8f)
                                {
                                    npc.velocity.X = 8f;
                                }
                            }
                        }
                        if (npc.position.X + (npc.width / 2) < Main.npc[(int)npc.ai[1]].position.X + (Main.npc[(int)npc.ai[1]].width / 2) - 500f || npc.position.X + (npc.width / 2) > Main.npc[(int)npc.ai[1]].position.X + (Main.npc[(int)npc.ai[1]].width / 2) + 500f)
                        {
                            npc.TargetClosest(true);
                            npc.ai[2] = 5f;
                            vector24 = new Vector2(npc.position.X + npc.width * 0.5f, npc.position.Y + npc.height * 0.5f);
                            num188 = Main.player[npc.target].position.X + (Main.player[npc.target].width / 2) - vector24.X;
                            num189 = Main.player[npc.target].position.Y + (Main.player[npc.target].height / 2) - vector24.Y;
                            num190 = (float)Math.Sqrt(num188 * num188 + num189 * num189);
                            if (Main.expertMode)
                            {
                                num190 = 22f / num190;
                            }
                            else
                            {
                                num190 = 17f / num190;
                            }
                            npc.velocity.X = num188 * num190;
                            npc.velocity.Y = num189 * num190;
                            npc.netUpdate = true;
                            return;
                        }
                    }
                    else
                    {
                        if (npc.ai[2] == 5f && ((npc.velocity.X > 0f && npc.position.X + (npc.width / 2) > Main.player[npc.target].position.X + (Main.player[npc.target].width / 2)) || (npc.velocity.X < 0f && npc.position.X + (npc.width / 2) < Main.player[npc.target].position.X + (Main.player[npc.target].width / 2))))
                        {
                            npc.ai[2] = 0f;
                            return;
                        }
                    }
                }
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