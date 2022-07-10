using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Projectiles.Ranged
{
    public class BlackCoin : ModProjectile
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Black Coin");
        }
        public override void SetDefaults()
		{
			projectile.width = 4;
			projectile.height = 4;
            projectile.aiStyle = 1;
            aiType = ProjectileID.Bullet;
            projectile.friendly = true;
			projectile.penetrate = 1;
            projectile.alpha = 255;
            projectile.timeLeft = 600;
            projectile.ranged = true;
            projectile.extraUpdates = 1;
			projectile.tileCollide = true;
			projectile.ignoreWater = false;
        }
		public override void AI()
		{
            if (projectile.alpha > 0)
            {
                projectile.alpha -= 25;
            }
            if (projectile.alpha < 0)
            {
                projectile.alpha = 0;
            }
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            //Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 10, 1f, 0f);
            return base.OnTileCollide(oldVelocity);
        }
        public override void Kill(int timeLeft)
        {
            Main.PlaySound(SoundID.Dig, (int)projectile.position.X, (int)projectile.position.Y, 1, 1f, 0f);
            for (int num = 0; num < 10; num++)
            {
                int num2 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 82, 0f, 0f, 0, default(Color), 1f);
                Main.dust[num2].noGravity = true;
                Dust dust = Main.dust[num2];
                dust.velocity -= projectile.velocity * 0.5f;
            }
        }
    }
}