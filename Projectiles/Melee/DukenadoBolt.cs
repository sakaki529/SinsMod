using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Projectiles.Melee
{
    public class DukenadoBolt : ModProjectile
    {
        public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Tornado");
            Main.projFrames[projectile.type] = 3;
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 4;
            ProjectileID.Sets.TrailingMode[projectile.type] = 2;
        }
		public override void SetDefaults()
		{
            projectile.width = 30;
            projectile.height = 30;
            projectile.aiStyle = 71;
            projectile.melee = true;
            projectile.penetrate = 1;
            projectile.timeLeft = 180;
            projectile.tileCollide = true;
            projectile.friendly = true;
            projectile.scale = 1.0f;
            projectile.extraUpdates = 1;
            //projectile.light = 0.5f;
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
                float num3 = projectile.oldRot[i];
                float num4 = (9 - i) / 9f;
                Main.spriteBatch.Draw(texture, value + projectile.Size / 2f - Main.screenPosition + new Vector2(0, projectile.gfxOffY), rectangle, color2, num3, origin, projectile.scale * num4, SpriteEffects.None, 0f);
            }
            Main.spriteBatch.Draw(texture, projectile.Center - Main.screenPosition + new Vector2(0f, projectile.gfxOffY), rectangle, projectile.GetAlpha(lightColor), projectile.rotation, origin, projectile.scale, SpriteEffects.None, 0f);
            return false;
        }
        public override void AI()
		{
            projectile.frame++;
            if (projectile.frame >= Main.projFrames[projectile.type])
            {
                projectile.frame = 0;
            }
            int num = Main.rand.Next(3);
            for (int i = 0; i < num; ++i)
            {
                Vector2 vector = projectile.velocity;
                vector.Normalize();
                vector.X *= projectile.width;
                vector.Y *= projectile.height;
                vector /= 2;
                vector = vector.RotatedBy((i - 2) * Math.PI / 6);
                vector += projectile.Center;
                Vector2 vector2 = (Main.rand.NextFloat() * (float)Math.PI - (float)Math.PI / 2f).ToRotationVector2();
                vector2 *= Main.rand.Next(3, 8);
                int num2 = Dust.NewDust(vector + vector2, 0, 0, 172, vector2.X * 2f, vector2.Y * 2f, 100, new Color(), 1.4f);
                Main.dust[num2].noGravity = true;
                Main.dust[num2].noLight = true;
                Main.dust[num2].velocity /= 4f;
                Main.dust[num2].velocity -= projectile.velocity;
            }
            projectile.rotation += 0.2f * (projectile.velocity.X > 0f ? 1f : -1f);
        }
        public override void Kill(int timeLeft)
        {
            int num = 36;
            for (int i = 0; i < num; ++i)
            {
                Vector2 vector = (Vector2.Normalize(projectile.velocity) * new Vector2(projectile.width / 2f, projectile.height) * 0.4f).RotatedBy(i - (num / 2 - 1) * 6.28318548202515 / num, new Vector2()) + projectile.Center;
                Vector2 vector2 = vector - projectile.Center;
                int num2 = Dust.NewDust(vector + vector2, 0, 0, 172, vector2.X * 2f, vector2.Y * 2f, 100, new Color(), 1.4f);
                Main.dust[num2].noGravity = true;
                Main.dust[num2].noLight = true;
                Main.dust[num2].velocity = vector2;
            }
            if (projectile.owner == Main.myPlayer)
            {
                int num3 = (int)(projectile.position.Y / 16f);
                int num4 = (int)(projectile.position.X / 16f);
                int num5 = 100;
                if (num4 < 10)
                {
                    num4 = 10;
                }
                if (num4 > Main.maxTilesX - 10)
                {
                    num4 = Main.maxTilesX - 10;
                }
                if (num3 < 10)
                {
                    num3 = 10;
                }
                if (num3 > Main.maxTilesY - num5 - 10)
                {
                    num3 = Main.maxTilesY - num5 - 10;
                }
                for (int num6 = num3; num6 < num3 + num5; num6++)
                {
                    Tile tile = Main.tile[num4, num6];
                    if (tile.active() && (Main.tileSolid[tile.type] || tile.liquid != 0))
                    {
                        num3 = num6;
                        break;
                    }
                }
                int num7 = Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0f, 0f, mod.ProjectileType("Dukenado"), projectile.damage, projectile.knockBack, projectile.owner, 16f, 15f);
                Main.projectile[num7].netUpdate = true;
            }
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White * (1f - projectile.alpha / 255f);
        }
    }
}