using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Projectiles.Melee
{
    public class Comet : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Star Breaker");
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 4;
            ProjectileID.Sets.TrailingMode[projectile.type] = 2;
        }
		public override void SetDefaults()
		{
            projectile.width = 32;
            projectile.height = 32;
            projectile.melee = true;
            projectile.tileCollide = false;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.ignoreWater = true;
            projectile.alpha = 255;
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D texture = Main.projectileTexture[projectile.type];
            Texture2D star = mod.GetTexture("Extra/Projectile/Star");
            int num = Main.projectileTexture[projectile.type].Height / Main.projFrames[projectile.type];
            int num2 = num * projectile.frame;
            Rectangle rectangle = new Rectangle(0, num2, texture.Width, num);
            Vector2 origin = rectangle.Size() / 2f;
            Color color = lightColor;
            color = projectile.GetAlpha(lightColor);
            for (int i = 0; i < ProjectileID.Sets.TrailCacheLength[projectile.type]; i++)
            {
                Color color2 = color;
                color2 *= (float)(ProjectileID.Sets.TrailCacheLength[projectile.type] - i) / ProjectileID.Sets.TrailCacheLength[projectile.type];
                Vector2 value = projectile.oldPos[i];
                Main.spriteBatch.Draw(texture, value + projectile.Size / 2f - Main.screenPosition + new Vector2(0, projectile.gfxOffY), new Microsoft.Xna.Framework.Rectangle?(rectangle), color2, projectile.rotation, origin, projectile.scale, SpriteEffects.None, 0f);
            }
            Main.spriteBatch.Draw(texture, projectile.Center - Main.screenPosition + new Vector2(0f, projectile.gfxOffY), rectangle, projectile.GetAlpha(lightColor), projectile.rotation, origin, projectile.scale, SpriteEffects.None, 0f);
            Main.spriteBatch.Draw(star, projectile.Center - Main.screenPosition + new Vector2(0f, projectile.gfxOffY), rectangle, projectile.GetAlpha(lightColor), projectile.rotation, origin, projectile.scale, SpriteEffects.None, 0f);
            return false;
        }
        public override void AI()
        {
            if (projectile.soundDelay == 0)
            {
                projectile.soundDelay = 20 + Main.rand.Next(40);
                Main.PlaySound(SoundID.Item9, projectile.position);
            }
            if (projectile.Center.Y > (int)projectile.ai[1])
            {
                projectile.tileCollide = true;
            }
            if (projectile.alpha > 100)
            {
                projectile.alpha -= 24;
            }
            if (projectile.alpha < 100)
            {
                projectile.alpha = 100;
            }
            int num = Dust.NewDust(projectile.position, projectile.width, projectile.height, 92, projectile.velocity.X, projectile.velocity.Y, 200);
            Main.dust[num].noGravity = true;
            Main.dust[num].velocity *= 0.4f;
            projectile.rotation = projectile.velocity.ToRotation() - 1.57079637f;
        }
        public override void Kill(int timeLeft)
        {
            if (Main.rand.Next(10) == 0)
            {
                for (int i = 0; i < 1; i++)
                {
                    Item.NewItem((int)projectile.position.X, (int)projectile.position.Y, projectile.width, projectile.height, ItemID.Star);
                }
            }
            Main.PlaySound(SoundID.Item10, projectile.position);
            projectile.velocity /= 2f;
            for (int i = 0; i < 40; i++)
            {
                if (Main.rand.NextFloat() < 0.7f)
                {
                    int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, mod.DustType("Black"), projectile.velocity.X * 0.01f, projectile.velocity.Y * 0.01f, 150, default(Color), 3.0f);
                    Main.dust[dust].noLight = true;
                }
            }
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(255, 255, 255, 255) * (1f - projectile.alpha / 255f);
        }
    }
}