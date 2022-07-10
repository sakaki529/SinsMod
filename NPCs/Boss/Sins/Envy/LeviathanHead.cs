using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.NPCs.Boss.Sins.Envy
{
    [AutoloadBossHead]
    public class LeviathanHead : ModNPC
    {
        private bool FirstPhase = true;
        private bool Esc;
        private int EscTimer;
        private bool Spawned;
        private bool canFire;
        public override string BossHeadTexture => "SinsMod/NPCs/Boss/Sins/Envy/Envy_Head_Boss";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Leviathan");
            Main.npcFrameCount[npc.type] = 3;
            NPCID.Sets.TrailingMode[npc.type] = 1;
            NPCID.Sets.TrailCacheLength[npc.type] = 4;
        }
        public override void SetDefaults()
        {
            npc.width = 32;
            npc.height = 28;
            npc.lifeMax = 80000;
            npc.damage = 100;
            npc.defense = 20;
            npc.knockBackResist = 0f;
            npc.aiStyle = -1;
            aiType = -1;
            npc.boss = true;
            npc.lavaImmune = true;
            npc.noGravity = true; 
            npc.noTileCollide = true;
            npc.netAlways = true;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.alpha = 255;
            npc.npcSlots = 5f;
            npc.value = Item.buyPrice(0, 0, 0, 0);
            for (int i = 0; i < npc.buffImmune.Length; i++)
            {
                npc.buffImmune[i] = true;
                npc.buffImmune[BuffID.Ichor] = false;
                npc.buffImmune[mod.BuffType("Chroma")] = false;
            }
            if (SinsMod.Instance.SinsMusicLoaded)
            {
                Mod mod = ModLoader.GetMod("SinsModMusic");
            }
            else
            {
                music = MusicID.Boss3;
            }
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = (int)(npc.lifeMax * 0.8f * bossLifeScale);
            npc.damage = 150;
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            Texture2D texture = Main.npcTexture[npc.type];
            Texture2D glowMask = mod.GetTexture("Glow/NPC/LeviathanHead_Glow");
            Color glowColor = Color.Lerp(drawColor, Color.White, (float)Math.Cos(6.28318548f * (Main.LocalPlayer.miscCounter / 100f)) * 0.5f + 0.75f);
            SpriteEffects spriteEffects = (npc.spriteDirection == 1) ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            spriteBatch.Draw(texture, npc.Center - Main.screenPosition, new Rectangle?(npc.frame), npc.GetAlpha(drawColor), npc.rotation, npc.frame.Size() / 2f, npc.scale, spriteEffects, 0f);
            spriteBatch.Draw(glowMask, npc.Center - Main.screenPosition, new Rectangle?(npc.frame), npc.GetAlpha(glowColor), npc.rotation, npc.frame.Size() / 2f, npc.scale, spriteEffects, 0f);
            return false;
        }
        public override void FindFrame(int frameHeight)
        {
            npc.frameCounter++;
            if (Main.netMode != 1 && canFire)
            {
                if (npc.frameCounter >= 4)
                {
                    npc.frameCounter = 0;
                    if (npc.frame.Y < frameHeight * 2)
                    {
                        npc.frame.Y = npc.frame.Y + frameHeight;
                    }
                }
            }
            else
            {
                if (npc.frameCounter >= 4)
                {
                    npc.frameCounter = 0;
                    if (npc.frame.Y > 0)
                    {
                        npc.frame.Y = npc.frame.Y - frameHeight;
                    }
                }
            }
            if (npc.frame.Y / frameHeight >= Main.npcFrameCount[npc.type])
            {
                npc.frame.Y = 0;
            }
        }
        public override void AI()
        {
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
                npc.localAI[0] = 255;
                npc.velocity.Y++;
                EscTimer++;
                if (EscTimer > 120)
                {
                    npc.active = false;
                }
            }
            npc.alpha -= 12;
            if (npc.alpha < 0)
            {
                npc.alpha = 0;
            }
            if (npc.ai[3] > 0f)
            {
                npc.realLife = (int)npc.ai[3];
            }
            if (Main.netMode != 1)
            {
                if (!Spawned)
                {
                    int num = npc.whoAmI;
                    for (int i = 0; i < 50; i++)
                    {
                        int num2;
                        if (i < 48)
                        {
                            num2 = NPC.NewNPC((int)npc.position.X + npc.width / 2, (int)npc.position.Y + npc.height / 2, mod.NPCType("LeviathanBody"), npc.whoAmI, 0f, 0f, 0f, 0f, 255);
                        }
                        else if (i < 49)
                        {
                            num2 = NPC.NewNPC((int)npc.position.X + npc.width / 2, (int)npc.position.Y + npc.height / 2, mod.NPCType("LeviathanBody2"), npc.whoAmI, 0f, 0f, 0f, 0f, 255);
                        }
                        else
                        {
                            num2 = NPC.NewNPC((int)npc.position.X + npc.width / 2, (int)npc.position.Y + npc.height / 2, mod.NPCType("LeviathanTail"), npc.whoAmI, 0f, 0f, 0f, 0f, 255);
                        }
                        Main.npc[num2].realLife = npc.whoAmI;
                        Main.npc[num2].ai[2] = npc.whoAmI;
                        Main.npc[num2].ai[1] = num;
                        Main.npc[num].ai[0] = num2;
                        num = num2;
                    }
                    Spawned = true;
                }
                if (!npc.active && Main.netMode == 2)
                {
                    NetMessage.SendData(28, -1, -1, null, npc.whoAmI, -1f, 0f, 0f, 0, 0, 0);
                }
            }
            if (npc.life < npc.lifeMax / 2)
            {
                npc.ai[0]++;
                if (npc.ai[0] > 300 && Main.rand.Next(300) == 0)
                {
                    npc.ai[0] = -1;
                }
                if (npc.ai[0] < 0)
                {
                    Projectile.NewProjectile(npc.Center, Vector2.Zero, mod.ProjectileType("ElectricalDischarge"), 120, 0, Main.myPlayer, 1f, 0f);
                }
            }
            if (FirstPhase)
            {
                canFire = Vector2.Distance(player.Center, npc.Center) < 500f;
                if (npc.life < npc.lifeMax * 0.75f)
                {
                    if (Collision.CanHit(npc.position, npc.width, npc.height, player.position, player.width, player.height))
                    {
                        npc.localAI[2] += 1f;
                        if (npc.localAI[2] > 22f)
                        {
                            npc.localAI[2] = 0f;
                            Main.PlaySound(SoundID.Item34, npc.position);
                        }
                        if (Main.netMode != 1 && canFire)
                        {
                            npc.localAI[1] += 1f;
                            if (npc.life < npc.lifeMax * 0.75)
                            {
                                npc.localAI[1] += 1f;
                            }
                            if (npc.life < npc.lifeMax * 0.5)
                            {
                                npc.localAI[1] += 1f;
                            }
                            if (npc.life < npc.lifeMax * 0.25)
                            {
                                npc.localAI[1] += 1f;
                            }
                            if (npc.life < npc.lifeMax * 0.1)
                            {
                                npc.localAI[1] += 2f;
                            }
                            if (npc.localAI[1] > 8f)
                            {
                                npc.localAI[1] = 0f;
                                float vel = 6f;
                                int dmg = 30;
                                if (Main.expertMode)
                                {
                                    dmg = 27;
                                }
                                int type = ProjectileID.EyeFire;
                                Vector2 position = npc.Center;
                                Vector2 vector = player.Center - npc.Center;
                                float dist = (float)Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
                                dist = vel / dist;
                                vector *= dist;
                                vector.X += Main.rand.Next(-40, 41) * 0.01f;
                                vector.Y += Main.rand.Next(-40, 41) * 0.01f;
                                vector += npc.velocity * 0.5f;
                                position -= vector * 1f;
                                int proj = Projectile.NewProjectile(position.X, position.Y, vector.X, vector.Y, type, dmg, 0f, Main.myPlayer, 0f, 0f);
                            }
                        }
                    }
                }
            }
            //npc.velocity.Length();
            float speed = 16f;
            float acceleration = 0.2f;
            if (!player.wet && Main.expertMode)
            {
                speed = 24f;
                acceleration = 0.35f;
            }
            Vector2 npcCenter = new Vector2(npc.position.X + npc.width * 0.5f, npc.position.Y + npc.height * 0.5f);
            float targetXPos = Main.player[npc.target].position.X + (Main.player[npc.target].width / 2);
            float targetYPos = Main.player[npc.target].position.Y + (Main.player[npc.target].height / 2);
            float targetRoundedPosX = (int)(targetXPos / 16.0) * 16;
            float targetRoundedPosY = (int)(targetYPos / 16.0) * 16;
            npcCenter.X = (int)(npcCenter.X / 16.0) * 16;
            npcCenter.Y = (int)(npcCenter.Y / 16.0) * 16;
            float dirX = targetRoundedPosX - npcCenter.X;
            float dirY = targetRoundedPosY - npcCenter.Y;
            float length = (float)Math.Sqrt(dirX * dirX + dirY * dirY);
            float absDirX = Math.Abs(dirX);
            float absDirY = Math.Abs(dirY);
            float newSpeed = speed / length;
            dirX *= newSpeed;
            dirY *= newSpeed;
            if (npc.velocity.X > 0.0 && dirX > 0.0 || npc.velocity.X < 0.0 && dirX < 0.0 || npc.velocity.Y > 0.0 && dirY > 0.0 || npc.velocity.Y < 0.0 && dirY < 0.0)
            {
                if (npc.velocity.X < dirX)
                {
                    npc.velocity.X = npc.velocity.X + acceleration;
                }
                else if (npc.velocity.X > dirX)
                {
                    npc.velocity.X = npc.velocity.X - acceleration;
                }
                if (npc.velocity.Y < dirY)
                {
                    npc.velocity.Y = npc.velocity.Y + acceleration;
                }
                else if (npc.velocity.Y > dirY)
                {
                    npc.velocity.Y = npc.velocity.Y - acceleration;
                }
                if (Math.Abs(dirY) < speed * 0.2 && (npc.velocity.X > 0.0 && dirX < 0.0 || npc.velocity.X < 0.0 && dirX > 0.0))
                {
                    if (npc.velocity.Y > 0.0)
                    {
                        npc.velocity.Y = npc.velocity.Y + acceleration * 2f;
                    }
                    else
                    {
                        npc.velocity.Y = npc.velocity.Y - acceleration * 2f;
                    }
                }
                if (Math.Abs(dirX) < speed * 0.2 && (npc.velocity.Y > 0.0 && dirY < 0.0 || npc.velocity.Y < 0.0 && dirY > 0.0))
                {
                    if (npc.velocity.X > 0.0)
                    {
                        npc.velocity.X = npc.velocity.X + acceleration * 2f;
                    }
                    else
                    {
                        npc.velocity.X = npc.velocity.X - acceleration * 2f;
                    }
                }
            }
            else if (absDirX > absDirY)
            {
                if (npc.velocity.X < dirX)
                {
                    npc.velocity.X = npc.velocity.X + acceleration * 1.1f;
                }
                else if (npc.velocity.X > dirX)
                {
                    npc.velocity.X = npc.velocity.X - acceleration * 1.1f;
                }
                if (Math.Abs(npc.velocity.X) + Math.Abs(npc.velocity.Y) < speed * 0.5)
                {
                    if (npc.velocity.Y > 0.0)
                    {
                        npc.velocity.Y = npc.velocity.Y + acceleration;
                    }
                    else
                    {
                        npc.velocity.Y = npc.velocity.Y - acceleration;
                    }
                }
            }
            else
            {
                if (npc.velocity.Y < dirY)
                {
                    npc.velocity.Y = npc.velocity.Y + acceleration * 1.1f;
                }
                else if (npc.velocity.Y > dirY)
                {
                    npc.velocity.Y = npc.velocity.Y - acceleration * 1.1f;
                }
                if (Math.Abs(npc.velocity.X) + Math.Abs(npc.velocity.Y) < speed * 0.5)
                {
                    if (npc.velocity.X > 0.0)
                    {
                        npc.velocity.X = npc.velocity.X + acceleration;
                    }
                    else
                    {
                        npc.velocity.X = npc.velocity.X - acceleration;
                    }
                }
            }
            npc.rotation = (float)Math.Atan2(npc.velocity.Y, npc.velocity.X) + 1.57f;
            npc.spriteDirection = npc.velocity.X < 0f ? 1 : -1;
        }
        public override bool CheckDead()
        {
            NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y + 48, mod.NPCType("Envy"), npc.whoAmI);
            return true;
        }
        public override bool CheckActive()
        {
            return false;
        }
        public override bool PreNPCLoot()
        {
            Player player = Main.player[npc.target];
            float num = 1E+08f;
            Vector2 position = npc.position;
            for (int i = 0; i < 200; i++)
            {
                if (Main.npc[i].active && (Main.npc[i].type == mod.NPCType("LeviathanHead") || Main.npc[i].type == mod.NPCType("LeviathanBody") || Main.npc[i].type == mod.NPCType("LeviathanBody2") || Main.npc[i].type == mod.NPCType("LeviathanTail")))
                {
                    float num2 = Math.Abs(Main.npc[i].Center.X - player.Center.X) + Math.Abs(Main.npc[i].Center.Y - player.Center.Y);
                    if (num2 < num)
                    {
                        num = num2;
                        position = Main.npc[i].position;
                    }
                }
            }
            return false;
        }
    }
}