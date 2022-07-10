using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Projectiles.Magic
{
    public class Eschatology : ModProjectile
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Eschatology");
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 8;
            ProjectileID.Sets.TrailingMode[projectile.type] = 2;
        }
		public override void SetDefaults()
		{
            projectile.width = 38;
            projectile.height = 38;
            projectile.friendly = true;
            projectile.magic = true;
			projectile.penetrate = 2;
			projectile.timeLeft = 160;
            projectile.tileCollide = false;
            projectile.ignoreWater = false;
            projectile.extraUpdates = 1;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 0;
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width * 0.5f, projectile.height * 0.5f);
            for (int i = 0; i < projectile.oldPos.Length; i++)
            {
                Vector2 drawPos = projectile.oldPos[i] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
                Color color = projectile.GetAlpha(lightColor * 0.35f) * ((float)(projectile.oldPos.Length - i) / projectile.oldPos.Length);
                spriteBatch.Draw(Main.projectileTexture[projectile.type], drawPos, null, color, projectile.rotation, drawOrigin, projectile.scale, projectile.velocity.X < 0f ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0f);
            }
            return true;
        }
        public override void AI()
        {
            projectile.rotation = (float)Math.Atan2(projectile.velocity.Y, projectile.velocity.X) + 1.57f;
        }
        public override void Kill(int timeleft)
		{
            Main.PlaySound(SoundID.Item, (int)projectile.position.X, (int)projectile.position.Y, 74);
            for (int i = 0; i < 10; i++)
            {
                float num = projectile.oldVelocity.X * (5f / i);
                float num2 = projectile.oldVelocity.Y * (5f / i);
                int num3 = Dust.NewDust(new Vector2(projectile.oldPosition.X - num, projectile.oldPosition.Y - num2), projectile.width, projectile.height, 93, projectile.oldVelocity.X, projectile.oldVelocity.Y, 100, default(Color), 1.2f);
                Main.dust[num3].noGravity = true;
                Dust dust = Main.dust[num3];
                dust.velocity *= 0.08f;
                num3 = Dust.NewDust(new Vector2(projectile.oldPosition.X - num, projectile.oldPosition.Y - num2), projectile.width, projectile.height, 93, projectile.oldVelocity.X, projectile.oldVelocity.Y, 100, default(Color), 1.0f);
                dust = Main.dust[num3];
                dust.velocity *= 0.05f;
                dust.noGravity = true;
            }
        }
    }
}