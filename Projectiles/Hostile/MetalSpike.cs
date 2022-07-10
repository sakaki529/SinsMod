using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace SinsMod.Projectiles.Hostile
{
    public class MetalSpike : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Spike");
        }
        public override void SetDefaults()
        {
            projectile.width = 6;
            projectile.height = 6;
            projectile.hostile = true;
            projectile.alpha = 255;
            projectile.aiStyle = 1;
            projectile.penetrate = -1;
        }
        public override void AI()
        {
            if (projectile.alpha == 0 && Main.rand.Next(3) == 0)
            {
                int num = Dust.NewDust(projectile.position - projectile.velocity * 3f, projectile.width, projectile.height, 11, 0f, 0f, 50, default(Color), 0.7f);
                Main.dust[num].velocity *= 0.3f;
                Main.dust[num].velocity += projectile.velocity * 0.3f;
                Main.dust[num].noGravity = true;
            }
            projectile.alpha -= 50;
            if (projectile.alpha < 0)
            {
                projectile.alpha = 0;
            }
            if (projectile.ai[1] == 0f)
            {
                projectile.ai[1] = 1f;
                Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 17, 1f, 0f);
            }
            projectile.ai[0] += 1f;
            if (projectile.ai[0] >= 5f)
            {
                projectile.ai[0] = 5f;
                projectile.velocity.Y = projectile.velocity.Y + 0.15f;
            }
        }
        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 5; i++)
            {
                Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, 11, projectile.oldVelocity.X * 0.5f, projectile.oldVelocity.Y * 0.5f, 0, default(Color), 1.0f);
            }
        }
    }
}