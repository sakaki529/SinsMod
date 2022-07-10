using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.NPCs.Boss.TartarusGuardian
{
    [AutoloadBossHead]
    public class TartarusHead : ModNPC
    {
        public static bool isSegmentAttack = false;
        private bool Esc;
        private bool flies = true;
        private bool Roar = true;
        private bool TailSpawned;
        private bool Flame;
        private int FlameCounter;
        private bool Dash;
        private int DashTime;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Guardian of Tartarus");
            NPCID.Sets.TrailingMode[npc.type] = 1;
            NPCID.Sets.TrailCacheLength[npc.type] = 5;
        }
        public override void SetDefaults()
        {
            npc.width = 40;
            npc.height = 40;
            npc.lifeMax = 7500000;
            npc.damage = 300;
            npc.defense = 0;
            npc.knockBackResist = 0f;
            npc.aiStyle = -1;
            aiType = -1;
            npc.boss = true;
            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.behindTiles = true;
            npc.netAlways = true;
            npc.HitSound = SoundID.NPCHit56;
            npc.DeathSound = mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/TartarusRoar");
            npc.value = Item.buyPrice(45, 0, 0, 0);
            npc.alpha = 255;
            npc.npcSlots = 10f;
            for (int i = 0; i < npc.buffImmune.Length; i++)
            {
                npc.buffImmune[i] = true;
            }
            if (SinsMod.Instance.SinsMusicLoaded)
            {
                Mod mod = ModLoader.GetMod("SinsModMusic");
                music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/DSDM");
            }
            else
            {
                music = MusicID.Boss3;
            }
            bossBag = mod.ItemType("TartarusBag");
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = 10000000 + 500000 * numPlayers;
            npc.damage = 500;
            npc.value = Item.buyPrice(80, 0, 0, 0);
        }
        public override void BossHeadRotation(ref float rotation)
        {
            rotation = npc.rotation;
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            for (int i = 0; i < NPCID.Sets.TrailCacheLength[npc.type]; i++)
            {
                Vector2 vector = new Vector2(Main.npcTexture[npc.type].Width / 2, Main.npcTexture[npc.type].Height / Main.npcFrameCount[npc.type] / 2);
                Color color = npc.GetAlpha(drawColor);
                color.R = (byte)(color.R * (10 - i) / 20);
                color.G = (byte)(color.G * (10 - i) / 20);
                color.B = (byte)(color.B * (10 - i) / 20);
                color.A = (byte)(color.A * (10 - i) / 20);
                Main.spriteBatch.Draw(Main.npcTexture[npc.type], new Vector2(npc.oldPos[i].X - Main.screenPosition.X + (npc.width / 2) - Main.npcTexture[npc.type].Width * npc.scale / 2f + vector.X * npc.scale, npc.oldPos[i].Y - Main.screenPosition.Y + npc.height - Main.npcTexture[npc.type].Height * npc.scale / Main.npcFrameCount[npc.type] + 4f + vector.Y * npc.scale), new Microsoft.Xna.Framework.Rectangle?(npc.frame), color, npc.rotation, vector, npc.scale, npc.spriteDirection == 1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0f);
            }
            Texture2D texture = Main.npcTexture[npc.type];
            Texture2D glowMask = mod.GetTexture("Glow/NPC/TartarusHead_Glow");
            SpriteEffects effects = (npc.spriteDirection == 1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            spriteBatch.Draw(texture, npc.Center - Main.screenPosition, new Rectangle?(npc.frame), npc.GetAlpha(drawColor), npc.rotation, npc.frame.Size() / 2f, npc.scale, effects, 0f);
            spriteBatch.Draw(glowMask, npc.Center - Main.screenPosition, new Rectangle?(npc.frame), npc.GetAlpha(Color.White), npc.rotation, npc.frame.Size() / 2f, npc.scale, effects, 0f);
            return false;
        }
        public override bool StrikeNPC(ref double damage, int defense, ref float knockback, int hitDirection, ref bool crit)
        {
            Player player = Main.player[npc.target];
            SinsPlayer modPlayer = player.GetModPlayer<SinsPlayer>();
            damage /= 2f;
            if (damage >= 40000 && !modPlayer.Dev)
            {
                damage = 0;
                return false;
            }
            if (crit)
            {
                if (damage >= 20000)
                {
                    damage = 0;
                    return false;
                }
            }
            return base.StrikeNPC(ref damage, defense, ref knockback, hitDirection, ref crit);
        }
        public override void HitEffect(int hitDirection, double damage)
        {
            if (npc.life <= 0)
            {
                float num = Main.rand.Next(-100, 100) / 100;
                Gore.NewGore(npc.position, npc.velocity * num * Main.rand.NextFloat(), mod.GetGoreSlot("Gores/Boss/TartarusHead1"), npc.scale);
                Gore.NewGore(npc.position, npc.velocity * num * Main.rand.NextFloat(), mod.GetGoreSlot("Gores/Boss/TartarusHead2"), npc.scale);
                Gore.NewGore(npc.position, npc.velocity * num * Main.rand.NextFloat(), mod.GetGoreSlot("Gores/Boss/TartarusHead3"), npc.scale);
            }
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.KillMe(PlayerDeathReason.ByNPC(10), 1000, 0, false);
        }
        public override void AI()
        {
            Vector2 value = npc.Center + (npc.rotation - 1.57079637f).ToRotationVector2() * 8f;
            Vector2 value2 = npc.rotation.ToRotationVector2() * 16f;
            Dust dust = Main.dust[Dust.NewDust(value + value2, 0, 0, 21, npc.velocity.X, npc.velocity.Y, 100, Color.Transparent, 1f + Main.rand.NextFloat() * 2f)];
            dust.noGravity = true;
            dust.noLight = true;
            dust.position -= new Vector2(4f);
            dust.fadeIn = 1f;
            dust.velocity = Vector2.Zero;
            Dust dust2 = Main.dust[Dust.NewDust(value - value2, 0, 0, 21, npc.velocity.X, npc.velocity.Y, 100, Color.Transparent, 1f + Main.rand.NextFloat() * 2f)];
            dust2.noGravity = true;
            dust2.noLight = true;
            dust2.position -= new Vector2(4f);
            dust2.fadeIn = 1f;
            dust2.velocity = Vector2.Zero;
            Player player = Main.player[npc.target];
            if (npc.target < 0 || npc.target == 255 || player.dead || !player.active)
            {
                npc.TargetClosest(true);
            }
            if (player.dead || !player.active)
            {
                player = Main.player[npc.target];
                if (player.dead || !player.active)
                {
                    Esc = true;
                }
            }
            if (Esc)
            {
                npc.TargetClosest(true);
                npc.alpha += 2;
                if (npc.alpha >= 255)
                {
                    npc.active = false;
                }
            }
            else
            {
                npc.alpha -= 10;
                if (npc.alpha < 0)
                {
                    npc.alpha = 0;
                }
            }
            if (player.Center.Y < npc.Center.Y && Vector2.Distance(Main.player[npc.target].Center, npc.Center) > 1500f)
            {
                if (player.Center.Y < npc.Center.Y && npc.velocity.Y > 0)
                {
                    npc.velocity.Y -= 5f;
                }
                if (player.Center.Y > npc.Center.Y && npc.velocity.Y < 0)
                {
                    npc.velocity.Y += 5f;
                }
            }
            if (npc.ai[3] > 0f)
            {
                npc.realLife = (int)npc.ai[3];
            }
            npc.velocity.Length();
            if (Main.netMode != 1)
            {
                if (!TailSpawned && npc.ai[0] == 0f)
                {
                    int num = npc.whoAmI;
                    for (int i = 0; i < 97; i++)
                    {
                        int num2;
                        if (i >= 0 && i < 96 && i == 3)
                        {
                            num2 = NPC.NewNPC((int)npc.position.X + npc.width / 2, (int)npc.position.Y + npc.height / 2, mod.NPCType("TartarusBody2"), npc.whoAmI, 0f, 0f, 0f, 0f, 255);
                        }
                        else if (i >= 0 && i < 96 && i % 2 == 0)
                        {
                            num2 = NPC.NewNPC((int)npc.position.X + npc.width / 2, (int)npc.position.Y + npc.height / 2, mod.NPCType("TartarusBody"), npc.whoAmI, 0f, 0f, 0f, 0f, 255);
                        }
                        else
                        {
                            if (i >= 0 && i < 96)
                            {
                                num2 = NPC.NewNPC((int)npc.position.X + npc.width / 2, (int)npc.position.Y + npc.height / 2, mod.NPCType("TartarusBody"), npc.whoAmI, 0f, 0f, 0f, 0f, 255);
                            }
                            else
                            {
                                num2 = NPC.NewNPC((int)npc.position.X + npc.width / 2, (int)npc.position.Y + npc.height / 2, mod.NPCType("TartarusTail"), npc.whoAmI, 0f, 0f, 0f, 0f, 255);
                            }
                        }
                        Main.npc[num2].realLife = npc.whoAmI;
                        Main.npc[num2].ai[2] = npc.whoAmI;
                        Main.npc[num2].ai[1] = num;
                        Main.npc[num].ai[0] = num2;
                        num = num2;
                    }
                    TailSpawned = true;
                }
                if (!npc.active && Main.netMode == 2)
                {
                    NetMessage.SendData(28, -1, -1, null, npc.whoAmI, -1f, 0f, 0f, 0, 0, 0);
                }
            }
            if (Roar)
            {
                if (Vector2.Distance(Main.player[Main.myPlayer].Center, npc.Center) < 8000f)
                {
                    Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/TartarusRoar"), (int)Main.player[Main.myPlayer].position.X, (int)Main.player[Main.myPlayer].position.Y);
                    Roar = false;
                    SinsPlayer modPlayer = player.GetModPlayer<SinsPlayer>();
                    modPlayer.shakeTime = 200;
                }
            }
            if (Vector2.Distance(Main.player[Main.myPlayer].Center, npc.Center) < 2400f && !Flame && Main.rand.Next(120) == 0)
            {
                Flame = true;
            }
            if (Flame)
            {
                AbyssalFlame();
            }
            if (Main.netMode != 1)
            {
                isSegmentAttack = true;
                npc.localAI[0] += 1f;
                if (npc.localAI[0] >= 360f)
                {
                    npc.localAI[0] = 0f;
                    npc.TargetClosest(true);
                    npc.netUpdate = true;
                    Projectile.NewProjectile(npc.Center.X + Main.rand.Next(-300, 301), npc.Center.Y + Main.rand.Next(-300, 301), 0f, 0f, mod.ProjectileType("AbyssalSphere"), Main.expertMode ? 60 : 80, 0f, Main.myPlayer, 0f, 0f);
                }
            }
            else
            {
                isSegmentAttack = false;
            }
            float num9 = 22f;//speed
            float num10 = 0.44f;//turn
            Vector2 vector2 = new Vector2(npc.position.X + npc.width * 0.5f, npc.position.Y + npc.height * 0.5f);
            float num11 = Main.player[npc.target].position.X + (Main.player[npc.target].width / 2);
            float num12 = Main.player[npc.target].position.Y + (Main.player[npc.target].height / 2);
            int num13 = -1;
            int num14 = (int)(Main.player[npc.target].Center.X / 16f);
            int num15 = (int)(Main.player[npc.target].Center.Y / 16f);
            for (int m = num14 - 2; m <= num14 + 2; m++)
            {
                for (int n = num15; n <= num15 + 15; n++)
                {
                    if (WorldGen.SolidTile2(m, n))
                    {
                        num13 = n;
                        break;
                    }
                }
                if (num13 > 0)
                {
                    break;
                }
            }
            /*if (num13 > 0)
            {
                num13 *= 16;
                float num16 = num13 - 600;
                if (Main.player[npc.target].position.Y > num16)
                {
                    num12 = num16;
                    if (Math.Abs(npc.Center.X - Main.player[npc.target].Center.X) < 600f)
                    {
                        if (npc.velocity.X > 0f)
                        {
                            num11 = Main.player[npc.target].Center.X + 600f;
                        }
                        else
                        {
                            num11 = Main.player[npc.target].Center.X - 600f;
                        }
                    }
                }
            }
            else
            {
                num9 = 26f;
                num10 = 0.8f;
            }*/
            if (Main.rand.Next(500) == 0 && Main.expertMode)
            {
                Dash = true;
            }
            if (Dash)
            {
                float speed = MathHelper.Clamp(num9 + 42f * (1.025f - npc.life / npc.lifeMax), num9, num9 + 42f);
                float dist = Vector2.Distance(npc.Center, player.Center);
                if (dist > 240f)
                {
                    Vector2 Velocity = npc.DirectionTo(player.Center) * speed;
                    npc.velocity = Vector2.Lerp(npc.velocity, Velocity, 0.0333333351f);
                }
                DashTime++;
                if ((DashTime > 240) || (DashTime > 40 && Main.rand.Next(60) == 0))
                {
                    Dash = false;
                    DashTime = 0;
                }
            }
            float num17 = num9 * 1.3f;
            float num18 = num9 * 0.7f;
            float num19 = npc.velocity.Length();
            if (num19 > 0f)
            {
                if (num19 > num17)
                {
                    npc.velocity.Normalize();
                    npc.velocity *= num17;
                }
                else
                {
                    if (num19 < num18)
                    {
                        npc.velocity.Normalize();
                        npc.velocity *= num18;
                    }
                }
            }
            if (num13 > 0)
            {
                for (int num20 = 0; num20 < 200; num20++)
                {
                    if (Main.npc[num20].active && Main.npc[num20].type == npc.type && num20 != npc.whoAmI)
                    {
                        Vector2 vector3 = Main.npc[num20].Center - npc.Center;
                        if (vector3.Length() < 400f)
                        {
                            vector3.Normalize();
                            vector3 *= 1000f;
                            num11 -= vector3.X;
                            num12 -= vector3.Y;
                        }
                    }
                }
            }
            else
            {
                for (int num21 = 0; num21 < 200; num21++)
                {
                    if (Main.npc[num21].active && Main.npc[num21].type == npc.type && num21 != npc.whoAmI)
                    {
                        Vector2 vector4 = Main.npc[num21].Center - npc.Center;
                        if (vector4.Length() < 60f)
                        {
                            vector4.Normalize();
                            vector4 *= 200f;
                            num11 -= vector4.X;
                            num12 -= vector4.Y;
                        }
                    }
                }
            }
            num11 = (int)(num11 / 16f) * 16;
            num12 = (int)(num12 / 16f) * 16;
            vector2.X = (int)(vector2.X / 16f) * 16;
            vector2.Y = (int)(vector2.Y / 16f) * 16;
            num11 -= vector2.X;
            num12 -= vector2.Y;
            float num22 = (float)Math.Sqrt(num11 * num11 + num12 * num12);
            if (npc.ai[1] > 0f && npc.ai[1] < Main.npc.Length)
            {
                try
                {
                    vector2 = new Vector2(npc.position.X + npc.width * 0.5f, npc.position.Y + npc.height * 0.5f);
                    num11 = Main.npc[(int)npc.ai[1]].position.X + (Main.npc[(int)npc.ai[1]].width / 2) - vector2.X;
                    num12 = Main.npc[(int)npc.ai[1]].position.Y + (Main.npc[(int)npc.ai[1]].height / 2) - vector2.Y;
                }
                catch
                { }
                npc.rotation = (float)Math.Atan2(num12, num11) + 1.57f;
                num22 = (float)Math.Sqrt(num11 * num11 + num12 * num12);
                int width = npc.width;
                num22 = (num22 - width) / num22;
                num11 *= num22;
                num12 *= num22;
                npc.velocity = Vector2.Zero;
                npc.position.X = npc.position.X + num11;
                npc.position.Y = npc.position.Y + num12;
            }
            else
            {
                num22 = (float)Math.Sqrt(num11 * num11 + num12 * num12);
                float num23 = Math.Abs(num11);
                float num24 = Math.Abs(num12);
                float num25 = num9 / num22;
                num11 *= num25;
                num12 *= num25;
                if ((npc.velocity.X > 0f && num11 > 0f) || (npc.velocity.X < 0f && num11 < 0f) || (npc.velocity.Y > 0f && num12 > 0f) || (npc.velocity.Y < 0f && num12 < 0f))
                {
                    if (npc.velocity.X < num11)
                    {
                        npc.velocity.X = npc.velocity.X + num10;
                    }
                    else
                    {
                        if (npc.velocity.X > num11)
                        {
                            npc.velocity.X = npc.velocity.X - num10;
                        }
                    }
                    if (npc.velocity.Y < num12)
                    {
                        npc.velocity.Y = npc.velocity.Y + num10;
                    }
                    else
                    {
                        if (npc.velocity.Y > num12)
                        {
                            npc.velocity.Y = npc.velocity.Y - num10;
                        }
                    }
                    if (Math.Abs(num12) < num9 * 0.2 && ((npc.velocity.X > 0f && num11 < 0f) || (npc.velocity.X < 0f && num11 > 0f)))
                    {
                        if (npc.velocity.Y > 0f)
                        {
                            npc.velocity.Y = npc.velocity.Y + num10 * 2f;
                        }
                        else
                        {
                            npc.velocity.Y = npc.velocity.Y - num10 * 2f;
                        }
                    }
                    if (Math.Abs(num11) < num9 * 0.2 && ((npc.velocity.Y > 0f && num12 < 0f) || (npc.velocity.Y < 0f && num12 > 0f)))
                    {
                        if (npc.velocity.X > 0f)
                        {
                            npc.velocity.X = npc.velocity.X + num10 * 2f;
                        }
                        else
                        {
                            npc.velocity.X = npc.velocity.X - num10 * 2f;
                        }
                    }
                }
                else
                {
                    if (num23 > num24)
                    {
                        if (npc.velocity.X < num11)
                        {
                            npc.velocity.X = npc.velocity.X + num10 * 1.1f;
                        }
                        else
                        {
                            if (npc.velocity.X > num11)
                            {
                                npc.velocity.X = npc.velocity.X - num10 * 1.1f;
                            }
                        }
                        if ((Math.Abs(npc.velocity.X) + Math.Abs(npc.velocity.Y)) < num9 * 0.5)
                        {
                            if (npc.velocity.Y > 0f)
                            {
                                npc.velocity.Y = npc.velocity.Y + num10;
                            }
                            else
                            {
                                npc.velocity.Y = npc.velocity.Y - num10;
                            }
                        }
                    }
                    else
                    {
                        if (npc.velocity.Y < num12)
                        {
                            npc.velocity.Y = npc.velocity.Y + num10 * 1.1f;
                        }
                        else
                        {
                            if (npc.velocity.Y > num12)
                            {
                                npc.velocity.Y = npc.velocity.Y - num10 * 1.1f;
                            }
                        }
                        if ((Math.Abs(npc.velocity.X) + Math.Abs(npc.velocity.Y)) < num9 * 0.5)
                        {
                            if (npc.velocity.X > 0f)
                            {
                                npc.velocity.X = npc.velocity.X + num10;
                            }
                            else
                            {
                                npc.velocity.X = npc.velocity.X - num10;
                            }
                        }
                    }
                }
            }
            npc.rotation = (float)Math.Atan2(npc.velocity.Y, npc.velocity.X) + 1.57f;
        }
        public void AbyssalFlame()
        {
            if (FlameCounter % 4 == 0)
            {
                if (npc.soundDelay == 0)
                {
                    npc.soundDelay = 3;
                    Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 20, 1f, 0f);
                    Vector2 vector = new Vector2(npc.velocity.X, npc.velocity.Y);
                    if (vector == Vector2.Zero)
                    {
                        vector = new Vector2(0f, -1f);
                    }
                    vector.Normalize();
                    vector *= 24f;
                    float num = MathHelper.ToRadians(2.5f);
                    int num2 = 3;
                    for (int i = 0; i < num2; i++)
                    {
                        Vector2 vector2 = Utils.RotatedBy(new Vector2(vector.X, vector.Y), MathHelper.Lerp(-num, num, i / (num2 - 1f)), default(Vector2));
                        int num3 = Projectile.NewProjectile(npc.Center.X, npc.Center.Y, vector2.X, vector2.Y, mod.ProjectileType("AbyssalFlames"), 100, 1f, Main.myPlayer);
                    }
                }
            }
            if (FlameCounter >= 60)
            {
                Flame = false;
                FlameCounter = 0;
                return;
            }
            FlameCounter += 1;
        }
        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 1.2f;
            return null;
        }
        public override void BossLoot(ref string name, ref int potionType)
        {
            Player player = Main.player[npc.target];
            float num = 1E+08f;
            Vector2 position = npc.position;
            for (int i = 0; i < 200; i++)
            {
                if (Main.npc[i].active && (Main.npc[i].type == mod.NPCType("TartarusHead") || Main.npc[i].type == mod.NPCType("TartarusBody") || Main.npc[i].type == mod.NPCType("TartarusBody2") || Main.npc[i].type == mod.NPCType("TartarusTail") || Main.npc[i].type == mod.NPCType("TartarusWings")))
                {
                    float num2 = Math.Abs(Main.npc[i].Center.X - player.Center.X) + Math.Abs(Main.npc[i].Center.Y - player.Center.Y);
                    if (num2 < num)
                    {
                        num = num2;
                        position = Main.npc[i].position;
                    }
                }
            }
            npc.position = position;
            potionType = mod.ItemType("LifeElixir");
            if (Main.rand.Next(1000000) == 0)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("Error403Forbidden"));
            }
            if (Main.rand.Next(10) == 0)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("TartarusTrophy"));
            }
            if (Main.expertMode)
            {
                npc.DropBossBags();
            }
            else
            {
                if (Main.rand.Next(7) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("TartarusMask"));
                }
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("Axion"), Main.rand.Next(7, 15));
                switch (Main.rand.Next(4))
                {
                    case 0:
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("TartarusWhip"));
                        break;
                    case 1:
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("AbyssalFlamethrower"));
                        break;
                    case 2:
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("AbyssalStaff"));
                        break;
                    case 3:
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("AbyssalGuardianStaff"));
                        break;
                }
            }
            SinsWorld.downedTartarus = true;
        }
        public override bool CheckActive()
        {
            return false;
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (spawnInfo.playerSafe)
            {
                return 0f;
            }
            if (!SinsWorld.downedTartarus && SinsWorld.downedSins && !NPC.AnyNPCs(mod.NPCType("TartarusHead")) && spawnInfo.player.GetModPlayer<SinsPlayer>().ZoneTartarus)
            {
                return 0.008f;
            }
            return 0f;
        }
    }
}