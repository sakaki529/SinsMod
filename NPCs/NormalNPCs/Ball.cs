using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.NPCs.NormalNPCs
{
    public class Ball : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ball");
        }
        public override void SetDefaults()
        {
            npc.width = 16;
            npc.height = 16;
            npc.lifeMax = 50;
            npc.damage = 10;
            npc.defense = 0;
            npc.knockBackResist = 0f;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.alpha = 100;
            npc.HitSound = SoundID.NPCHit3;
            npc.DeathSound = SoundID.NPCDeath3;
            npc.value = Item.sellPrice(0, 0, 0, 0);
            for (int k = 0; k < npc.buffImmune.Length; k++)
            {
                if (k != 69)
                {
                    npc.buffImmune[k] = true;
                }
            }
        }
        public override void HitEffect(int hitDirection, double damage)
        {
            if (npc.life <= 0)
            {
                for (int k = 0; k < 20; k++)
                {
                    int num133 = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y + 2f), npc.width, npc.height, 44, npc.velocity.X * 0.2f, npc.velocity.Y * 0.2f, 100, default(Color), 1.3f);
                    Main.dust[num133].noGravity = true;
                    Dust dust = Main.dust[num133];
                    dust.velocity *= 0.3f;
                }
            }
        }
        public override void AI()
        {
            int num;
            for (int num132 = 0; num132 < 2; num132 = num + 1)
            {
                int num133 = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y + 2f), npc.width, npc.height, 44, npc.velocity.X * 0.2f, npc.velocity.Y * 0.2f, 100, default(Color), 1.3f);
                Main.dust[num133].noGravity = true;
                Dust dust = Main.dust[num133];
                dust.velocity *= 0.3f;
                Dust dust2 = Main.dust[num133];
                dust2.velocity.X = dust2.velocity.X - npc.velocity.X * 0.2f;
                Dust dust3 = Main.dust[num133];
                dust3.velocity.Y = dust3.velocity.Y - npc.velocity.Y * 0.2f;
                num = num132;
            }
            bool target = false;
            npc.localAI[0]++;
            if(npc.localAI[0] > 5f)
            {
                target = true;
            }
            if(target)
            {
                npc.TargetClosest(true);
            }
            else
            {
                npc.TargetClosest(false);
            }
            float num125 = 7f;
            Vector2 vector16 = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
            float num126 = Main.player[npc.target].position.X + (float)(Main.player[npc.target].width / 2) - vector16.X;
            float num127 = Main.player[npc.target].position.Y + (float)(Main.player[npc.target].height / 2) - vector16.Y;
            float num128 = (float)Math.Sqrt((double)(num126 * num126 + num127 * num127));
            num128 = num125 / num128;
            npc.velocity.X = num126 * num128;
            npc.velocity.Y = num127 * num128;

            npc.rotation += 0.1f * (float)npc.direction;
            float num143 = 15f;
            float num144 = 0.0833333358f;
            Vector2 center = npc.Center;
            Vector2 center2 = Main.player[npc.target].Center;
            Vector2 vec = center2 - center;
            vec.Normalize();
            if (vec.HasNaNs())
            {
                vec = new Vector2((float)npc.direction, 0f);
            }
            npc.velocity = (npc.velocity * (num143 - 1f) + vec * (npc.velocity.Length() + num144)) / num143;
            if (npc.velocity.Length() < 6f)
            {
                npc.velocity *= 1.05f;
                return;
            }
        }
        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            return false;
        }
        public override bool PreNPCLoot()
        {
            return false;
        }
    }
}
