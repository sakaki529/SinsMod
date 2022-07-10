using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.NPCs.Boss.Madness
{
    [AutoloadBossHead]
    public class BlackCrystal : ModNPC
    {
        private bool FirstPhase = true;
        private bool SecondPhase;
        private bool NoCrystalsPhase;
        private bool Esc;
        private int EscTimer;
        private int movement;
        private int[] Count = new int[3];
        private bool ChargeMode;
        private bool SpawnedCrystals;
        private int SmallCrystalsCount;
        private float shieldOpacity;
        internal float getShieldOpacity;
        public override string BossHeadTexture => "SinsMod/NPCs/Boss/Madness/Madness_Head_Boss";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Black Crystal");
            NPCID.Sets.TrailingMode[npc.type] = 1;
        }
        public override void SetDefaults()
        {
            npc.width = 42;
            npc.height = 82;
            npc.lifeMax = 6000000; 
            npc.damage = 600;
            npc.defense = 90;
            npc.knockBackResist = 0f;
            npc.aiStyle = -1;
            aiType = -1;
            npc.boss = true;
            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.HitSound = mod.GetLegacySoundSlot(SoundType.NPCHit, "Sounds/NPCHit/BCHit");
            npc.DeathSound = mod.GetLegacySoundSlot(SoundType.NPCKilled, "Sounds/NPCKilled/BCKilled");
            npc.npcSlots = 10f;
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
            npc.GetGlobalNPC<SinsNPC>().trail = true;
            npc.GetGlobalNPC<SinsNPC>().drawCenter = true;
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = 7500000 + 250000 * numPlayers;
            npc.damage = 900;
        }
        public override void PostDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            if (shieldOpacity > 0)
            {
                if (shieldOpacity > 1)
                {
                    shieldOpacity = 1;
                }
                if (shieldOpacity < 0)
                {
                    shieldOpacity = 0;
                }
                Texture2D texture = mod.GetTexture("Extra/NPC/Shield");
                SpriteEffects spriteEffects = SpriteEffects.None;
                Color color = Color.Lerp(SinsColor.MediumBlack, Color.Black, (float)Math.Cos(6.28318548f * (Main.LocalPlayer.miscCounter / 100f)) * 0.4f + 0.5f);
                Main.spriteBatch.Draw(texture, npc.Center + npc.Size / 2 - texture.Size() / 2 - Main.screenPosition, new Rectangle(0, 0, texture.Width, texture.Height), color * shieldOpacity, 0, npc.frame.Size() / 2f, npc.scale, spriteEffects, 0f);
            }
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
                npc.width = 50;
                npc.height = 50;
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
                    Main.dust[num2].shader = GameShaders.Armor.GetSecondaryShader(56, Main.LocalPlayer);
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
        public void SpawnCrystals()
        {
            int num = NPC.NewNPC((int)(npc.position.X + (npc.width / 2)), (int)npc.position.Y + npc.height / 2, mod.NPCType("BlackCrystalSmall"), npc.whoAmI, 4f, npc.whoAmI, 0f, 0f, 255);//laser LA
            Main.npc[num].target = npc.target;
            Main.npc[num].netUpdate = true;
            num = NPC.NewNPC((int)(npc.position.X + (npc.width / 2)), (int)npc.position.Y + npc.height / 2, mod.NPCType("BlackCrystalSmall"), npc.whoAmI, 3f, npc.whoAmI, 0f, 0f, 255);//cannon RA
            Main.npc[num].target = npc.target;
            Main.npc[num].netUpdate = true;
            num = NPC.NewNPC((int)(npc.position.X + (npc.width / 2)), (int)npc.position.Y + npc.height / 2, mod.NPCType("BlackCrystalSmall"), npc.whoAmI, 2f, npc.whoAmI, 0f, 0f, 255);//vice LB?
            Main.npc[num].target = npc.target;
            Main.npc[num].netUpdate = true;
            num = NPC.NewNPC((int)(npc.position.X + (npc.width / 2)), (int)npc.position.Y + npc.height / 2, mod.NPCType("BlackCrystalSmall"), npc.whoAmI, 1f, npc.whoAmI, 0f, 0f, 255);//saw LA?
            Main.npc[num].target = npc.target;
            Main.npc[num].netUpdate = true;
            SpawnedCrystals = true;
        }
        public override bool PreAI()
        {
            getShieldOpacity = shieldOpacity;
            SmallCrystalsCount = NPC.CountNPCS(mod.NPCType("BlackCrystalSmall"));
            npc.spriteDirection = 0;
            if (!SpawnedCrystals)
            {
                SpawnCrystals();
            }
            NoCrystalsPhase = SmallCrystalsCount == 0;
            npc.GetGlobalNPC<SinsNPC>().defenceMode = !NoCrystalsPhase || shieldOpacity > 0.5f;
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
            if (npc.life < npc.lifeMax / 4)
            {
                npc.reflectingProjectiles = true;
            }
            if (npc.life < npc.lifeMax / 2 && !SecondPhase)
            {
                SpawnCrystals();
                SecondPhase = true;
            }
            if (NoCrystalsPhase)
            {
                switch (movement)
                {
                    case 0:
                        Reset();
                        movement = Main.rand.Next(1, 3);
                        break;
                    case 1://charge
                        movement = 0;
                        break;
                    case 2://shadow
                        npc.velocity = Vector2.Zero;
                        npc.rotation += shieldOpacity / 4;
                        if (Count[0] == 0)
                        {
                            npc.rotation = 0f;
                            Count[0] = 1;
                        }
                        if (Count[0] == 1)
                        {
                            if (shieldOpacity < 1)
                            {
                                shieldOpacity += 0.02f;
                            }
                            else
                            {
                                Count[1] += 1;
                                if (Count[1] % 30 == 0)
                                {
                                    float speed = Utils.NextFloat(Main.rand, 16f, 20f);
                                    Vector2 vector = new Vector2(Main.rand.Next(-100, 100), Main.rand.Next(-100, 100));
                                    vector.Normalize();
                                    vector.X *= speed;
                                    vector.Y *= speed;
                                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y, vector.X, vector.Y, mod.ProjectileType("BlackCrystalShadow"), 200, 0f, Main.myPlayer, 0f, 0f);
                                }
                                if (Count[1] > 300)
                                {
                                    Count[0] = 2;
                                }
                            }
                        }
                        else if (Count[0] == 2)
                        {
                            if (shieldOpacity > 0)
                            {
                                shieldOpacity -= 0.02f;
                            }
                            if (shieldOpacity <= 0)
                            {
                                Count[0] = 3;
                            }
                        }
                        if (Count[0] == 3)
                        {
                            movement = 0;
                        }
                        break;
                    case 3:
                        break;
                }
            }
            else
            {
                npc.rotation = npc.velocity.X / 30f;
                int ChargeSpeed = (SecondPhase ? 38 : 30) + 2 * (4 - SmallCrystalsCount);
                if (!ChargeMode)
                {
                    Count[1] += 1;
                    if (Count[1] >= 600)
                    {
                        if (Main.expertMode || SecondPhase)
                        {
                            npc.ai[1] = 1f;
                            ChargeMode = true;
                        }
                        Count[1] = 0;
                    }
                    if (Count[1] % 90 == 0)
                    {
                        if (SmallCrystalsCount < 4)
                        {
                            Projectile.NewProjectile(npc.Center.X, npc.Center.Y, 0f, 0f, mod.ProjectileType("SpreadShortcut"), Main.expertMode ? 90 : 120, 1f, Main.myPlayer, mod.ProjectileType("BlackMatter"), 1f);
                        }
                    }
                    float num2 = 0.12f;
                    float num3 = 6f;
                    float num4 = 0.3f;
                    float num5 = 24f;
                    if (npc.position.Y > player.position.Y - 250f)
                    {
                        if (npc.velocity.Y > 0f)
                        {
                            npc.velocity.Y = npc.velocity.Y * 0.98f;
                        }
                        npc.velocity.Y = npc.velocity.Y - num2;
                        if (npc.velocity.Y > num3)
                        {
                            npc.velocity.Y = num3;
                        }
                    }
                    else if (npc.position.Y < player.position.Y - 250f)
                    {
                        if (npc.velocity.Y < 0f)
                        {
                            npc.velocity.Y = npc.velocity.Y * 0.98f;
                        }
                        npc.velocity.Y = npc.velocity.Y + num2;
                        if (npc.velocity.Y < -num3)
                        {
                            npc.velocity.Y = -num3;
                        }
                    }
                    if (npc.position.X + (npc.width / 2) > player.position.X + (player.width / 2))
                    {
                        if (npc.velocity.X > 0f)
                        {
                            npc.velocity.X = npc.velocity.X * 0.98f;
                        }
                        npc.velocity.X = npc.velocity.X - num4;
                        if (npc.velocity.X > num5)
                        {
                            npc.velocity.X = num5;
                        }
                    }
                    if (npc.position.X + (npc.width / 2) < player.position.X + (player.width / 2))
                    {
                        if (npc.velocity.X < 0f)
                        {
                            npc.velocity.X = npc.velocity.X * 0.98f;
                        }
                        npc.velocity.X = npc.velocity.X + num4;
                        if (npc.velocity.X < -num5)
                        {
                            npc.velocity.X = -num5;
                        }
                    }
                }
                else
                {
                    if (Main.expertMode && SecondPhase)
                    {
                        Count[0]++;
                    }
                    Count[0]++;
                    if (Count[0] == 2)
                    {
                        Main.PlaySound(15, (int)npc.position.X, (int)npc.position.Y, 0, 1f, 0f);
                    }
                    if (Count[0] >= 400)
                    {
                        npc.ai[1] = 0f;
                        Count[0] = 0;
                        ChargeMode = false;
                    }
                    npc.ai[0] += 1f;
                    float dist = Vector2.Distance(npc.Center, player.Center);
                    if (npc.ai[0] > 0f && dist > 60f)
                    {
                        npc.ai[0] = 0f;
                        Vector2 Velocity = npc.DirectionTo(player.Center) * ChargeSpeed;
                        npc.velocity = Vector2.Lerp(npc.velocity, Velocity, 0.0333333351f);
                    }
                }
            }
        }
        private void ShotCrystals()
        {
            Vector2 vector = new Vector2(npc.position.X + npc.width * 0.5f, npc.position.Y + npc.height * 0.5f);
            float num = Main.player[npc.target].position.X + (Main.player[npc.target].width / 2) - vector.X;
            float num2 = Main.player[npc.target].position.Y + (Main.player[npc.target].height / 2) - vector.Y;
            float num3 = (float)Math.Sqrt(num * num + num2 * num2);
            float num4 = 8f;
            num3 = num4 / num3;
            num *= num3;
            num2 *= num3;
            for (int i = 0; i < (SecondPhase ? Main.rand.Next(2, 5) : Main.rand.Next(2, 4)); i++)
            {
                num += Main.rand.Next(-40, 41) * 0.02f;
                num2 += Main.rand.Next(-40, 41) * 0.02f;
                vector.X += num * 8f;
                vector.Y += num2 * 8f;
                int num6 = Projectile.NewProjectile(vector.X, vector.Y, num / 2f, num2 / 2f, mod.ProjectileType(""), Main.expertMode ? 70 : 120, 0f, Main.myPlayer, 0f, 0f);
                Main.projectile[num6].velocity *= 2.2f;
                Main.projectile[num6].timeLeft = 120;
            }
        }
        private void Reset()
        {
            Count[0] = 0;
            Count[1] = 0;
            Count[2] = 0;
        }
        public override bool CheckDead()
        {
            NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("WillOfMadness"), npc.whoAmI, 32767);
            return true;
        }
        public override bool CheckActive()
        {
            return false;
        }
        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 1.2f;
            return null;
        }
        public override bool PreNPCLoot()
        {
            return false;
        }
    }
}