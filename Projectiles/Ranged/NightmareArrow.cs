using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Projectiles.Ranged
{
    public class NightmareArrow : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Nigthmare Arrow");
        }
        public override void SetDefaults()
        {
            projectile.width = 14;
            projectile.height = 14;
            projectile.aiStyle = 1;
            aiType = ProjectileID.Bullet;//Normal arrow, don't need
            projectile.ranged = true;
            projectile.arrow = true;
            projectile.penetrate = 1;
            projectile.timeLeft = 300;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.tileCollide = true;
            projectile.ignoreWater = true;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 60;
            projectile.extraUpdates = 1;
            projectile.alpha = 255;
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
            /*if (projectile.alpha > 0)
            {
                projectile.alpha -= 75;
            }*/
            if (projectile.alpha < 0)
            {
                for (int i = 0; i < 12; i++)
                {
                    Vector2 vector = Vector2.UnitX * -projectile.width / 2f;
                    vector += -Vector2.UnitY.RotatedBy(i * 3.14159274f / 6f, default(Vector2)) * new Vector2(8f, 16f);
                    vector = vector.RotatedBy(projectile.rotation - 1.57079637f, default(Vector2));
                    int num = Dust.NewDust(projectile.Center, 0, 0, 172, 0f, 0f, 160, default(Color), 1.1f);
                    Main.dust[num].scale = 1.1f;
                    Main.dust[num].noGravity = true;
                    Main.dust[num].position = projectile.Center + vector;
                    Main.dust[num].velocity = projectile.velocity * 0.1f;
                    Main.dust[num].velocity = Vector2.Normalize(projectile.Center - projectile.velocity * 3f - Main.dust[num].position) * 1.25f;
                    Main.dust[num].shader = GameShaders.Armor.GetSecondaryShader(44, Main.LocalPlayer);
                }
                projectile.alpha = 0;
            }
            if (projectile.ai[0] == 0f)
            {
                Main.PlaySound(SoundID.Item, (int)projectile.position.X, (int)projectile.position.Y, 60, 1f, 0f);
                projectile.ai[0] = 1f;
            }
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (projectile.penetrate == 1)
            {
                projectile.penetrate++;
            }
            target.AddBuff(mod.BuffType("Nightmare"), 30);
            if (Main.rand.Next(4) == 0)
            {
                Projectile.NewProjectile(target.Center.X, target.Center.Y, 0f, 0f, mod.ProjectileType("NigthmareExplosion"), projectile.damage / 4, projectile.knockBack, projectile.owner, 0f, 0f);
            }
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(mod.BuffType("Nightmare"), 30);
            if (Main.rand.Next(4) == 0)
            {
                Projectile.NewProjectile(target.Center.X, target.Center.Y, 0f, 0f, mod.ProjectileType("NigthmareExplosion"), projectile.damage / 4, projectile.knockBack, projectile.owner, 0f, 0f);
            }
        }
        public override void OnHitPvp(Player target, int damage, bool crit)
        {
            if (projectile.penetrate == 1)
            {
                projectile.penetrate++;
            }
            target.AddBuff(mod.BuffType("Nightmare"), 30);
            if (Main.rand.Next(4) == 0)
            {
                Projectile.NewProjectile(target.Center.X, target.Center.Y, 0f, 0f, mod.ProjectileType("NigthmareExplosion"), projectile.damage / 4, projectile.knockBack, projectile.owner, 0f, 0f);
            }
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0f, 0f, mod.ProjectileType("NigthmareExplosion"), projectile.damage / 5, projectile.knockBack, projectile.owner, 0f, 0f);
            return base.OnTileCollide(oldVelocity);
        }
        public override void Kill(int timeLeft)
        {
            Main.PlaySound(SoundID.Item10, projectile.Center);
            for (int i = 0; i < 6; i++)
            {
                float num = Main.rand.Next(-4, 5);
                float num2 = Main.rand.Next(-4, 5);
                float num3 = Main.rand.Next(2, 6);
                float num4 = (float)Math.Sqrt(num * num + num2 * num2);
                num4 = num3 / num4;
                num *= num4;
                num2 *= num4;
                int num5 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 182, 0f, 0f, 100, default(Color), 1.2f);
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