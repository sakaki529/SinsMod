using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.NPCs.Boss.Sins.Sloth
{
    [AutoloadBossHead]
    public class Sloth : ModNPC
    {
        internal const int size = 120;
        internal const int particleSize = 12;
        internal IList<Particle> particles = new List<Particle>();
        internal float[,] aura = new float[size, size];
        private bool FirstPhase = true;
        private bool SecondPhase;
        private bool Esc;
        private int EscTimer;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sin of Sloth");
            NPCID.Sets.TrailingMode[npc.type] = 1;
        }
        public override void SetDefaults()
        {
            npc.width = 96;
            npc.height = 96;
            npc.lifeMax = 20000;
            npc.damage = 100;
            npc.defense = 0;
            npc.knockBackResist = 0f;
            npc.aiStyle = -1;
            aiType = -1;
            npc.boss = true;
            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.behindTiles = false;
            npc.HitSound = SoundID.NPCHit54;
            npc.DeathSound = SoundID.NPCDeath62;
            npc.npcSlots = 1f;
            npc.netAlways = true;
            npc.value = Item.buyPrice(0, 20, 0, 0);
            bossBag = mod.ItemType("SlothBag");
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
            npc.damage = 180;
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
                        spriteBatch.Draw(mod.GetTexture("NPCs/Boss/Sins/Sloth/Particle"), drawPos, null, npc.GetAlpha(Color.White) * aura[x, y], 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
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
                        Dust dust = Main.dust[Dust.NewDust(npc.Left, npc.width, npc.height / 2, 242, 0f, 0f, 0, SinsColor.Sloth, 1f)];
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
            npc.TargetClosest(true);
            if (FirstPhase == true)
            {
                npc.rotation = npc.velocity.X / 15f;
                /*float speed = 9f;
                npc.velocity.X = (float)Math.Cos((float)Math.Atan2(npc.Center.Y - Main.player[npc.target].Center.Y, npc.Center.X - Main.player[npc.target].Center.X)) * -speed;
                npc.velocity.Y = (float)Math.Sin((float)Math.Atan2(npc.Center.Y - Main.player[npc.target].Center.Y, npc.Center.X - Main.player[npc.target].Center.X)) * -speed;*/

                /*npc.noGravity = true;
                npc.noTileCollide = true;
                if (npc.type == 83)
                {
                    Lighting.AddLight((int)((npc.position.X + (npc.width / 2)) / 16f), (int)((npc.position.Y + (npc.height / 2)) / 16f), 0.2f, 0.05f, 0.3f);
                }
                else
                {
                    if (npc.type == 179)
                    {
                        Lighting.AddLight((int)((npc.position.X + (npc.width / 2)) / 16f), (int)((npc.position.Y + (npc.height / 2)) / 16f), 0.3f, 0.15f, 0.05f);
                    }
                    else
                    {
                        Lighting.AddLight((int)((npc.position.X + (npc.width / 2)) / 16f), (int)((npc.position.Y + (npc.height / 2)) / 16f), 0.05f, 0.2f, 0.3f);
                    }
                }
                if (npc.target < 0 || npc.target == 255 || Main.player[npc.target].dead)
                {
                    npc.TargetClosest(true);
                }
                if (npc.ai[0] == 0f)
                {
                    float num = 9f;
                    Vector2 vector = new Vector2(npc.position.X + npc.width * 0.5f, npc.position.Y + npc.height * 0.5f);
                    float num2 = Main.player[npc.target].position.X + (Main.player[npc.target].width / 2) - vector.X;
                    float num3 = Main.player[npc.target].position.Y + (Main.player[npc.target].height / 2) - vector.Y;
                    float num4 = (float)Math.Sqrt(num2 * num2 + num3 * num3);
                    num4 = num / num4;
                    num2 *= num4;
                    num3 *= num4;
                    npc.velocity.X = num2;
                    npc.velocity.Y = num3;
                    npc.rotation = (float)Math.Atan2(npc.velocity.Y, npc.velocity.X) + 0.785f;
                    npc.ai[0] = 1f;
                    npc.ai[1] = 0f;
                    npc.netUpdate = true;
                    return;
                }
                if (npc.ai[0] == 1f)
                {
                    if (npc.justHit)
                    {
                        npc.ai[0] = 2f;
                        npc.ai[1] = 0f;
                    }
                    npc.velocity *= 0.99f;
                    npc.ai[1] += 1f;
                    if (npc.ai[1] >= 100f)
                    {
                        npc.netUpdate = true;
                        npc.ai[0] = 2f;
                        npc.ai[1] = 0f;
                        npc.velocity.X = 0f;
                        npc.velocity.Y = 0f;
                        return;
                    }
                }
                else
                {
                    if (npc.justHit)
                    {
                        npc.ai[0] = 2f;
                        npc.ai[1] = 0f;
                    }
                    npc.velocity *= 0.96f;
                    npc.ai[1] += 1f;
                    float num5 = npc.ai[1] / 120f;
                    num5 = 0.1f + num5 * 0.4f;
                    npc.rotation += num5 * npc.direction;
                    if (npc.ai[1] >= 120f)
                    {
                        npc.netUpdate = true;
                        npc.ai[0] = 0f;
                        npc.ai[1] = 0f;
                    }
                }*/

                /*npc.noGravity = true;
                if (!npc.noTileCollide)
                {
                    if (npc.collideX)
                    {
                        npc.velocity.X = npc.oldVelocity.X * -0.5f;
                        if (npc.direction == -1 && npc.velocity.X > 0f && npc.velocity.X < 2f)
                        {
                            npc.velocity.X = 2f;
                        }
                        if (npc.direction == 1 && npc.velocity.X < 0f && npc.velocity.X > -2f)
                        {
                            npc.velocity.X = -2f;
                        }
                    }
                    if (npc.collideY)
                    {
                        npc.velocity.Y = npc.oldVelocity.Y * -0.5f;
                        if (npc.velocity.Y > 0f && npc.velocity.Y < 1f)
                        {
                            npc.velocity.Y = 1f;
                        }
                        if (npc.velocity.Y < 0f && npc.velocity.Y > -1f)
                        {
                            npc.velocity.Y = -1f;
                        }
                    }
                }
                if (Main.dayTime && npc.position.Y <= Main.worldSurface * 16.0)
                {
                    if (npc.timeLeft > 10)
                    {
                        npc.timeLeft = 10;
                    }
                    npc.directionY = -1;
                    if (npc.velocity.Y > 0f)
                    {
                        npc.direction = 1;
                    }
                    npc.direction = -1;
                    if (npc.velocity.X > 0f)
                    {
                        npc.direction = 1;
                    }
                }
                else
                {
                    npc.TargetClosest(true);
                }
                if (npc.type == 170 || npc.type == 171 || npc.type == 180)
                {
                    if (Collision.CanHit(npc.position, npc.width, npc.height, Main.player[npc.target].position, Main.player[npc.target].width, Main.player[npc.target].height))
                    {
                        if (npc.ai[1] > 0f && !Collision.SolidCollision(npc.position, npc.width, npc.height))
                        {
                            npc.ai[1] = 0f;
                            npc.ai[0] = 0f;
                            npc.netUpdate = true;
                        }
                    }
                    else
                    {
                        if (npc.ai[1] == 0f)
                        {
                            npc.ai[0] += 1f;
                        }
                    }
                    if (npc.ai[0] >= 300f)
                    {
                        npc.ai[1] = 1f;
                        npc.ai[0] = 0f;
                        npc.netUpdate = true;
                    }
                    if (npc.ai[1] == 0f)
                    {
                        npc.alpha = 0;
                        npc.noTileCollide = false;
                    }
                    else
                    {
                        npc.wet = false;
                        npc.alpha = 200;
                        npc.noTileCollide = true;
                    }
                    npc.rotation = npc.velocity.Y * 0.1f * npc.direction;
                    npc.TargetClosest(true);
                    if (npc.direction == -1 && npc.velocity.X > -4f && npc.position.X > Main.player[npc.target].position.X + Main.player[npc.target].width)
                    {
                        npc.velocity.X = npc.velocity.X - 0.08f;
                        if (npc.velocity.X > 4f)
                        {
                            npc.velocity.X = npc.velocity.X - 0.04f;
                        }
                        else
                        {
                            if (npc.velocity.X > 0f)
                            {
                                npc.velocity.X = npc.velocity.X - 0.2f;
                            }
                        }
                        if (npc.velocity.X < -4f)
                        {
                            npc.velocity.X = -4f;
                        }
                    }
                    else
                    {
                        if (npc.direction == 1 && npc.velocity.X < 4f && npc.position.X + (float)npc.width < Main.player[npc.target].position.X)
                        {
                            npc.velocity.X = npc.velocity.X + 0.08f;
                            if (npc.velocity.X < -4f)
                            {
                                npc.velocity.X = npc.velocity.X + 0.04f;
                            }
                            else
                            {
                                if (npc.velocity.X < 0f)
                                {
                                    npc.velocity.X = npc.velocity.X + 0.2f;
                                }
                            }
                            if (npc.velocity.X > 4f)
                            {
                                npc.velocity.X = 4f;
                            }
                        }
                    }
                    if (npc.directionY == -1 && npc.velocity.Y > -2.5 && npc.position.Y > Main.player[npc.target].position.Y + Main.player[npc.target].height)
                    {
                        npc.velocity.Y = npc.velocity.Y - 0.1f;
                        if (npc.velocity.Y > 2.5)
                        {
                            npc.velocity.Y = npc.velocity.Y - 0.05f;
                        }
                        else
                        {
                            if (npc.velocity.Y > 0f)
                            {
                                npc.velocity.Y = npc.velocity.Y - 0.15f;
                            }
                        }
                        if (npc.velocity.Y < -2.5)
                        {
                            npc.velocity.Y = -2.5f;
                        }
                    }
                    else
                    {
                        if (npc.directionY == 1 && npc.velocity.Y < 2.5 && npc.position.Y + npc.height < Main.player[npc.target].position.Y)
                        {
                            npc.velocity.Y = npc.velocity.Y + 0.1f;
                            if (npc.velocity.Y < -2.5)
                            {
                                npc.velocity.Y = npc.velocity.Y + 0.05f;
                            }
                            else
                            {
                                if (npc.velocity.Y < 0f)
                                {
                                    npc.velocity.Y = npc.velocity.Y + 0.15f;
                                }
                            }
                            if (npc.velocity.Y > 2.5)
                            {
                                npc.velocity.Y = 2.5f;
                            }
                        }
                    }
                }
                else
                {
                    if (npc.type == 116)//WoFsummon
                    {
                        npc.TargetClosest(true);
                        Lighting.AddLight((int)(npc.position.X + (npc.width / 2)) / 16, (int)(npc.position.Y + (npc.height / 2)) / 16, 0.3f, 0.2f, 0.1f);
                        if (npc.direction == -1 && npc.velocity.X > -6f)
                        {
                            npc.velocity.X = npc.velocity.X - 0.1f;
                            if (npc.velocity.X > 6f)
                            {
                                npc.velocity.X = npc.velocity.X - 0.1f;
                            }
                            else
                            {
                                if (npc.velocity.X > 0f)
                                {
                                    npc.velocity.X = npc.velocity.X - 0.2f;
                                }
                            }
                            if (npc.velocity.X < -6f)
                            {
                                npc.velocity.X = -6f;
                            }
                        }
                        else
                        {
                            if (npc.direction == 1 && npc.velocity.X < 6f)
                            {
                                npc.velocity.X = npc.velocity.X + 0.1f;
                                if (npc.velocity.X < -6f)
                                {
                                    npc.velocity.X = npc.velocity.X + 0.1f;
                                }
                                else
                                {
                                    if (npc.velocity.X < 0f)
                                    {
                                        npc.velocity.X = npc.velocity.X + 0.2f;
                                    }
                                }
                                if (npc.velocity.X > 6f)
                                {
                                    npc.velocity.X = 6f;
                                }
                            }
                        }
                        if (npc.directionY == -1 && npc.velocity.Y > -2.5)
                        {
                            npc.velocity.Y = npc.velocity.Y - 0.04f;
                            if (npc.velocity.Y > 2.5)
                            {
                                npc.velocity.Y = npc.velocity.Y - 0.05f;
                            }
                            else
                            {
                                if (npc.velocity.Y > 0f)
                                {
                                    npc.velocity.Y = npc.velocity.Y - 0.15f;
                                }
                            }
                            if (npc.velocity.Y < -2.5)
                            {
                                npc.velocity.Y = -2.5f;
                            }
                        }
                        else
                        {
                            if (npc.directionY == 1 && npc.velocity.Y < 1.5)
                            {
                                npc.velocity.Y = npc.velocity.Y + 0.04f;
                                if (npc.velocity.Y < -2.5)
                                {
                                    npc.velocity.Y = npc.velocity.Y + 0.05f;
                                }
                                else
                                {
                                    if (npc.velocity.Y < 0f)
                                    {
                                        npc.velocity.Y = npc.velocity.Y + 0.15f;
                                    }
                                }
                                if (npc.velocity.Y > 2.5)
                                {
                                    npc.velocity.Y = 2.5f;
                                }
                            }
                        }
                        if (Main.rand.Next(40) == 0)
                        {
                            int num = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y + npc.height * 0.25f), npc.width, (int)(npc.height * 0.5f), 5, npc.velocity.X, 2f, 0, default(Color), 1f);
                            Dust expr_B48_cp_0_cp_0 = Main.dust[num];
                            expr_B48_cp_0_cp_0.velocity.X = expr_B48_cp_0_cp_0.velocity.X * 0.5f;
                            Dust expr_B62_cp_0_cp_0 = Main.dust[num];
                            expr_B62_cp_0_cp_0.velocity.Y = expr_B62_cp_0_cp_0.velocity.Y * 0.1f;
                        }
                    }
                    else
                    {
                        if (npc.type == 133)//wanderingEye
                        {
                            if (npc.life < npc.lifeMax * 0.5)
                            {
                                if (npc.direction == -1 && npc.velocity.X > -6f)
                                {
                                    npc.velocity.X = npc.velocity.X - 0.1f;
                                    if (npc.velocity.X > 6f)
                                    {
                                        npc.velocity.X = npc.velocity.X - 0.1f;
                                    }
                                    else
                                    {
                                        if (npc.velocity.X > 0f)
                                        {
                                            npc.velocity.X = npc.velocity.X + 0.05f;
                                        }
                                    }
                                    if (npc.velocity.X < -6f)
                                    {
                                        npc.velocity.X = -6f;
                                    }
                                }
                                else
                                {
                                    if (npc.direction == 1 && npc.velocity.X < 6f)
                                    {
                                        npc.velocity.X = npc.velocity.X + 0.1f;
                                        if (npc.velocity.X < -6f)
                                        {
                                            npc.velocity.X = npc.velocity.X + 0.1f;
                                        }
                                        else
                                        {
                                            if (npc.velocity.X < 0f)
                                            {
                                                npc.velocity.X = npc.velocity.X - 0.05f;
                                            }
                                        }
                                        if (npc.velocity.X > 6f)
                                        {
                                            npc.velocity.X = 6f;
                                        }
                                    }
                                }
                                if (npc.directionY == -1 && npc.velocity.Y > -4f)
                                {
                                    npc.velocity.Y = npc.velocity.Y - 0.1f;
                                    if (npc.velocity.Y > 4f)
                                    {
                                        npc.velocity.Y = npc.velocity.Y - 0.1f;
                                    }
                                    else
                                    {
                                        if (npc.velocity.Y > 0f)
                                        {
                                            npc.velocity.Y = npc.velocity.Y + 0.05f;
                                        }
                                    }
                                    if (npc.velocity.Y < -4f)
                                    {
                                        npc.velocity.Y = -4f;
                                    }
                                }
                                else
                                {
                                    if (npc.directionY == 1 && npc.velocity.Y < 4f)
                                    {
                                        npc.velocity.Y = npc.velocity.Y + 0.1f;
                                        if (npc.velocity.Y < -4f)
                                        {
                                            npc.velocity.Y = npc.velocity.Y + 0.1f;
                                        }
                                        else
                                        {
                                            if (npc.velocity.Y < 0f)
                                            {
                                                npc.velocity.Y = npc.velocity.Y - 0.05f;
                                            }
                                        }
                                        if (npc.velocity.Y > 4f)
                                        {
                                            npc.velocity.Y = 4f;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (npc.direction == -1 && npc.velocity.X > -4f)
                                {
                                    npc.velocity.X = npc.velocity.X - 0.1f;
                                    if (npc.velocity.X > 4f)
                                    {
                                        npc.velocity.X = npc.velocity.X - 0.1f;
                                    }
                                    else
                                    {
                                        if (npc.velocity.X > 0f)
                                        {
                                            npc.velocity.X = npc.velocity.X + 0.05f;
                                        }
                                    }
                                    if (npc.velocity.X < -4f)
                                    {
                                        npc.velocity.X = -4f;
                                    }
                                }
                                else
                                {
                                    if (npc.direction == 1 && npc.velocity.X < 4f)
                                    {
                                        npc.velocity.X = npc.velocity.X + 0.1f;
                                        if (npc.velocity.X < -4f)
                                        {
                                            npc.velocity.X = npc.velocity.X + 0.1f;
                                        }
                                        else
                                        {
                                            if (npc.velocity.X < 0f)
                                            {
                                                npc.velocity.X = npc.velocity.X - 0.05f;
                                            }
                                        }
                                        if (npc.velocity.X > 4f)
                                        {
                                            npc.velocity.X = 4f;
                                        }
                                    }
                                }
                                if (npc.directionY == -1 && npc.velocity.Y > -1.5)
                                {
                                    npc.velocity.Y = npc.velocity.Y - 0.04f;
                                    if (npc.velocity.Y > 1.5)
                                    {
                                        npc.velocity.Y = npc.velocity.Y - 0.05f;
                                    }
                                    else
                                    {
                                        if (npc.velocity.Y > 0f)
                                        {
                                            npc.velocity.Y = npc.velocity.Y + 0.03f;
                                        }
                                    }
                                    if (npc.velocity.Y < -1.5)
                                    {
                                        npc.velocity.Y = -1.5f;
                                    }
                                }
                                else
                                {
                                    if (npc.directionY == 1 && npc.velocity.Y < 1.5)
                                    {
                                        npc.velocity.Y = npc.velocity.Y + 0.04f;
                                        if (npc.velocity.Y < -1.5)
                                        {
                                            npc.velocity.Y = npc.velocity.Y + 0.05f;
                                        }
                                        else
                                        {
                                            if (npc.velocity.Y < 0f)
                                            {
                                                npc.velocity.Y = npc.velocity.Y - 0.03f;
                                            }
                                        }
                                        if (npc.velocity.Y > 1.5)
                                        {
                                            npc.velocity.Y = 1.5f;
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            float num2 = 4f;
                            float num3 = 1.5f;
                            num2 *= 1f + (1f - npc.scale);
                            num3 *= 1f + (1f - npc.scale);
                            if (npc.direction == -1 && npc.velocity.X > -num2)
                            {
                                npc.velocity.X = npc.velocity.X - 0.1f;
                                if (npc.velocity.X > num2)
                                {
                                    npc.velocity.X = npc.velocity.X - 0.1f;
                                }
                                else
                                {
                                    if (npc.velocity.X > 0f)
                                    {
                                        npc.velocity.X = npc.velocity.X + 0.05f;
                                    }
                                }
                                if (npc.velocity.X < -num2)
                                {
                                    npc.velocity.X = -num2;
                                }
                            }
                            else
                            {
                                if (npc.direction == 1 && npc.velocity.X < num2)
                                {
                                    npc.velocity.X = npc.velocity.X + 0.1f;
                                    if (npc.velocity.X < -num2)
                                    {
                                        npc.velocity.X = npc.velocity.X + 0.1f;
                                    }
                                    else
                                    {
                                        if (npc.velocity.X < 0f)
                                        {
                                            npc.velocity.X = npc.velocity.X - 0.05f;
                                        }
                                    }
                                    if (npc.velocity.X > num2)
                                    {
                                        npc.velocity.X = num2;
                                    }
                                }
                            }
                            if (npc.directionY == -1 && npc.velocity.Y > -num3)
                            {
                                npc.velocity.Y = npc.velocity.Y - 0.04f;
                                if (npc.velocity.Y > num3)
                                {
                                    npc.velocity.Y = npc.velocity.Y - 0.05f;
                                }
                                else
                                {
                                    if (npc.velocity.Y > 0f)
                                    {
                                        npc.velocity.Y = npc.velocity.Y + 0.03f;
                                    }
                                }
                                if (npc.velocity.Y < -num3)
                                {
                                    npc.velocity.Y = -num3;
                                }
                            }
                            else
                            {
                                if (npc.directionY == 1 && npc.velocity.Y < num3)
                                {
                                    npc.velocity.Y = npc.velocity.Y + 0.04f;
                                    if (npc.velocity.Y < -num3)
                                    {
                                        npc.velocity.Y = npc.velocity.Y + 0.05f;
                                    }
                                    else
                                    {
                                        if (npc.velocity.Y < 0f)
                                        {
                                            npc.velocity.Y = npc.velocity.Y - 0.03f;
                                        }
                                    }
                                    if (npc.velocity.Y > num3)
                                    {
                                        npc.velocity.Y = num3;
                                    }
                                }
                            }
                        }
                    }
                }
                if ((npc.type == 2 || npc.type == 133 || npc.type == 190 || npc.type == 191 || npc.type == 192 || npc.type == 193 || npc.type == 194) && Main.rand.Next(40) == 0)
                {
                    int num4 = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y + npc.height * 0.25f), npc.width, (int)(npc.height * 0.5f), 5, npc.velocity.X, 2f, 0, default(Color), 1f);
                    Dust expr_1490_cp_0_cp_0 = Main.dust[num4];
                    expr_1490_cp_0_cp_0.velocity.X = expr_1490_cp_0_cp_0.velocity.X * 0.5f;
                    Dust expr_14AB_cp_0_cp_0 = Main.dust[num4];
                    expr_14AB_cp_0_cp_0.velocity.Y = expr_14AB_cp_0_cp_0.velocity.Y * 0.1f;
                }
                if (npc.wet && npc.type != 170 && npc.type != 171 && npc.type != 172)
                {
                    if (npc.velocity.Y > 0f)
                    {
                        npc.velocity.Y = npc.velocity.Y * 0.95f;
                    }
                    npc.velocity.Y = npc.velocity.Y - 0.5f;
                    if (npc.velocity.Y < -4f)
                    {
                        npc.velocity.Y = -4f;
                    }
                    npc.TargetClosest(true);
                }*/
                bool flag = npc.localAI[0] == 0f;
                if (flag)
                {
                    npc.localAI[0] = npc.Center.Y;
                    npc.netUpdate = true;
                }
                bool flag2 = npc.Center.Y >= npc.localAI[0];
                if (flag2)
                {
                    npc.localAI[1] = -1f;
                    npc.netUpdate = true;
                }
                bool flag3 = npc.Center.Y <= npc.localAI[0] - 10f;
                if (flag3)
                {
                    npc.localAI[1] = 1f;
                    npc.netUpdate = true;
                }
                npc.velocity.Y = MathHelper.Clamp(npc.velocity.Y + 0.05f * npc.localAI[1], -2f, 2f);
                npc.velocity.X *= 0f;
                int value = 9;
                if (Main.expertMode)
                {
                    value = 6;
                }
                if (SinsWorld.LimitCut)
                {
                    value = 3;
                }
                if (Main.rand.NextBool(value))
                {
                    ShotNeedle();
                }
                if (Main.expertMode && npc.life < (int)((double)npc.lifeMax * 0.4f))
                {
                    FirstPhase = false;
                }
                if (!Main.expertMode && npc.life < (int)((double)npc.lifeMax * 0.2f))
                {
                    FirstPhase = false;
                }
                npc.ai[0]++;
                if (npc.ai[0] >= 400)
                {
                    if (Main.netMode != 1 && npc.ai[1] == 0)
                    {
                        int num = Projectile.NewProjectile(npc.Center.X, npc.Center.Y, 0f, 0f, mod.ProjectileType("ShockwaveBoom"), 0, 0, Main.myPlayer, 0, 0);
                        Main.projectile[num].Center = npc.Center;
                        npc.ai[1] = 1;
                    }
                    if (npc.ai[0] >= 420)
                    {
                        if (Main.netMode != 1)
                        {
                            player.mount.Dismount(player);
                            for (int i = 0; i < 1000; i++)
                            {
                                if (Main.projectile[i].active && (Main.projHook[Main.projectile[i].type] || Main.projectile[i].aiStyle == 7))
                                {
                                    Main.projectile[i].Kill();
                                    Main.projectile[i].active = false;
                                }
                            }
                            Vector2 vector = player.Center - npc.Center;
                            vector.Normalize();
                            player.velocity = vector * 24f;
                        }
                        npc.ai[0] = 0;
                        npc.ai[1] = 0;
                    }
                }
                return;
            }
            if (!SecondPhase)
            {
                SecondPhase = true;
                npc.ai[0] = 0;
                npc.ai[1] = 90;
            }
            if (SecondPhase == true)
            {
                npc.ai[1]++;
                npc.TargetClosest(true);
                Vector2 vector = new Vector2(npc.Center.X, npc.Center.Y - 63);
                float num = Main.rand.Next(-1000, 1001);
                float num2 = Main.rand.Next(-1000, 1001);
                float num3 = (float)Math.Sqrt(num * num + num2 * num2);
                float num4 = 8f;
                npc.velocity *= 0.95f;
                num3 = num4 / num3;
                num *= num3;
                num2 *= num3;
                npc.rotation += 0.3f;
                vector.X += num * 4f;
                vector.Y += num2 * 4f;
                float speed = 6.5f;
                if (Main.expertMode)
                {
                    speed = 7.25f;
                }
                if (SinsWorld.LimitCut)
                {
                    speed = 8f;
                }
                npc.velocity.X = (float)Math.Cos((float)Math.Atan2(npc.Center.Y - Main.player[npc.target].Center.Y, npc.Center.X - Main.player[npc.target].Center.X)) * -speed;
                npc.velocity.Y = (float)Math.Sin((float)Math.Atan2(npc.Center.Y - Main.player[npc.target].Center.Y, npc.Center.X - Main.player[npc.target].Center.X)) * -speed;
                if (npc.ai[1] % 30 == 0 && SinsWorld.LimitCut)
                {
                    ShotNeedle();
                }
                else if (npc.ai[1] % 45 == 0 && Main.expertMode)
                {
                    ShotNeedle();
                }
                if (npc.ai[1] >= 90)
                {
                    Color color = new Color();
                    Rectangle rectangle = new Rectangle((int)npc.position.X, (int)(npc.position.Y + ((npc.height - npc.width) / 2)), npc.width, npc.width);
                    int count = 30;
                    float vectorReduce = 0.5f;
                    for (int i = 0; i < count; i++)
                    {
                        int dust = Dust.NewDust(npc.position, rectangle.Width, rectangle.Height, 41, 0, 0, 100, color, 1.5f);
                        Main.dust[dust].noGravity = true;
                        Main.dust[dust].velocity.X = vectorReduce * (Main.dust[dust].position.X - (npc.position.X + (npc.width / 2)));
                        Main.dust[dust].velocity.Y = vectorReduce * (Main.dust[dust].position.Y - (npc.position.Y + (npc.height / 2)));
                    }
                    float distance = 560f;
                    if (Main.expertMode)
                    {
                        distance = 500f;
                    }
                    if (SinsWorld.LimitCut)
                    {
                        distance = 440f;
                    }
                    npc.ai[0] %= (float)Math.PI * 2f;
                    Vector2 offset = new Vector2((float)Math.Cos(npc.ai[0]), (float)Math.Sin(npc.ai[0]));
                    npc.position = Main.player[npc.target].position + distance * offset;
                    Main.PlaySound(SoundID.Roar, (int)npc.position.X, (int)npc.position.Y, 0);
                    npc.ai[1] = 0;
                    npc.ai[0] += 10;
                    for (int i = 0; i < 8; i++)
                    {
                        Projectile.NewProjectile(npc.Center, Vector2.UnitX.RotatedBy(Math.PI / 4 * i), mod.ProjectileType("SlothScythe"), 30, 0f, Main.myPlayer);
                    }
                }
            }
        }
        private void ShotNeedle()
        {
            if (npc.target != -1)
            {
                Vector2 vector = Main.player[npc.target].Center - npc.Center;
                vector = vector * (2 / (float)Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y));
                int spread = 18;
                int spreadMin = 12;
                float spreadMult = 0.25f;
                bool flag = Collision.CanHitLine(npc.Center, 1, 1, Main.player[npc.target].Center, 1, 1);
                for (int i = 0; i < 2; i++)
                {
                    int num = Main.rand.Next(spreadMin, spread + 1);
                    if (Main.rand.NextBool(2))
                    {
                        num *= -1;
                    }
                    vector.X = vector.X + num * spreadMult;
                    num = Main.rand.Next(spreadMin, spread + 1);
                    if (Main.rand.NextBool(2))
                    {
                        num *= -1;
                    }
                    vector.Y = vector.Y + num * spreadMult;
                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y, vector.X, vector.Y, mod.ProjectileType("SlothNeedle"), 25, 0f);
                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y, -vector.X, -vector.Y, mod.ProjectileType("SlothNeedle"), 25, 0f);
                }
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
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("SlothTrophy"));
            }
            if (Main.expertMode)
            {
                npc.DropBossBags();
            }
            else
            {
                if (Main.rand.Next(7) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType(""));
                }
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("EssenceOfSloth"), Main.rand.Next(4, 9));
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
            SinsWorld.downedSloth = true;
        }
    }
}