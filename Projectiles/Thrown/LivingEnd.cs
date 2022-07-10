using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace SinsMod.Projectiles.Thrown
{
    public class LivingEnd : ModProjectile
    {
        private bool collide;
        private bool stop;
        public override string Texture => "SinsMod/Items/Weapons/Thrown/LivingEnd";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("HeadStone");
        }
        public override void SetDefaults()
        {
            projectile.width = 32;
            projectile.height = 32;
            projectile.aiStyle = 2;
            projectile.timeLeft = 900;
            projectile.thrown = true;
            projectile.friendly = true;
            projectile.tileCollide = true;
            projectile.ignoreWater = false;
            projectile.penetrate = -1;
            projectile.netUpdate = true;
            aiType = 48;
        }
        public override void AI()
        {
            projectile.ai[0]++;
            if (projectile.ai[0] >= 40f || collide)
            {
                projectile.rotation = 0f;
                projectile.velocity.Y++;
                if (!stop)
                {
                    projectile.velocity = Vector2.Zero;
                    stop = true;
                }
            }
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.immune[projectile.owner] = 3;
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (!collide)
            {
                projectile.timeLeft = 300;
                collide = true;
            }
            return false;
        }
        public override void Kill(int timeLeft)
        {
            int num;
            for (int num2 = 0; num2 < 10; num2 = num + 1)
            {
                Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 31, 0f, 0f, 100, default(Color), 1.5f);
                num = num2;
            }
            for (int num3 = 0; num3 < 5; num3 = num + 1)
            {
                int num4 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 31, 0f, 0f, 100, default(Color), 1.5f);
                Main.dust[num4].noGravity = true;
                Dust dust = Main.dust[num4];
                dust.velocity *= 3f;
                num4 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 31, 0f, 0f, 100, default(Color), 1.0f);
                dust = Main.dust[num4];
                dust.velocity *= 2f;
                num = num3;
            }
        }
    }
}