using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.NPCs.Boss.Madness
{
    [AutoloadBossHead]
    public class BCCSummonHeal : ModNPC
    {
        private bool Start = false;
        private bool Esc = false;
        private int EscTimer;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Nature Core");
            Main.npcFrameCount[npc.type] = 8;
            NPCID.Sets.TrailingMode[npc.type] = 1;
        }
        public override void SetDefaults()
        {
            npc.width = 56;
            npc.height = 56;
            if (BlackCrystalCore.isThirdPhase)
            {
                npc.lifeMax = 150000;
                npc.damage = 20;
            }
            else if (BlackCrystalCore.isSecondPhase)
            {
                npc.lifeMax = 100000;
                npc.damage = 100;
            }
            else
            {
                npc.lifeMax = 75000;
                npc.damage = 50;
            }
            npc.defense = 0;
            npc.knockBackResist = 0f;
            npc.aiStyle = -1;//14
            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.HitSound = mod.GetLegacySoundSlot(SoundType.NPCHit, "Sounds/NPCHit/BCHit");
            npc.DeathSound = mod.GetLegacySoundSlot(SoundType.NPCKilled, "Sounds/NPCKilled/BCCCKilled");
            npc.npcSlots = 10f;
            npc.netAlways = true;
            npc.scale = 0.8f;
            npc.value = Item.buyPrice(0, 0, 0, 0);
            for (int i = 0; i < npc.buffImmune.Length; i++)
            {
                npc.buffImmune[i] = true;
            }
            if (SinsMod.Instance.SinsMusicLoaded)
            {
                Mod mod = ModLoader.GetMod("SinsModMusic");
                music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/MOL");
            }
            else
            {
                music = MusicID.Boss2;
            }
            npc.GetGlobalNPC<SinsNPC>().trail = true;
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            if (BlackCrystalCore.isThirdPhase)
            {
                npc.lifeMax = 200000 + 25000 * numPlayers;
                npc.damage = 300;
            }
            else if (BlackCrystalCore.isSecondPhase)
            {
                npc.lifeMax = 150000 + 25000 * numPlayers;
                npc.damage = 200;
            }
            else
            {
                npc.lifeMax = 100000 + 25000 * numPlayers;
                npc.damage = 150;
            }
        }
        public override void FindFrame(int frameHeight)
        {
            npc.spriteDirection = 0;
            npc.frameCounter++;
            if (npc.frameCounter >= 4)
            {
                npc.frameCounter = 0;
                npc.frame.Y = npc.frame.Y + frameHeight;
            }
            if (npc.frame.Y / frameHeight >= Main.npcFrameCount[npc.type])
            {
                npc.frame.Y = 0;
            }
        }
        public override void HitEffect(int hitDirection, double damage)
        {
            for (int i = 0; i < 2; i++)
            {
                int d = Dust.NewDust(npc.position, npc.width, npc.height, 235, hitDirection, -1f, 50, default(Color), 0.9f);
                Main.dust[d].shader = GameShaders.Armor.GetSecondaryShader(44, Main.LocalPlayer);
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
                    int num = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, 235, 0f, 0f, 100, default(Color), 1.2f);
                    Main.dust[num].velocity *= 3f;
                    Main.dust[num].shader = GameShaders.Armor.GetSecondaryShader(44, Main.LocalPlayer);
                    if (Main.rand.Next(2) == 0)
                    {
                        Main.dust[num].scale = 0.5f;
                        Main.dust[num].fadeIn = 1f + Main.rand.Next(10) * 0.1f;
                    }
                }
                for (int k = 0; k < 40; k++)
                {
                    int num2 = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, 245, 0f, 0f, 100, default(Color), 1.0f);
                    Main.dust[num2].noGravity = true;
                    Main.dust[num2].velocity *= 5f;
                    num2 = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, 245, 0f, 0f, 100, default(Color), 1.2f);
                    Main.dust[num2].velocity *= 2f;
                }
            }
        }
        public override bool StrikeNPC(ref double damage, int defense, ref float knockback, int hitDirection, ref bool crit)
        {
            return false;
        }
        public override void ModifyHitByItem(Player player, Item item, ref int damage, ref float knockback, ref bool crit)
        {
            if (BlackCrystalCore.isMeleeResist || (BlackCrystalCore.isRangedResist && item.ranged) || (BlackCrystalCore.isMagicResist && item.magic) || (BlackCrystalCore.isThrownResist && item.thrown) || (BlackCrystalCore.isSummonResist && item.summon))
            {
                damage = 0;
                Main.PlaySound(mod.GetLegacySoundSlot(SoundType.NPCHit, "Sounds/NPCHit/BCResist").WithVolume(0.5f), (int)npc.position.X, (int)npc.position.Y);
            }
            else if (!item.melee && !item.magic && !item.ranged && !item.thrown && !item.summon)
            {
                damage = 0;
                Main.PlaySound(mod.GetLegacySoundSlot(SoundType.NPCHit, "Sounds/NPCHit/BCResist").WithVolume(0.5f), (int)npc.position.X, (int)npc.position.Y);
            }
        }
        public override void ModifyHitByProjectile(Projectile projectile, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if ((projectile.penetrate == -1 || projectile.penetrate > 10) && !projectile.minion && !projectile.sentry)
            {
                projectile.penetrate = 8;
            }
            if (!projectile.melee && !projectile.magic && !projectile.ranged && !projectile.thrown && !projectile.minion && !projectile.sentry)
            {
                damage = 0;
                Main.PlaySound(mod.GetLegacySoundSlot(SoundType.NPCHit, "Sounds/NPCHit/BCResist").WithVolume(0.5f), (int)npc.position.X, (int)npc.position.Y);
            }
            if ((BlackCrystalCore.isMeleeResist && projectile.melee) || (BlackCrystalCore.isRangedResist && projectile.ranged) || (BlackCrystalCore.isMagicResist && projectile.magic) || (BlackCrystalCore.isThrownResist && projectile.thrown) || (BlackCrystalCore.isSummonResist && (projectile.minion || projectile.sentry)))
            {
                damage = 0;
                Main.PlaySound(mod.GetLegacySoundSlot(SoundType.NPCHit, "Sounds/NPCHit/BCResist").WithVolume(0.5f), (int)npc.position.X, (int)npc.position.Y);
            }
        }
        public override bool PreAI()
        {
            npc.boss = true;
            npc.spriteDirection = 0;
            if (npc.life > npc.lifeMax)
            {
                npc.life = npc.lifeMax;
            }
            return true;
        }
        public override void AI()
        {
            Player player = Main.player[npc.target];
            if (!NPC.AnyNPCs(mod.NPCType("BlackCrystalCore")))
            {
                npc.active = false;
                return;
            }
            if (!Start)
            {
                for (int i = 0; i < 15; i++)
                {
                    int d = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, 235, 0f, 0f, 100, default(Color), 1.5f);
                    Main.dust[d].shader = GameShaders.Armor.GetSecondaryShader(44, Main.LocalPlayer);
                }
                Start = true;
            }
            if (npc.life < npc.lifeMax)
            {
                npc.life += Main.expertMode ? 10 : 5;
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
            npc.ai[0]++;
            if (npc.ai[0] >= 180)
            {
                npc.velocity = npc.DirectionTo(player.Center) * Main.rand.Next(12, 19);
                npc.ai[0] = 0;
            }

            npc.noGravity = true;
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
            npc.TargetClosest(true);
            float[] acceleX = new float[2];
            acceleX[0] = 1f;//0.1
            acceleX[1] = 0.5f;//0.05
            float[] acceleY = new float[3];
            acceleY[0] = 0.4f;//0.04
            acceleY[1] = 0.5f;//0.05
            acceleY[2] = 0.3f;//0.03
            float maxVelX = 16f;//4
            float maxVelY = 9f;//1.5
            if (npc.direction == -1 && npc.velocity.X > -maxVelX)
            {
                npc.velocity.X = npc.velocity.X - acceleX[0];
                if (npc.velocity.X > maxVelX)
                {
                    npc.velocity.X = npc.velocity.X - acceleX[0];
                }
                else
                {
                    if (npc.velocity.X > 0f)
                    {
                        npc.velocity.X = npc.velocity.X + acceleX[1];
                    }
                }
                if (npc.velocity.X < -maxVelX)
                {
                    npc.velocity.X = -maxVelX;
                }
            }
            else
            {
                if (npc.direction == 1 && npc.velocity.X < maxVelX)
                {
                    npc.velocity.X = npc.velocity.X + acceleX[0];
                    if (npc.velocity.X < -maxVelX)
                    {
                        npc.velocity.X = npc.velocity.X + acceleX[0];
                    }
                    else
                    {
                        if (npc.velocity.X < 0f)
                        {
                            npc.velocity.X = npc.velocity.X - acceleX[1];
                        }
                    }
                    if (npc.velocity.X > maxVelX)
                    {
                        npc.velocity.X = maxVelX;
                    }
                }
            }
            if (npc.directionY == -1 && npc.velocity.Y > -maxVelY)
            {
                npc.velocity.Y = npc.velocity.Y - acceleY[0];
                if (npc.velocity.Y > maxVelY)
                {
                    npc.velocity.Y = npc.velocity.Y - acceleY[1];
                }
                else
                {
                    if (npc.velocity.Y > 0f)
                    {
                        npc.velocity.Y = npc.velocity.Y + acceleY[2];
                    }
                }
                if (npc.velocity.Y < -maxVelY)
                {
                    npc.velocity.Y = -maxVelY;
                }
            }
            else
            {
                if (npc.directionY == 1 && npc.velocity.Y < maxVelY)
                {
                    npc.velocity.Y = npc.velocity.Y + acceleY[0];
                    if (npc.velocity.Y < -maxVelY)
                    {
                        npc.velocity.Y = npc.velocity.Y + acceleY[1];
                    }
                    else
                    {
                        if (npc.velocity.Y < 0f)
                        {
                            npc.velocity.Y = npc.velocity.Y - acceleY[2];
                        }
                    }
                    if (npc.velocity.Y > maxVelY)
                    {
                        npc.velocity.Y = maxVelY;
                    }
                }
            }
            npc.ai[1] += 1f;
            //npc.ai[1] += 1f;
            if (npc.ai[1] > 200f)
            {
                //if (!Main.player[npc.target].wet && Collision.CanHit(npc.position, npc.width, npc.height, Main.player[npc.target].position, Main.player[npc.target].width, Main.player[npc.target].height))
                {
                    npc.ai[1] = 0f;
                }
                float acceleX2 = 0.2f;
                float acceleY2 = 0.1f;
                float maxVelX2 = 4f;
                float maxVelY2 = 1.5f;
                acceleX2 = 1.2f;
                acceleY2 = 0.7f;
                maxVelX2 = 32f;
                maxVelY2 = 12.5f;
                if (npc.ai[1] > 1000f)
                {
                    npc.ai[1] = 0f;
                }
                npc.ai[2] += 1f;
                if (npc.ai[2] > 0f)
                {
                    if (npc.velocity.Y < maxVelY2)
                    {
                        npc.velocity.Y = npc.velocity.Y + acceleY2;
                    }
                }
                else
                {
                    if (npc.velocity.Y > -maxVelY2)
                    {
                        npc.velocity.Y = npc.velocity.Y - acceleY2;
                    }
                }
                if (npc.ai[2] < -150f || npc.ai[2] > 150f)
                {
                    if (npc.velocity.X < maxVelX2)
                    {
                        npc.velocity.X = npc.velocity.X + acceleX2;
                    }
                }
                else
                {
                    if (npc.velocity.X > -maxVelX2)
                    {
                        npc.velocity.X = npc.velocity.X - acceleX2;
                    }
                }
                if (npc.ai[2] > 300f)
                {
                    npc.ai[2] = -300f;
                }
            }
            if (Main.netMode != 1)
            {
                /*if (npc.type == 48)
                {
                    npc.ai[0] += 1f;
                    if (npc.ai[0] == 30f || npc.ai[0] == 60f || npc.ai[0] == 90f)
                    {
                        if (Collision.CanHit(npc.position, npc.width, npc.height, Main.player[npc.target].position, Main.player[npc.target].width, Main.player[npc.target].height))
                        {
                            float num213 = 6f;
                            Vector2 vector27 = new Vector2(npc.position.X + npc.width * 0.5f, npc.position.Y + npc.height * 0.5f);
                            float num214 = Main.player[npc.target].position.X + Main.player[npc.target].width * 0.5f - vector27.X + Main.rand.Next(-100, 101);
                            float num215 = Main.player[npc.target].position.Y + Main.player[npc.target].height * 0.5f - vector27.Y + Main.rand.Next(-100, 101);
                            float num216 = (float)Math.Sqrt(num214 * num214 + num215 * num215);
                            num216 = num213 / num216;
                            num214 *= num216;
                            num215 *= num216;
                            int num217 = 15;
                            int num218 = 38;
                            int num219 = Projectile.NewProjectile(vector27.X, vector27.Y, num214, num215, num218, num217, 0f, Main.myPlayer, 0f, 0f);
                            Main.projectile[num219].timeLeft = 300;
                        }
                    }
                    else
                    {
                        if (npc.ai[0] >= 400 + Main.rand.Next(400))
                        {
                            npc.ai[0] = 0f;
                        }
                    }
                }*/
            }
        }
        public override bool CheckActive()
        {
            return false;
        }
        public override bool PreNPCLoot()
        {
            return false;
        }
    }
}