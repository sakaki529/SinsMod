using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

namespace SinsMod.Projectiles.Ranged
{
    public class BlessedExplosion : ModProjectile
    {
        public override string Texture => "SinsMod/Extra/Placeholder/BlankTex";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Blessed Explosion");
        }
        public override void SetDefaults()
        {
            projectile.ranged = true;
            projectile.width = 100;
            projectile.height = 100;
            projectile.penetrate = 1;
            projectile.hostile = false;
            projectile.friendly = true;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
            projectile.timeLeft = 10;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 3;
        }
        public override void AI()
        {
            if (projectile.ai[0] == 0f)
            {
                projectile.ai[0] = 1f;
            }
            for (int i = 0; i < 8; i++)
            {
                float num = Main.rand.Next(-10, 11);
                float num2 = Main.rand.Next(-10, 11);
                float num3 = Main.rand.Next(3, 9);
                float num4 = (float)Math.Sqrt(num * num + num2 * num2);
                num4 = num3 / num4;
                num *= num4;
                num2 *= num4;
                int NewDust = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 246, 0f, 0f, 100, default(Color), 1.8f);
                Main.dust[NewDust].noGravity = true;
                Main.dust[NewDust].position.X = projectile.Center.X;
                Main.dust[NewDust].position.Y = projectile.Center.Y;
                Dust dust = Main.dust[NewDust];
                dust.position.X = dust.position.X + Main.rand.Next(-10, 11);
                Dust dust2 = Main.dust[NewDust];
                dust2.position.Y = dust2.position.Y + Main.rand.Next(-10, 11);
                Main.dust[NewDust].velocity.X = num;
                Main.dust[NewDust].velocity.Y = num2;
                Dust dust3 = Main.dust[NewDust];
                dust3.velocity.X = dust3.velocity.X * 0.7f;
                Dust dust4 = Main.dust[NewDust];
                dust4.velocity.Y = dust4.velocity.Y * 0.7f;
            }
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (projectile.penetrate == 1)
            {
                projectile.penetrate++;
            }
        }
        public override void OnHitPvp(Player target, int damage, bool crit)
        {
            if (projectile.penetrate == 1)
            {
                projectile.penetrate++;
            }
        }
    }
}