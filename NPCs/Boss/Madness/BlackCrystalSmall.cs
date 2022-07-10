using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.NPCs.Boss.Madness
{
    [AutoloadBossHead]
    public class BlackCrystalSmall : ModNPC
    {
        private bool FirstPhase = true;
        private bool SecondPhase = false;
        private bool Esc;
        private int EscTimer;
        private int Count;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Black Crystal");
        }
        public override void SetDefaults()
        {
            npc.width = 30;
            npc.height = 50;
            npc.lifeMax = 800000; 
            npc.damage = 400;
            npc.defense = 120;
            npc.knockBackResist = 0f;
            npc.aiStyle = -1;
            aiType = -1;
            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.HitSound = mod.GetLegacySoundSlot(SoundType.NPCHit, "Sounds/NPCHit/BCHit");
            npc.DeathSound = mod.GetLegacySoundSlot(SoundType.NPCKilled, "Sounds/NPCKilled/BCCCKilled");
            npc.npcSlots = 1f;
            npc.netAlways = true;
            npc.netUpdate = true;
            npc.value = Item.buyPrice(0, 0, 0, 0);
            for (int i = 0; i < npc.buffImmune.Length; i++)
            {
                npc.buffImmune[i] = true;
            }
            if (SinsMod.Instance.SinsMusicLoaded)
            {
                Mod mod = ModLoader.GetMod("SinsModMusic");
                music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/RAT");
            }
            else
            {
                music = MusicID.Boss2;
            }
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = (int)(npc.lifeMax * 0.8f * bossLifeScale);
            npc.damage = 450;
        }
        public override void HitEffect(int hitDirection, double damage)
        {
            for (int i = 0; i < 2; i++)
            {
                int d = Dust.NewDust(npc.position, npc.width, npc.height, 186, hitDirection, -1f, 0, default(Color), 1.0f);
                Main.dust[d].shader = GameShaders.Armor.GetSecondaryShader(56, Main.LocalPlayer);
            }
            if (npc.life <= 0)
            {
                npc.position.X = npc.position.X + (npc.width / 2);
                npc.position.Y = npc.position.Y + (npc.height / 2);
                npc.width = 30;
                npc.height = 30;
                npc.position.X = npc.position.X - (npc.width / 2);
                npc.position.Y = npc.position.Y - (npc.height / 2);
                for (int j = 0; j < 20; j++)
                {
                    int num = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, 186, 0f, 0f, 100, default(Color), 1.2f);
                    Main.dust[num].velocity *= 3f;
                    Main.dust[num].shader = GameShaders.Armor.GetSecondaryShader(56, Main.LocalPlayer);
                    if (Main.rand.Next(2) == 0)
                    {
                        Main.dust[num].scale = 0.5f;
                        Main.dust[num].fadeIn = 1f + Main.rand.Next(10) * 0.1f;
                    }
                }
                for (int k = 0; k < 40; k++)
                {
                    int num2 = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, 186, 0f, 0f, 100, default(Color), 1.0f);
                    Main.dust[num2].noGravity = true;
                    Main.dust[num2].velocity *= 5f;
                    num2 = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, 245, 0f, 0f, 100, default(Color), 1.2f);
                    Main.dust[num2].velocity *= 2f;
                    Main.dust[num2].shader = GameShaders.Armor.GetSecondaryShader(56, Main.LocalPlayer);
                }
            }
        }
        public override bool StrikeNPC(ref double damage, int defense, ref float knockback, int hitDirection, ref bool crit)
        {
            return false;
        }
        public override bool PreAI()
        {
            npc.boss = true;
            Player player = Main.player[npc.target];
            npc.spriteDirection = 0;
            if (!Main.npc[(int)npc.ai[1]].active || Main.npc[(int)npc.ai[1]].type != mod.NPCType("BlackCrystal"))
            {
                npc.life = 0;
                npc.active = false;
            }
            for (int i = 0; i < 8; i++)
            {
                int dust = Dust.NewDust(new Vector2(Main.npc[(int)npc.ai[1]].position.X, Main.npc[(int)npc.ai[1]].position.Y), npc.width, npc.height, 109, 0f, 0f, 150, default(Color), 1.3f);
                Main.dust[dust].noGravity = true;
                Main.dust[dust].fadeIn = 1.5f;
                Main.dust[dust].scale = 1.4f;
                Vector2 vector = new Vector2(Main.rand.Next(-100, 101), Main.rand.Next(-100, 101));
                vector.Normalize();
                vector *= Main.rand.Next(50, 100) * 0.04f;
                vector.Normalize();
                Main.dust[dust].position = npc.Center - vector;
                Vector2 newVelocity = Main.npc[(int)npc.ai[1]].Center - npc.Center;
                Main.dust[dust].velocity = newVelocity * 0.1f;
            }
            /*if (Main.rand.Next(1) == 0)
            {
                int dust = Dust.NewDust(new Vector2(npc.Center.X, npc.Center.Y), npc.width, npc.width, 109, 0f, 0f, 150, default(Color), 1.3f);
                Main.dust[dust].noGravity = true;
                Main.dust[dust].fadeIn = 1.5f;
                Main.dust[dust].scale = 1.4f;
                Vector2 vector = new Vector2(Main.rand.Next(-100, 101), Main.rand.Next(-100, 101));
                vector.Normalize();
                vector *= Main.rand.Next(50, 100) * 0.04f;
                vector.Normalize();
                Main.dust[dust].position = Main.npc[(int)npc.ai[1]].Center - vector;
                Vector2 newVelocity = npc.Center - Main.npc[(int)npc.ai[1]].Center;
                Main.dust[dust].velocity = newVelocity * 0.1f;
            }*/
            return true;
        }
        public override void AI()
        {
            Player player = Main.player[npc.target];
            player.releaseMount = false;
            if (player.mount.Active)
            {
                player.mount.Dismount(player);
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
            if (Esc == true)
            {
                npc.velocity.Y--;
                EscTimer++;
                if (EscTimer > 120)
                {
                    npc.active = false;
                }
                return;
            }
            if (npc.life < npc.lifeMax / 5)
            {
                npc.reflectingProjectiles = true;
            }
            #region saw
            if ((int)npc.ai[0] == 1)
            {
                npc.rotation = npc.velocity.X / 60f;
                Vector2 vector = new Vector2(npc.position.X + npc.width * 0.5f, npc.position.Y + npc.height * 0.5f);
                float num = Main.npc[(int)npc.ai[1]].position.X + (Main.npc[(int)npc.ai[1]].width / 2) - 200f * 1/*npc.ai[0]*/ - vector.X;
                float num2 = Main.npc[(int)npc.ai[1]].position.Y + 230f - vector.Y;
                float num3 = (float)Math.Sqrt(num * num + num2 * num2);
                if (npc.ai[2] != 99f)
                {
                    if (num3 > 800f)
                    {
                        npc.ai[2] = 99f;
                    }
                }
                else
                {
                    if (num3 < 400f)
                    {
                        npc.ai[2] = 0f;
                    }
                }
                if (!Main.npc[(int)npc.ai[1]].active)
                {
                    npc.ai[2] += 10f;
                    if (npc.ai[2] > 50f || Main.netMode != 2)
                    {
                        npc.life = -1;
                        npc.HitEffect(0, 10.0);
                        npc.active = false;
                    }
                }
                if (npc.ai[2] == 99f)
                {
                    if (npc.position.Y > Main.npc[(int)npc.ai[1]].position.Y)
                    {
                        if (npc.velocity.Y > 0f)
                        {
                            npc.velocity.Y = npc.velocity.Y * 0.96f;
                        }
                        npc.velocity.Y = npc.velocity.Y - 0.1f;
                        if (npc.velocity.Y > 8f)
                        {
                            npc.velocity.Y = 8f;
                        }
                    }
                    else
                    {
                        if (npc.position.Y < Main.npc[(int)npc.ai[1]].position.Y)
                        {
                            if (npc.velocity.Y < 0f)
                            {
                                npc.velocity.Y = npc.velocity.Y * 0.96f;
                            }
                            npc.velocity.Y = npc.velocity.Y + 0.1f;
                            if (npc.velocity.Y < -8f)
                            {
                                npc.velocity.Y = -8f;
                            }
                        }
                    }
                    if (npc.position.X + (npc.width / 2) > Main.npc[(int)npc.ai[1]].position.X + (Main.npc[(int)npc.ai[1]].width / 2))
                    {
                        if (npc.velocity.X > 0f)
                        {
                            npc.velocity.X = npc.velocity.X * 0.96f;
                        }
                        npc.velocity.X = npc.velocity.X - 0.5f;
                        if (npc.velocity.X > 12f)
                        {
                            npc.velocity.X = 12f;
                        }
                    }
                    if (npc.position.X + (npc.width / 2) < Main.npc[(int)npc.ai[1]].position.X + (Main.npc[(int)npc.ai[1]].width / 2))
                    {
                        if (npc.velocity.X < 0f)
                        {
                            npc.velocity.X = npc.velocity.X * 0.96f;
                        }
                        npc.velocity.X = npc.velocity.X + 0.5f;
                        if (npc.velocity.X < -12f)
                        {
                            npc.velocity.X = -12f;
                            return;
                        }
                    }
                }
                else
                {
                    if (npc.ai[2] == 0f || npc.ai[2] == 3f)
                    {
                        if (Main.npc[(int)npc.ai[1]].ai[1] != 0f)//charging
                        {
                            Vector2 vector2 = new Vector2(npc.position.X + npc.width * 0.5f, npc.position.Y + npc.height * 0.5f);
                            float num4 = Main.player[npc.target].position.X + (Main.player[npc.target].width / 2) - vector2.X;
                            float num5 = Main.player[npc.target].position.Y + (Main.player[npc.target].height / 2) - vector2.Y;
                            float num6 = (float)Math.Sqrt(num4 * num4 + num5 * num5);
                            num6 = 7f / num6;
                            num4 *= num6;
                            num5 *= num6;
                            if (npc.velocity.X > num4)
                            {
                                if (npc.velocity.X > 0f)
                                {
                                    npc.velocity.X = npc.velocity.X * 0.97f;
                                }
                                npc.velocity.X = npc.velocity.X - 0.05f;
                            }
                            if (npc.velocity.X < num4)
                            {
                                if (npc.velocity.X < 0f)
                                {
                                    npc.velocity.X = npc.velocity.X * 0.97f;
                                }
                                npc.velocity.X = npc.velocity.X + 0.05f;
                            }
                            if (npc.velocity.Y > num5)
                            {
                                if (npc.velocity.Y > 0f)
                                {
                                    npc.velocity.Y = npc.velocity.Y * 0.97f;
                                }
                                npc.velocity.Y = npc.velocity.Y - 0.05f;
                            }
                            if (npc.velocity.Y < num5)
                            {
                                if (npc.velocity.Y < 0f)
                                {
                                    npc.velocity.Y = npc.velocity.Y * 0.97f;
                                }
                                npc.velocity.Y = npc.velocity.Y + 0.05f;
                            }
                            npc.ai[3] += 1f;
                            if (npc.ai[3] >= 600f)
                            {
                                npc.ai[2] = 0f;
                                npc.ai[3] = 0f;
                                npc.netUpdate = true;
                            }
                        }
                        else
                        {
                            npc.ai[3] += 1f;
                            if (npc.ai[3] >= 300f)
                            {
                                npc.ai[2] += 1f;
                                npc.ai[3] = 0f;
                                npc.netUpdate = true;
                            }
                            if (npc.position.Y > Main.npc[(int)npc.ai[1]].position.Y + 320f)
                            {
                                if (npc.velocity.Y > 0f)
                                {
                                    npc.velocity.Y = npc.velocity.Y * 0.96f;
                                }
                                npc.velocity.Y = npc.velocity.Y - 0.04f;
                                if (npc.velocity.Y > 3f)
                                {
                                    npc.velocity.Y = 3f;
                                }
                            }
                            else
                            {
                                if (npc.position.Y < Main.npc[(int)npc.ai[1]].position.Y + 260f)
                                {
                                    if (npc.velocity.Y < 0f)
                                    {
                                        npc.velocity.Y = npc.velocity.Y * 0.96f;
                                    }
                                    npc.velocity.Y = npc.velocity.Y + 0.04f;
                                    if (npc.velocity.Y < -3f)
                                    {
                                        npc.velocity.Y = -3f;
                                    }
                                }
                            }
                            if (npc.position.X + (npc.width / 2) > Main.npc[(int)npc.ai[1]].position.X + (Main.npc[(int)npc.ai[1]].width / 2))
                            {
                                if (npc.velocity.X > 0f)
                                {
                                    npc.velocity.X = npc.velocity.X * 0.96f;
                                }
                                npc.velocity.X = npc.velocity.X - 0.3f;
                                if (npc.velocity.X > 12f)
                                {
                                    npc.velocity.X = 12f;
                                }
                            }
                            if (npc.position.X + (npc.width / 2) < Main.npc[(int)npc.ai[1]].position.X + (Main.npc[(int)npc.ai[1]].width / 2) - 250f)
                            {
                                if (npc.velocity.X < 0f)
                                {
                                    npc.velocity.X = npc.velocity.X * 0.96f;
                                }
                                npc.velocity.X = npc.velocity.X + 0.3f;
                                if (npc.velocity.X < -12f)
                                {
                                    npc.velocity.X = -12f;
                                }
                            }
                        }
                        Vector2 vector3 = new Vector2(npc.position.X + npc.width * 0.5f, npc.position.Y + npc.height * 0.5f);
                        float num7 = Main.npc[(int)npc.ai[1]].position.X + (Main.npc[(int)npc.ai[1]].width / 2) - 200f * 1/*npc.ai[0]*/ - vector3.X;
                        float num8 = Main.npc[(int)npc.ai[1]].position.Y + 230f - vector3.Y;
                        Math.Sqrt(num7 * num7 + num8 * num8);
                        return;
                    }
                    if (npc.ai[2] == 1f)
                    {
                        Vector2 vector4 = new Vector2(npc.position.X + npc.width * 0.5f, npc.position.Y + npc.height * 0.5f);
                        float num9 = Main.npc[(int)npc.ai[1]].position.X + (Main.npc[(int)npc.ai[1]].width / 2) - 200f * 1/*npc.ai[0]*/ - vector4.X;
                        float num10 = Main.npc[(int)npc.ai[1]].position.Y + 230f - vector4.Y;
                        float num11 = (float)Math.Sqrt(num9 * num9 + num10 * num10);
                        npc.velocity.X = npc.velocity.X * 0.95f;
                        npc.velocity.Y = npc.velocity.Y - 0.1f;
                        if (npc.velocity.Y < -8f)
                        {
                            npc.velocity.Y = -8f;
                        }
                        if (npc.position.Y < Main.npc[(int)npc.ai[1]].position.Y - 200f)
                        {
                            npc.TargetClosest(true);
                            npc.ai[2] = 2f;
                            vector4 = new Vector2(npc.position.X + npc.width * 0.5f, npc.position.Y + npc.height * 0.5f);
                            num9 = Main.player[npc.target].position.X + (Main.player[npc.target].width / 2) - vector4.X;
                            num10 = Main.player[npc.target].position.Y + (Main.player[npc.target].height / 2) - vector4.Y;
                            num11 = (float)Math.Sqrt(num9 * num9 + num10 * num10);
                            num11 = 22f / num11;
                            npc.velocity.X = num9 * num11 * 3f;
                            npc.velocity.Y = num10 * num11 * 3f;
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
                                npc.TargetClosest(true);
                                Vector2 vector5 = new Vector2(npc.position.X + npc.width * 0.5f, npc.position.Y + npc.height * 0.5f);
                                float num12 = Main.player[npc.target].position.X + (Main.player[npc.target].width / 2) - vector5.X;
                                float num13 = Main.player[npc.target].position.Y + (Main.player[npc.target].height / 2) - vector5.Y;
                                float num14 = (float)Math.Sqrt(num12 * num12 + num13 * num13);
                                num14 = 7f / num14;
                                num12 *= num14;
                                num13 *= num14;
                                if (npc.velocity.X > num12)
                                {
                                    if (npc.velocity.X > 0f)
                                    {
                                        npc.velocity.X = npc.velocity.X * 0.97f;
                                    }
                                    npc.velocity.X = npc.velocity.X - 0.07f;
                                }
                                if (npc.velocity.X < num12)
                                {
                                    if (npc.velocity.X < 0f)
                                    {
                                        npc.velocity.X = npc.velocity.X * 0.97f;
                                    }
                                    npc.velocity.X = npc.velocity.X + 0.07f;
                                }
                                if (npc.velocity.Y > num13)
                                {
                                    if (npc.velocity.Y > 0f)
                                    {
                                        npc.velocity.Y = npc.velocity.Y * 0.97f;
                                    }
                                    npc.velocity.Y = npc.velocity.Y - 0.7f;
                                }
                                if (npc.velocity.Y < num13)
                                {
                                    if (npc.velocity.Y < 0f)
                                    {
                                        npc.velocity.Y = npc.velocity.Y * 0.97f;
                                    }
                                    npc.velocity.Y = npc.velocity.Y + 0.07f;
                                }
                                npc.ai[3] += 1f;
                                if (npc.ai[3] >= 600f)
                                {
                                    npc.ai[2] = 0f;
                                    npc.ai[3] = 0f;
                                    npc.netUpdate = true;
                                }
                                vector5 = new Vector2(npc.position.X + npc.width * 0.5f, npc.position.Y + npc.height * 0.5f);
                                num12 = Main.npc[(int)npc.ai[1]].position.X + (Main.npc[(int)npc.ai[1]].width / 2) - 200f * 1/*npc.ai[0]*/ - vector5.X;
                                num13 = Main.npc[(int)npc.ai[1]].position.Y + 230f - vector5.Y;
                                num14 = (float)Math.Sqrt(num12 * num12 + num13 * num13);
                                return;
                            }
                            if (npc.ai[2] == 5f && ((npc.velocity.X > 0f && npc.position.X + (npc.width / 2) > Main.player[npc.target].position.X + (Main.player[npc.target].width / 2)) || (npc.velocity.X < 0f && npc.position.X + (npc.width / 2) < Main.player[npc.target].position.X + (Main.player[npc.target].width / 2))))
                            {
                                npc.ai[2] = 0f;
                            }
                        }
                    }
                }
                return;
            }
            #endregion
            #region vice
            if ((int)npc.ai[0] == 2)
            {
                npc.rotation = npc.velocity.X / 60f;
                Vector2 vector = new Vector2(npc.position.X + npc.width * 0.5f, npc.position.Y + npc.height * 0.5f);
                float num = Main.npc[(int)npc.ai[1]].position.X + (Main.npc[(int)npc.ai[1]].width / 2) - 200f * -1/*npc.ai[0]*/ - vector.X;
                float num2 = Main.npc[(int)npc.ai[1]].position.Y + 230f - vector.Y;
                float num3 = (float)Math.Sqrt(num * num + num2 * num2);
                if (npc.ai[2] != 99f)
                {
                    if (num3 > 800f)
                    {
                        npc.ai[2] = 99f;
                    }
                }
                else
                {
                    if (num3 < 400f)
                    {
                        npc.ai[2] = 0f;
                    }
                }
                if (!Main.npc[(int)npc.ai[1]].active)
                {
                    npc.ai[2] += 10f;
                    if (npc.ai[2] > 50f || Main.netMode != 2)
                    {
                        npc.life = -1;
                        npc.HitEffect(0, 10.0);
                        npc.active = false;
                    }
                }
                if (npc.ai[2] == 99f)
                {
                    if (npc.position.Y > Main.npc[(int)npc.ai[1]].position.Y)
                    {
                        if (npc.velocity.Y > 0f)
                        {
                            npc.velocity.Y = npc.velocity.Y * 0.96f;
                        }
                        npc.velocity.Y = npc.velocity.Y - 0.1f;
                        if (npc.velocity.Y > 8f)
                        {
                            npc.velocity.Y = 8f;
                        }
                    }
                    else
                    {
                        if (npc.position.Y < Main.npc[(int)npc.ai[1]].position.Y)
                        {
                            if (npc.velocity.Y < 0f)
                            {
                                npc.velocity.Y = npc.velocity.Y * 0.96f;
                            }
                            npc.velocity.Y = npc.velocity.Y + 0.1f;
                            if (npc.velocity.Y < -8f)
                            {
                                npc.velocity.Y = -8f;
                            }
                        }
                    }
                    if (npc.position.X + (npc.width / 2) > Main.npc[(int)npc.ai[1]].position.X + (Main.npc[(int)npc.ai[1]].width / 2))
                    {
                        if (npc.velocity.X > 0f)
                        {
                            npc.velocity.X = npc.velocity.X * 0.96f;
                        }
                        npc.velocity.X = npc.velocity.X - 0.5f;
                        if (npc.velocity.X > 12f)
                        {
                            npc.velocity.X = 12f;
                        }
                    }
                    if (npc.position.X + (npc.width / 2) < Main.npc[(int)npc.ai[1]].position.X + (Main.npc[(int)npc.ai[1]].width / 2))
                    {
                        if (npc.velocity.X < 0f)
                        {
                            npc.velocity.X = npc.velocity.X * 0.96f;
                        }
                        npc.velocity.X = npc.velocity.X + 0.5f;
                        if (npc.velocity.X < -12f)
                        {
                            npc.velocity.X = -12f;
                            return;
                        }
                    }
                }
                else
                {
                    if (npc.ai[2] == 0f || npc.ai[2] == 3f)
                    {
                        if (Main.npc[(int)npc.ai[1]].ai[1] == 3f && npc.timeLeft > 10)
                        {
                            //npc.timeLeft = 10;
                        }
                        if (Main.npc[(int)npc.ai[1]].ai[1] != 0f)
                        {
                            npc.TargetClosest(true);
                            if (Main.player[npc.target].dead)
                            {
                                npc.velocity.Y = npc.velocity.Y + 0.1f;
                                if (npc.velocity.Y > 16f)
                                {
                                    npc.velocity.Y = 16f;
                                }
                            }
                            else
                            {
                                Vector2 vector2 = new Vector2(npc.position.X + npc.width * 0.5f, npc.position.Y + npc.height * 0.5f);
                                float num4 = Main.player[npc.target].position.X + (Main.player[npc.target].width / 2) - vector2.X;
                                float num5 = Main.player[npc.target].position.Y + (Main.player[npc.target].height / 2) - vector2.Y;
                                float num6 = (float)Math.Sqrt(num4 * num4 + num5 * num5);
                                num6 = 12f / num6;
                                num4 *= num6;
                                num5 *= num6;
                                if (Math.Abs(npc.velocity.X) + Math.Abs(npc.velocity.Y) < 2f)
                                {
                                    npc.velocity.X = num4;
                                    npc.velocity.Y = num5;
                                    npc.netUpdate = true;
                                }
                                else
                                {
                                    npc.velocity *= 0.97f;
                                }
                                npc.ai[3] += 1f;
                                if (npc.ai[3] >= 600f)
                                {
                                    npc.ai[2] = 0f;
                                    npc.ai[3] = 0f;
                                    npc.netUpdate = true;
                                }
                            }
                        }
                        else
                        {
                            npc.ai[3] += 1f;
                            if (npc.ai[3] >= 600f)
                            {
                                npc.ai[2] += 1f;
                                npc.ai[3] = 0f;
                                npc.netUpdate = true;
                            }
                            if (npc.position.Y > Main.npc[(int)npc.ai[1]].position.Y + 300f)
                            {
                                if (npc.velocity.Y > 0f)
                                {
                                    npc.velocity.Y = npc.velocity.Y * 0.96f;
                                }
                                npc.velocity.Y = npc.velocity.Y - 0.1f;
                                if (npc.velocity.Y > 3f)
                                {
                                    npc.velocity.Y = 3f;
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
                                    npc.velocity.Y = npc.velocity.Y + 0.1f;
                                    if (npc.velocity.Y < -3f)
                                    {
                                        npc.velocity.Y = -3f;
                                    }
                                }
                            }
                            if (npc.position.X + (npc.width / 2) > Main.npc[(int)npc.ai[1]].position.X + (Main.npc[(int)npc.ai[1]].width / 2) + 250f)
                            {
                                if (npc.velocity.X > 0f)
                                {
                                    npc.velocity.X = npc.velocity.X * 0.94f;
                                }
                                npc.velocity.X = npc.velocity.X - 0.3f;
                                if (npc.velocity.X > 9f)
                                {
                                    npc.velocity.X = 9f;
                                }
                            }
                            if (npc.position.X + (npc.width / 2) < Main.npc[(int)npc.ai[1]].position.X + (Main.npc[(int)npc.ai[1]].width / 2))
                            {
                                if (npc.velocity.X < 0f)
                                {
                                    npc.velocity.X = npc.velocity.X * 0.94f;
                                }
                                npc.velocity.X = npc.velocity.X + 0.2f;
                                if (npc.velocity.X < -8f)
                                {
                                    npc.velocity.X = -8f;
                                }
                            }
                        }
                        Vector2 vector3 = new Vector2(npc.position.X + npc.width * 0.5f, npc.position.Y + npc.height * 0.5f);
                        float num7 = Main.npc[(int)npc.ai[1]].position.X + (Main.npc[(int)npc.ai[1]].width / 2) - 200f * -1/*npc.ai[0]*/ - vector3.X;
                        float num8 = Main.npc[(int)npc.ai[1]].position.Y + 230f - vector3.Y;
                        Math.Sqrt(num7 * num7 + num8 * num8);
                        return;
                    }
                    if (npc.ai[2] == 1f)
                    {
                        if (npc.velocity.Y > 0f)
                        {
                            npc.velocity.Y = npc.velocity.Y * 0.9f;
                        }
                        Vector2 vector4 = new Vector2(npc.position.X + npc.width * 0.5f, npc.position.Y + npc.height * 0.5f);
                        float num9 = Main.npc[(int)npc.ai[1]].position.X + (Main.npc[(int)npc.ai[1]].width / 2) - 280f * -1/*npc.ai[0]*/ - vector4.X;
                        float num10 = Main.npc[(int)npc.ai[1]].position.Y + 230f - vector4.Y;
                        float num11 = (float)Math.Sqrt(num9 * num9 + num10 * num10);
                        npc.velocity.X = (npc.velocity.X * 5f + Main.npc[(int)npc.ai[1]].velocity.X) / 6f;
                        npc.velocity.X = npc.velocity.X + 0.5f;
                        npc.velocity.Y = npc.velocity.Y - 0.5f;
                        if (npc.velocity.Y < -9f)
                        {
                            npc.velocity.Y = -9f;
                        }
                        if (npc.position.Y < Main.npc[(int)npc.ai[1]].position.Y - 280f)
                        {
                            npc.TargetClosest(true);
                            npc.ai[2] = 2f;
                            vector4 = new Vector2(npc.position.X + npc.width * 0.5f, npc.position.Y + npc.height * 0.5f);
                            num9 = Main.player[npc.target].position.X + (Main.player[npc.target].width / 2) - vector4.X;
                            num10 = Main.player[npc.target].position.Y + (Main.player[npc.target].height / 2) - vector4.Y;
                            num11 = (float)Math.Sqrt(num9 * num9 + num10 * num10);
                            num11 = 20f / num11;
                            npc.velocity.X = num9 * num11 * 3f;
                            npc.velocity.Y = num10 * num11 * 3f;
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
                                if (npc.ai[3] >= 4f)
                                {
                                    npc.ai[2] = 3f;
                                    npc.ai[3] = 0f;
                                    return;
                                }
                                npc.ai[2] = 1f;
                                npc.ai[3] += 1f;
                                return;
                            }
                        }
                        else
                        {
                            if (npc.ai[2] == 4f)
                            {
                                Vector2 vector5 = new Vector2(npc.position.X + npc.width * 0.5f, npc.position.Y + npc.height * 0.5f);
                                float num12 = Main.npc[(int)npc.ai[1]].position.X + (Main.npc[(int)npc.ai[1]].width / 2) - 200f * -1/*npc.ai[0]*/ - vector5.X;
                                float num13 = Main.npc[(int)npc.ai[1]].position.Y + 230f - vector5.Y;
                                float num14 = (float)Math.Sqrt(num12 * num12 + num13 * num13);
                                npc.velocity.Y = (npc.velocity.Y * 5f + Main.npc[(int)npc.ai[1]].velocity.Y) / 6f;
                                npc.velocity.X = npc.velocity.X + 0.5f;
                                if (npc.velocity.X > 12f)
                                {
                                    npc.velocity.X = 12f;
                                }
                                if (npc.position.X + (npc.width / 2) < Main.npc[(int)npc.ai[1]].position.X + (Main.npc[(int)npc.ai[1]].width / 2) - 500f || npc.position.X + (npc.width / 2) > Main.npc[(int)npc.ai[1]].position.X + (Main.npc[(int)npc.ai[1]].width / 2) + 500f)
                                {
                                    npc.TargetClosest(true);
                                    npc.ai[2] = 5f;
                                    vector5 = new Vector2(npc.position.X + npc.width * 0.5f, npc.position.Y + npc.height * 0.5f);
                                    num12 = Main.player[npc.target].position.X + (Main.player[npc.target].width / 2) - vector5.X;
                                    num13 = Main.player[npc.target].position.Y + (Main.player[npc.target].height / 2) - vector5.Y;
                                    num14 = (float)Math.Sqrt(num12 * num12 + num13 * num13);
                                    num14 = 17f / num14;
                                    npc.velocity.X = num12 * num14 * 3f;
                                    npc.velocity.Y = num13 * num14 * 3f;
                                    npc.netUpdate = true;
                                    return;
                                }
                            }
                            else
                            {
                                if (npc.ai[2] == 5f && npc.position.X + (npc.width / 2) < Main.player[npc.target].position.X + (Main.player[npc.target].width / 2) - 100f)
                                {
                                    if (npc.ai[3] >= 4f)
                                    {
                                        npc.ai[2] = 0f;
                                        npc.ai[3] = 0f;
                                        return;
                                    }
                                    npc.ai[2] = 4f;
                                    npc.ai[3] += 1f;
                                }
                            }
                        }
                    }
                }
                return;
            }
            #endregion
            #region cannon
            if ((int)npc.ai[0] == 3)
            {
                npc.rotation = npc.velocity.X / 30f;
                if (!Main.npc[(int)npc.ai[1]].active)
                {
                    npc.ai[2] += 10f;
                    if (npc.ai[2] > 50f || Main.netMode != 2)
                    {
                        npc.life = -1;
                        npc.HitEffect(0, 10.0);
                        npc.active = false;
                    }
                }
                if (npc.ai[2] == 0f)
                {
                    if (Main.npc[(int)npc.ai[1]].ai[1] == 3f && npc.timeLeft > 10)
                    {
                        npc.timeLeft = 10;
                    }
                    if (Main.npc[(int)npc.ai[1]].ai[1] != 0f)
                    {
                        npc.localAI[0] += 2f;
                        if (npc.position.Y > Main.npc[(int)npc.ai[1]].position.Y - 100f)
                        {
                            if (npc.velocity.Y > 0f)
                            {
                                npc.velocity.Y = npc.velocity.Y * 0.96f;
                            }
                            npc.velocity.Y = npc.velocity.Y - 0.07f;
                            if (npc.velocity.Y > 6f)
                            {
                                npc.velocity.Y = 6f;
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
                                if (npc.velocity.Y < -6f)
                                {
                                    npc.velocity.Y = -6f;
                                }
                            }
                        }
                        if (npc.position.X + (npc.width / 2) > Main.npc[(int)npc.ai[1]].position.X + (Main.npc[(int)npc.ai[1]].width / 2) - 120f * -1/*npc.ai[0]*/)
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
                        if (npc.position.X + (npc.width / 2) < Main.npc[(int)npc.ai[1]].position.X + (Main.npc[(int)npc.ai[1]].width / 2) - 120f * -1/*npc.ai[0]*/)
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
                        if (npc.ai[3] >= 1100f)
                        {
                            npc.localAI[0] = 0f;
                            npc.ai[2] = 1f;
                            npc.ai[3] = 0f;
                            npc.netUpdate = true;
                        }
                        if (npc.position.Y > Main.npc[(int)npc.ai[1]].position.Y - 150f)
                        {
                            if (npc.velocity.Y > 0f)
                            {
                                npc.velocity.Y = npc.velocity.Y * 0.96f;
                            }
                            npc.velocity.Y = npc.velocity.Y - 0.04f;
                            if (npc.velocity.Y > 3f)
                            {
                                npc.velocity.Y = 3f;
                            }
                        }
                        else
                        {
                            if (npc.position.Y < Main.npc[(int)npc.ai[1]].position.Y - 150f)
                            {
                                if (npc.velocity.Y < 0f)
                                {
                                    npc.velocity.Y = npc.velocity.Y * 0.96f;
                                }
                                npc.velocity.Y = npc.velocity.Y + 0.04f;
                                if (npc.velocity.Y < -3f)
                                {
                                    npc.velocity.Y = -3f;
                                }
                            }
                        }
                        if (npc.position.X + (npc.width / 2) > Main.npc[(int)npc.ai[1]].position.X + (Main.npc[(int)npc.ai[1]].width / 2) + 200f)
                        {
                            if (npc.velocity.X > 0f)
                            {
                                npc.velocity.X = npc.velocity.X * 0.96f;
                            }
                            npc.velocity.X = npc.velocity.X - 0.2f;
                            if (npc.velocity.X > 8f)
                            {
                                npc.velocity.X = 8f;
                            }
                        }
                        if (npc.position.X + (npc.width / 2) < Main.npc[(int)npc.ai[1]].position.X + (Main.npc[(int)npc.ai[1]].width / 2) + 160f)
                        {
                            if (npc.velocity.X < 0f)
                            {
                                npc.velocity.X = npc.velocity.X * 0.96f;
                            }
                            npc.velocity.X = npc.velocity.X + 0.2f;
                            if (npc.velocity.X < -8f)
                            {
                                npc.velocity.X = -8f;
                            }
                        }
                    }
                    Count++;
                    if (Count >= 5)
                    {
                        for (int i = 0; i < Main.maxPlayers; i++)
                        {
                            Player target = Main.player[i];
                            if (target.active && !target.dead)
                            {
                                ShineAttack(target.whoAmI);
                            }
                        }
                        Count = -20;
                    }
                    /*Vector2 vector = new Vector2(npc.position.X + npc.width * 0.5f, npc.position.Y + npc.height * 0.5f);
                    float num = Main.npc[(int)npc.ai[1]].position.X + (Main.npc[(int)npc.ai[1]].width / 2) - 200f * -1 //npc.ai[0]// - vector.X;
                    float num2 = Main.npc[(int)npc.ai[1]].position.Y + 230f - vector.Y;
                    float num3 = (float)Math.Sqrt(num * num + num2 * num2);
                    if (Main.netMode != 1)
                    {
                        npc.localAI[0] += 1f;
                        if (npc.localAI[0] > 80f)
                        {
                            npc.localAI[0] = 0f;
                            float num4 = 12f;
                            int num5 = 200;
                            if (Main.expertMode)
                            {
                                num5 = (int)(num5 / Main.expertDamage);
                            }
                            int type = mod.ProjectileType("GreedShot");
                            num3 = num4 / num3;
                            num = -num * num3;
                            num2 = -num2 * num3;
                            num += Main.rand.Next(-40, 41) * 0.01f;
                            num2 += Main.rand.Next(-40, 41) * 0.01f;
                            vector.X += num * 4f;
                            vector.Y += num2 * 4f;
                            Projectile.NewProjectile(vector.X, vector.Y, num, num2, type, num5, 0f, Main.myPlayer, -1f, 0f);
                            return;
                        }
                    }*/
                }
                else
                {
                    if (npc.ai[2] == 1f)
                    {
                        npc.ai[3] += 1f;
                        if (npc.ai[3] >= 300f)
                        {
                            npc.localAI[0] = 0f;
                            npc.ai[2] = 0f;
                            npc.ai[3] = 0f;
                            npc.netUpdate = true;
                        }
                        Vector2 vector2 = new Vector2(npc.position.X + npc.width * 0.5f, npc.position.Y + npc.height * 0.5f);
                        float num6 = Main.npc[(int)npc.ai[1]].position.X + (Main.npc[(int)npc.ai[1]].width / 2) - vector2.X;
                        float num7 = Main.npc[(int)npc.ai[1]].position.Y - vector2.Y;
                        num7 = Main.player[npc.target].position.Y + (Main.player[npc.target].height / 2) - 80f - vector2.Y;
                        float num8 = (float)Math.Sqrt(num6 * num6 + num7 * num7);
                        num8 = 6f / num8;
                        num6 *= num8;
                        num7 *= num8;
                        if (npc.velocity.X > num6)
                        {
                            if (npc.velocity.X > 0f)
                            {
                                npc.velocity.X = npc.velocity.X * 0.9f;
                            }
                            npc.velocity.X = npc.velocity.X - 0.04f;
                        }
                        if (npc.velocity.X < num6)
                        {
                            if (npc.velocity.X < 0f)
                            {
                                npc.velocity.X = npc.velocity.X * 0.9f;
                            }
                            npc.velocity.X = npc.velocity.X + 0.04f;
                        }
                        if (npc.velocity.Y > num7)
                        {
                            if (npc.velocity.Y > 0f)
                            {
                                npc.velocity.Y = npc.velocity.Y * 0.9f;
                            }
                            npc.velocity.Y = npc.velocity.Y - 0.08f;
                        }
                        if (npc.velocity.Y < num7)
                        {
                            if (npc.velocity.Y < 0f)
                            {
                                npc.velocity.Y = npc.velocity.Y * 0.9f;
                            }
                            npc.velocity.Y = npc.velocity.Y + 0.08f;
                        }
                        npc.TargetClosest(true);
                        Count++;
                        if (Count >= 5)
                        {
                            for (int i = 0; i < Main.maxPlayers; i++)
                            {
                                Player target = Main.player[i];
                                if (target.active && !target.dead)
                                {
                                    ShineAttack(target.whoAmI);
                                }
                            }
                            Count = -20;
                        }
                        /*vector2 = new Vector2(npc.position.X + npc.width * 0.5f, npc.position.Y + npc.height * 0.5f);
                        num6 = Main.player[npc.target].position.X + (Main.player[npc.target].width / 2) - vector2.X;
                        num7 = Main.player[npc.target].position.Y + (Main.player[npc.target].height / 2) - vector2.Y;
                        num8 = (float)Math.Sqrt(num6 * num6 + num7 * num7);
                        if (Main.netMode != 1)
                        {
                            npc.localAI[0] += 1f;
                            if (npc.localAI[0] > 40f)
                            {
                                npc.localAI[0] = 0f;
                                float num9 = 10f;
                                int num10 = 200;
                                if (Main.expertMode)
                                {
                                    num10 = (int)(num10 / Main.expertDamage);
                                }
                                int type2 = mod.ProjectileType("GreedShot");
                                num8 = num9 / num8;
                                num6 *= num8;
                                num7 *= num8;
                                num6 += Main.rand.Next(-40, 41) * 0.01f;
                                num7 += Main.rand.Next(-40, 41) * 0.01f;
                                vector2.X += num6 * 4f;
                                vector2.Y += num7 * 4f;
                                Projectile.NewProjectile(vector2.X, vector2.Y, num6, num7, type2, num10, 0f, Main.myPlayer, -1f, 0f);
                            }
                        }*/
                    }
                }
                return;
            }
            #endregion
            #region laser
            //if ((int)npc.ai[0] == 4)
            {
                npc.rotation = npc.velocity.X / 30f;
                if (!Main.npc[(int)npc.ai[1]].active)
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
                        npc.localAI[0] += 3f;
                        if (npc.position.Y > Main.npc[(int)npc.ai[1]].position.Y - 100f)
                        {
                            if (npc.velocity.Y > 0f)
                            {
                                npc.velocity.Y = npc.velocity.Y * 0.96f;
                            }
                            npc.velocity.Y = npc.velocity.Y - 0.07f;
                            if (npc.velocity.Y > 6f)
                            {
                                npc.velocity.Y = 6f;
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
                                if (npc.velocity.Y < -6f)
                                {
                                    npc.velocity.Y = -6f;
                                }
                            }
                        }
                        if (npc.position.X + (npc.width / 2) > Main.npc[(int)npc.ai[1]].position.X + (Main.npc[(int)npc.ai[1]].width / 2) - 120f * 1/*npc.ai[0]*/)
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
                        if (npc.position.X + (npc.width / 2) < Main.npc[(int)npc.ai[1]].position.X + (Main.npc[(int)npc.ai[1]].width / 2) - 120f * 1/*npc.ai[0]*/)
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
                        if (npc.ai[3] >= 800f)
                        {
                            npc.ai[2] += 1f;
                            npc.ai[3] = 0f;
                            npc.netUpdate = true;
                        }
                        if (npc.position.Y > Main.npc[(int)npc.ai[1]].position.Y - 100f)
                        {
                            if (npc.velocity.Y > 0f)
                            {
                                npc.velocity.Y = npc.velocity.Y * 0.96f;
                            }
                            npc.velocity.Y = npc.velocity.Y - 0.1f;
                            if (npc.velocity.Y > 3f)
                            {
                                npc.velocity.Y = 3f;
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
                                npc.velocity.Y = npc.velocity.Y + 0.1f;
                                if (npc.velocity.Y < -3f)
                                {
                                    npc.velocity.Y = -3f;
                                }
                            }
                        }
                        if (npc.position.X + (npc.width / 2) > Main.npc[(int)npc.ai[1]].position.X + (Main.npc[(int)npc.ai[1]].width / 2) - 180f * 1/*npc.ai[0]*/)
                        {
                            if (npc.velocity.X > 0f)
                            {
                                npc.velocity.X = npc.velocity.X * 0.96f;
                            }
                            npc.velocity.X = npc.velocity.X - 0.14f;
                            if (npc.velocity.X > 8f)
                            {
                                npc.velocity.X = 8f;
                            }
                        }
                        if (npc.position.X + (npc.width / 2) < Main.npc[(int)npc.ai[1]].position.X + (Main.npc[(int)npc.ai[1]].width / 2) - 180f * 1/*npc.ai[0]*/)
                        {
                            if (npc.velocity.X < 0f)
                            {
                                npc.velocity.X = npc.velocity.X * 0.96f;
                            }
                            npc.velocity.X = npc.velocity.X + 0.14f;
                            if (npc.velocity.X < -8f)
                            {
                                npc.velocity.X = -8f;
                            }
                        }
                    }
                    npc.TargetClosest(true);
                    Count++;
                    if (Count >= 5)
                    {
                        for (int i = 0; i < Main.maxPlayers; i++)
                        {
                            Player target = Main.player[i];
                            if (target.active && !target.dead)
                            {
                                ShineAttack(target.whoAmI);
                            }
                        }
                        Count = -20;
                    }
                    /*Vector2 vector = new Vector2(npc.position.X + npc.width * 0.5f, npc.position.Y + npc.height * 0.5f);
                    float num = Main.player[npc.target].position.X + (Main.player[npc.target].width / 2) - vector.X;
                    float num2 = Main.player[npc.target].position.Y + (Main.player[npc.target].height / 2) - vector.Y;
                    float num3 = (float)Math.Sqrt(num * num + num2 * num2);
                    if (Main.netMode != 1)
                    {
                        npc.localAI[0] += 1f;
                        if (npc.localAI[0] > 50f)
                        {
                            npc.localAI[0] = 0f;
                            float num4 = 8f;
                            int dmg = 190;
                            if (Main.expertMode)
                            {
                                dmg = (int)(dmg / Main.expertDamage);
                            }
                            int type = mod.ProjectileType("BlackCrystalShot");
                            if (Main.npc[(int)npc.ai[1]].ai[1] != 0)
                            {
                                type = mod.ProjectileType("BlackCrystalShot");
                            }
                            num3 = num4 / num3;
                            num *= num3;
                            num2 *= num3;
                            for (int i = 0; i < (Main.npc[(int)npc.ai[1]].ai[1] != 0 ? 4 : 1); i++)
                            {
                                num += Main.rand.Next(-40, 41) * 0.015f;
                                num2 += Main.rand.Next(-40, 41) * 0.015f;
                                vector.X += num * 8f;
                                vector.Y += num2 * 8f;
                                int num6 = Projectile.NewProjectile(vector.X, vector.Y, num / 2f, num2 / 2f, type, dmg, 0f, Main.myPlayer, 0f, 0f);
                                Main.projectile[num6].velocity *= 3f;
                            }
                            return;
                        }
                    }*/
                }
                else
                {
                    if (npc.ai[2] == 1f)
                    {
                        npc.ai[3] += 1f;
                        if (npc.ai[3] >= 200f)
                        {
                            npc.localAI[0] = 0f;
                            npc.ai[2] = 0f;
                            npc.ai[3] = 0f;
                            npc.netUpdate = true;
                        }
                        Vector2 vector2 = new Vector2(npc.position.X + npc.width * 0.5f, npc.position.Y + npc.height * 0.5f);
                        float num7 = Main.player[npc.target].position.X + (Main.player[npc.target].width / 2) - 350f - vector2.X;
                        float num8 = Main.player[npc.target].position.Y + (Main.player[npc.target].height / 2) - 20f - vector2.Y;
                        float num9 = (float)Math.Sqrt(num7 * num7 + num8 * num8);
                        num9 = 7f / num9;
                        num7 *= num9;
                        num8 *= num9;
                        if (npc.velocity.X > num7)
                        {
                            if (npc.velocity.X > 0f)
                            {
                                npc.velocity.X = npc.velocity.X * 0.9f;
                            }
                            npc.velocity.X = npc.velocity.X - 0.1f;
                        }
                        if (npc.velocity.X < num7)
                        {
                            if (npc.velocity.X < 0f)
                            {
                                npc.velocity.X = npc.velocity.X * 0.9f;
                            }
                            npc.velocity.X = npc.velocity.X + 0.1f;
                        }
                        if (npc.velocity.Y > num8)
                        {
                            if (npc.velocity.Y > 0f)
                            {
                                npc.velocity.Y = npc.velocity.Y * 0.9f;
                            }
                            npc.velocity.Y = npc.velocity.Y - 0.03f;
                        }
                        if (npc.velocity.Y < num8)
                        {
                            if (npc.velocity.Y < 0f)
                            {
                                npc.velocity.Y = npc.velocity.Y * 0.9f;
                            }
                            npc.velocity.Y = npc.velocity.Y + 0.03f;
                        }
                        npc.TargetClosest(true);
                        Count++;
                        if (Count >= 5)
                        {
                            for (int i = 0; i < Main.maxPlayers; i++)
                            {
                                Player target = Main.player[i];
                                if (target.active && !target.dead)
                                {
                                    ShineAttack(target.whoAmI);
                                }
                            }
                            Count = -20;
                        }
                        /*vector2 = new Vector2(npc.position.X + npc.width * 0.5f, npc.position.Y + npc.height * 0.5f);
                        num7 = Main.player[npc.target].position.X + (Main.player[npc.target].width / 2) - vector2.X;
                        num8 = Main.player[npc.target].position.Y + (Main.player[npc.target].height / 2) - vector2.Y;
                        num9 = (float)Math.Sqrt(num7 * num7 + num8 * num8);
                        if (Main.netMode == 1)
                        {
                            npc.localAI[0] += 1f;
                            if (npc.localAI[0] > 80f)
                            {
                                npc.localAI[0] = 0f;
                                float num10 = 10f;
                                int num11 = 190;
                                if (Main.expertMode)
                                {
                                    num11 = (int)(num11 / Main.expertDamage);
                                }
                                int type2 = mod.ProjectileType("BlackCrystalShot");
                                if (Main.npc[(int)npc.ai[1]].ai[1] != 0)
                                {
                                    type2 = mod.ProjectileType("BlackCrystalShot");
                                }
                                num9 = num10 / num9;
                                num7 *= num9;
                                num8 *= num9;
                                for (int j = 0; j < (Main.npc[(int)npc.ai[1]].ai[1] != 0 ? 4 : 1); j++)
                                {
                                    num7 += Main.rand.Next(-40, 41) * 0.015f;
                                    num8 += Main.rand.Next(-40, 41) * 0.015f;
                                    vector2.X += num7 * 8f;
                                    vector2.Y += num8 * 8f;
                                    int num12 = Projectile.NewProjectile(vector2.X, vector2.Y, num7 / 2f, num8 / 2f, type2, num11, 0f, Main.myPlayer, 0f, 0f);
                                    Main.projectile[num12].velocity *= 3f;
                                }
                            }
                        }*/
                    }
                }
                return;
            }
            #endregion
        }
        private void ShineAttack(int target)
        {
            if (!Main.player[target].active || Main.player[target].dead)
            {
                return;
            }
            Vector2 vec = npc.DirectionTo(Main.player[target].Center);
            if (vec.HasNaNs())
            {
                vec = Vector2.UnitY;
            }
            int direction = (vec.X > 0f) ? 1 : -1;
            npc.direction = direction;
            if (Main.player[Main.myPlayer].active)
            {
                Vector2 vector = Main.player[target].position + Main.player[target].Size * Utils.RandomVector2(Main.rand, 0f, 1f) - npc.Center;
                int num3;
                int num4 = 1;
                if (Main.expertMode || SinsWorld.LimitCut)
                {
                    num4 += 1;
                }
                for (int i = 0; i < num4; i = num3 + 1)
                {
                    Vector2 vector2 = npc.Center + vector;
                    if (i > 0)
                    {
                        vector2 = npc.Center + vector.RotatedByRandom(0.78539818525314331) * (Main.rand.NextFloat() * 0.5f + 0.75f);
                    }
                    float x = Main.rgbToHsl(new Color(20 + (int)(Main.DiscoR * 1.0f), 0, 20 + (int)(Main.DiscoR * 1.0f))).X;
                    Projectile.NewProjectile(vector2.X, vector2.Y, 0f, 0f, mod.ProjectileType("BlackCrystalExplosion"), 200, 0f, Main.myPlayer, -1, npc.whoAmI);
                    num3 = i;
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