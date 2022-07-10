using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Utilities;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.GameContent.Events;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace SinsMod.NPCs.NormalNPCs
{
    public class BlackBeast : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Black Beast");
            Main.npcFrameCount[npc.type] = 17;
            NPCID.Sets.FighterUsesDD2PortalAppearEffect[npc.type] = true;
        }
        public override void SetDefaults()
        {
            npc.lifeMax = 50000;
            npc.defense = 1000;
            npc.damage = 300;
            npc.width = 18;
            npc.height = 40;
            npc.aiStyle = -1;
            /*npc.aiStyle = 107;
            aiType = 569;*/
            npc.HitSound = SoundID.DD2_WitherBeastHurt;
            npc.DeathSound = SoundID.DD2_WitherBeastDeath;
            npc.knockBackResist = 0.005f;
            npc.value = Item.sellPrice(0, 6, 0, 0);
            npc.npcSlots = 0f;
            npc.lavaImmune = true;
            //npc.LazySetLiquidMovementDD2();
            npc.netAlways = true;
            for (int i = 0; i < npc.buffImmune.Length; i++)
            {
                npc.buffImmune[i] = true;
            }
            banner = npc.type;
            bannerItem = mod.ItemType("BlackBeastBanner");
            npc.GetGlobalNPC<SinsNPC>().damageMult = 0.5f;
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            Texture2D texture = Main.npcTexture[npc.type];
            Texture2D glowTexture = mod.GetTexture("Glow/NPC/BlackBeast_Glow");
            SpriteEffects spriteEffects = (npc.spriteDirection == 1) ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            spriteEffects ^= SpriteEffects.FlipHorizontally;
            Vector2 vector = new Vector2(Main.npcTexture[npc.type].Width / 2, Main.npcTexture[npc.type].Height / Main.npcFrameCount[npc.type] / 2);
            Color color = Lighting.GetColor((int)(npc.position.X + npc.width * 0.5) / 16, (int)((npc.position.Y + npc.height * 0.5) / 16.0));
            Color color2 = Color.White;
            Color color3 = color;
            color2.A = 127;
            float num = 0f;
            float num2 = -3f;
            int num3 = 0;
            int num4 = 0;
            int num5 = 1;
            int num6 = 15;
            int num7 = 0;
            int num8 = 4;
            float num9 = (float)Math.Cos(Main.GlobalTime % 1.5f / 1.5f * 6.28318548f) / 6f + 0.75f;
            float num10 = 0f;
            float amount = 0.5f;
            float amount2 = 0f;
            float scale = npc.scale;
            float value = npc.scale;
            float scaleFactor = 4f;
            if (npc.localAI[3] < 60f)
            {
                float num11 = 8f;
                float num12 = npc.localAI[3] / 60f;
                num8 = 3;
                num9 = 1f - num12 * num12;
                scaleFactor = num11;
                color2 = new Color(120, 120, 120, 200);
                amount2 = 1f;
                color3 = Color.Lerp(Color.Transparent, color3, num12 * num12);
            }
            for (int i = num5; i < num3; i += num4)
            {
                Vector2 vector2 = npc.oldPos[i];
                Color color4 = color3;
                color4 = Color.Lerp(color4, color2, amount);
                color4 = npc.GetAlpha(color4);
                color4 *= (float)(num3 - i) / num6;
                float rotation = npc.rotation;
                if (num7 == 1)
                {
                    float num13 = npc.oldRot[i];
                }
                float scale2 = MathHelper.Lerp(scale, value, 1f - (float)(num3 - i) / num6);
                Vector2 vector3 = npc.oldPos[i] + new Vector2(npc.width, npc.height) / 2f - Main.screenPosition;
                vector3 -= new Vector2(texture.Width, texture.Height / Main.npcFrameCount[npc.type]) * npc.scale / 2f;
                vector3 += vector * npc.scale + new Vector2(0f, num + num2 + npc.gfxOffY);
                Main.spriteBatch.Draw(texture, vector3, npc.frame, color4, npc.rotation, vector, scale2, spriteEffects, 0f);
            }
            int num14;
            for (int num15 = 0; num15 < num8; num15 = num14 + 1)
            {
                Color color5 = color;
                color5 = Color.Lerp(color5, color2, amount);
                color5 = npc.GetAlpha(color5);
                color5 = Color.Lerp(color5, color2, amount2);
                color5 *= 1f - num9;
                Vector2 vector4 = npc.Center + ((float)num15 / num8 * 6.28318548f + npc.rotation + num10).ToRotationVector2() * scaleFactor * num9 - Main.screenPosition;
                vector4 -= new Vector2(texture.Width, texture.Height / Main.npcFrameCount[npc.type]) * npc.scale / 2f;
                vector4 += vector * npc.scale + new Vector2(0f, num + num2 + npc.gfxOffY);
                Main.spriteBatch.Draw(texture, vector4, npc.frame, color5, npc.rotation, vector, npc.scale, spriteEffects, 0f);
                num14 = num15;
            }
            Vector2 vector5 = npc.Center - Main.screenPosition;
            vector5 -= new Vector2(texture.Width, texture.Height / Main.npcFrameCount[npc.type]) * npc.scale / 2f;
            vector5 += vector * npc.scale + new Vector2(0f, num + num2 + npc.gfxOffY);
            Main.spriteBatch.Draw(texture, vector5, npc.frame, npc.GetAlpha(color3), npc.rotation, vector, npc.scale, spriteEffects, 0f);
            if (npc.localAI[3] >= 60f)
            {
                Color color6 = new Color(127 - npc.alpha, 127 - npc.alpha, 127 - npc.alpha, 0).MultiplyRGBA(color2);
                for (int num16 = 0; num16 < num8; num16 = num14 + 1)
                {
                    Color color7 = color6;
                    color7 = npc.GetAlpha(color7);
                    color7 *= 1f - num9;
                    Vector2 vector6 = npc.Center + ((float)num16 / num8 * 6.28318548f + npc.rotation + num10).ToRotationVector2() * (4f * num9 + 2f) - Main.screenPosition;
                    vector6 -= new Vector2(texture.Width, texture.Height / Main.npcFrameCount[npc.type]) * npc.scale / 2f;
                    vector6 += vector * npc.scale + new Vector2(0f, num + num2 + npc.gfxOffY);
                    Main.spriteBatch.Draw(glowTexture, vector6, npc.frame, color7, npc.rotation, vector, npc.scale, spriteEffects, 0f);
                    num14 = num16;
                }
                Main.spriteBatch.Draw(glowTexture, vector5, npc.frame, color6, npc.rotation, vector, npc.scale, spriteEffects, 0f);
                float num17 = npc.localAI[0];
                if (num17 > 0f)
                {
                    byte value2 = (byte)((Math.Cos(num17 * 6.28318548f / 60f) * 0.5 + 0.5) * 32.0 + 0.0);
                    Color color8 = new Color(255, 255, 255, value2) * 0.75f;
                    float num18 = 1f;
                    if (num17 < 60f)
                    {
                        float num19 = Utils.InverseLerp(0f, 60f, num17, false);
                        color8 *= num19;
                        num18 = MathHelper.Lerp(1f, 0.5f, 1f - num19 * num19);
                    }
                    Texture2D texture2 = Main.projectileTexture[mod.ProjectileType("BlackCrystalExplosion")];
                    Vector2 origin = texture2.Size() / 2f;
                    Vector2 scale3 = new Vector2(num18);
                    float num20 = num17 * 0.00418879045f;
                    float num21 = 1.57079637f;
                    scale3.Y *= 1f;
                    scale3.X *= 1f;
                    for (float num22 = 0f; num22 < 16f; num22 += 1f)
                    {
                        float num23 = num20 + 6.28318548f * (num22 / 16f);
                        Vector2 position = npc.Center - Main.screenPosition + num23.ToRotationVector2() * 400f * num18;
                        Main.spriteBatch.Draw(texture2, position, null, color8, num23 + 1.57079637f + num21, origin, scale3, SpriteEffects.None, 0f);
                    }
                }
            }
            return false;
        }
        public override void HitEffect(int hitDirection, double damage)
        {
            int num;
            if (npc.life > 0)
            {
                int num2 = 0;
                while (num2 < damage / npc.lifeMax * 20.0)
                {
                    Dust.NewDust(npc.position, npc.width, npc.height, 5, hitDirection, -1f, 0, default(Color), 1f);
                    num = num2;
                    num2 = num + 1;
                }
            }
            else
            {
                int num3 = 0;
                while (num3 < 20f)
                {
                    Dust.NewDust(npc.position, npc.width, npc.height, 5, hitDirection, -1f, 0, default(Color), 1f);
                    num = num3;
                    num3 = num + 1;
                }
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/NPC/BlackBrute1"), npc.scale);
                Gore.NewGore(new Vector2(npc.position.X, npc.position.Y + 20f), npc.velocity, mod.GetGoreSlot("Gores/NPC/BlackBrute2"), npc.scale);
                Gore.NewGore(new Vector2(npc.position.X, npc.position.Y + 20f), npc.velocity, mod.GetGoreSlot("Gores/NPC/BlackBrute4"), npc.scale);
                Gore.NewGore(new Vector2(npc.position.X, npc.position.Y + 34f), npc.velocity, mod.GetGoreSlot("Gores/NPC/BlackBrute3"), npc.scale);
                Gore.NewGore(new Vector2(npc.position.X, npc.position.Y + 34f), npc.velocity, mod.GetGoreSlot("Gores/NPC/BlackBrute3"), npc.scale);
            }
        }
        public override void FindFrame(int frameHeight)
        {
            int num = 1;
            if (!Main.dedServ)
            {
                if (!Main.NPCLoaded[npc.type] || Main.npcTexture[npc.type] == null)
                {
                    return;
                }
                num = Main.npcTexture[npc.type].Height / Main.npcFrameCount[npc.type];
            }
            if (npc.ai[0] > 0f)
            {
                int num2 = npc.frame.Y / npc.frame.Height;
                npc.spriteDirection = npc.direction;
                if (num2 < 5 || num2 > 16)
                {
                    npc.frameCounter = 0.0;
                }
                num2 = 7;
                npc.frameCounter += 1.0;
                int num4 = 0;
                if (npc.frameCounter >= (5 * ++num4))
                {
                    num2 = 8;
                }
                if (npc.frameCounter >= (5 * ++num4))
                {
                    num2 = 9;
                }
                if (npc.frameCounter >= (5 * ++num4))
                {
                    num2 = 10;
                }
                if (npc.frameCounter >= (5 * ++num4))
                {
                    num2 = 7;
                }
                if (npc.frameCounter >= (5 * ++num4))
                {
                    num2 = 8;
                }
                if (npc.frameCounter >= (5 * ++num4))
                {
                    num2 = 9;
                }
                if (npc.frameCounter >= (5 * ++num4))
                {
                    num2 = 10;
                }
                if (npc.frameCounter >= (5 * ++num4))
                {
                    num2 = 7;
                }
                if (npc.frameCounter >= (5 * ++num4))
                {
                    num2 = 8;
                }
                if (npc.frameCounter >= (5 * ++num4))
                {
                    num2 = 9;
                }
                if (npc.frameCounter >= (5 * ++num4))
                {
                    num2 = 10;
                }
                if (npc.frameCounter >= (5 * ++num4))
                {
                    num2 = 7;
                }
                if (npc.frameCounter >= (5 * ++num4))
                {
                    num2 = 8;
                }
                if (npc.frameCounter >= (5 * ++num4))
                {
                    num2 = 9;
                }
                if (npc.frameCounter >= (5 * ++num4))
                {
                    num2 = 10;
                }
                if (npc.frameCounter >= (5 * ++num4))
                {
                    num2 = 7;
                }
                if (npc.frameCounter >= (5 * ++num4))
                {
                    num2 = 8;
                }
                if (npc.frameCounter >= (5 * ++num4))
                {
                    num2 = 9;
                }
                if (npc.frameCounter >= (5 * ++num4))
                {
                    num2 = 10;
                }
                if (npc.frameCounter >= (5 * ++num4))
                {
                    num2 = 7;
                }
                if (npc.frameCounter >= (5 * ++num4))
                {
                    num2 = 8;
                }
                if (npc.frameCounter >= (5 * ++num4))
                {
                    num2 = 9;
                }
                if (npc.frameCounter >= (5 * ++num4))
                {
                    num2 = 10;
                }
                if (npc.frameCounter >= (5 * ++num4))
                {
                    num2 = 11;
                }
                if (npc.frameCounter >= (5 * ++num4))
                {
                    num2 = 12;
                }
                if (npc.frameCounter >= (5 * ++num4))
                {
                    num2 = 13;
                }
                if (npc.frameCounter >= (5 * (num4 + 1)))
                {
                    num2 = 14;
                }
                if (npc.frameCounter >= 270.0)
                {
                    num2 = 14;
                    npc.frameCounter -= 10.0;
                }
                npc.frame.Y = num * num2;
            }
            else
            {
                if (npc.velocity.Y == 0f)
                {
                    npc.spriteDirection = npc.direction;
                }
                if (npc.velocity.Y != 0f || (npc.direction == -1 && npc.velocity.X > 0f) || (npc.direction == 1 && npc.velocity.X < 0f))
                {
                    npc.frameCounter = 0.0;
                    npc.frame.Y = num * 4;
                }
                else
                {
                    if (npc.velocity.X == 0f)
                    {
                        npc.frameCounter = 0.0;
                        npc.frame.Y = num * 6;
                    }
                    else
                    {
                        npc.frameCounter += Math.Abs(npc.velocity.X);
                        if (npc.frameCounter >= 56.0 || npc.frameCounter < 0.0)
                        {
                            npc.frameCounter = 0.0;
                        }
                        npc.frame.Y = num * (int)(npc.frameCounter / 8.0);
                    }
                }
            }
        }
        public override void AI()
        {
            bool flag = npc.velocity.X == 0f && npc.velocity.Y == 0f && !npc.justHit;
            bool flag2 = false;
            bool flag3 = false;
            bool flag4 = false;
            int num = 32;
            int num2 = 15;
            float num3 = 9f;
            bool flag5 = false;
            float num4 = 40f;
            int num5 = 30;
            int num6 = 0;
            bool flag6 = false;
            bool flag7 = true;
            float num7 = 0.9f;
            bool flag8 = false;
            bool flag9 = false;
            bool flag10 = false;
            bool flag11 = false;
            bool flag12 = false;
            bool flag13 = false;
            bool flag14 = false;
            bool flag15 = true;
            int num8 = 70;
            int num9 = num8 / 2;
            float scaleFactor = 11f;
            Vector2 zero = Vector2.Zero;
            int num10 = 1;
            int num11 = 81;
            float num12 = 700f;
            float num13 = 0f;
            float num14 = 0.1f;
            Vector2? vector = null;
            float num15 = 0.5f;
            int num16 = 1;
            float scaleFactor2 = 1f;
            bool flag16 = false;
            bool flag17 = true;
            bool flag18 = false;
            int num17 = 30;
            bool flag19 = false;
            bool flag20 = false;
            bool flag21 = false;
            bool flag22 = false;
            LegacySoundStyle style = null;
            int num18 = 0;
            bool flag23 = false;
            float num19 = 1f;
            float num20 = 0.07f;
            float num21 = 0.8f;
            float num22 = npc.width / 2 + 6;
            bool flag24 = npc.directionY < 0;
            bool flag25 = false;
            int num23 = 1;
            bool flag26 = false;
            float num24 = 0.025f;
            NPCAimedTarget targetData = npc.GetTargetData(true);
            if (targetData.Type == NPCTargetType.NPC && Main.npc[npc.TranslatedTargetIndex].type == 548 && Main.npc[npc.TranslatedTargetIndex].dontTakeDamageFromHostiles)
            {
                NPCUtils.TargetClosestOldOnesInvasion(npc, true, null);
                targetData = npc.GetTargetData(true);
            }
            if (!targetData.Invalid)
            {
                flag2 = !Collision.CanHit(npc.Center, 0, 0, targetData.Center, 0, 0) && (npc.direction == Math.Sign(targetData.Center.X - npc.Center.X) || (npc.noGravity && npc.Distance(targetData.Center) > 50f && npc.Center.Y > targetData.Center.Y));
            }
            flag2 &= npc.ai[0] <= 0f;
            if (flag2)
            {
                if (npc.velocity.Y == 0f || Math.Abs(targetData.Center.Y - npc.Center.Y) > 800f)
                {
                    npc.noGravity = true;
                    npc.noTileCollide = true;
                }
            }
            else
            {
                npc.noGravity = false;
                npc.noTileCollide = false;
            }
            bool flag27 = NPCID.Sets.FighterUsesDD2PortalAppearEffect[npc.type];
            bool flag28 = true;

            num5 = 110;
            num20 = 0.16f;
            num21 = 0.7f;
            num19 = 1.4f;
            flag5 = true;
            num4 = 600f;
            flag20 = DD2Event.EnemiesShouldChasePlayers;
            //if (!Main.expertMode)
            {
                flag20 = true;
            }
            if (npc.localAI[3] < 60f)
            {
                num20 = 0.01f + npc.localAI[3] / 60f * 0.05f;
            }
            if (npc.ai[0] == 0f)
            {
                float[] arg_1358_0 = npc.localAI;
                int arg_1358_1 = 1;
                SlotId invalid = SlotId.Invalid;
                arg_1358_0[arg_1358_1] = invalid.ToFloat();
            }
            if (npc.ai[0] == 1f)
            {
                npc.HitSound = SoundID.DD2_WitherBeastCrystalImpact;
                npc.ai[0] += 1f;
                if (Main.rand.Next(10) == 0)
                {
                    Dust expr_13CA = Dust.NewDustDirect(npc.TopLeft, npc.width, npc.height, 271, 0f, -3f, 0, Color.Transparent, 0.6f);
                    expr_13CA.velocity.X = expr_13CA.velocity.X / 2f;
                    expr_13CA.noGravity = true;
                    expr_13CA.fadeIn = 1.5f;
                    expr_13CA.position.Y = expr_13CA.position.Y + 4f;
                }
                ActiveSound activeSound = Main.GetActiveSound(SlotId.FromFloat(npc.localAI[1]));
                if (activeSound == null)
                {
                    npc.localAI[1] = Main.PlayTrackedSound(SoundID.DD2_WitherBeastAuraPulse, npc.Center).ToFloat();
                }
                else
                {
                    activeSound.Position = npc.Center;
                }
                npc.localAI[0] += 1f;
                if (npc.localAI[0] > 60f && Main.rand.Next(10) == 0)//aura circle
                {
                    Vector2 vector2 = npc.Center + (Main.rand.NextFloat() * 6.28318548f).ToRotationVector2() * 400f * (0.3f + 0.7f * Main.rand.NextFloat());
                    Point point = vector2.ToTileCoordinates();
                    if (!WorldGen.SolidTile(point.X, point.Y))
                    {
                        Dust dust3 = Dust.NewDustPerfect(vector2, 175, new Vector2(0f, -3f), 0, new Color(255, 255, 255, 127), 1.5f);
                        dust3.velocity = npc.DirectionTo(dust3.position) * dust3.velocity.Length();
                        dust3.fadeIn = 1.5f;
                        dust3.noGravity = true;
                    }
                }
                if (Main.netMode != 2)
                {
                    Player player = Main.player[Main.myPlayer];
                    if (!player.dead && player.active && (player.Center - npc.Center).Length() < 400f)
                    {
                        player.AddBuff(195, 3, false);
                    }
                }
                if (npc.ai[1] > 0f)
                {
                    npc.ai[1] -= 1f;
                }
                if (npc.ai[1] <= 0f)
                {
                    npc.ai[1] = 60f;
                    if (Main.netMode != 1)
                    {
                        /*int num31 = npc.lifeMax / 20;
                        if (num31 > npc.lifeMax - npc.life)
                        {
                            num31 = npc.lifeMax - npc.life;
                        }
                        if (num31 > 0)
                        {
                            npc.life += num31;
                            npc.HealEffect(num31, true);
                            npc.netUpdate = true;
                        }*/
                        for (int i = 0; i < 200; i++)
                        {
                            if (Main.npc[i].active && !Main.npc[i].friendly && Main.npc[i].damage > 0)
                            {
                                if (Vector2.Distance(Main.npc[i].Center, npc.Center) <= 400f)
                                {
                                    Main.npc[i].GetGlobalNPC<SinsNPC>().BlackBruteBuff = true;
                                    int num32 = Main.npc[i].lifeMax / 20;
                                    if (num32 > Main.npc[i].lifeMax - Main.npc[i].life)
                                    {
                                        num32 = Main.npc[i].lifeMax - Main.npc[i].life;
                                    }
                                    if (num32 > 0)
                                    {
                                        Main.npc[i].life += num32;
                                        Main.npc[i].HealEffect(num32, true);
                                        Main.npc[i].netUpdate = true;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            if (flag26)//not
            {
                bool flag29 = npc.velocity.Y == 0f;
                for (int num39 = 0; num39 < 200; num39++)
                {
                    if (num39 != npc.whoAmI && Main.npc[num39].active && Main.npc[num39].type == npc.type && Math.Abs(npc.position.X - Main.npc[num39].position.X) + Math.Abs(npc.position.Y - Main.npc[num39].position.Y) < (float)npc.width)
                    {
                        if (npc.position.X < Main.npc[num39].position.X)
                        {
                            npc.velocity.X = npc.velocity.X - num24;
                        }
                        else
                        {
                            npc.velocity.X = npc.velocity.X + num24;
                        }
                        if (npc.position.Y < Main.npc[num39].position.Y)
                        {
                            npc.velocity.Y = npc.velocity.Y - num24;
                        }
                        else
                        {
                            npc.velocity.Y = npc.velocity.Y + num24;
                        }
                    }
                }
                if (flag29)
                {
                    npc.velocity.Y = 0f;
                }
            }
            if (flag27)//spawn
            {
                if (npc.localAI[3] == 0f)
                {
                    npc.alpha = 255;
                }
                if (npc.localAI[3] == 30f)
                {
                    Main.PlayTrackedSound(SoundID.DD2_EtherianPortalSpawnEnemy, npc.Center);
                }
                if (npc.localAI[3] < 60f)
                {
                    npc.localAI[3] += 1f;
                    npc.alpha -= 5;
                    if (npc.alpha < 0)
                    {
                        npc.alpha = 0;
                    }
                    int num40 = (int)npc.localAI[3] / 10;
                    float num41 = npc.Size.Length() / 2f;
                    num41 /= 20f;
                    int maxValue3 = 5;
                    for (int num42 = 0; num42 < num40; num42++)
                    {
                        if (Main.rand.Next(maxValue3) == 0)
                        {
                            Dust expr_1F66 = Dust.NewDustDirect(npc.position, npc.width, npc.height, 175, npc.velocity.X * 1f, 0f, 100, default(Color), 2f);//1f
                            expr_1F66.scale = 0.55f;
                            expr_1F66.fadeIn = 0.7f;
                            expr_1F66.velocity *= 0.1f * num41;
                            expr_1F66.velocity += npc.velocity;
                            expr_1F66.noGravity = true;
                        }
                    }
                }
            }
            bool flag30 = false;
            if ((flag12 | flag5) && npc.ai[0] > 0f)
            {
                flag17 = false;
            }
            if (flag12 && npc.ai[1] > 0f)
            {
                flag21 = true;
            }
            if (flag5 && npc.ai[0] > 0f)
            {
                flag21 = true;
            }
            if (flag5)//use
            {
                if (npc.ai[0] < 0f)
                {
                    npc.ai[0] += 1f;
                    flag = false;
                }
                if (npc.ai[0] == 0f && (npc.velocity.Y == 0f | flag6) && targetData.Type != NPCTargetType.None && (Collision.CanHit(npc.position, npc.width, npc.height, targetData.Position, targetData.Width, targetData.Height) || Collision.CanHitLine(npc.position, npc.width, npc.height, targetData.Position, targetData.Width, targetData.Height)) && (targetData.Center - npc.Center).Length() < num4)
                {
                    npc.ai[0] = num5;
                    npc.netUpdate = true;
                }
                if (npc.ai[0] > 0f)
                {
                    npc.spriteDirection = npc.direction * num23;
                    if (flag7)
                    {
                        npc.velocity.X = npc.velocity.X * num7;
                        flag23 = true;
                        flag19 = true;
                        npc.ai[3] = 0f;
                    }
                    npc.ai[0] -= 1f;
                    if (npc.ai[0] == 0f)
                    {
                        npc.ai[0] = -num6;
                    }
                }
            }
            if (flag3 && npc.ai[0] > 0f)//not
            {
                if (flag15)
                {
                    NPCUtils.TargetClosestOldOnesInvasion(npc, true, null);
                    targetData = npc.GetTargetData(true);
                }
                if (npc.ai[0] == num9)
                {
                    Vector2 vector3 = npc.Center + zero;
                    Vector2 vector4 = targetData.Center - vector3;
                    vector4.Y -= Math.Abs(vector4.X) * num14;
                    Vector2 vector5 = vector4.SafeNormalize(-Vector2.UnitY) * scaleFactor;
                    for (int num43 = 0; num43 < num16; num43++)
                    {
                        Vector2 vector6 = vector5;
                        Vector2 vector7 = vector3;
                        if (vector.HasValue)
                        {
                            vector6 += vector.Value;
                        }
                        else
                        {
                            vector6 += Utils.RandomVector2(Main.rand, -num15, num15);
                        }
                        vector7 += vector5 * scaleFactor2;
                        if (Main.netMode != 1)
                        {
                            Projectile.NewProjectile(vector7, vector6, num11, num10, 0f, Main.myPlayer, 0f, 0f);
                        }
                    }
                }
            }
            if (flag4 && npc.ai[0] > 0f)
            {
                if (npc.velocity.Y != 0f && npc.ai[0] < num2)
                {
                    npc.ai[0] = num2;
                }
                if (npc.ai[0] == num)
                {
                    npc.velocity.Y = -num3;
                }
            }
            if (!flag16 & flag17)//use
            {
                if (npc.velocity.Y == 0f && npc.velocity.X * npc.direction < 0f)
                {
                    flag18 = true;
                }
                if ((npc.position.X == npc.oldPosition.X || npc.ai[3] >= num17) | flag18)
                {
                    npc.ai[3] += 1f;
                }
                else
                {
                    if (Math.Abs(npc.velocity.X) > 0.9f && npc.ai[3] > 0f)
                    {
                        npc.ai[3] -= 1f;
                    }
                }
                if (npc.ai[3] > num17 * 10)
                {
                    npc.ai[3] = 0f;
                }
                if (npc.justHit && !flag28)
                {
                    npc.ai[3] = 0f;
                }
                if (npc.ai[3] == num17)
                {
                    npc.netUpdate = true;
                    if (flag28)
                    {
                        npc.noGravity = true;
                        npc.noTileCollide = true;
                        npc.position.X = npc.position.X + npc.direction * npc.width * 2;
                        int num44 = 20;
                        float num45 = npc.Size.Length() / 2f;
                        num45 /= 20f;
                        int maxValue4 = 5;
                        for (int num46 = 0; num46 < num44; num46++)
                        {
                            if (Main.rand.Next(maxValue4) == 0)
                            {
                                /*Dust expr_24C5 = Dust.NewDustDirect(npc.position, npc.width, npc.height, 27, npc.velocity.X * 1f, 0f, 100, default(Color), 1f);
                                expr_24C5.scale = 0.55f;
                                expr_24C5.fadeIn = 0.7f;
                                expr_24C5.velocity *= 3f * num45;
                                expr_24C5.noGravity = true;
                                expr_24C5.fadeIn = 1.5f;
                                expr_24C5.velocity *= 3f;*/
                            }
                        }
                        return;
                    }
                }
            }
            if (!flag19)//use
            {
                if (npc.ai[3] < num17 & flag20)
                {
                    if (num18 > 0 && Main.rand.Next(num18) == 0)
                    {
                        Main.PlayTrackedSound(style, npc.Center);
                    }
                    NPCUtils.TargetClosestOldOnesInvasion(npc, true, null);
                    targetData = npc.GetTargetData(true);
                }
                else
                {
                    if (!flag21)
                    {
                        if (flag22 && npc.timeLeft > 10)
                        {
                            npc.timeLeft = 10;
                        }
                        if (npc.velocity.X == 0f)
                        {
                            if (npc.velocity.Y == 0f)
                            {
                                npc.ai[2] += 1f;
                                if (npc.ai[2] >= 2f)
                                {
                                    npc.direction *= -1;
                                    npc.spriteDirection = npc.direction * num23;
                                    npc.ai[2] = 0f;
                                }
                            }
                        }
                        else
                        {
                            npc.ai[2] = 0f;
                        }
                        if (npc.direction == 0)
                        {
                            npc.direction = 1;
                        }
                    }
                }
            }
            if (!flag23)
            {
                if (npc.velocity.X < -num19 || npc.velocity.X > num19)
                {
                    if (npc.velocity.Y == 0f)
                    {
                        npc.velocity *= num21;
                    }
                }
                else
                {
                    if ((npc.velocity.X < num19 && npc.direction == 1) || (npc.velocity.X > -num19 && npc.direction == -1))
                    {
                        npc.velocity.X = MathHelper.Clamp(npc.velocity.X + num20 * npc.direction, -num19, num19);
                    }
                }
            }
            if (flag12)//not
            {
                if (npc.confused)
                {
                    npc.ai[0] = 0f;
                }
                else
                {
                    if (npc.ai[1] > 0f)
                    {
                        npc.ai[1] -= 1f;
                    }
                    if (npc.justHit)
                    {
                        npc.ai[1] = 30f;
                        npc.ai[0] = 0f;
                    }
                    if (npc.ai[0] > 0f)
                    {
                        if (flag15)
                        {
                            NPCUtils.TargetClosestOldOnesInvasion(npc, true, null);
                            targetData = npc.GetTargetData(true);
                        }
                        if (npc.ai[1] == num9)
                        {
                            Vector2 vector8 = npc.Center + zero;
                            Vector2 vector9 = targetData.Center - vector8;
                            vector9.Y -= Math.Abs(vector9.X) * num14;
                            Vector2 vector10 = vector9.SafeNormalize(-Vector2.UnitY) * scaleFactor;
                            for (int num47 = 0; num47 < num16; num47++)
                            {
                                Vector2 vector11 = vector8;
                                Vector2 vector12 = vector10;
                                if (vector.HasValue)
                                {
                                    vector12 += vector.Value;
                                }
                                else
                                {
                                    vector12 += Utils.RandomVector2(Main.rand, -num15, num15);
                                }
                                vector11 += vector12 * scaleFactor2;
                                if (Main.netMode != 1)
                                {
                                    Projectile.NewProjectile(vector11, vector12, num11, num10, 0f, Main.myPlayer, 0f, 0f);
                                }
                            }
                            if (Math.Abs(vector10.Y) > Math.Abs(vector10.X) * 2f)
                            {
                                npc.ai[0] = (vector10.Y > 0f) ? 1 : 5;
                            }
                            else
                            {
                                if (Math.Abs(vector10.X) > Math.Abs(vector10.Y) * 2f)
                                {
                                    npc.ai[0] = 3f;
                                }
                                else
                                {
                                    npc.ai[0] = (vector10.Y > 0f) ? 2 : 4;
                                }
                            }
                        }
                        if ((npc.velocity.Y != 0f && !flag14) || npc.ai[1] <= 0f)
                        {
                            npc.ai[0] = 0f;
                            npc.ai[1] = 0f;
                        }
                        else
                        {
                            if (!flag13)
                            {
                                npc.velocity.X = npc.velocity.X * 0.9f;
                                npc.spriteDirection = npc.direction * num23;
                            }
                        }
                    }
                    if ((npc.ai[0] <= 0f | flag13) && (npc.velocity.Y == 0f | flag14) && npc.ai[1] <= 0f && targetData.Type != NPCTargetType.None && Collision.CanHit(npc.position, npc.width, npc.height, targetData.Position, targetData.Width, targetData.Height))
                    {
                        Vector2 vector13 = targetData.Center - npc.Center;
                        if (vector13.Length() < num12)
                        {
                            npc.netUpdate = true;
                            npc.velocity.X = npc.velocity.X * 0.5f;
                            npc.ai[0] = 3f;
                            npc.ai[1] = num8;
                            if (Math.Abs(vector13.Y) > Math.Abs(vector13.X) * 2f)
                            {
                                npc.ai[0] = (vector13.Y > 0f) ? 1 : 5;
                            }
                            else
                            {
                                if (Math.Abs(vector13.X) > Math.Abs(vector13.Y) * 2f)
                                {
                                    npc.ai[0] = 3f;
                                }
                                else
                                {
                                    npc.ai[0] = (vector13.Y > 0f) ? 2 : 4;
                                }
                            }
                        }
                    }
                    if (npc.ai[0] <= 0f | flag13)
                    {
                        bool flag31 = npc.Distance(targetData.Center) < num13;
                        if (flag31 && Collision.CanHitLine(npc.position, npc.width, npc.height, targetData.Position, targetData.Width, targetData.Height))
                        {
                            npc.ai[3] = 0f;
                        }
                        if ((npc.velocity.X < -num19 || npc.velocity.X > num19) | flag31)
                        {
                            if (npc.velocity.Y == 0f)
                            {
                                npc.velocity.X = npc.velocity.X * num21;
                            }
                        }
                        else
                        {
                            if ((npc.velocity.X < num19 && npc.direction == 1) || (npc.velocity.X > -num19 && npc.direction == -1))
                            {
                                npc.velocity.X = MathHelper.Clamp(npc.velocity.X + num20 * npc.direction, -num19, num19);
                            }
                        }
                    }
                }
            }
            if (npc.velocity.Y == 0f)
            {
                int num48 = (int)(npc.Bottom.Y + 7f) / 16;
                int arg_2C0D_0 = (int)npc.Left.X / 16;
                int num49 = (int)npc.Right.X / 16;
                for (int num50 = arg_2C0D_0; num50 <= num49; num50++)
                {
                    num50 = Utils.Clamp<int>(num50, 0, Main.maxTilesX);
                    num48 = Utils.Clamp<int>(num48, 0, Main.maxTilesY);
                    Tile tile = Main.tile[num50, num48];
                    if (tile == null)
                    {
                        return;
                    }
                    if (tile.nactive() && Main.tileSolid[tile.type])
                    {
                        flag30 = true;
                        break;
                    }
                }
            }
            Point point2 = npc.Center.ToTileCoordinates();
            if (WorldGen.InWorld(point2.X, point2.Y, 5) && !npc.noGravity)
            {
                Vector2 vector14;
                int width;
                int height;
                GetTileCollisionParameters(out vector14, out width, out height);
                Vector2 value = npc.position - vector14;
                Collision.StepUp(ref vector14, ref npc.velocity, width, height, ref npc.stepSpeed, ref npc.gfxOffY, 1, false, 0);
                npc.position = vector14 + value;
            }
            if (flag30)//use
            {
                int num51 = (int)(npc.Center.X + num22 * npc.direction) / 16;
                int num52 = ((int)npc.Bottom.Y - 15) / 16;
                bool flag32 = npc.position.Y + npc.height - num52 * 16 > 20f;
                Tile tileSafely = Framing.GetTileSafely(num51 + npc.direction, num52 + 1);
                Tile tileSafely2 = Framing.GetTileSafely(num51, num52 + 1);
                Tile tileSafely3 = Framing.GetTileSafely(num51, num52);
                Tile tileSafely4 = Framing.GetTileSafely(num51, num52 - 1);
                Tile tileSafely5 = Framing.GetTileSafely(num51, num52 - 2);
                Tile tileSafely6 = Framing.GetTileSafely(num51, num52 - 3);
                if (flag8 && tileSafely4.nactive() && (tileSafely4.type == 10 || tileSafely4.type == 388))
                {
                    npc.ai[0] += 1f;
                    npc.ai[3] = 0f;
                    if (npc.ai[0] >= 60f)
                    {
                        if (flag9)
                        {
                            npc.ai[1] = 0f;
                        }
                        int num53 = 5;
                        if (Main.tile[num51, num52 - 1].type == 388)
                        {
                            num53 = 2;
                        }
                        npc.velocity.X = 0.5f * -npc.direction;
                        npc.ai[1] += num53;
                        bool flag33 = false;
                        if (npc.ai[1] >= 10f)
                        {
                            flag33 = true;
                            npc.ai[1] = 10f;
                        }
                        if (flag10)
                        {
                            flag33 = true;
                        }
                        WorldGen.KillTile(num51, num52 - 1, true, false, false);
                        if (Main.netMode != 1 & flag33)
                        {
                            if (flag11)
                            {
                                WorldGen.KillTile(num51, num52 - 1, false, false, false);
                                if (Main.netMode == 2)
                                {
                                    NetMessage.SendData(17, -1, -1, null, 0, num51, num52 - 1, 0f, 0, 0, 0);
                                }
                            }
                            else
                            {
                                if (tileSafely4.type == 10)
                                {
                                    bool flag34 = WorldGen.OpenDoor(num51, num52 - 1, npc.direction);
                                    if (!flag34)
                                    {
                                        npc.ai[3] = num17;
                                        npc.netUpdate = true;
                                    }
                                    if (Main.netMode == 2 & flag34)
                                    {
                                        NetMessage.SendData(19, -1, -1, null, 0, num51, num52 - 1, npc.direction, 0, 0, 0);
                                    }
                                }
                                if (tileSafely4.type == 388)
                                {
                                    bool flag35 = WorldGen.ShiftTallGate(num51, num52 - 1, false);
                                    if (!flag35)
                                    {
                                        npc.ai[3] = num17;
                                        npc.netUpdate = true;
                                    }
                                    if (Main.netMode == 2 & flag35)
                                    {
                                        NetMessage.SendData(19, -1, -1, null, 4, num51, num52 - 1, npc.direction, 0, 0, 0);
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    int num54 = npc.spriteDirection * num23;
                    if (npc.velocity.X * num54 > 0f)
                    {
                        if (npc.height >= 32 && tileSafely5.nactive() && Main.tileSolid[tileSafely5.type])
                        {
                            npc.netUpdate = true;
                            npc.velocity.Y = -7f;
                            if (tileSafely6.nactive() && Main.tileSolid[tileSafely6.type])
                            {
                                npc.velocity.Y = -8f;
                            }
                        }
                        else
                        {
                            if (tileSafely4.nactive() && Main.tileSolid[tileSafely4.type])
                            {
                                npc.velocity.Y = -6f;
                                npc.netUpdate = true;
                            }
                            else
                            {
                                if (flag32 && tileSafely3.nactive() && !tileSafely3.topSlope() && Main.tileSolid[tileSafely3.type])
                                {
                                    npc.velocity.Y = -5f;
                                    npc.netUpdate = true;
                                }
                                else
                                {
                                    if (flag24 && (!tileSafely2.nactive() || !Main.tileSolid[tileSafely2.type]) && (!tileSafely.nactive() || !Main.tileSolid[tileSafely.type]))
                                    {
                                        npc.velocity.X = npc.velocity.X * 1.5f;
                                        npc.velocity.Y = -8f;
                                        npc.netUpdate = true;
                                    }
                                    else
                                    {
                                        if (flag8)
                                        {
                                            npc.ai[0] = 0f;
                                            npc.ai[1] = 0f;
                                        }
                                    }
                                }
                            }
                        }
                        if ((npc.velocity.Y == 0f & flag) && npc.ai[3] == 1f)
                        {
                            npc.velocity.Y = -5f;
                            npc.netUpdate = true;
                        }
                    }
                    if (flag25 && npc.velocity.Y == 0f && Math.Abs(targetData.Center.X - npc.Center.X) < 100f && Math.Abs(targetData.Center.Y - npc.Center.Y) < 50f && Math.Abs(npc.velocity.X) >= 1f && npc.velocity.X * npc.direction > 0f)
                    {
                        npc.velocity.X = MathHelper.Clamp(npc.velocity.X * 2f, -3f, 3f);
                        npc.velocity.Y = -4f;
                        npc.netAlways = true;
                    }
                }
            }
            else
            {
                if (flag8)
                {
                    npc.ai[0] = 0f;
                    npc.ai[1] = 0f;
                }
            }
            if (flag2 && npc.noTileCollide)
            {
                if (flag27)
                {
                    if (npc.alpha < 60)
                    {
                        npc.alpha += 20;
                    }
                    npc.localAI[3] = 40f;
                }
                bool arg_34E6_0 = npc.velocity.Y == 0f;
                if (Math.Abs(npc.Center.X - targetData.Center.X) > 200f)
                {
                    npc.spriteDirection = npc.direction = (targetData.Center.X > npc.Center.X) ? 1 : -1;
                    npc.velocity.X = MathHelper.Lerp(npc.velocity.X, npc.direction, 0.05f);
                }
                int num55 = 80;
                int height2 = npc.height;
                Vector2 position2 = new Vector2(npc.Center.X - (num55 / 2), npc.position.Y + npc.height - height2);
                bool flag36 = false;
                if (npc.position.Y + npc.height < targetData.Position.Y + targetData.Height - 16f)
                {
                    flag36 = true;
                }
                if (flag36)
                {
                    npc.velocity.Y = npc.velocity.Y + 0.5f;
                }
                else
                {
                    if (Collision.SolidCollision(position2, num55, height2) || targetData.Center.Y - npc.Center.Y < -100f)
                    {
                        if (npc.velocity.Y > 0f)
                        {
                            npc.velocity.Y = 0f;
                        }
                        if (npc.velocity.Y > -0.2)
                        {
                            npc.velocity.Y = npc.velocity.Y - 0.025f;
                        }
                        else
                        {
                            npc.velocity.Y = npc.velocity.Y - 0.2f;
                        }
                        if (npc.velocity.Y < -4f)
                        {
                            npc.velocity.Y = -4f;
                        }
                    }
                    else
                    {
                        if (npc.velocity.Y < 0f)
                        {
                            npc.velocity.Y = 0f;
                        }
                        if (npc.velocity.Y < 0.1)
                        {
                            npc.velocity.Y = npc.velocity.Y + 0.025f;
                        }
                        else
                        {
                            npc.velocity.Y = npc.velocity.Y + 0.5f;
                        }
                    }
                }
                if (npc.velocity.Y > 10f)
                {
                    npc.velocity.Y = 10f;
                }
                if (arg_34E6_0)
                {
                    npc.velocity.Y = 0f;
                }
            }
        }
        private void GetTileCollisionParameters(out Vector2 cPosition, out int cWidth, out int cHeight)
        {
            cPosition = npc.position;
            cWidth = npc.width;
            cHeight = npc.height;
            if (cHeight != npc.height)
            {
                cPosition.Y += npc.height - cHeight;
            }
        }
    }
}