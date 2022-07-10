using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Projectiles.Thrown
{
    public class MidnightReaperBlade : ModProjectile
	{
        public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Midnight Reaper");
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 3;
            ProjectileID.Sets.TrailingMode[projectile.type] = 2;
        }
		public override void SetDefaults()
		{
            projectile.width = 32;
			projectile.height = 32;
            projectile.thrown = true;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.timeLeft = 90;
            projectile.penetrate = 1;
            projectile.ignoreWater = true;
            projectile.tileCollide = false;
            projectile.scale = 0.7f;
            projectile.extraUpdates = 1;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 2;
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D texture = Main.projectileTexture[projectile.type];
            Texture2D glowTexture = mod.GetTexture("Glow/Projectile/MidnightReaperBlade_Glow");
            int num = Main.projectileTexture[projectile.type].Height / Main.projFrames[projectile.type];
            int num2 = num * projectile.frame;
            Rectangle rectangle = new Rectangle(0, num2, texture.Width, num);
            Vector2 origin = rectangle.Size() / 2f;
            Color color = Color.White;
            color = projectile.GetAlpha(color);
            Color GlowColor = SinsColor.MidnightPurple;
            GlowColor.A = 0;
            for (int i = 0; i < ProjectileID.Sets.TrailCacheLength[projectile.type]; i++)
            {
                Color color2 = color;
                color2 *= (float)(ProjectileID.Sets.TrailCacheLength[projectile.type] - i) / ProjectileID.Sets.TrailCacheLength[projectile.type];
                Color GlowColor2 = GlowColor;
                GlowColor2 *= (float)(ProjectileID.Sets.TrailCacheLength[projectile.type] - i) / ProjectileID.Sets.TrailCacheLength[projectile.type];
                Vector2 value = projectile.oldPos[i];
                float num3 = projectile.oldRot[i];
                float num4 = (4 - i) / 4f;
                Main.spriteBatch.Draw(texture, value + projectile.Size / 2f - Main.screenPosition + new Vector2(0, projectile.gfxOffY), rectangle, color2, num3, origin, projectile.scale * num4, SpriteEffects.None, 0f);
                Main.spriteBatch.Draw(glowTexture, value + projectile.Size / 2f - Main.screenPosition + new Vector2(0, projectile.gfxOffY), rectangle, GlowColor2, num3, origin, projectile.scale * num4, SpriteEffects.None, 0f);
            }
            Main.spriteBatch.Draw(texture, projectile.Center - Main.screenPosition + new Vector2(0f, projectile.gfxOffY), rectangle, projectile.GetAlpha(Color.White), projectile.rotation, origin, projectile.scale, SpriteEffects.None, 0f);
            Main.spriteBatch.Draw(glowTexture, projectile.Center - Main.screenPosition + new Vector2(0f, projectile.gfxOffY), rectangle, projectile.GetAlpha(GlowColor), projectile.rotation, origin, projectile.scale, SpriteEffects.None, 0f);
            return false;
        }
        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            if (projectile.localAI[0] == 0)
            {
                for (int i = 0; i < Main.rand.Next(3, 6); i++)
                {
                    int d = Dust.NewDust(projectile.position, projectile.width, projectile.height, 27, 0f, 0f, 0, default(Color), 1f);
                    Main.dust[d].noGravity = true;
                    Main.dust[d].velocity *= 3f;
                    Main.dust[d].scale = 0.7f + Utils.NextFloat(Main.rand, 0.3f);
                }
                projectile.localAI[0] = 1;
            }
            if (projectile.ai[0] != 0)
            {
                projectile.melee = true;
                projectile.thrown = false;
            }
            float num = projectile.Center.X;
            float num2 = projectile.Center.Y;
            float num3 = 600f;
            bool flag = false;
            for (int num4 = 0; num4 < 200; num4++)
            {
                if (Main.npc[num4].CanBeChasedBy(projectile, false))
                {
                    float num5 = Main.npc[num4].position.X + (Main.npc[num4].width / 2);
                    float num6 = Main.npc[num4].position.Y + (Main.npc[num4].height / 2);
                    float num7 = Math.Abs(projectile.position.X + (projectile.width / 2) - num5) + Math.Abs(projectile.position.Y + (projectile.height / 2) - num6);
                    if (num7 < num3)
                    {
                        num3 = num7;
                        num = num5;
                        num2 = num6;
                        flag = true;
                    }
                }
            }
            if (flag)
            {
                float num8 = 22f;
                Vector2 vector = new Vector2(projectile.position.X + projectile.width * 0.5f, projectile.position.Y + projectile.height * 0.5f);
                float num9 = num - vector.X;
                float num10 = num2 - vector.Y;
                float num11 = (float)Math.Sqrt(num9 * num9 + num10 * num10);
                num11 = num8 / num11;
                num9 *= num11;
                num10 *= num11;
                projectile.velocity.X = (projectile.velocity.X * 1f + num9) / 2f;
                projectile.velocity.Y = (projectile.velocity.Y * 1f + num10) / 2f;
            }
            projectile.alpha -= 15;
            if (projectile.alpha < 0)
            {
                projectile.alpha = 0;
            }
            if (projectile.timeLeft < 15)
            {
                projectile.scale *= 0.92f;
            }
            projectile.direction = (projectile.ai[1] <= 0f) ? 1 : -1;
            projectile.spriteDirection = projectile.direction;
            projectile.rotation += projectile.ai[1] * 0.2f;
            projectile.ai[1] -= 0.0166f * projectile.ai[1];
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (projectile.penetrate == 1)
            {
                projectile.penetrate++;
            }
            if (projectile.numHits >= 4)
            {
                projectile.Kill();
            }
            target.AddBuff(BuffID.ShadowFlame, 90);
            target.AddBuff(BuffID.Bleeding, 90);
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(BuffID.ShadowFlame, 90);
            target.AddBuff(BuffID.Bleeding, 90);
        }
        public override void OnHitPvp(Player target, int damage, bool crit)
        {
            if (projectile.penetrate == 1)
            {
                projectile.penetrate++;
            }
            if (projectile.numHits >= 5)
            {
                projectile.Kill();
            }
            target.AddBuff(BuffID.ShadowFlame, 90);
            target.AddBuff(BuffID.Bleeding, 90);
        }
        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < Main.rand.Next(3, 6); i++)
            {
                int num = Dust.NewDust(projectile.position, projectile.width, projectile.height, 27, 0f, 0f, 0, default(Color), 1f);
                Main.dust[num].noGravity = true;
                Main.dust[num].velocity *= 3f;
                Main.dust[num].scale = 0.7f + Utils.NextFloat(Main.rand, 0.3f);
            }
        }
    }
}