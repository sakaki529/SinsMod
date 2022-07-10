using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;

namespace SinsMod.NPCs.Boss.Madness
{
    [AutoloadBossHead]
    public class BlackCrystalCore : ModNPC
    {
        private bool Start;
        private bool FirstPhase = true;
        private bool SecondPhase;
        private bool ThirdPhase;
        private bool SaveSecondPhase;
        private bool SaveThirdPhase;
        private bool Esc;
        private int EscTimer;
        private int ConstAICounter;
        private int[] Count = new int[2];
        private int Delay;
        private int ChangeResistTimer;
        private int MadeClone;
        private int HealTimer;
        private bool MeleeResist = true;
        private bool RangedResist = true;
        private bool MagicResist = true;
        private bool ThrownResist = true;
        private bool SummonResist = true;
        public static bool isFirstPhase;
        public static bool isSecondPhase;
        public static bool isThirdPhase;
        public static bool isMeleeResist;
        public static bool isRangedResist;
        public static bool isMagicResist;
        public static bool isThrownResist;
        public static bool isSummonResist;
        //Melee
        private int ChargeTimer;
        //Ranged
        //Magic
        //Thrown
        //Summon
        private int SACount;
        private int SHCount;
        private bool CanSummonAttack = true;
        private bool CanSummonHeal = true;
        private bool SummonChargeMode;
        private bool SaveTeleport;
        private bool Teleporting;
        public override string BossHeadTexture => "SinsMod/NPCs/Boss/Madness/Madness_Head_Boss";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Black Core");
            Main.npcFrameCount[npc.type] = 8;
            NPCID.Sets.TrailingMode[npc.type] = 1;
        }
        public override void SetDefaults()
        {
            npc.width = 56;
            npc.height = 56;
            npc.lifeMax = 10000000;
            npc.damage = 900;
            npc.defense = 60;
            npc.knockBackResist = 0f;
            npc.aiStyle = -1;
            aiType = -1;
            npc.boss = true;
            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.HitSound = mod.GetLegacySoundSlot(SoundType.NPCHit, "Sounds/NPCHit/BCHit");
            npc.DeathSound = mod.GetLegacySoundSlot(SoundType.NPCKilled, "Sounds/NPCKilled/BCKilled");
            npc.npcSlots = 1f;
            npc.netAlways = true;
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
            npc.GetGlobalNPC<SinsNPC>().drawCenter = true;
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = 12500000 + 1500000 * numPlayers;
            npc.damage = 1200;
            npc.defense = 120;
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
        public override void ModifyHitPlayer(Player target, ref int damage, ref bool crit)
        {
            if (!SummonResist)
            {
                if (npc.alpha != 0)
                {
                    damage = 0;
                    return;
                }
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
                    int num = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, 186, 0f, 0f, 50, default(Color), 1.2f);
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
                    int num2 = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, 186, 0f, 0f, 0, default(Color), 1.0f);
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
        public override void ModifyHitByItem(Player player, Item item, ref int damage, ref float knockback, ref bool crit)
        {
            if (MeleeResist || (RangedResist && item.ranged) || (MagicResist && item.magic) || (ThrownResist && item.thrown) || (SummonResist && item.summon))
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
            if ((MeleeResist && projectile.melee) || (RangedResist && projectile.ranged) || (MagicResist && projectile.magic) || (ThrownResist && projectile.thrown) || (SummonResist && (projectile.minion || projectile.sentry)))
            {
                damage = 0;
                Main.PlaySound(mod.GetLegacySoundSlot(SoundType.NPCHit, "Sounds/NPCHit/BCResist").WithVolume(0.5f), (int)npc.position.X, (int)npc.position.Y);
            }
        }
        private void MakeClone()
        {
            for (int i = 0; i < 5; i++)
            {
                NPC.NewNPC((int)(npc.Center.X + Math.Sin(i * 72) * 150.0), (int)(npc.Center.Y + Math.Cos(i * 72) * 150.0), mod.NPCType("BlackCrystalCoreClone"), npc.whoAmI, i * 72, 0f, npc.whoAmI, i * 72, 255);
            }
            MadeClone += 1;
        }
        private void ChangeResist()
        {
            switch (Main.rand.Next(5))
            {
                case 0:
                    if (MeleeResist)
                    {
                        Projectile.NewProjectile(npc.Center.X, npc.Center.Y, 0f, 0f, mod.ProjectileType("RingEffect"), 0, 0f, Main.myPlayer, 1f, 0f);
                        Reset();
                    }
                    MeleeResist = false;
                    RangedResist = true;
                    MagicResist = true;
                    ThrownResist = true;
                    SummonResist = true;
                    break;
                case 1:
                    if (RangedResist)
                    {
                        Projectile.NewProjectile(npc.Center.X, npc.Center.Y, 0f, 0f, mod.ProjectileType("RingEffect"), 0, 0f, Main.myPlayer, 2f, 0f);
                        Reset();
                    }
                    MeleeResist = true;
                    RangedResist = false;
                    MagicResist = true;
                    ThrownResist = true;
                    SummonResist = true;
                    break;
                case 2:
                    if (MagicResist)
                    {
                        Projectile.NewProjectile(npc.Center.X, npc.Center.Y, 0f, 0f, mod.ProjectileType("RingEffect"), 0, 0f, Main.myPlayer, 3f, 0f);
                        Reset();
                    }
                    MeleeResist = true;
                    RangedResist = true;
                    MagicResist = false;
                    ThrownResist = true;
                    SummonResist = true;
                    break;
                case 3:
                    if (ThrownResist)
                    {
                        Projectile.NewProjectile(npc.Center.X, npc.Center.Y, 0f, 0f, mod.ProjectileType("RingEffect"), 0, 0f, Main.myPlayer, 4f, 0f);
                        Reset();
                    }
                    MeleeResist = true;
                    RangedResist = true;
                    MagicResist = true;
                    ThrownResist = false;
                    SummonResist = true;
                    break;
                case 4:
                    if (SummonResist)
                    {
                        Projectile.NewProjectile(npc.Center.X, npc.Center.Y, 0f, 0f, mod.ProjectileType("RingEffect"), 0, 0f, Main.myPlayer, 5f, 0f);
                        Reset();
                    }
                    MeleeResist = true;
                    RangedResist = true;
                    MagicResist = true;
                    ThrownResist = true;
                    SummonResist = false;
                    break;
            }
        }
        public override bool PreAI()
        {
            isFirstPhase = FirstPhase;
            isSecondPhase = SecondPhase;
            isThirdPhase = ThirdPhase;
            isMeleeResist = MeleeResist;
            isRangedResist = RangedResist;
            isMagicResist = MagicResist;
            isThrownResist = ThrownResist;
            isSummonResist = SummonResist;
            SACount = NPC.CountNPCS(mod.NPCType("BCCSummonAttack"));
            SHCount = NPC.CountNPCS(mod.NPCType("BCCSummonHeal"));
            npc.dontTakeDamage = NPC.AnyNPCs(mod.NPCType("BlackCrystalCoreClone"));
            npc.spriteDirection = 0;
            if (npc.life > npc.lifeMax)
            {
                npc.life = npc.lifeMax;
            }
            if (npc.alpha > 0 && !SaveTeleport)
            {
                npc.alpha -= 20;
                if (npc.alpha < 0)
                {
                    npc.alpha = 0;
                }
            }
            if (npc.life < npc.lifeMax / 2 || SaveSecondPhase)
            {
                SecondPhase = true;
            }
            if ((npc.life < npc.lifeMax / 10 && Main.expertMode) || SaveThirdPhase)
            {
                ThirdPhase = true;
            }
            ConstAI();
            return true;
        }
        public override void AI()
        {
            if (!Start)
            {
                FirstPhase = true;
                SecondPhase = false;
                ThirdPhase = false;
                MakeClone();
                //ChangeResist();
                MeleeResist = false;
                Start = true;
            }
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
            int ChangeTime = Main.expertMode ? 60 : 120;
            ChangeResistTimer++;
            if (ChangeResistTimer > ChangeTime)
            {
                if (Main.rand.Next(10) == 0)
                {
                    //ChangeResist();
                }
                ChangeResistTimer = 0;
            }
            if (NPC.AnyNPCs(mod.NPCType("BCCSummonHeal")))
            {
                HealTimer += 1;
                if (HealTimer == 60)
                {
                    if (npc.life < npc.lifeMax && SHCount >= 1)
                    {
                        npc.HealEffect(Main.expertMode ? (10000 * SHCount) : (5000 * SHCount), false);
                        npc.life += Main.expertMode ? (10000 * SHCount) : (5000 * SHCount);
                    }
                    HealTimer = 0;
                }
            }
            if (!NPC.AnyNPCs(mod.NPCType("BCCSummonHeal")))
            {
                HealTimer = 0;
            }
            if (Delay > 0)
            {
                Delay--;
            }
            if (Delay < 0)
            {
                Delay = 0;
            }
            if (SecondPhase)
            {
                SaveSecondPhase = true;
                if (MadeClone == 1)
                {
                    MakeClone();
                }
                if (RangedResist)
                {
                    npc.reflectingProjectiles = true;
                }
                else if (!RangedResist)
                {
                    npc.reflectingProjectiles = false;
                }
            }
            if (ThirdPhase)
            {
                SaveThirdPhase = true;
                if (MadeClone == 2)
                {
                    MakeClone();
                }
            }
            if (!MeleeResist)
            {
                if (ChargeTimer == 0)
                {
                    Vector2 TargetVector = new Vector2(player.Center.X, player.Center.Y);
                    Vector2 ReachVelocity = npc.DirectionTo(TargetVector) * (SecondPhase ? (ThirdPhase ? 26 : 23) : 20);
                    npc.velocity = Vector2.Lerp(npc.velocity, ReachVelocity, 0.05f);
                    float distance = npc.Distance(TargetVector);
                    bool flag = distance < 600f;
                    bool flag2 = distance > 1000f;
                    bool flag3 = distance > 8000f;
                    if (flag)
                    {
                        Vector2 vector = npc.Center;
                        float rotation = (float)Math.Atan2(vector.Y - (player.position.Y + player.height * 0.5f), vector.X - (player.position.X + player.width * 0.5f));
                        npc.velocity = new Vector2((float)(Math.Cos(rotation) * 28.0 * -1.0) + Main.rand.Next(-40, 41) * 0.05f, (float)(Math.Sin(rotation) * 28.0 * -1.0) + Main.rand.Next(-40, 41) * 0.05f);
                        npc.velocity *= SecondPhase ? (ThirdPhase ? 1.4f : 1.2f) : 1;
                        if (Main.netMode != 1)
                        {
                            ChargeTimer = 2;
                        }
                    }
                    else if (flag2)
                    {
                        Vector2 vector = npc.Center;
                        float rotation = (float)Math.Atan2(vector.Y - (player.position.Y + player.height * 0.5f), vector.X - (player.position.X + player.width * 0.5f));
                        npc.velocity = new Vector2((float)(Math.Cos(rotation) * 28.0 * -1.0) + Main.rand.Next(-40, 41) * 0.05f, (float)(Math.Sin(rotation) * 28.0 * -1.0) + Main.rand.Next(-40, 41) * 0.05f);
                        npc.velocity *= SecondPhase ? (ThirdPhase ? 2.2f : 2f) : 1.8f;
                        Delay = SecondPhase ? (ThirdPhase ? 47 : 52) : 62;
                        if (Main.netMode != 1)
                        {
                            ChargeTimer = 2;
                        }
                    }
                    else if (flag3)
                    {
                        Vector2 vector = npc.Center;
                        float rotation = (float)Math.Atan2(vector.Y - (player.position.Y + player.height * 0.5f), vector.X - (player.position.X + player.width * 0.5f));
                        npc.velocity = new Vector2((float)(Math.Cos(rotation) * 28.0 * -1.0) + Main.rand.Next(-40, 41) * 0.05f, (float)(Math.Sin(rotation) * 28.0 * -1.0) + Main.rand.Next(-40, 41) * 0.05f);
                        npc.velocity *= SecondPhase ? (ThirdPhase ? 6f : 5f) : 4f;
                        Delay = SecondPhase ? (ThirdPhase ? 47 : 52) : 62;
                        if (Main.netMode != 1)
                        {
                            ChargeTimer = 2;
                        }
                    }
                }
                bool flag4 = ChargeTimer >= 1;
                if (flag4)
                {
                    ChargeTimer++;
                    if (ChargeTimer >= (SecondPhase ? (ThirdPhase ? 27 : 32) : 42) && Delay == 0)
                    {
                        if (!NPC.AnyNPCs(mod.NPCType("BlackCrystalCoreClone")) && SecondPhase)
                        {
                            //Projectile.NewProjectile(npc.Center.X, npc.Center.Y, 0f, 0f, mod.ProjectileType("SpreadShortcut"), 75, 1f, Main.myPlayer, mod.ProjectileType("BlackMatter"), mod.NPCType("BlackCrystalCore"));
                        }
                        npc.velocity = Vector2.Zero;
                        ChargeTimer = 0;
                    }
                    if (!NPC.AnyNPCs(mod.NPCType("BlackCrystalCoreClone")))
                    {
                        if (ChargeTimer % (SecondPhase ? (ThirdPhase ? 11 : 14) : 18) == 0)
                        {
                            int num = 2;
                            float num2 = MathHelper.ToRadians(15f);
                            for (int i = 0; i < num; i++)
                            {
                                Vector2 vector = Utils.RotatedBy(new Vector2(-npc.velocity.X / 2f, -npc.velocity.Y / 2f), MathHelper.Lerp(-num2, num2, i / (num - 1)), default(Vector2));
                                Projectile.NewProjectile(npc.Center.X, npc.Center.Y, vector.X, vector.Y, mod.ProjectileType("LightningDark"), 60, 1f, Main.myPlayer, vector.ToRotation(), Main.rand.Next(100));
                                vector *= 1.05f;
                            }
                        }
                    }
                }
                return;
            }
            if (!RangedResist)
            {
                if (ThirdPhase)
                {

                }
                else if (SecondPhase)
                {

                }
                /*Vector2 TargetVector = new Vector2(player.Center.X + 480, player.Center.Y);
                if (npc.Center.X < player.Center.X)
                {
                    TargetVector = new Vector2(player.Center.X - 480, player.Center.Y);
                }
                Vector2 vector = TargetVector - npc.Center;
                vector.Normalize();
                vector *= 18f;
                npc.velocity = vector;*/
                float vel = 26f;
                float accel = 1.2f;
                int dir = 1;
                if (npc.position.X + (npc.width / 2) < player.position.X + player.width)
                {
                    dir = -1;
                }
                Vector2 vector = new Vector2(npc.position.X + npc.width * 0.5f, npc.position.Y + npc.height * 0.5f);
                float posX = player.position.X + (player.width / 2) + (dir * 400) - vector.X;
                float posY = player.position.Y + (player.height / 2) - vector.Y;
                float sqrt = (float)Math.Sqrt(posX * posX + posY * posY);
                sqrt = vel / sqrt;
                posX *= sqrt;
                posY *= sqrt;
                if (npc.velocity.X < posX)
                {
                    npc.velocity.X = npc.velocity.X + accel;
                    if (npc.velocity.X < 0f && posX > 0f)
                    {
                        npc.velocity.X = npc.velocity.X + accel;
                    }
                }
                else
                {
                    if (npc.velocity.X > posX)
                    {
                        npc.velocity.X = npc.velocity.X - accel;
                        if (npc.velocity.X > 0f && posX < 0f)
                        {
                            npc.velocity.X = npc.velocity.X - accel;
                        }
                    }
                }
                if (npc.velocity.Y < posY)
                {
                    npc.velocity.Y = npc.velocity.Y + accel;
                    if (npc.velocity.Y < 0f && posY > 0f)
                    {
                        npc.velocity.Y = npc.velocity.Y + accel;
                    }
                }
                else
                {
                    if (npc.velocity.Y > posY)
                    {
                        npc.velocity.Y = npc.velocity.Y - accel;
                        if (npc.velocity.Y > 0f && posY < 0f)
                        {
                            npc.velocity.Y = npc.velocity.Y - accel;
                        }
                    }
                }
                float num = Main.player[npc.target].position.X + (Main.player[npc.target].width / 2) - vector.X;
                float num2 = Main.player[npc.target].position.Y + (Main.player[npc.target].height / 2) - vector.Y;
                float num3 = (float)Math.Sqrt(num * num + num2 * num2);
                float num4 = 8f;
                num3 = num4 / num3;
                num *= num3;
                num2 *= num3;
                Count[0]++;
                if (Count[0] >= 180)
                {
                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y, player.Center.X < npc.Center.X ? -18 : 18, 0f, mod.ProjectileType("CursedSpirit"), Main.expertMode ? 500 : 1000, 1f, Main.myPlayer, 2f, 0f);
                    Count[0] = 0;
                }
                if (Count[0] % 60 == 0 && !NPC.AnyNPCs(mod.NPCType("BlackCrystalCoreClone")))
                {
                    /*Vector2 vector2 = player.Center - npc.Center;
                    vector2.Normalize();
                    vector2 *= 15f;
                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y, vector2.X, vector2.Y, mod.ProjectileType("CursedSpirit2"), Main.expertMode ? 150 : 250, 0f, Main.myPlayer, 0f, 0f);*/
                    for (int i = 0; i < (SecondPhase ? Main.rand.Next(3, 5) : Main.rand.Next(2, 4)); i++)
                    {
                        num += Main.rand.Next(-40, 41) * 0.05f;
                        num2 += Main.rand.Next(-40, 41) * 0.05f;
                        vector.X += num * 12f;
                        vector.Y += num2 * 12f;
                        int num6 = Projectile.NewProjectile(vector.X, vector.Y, num / 2f, num2 / 2f, mod.ProjectileType("CursedSpirit3"), Main.expertMode ? 80 : 140, 0f, Main.myPlayer, 2f, 0f);
                        Main.projectile[num6].velocity *= 3f;
                        Main.projectile[num6].timeLeft = 120;
                    }
                }
                return;
            }
            if (!MagicResist)
            {
                if (ThirdPhase)
                {

                }
                else if (SecondPhase)
                {

                }
                return;
            }
            if (!ThrownResist)
            {
                if (ThirdPhase)
                {

                }
                else if (SecondPhase)
                {

                }
                return;
            }
            if (!SummonResist)
            {
                if (!NPC.AnyNPCs(mod.NPCType("BCCSummonAttack")) && CanSummonAttack)
                {
                    for (int i = 0; i < (SecondPhase ? 1 : 1); i++)
                    {
                        int num = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y + npc.height / 2, mod.NPCType("BCCSummonAttack"), npc.whoAmI);
                        NPC Summon = Main.npc[num];
                        Summon.ai[0] = i;
                        if (i == (SecondPhase ? 1 : 0))
                        {
                            CanSummonAttack = false;
                        }
                    }
                }
                if (!NPC.AnyNPCs(mod.NPCType("BCCSummonHeal")) && CanSummonHeal)
                {
                    for (int i = 0; i < (SecondPhase ? 1 : 1); i++)
                    {
                        int num = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y + npc.height / 2, mod.NPCType("BCCSummonHeal"), npc.whoAmI);
                        NPC Summon = Main.npc[num];
                        Summon.ai[0] = i;
                        if (SecondPhase ? i == 1 : i == 0)
                        {
                            CanSummonHeal = false;
                        }
                    }
                }
                if (ThirdPhase)
                {

                }
                if (SecondPhase)
                {
                    if (Count[1] > 200)
                    {
                        if (npc.alpha < 255 && !Teleporting)
                        {
                            SaveTeleport = true;
                            npc.alpha += 5;
                        }
                        if (npc.alpha >= 255)
                        {
                            npc.position.X = player.position.X + ((Main.rand.Next(2) == 0) ? 480 : -480);
                            npc.position.Y = player.position.Y + ((Main.rand.Next(2) == 0) ? 480 : -480);
                            Teleporting = true;
                        }
                        if (Teleporting)
                        {
                            if (npc.alpha > 0)
                            {
                                npc.alpha -= 5;
                            }
                            if (npc.alpha <= 0)
                            {
                                npc.alpha = 0;
                                SaveTeleport = false;
                                Teleporting = false;
                                Count[1] = 0;
                            }
                        }
                    }
                }
                int SummonChargeSpeed = SecondPhase ? (ThirdPhase ? 44 : 40) : 38;
                if (!SummonChargeMode)
                {
                    Count[0] += 1;
                    if (Count[0] >= 600)
                    {
                        Count[0] = 0;
                        SummonChargeMode = true;
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
                    else if(npc.position.Y < player.position.Y - 250f)
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
                    if (SecondPhase)
                    {
                        Count[1]++;
                    }
                    Count[0] += 1;
                    if (Count[0] == 2)
                    {
                        Main.PlaySound(SoundID.Roar, (int)npc.Center.X, (int)npc.Center.Y, 0, 1f, 0f);
                    }
                    if (Count[0] >= 400)
                    {
                        Count[0] = 0;
                        SummonChargeMode = false;
                    }
                    npc.ai[1] += 1f;
                    float dist = Vector2.Distance(npc.Center, player.Center);
                    if (npc.ai[1] > 0f && dist > 60f)
                    {
                        npc.ai[1] = 0f;
                        Vector2 Velocity = npc.DirectionTo(player.Center) * SummonChargeSpeed;
                        npc.velocity = Vector2.Lerp(npc.velocity, Velocity, 0.0333333351f);
                    }
                    /*Vector2 vector2 = new Vector2(npc.position.X + npc.width * 0.5f, npc.position.Y + npc.height * 0.5f);
                    float num6 = player.position.X + (player.width / 2) - vector2.X;
                    float num7 = player.position.Y + (player.height / 2) - vector2.Y;
                    float num8 = (float)Math.Sqrt(num6 * num6 + num7 * num7);
                    float num9 = 1.5f;
                    num8 = num9 / num8;
                    npc.velocity.X = num6 * num8 * SummonChargeSpeed;
                    npc.velocity.Y = num7 * num8 * SummonChargeSpeed;*/
                }
                return;
            }
        }
        private void ConstAI()
        {
            Player player = Main.player[npc.target];
            ConstAICounter++;
            if (Main.rand.Next(360) == 0)
            {
                for (int i = 0; i < 2; i++)
                {
                    Projectile.NewProjectile(player.Center.X + (250 * (i == 0 ? -1 : 1)), player.Center.Y, 15 * (i == 0 ? -1 : 1), 0, mod.ProjectileType("Purification"), Main.expertMode ? 60 : 70, 1f, Main.myPlayer, i == 0 ? 0 : 1, 0f);
                }
            }
            if (Main.rand.Next(120) == 0)
            {
                float distance = Main.rand.Next(1000);
                npc.ai[0] %= (float)Math.PI * 2f;
                Vector2 offset = new Vector2((float)Math.Cos(npc.ai[0]), (float)Math.Sin(npc.ai[0]));
                Vector2 position = Main.player[npc.target].position + distance * offset;
                Projectile.NewProjectile(position, Vector2.Zero, mod.ProjectileType("PurificationRune"), Main.expertMode ? 100 : 120, 1f, Main.myPlayer, Main.rand.Next(6), 0f);
                npc.ai[0] += 10;
            }
        }
        private void Reset()
        {
            Count[0] = 0;
            Count[1] = 0;
            if (MeleeResist)
            {
                ChargeTimer = 0;
            }
            if (MagicResist)
            {

            }
            if (RangedResist)
            {

            }
            if (ThrownResist)
            {

            }
            if (SummonResist)
            {
                CanSummonAttack = true;
                CanSummonHeal = true;
                SummonChargeMode = false;
                SaveTeleport = false;
                Teleporting = false;
            }
        }
        public override bool CheckDead()
        {
            NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("WillOfMadness"), npc.whoAmI);
            return base.CheckDead();
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
        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = ItemID.Heart;
        }
    }
}