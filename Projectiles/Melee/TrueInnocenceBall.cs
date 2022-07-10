using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Projectiles.Melee
{
    public class TrueInnocenceBall : ModProjectile
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("True The Innocence");
            Main.projFrames[projectile.type] = 2;
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;
        }
        public override void SetDefaults()
        {
            projectile.width = 14;
            projectile.height = 14;
            projectile.melee = true;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.penetrate = -1;
        }
        public override void PostDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            int count = (int)projectile.ai[1] + 1;
            if (count > ProjectileID.Sets.TrailCacheLength[projectile.type])
            {
                count = ProjectileID.Sets.TrailCacheLength[projectile.type];
            }
            Texture2D texture = Main.projectileTexture[projectile.type];
            Vector2 origin = new Vector2(texture.Width * 0.5f, projectile.height / 2);
            Vector2 drawPos = projectile.position - Main.screenPosition + origin;
            drawPos.Y += projectile.gfxOffY;
            int frameHeight = texture.Height / Main.projFrames[projectile.type];
            Rectangle frame = new Rectangle(0, projectile.frame * frameHeight, texture.Width, frameHeight);
            Color color = GetAlpha(lightColor).Value;
            SpriteEffects spriteEffects = SpriteEffects.None;
            if (projectile.spriteDirection == -1)
            {
                spriteEffects = SpriteEffects.FlipHorizontally;
            }
            for (int k = 1; k < count; k++)
            {
                Vector2 drawOff = projectile.velocity * k * 1.5f;
                float strength = 0.4f - k * 0.06f;
                strength *= 1f - projectile.alpha / 255f;
                Color drawColor = color;
                drawColor.R = (byte)(color.R * strength);
                drawColor.G = (byte)(color.G * strength);
                drawColor.B = (byte)(color.B * strength);
                drawColor.A = (byte)(color.A * strength / 2f);
                float scale = projectile.scale;
                scale -= k * 0.1f;
                Main.spriteBatch.Draw(texture, drawPos - drawOff, frame, drawColor, projectile.rotation, origin, scale, spriteEffects, 0f);
            }
        }
        public override void AI()
        {
            projectile.frame = (int)projectile.ai[0] == 0 ? 0 : 1;
            projectile.velocity *= 0.985f;
            projectile.rotation += projectile.velocity.X * 0.2f;
            if (projectile.velocity.X > 0f)
            {
                projectile.rotation += 0.08f;
            }
            else
            {
                projectile.rotation -= 0.08f;
            }
            projectile.ai[1] += 1f;
            if (projectile.ai[1] > 45f)
            {
                projectile.alpha += 10;
                if (projectile.alpha >= 255)
                {
                    projectile.alpha = 255;
                    projectile.Kill();
                }
            }
        }
        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 4; i++)
            {
                int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, (int)projectile.ai[0] == 0 ? 73 : 59, 0f, 0f, 100, default(Color), (int)projectile.ai[0] == 0 ? 0.9f : 1.0f);
                Main.dust[dust].noGravity = true;
                Main.dust[dust].velocity *= 2f;
            }
        }
        public override Color? GetAlpha(Color lightColor)
        {
            int value = 255 - projectile.alpha;
            return new Color(value, value, value, 0);
        }
    }
}