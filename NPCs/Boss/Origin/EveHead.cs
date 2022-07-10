using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.NPCs.Boss.Origin
{
    public class EveHead : ModNPC
    {
        private bool Esc;
        private int EscTimer;
        private int[] Count;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Eve");
        }
        public override void SetDefaults()
        {
            npc.width = 20;
            npc.height = 20;
            npc.lifeMax = 1000000; 
            npc.damage = 200;
            npc.defense = 40;
            npc.knockBackResist = 0f;
            npc.aiStyle = -1;
            aiType = -1;
            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.HitSound = SoundID.NPCHit5;
            npc.DeathSound = SoundID.NPCDeath7;
            npc.npcSlots = 1f;
            npc.netAlways = true;
            npc.netUpdate = true;
            npc.dontTakeDamage = true;
            npc.dontCountMe = true;
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
            npc.damage = 300;
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
            return false;
        }
        public override bool PreAI()
        {
            npc.boss = true;
            if (!Main.npc[(int)npc.ai[0]].active || Main.npc[(int)npc.ai[0]].type != mod.NPCType("Eve"))
            {
                npc.life = 0;
                npc.HitEffect(0, 1);
                npc.active = false;
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
            NPC eve = Main.npc[(int)npc.ai[0]];
            npc.spriteDirection = eve.spriteDirection;
            npc.position.X = eve.position.X + eve.spriteDirection == -1 ? -4 : 4;
            npc.position.Y = eve.position.Y - 24;
        }
        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            return false;
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