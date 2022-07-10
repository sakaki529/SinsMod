using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.NPCs.Boss.Sins.Wrath
{
    [AutoloadBossHead]
    public class Wrath : ModNPC
    {
        internal const int size = 120;
        internal const int particleSize = 12;
        internal IList<Particle> particles = new List<Particle>();
        internal float[,] aura = new float[size, size];
        private bool FirstPhase = true;
        private bool SecondPhase;
        private bool ThirdPhase;
        private bool Esc;
        private int EscTimer;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sin of Wrath");
            NPCID.Sets.TrailingMode[npc.type] = 1;
        }
        public override void SetDefaults()
        {
            npc.width = 96;
            npc.height = 96;
            npc.lifeMax = 280000;
            npc.damage = 300;
            npc.defense = 0;
            npc.knockBackResist = 0f;
            npc.aiStyle = -1;
            aiType = -1;
            npc.boss = true;
            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.HitSound = SoundID.NPCHit54;
            npc.DeathSound = SoundID.NPCDeath62;
            npc.npcSlots = 1f;
            npc.netAlways = true;
            npc.value = Item.buyPrice(0, 90, 0, 0);
            bossBag = mod.ItemType("WrathBag");
            for (int i = 0; i < npc.buffImmune.Length; i++)
            {
                npc.buffImmune[i] = true;
                npc.buffImmune[BuffID.Ichor] = false;
                npc.buffImmune[mod.BuffType("Nightmare")] = false;
                npc.buffImmune[mod.BuffType("Chroma")] = false;
                npc.buffImmune[mod.BuffType("SuperSlow")] = false;
            }
            if (SinsMod.Instance.SinsMusicLoaded)
            {
                Mod mod = ModLoader.GetMod("SinsModMusic");
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
        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            if (npc.ai[3] == 0f)
            {
                for (int x = 0; x < size; x++)
                {
                    for (int y = 0; y < size; y++)
                    {
                        Vector2 drawPos = npc.Center - new Vector2(size / 2, size / 2) - Main.screenPosition;
                        drawPos.X += x * 2 - size / 2;
                        drawPos.Y += y * 2 - size / 2;
                        spriteBatch.Draw(mod.GetTexture("NPCs/Boss/Sins/Wrath/Particle"), drawPos, null, npc.GetAlpha(Color.White) * aura[x, y], 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
                    }
                }
            }
            else
            {
                Main.spriteBatch.End();
                Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.Default, RasterizerState.CullNone, null, Main.GameViewMatrix.ZoomMatrix);
                MiscShaderData deathShader = GameShaders.Misc["SinsMod:DeathAnimation"];
                deathShader.UseOpacity(1f);
                if (npc.ai[3] > 30f)
                {
                    deathShader.UseOpacity(1f - (npc.ai[3] - 30f) / 150f);
                }
                if (npc.ai[3] != 0f)
                {
                    deathShader.Apply(null);
                }
            }
            Texture2D texture = Main.npcTexture[npc.type];
            SpriteEffects spriteEffects = (npc.spriteDirection == 1) ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            Main.spriteBatch.Draw(texture, npc.Center - Main.screenPosition, npc.frame, npc.GetAlpha(Color.White), npc.rotation, npc.frame.Size() / 2f, npc.scale, spriteEffects, 0f);
            return false;
        }
        public override void PostDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.TransformationMatrix);
        }
        public override bool PreAI()
        {
            if (!Main.dedServ)
            {
                Particle.UpdateParticles(size, particleSize, aura, particles);
            }
            if (npc.ai[3] > 0f)
            {
                ResetEffects();
                for (int i = 0; i < npc.buffImmune.Length; i++)
                {
                    npc.buffImmune[i] = true;
                }
                if (npc.rotation != 0f)
                {
                    npc.rotation *= 0.95f;
                    if (npc.rotation < 0.05f || npc.rotation > -0.05f)
                    {
                        npc.rotation = 0f;
                    }
                }
                npc.dontTakeDamage = true;
                if (npc.ai[3] == 1f)
                {
                    Main.PlaySound(npc.DeathSound, npc.Center);
                }
                npc.ai[3] += 1f;
                //npc.velocity = Vector2.UnitY * npc.velocity.Length();
                npc.velocity.X *= 0.95f;
                npc.velocity.Y *= 0.95f;
                if (npc.velocity.X < 0.5f && npc.velocity.Y < 0.5f)
                {
                    npc.velocity = Vector2.Zero;
                }
                if (npc.ai[3] > 120f)
                {
                    //npc.Opacity = 1f - (npc.ai[3] - 120f) / 60f;
                }
                if (Main.rand.NextBool(5) && npc.ai[3] < 120f)
                {
                    for (int dustNumber = 0; dustNumber < 3; dustNumber++)
                    {
                        Dust dust = Main.dust[Dust.NewDust(npc.Left, npc.width, npc.height / 2, 242, 0f, 0f, 0, SinsColor.Wrath, 1f)];
                        dust.position = npc.Center + Vector2.UnitY.RotatedByRandom(4.1887903213500977) * new Vector2(npc.width * 1.5f, npc.height * 1.1f) * 0.8f * (0.8f + Main.rand.NextFloat() * 0.2f);
                        dust.velocity.X = 0f;
                        dust.velocity.Y = -Math.Abs(dust.velocity.Y - dustNumber + npc.velocity.Y - 4f) * 3f;
                        dust.noGravity = true;
                        dust.fadeIn = 1f;
                        dust.scale = 1f + Main.rand.NextFloat() + dustNumber * 0.3f;
                    }
                }
                if (npc.ai[3] >= 180f)
                {
                    npc.life = 0;
                    npc.HitEffect(0, 0);
                    npc.checkDead();
                }
                return false;
            }
            return base.PreAI();
        }
        public override void AI()
        {
            bool LC = SinsWorld.LimitCut;
            Player player = Main.player[npc.target];
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
                if (EscTimer > 120)
                {
                    npc.active = false;
                }
                return;
            }
            if (npc.life <= npc.lifeMax / 2)
            {
                SecondPhase = true;
            }
            if (npc.life <= npc.lifeMax / 5)
            {
                ThirdPhase = true;
            }
            if (FirstPhase == true)
            {
                npc.ai[0]++;
                if (npc.ai[0] >= 30)
                {
                    if (Main.rand.Next(Main.expertMode ? (SecondPhase ? (ThirdPhase ? 80 : 60) : 40) : (SecondPhase ? 80 : 60)) == 0)
                    {
                        Vector2 vector = player.Center - npc.Center;
                        vector.Normalize();
                        vector *= Main.rand.NextFloat(2f, 6f);
                        Projectile.NewProjectile(npc.Center.X, npc.Center.Y, vector.X, vector.Y, mod.ProjectileType("WrathBall"), Main.expertMode ? 80 : 120, 0f, Main.myPlayer, npc.target, 0f);
                        npc.ai[0] = 0;
                    }
                }
                if (SecondPhase)
                {
                    npc.ai[1]++;
                    if (npc.ai[1] >= (Main.expertMode ? (ThirdPhase ? 60 : 90) : (ThirdPhase ? 90 : 120)))
                    {
                        Vector2 vector = player.Center - npc.Center;
                        vector.Normalize();
                        vector *= Main.rand.NextFloat(2f, 6f);
                        Projectile.NewProjectile(npc.Center.X, npc.Center.Y, vector.X, vector.Y, mod.ProjectileType("WrathBall"), Main.expertMode ? 90 : 140, 0f, Main.myPlayer, npc.target, 0f);
                        npc.ai[1] = 0;
                    }
                }
                int minTilePosX = (int)(npc.position.X / 16.0) - 1;
                int maxTilePosX = (int)((npc.position.X + npc.width) / 16.0) + 10;
                int minTilePosY = (int)(npc.position.Y / 16.0) - 1;
                int maxTilePosY = (int)((npc.position.Y + npc.height) / 16.0) + 10;
                if (minTilePosX < 0)
                {
                    minTilePosX = 0;
                }
                if (maxTilePosX > Main.maxTilesX)
                {
                    maxTilePosX = Main.maxTilesX;
                }
                if (minTilePosY < 0)
                {
                    minTilePosY = 0;
                }
                if (maxTilePosY > Main.maxTilesY)
                {
                    maxTilePosY = Main.maxTilesY;
                }
                bool collision = false;
                // This is the initial check for collision with tiles.
                for (int i = minTilePosX; i < maxTilePosX; ++i)
                {
                    for (int j = minTilePosY; j < maxTilePosY; ++j)
                    {
                        if (Main.tile[i, j] != null && (Main.tile[i, j].nactive() && (Main.tileSolid[Main.tile[i, j].type] || Main.tileSolidTop[Main.tile[i, j].type] && Main.tile[i, j].frameY == 0) || Main.tile[i, j].liquid > 64))
                        {
                            Vector2 vector2;
                            vector2.X = i * 50;
                            vector2.Y = j * 50;
                            if (npc.position.X + npc.width > vector2.X && npc.position.X < vector2.X + 16.0 && (npc.position.Y + npc.height > (double)vector2.Y && npc.position.Y < vector2.Y + 16.0))
                            {
                                collision = true;
                                if (Main.rand.Next(100) == 0 && Main.tile[i, j].nactive())
                                {
                                    //WorldGen.KillTile(i, j, true, true, false);
                                }
                            }
                        }
                    }
                }
                // If there is no collision with tiles, we check if the distance between this NPC and its target is too large, so that we can still trigger 'collision'.
                if (!collision)
                {
                    Rectangle rectangle1 = new Rectangle((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height);
                    int maxDistance = 600;
                    bool playerCollision = true;
                    for (int index = 0; index < 255; ++index)
                    {
                        if (Main.player[index].active)
                        {
                            Rectangle rectangle2 = new Rectangle((int)Main.player[index].position.X - maxDistance, (int)Main.player[index].position.Y - maxDistance, maxDistance * 2, maxDistance * 2);
                            if (rectangle1.Intersects(rectangle2))
                            {
                                playerCollision = false;
                                break;
                            }
                        }
                    }
                    if (playerCollision)
                    {
                        collision = true;
                    }
                }

                // speed determines the max speed at which this NPC can move.
                // Higher value = faster speed.
                float speed = MathHelper.Clamp(20f + 24f * (1.025f - npc.life / npc.lifeMax), 20f, 44);
                if (LC)
                {
                    speed = MathHelper.Clamp(24f + 24f * (1.025f - npc.life / npc.lifeMax), 24f, 48);
                }
                // acceleration is exactly what it sounds like. The speed at which this NPC accelerates.
                float acceleration = MathHelper.Clamp(1.8f + 2f * (1.025f - npc.life / npc.lifeMax), 1.8f, 3.8f);
                if (LC)
                {
                    acceleration = MathHelper.Clamp(2.2f + 2.2f * (1.025f - npc.life / npc.lifeMax), 2.2f, 4.4f);
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
                // If we do not have any type of collision, we want the NPC to fall down and de-accelerate along the X axis.
                if (!collision)
                {
                    npc.TargetClosest(true);
                    npc.velocity.Y = npc.velocity.Y + 0.11f;
                    if (npc.velocity.Y > speed)
                        npc.velocity.Y = speed;
                    if (Math.Abs(npc.velocity.X) + Math.Abs(npc.velocity.Y) < speed * 0.4)
                    {
                        if (npc.velocity.X < 0.0)
                        {
                            npc.velocity.X = npc.velocity.X - acceleration * 1.1f;
                        }
                        else
                        {
                            npc.velocity.X = npc.velocity.X + acceleration * 1.1f;
                        }
                    }
                    else if (npc.velocity.Y == speed)
                    {
                        if (npc.velocity.X < dirX)
                        {
                            npc.velocity.X = npc.velocity.X + acceleration;
                        }
                        else if (npc.velocity.X > dirX)
                        {
                            npc.velocity.X = npc.velocity.X - acceleration;
                        }
                    }
                    else if (npc.velocity.Y > 4.0)
                    {
                        if (npc.velocity.X < 0.0)
                        {
                            npc.velocity.X = npc.velocity.X + acceleration * 1.0f;
                        }
                        else
                        {
                            npc.velocity.X = npc.velocity.X - acceleration * 1.0f;
                        }
                    }
                }
                else
                {
                    float absDirX = Math.Abs(dirX);
                    float absDirY = Math.Abs(dirY);
                    float newSpeed = speed / length;
                    dirX = dirX * newSpeed;
                    dirY = dirY * newSpeed;
                    if (npc.velocity.X > 0.0 && dirX > 0.0 || npc.velocity.X < 0.0 && dirX < 0.0 || (npc.velocity.Y > 0.0 && dirY > 0.0 || npc.velocity.Y < 0.0 && dirY < 0.0))
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
                }

                // Some netupdate stuff (multiplayer compatibility).
                if (collision)
                {
                    if (npc.localAI[0] != 1)
                    {
                        npc.netUpdate = true;
                    }
                    npc.localAI[0] = 1f;
                }
                else
                {
                    if (npc.localAI[0] != 0.0)
                    {
                        npc.netUpdate = true;
                    }
                    npc.localAI[0] = 0.0f;
                }
                if ((npc.velocity.X > 0.0 && npc.oldVelocity.X < 0.0 || npc.velocity.X < 0.0 && npc.oldVelocity.X > 0.0 || (npc.velocity.Y > 0.0 && npc.oldVelocity.Y < 0.0 || npc.velocity.Y < 0.0 && npc.oldVelocity.Y > 0.0)) && !npc.justHit)
                {
                    npc.netUpdate = true;
                }
                return;
            }
        }
        public override bool CheckDead()
        {
            if (npc.ai[3] == 0f)
            {
                npc.ai[3] = 1f;
                npc.damage = 0;
                npc.life = npc.lifeMax;
                npc.dontTakeDamage = true;
                npc.netUpdate = true;
                return false;
            }
            return base.CheckDead();
        }
        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 1.2f;
            return null;
        }
        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = ItemID.SuperHealingPotion;
            if (Main.rand.Next(10) == 0)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("EyeOfSatanTrophy"));
            }
            if (Main.expertMode)
            {
                npc.DropBossBags();
            }
            else
            {
                if (Main.rand.Next(7) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("WrathMask"));
                }
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("EssenceOfWrath"), Main.rand.Next(4, 9));
                switch (Main.rand.Next(3))
                {
                    case 0:
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType(""));
                        break;
                    case 1:
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType(""));
                        break;
                    case 2:
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType(""));
                        break;
                }
            }
            SinsWorld.downedWrath = true;
        }
    }
}