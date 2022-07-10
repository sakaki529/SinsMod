using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.NPCs.Boss.Madness
{
    //[AutoloadBossHead]
    public class BlackCrystalCoreClone : ModNPC
    {
        public int Count;
        public bool Start;
        private bool FirstPhase = true;
        private bool Esc;
        private int EscTimer;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("White Core");
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
                npc.damage = 450;
            }
            else if (BlackCrystalCore.isSecondPhase)
            {
                npc.lifeMax = 200000;
                npc.damage = 300;
            }
            else
            {
                npc.lifeMax = 150000;
                npc.damage = 150;
            }
            npc.aiStyle = -1;
            aiType = -1;
            npc.defense = 0;
            npc.knockBackResist = 0f;
            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.HitSound = mod.GetLegacySoundSlot(SoundType.NPCHit, "Sounds/NPCHit/BCHit");
            npc.DeathSound = mod.GetLegacySoundSlot(SoundType.NPCKilled, "Sounds/NPCKilled/BCCCKilled");
            npc.npcSlots = 10f;
            npc.netAlways = true;
            npc.scale = 0.9f;
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
            if (BlackCrystalCore.isThirdPhase)
            {
                npc.lifeMax = 300000 + 50000 * numPlayers;
                npc.damage = 600;
            }
            else if (BlackCrystalCore.isSecondPhase)
            {
                npc.lifeMax = 250000 + 50000 * numPlayers;
                npc.damage = 450;
            }
            else
            {
                npc.lifeMax = 200000 + 50000 * numPlayers;
                npc.damage = 300;
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
            npc.netUpdate = true;
            npc.spriteDirection = 0;
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
            npc.spriteDirection = 0;
            npc.TargetClosest(true);
            Vector2 vector = Main.player[npc.target].Center - npc.Center;
            //npc.rotation = vector.ToRotation();
            Count++;
            if (Count > 60)
            {
                if (Main.netMode != 1)
                {
                    if (!BlackCrystalCore.isRangedResist)
                    {
                        vector = Main.npc[(int)npc.ai[2]].Center - npc.Center;
                        vector.Normalize();
                        vector *= -30f;
                        Projectile.NewProjectile(npc.Center.X,npc.Center.Y, vector.X, vector.Y, mod.ProjectileType("VoidBlast"), 100, 0f, Main.myPlayer, 0f, 0f);
                    }
                    if (!BlackCrystalCore.isSummonResist)
                    {
                        for (int j = 0; j < 5; j++)
                        {
                            //int d = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, 235, 0f, 0f, 100, default(Color), 2f);
                            //Main.dust[d].shader = GameShaders.Armor.GetSecondaryShader(44, Main.LocalPlayer);
                        }
                        vector.Normalize();
                        vector *= 16f;
                        //Projectile.NewProjectile(npc.Center.X,npc.Center.Y, vector.X, vector.Y, mod.ProjectileType(""), damage, 1f, npc.target, 0f, 0f);
                    }
                }
                Count = 0;
            }
            Player player = Main.player[npc.target];
            NPC nPC = Main.npc[(int)npc.ai[2]];
            if (!Main.npc[(int)npc.ai[2]].active || Main.npc[(int)npc.ai[2]].type != mod.NPCType("BlackCrystalCore"))
            {
                npc.active = false;
                npc.netUpdate = true;
                return false;
            }
            npc.alpha = nPC.alpha;
            double num = npc.ai[1];
            double num2 = num * 0.017453292519943295;
            double num3 = 150.0;
            npc.position.X = nPC.Center.X - (int)(Math.Cos(num2) * num3) - (npc.width / 2);
            npc.position.Y = nPC.Center.Y - (int)(Math.Sin(num2) * num3) - (npc.height / 2);
            npc.ai[1] += 5f;//Rotation Speed
            return false;
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            if (Main.expertMode)
            {
                target.AddBuff(mod.BuffType("SuperSlow"), 20);
            }
            else if(BlackCrystalCore.isSecondPhase)
            {
                target.AddBuff(mod.BuffType("SuperSlow"), 10);
            }
        }
        public override void ModifyHitPlayer(Player target, ref int damage, ref bool crit)
        {
            if (npc.alpha != 0)
            {
                damage *= 0;
                return;
            }
            base.ModifyHitPlayer(target, ref damage, ref crit);
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