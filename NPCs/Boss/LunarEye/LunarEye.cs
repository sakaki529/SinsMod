using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.NPCs.Boss.LunarEye
{
    [AutoloadBossHead]
    public class LunarEye : ModNPC
    {
        private bool Esc;
        private int EscTimer;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("True Eye of Cthulhu");
            Main.npcFrameCount[npc.type] = 4;
        }
        public override void SetDefaults()
        {
            npc.width = 74;
            npc.height = 74;
            npc.lifeMax = 120000;
            npc.damage = 200;
            npc.defense = 60;
            npc.knockBackResist = 0f;
            npc.boss = true;
            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.netAlways = true;
            npc.netUpdate = true;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.npcSlots = 10f;
            npc.value = Item.buyPrice(0, 80, 0, 0);
            for (int i = 0; i < npc.buffImmune.Length; i++)
            {
                //npc.buffImmune[i] = true;
            }
            bossBag = mod.ItemType("LunarEyeBag");
            music = MusicID.TheTowers;
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = (int)(npc.lifeMax * 0.8f * bossLifeScale);
            npc.damage = 300;
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            Texture2D texture = Main.npcTexture[npc.type];
            Texture2D eye = mod.GetTexture("Extra/NPC/LunarEye_Extra");
            Vector2 origin = new Vector2(40f, 40f);
            Vector2 value = new Vector2(30f, 30f);
            Vector2 center = npc.Center;
            Point point4 = npc.Center.ToTileCoordinates();
            Color alpha = npc.GetAlpha(Color.Lerp(Lighting.GetColor(point4.X, point4.Y), Color.White, 0.3f));
            Main.spriteBatch.Draw(texture, npc.Center - Main.screenPosition, new Rectangle?(npc.frame), alpha, npc.rotation, origin, npc.scale, SpriteEffects.None, 0f);
            Vector2 value2 = Utils.Vector2FromElipse(npc.localAI[0].ToRotationVector2(), value * npc.localAI[1]);
            Main.spriteBatch.Draw(eye, npc.Center - Main.screenPosition + value2, null, alpha, npc.rotation, eye.Size() / 2f, npc.scale * npc.localAI[2], SpriteEffects.None, 0f);
            return false;
        }
        public override void FindFrame(int frameHeight)
        {
            npc.spriteDirection = 0;
            npc.frameCounter++;
            if (npc.frameCounter >= 6)
            {
                npc.frameCounter = 0;
                npc.frame.Y = npc.frame.Y + frameHeight;
            }
            if (npc.frame.Y / frameHeight >= Main.npcFrameCount[npc.type])
            {
                npc.frame.Y = 0;
            }
        }
        public override bool StrikeNPC(ref double damage, int defense, ref float knockback, int hitDirection, ref bool crit)
        {
            Player player = Main.player[npc.target];
            SinsPlayer modPlayer = player.GetModPlayer<SinsPlayer>();
            damage *= Main.expertMode ? 0.9f : 0.95f;
            if (damage >= npc.lifeMax * 0.5f && !modPlayer.Dev)
            {
                damage = 1;
                return false;
            }
            if (crit)
            {
                if (damage * 2 >= npc.lifeMax * 0.5f)
                {
                    damage = 1;
                    return false;
                }
            }
            return base.StrikeNPC(ref damage, defense, ref knockback, hitDirection, ref crit);
        }
        public override void HitEffect(int hitDirection, double damage)
        {
            if (npc.life > 0)
            {
                for (int i = 0; i < 20; i++)
                {
                    Dust.NewDust(npc.position, npc.width, npc.height, 209, 0f, 0f, 0, default(Color), 1f);
                }
            }
            if (npc.life <= 0)
            {
                float num = Main.rand.Next(-100, 100) / 200;
                for (int i = 0; i < 3; i++)
                {
                    Gore.NewGore(npc.position, npc.velocity * num * Main.rand.NextFloat(), mod.GetGoreSlot("Gores/Boss/LunarEye1"), npc.scale);
                }
                for (int j = 0; j < 4; j++)
                {
                    Gore.NewGore(npc.position, npc.velocity * num * Main.rand.NextFloat(), mod.GetGoreSlot("Gores/Boss/LunarEye2"), npc.scale * (j > 2 ? npc.scale * 0.6f: npc.scale));
                }
                for (int k = 0; k < 50; k++)
                {
                    Dust.NewDust(npc.position, npc.width, npc.height, 209, 0f, 0f, 0, default(Color), 1f);
                }
            }
        }
        public override void AI()
        {
            Player player = Main.player[npc.target];
            if (Main.rand.Next(420) == 0)
            {
                Main.PlaySound(29, (int)npc.Center.X, (int)npc.Center.Y, Main.rand.Next(100, 101), 1f, 0f);
            }
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
                npc.velocity.Y -= 0.2f;
                EscTimer++;
                if (EscTimer > 180)
                {
                    npc.active = false;
                }
                return;
            }
            if (npc.justHit)
            {
                if (NPC.CountNPCS(mod.NPCType("LunarEyeSmall")) < 2 && npc.life <= npc.lifeMax / 5 * 2 && NPC.downedMoonlord)
                {
                    float ai = NPC.CountNPCS(mod.NPCType("LunarEyeSmall") < 1 ? 1 : 150);
                    NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y + npc.height / 2, mod.NPCType("LunarEyeSmall"), npc.whoAmI, ai, ai);
                }
                else if (NPC.CountNPCS(mod.NPCType("LunarEyeSmall")) < 1)
                {
                    NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y + npc.height / 2, mod.NPCType("LunarEyeSmall"), npc.whoAmI, 1f, 360f);
                }
            }
            Vector2 value22 = new Vector2(30f);
            float num1241 = 0f;
            float num1242 = npc.ai[0];
            float[] var_9_3D22C_cp_0 = npc.ai;
            int var_9_3D22C_cp_1 = 1;
            float num244 = var_9_3D22C_cp_0[var_9_3D22C_cp_1];
            var_9_3D22C_cp_0[var_9_3D22C_cp_1] = num244 + 1f;
            int num1243 = 0;
            int num1244 = 0;
            while (num1243 < 10)
            {
                num1241 = NPC.MoonLordAttacksArray2[1, num1243];
                if (num1241 + num1244 > npc.ai[1])
                {
                    break;
                }
                num1244 += (int)num1241;
                int num = num1243;
                num1243 = num + 1;
            }
            if (num1243 == 10)
            {
                num1243 = 0;
                npc.ai[1] = 0f;
                num1241 = NPC.MoonLordAttacksArray2[1, num1243];
                num1244 = 0;
            }
            npc.ai[0] = NPC.MoonLordAttacksArray2[0, num1243];
            float num1245 = (int)npc.ai[1] - num1244;
            if (npc.ai[0] != num1242)
            {
                npc.netUpdate = true;
            }
            if (npc.ai[0] == -1f)
            {
                float[] var_9_3D357_cp_0 = npc.ai;
                int var_9_3D357_cp_1 = 1;
                num244 = var_9_3D357_cp_0[var_9_3D357_cp_1];
                var_9_3D357_cp_0[var_9_3D357_cp_1] = num244 + 1f;
                if (npc.ai[1] > 180f)
                {
                    npc.ai[1] = 0f;
                }
                float value23;
                if (npc.ai[1] < 60f)
                {
                    value23 = 0.75f;
                    npc.localAI[0] = 0f;
                    npc.localAI[1] = (float)Math.Sin(npc.ai[1] * 6.28318548f / 15f) * 0.35f;
                    if (npc.localAI[1] < 0f)
                    {
                        npc.localAI[0] = 3.14159274f;
                    }
                }
                else
                {
                    if (npc.ai[1] < 120f)
                    {
                        value23 = 1f;
                        if (npc.localAI[1] < 0.5f)
                        {
                            npc.localAI[1] += 0.025f;
                        }
                        npc.localAI[0] += 0.209439516f;
                    }
                    else
                    {
                        value23 = 1.15f;
                        npc.localAI[1] -= 0.05f;
                        if (npc.localAI[1] < 0f)
                        {
                            npc.localAI[1] = 0f;
                        }
                    }
                }
                npc.localAI[2] = MathHelper.Lerp(npc.localAI[2], value23, 0.3f);
            }
            if (npc.ai[0] == 0f)
            {
                npc.TargetClosest(false);
                Vector2 v7 = Main.player[npc.target].Center + Main.player[npc.target].velocity * 20f - npc.Center;
                npc.localAI[0] = npc.localAI[0].AngleLerp(v7.ToRotation(), 0.5f);
                npc.localAI[1] += 0.05f;
                if (npc.localAI[1] > 0.7f)
                {
                    npc.localAI[1] = 0.7f;
                }
                npc.localAI[2] = MathHelper.Lerp(npc.localAI[2], 1f, 0.2f);
                float scaleFactor9 = 24f;
                Vector2 center23 = npc.Center;
                Vector2 center24 = Main.player[npc.target].Center;
                Vector2 value24 = center24 - center23;
                Vector2 vector213 = value24 - Vector2.UnitY * 200f;
                vector213 = Vector2.Normalize(vector213) * scaleFactor9;
                int num1246 = 30;
                npc.velocity.X = (npc.velocity.X * (num1246 - 1) + vector213.X) / (float)num1246;
                npc.velocity.Y = (npc.velocity.Y * (num1246 - 1) + vector213.Y) / (float)num1246;
                float num1247 = 0.25f;
                int num;
                for (int num1248 = 0; num1248 < 200; num1248 = num + 1)
                {
                    if (num1248 != npc.whoAmI && Main.npc[num1248].active && Main.npc[num1248].type == 400 && Vector2.Distance(npc.Center, Main.npc[num1248].Center) < 300f)
                    {
                        if (npc.position.X < Main.npc[num1248].position.X)
                        {
                            npc.velocity.X = npc.velocity.X - num1247;
                        }
                        else
                        {
                            npc.velocity.X = npc.velocity.X + num1247;
                        }
                        if (npc.position.Y < Main.npc[num1248].position.Y)
                        {
                            npc.velocity.Y = npc.velocity.Y - num1247;
                        }
                        else
                        {
                            npc.velocity.Y = npc.velocity.Y + num1247;
                        }
                    }
                    num = num1248;
                }
                return;
            }
            if (npc.ai[0] == 1f)
            {
                if (num1245 == 0f)
                {
                    npc.TargetClosest(false);
                    npc.netUpdate = true;
                }
                npc.velocity *= 0.95f;
                if (npc.velocity.Length() < 1f)
                {
                    npc.velocity = Vector2.Zero;
                }
                Vector2 v8 = Main.player[npc.target].Center + Main.player[npc.target].velocity * 20f - npc.Center;
                npc.localAI[0] = npc.localAI[0].AngleLerp(v8.ToRotation(), 0.5f);
                npc.localAI[1] += 0.05f;
                if (npc.localAI[1] > 1f)
                {
                    npc.localAI[1] = 1f;
                }
                if (num1245 < 20f)
                {
                    npc.localAI[2] = MathHelper.Lerp(npc.localAI[2], 1.1f, 0.2f);
                }
                else
                {
                    npc.localAI[2] = MathHelper.Lerp(npc.localAI[2], 0.4f, 0.2f);
                }
                if (num1245 == num1241 - 35f)
                {
                    Main.PlaySound(4, (int)npc.position.X, (int)npc.position.Y, 6, 1f, 0f);
                }
                if ((num1245 == num1241 - 14f || num1245 == num1241 - 7f || num1245 == num1241) && Main.netMode != 1)
                {
                    Vector2 vector214 = Utils.Vector2FromElipse(npc.localAI[0].ToRotationVector2(), value22 * npc.localAI[1]);
                    Vector2 vector215 = Vector2.Normalize(v8) * 8f;
                    Projectile.NewProjectile(npc.Center.X + vector214.X, npc.Center.Y + vector214.Y, vector215.X, vector215.Y, 462, 40, 0f, Main.myPlayer, 0f, 0f);
                    return;
                }
            }
            else
            {
                if (npc.ai[0] == 2f)
                {
                    if (num1245 < 15f)
                    {
                        npc.localAI[1] -= 0.07f;
                        if (npc.localAI[1] < 0f)
                        {
                            npc.localAI[1] = 0f;
                        }
                        npc.localAI[2] = MathHelper.Lerp(npc.localAI[2], 0.4f, 0.2f);
                        npc.velocity *= 0.8f;
                        if (npc.velocity.Length() < 1f)
                        {
                            npc.velocity = Vector2.Zero;
                            return;
                        }
                    }
                    else
                    {
                        if (num1245 < 75f)
                        {
                            float num1249 = (num1245 - 15f) / 10f;
                            int num1250 = 0;
                            int num1251 = 0;
                            switch ((int)num1249)
                            {
                                case 0:
                                    num1250 = 0;
                                    num1251 = 2;
                                    break;
                                case 1:
                                    num1250 = 2;
                                    num1251 = 5;
                                    break;
                                case 2:
                                    num1250 = 5;
                                    num1251 = 3;
                                    break;
                                case 3:
                                    num1250 = 3;
                                    num1251 = 1;
                                    break;
                                case 4:
                                    num1250 = 1;
                                    num1251 = 4;
                                    break;
                                case 5:
                                    num1250 = 4;
                                    num1251 = 0;
                                    break;
                            }
                            Vector2 spinningpoint2 = Vector2.UnitY * -30f;
                            Vector2 value25 = spinningpoint2.RotatedBy(num1250 * 6.28318548f / 6f, default(Vector2));
                            Vector2 value26 = spinningpoint2.RotatedBy(num1251 * 6.28318548f / 6f, default(Vector2));
                            Vector2 vector216 = Vector2.Lerp(value25, value26, num1249 - (float)((int)num1249));
                            float value27 = vector216.Length() / 30f;
                            npc.localAI[0] = vector216.ToRotation();
                            npc.localAI[1] = MathHelper.Lerp(npc.localAI[1], value27, 0.5f);
                            int num;
                            for (int num1252 = 0; num1252 < 2; num1252 = num + 1)
                            {
                                int num1253 = Dust.NewDust(npc.Center + vector216 - Vector2.One * 4f, 0, 0, 229, 0f, 0f, 0, default(Color), 1f);
                                Dust dust = Main.dust[num1253];
                                dust.velocity += vector216 / 15f;
                                Main.dust[num1253].noGravity = true;
                                num = num1252;
                            }
                            if ((num1245 - 15f) % 10f == 0f && Main.netMode != 1)
                            {
                                Vector2 vector217 = Vector2.Normalize(vector216);
                                if (vector217.HasNaNs())
                                {
                                    vector217 = Vector2.UnitY * -1f;
                                }
                                vector217 *= 4f;
                                int num1254 = Projectile.NewProjectile(npc.Center.X + vector216.X, npc.Center.Y + vector216.Y, vector217.X, vector217.Y, 454, 60, 0f, Main.myPlayer, 30f, npc.whoAmI);
                                return;
                            }
                        }
                        else
                        {
                            if (num1245 < 105f)
                            {
                                npc.localAI[0] = npc.localAI[0].AngleLerp(npc.ai[2] - 1.57079637f, 0.2f);
                                npc.localAI[2] = MathHelper.Lerp(npc.localAI[2], 0.75f, 0.2f);
                                if (num1245 == 75f)
                                {
                                    npc.TargetClosest(false);
                                    npc.netUpdate = true;
                                    npc.velocity = Vector2.UnitY * -7f;
                                    int num;
                                    for (int num1255 = 0; num1255 < 1000; num1255 = num + 1)
                                    {
                                        Projectile projectile7 = Main.projectile[num1255];
                                        if (projectile7.active && projectile7.type == 454 && projectile7.ai[1] == npc.whoAmI && projectile7.ai[0] != -1f)
                                        {
                                            Projectile projectile8 = projectile7;
                                            projectile8.velocity += npc.velocity;
                                            projectile7.netUpdate = true;
                                        }
                                        num = num1255;
                                    }
                                }
                                npc.velocity.Y = npc.velocity.Y * 0.96f;
                                npc.ai[2] = (Main.player[npc.target].Center - npc.Center).ToRotation() + 1.57079637f;
                                npc.rotation = npc.rotation.AngleTowards(npc.ai[2], 0.104719758f);
                                return;
                            }
                            if (num1245 < 120f)
                            {
                                Main.PlaySound(29, (int)npc.Center.X, (int)npc.Center.Y, 102, 1f, 0f);
                                if (num1245 == 105f)
                                {
                                    npc.netUpdate = true;
                                }
                                Vector2 velocity6 = (npc.ai[2] - 1.57079637f).ToRotationVector2() * 12f;
                                npc.velocity = velocity6 * 2f;
                                int num;
                                for (int num1256 = 0; num1256 < 1000; num1256 = num + 1)
                                {
                                    Projectile projectile9 = Main.projectile[num1256];
                                    if (projectile9.active && projectile9.type == 454 && projectile9.ai[1] == (float)npc.whoAmI && projectile9.ai[0] != -1f)
                                    {
                                        projectile9.ai[0] = -1f;
                                        projectile9.velocity = velocity6;
                                        projectile9.netUpdate = true;
                                    }
                                    num = num1256;
                                }
                                return;
                            }
                            npc.velocity *= 0.92f;
                            npc.rotation = npc.rotation.AngleLerp(0f, 0.2f);
                            return;
                        }
                    }
                }
                else
                {
                    if (npc.ai[0] == 3f)
                    {
                        if (num1245 < 15f)
                        {
                            npc.localAI[1] -= 0.07f;
                            if (npc.localAI[1] < 0f)
                            {
                                npc.localAI[1] = 0f;
                            }
                            npc.localAI[2] = MathHelper.Lerp(npc.localAI[2], 0.4f, 0.2f);
                            npc.velocity *= 0.9f;
                            if (npc.velocity.Length() < 1f)
                            {
                                npc.velocity = Vector2.Zero;
                                return;
                            }
                        }
                        else
                        {
                            if (num1245 < 45f)
                            {
                                npc.localAI[0] = 0f;
                                npc.localAI[1] = (float)Math.Sin((num1245 - 15f) * 6.28318548f / 15f) * 0.5f;
                                if (npc.localAI[1] < 0f)
                                {
                                    npc.localAI[0] = 3.14159274f;
                                    return;
                                }
                            }
                            else
                            {
                                if (num1245 >= 185f)
                                {
                                    npc.velocity *= 0.88f;
                                    npc.rotation = npc.rotation.AngleLerp(0f, 0.2f);
                                    npc.localAI[1] -= 0.07f;
                                    if (npc.localAI[1] < 0f)
                                    {
                                        npc.localAI[1] = 0f;
                                    }
                                    npc.localAI[2] = MathHelper.Lerp(npc.localAI[2], 1f, 0.2f);
                                    return;
                                }
                                if (num1245 == 45f)
                                {
                                    npc.ai[2] = (Main.rand.Next(2) == 0).ToDirectionInt() * 6.28318548f / 40f;
                                    npc.netUpdate = true;
                                }
                                if ((num1245 - 15f - 30f) % 40f == 0f)
                                {
                                    npc.ai[2] *= 0.95f;
                                }
                                npc.localAI[0] += npc.ai[2];
                                npc.localAI[1] += 0.05f;
                                if (npc.localAI[1] > 1f)
                                {
                                    npc.localAI[1] = 1f;
                                }
                                Vector2 vector218 = npc.localAI[0].ToRotationVector2() * value22 * npc.localAI[1];
                                float scaleFactor10 = MathHelper.Lerp(8f, 20f, (num1245 - 15f - 30f) / 140f);
                                npc.velocity = Vector2.Normalize(vector218) * scaleFactor10;
                                npc.rotation = npc.rotation.AngleLerp(npc.velocity.ToRotation() + 1.57079637f, 0.2f);
                                if ((num1245 - 15f - 30f) % 10f == 0f && Main.netMode != 1)
                                {
                                    Vector2 vector219 = npc.Center + Vector2.Normalize(vector218) * value22.Length() * 0.4f;
                                    Vector2 vector220 = Vector2.Normalize(vector218) * 8f;
                                    float ai3 = (6.28318548f * (float)Main.rand.NextDouble() - 3.14159274f) / 30f + 0.0174532924f * npc.ai[2];
                                    Projectile.NewProjectile(vector219.X, vector219.Y, vector220.X, vector220.Y, 452, 50, 0f, Main.myPlayer, 0f, ai3);
                                    return;
                                }
                            }
                        }
                    }
                    else
                    {
                        if (npc.ai[0] == 4f)
                        {
                            if (num1245 == 0f)
                            {
                                npc.TargetClosest(false);
                                npc.netUpdate = true;
                            }
                            if (num1245 < 180f)
                            {
                                npc.localAI[2] = MathHelper.Lerp(npc.localAI[2], 1f, 0.2f);
                                npc.localAI[1] -= 0.05f;
                                if (npc.localAI[1] < 0f)
                                {
                                    npc.localAI[1] = 0f;
                                }
                                npc.velocity *= 0.95f;
                                if (npc.velocity.Length() < 1f)
                                {
                                    npc.velocity = Vector2.Zero;
                                }
                                if (num1245 >= 60f)
                                {
                                    Vector2 center25 = npc.Center;
                                    int num1257 = 0;
                                    if (num1245 >= 120f)
                                    {
                                        num1257 = 1;
                                    }
                                    int num;
                                    for (int num1258 = 0; num1258 < 1 + num1257; num1258 = num + 1)
                                    {
                                        int num1259 = 229;
                                        float num1260 = 0.8f;
                                        if (num1258 % 2 == 1)
                                        {
                                            num1259 = 229;
                                            num1260 = 1.65f;
                                        }
                                        Vector2 vector221 = center25 + ((float)Main.rand.NextDouble() * 6.28318548f).ToRotationVector2() * value22 / 2f;
                                        int num1261 = Dust.NewDust(vector221 - Vector2.One * 8f, 16, 16, num1259, npc.velocity.X / 2f, npc.velocity.Y / 2f, 0, default(Color), 1f);
                                        Main.dust[num1261].velocity = Vector2.Normalize(center25 - vector221) * 3.5f * (10f - num1257 * 2f) / 10f;
                                        Main.dust[num1261].noGravity = true;
                                        Main.dust[num1261].scale = num1260;
                                        Main.dust[num1261].customData = this;
                                        num = num1258;
                                    }
                                    return;
                                }
                            }
                            else
                            {
                                if (num1245 < num1241 - 15f)
                                {
                                    if (num1245 == 180f && Main.netMode != 1)
                                    {
                                        npc.TargetClosest(false);
                                        Vector2 vector222 = Main.player[npc.target].Center - npc.Center;
                                        vector222.Normalize();
                                        float num1262 = -1f;
                                        if (vector222.X < 0f)
                                        {
                                            num1262 = 1f;
                                        }
                                        vector222 = vector222.RotatedBy(-(double)num1262 * 6.28318548f / 6f, default(Vector2));
                                        Projectile.NewProjectile(npc.Center.X, npc.Center.Y, vector222.X, vector222.Y, mod.ProjectileType("PhantasmalDeathray"), 80, 0f, Main.myPlayer, num1262 * 6.28318548f / 540f, npc.whoAmI);
                                        npc.ai[2] = (vector222.ToRotation() + 9.424778f) * num1262;
                                        npc.netUpdate = true;
                                    }
                                    npc.localAI[1] += 0.05f;
                                    if (npc.localAI[1] > 1f)
                                    {
                                        npc.localAI[1] = 1f;
                                    }
                                    float num1263 = (npc.ai[2] >= 0f).ToDirectionInt();
                                    float num1264 = npc.ai[2];
                                    if (num1264 < 0f)
                                    {
                                        num1264 *= -1f;
                                    }
                                    num1264 += -9.424778f;
                                    num1264 += num1263 * 6.28318548f / 540f;
                                    npc.localAI[0] = num1264;
                                    npc.ai[2] = (num1264 + 9.424778f) * num1263;
                                    return;
                                }
                                npc.localAI[1] -= 0.07f;
                                if (npc.localAI[1] < 0f)
                                {
                                    npc.localAI[1] = 0f;
                                    return;
                                }
                            }
                        }
                    }
                }
            }
        }
        public override Color? GetAlpha(Color drawColor)
        {
            return Color.White * (1f - npc.alpha / 255);
        }
        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = ItemID.GreaterHealingPotion;
            if (Main.rand.Next(10) == 0)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("LunarEyeTrophy"));
            }
            if (Main.expertMode)
            {
                npc.DropBossBags();
            }
            else
            {
                if (Main.rand.Next(7) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("LunarEyeMask"));
                }
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("MoonDrip"), Main.rand.Next(2, 7));
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.FragmentVortex, Main.rand.Next(20, 36));
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.FragmentNebula, Main.rand.Next(20, 36));
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.FragmentSolar, Main.rand.Next(20, 36));
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.FragmentStardust, Main.rand.Next(20, 36));
                if (NPC.downedMoonlord)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.LunarOre, Main.rand.Next(30, 51));
                }
            }
            SinsWorld.downedLunarEye = true;
        }
    }
}