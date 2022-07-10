using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.NPCs.Boss.Madness
{
    [AutoloadBossHead]
    public class BCCSummonAttack : ModNPC
    {
        private bool Start = false;
        private bool Esc = false;
        private int EscTimer;
        private int movementCounter;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Void Core");
            Main.npcFrameCount[npc.type] = 8;
            NPCID.Sets.TrailingMode[npc.type] = 1;
        }
        public override void SetDefaults()
        {
            npc.width = 56;
            npc.height = 56;
            if (BlackCrystalCore.isThirdPhase)
            {
                npc.lifeMax = 250000;
                npc.damage = 500;
            }
            else if (BlackCrystalCore.isSecondPhase)
            {
                npc.lifeMax = 200000;
                npc.damage = 400;
            }
            else
            {
                npc.lifeMax = 150000;
                npc.damage = 300;
            }
            npc.defense = 0;
            npc.knockBackResist = 0f;
            npc.aiStyle = -1;
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
                npc.lifeMax = 300000 + 50000 * numPlayers;
                npc.damage = 600;
            }
            else if (BlackCrystalCore.isSecondPhase)
            {
                npc.lifeMax = 250000 + 50000 * numPlayers;
                npc.damage = 500;
            }
            else
            {
                npc.lifeMax = 200000 + 50000 * numPlayers;
                npc.damage = 400;
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
                npc.ai[1] = npc.ai[0];
                Start = true;
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
            movementCounter++;
            npc.TargetClosest(true);
            if (movementCounter < 800)
            {
                Vector2 direction = Main.player[npc.target].Center - npc.Center;
                direction.Normalize();
                npc.velocity *= 0.985f;
                int dust = Dust.NewDust(npc.position + npc.velocity, npc.width, npc.height, 206, npc.velocity.X * 0.5f, npc.velocity.Y * 0.5f, 0, default(Color), 1f);
                Main.dust[dust].noGravity = true;
                if (Math.Sqrt(npc.velocity.X * npc.velocity.X + npc.velocity.Y * npc.velocity.Y) >= 7.0)
                {
                    int dust2 = Dust.NewDust(npc.position + npc.velocity, npc.width, npc.height, 206, npc.velocity.X * 0.5f, npc.velocity.Y * 0.5f, 0, default(Color), 1f);
                    Main.dust[dust2].noGravity = true;
                    Main.dust[dust2].scale = 2f;
                    dust2 = Dust.NewDust(npc.position + npc.velocity, npc.width, npc.height, 206, npc.velocity.X * 0.5f, npc.velocity.Y * 0.5f, 0, default(Color), 1f);
                    Main.dust[dust2].noGravity = true;
                    Main.dust[dust2].scale = 2f;
                }
                if (Math.Sqrt(npc.velocity.X * npc.velocity.X + npc.velocity.Y * npc.velocity.Y) < 16.0)
                {
                    if (Main.rand.Next(18) == 0)
                    {
                        direction.X *= Main.rand.Next(20, 33);
                        direction.Y *= Main.rand.Next(20, 33);
                        npc.velocity.X = direction.X;
                        npc.velocity.Y = direction.Y;
                    }
                }
            }
            if (movementCounter == 800)
            {
                npc.velocity.X = 0f;
                npc.velocity.Y = 0f;
            }
            if (movementCounter > 800)
            {
                float speed = 20f;
                float acceleration = 0.5f;
                Vector2 vector2 = new Vector2(npc.position.X + npc.width * 0.5f, npc.position.Y + npc.height * 0.5f);
                float xDir = Main.player[npc.target].position.X + (Main.player[npc.target].width / 2) - vector2.X;
                float yDir = Main.player[npc.target].position.Y + (Main.player[npc.target].height / 2) - 120f - vector2.Y;
                float length = (float)Math.Sqrt(xDir * xDir + yDir * yDir);
                if (length > 400f && Main.expertMode)
                {
                    speed += 1f;
                    acceleration += 0.05f;
                    bool flag13 = length > 600f;
                    if (flag13)
                    {
                        speed += 1f;
                        acceleration += 0.08f;
                        bool flag14 = length > 800f;
                        if (flag14)
                        {
                            speed += 1f;
                            acceleration += 0.05f;
                        }
                    }
                }
                float num10 = speed / length;
                xDir *= num10;
                yDir *= num10;
                if (npc.velocity.X < xDir)
                {
                    npc.velocity.X = npc.velocity.X + acceleration;
                    if (npc.velocity.X < 0f && xDir > 0f)
                    {
                        npc.velocity.X = npc.velocity.X + acceleration;
                    }
                }
                else
                {
                    if (npc.velocity.X > xDir)
                    {
                        npc.velocity.X = npc.velocity.X - acceleration;
                        if (npc.velocity.X > 0f && xDir < 0f)
                        {
                            npc.velocity.X = npc.velocity.X - acceleration;
                        }
                    }
                }
                if (npc.velocity.Y < yDir)
                {
                    npc.velocity.Y = npc.velocity.Y + acceleration;
                    if (npc.velocity.Y < 0f && yDir > 0f)
                    {
                        npc.velocity.Y = npc.velocity.Y + acceleration;
                    }
                }
                else
                {
                    if (npc.velocity.Y > yDir)
                    {
                        npc.velocity.Y = npc.velocity.Y - acceleration;
                        bool flag22 = npc.velocity.Y > 0f && yDir < 0f;
                        if (flag22)
                        {
                            npc.velocity.Y = npc.velocity.Y - acceleration;
                        }
                    }
                }
            }
            if (movementCounter > 920)
            {
                movementCounter = 0;
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