using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.NPCs.Boss.Origin
{
    [AutoloadBossHead]
    public class Eve : ModNPC
    {
        private bool Start;
        private bool SecondPhase;
        private bool Esc;
        private int EscTimer;
        private int[] Count;
        public override string BossHeadTexture => "SinsMod/NPCs/Boss/Origin/OriginWhite_Head_Boss";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Eve");
        }
        public override void SetDefaults()
        {
            npc.width = 24;
            npc.height = 42;
            npc.lifeMax = 1000000; 
            npc.damage = 300;
            npc.defense = 40;
            npc.knockBackResist = 0f;
            npc.aiStyle = -1;
            aiType = -1;
            npc.boss = true;
            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.HitSound = SoundID.NPCHit5;
            npc.DeathSound = SoundID.NPCDeath7;
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
            npc.damage = 400;
        }
        public override void HitEffect(int hitDirection, double damage)
        {
            for (int i = 0; i < 2; i++)
            {
                int d = Dust.NewDust(npc.position, npc.width, npc.height, 87, 0f, 0f, 100, SinsColor.MediumWhite, 1.2f);
                Main.dust[d].noGravity = true;
                Main.dust[d].velocity *= 3f;
            }
            if (npc.life <= 0)
            {
                for (int j = 0; j < 20; j++)
                {
                    int num = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, 87, 0f, 0f, 100, SinsColor.MediumWhite, 1.2f);
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
                    int num2 = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, 87, 0f, 0f, 100, SinsColor.MediumWhite, 1.2f);
                    Main.dust[num2].noGravity = true;
                    Main.dust[num2].velocity *= 5f;
                    num2 = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, 31, 0f, 0f, 100, default(Color), 1.2f);
                    Main.dust[num2].noGravity = true;
                    Main.dust[num2].velocity *= 2f;
                }
            }
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
            if (!Start)
            {
                int adam = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y + npc.height / 2, mod.NPCType("Adam"), npc.whoAmI, npc.whoAmI, 0, 0);
                NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y - 42, mod.NPCType("EveHead"), npc.whoAmI, npc.whoAmI, 0, 0);
                npc.ai[0] = adam;
                Start = true;
            }
            Player player = Main.player[npc.target];
            npc.spriteDirection = player.Center.X < npc.Center.X ? -1 : 1;
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
        }
        public override bool CheckDead()
        {
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
    }
}