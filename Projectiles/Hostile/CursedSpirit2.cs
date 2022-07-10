using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Projectiles.Hostile
{
    public class CursedSpirit2 : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("怨");
            Main.projFrames[projectile.type] = 2;
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 6;
            ProjectileID.Sets.TrailingMode[projectile.type] = 1;
        }
        public override void SetDefaults()
        {
            projectile.width = 34;
            projectile.height = 34;
            projectile.aiStyle = 14;
            projectile.friendly = false;
            projectile.hostile = true;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
            projectile.timeLeft = 40;
            projectile.penetrate = -1;
            projectile.alpha = 255;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 0;
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
        public override bool PreAI()
        {
            if (projectile.ai[0] == 2)
            {
                projectile.frame = 1;
            }
            if (projectile.ai[0] == 1)
            {
                projectile.friendly = true;
                projectile.hostile = false;
                projectile.magic = true;
            }
            if (projectile.alpha < 80)
            {
                projectile.alpha = 80;
            }
            else if (projectile.alpha > 0)
            {
                projectile.alpha -= 25;
            }
            projectile.rotation = (float)Math.Atan2(projectile.velocity.Y, projectile.velocity.X) + 1.57f;
            return false;
        }
        public override void Kill(int timeLeft)
        {
            Main.PlaySound(103, (int)projectile.position.X, (int)projectile.position.Y, 90, 1f, 0f);
            int num = 5 + Main.rand.Next(1, 4);
            double num2 = Main.rand.NextDouble();
            double num3 = Math.Atan2(projectile.velocity.X, projectile.velocity.Y) - (num2 / 2f);
            double num4 = num2 / num;
            for (int i = 0; i < num; i++)
            {
                double num5 = num3 + num4 * (i + i * i) / 2.0 + (32f * i);
                Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, (float)(Math.Sin(num5) * 15), (float)(Math.Cos(num5) * 15), mod.ProjectileType("CursedSpirit3"), (int)((double)projectile.damage * 0.5f), 1f, Main.myPlayer, projectile.ai[0], 0f);
                Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, (float)(-Math.Sin(num5) * 15), (float)(-Math.Cos(num5) * 15), mod.ProjectileType("CursedSpirit3"), (int)((double)projectile.damage * 0.5f), 1f, Main.myPlayer, projectile.ai[0], 0f);
            }
            for (int i = 0; i < 30; i++)
            {
                float num6 = Main.rand.Next(-25, 26);
                float num7 = Main.rand.Next(-25, 26);
                float num8 = Main.rand.Next(16, 24);
                float num9 = (float)Math.Sqrt(num6 * num6 + num7 * num7);
                num9 = num8 / num9;
                num6 *= num9;
                num7 *= num9;
                int NewDust = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 226, 0f, 0f, 100, default(Color), 1.0f);
                Main.dust[NewDust].noGravity = true;
                Main.dust[NewDust].position.X = projectile.Center.X;
                Main.dust[NewDust].position.Y = projectile.Center.Y;
                Dust dust = Main.dust[NewDust];
                dust.position.X = dust.position.X + Main.rand.Next(-10, 11);
                Dust dust2 = Main.dust[NewDust];
                dust2.position.Y = dust2.position.Y + Main.rand.Next(-10, 11);
                Main.dust[NewDust].velocity.X = num6;
                Main.dust[NewDust].velocity.Y = num7;
                Dust dust3 = Main.dust[NewDust];
                dust3.velocity.X = dust3.velocity.X * 0.7f;
                Dust dust4 = Main.dust[NewDust];
                dust4.velocity.Y = dust4.velocity.Y * 0.7f;
            }
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(255, 255, 255, 125) * (1f - projectile.alpha / 255f);
        }
    }
}