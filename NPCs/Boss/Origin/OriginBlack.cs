using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.NPCs.Boss.Origin
{
    [AutoloadBossHead]
    public class OriginBlack : ModNPC//bases white
    {
        internal const int size = 120;
        internal const int particleSize = 12;
        internal IList<Particle> particles = new List<Particle>();
        internal float[,] aura = new float[size, size];
        private bool SecondPhase;
        private bool OnlyMe;
        private bool fromEden;
        private bool Esc;
        private int EscTimer;
        private int movement;
        private int[] Count = new int[4];
        private bool Start;
        private float rot;
        private float rotInc;
        private float rotMult = 1;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sin of Origin");
            NPCID.Sets.TrailingMode[npc.type] = 1;
        }
        public override void SetDefaults()
        {
            npc.width = 96;
            npc.height = 96;
            npc.lifeMax = 600000;
            npc.damage = 360;
            npc.defense = 70;
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
            npc.value = Item.buyPrice(0, 80, 0, 0);
            bossBag = mod.ItemType("OriginBag");
            for (int i = 0; i < npc.buffImmune.Length; i++)
            {
                npc.buffImmune[i] = true;
                npc.buffImmune[mod.BuffType("Chroma")] = false;
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
            npc.damage = 420;
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
                        spriteBatch.Draw(mod.GetTexture("NPCs/Boss/Origin/ParticleBlack"), drawPos, null, npc.GetAlpha(Color.White) * aura[x, y], 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
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
                        Dust dust = Main.dust[Dust.NewDust(npc.Left, npc.width, npc.height / 2, 242, 0f, 0f, 0, SinsColor.MediumBlack, 1f)];
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
            if (Start)
            {
                bool flag = npc.life < npc.lifeMax / 3;
                SecondPhase = flag;
                if (!OnlyMe && fromEden)
                {
                    OnlyMe = npc.ai[2] == -1 || Main.npc[(int)npc.ai[2]].type != mod.NPCType("OriginWhite") || Main.npc[(int)npc.ai[2]].life <= 0;
                }
            }
            else
            {
                if (npc.ai[0] != 0)
                {
                    fromEden = true;
                    npc.ai[1]++;
                    npc.velocity.X = 18 * npc.ai[0];
                    if (npc.ai[1] > 20)
                    {
                        npc.ai[0] = 0;
                        npc.ai[1] = 0;
                    }
                }
                else
                {
                    Start = true;
                }
                return;
            }
            if (OnlyMe)
            {
                return;
            }
            NPC white = Main.npc[(int)npc.ai[2]];
            npc.target = white.target;
            movement = (int)white.ai[1];
            switch (movement)
            {
                case 0:
                    npc.ai[0] = 0;
                    Count[0] = 0;
                    Count[1] = 0;
                    Count[2] = 0;
                    rot = 0;
                    rotInc = 0;
                    rotMult = 1;
                    break;
                case 1:
                    npc.rotation = npc.velocity.X / 10f;
                    Vector2 vector = new Vector2(npc.position.X + npc.width * 0.5f, npc.position.Y + npc.height * 0.5f);
                    float num = player.position.X + player.width / 2 - vector.X;
                    float num2 = player.position.Y + player.height / 2 - 120f - vector.Y;
                    float num3 = (float)Math.Sqrt(num * num + num2 * num2);
                    float num4 = 24f / num3;
                    num *= num4;
                    num2 *= num4;
                    if (npc.velocity.X < num)
                    {
                        npc.velocity.X = npc.velocity.X + 0.2f;
                        if (npc.velocity.X < 0f && num > 0f)
                        {
                            npc.velocity.X = npc.velocity.X + 0.2f;
                        }
                    }
                    else
                    {
                        if (npc.velocity.X > num)
                        {
                            npc.velocity.X = npc.velocity.X - 0.2f;
                            if (npc.velocity.X > 0f && num < 0f)
                            {
                                npc.velocity.X = npc.velocity.X - 0.2f;
                            }
                        }
                    }
                    if (npc.velocity.Y < num2)
                    {
                        npc.velocity.Y = npc.velocity.Y + 0.2f;
                        if (npc.velocity.Y < 0f && num2 > 0f)
                        {
                            npc.velocity.Y = npc.velocity.Y + 0.2f;
                        }
                    }
                    else
                    {
                        if (npc.velocity.Y > num2)
                        {
                            npc.velocity.Y = npc.velocity.Y - 0.2f;
                            if (npc.velocity.Y > 0f && num2 < 0f)
                            {
                                npc.velocity.Y = npc.velocity.Y - 0.2f;
                            }
                        }
                    }
                    if (Main.rand.Next(30) == 0)
                    {
                        Vector2 vector2 = new Vector2(npc.position.X + npc.width * 0.5f, npc.position.Y + npc.height / 2);
                        float num5 = (float)Math.Atan2(vector2.Y - (player.position.Y + player.height * 0.5f), vector2.X - (player.position.X + player.width * 0.5f));
                        npc.velocity.X = (float)(Math.Cos(num5) * 24) * -1f;
                        npc.velocity.Y = (float)(Math.Sin(num5) * 24) * -1f;
                        npc.netUpdate = true;
                    }
                    break;
                case 2:
                    if (Count[0] == 0f)
                    {
                        float num6 = Main.rand.NextFloat(Main.expertMode ? 18f : 15f, Main.expertMode ? 22f : 19f);
                        if (LC)
                        {
                            num6 = Main.rand.NextFloat(20f, 26f);
                        }
                        Vector2 vector3 = new Vector2(npc.position.X + npc.width * 0.5f, npc.position.Y + npc.height * 0.5f);
                        float num7 = Main.player[npc.target].position.X + (Main.player[npc.target].width / 2) - vector3.X;
                        float num8 = Main.player[npc.target].position.Y + (Main.player[npc.target].height / 2) - vector3.Y;
                        float num9 = (float)Math.Sqrt(num7 * num7 + num8 * num8);
                        num9 = num6 / num9;
                        num7 *= num9;
                        num8 *= num9;
                        npc.velocity.X = num7;
                        npc.velocity.Y = num8;
                        npc.rotation = (float)Math.Atan2(npc.velocity.Y, npc.velocity.X) + 0.785f;
                        Count[0] = 1;
                        Count[1] = 0;
                        npc.netUpdate = true;
                        return;
                    }
                    if (Count[0] == 1)
                    {
                        npc.velocity *= 0.99f;
                        Count[1] += 1;
                        int min = Main.expertMode ? 40 : 60;
                        int max = Main.expertMode ? 61 : 81;
                        if (LC)
                        {
                            min = 40;
                            max = 61;
                        }
                        if (Count[1] >= Main.rand.Next(min, max))
                        {
                            npc.netUpdate = true;
                            Count[0] = 2;
                            Count[1] = 0;
                            npc.velocity.X = 0f;
                            npc.velocity.Y = 0f;
                            return;
                        }
                    }
                    else
                    {
                        npc.velocity *= 0.98f;
                        Count[1] += 1;
                        float num10 = Count[1] / 100f;
                        num10 = 0.1f + num10 * 0.4f;
                        npc.rotation += num10 * npc.direction;
                        int min = Main.expertMode ? 40 : 70;
                        int max = Main.expertMode ? 61 : 91;
                        if (LC)
                        {
                            min = 40;
                            max = 61;
                        }
                        if (Count[1] >= Main.rand.Next(min, max))
                        {
                            npc.netUpdate = true;
                            Count[0] = 0;
                            Count[1] = 0;
                        }
                        if (Count[1] % 5 == 0)
                        {

                        }
                    }
                    break;
                case 3:
                    npc.alpha = white.alpha;
                    if (white.ai[0] == 2f)
                    {
                        npc.rotation = 0;
                        if (rotInc == 0)
                        {
                            rotInc = Main.rand.NextFloat(0.05f, 0.35f);
                            if (Main.rand.Next(2) == 0)
                            {
                                rotInc *= -1;
                            }
                        }
                        rotMult *= 1.02f;
                        rot += rotInc / rotMult;
                        npc.Center = player.Center + Utils.RotatedBy(new Vector2(320, 0), rot + 1 * (6.28f / 2f), default(Vector2));
                    }
                    else if (white.ai[0] == 3f)
                    {
                        npc.velocity = npc.DirectionTo(player.Center) * Main.rand.Next(12, 18);
                    }
                    else if (white.ai[0] == 4f)
                    {
                        rot = 0;
                        rotInc = 0;
                        rotMult = 1;
                    }
                    break;
                case 4:
                    break;
                default:
                    break;
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
            Main.npc[(int)npc.ai[2]].ai[2] = -1;
            return base.CheckDead();
        }
        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 1.2f;
            return null;
        }
        public override bool PreNPCLoot()
        {
            return !NPC.AnyNPCs(mod.NPCType("OriginWhite"));
        }
        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = ItemID.SuperHealingPotion;
            if (Main.rand.Next(10) == 0)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("OriginTrophy"));
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
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("EssenceOfOrigin"), Main.rand.Next(4, 9));
                switch (Main.rand.Next(3))
                {
                    case 0:
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("GardenOfEden"));
                        break;
                    case 1:
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType(""));
                        break;
                    case 2:
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType(""));
                        break;
                }
            }
            SinsWorld.downedOrigin = true;
        }
    }
}