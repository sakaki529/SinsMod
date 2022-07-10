using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.NPCs.Boss.Origin
{
    [AutoloadBossHead]
    public class Eden : ModNPC
    {
        private bool SecondPhase;
        private bool Esc;
        private int EscTimer;
        private int[] Count;
        public override string BossHeadTexture => "SinsMod/NPCs/Boss/Origin/Origin_Head_Boss";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Eden");
        }
        public override void SetDefaults()
        {
            npc.width = 42;
            npc.height = 42;
            npc.lifeMax = 1800000; 
            npc.damage = 400;
            npc.defense = 90;
            npc.knockBackResist = 0f;
            npc.aiStyle = -1;
            aiType = -1;
            npc.boss = true;
            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.HitSound = SoundID.NPCHit5;
            //npc.DeathSound = SoundID.NPCDeath7;
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
            }
            else
            {
                music = MusicID.LunarBoss;
            }
            npc.GetGlobalNPC<SinsNPC>().drawCenter = true;
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = (int)(npc.lifeMax * 0.8f * bossLifeScale);
            npc.damage = 600;
        }
        public override void HitEffect(int hitDirection, double damage)
        {
            if (npc.life > 0)
            {
                for (int i = 0; i < 2; i++)
                {
                    int d = Dust.NewDust(npc.position, npc.width, npc.height, 87, 0f, 0f, 100, SinsColor.EdenYellow, 1.2f);
                    Main.dust[d].noGravity = true;
                    Main.dust[d].velocity *= 3f;
                }
            }
            /*if (npc.life <= 0)
            {
                npc.position.X = npc.position.X + (npc.width / 2);
                npc.position.Y = npc.position.Y + (npc.height / 2);
                npc.width = 42;
                npc.height = 42;
                npc.position.X = npc.position.X - (npc.width / 2);
                npc.position.Y = npc.position.Y - (npc.height / 2);
                for (int j = 0; j < 20; j++)
                {
                    int num = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, 87, 0f, 0f, 100, SinsColor.EdenYellow, 1.2f);
                    Main.dust[num].velocity *= 3f;
                    Main.dust[num].noGravity = true;
                    if (Main.rand.Next(2) == 0)
                    {
                        Main.dust[num].scale = 0.5f;
                        Main.dust[num].fadeIn = 1f + Main.rand.Next(10) * 0.1f;
                    }
                }
                for (int k = 0; k < 40; k++)
                {
                    int num2 = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, 87, 0f, 0f, 100, SinsColor.EdenYellow, 1.2f);
                    Main.dust[num2].noGravity = true;
                    Main.dust[num2].velocity *= 5f;
                    num2 = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, 31, 0f, 0f, 100, default(Color), 1.2f);
                    Main.dust[num2].noGravity = true;
                    Main.dust[num2].velocity *= 2f;
                }
            }*/
        }
        public override bool StrikeNPC(ref double damage, int defense, ref float knockback, int hitDirection, ref bool crit)
        {
            Player player = Main.player[npc.target];
            SinsPlayer modPlayer = player.GetModPlayer<SinsPlayer>();
            damage /= 2;
            return base.StrikeNPC(ref damage, defense, ref knockback, hitDirection, ref crit);
        }
        public override bool PreAI()
        {
            npc.spriteDirection = 0;
            if (npc.ai[3] > 0f)
            {
                if (npc.ai[3] == 1)
                {
                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y, 0f, 0f, mod.ProjectileType("ExplosionEffect"), Main.expertMode ? 200 : 400, 0, Main.myPlayer, 1f);
                }
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
                npc.ai[3] += 1f;
                npc.velocity.X *= 0.95f;
                npc.velocity.Y *= 0.95f;
                if (npc.velocity.X < 0.5f && npc.velocity.Y < 0.5f)
                {
                    npc.velocity = Vector2.Zero;
                }
                if (npc.ai[3] >= 40)
                {
                    npc.life = 0;
                    npc.HitEffect(0, 0);
                    npc.checkDead();
                }
                return false;
            }
            return true;
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
            if (npc.life < npc.lifeMax / 2)
            {
                SecondPhase = true;
            }
            if (SecondPhase)
            {
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
            int num = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y + 48, mod.NPCType("OriginWhite"), npc.whoAmI, -1, 0, 0);
            int num2 = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y + 48, mod.NPCType("OriginBlack"), npc.whoAmI, 1, 0, num);
            Main.npc[num].ai[2] = num2;
            return true;
        }
        public override bool CheckActive()
        {
            return false;
        }
        public override bool PreNPCLoot()
        {
            return false;
        }
        public override Color? GetAlpha(Color drawColor)
        {
            Color color = SinsColor.EdenYellow;
            color.A = 80;
            return color * (1f - npc.alpha / 255f);
        }
    }
}