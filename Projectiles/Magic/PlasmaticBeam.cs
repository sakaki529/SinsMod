using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

namespace SinsMod.Projectiles.Magic
{
    public class PlasmaticBeam : ModProjectile
    {
        public override string Texture => "SinsMod/Extra/Placeholder/BlankTex";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Plasma Beam");
        }
        public override void SetDefaults()
        {
            projectile.width = 8;
            projectile.height = 8;
            projectile.magic = true;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.tileCollide = true;
            projectile.ignoreWater = true;
            projectile.extraUpdates = 100;
            projectile.timeLeft = 100;
        }
        public override void AI()
        {
            for (int i = 0; i < 4; i++)
            {
                Vector2 vector = projectile.position;
                vector -= projectile.velocity * (i * 0.25f);
                projectile.alpha = 255;
                int num = Dust.NewDust(vector, 1, 1, 206, 0f, 0f, 0, default(Color), 1f);
                Main.dust[num].position = vector;
                Dust dust = Main.dust[num];
                dust.position.X = dust.position.X + projectile.width / 2;
                Dust dust2 = Main.dust[num];
                dust2.position.Y = dust2.position.Y + projectile.height / 2;
                Main.dust[num].scale = Main.rand.Next(70, 110) * 0.013f;
                Dust dust3 = Main.dust[num];
                dust3.velocity *= 0.2f;
            }
        }
        public override void Kill(int timeLeft)
        {
            if (projectile.ai[0] == 1f)
            {
                for (int i = 0; i < 6; i++)
                {
                    Vector2 vector = Utils.RotatedBy(Vector2.UnitX, MathHelper.Lerp(0f, 6.28318548f, i / 6f), default(Vector2));
                    Projectile.NewProjectile(projectile.Center, vector.RotatedBy(projectile.velocity.ToRotation()) * 4f, mod.ProjectileType("PlasmaticSphere"), projectile.damage / 4, projectile.knockBack, projectile.owner, 0f, 0f);
                }
                /*for (int j = 0; j < 3; j++)
                {
                    float num = 0.783f;
                    double num2 = Math.Atan2(projectile.velocity.X, projectile.velocity.Y) - num / 2f;
                    double num3 = num / 8f;
                    double num4 = num2 + num3 * (j + j * j) / 2.0 + 24f * j;
                    Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, (float)(Math.Sin(num4) * 4f), (float)(Math.Cos(num4) * 4f), mod.ProjectileType("PlasmaticSphere"), projectile.damage / 4, projectile.knockBack, projectile.owner, 0f, 0f);
                    Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, -(float)(Math.Sin(num4) * 4f), -(float)(Math.Cos(num4) * 4f), mod.ProjectileType("PlasmaticSphere"), projectile.damage / 4, projectile.knockBack, projectile.owner, 0f, 0f);
                }*/
            }
        }
    }
}