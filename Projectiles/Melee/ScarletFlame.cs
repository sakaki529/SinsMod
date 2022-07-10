using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Projectiles.Melee
{
    public class ScarletFlame : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Scarlet Flame");
        }
		public override void SetDefaults()
		{
            projectile.width = 16;
            projectile.height = 16;
            projectile.melee = true;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.penetrate = 2;
            projectile.tileCollide = true;
            projectile.ignoreWater = false;
            projectile.GetGlobalProjectile<SinsProjectile>().drawCenter = true;
        }
        public override void AI()
        {
            Lighting.AddLight(projectile.Center, 1f, 0.2f, 0.2f);
            if (projectile.localAI[0] == 0f)
            {
                projectile.localAI[0] = 1f;
                Main.PlaySound(SoundID.Item20, projectile.Center);
            }
            int num = Dust.NewDust(new Vector2(projectile.position.X + projectile.velocity.X, projectile.position.Y + projectile.velocity.Y), projectile.width, projectile.height, (Main.rand.Next(2) == 0) ? 183 : 235, projectile.velocity.X / 3, projectile.velocity.Y / 3, 100, default(Color), 1.5f * projectile.scale);
            Main.dust[num].noGravity = true;
            projectile.ai[1]++;
            if (projectile.ai[1] >= 20f)
            {
                projectile.velocity.Y = projectile.velocity.Y + 0.2f;
            }
            if (projectile.velocity.Y > 16f)
            {
                projectile.velocity.Y = 16f;
            }
            projectile.rotation += 0.3f * projectile.direction;
            projectile.spriteDirection = projectile.direction;
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Main.PlaySound(SoundID.Item10, projectile.Center);
            projectile.ai[0] += 1f;
            if (projectile.ai[0] >= 5f)
            {
                projectile.position += projectile.velocity;
                projectile.Kill();
                return false;
            }
            if (projectile.velocity.X != oldVelocity.X)
            {
                projectile.velocity.X = -oldVelocity.X;
            }
            if (projectile.velocity.Y != oldVelocity.Y)
            {
                projectile.velocity.Y = -oldVelocity.Y/* * 0.9f*/;
            }
            return false;
        }
        public override void Kill(int timeLeft)
        {
            Main.PlaySound(SoundID.Item10, projectile.position);
            for (int num = 0; num < 20; num++)
            {
                int num2 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 183, -projectile.velocity.X * 0.2f, -projectile.velocity.Y * 0.2f, 100, default(Color), 1.25f * projectile.scale);
                Main.dust[num2].noGravity = true;
                Dust dust = Main.dust[num2];
                dust.velocity /= 2f;
                num2 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height,  235, -projectile.velocity.X * 0.2f, -projectile.velocity.Y * 0.2f, 100, default(Color), 1.25f * projectile.scale);
                dust = Main.dust[num2];
                dust.velocity /= 2f;
            }
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(200, 200, 200, 200) * (1f - projectile.alpha / 255f);
        }
    }
}