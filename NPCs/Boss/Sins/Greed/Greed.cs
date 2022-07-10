using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.NPCs.Boss.Sins.Greed
{
    [AutoloadBossHead]
    public class Greed : ModNPC
    {
        internal const int size = 120;
        internal const int particleSize = 12;
        internal IList<Particle> particles = new List<Particle>();
        internal float[,] aura = new float[size, size];
        private bool FirstPhase = true;
        private bool Esc;
        private int EscTimer;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sin of Greed");
            NPCID.Sets.TrailingMode[npc.type] = 1;
        }
        public override void SetDefaults()
        {
            npc.width = 96;
            npc.height = 96;
            npc.lifeMax = 1600;
            npc.damage = 30;
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
            npc.npcSlots = 5f;
            npc.netAlways = true;
            npc.value = Item.buyPrice(0, 2, 70, 0);
            bossBag = mod.ItemType("GreedBag");
            for (int i = 0; i < npc.buffImmune.Length; i++)
            {
                if (SinsWorld.Hopeless)
                {
                    npc.buffImmune[i] = true;
                }
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
            npc.damage = 50;
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
                        spriteBatch.Draw(mod.GetTexture("NPCs/Boss/Sins/Greed/Particle"), drawPos, null, npc.GetAlpha(Color.White) * aura[x, y], 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
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
                        Dust dust = Main.dust[Dust.NewDust(npc.Left, npc.width, npc.height / 2, 242, 0f, 0f, 0, SinsColor.Greed, 1f)];
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
            bool tacklePlayer = true;
            float maxDistanceAmt = 16f;
            float maxDistance = 360f;
            float increment = 0.02f;
            float closeIncrement = 0.03f;
            float distanceAmt = 1f;
            float distX = Main.player[npc.target].Center.X - npc.Center.X;
            float distY = Main.player[npc.target].Center.Y - npc.Center.Y;
            float dist = (float)Math.Sqrt(distX * distX + distY * distY);
            npc.ai[1] += 1f;
            if (LC)
            {
                npc.ai[1] += 1f;
                maxDistanceAmt = 20f;
                maxDistance = 400f;
                increment = 0.04f;
                closeIncrement = 0.06f;
            }
            if (npc.ai[1] > 600f)
            {
                if (tacklePlayer)
                {
                    increment *= 8f;
                    distanceAmt = 4f;
                    if (npc.ai[1] > 650f)
                    {
                        npc.ai[1] = 0f;
                    }
                }
                else
                {
                    npc.ai[1] = 0f;
                }
            }
            else if (dist < 250f)
            {
                npc.ai[0] += 0.9f;
                if (npc.ai[0] > 0f)
                {
                    npc.velocity.Y = npc.velocity.Y + closeIncrement;
                }
                else
                {
                    npc.velocity.Y = npc.velocity.Y - closeIncrement;
                }
                if (npc.ai[0] < -100f || npc.ai[0] > 100f)
                {
                    npc.velocity.X = npc.velocity.X + closeIncrement;
                }
                else
                {
                    npc.velocity.X = npc.velocity.X - closeIncrement;
                }
                if (npc.ai[0] > 200f)
                {
                    npc.ai[0] = -200f;
                }
            }
            if (dist > maxDistance)
            {
                distanceAmt = maxDistanceAmt + (maxDistanceAmt / 4f);
                increment = 0.3f;
            }
            else if (dist > maxDistance - (maxDistance / 7f))
            {
                distanceAmt = maxDistanceAmt - (maxDistanceAmt / 4f);
                increment = 0.2f;
            }
            else if (dist > maxDistance - (2 * (maxDistance / 7f)))
            {
                distanceAmt = (maxDistanceAmt / 2.66f);
                increment = 0.1f;
            }
            dist = distanceAmt / dist;
            distX *= dist; distY *= dist;
            if (Main.player[npc.target].dead)
            {
                distX = npc.direction * distanceAmt / 2f;
                distY = -distanceAmt / 2f;
            }
            if (npc.velocity.X < distX)
            {
                npc.velocity.X = npc.velocity.X + increment;
            }
            else if (npc.velocity.X > distX)
            {
                npc.velocity.X = npc.velocity.X - increment;
            }
            if (npc.velocity.Y < distY)
            {
                npc.velocity.Y = npc.velocity.Y + increment;
            }
            else if (npc.velocity.Y > distY)
            {
                npc.velocity.Y = npc.velocity.Y - increment;
            }
            if (FirstPhase)
            {
                npc.localAI[0]++;
                if (npc.localAI[0] >= (npc.life < (int)(npc.lifeMax * 0.7f) ? 30 : 60) && Main.rand.Next(100) == 0)
                {
                    Vector2 vector = player.Center - npc.Center;
                    vector.Normalize();
                    vector *= Main.rand.NextFloat(4.5f, 7f);
                    int num5 = Projectile.NewProjectile(npc.Center.X + Main.rand.Next(-46, 46), npc.Center.Y + Main.rand.Next(-46, 46), vector.X, vector.Y, ProjectileID.TopazBolt, 10, 0f, Main.myPlayer, 0, 0f);
                    Main.projectile[num5].friendly = false;
                    Main.projectile[num5].hostile = true;
                    Main.projectile[num5].tileCollide = false;
                    Main.projectile[num5].timeLeft = 360;
                    npc.localAI[0] = 0;
                }
                if (npc.life < (int)(npc.lifeMax * 0.4f))
                {
                    npc.localAI[1]++;
                    if (npc.localAI[1] >= 40 && Main.rand.Next(180) == 0)
                    {
                        Vector2 vector = player.Center - npc.Center;
                        vector.Normalize();
                        vector *= Main.rand.NextFloat(4.5f, 7f);
                        int num5 = Projectile.NewProjectile(npc.Center.X + Main.rand.Next(-46, 46), npc.Center.Y + Main.rand.Next(-46, 46), vector.X, vector.Y, ProjectileID.TopazBolt, 15, 0f, Main.myPlayer, 0, 0f);
                        Main.projectile[num5].friendly = false;
                        Main.projectile[num5].hostile = true;
                        Main.projectile[num5].tileCollide = false;
                        Main.projectile[num5].timeLeft = 360;
                        npc.localAI[1] = 0;
                    }
                }
                if (npc.life < (int)(npc.lifeMax * 0.1f) || (Main.expertMode && npc.life < (int)(npc.lifeMax * 0.25f)))
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
            potionType = ItemID.LesserHealingPotion;
            if (Main.rand.Next(10) == 0)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("DesertKingTrophy"));
            }
            if (Main.expertMode)
            {
                npc.DropBossBags();
            }
            else
            {
                if (Main.rand.Next(7) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("DesertKingMask"));
                }
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("EssenceOfGreed"), Main.rand.Next(4, 9));
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
            SinsWorld.downedGreed = true;
        }
    }
}