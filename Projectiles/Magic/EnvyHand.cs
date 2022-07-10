using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Projectiles.Magic
{
    public class EnvyHand : ModProjectile
    {
        private int Bounce = 2;
        public override string Texture => "SinsMod/Extra/Placeholder/BlankTex";
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Hand of Envy");
        }
		public override void SetDefaults()
		{
            projectile.width = 40;
			projectile.height = 40;
            projectile.magic = true;
            projectile.friendly = true;
            projectile.penetrate = 6;
            projectile.MaxUpdates = 3;
            projectile.tileCollide = true;
            projectile.ignoreWater = true;
        }
        public override void AI()
        {
            if (projectile.velocity.X != projectile.velocity.X)
            {
                if (Math.Abs(projectile.velocity.X) < 1f)
                {
                    projectile.velocity.X = -projectile.velocity.X;
                }
                else
                {
                    projectile.Kill();
                }
            }
            if (projectile.velocity.Y != projectile.velocity.Y)
            {
                if (Math.Abs(projectile.velocity.Y) < 1f)
                {
                    projectile.velocity.Y = -projectile.velocity.Y;
                }
                else
                {
                    projectile.Kill();
                }
            }
            Vector2 center = projectile.Center;
            projectile.scale = 1f - projectile.localAI[0];
            projectile.width = (int)(20f * projectile.scale);
            projectile.height = projectile.width;
            projectile.position.X = center.X - (projectile.width / 2);
            projectile.position.Y = center.Y - (projectile.height / 2);
            if (projectile.localAI[0] < 0.1)
            {
                projectile.localAI[0] += 0.01f;
            }
            else
            {
                projectile.localAI[0] += 0.025f;
            }
            if (projectile.localAI[0] >= 0.95f)
            {
                projectile.Kill();
            }
            projectile.velocity.X = projectile.velocity.X + projectile.ai[0] * 2f;
            projectile.velocity.Y = projectile.velocity.Y + projectile.ai[1] * 2f;
            if (projectile.velocity.Length() > 16f)
            {
                projectile.velocity.Normalize();
                projectile.velocity *= 16f;
            }
            projectile.ai[0] *= 1.05f;
            projectile.ai[1] *= 1.05f;
            if (projectile.scale < 1f)
            {
                int num = 0;
                while (num < projectile.scale * 10f)
                {
                    int num2 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 259, projectile.velocity.X, projectile.velocity.Y, 100, new Color(0, 255, 0), 1.1f);
                    Main.dust[num2].position = (Main.dust[num2].position + projectile.Center) / 2f;
                    Main.dust[num2].noGravity = true;
                    Main.dust[num2].velocity *= 0.1f;
                    Main.dust[num2].velocity -= projectile.velocity * (1.3f - projectile.scale);
                    Main.dust[num2].scale += projectile.scale * 0.05f;
                    num++;
                }
            }
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.immune[projectile.owner] = 3;
            target.AddBuff(BuffID.CursedInferno, 300);
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(BuffID.CursedInferno, 300);
        }
        public override void OnHitPvp(Player target, int damage, bool crit)
        {
            target.AddBuff(BuffID.CursedInferno, 300);
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Bounce--;
            if (Bounce <= 0)
            {
                projectile.Kill();
            }
            else
            {
                if (projectile.velocity.X != oldVelocity.X)
                {
                    projectile.velocity.X = -oldVelocity.X;
                }
                if (projectile.velocity.Y != oldVelocity.Y)
                {
                    projectile.velocity.Y = -oldVelocity.Y;
                }
            }
            return false;
        }
    }
}