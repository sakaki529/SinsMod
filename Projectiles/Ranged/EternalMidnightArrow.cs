using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Projectiles.Ranged
{
    public class EternalMidnightArrow : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Midnigth Arrow");
        }
        public override void SetDefaults()
        {
            projectile.width = 10;
            projectile.height = 10;
            projectile.ranged = true;
            projectile.aiStyle = 1;
            aiType = ProjectileID.Bullet;
            projectile.penetrate = 1;
            projectile.hostile = false;
            projectile.friendly = true;
            projectile.tileCollide = true;
            projectile.ignoreWater = true;
            projectile.timeLeft = 180;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 30;
            projectile.extraUpdates = 1;
            projectile.alpha = 255;
        }
        public override void AI()
        {
            if (projectile.alpha > 0)
            {
                projectile.alpha -= 75;
            }
            if (projectile.alpha < 0)
            {
                float num = 30f;
                int num2 = 0;
                while (num2 < num)
                {
                    Vector2 vector = Vector2.UnitX * 0f;
                    vector += -Vector2.UnitY.RotatedBy(num2 * (6.28318548f / num), default(Vector2)) * new Vector2(4f, 12f);
                    vector = vector.RotatedBy(projectile.velocity.ToRotation(), default(Vector2));
                    int num3 = Dust.NewDust(projectile.Center, 0, 0, 27, 0f, 0f, 0, default(Color), 1.0f);
                    Main.dust[num3].noGravity = true;
                    Main.dust[num3].position = projectile.Center + vector;
                    Main.dust[num3].velocity = projectile.velocity * 0f + vector.SafeNormalize(Vector2.UnitY) * 1f;
                    int num4 = num2;
                    num2 = num4 + 1;
                }
                projectile.alpha = 0;
            }
            if (projectile.ai[0] == 0f)
            {
                Main.PlaySound(SoundID.Item, (int)projectile.position.X, (int)projectile.position.Y, 60, 1f, 0f);
                projectile.ai[0] = 1f;
            }
            if (projectile.ai[0] == 1f)
            {
                for (int i = 0; i < 3; i++)
                {
                    float num5 = projectile.velocity.X / 3f * i;
                    float num6 = projectile.velocity.Y / 3f * i;
                    int num7 = Dust.NewDust(projectile.position, projectile.width, projectile.height, 27, 0f, 0f, 0, default(Color), 1.0f);
                    Main.dust[num7].position.X = projectile.Center.X - num5;
                    Main.dust[num7].position.Y = projectile.Center.Y - num6;
                    Main.dust[num7].velocity *= 0f;
                    Main.dust[num7].noGravity = true;
                    Main.dust[num7].scale = 1.0f;
                }
            }
            projectile.localAI[1] += 1f;
            if (projectile.localAI[1] == 15f)
            {
                projectile.localAI[1] = 0f;
                for (int i = 0; i < 12; i++)
                {
                    Vector2 vector = Vector2.UnitX * -projectile.width / 2f;
                    vector += -Vector2.UnitY.RotatedBy(i * 3.14159274f / 6f, default(Vector2)) * new Vector2(8f, 16f);
                    vector = vector.RotatedBy(projectile.rotation - 1.57079637f, default(Vector2));
                    int num8 = Dust.NewDust(projectile.Center, 0, 0, 27, 0f, 0f, 160, default(Color), 1.1f);
                    Main.dust[num8].scale = 1.1f;
                    Main.dust[num8].noGravity = true;
                    Main.dust[num8].position = projectile.Center + vector;
                    Main.dust[num8].velocity = projectile.velocity * 0.1f;
                    Main.dust[num8].velocity = Vector2.Normalize(projectile.Center - projectile.velocity * 3f - Main.dust[num8].position) * 1.25f;
                }
            }
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (projectile.penetrate == 1)
            {
                projectile.penetrate++;
            }
            Projectile.NewProjectile(target.Center.X, target.Center.Y, 0f, 0f, mod.ProjectileType("MidnightExplosion"), projectile.damage, projectile.knockBack, projectile.owner, 0f, 0f);
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            Projectile.NewProjectile(target.Center.X, target.Center.Y, 0f, 0f, mod.ProjectileType("MidnightExplosion"), projectile.damage, projectile.knockBack, projectile.owner, 0f, 0f);
        }
        public override void OnHitPvp(Player target, int damage, bool crit)
        {
            if (projectile.penetrate == 1)
            {
                projectile.penetrate++;
            }
            Projectile.NewProjectile(target.Center.X, target.Center.Y, 0f, 0f, mod.ProjectileType("MidnightExplosion"), projectile.damage, projectile.knockBack, projectile.owner, 0f, 0f);
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0f, 0f, mod.ProjectileType("MidnightExplosion"), projectile.damage / 2, projectile.knockBack, projectile.owner, 0f, 0f);
            return base.OnTileCollide(oldVelocity);
        }
        public override void Kill(int timeLeft)
        {
            Main.PlaySound(SoundID.Dig, (int)projectile.position.X, (int)projectile.position.Y, 1);
            for (int i = 0; i < 6; i++)
            {
                float num = Main.rand.Next(-4, 5);
                float num2 = Main.rand.Next(-4, 5);
                float num3 = Main.rand.Next(2, 6);
                float num4 = (float)Math.Sqrt(num * num + num2 * num2);
                num4 = num3 / num4;
                num *= num4;
                num2 *= num4;
                int num5 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 27, 0f, 0f, 100, default(Color), 1.2f);
                Main.dust[num5].noGravity = true;
                Main.dust[num5].position.X = projectile.Center.X;
                Main.dust[num5].position.Y = projectile.Center.Y;
                Dust dust = Main.dust[num5];
                dust.position.X = dust.position.X + Main.rand.Next(-4, 5);
                Dust dust2 = Main.dust[num5];
                dust2.position.Y = dust2.position.Y + Main.rand.Next(-4, 5);
                Main.dust[num5].velocity.X = num;
                Main.dust[num5].velocity.Y = num2;
                Dust dust3 = Main.dust[num5];
                dust3.velocity.X = dust3.velocity.X * 0.3f;
                Dust dust4 = Main.dust[num5];
                dust4.velocity.Y = dust4.velocity.Y * 0.3f;
            }
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White * (1f - projectile.alpha / 255f);
        }
    }
}