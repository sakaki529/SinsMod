using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.NPCs.PumpkinHead
{
    public class PumpkingFace : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Pumpkin");
            Main.npcFrameCount[npc.type] = 12;
        }
        public override void SetDefaults()
        {
            npc.width = 100;
            npc.height = 100;
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
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.npcSlots = 1f;
            npc.netAlways = true;
            npc.value = Item.buyPrice(0, 0, 0, 0);
            for (int i = 0; i < npc.buffImmune.Length; i++)
            {
                npc.buffImmune[i] = true;
            }
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {

        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            Texture2D texture = Main.npcTexture[npc.type];
            Texture2D glowMask = mod.GetTexture("Glow/NPC/PumpkingFace_Glow");
            SpriteEffects spriteEffects = SpriteEffects.None;
            spriteBatch.Draw(texture, npc.Center - Main.screenPosition, new Rectangle?(npc.frame), npc.GetAlpha(drawColor), npc.rotation, npc.frame.Size() / 2f, npc.scale, spriteEffects, 0f);
            spriteBatch.Draw(glowMask, npc.Center - Main.screenPosition, new Rectangle?(npc.frame), npc.GetAlpha(Color.White), npc.rotation, npc.frame.Size() / 2f, npc.scale, spriteEffects, 0f);
            return false;
        }
        public override void FindFrame(int frameHeight)
        {
            npc.frameCounter++;
            if (npc.frameCounter > 2)
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
            int num;
            if (npc.life > 0)
            {
                int num2 = 0;
                while (num2 < 10 / (double)npc.lifeMax * 100.0)
                {
                    Dust.NewDust(npc.position, npc.width, npc.height, 189, hitDirection, -1f, 0, default(Color), 1.1f);
                    num = num2;
                    num2 = num + 1;
                }
            }
            else
            {
                for (int num3 = 0; num3 < 60; num3 = num + 1)
                {
                    Dust.NewDust(npc.position, npc.width, npc.height, 189, 2 * hitDirection, -2f, 0, default(Color), 1.1f);
                    num = num3;
                }
                for (int num4 = 476; num4 <= 480; num4 = num + 1)
                {
                    Gore.NewGore(new Vector2(npc.position.X, npc.position.Y), npc.velocity * 0f, num4, npc.scale);
                    num = num4;
                }
            }
        }
        public override void AI()
        {
            npc.TargetClosest(true);
            npc.rotation -= 0.07f;
            float speed = 15f;
            if (npc.ai[0] != 0)
            {
                speed = npc.ai[0];
            }
            npc.velocity.X = (float)Math.Cos((float)Math.Atan2(npc.Center.Y - Main.player[npc.target].Center.Y, npc.Center.X - Main.player[npc.target].Center.X)) * -speed;
            npc.velocity.Y = (float)Math.Sin((float)Math.Atan2(npc.Center.Y - Main.player[npc.target].Center.Y, npc.Center.X - Main.player[npc.target].Center.X)) * -speed;
        }
        public override bool CheckDead()
        {
            Vector2 vector = new Vector2(npc.velocity.X, npc.velocity.Y);
            vector.Normalize();
            vector *= 20;
            float spread = 6f;
            float baseSpeed = (float)Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
            double startAngle = Math.Atan2(vector.X, vector.Y) - spread / 2;
            double deltaAngle = spread / 2f;
            double offsetAngle;
            for (int i = 0; i < 50; i++)
            {
                offsetAngle = startAngle + deltaAngle * i;
                Projectile.NewProjectile(npc.Center.X, npc.Center.Y, baseSpeed * (float)Math.Sin(offsetAngle), baseSpeed * (float)Math.Cos(offsetAngle), mod.ProjectileType("FlamingPumpkin"), 500, 0, Main.myPlayer);
            }
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