using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace SinsMod.Projectiles.Melee
{
    public class SpectreScythe : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 52;
            projectile.height = 42;
            projectile.melee = true;
            projectile.friendly = true;
            projectile.tileCollide = false;
            projectile.penetrate = -1;
            projectile.timeLeft = 120;
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            Vector2 origin = new Vector2(Main.projectileTexture[projectile.type].Width / 2f, Main.projectileTexture[projectile.type].Height / 2f);
            byte num = (byte)(projectile.timeLeft * 3);
            byte num2 = (byte)(100.0 * (num / byte.MaxValue));
            Color alpha = new Color(num, num, num, num2);
            if (projectile.timeLeft >= 85)
            {
                alpha = new Color(byte.MaxValue, byte.MaxValue, byte.MaxValue, 100);
            }
            SpriteEffects spriteEffects = projectile.spriteDirection == -1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            spriteBatch.Draw(Main.projectileTexture[projectile.type], new Vector2(projectile.position.X - Main.screenPosition.X + (projectile.width / 2f) - Main.projectileTexture[projectile.type].Width * projectile.scale / 2f + origin.X * projectile.scale, projectile.position.Y - Main.screenPosition.Y + (projectile.height / 2f) - Main.projectileTexture[projectile.type].Height * projectile.scale / 2f + origin.Y * projectile.scale), new Rectangle(0, 0, Main.projectileTexture[projectile.type].Width, Main.projectileTexture[projectile.type].Height), alpha, projectile.rotation, origin, projectile.scale, spriteEffects, 0f);
            return false;
        }
        public override void AI()
        {
            Lighting.AddLight((int)projectile.Center.X, (int)projectile.Center.Y, 0.5f, 0.6f, 0.5f);
            if (projectile.velocity.X < 0f)
            {
                projectile.spriteDirection = -1;
            }
            projectile.rotation += projectile.direction * 0.05f;
            projectile.rotation += projectile.direction * 0.5f * (projectile.timeLeft / 180f);
            projectile.velocity *= 0.96f;
            //projectile.velocity *= 0.95f;//ice scythe
        }
    }
}