using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.NPCs.PumpkinHead
{
    public class FlamingPumpkin : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Flaming Pumpkin");
            Main.npcFrameCount[npc.type] = 3;
            NPCID.Sets.TrailingMode[npc.type] = 1;
        }
        public override void SetDefaults()
        {
            npc.width = 24;
            npc.height = 24;
            /*if (BlackCrystalCore.isThirdPhase)
            {
                npc.lifeMax = 150000;
                npc.damage = 20;
            }
            else if (BlackCrystalCore.isSecondPhase)
            {
                npc.lifeMax = 100000;
                npc.damage = 100;
            }
            else*/
            {
                npc.lifeMax = 52900;
                npc.damage = 50;
            }
            npc.defense = 0;
            npc.knockBackResist = 0f;
            npc.aiStyle = -1;
            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.HitSound = SoundID.NPCHit3;
            npc.DeathSound = SoundID.NPCHit3;
            npc.npcSlots = 1f;
            npc.netAlways = true;
            npc.value = Item.buyPrice(0, 0, 0, 0);
            for (int i = 0; i < npc.buffImmune.Length; i++)
            {
                npc.buffImmune[i] = true;
            }
            npc.GetGlobalNPC<SinsNPC>().trail = true;
            npc.GetGlobalNPC<SinsNPC>().drawCenter = true;
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {

        }
        public override void FindFrame(int frameHeight)
        {
            npc.frameCounter++;
            if (npc.frameCounter > 0)
            {
                npc.frame.Y = npc.frame.Y + frameHeight;
                npc.frameCounter = 0;
                if (npc.frame.Y / frameHeight >= Main.npcFrameCount[npc.type])
                {
                    npc.frame.Y = 0;
                }
            }
        }
        public override void HitEffect(int hitDirection, double damage)
        {
            for (int i = 0; i < 2; i++)
            {
                int num = Dust.NewDust(npc.position, npc.width, npc.height, 6, hitDirection, -1f, 50, default(Color), 1f);
            }
            if (npc.life <= 0)
            {
                for (int num2 = 0; num2 < 10; num2++)
                {
                    int num3 = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, 6, -npc.velocity.X * 0.2f, -npc.velocity.Y * 0.2f, 100, default(Color), 2f);
                    Main.dust[num3].noGravity = true;
                    Dust dust = Main.dust[num3];
                    dust.velocity *= 2f;
                    num3 = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, 6, -npc.velocity.X * 0.2f, -npc.velocity.Y * 0.2f, 100, default(Color), 1f);
                    dust = Main.dust[num3];
                    dust.velocity *= 2f;
                }
            }
        }
        public override void AI()
        {
            if (npc.velocity.X < 0f)
            {
                npc.spriteDirection = -1;
                npc.rotation = (float)Math.Atan2(-npc.velocity.Y, -npc.velocity.X);
            }
            else
            {
                npc.spriteDirection = 1;
                npc.rotation = (float)Math.Atan2(npc.velocity.Y, npc.velocity.X);
            }
            if (Main.rand.NextBool(3))
            {
                Dust.NewDust(npc.position + npc.velocity, npc.width, npc.height, 6, npc.velocity.X * 0.5f, npc.velocity.Y * 0.5f);
            }
            Player player = Main.player[npc.target];
            if (npc.target < 0 || npc.target == 255 || player.dead || !player.active)
            {
                npc.TargetClosest(true);
            }
            /*if (!NPC.AnyNPCs(mod.NPCType("Cleyera")))
            {
                npc.active = false;
                return;
            }*/
            float speed = 20f;//PixPerFrame
            npc.ai[0] += 1f;
            float dist = Vector2.Distance(npc.Center, player.Center);
            if (npc.ai[0] > 0f && dist > 60f)
            {
                npc.ai[0] = 0f;
                Vector2 Velocity = npc.DirectionTo(player.Center) * speed;
                npc.velocity = Vector2.Lerp(npc.velocity, Velocity, 0.0333333351f);
            }
            /*npc.TargetClosest(true);
            Vector2 vector = new Vector2(npc.Center.X, npc.Center.Y);
            float num = Main.player[npc.target].Center.X - vector.X;
            float num2 = Main.player[npc.target].Center.Y - vector.Y;
            float num3 = (float)Math.Sqrt(num * num + num2 * num2);
            float num4 = 12f;
            num3 = num4 / num3;
            num *= num3;
            num2 *= num3;
            npc.velocity.X = (npc.velocity.X * 100f + num) / 101f;
            npc.velocity.Y = (npc.velocity.Y * 100f + num2) / 101f;
            npc.rotation = (float)Math.Atan2(num2, num) - 1.57f;
            int num5 = Dust.NewDust(npc.position, npc.width, npc.height, 180, 0f, 0f, 0, default(Color), 1f);
            Dust dust = Main.dust[num5];
            dust.velocity *= 0.1f;
            Main.dust[num5].scale = 1.3f;
            Main.dust[num5].noGravity = true;*/
        }
        public override bool CheckActive()
        {
            return false;
        }
        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 0.8f;
            return null;
        }
        public override bool PreNPCLoot()
        {
            return false;
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(200, 200, 200, 0);
        }
    }
}