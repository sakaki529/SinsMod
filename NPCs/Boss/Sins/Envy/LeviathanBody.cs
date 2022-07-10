using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.NPCs.Boss.Sins.Envy
{
    public class LeviathanBody : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Leviathan");
            NPCID.Sets.TrailingMode[npc.type] = 1;
            NPCID.Sets.TrailCacheLength[npc.type] = 3;
        }
        public override void SetDefaults()
        {
            npc.width = 28;
            npc.height = 28;
            npc.lifeMax = 120000;
            npc.damage = 76;
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
            npc.npcSlots = 1f;
            npc.value = Item.buyPrice(0, 0, 0, 0);
            npc.dontCountMe = true;
            npc.chaseable = false;
            for (int i = 0; i < npc.buffImmune.Length; i++)
            {
                npc.buffImmune[i] = true;
                npc.buffImmune[BuffID.Ichor] = false;
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
            npc.damage = 100;
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            Texture2D texture = Main.npcTexture[npc.type];
            Texture2D glowMask = mod.GetTexture("Glow/NPC/LeviathanBody_Glow");
            Color glowColor = Color.Lerp(drawColor, Color.White, (float)Math.Cos(6.28318548f * (Main.LocalPlayer.miscCounter / 100f)) * 0.5f + 0.75f/*0.75 = white*/);
            SpriteEffects spriteEffects = (npc.spriteDirection == 1) ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            spriteBatch.Draw(texture, npc.Center - Main.screenPosition, new Rectangle?(npc.frame), npc.GetAlpha(drawColor), npc.rotation, npc.frame.Size() / 2f, npc.scale, spriteEffects, 0f);
            spriteBatch.Draw(glowMask, npc.Center - Main.screenPosition, new Rectangle?(npc.frame), npc.GetAlpha(glowColor), npc.rotation, npc.frame.Size() / 2f, npc.scale, spriteEffects, 0f);
            for (int i = 0; i < NPCID.Sets.TrailCacheLength[npc.type]; i++)
            {
                Vector2 vector = new Vector2(Main.npcTexture[npc.type].Width / 2, Main.npcTexture[npc.type].Height / Main.npcFrameCount[npc.type] / 2);
                Color color = npc.GetAlpha(glowColor);
                color.R = (byte)(color.R * (10 - i) / 20);
                color.G = (byte)(color.G * (10 - i) / 20);
                color.B = (byte)(color.B * (10 - i) / 20);
                color.A = (byte)(color.A * (10 - i) / 20);
                Main.spriteBatch.Draw(glowMask, new Vector2(npc.oldPos[i].X - Main.screenPosition.X + npc.width / 2 * npc.scale, npc.oldPos[i].Y - Main.screenPosition.Y + npc.height / 2 * npc.scale), new Microsoft.Xna.Framework.Rectangle?(npc.frame), color, npc.rotation, vector, npc.scale, spriteEffects, 0f);
            }
            return false;
        }
        public override void ModifyHitByProjectile(Projectile projectile, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if (projectile.numHits > 4 && !projectile.minion && !projectile.sentry && projectile.minionSlots > 0)
            {
                damage /= projectile.numHits / 4;
            }
        }
        public override void AI()
        {
            Player player = Main.player[npc.target];
            Vector2 chasePosition = Main.npc[(int)npc.ai[1]].Center;
            Vector2 directionVector = chasePosition - npc.Center;
            npc.spriteDirection = (directionVector.X > 0f) ? 1 : -1;
            if (npc.ai[3] > 0)
            {
                npc.realLife = (int)npc.ai[3];
            }
            if (npc.target < 0 || npc.target == byte.MaxValue || Main.player[npc.target].dead)
            {
                npc.TargetClosest(true);
            }
            npc.alpha -= 12;
            if (npc.alpha < 0)
            {
                npc.alpha = 0;
            }
            if (Main.netMode != 1)
            {
                if (!Main.npc[(int)npc.ai[1]].active || Main.npc[(int)npc.ai[2]].type != mod.NPCType("LeviathanHead"))
                {
                    npc.life = 0;
                    if (Main.npc[(int)npc.ai[3]].localAI[0] != 255)
                    {
                        npc.HitEffect(0, 1);
                    }
                    npc.active = false;
                    NetMessage.SendData(28, -1, -1, null, npc.whoAmI, -1f, 0.0f, 0.0f, 0, 0, 0);
                }
            }
            if (npc.ai[1] < (double)Main.npc.Length)
            {
                Vector2 npcCenter = new Vector2(npc.position.X + npc.width * 0.5f, npc.position.Y + npc.height * 0.5f);
                float dirX = Main.npc[(int)npc.ai[1]].position.X + Main.npc[(int)npc.ai[1]].width / 2 - npcCenter.X;
                float dirY = Main.npc[(int)npc.ai[1]].position.Y + Main.npc[(int)npc.ai[1]].height / 2 - npcCenter.Y;
                npc.rotation = (float)Math.Atan2(dirY, dirX) + 1.57f;
                float length = (float)Math.Sqrt(dirX * dirX + dirY * dirY);
                float dist = (length - npc.width) / length;
                float posX = dirX * dist;
                float posY = dirY * dist;
                npc.spriteDirection = dirX < 0f ? 1 : -1;
                npc.velocity = Vector2.Zero;
                npc.position.X = npc.position.X + posX;
                npc.position.Y = npc.position.Y + posY;
            }
            npc.netUpdate = true;
            if (npc.life < npc.lifeMax / 2)
            {
                if (Main.npc[(int)npc.ai[2]].ai[0] < 0)
                {
                    Projectile.NewProjectile(npc.Center, Vector2.Zero, mod.ProjectileType("ElectricalDischarge"), 120, 0, Main.myPlayer, 0f, 0f);
                }
            }
            if (!player.dead)
            {
                npc.localAI[0] += 1f;
                if (Main.expertMode && npc.life < npc.lifeMax * 0.8)
                {
                    npc.localAI[0] += 0.6f;
                }
            }
            if (npc.localAI[0] >= 180f && Main.rand.Next(npc.life < npc.lifeMax * 0.75 ? 1600 : 1000) == 0)
            {
                npc.ai[3] = 0f;
                Vector2 position = npc.Center;
                Vector2 vector = player.Center - npc.Center;
                if (Main.netMode != 1)
                {
                    float vel = 10f;
                    int dmg = 40;
                    int type = ProjectileID.CursedFlameHostile;
                    if (Main.expertMode)
                    {
                        vel = 14;
                        dmg = 30;
                    }
                    float dist = (float)Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
                    dist = vel / dist;
                    vector *= dist;
                    vector.X += Main.rand.Next(-40, 41) * 0.05f;
                    vector.Y += Main.rand.Next(-40, 41) * 0.05f;
                    position += vector * 4f;
                    int proj = Projectile.NewProjectile(position.X, position.Y, vector.X, vector.Y, type, dmg, 0f, Main.myPlayer, 0f, 0f);
                }
            }
        }
        public override bool CheckActive()
        {
            return false;
        }
        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            return false;
        }
        public override bool PreNPCLoot()
        {
            return false;
        }
    }
}