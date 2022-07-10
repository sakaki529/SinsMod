using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Projectiles.Hostile
{
    public class SeraphBlast : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Seraph Blast");
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 15;
            ProjectileID.Sets.TrailingMode[projectile.type] = 1;
        }
        public override void SetDefaults()
        {
            projectile.width = 24;
            projectile.height = 24;
            projectile.friendly = false;
            projectile.hostile = true;
            projectile.tileCollide = true;
            projectile.ignoreWater = true;
            projectile.timeLeft = 1200;
            projectile.penetrate = -1;
            projectile.alpha = 255;
            projectile.extraUpdates = 1;
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D texture = Main.projectileTexture[projectile.type];
            int num = Main.projectileTexture[projectile.type].Height / Main.projFrames[projectile.type];
            int num2 = num * projectile.frame;
            Rectangle rectangle = new Rectangle(0, num2, texture.Width, num);
            Vector2 origin = rectangle.Size() / 2f;
            Color color = lightColor;
            color = projectile.GetAlpha(color);
            for (int i = 0; i < ProjectileID.Sets.TrailCacheLength[projectile.type]; i++)
            {
                Color color2 = color;
                color2 *= (float)(ProjectileID.Sets.TrailCacheLength[projectile.type] - i) / ProjectileID.Sets.TrailCacheLength[projectile.type];
                Vector2 value = projectile.oldPos[i];
                Main.spriteBatch.Draw(texture, value + projectile.Size / 2f - Main.screenPosition + new Vector2(0, projectile.gfxOffY), new Microsoft.Xna.Framework.Rectangle?(rectangle), color2, projectile.rotation, origin, projectile.scale, SpriteEffects.None, 0f);
            }
            Main.spriteBatch.Draw(texture, projectile.Center - Main.screenPosition + new Vector2(0f, projectile.gfxOffY), new Microsoft.Xna.Framework.Rectangle?(rectangle), projectile.GetAlpha(lightColor), projectile.rotation, origin, projectile.scale, SpriteEffects.None, 0f);
            return false;
        }
        public override void AI()
        {
            projectile.rotation = projectile.velocity.ToRotation() - 1.57079637f;
            if (projectile.alpha < 125)
            {
                projectile.alpha = 125;
            }
            else if (projectile.alpha > 0)
            {
                projectile.alpha -= 25;
            }
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.Lovestruck, 600);
            if (projectile.timeLeft > 1)
            {
                projectile.timeLeft = 1;
            }
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(BuffID.Lovestruck, 600);
        }
        public override void OnHitPvp(Player target, int damage, bool crit)
        {
            target.AddBuff(BuffID.Lovestruck, 600);
            if (projectile.timeLeft > 1)
            {
                projectile.timeLeft = 1;
            }
        }
        public override void Kill(int timeLeft)
        {
            Main.PlaySound(109, (int)projectile.position.X, (int)projectile.position.Y, 14, 1f, 0f);
            projectile.position.X = projectile.position.X + (projectile.width / 2);
            projectile.position.Y = projectile.position.Y + (projectile.height / 2);
            projectile.width = 150;
            projectile.height = 150;
            projectile.position.X = projectile.position.X - (projectile.width / 2);
            projectile.position.Y = projectile.position.Y - (projectile.height / 2);
            projectile.Damage();
            for (int i = 0; i < 40; i++)
            {
                float num = Main.rand.Next(-30, 31);
                float num2 = Main.rand.Next(-30, 31);
                float num3 = Main.rand.Next(16, 24);
                float num4 = (float)Math.Sqrt(num * num + num2 * num2);
                num4 = num3 / num4;
                num *= num4;
                num2 *= num4;
                int NewDust = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 21, 0f, 0f, 100, new Color(Main.DiscoR + 155, 50, 50), 1.8f);
                Main.dust[NewDust].noGravity = true;
                Main.dust[NewDust].position.X = projectile.Center.X;
                Main.dust[NewDust].position.Y = projectile.Center.Y;
                Dust dust = Main.dust[NewDust];
                dust.position.X = dust.position.X + Main.rand.Next(-10, 11);
                Dust dust2 = Main.dust[NewDust];
                dust2.position.Y = dust2.position.Y + Main.rand.Next(-10, 11);
                Main.dust[NewDust].velocity.X = num;
                Main.dust[NewDust].velocity.Y = num2;
                Dust dust3 = Main.dust[NewDust];
                dust3.velocity.X = dust3.velocity.X * 0.7f;
                Dust dust4 = Main.dust[NewDust];
                dust4.velocity.Y = dust4.velocity.Y * 0.7f;
            }
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(Main.DiscoR + 155, 155, 155, 125) * (1f - projectile.alpha / 255f);
        }
    }
}