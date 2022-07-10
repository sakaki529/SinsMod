using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Projectiles.Ranged
{
    public class BlackCrystalBullet : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Black Crystal Bullet");
        }
        public override void SetDefaults()
        {
            projectile.width = 4;
            projectile.height = 4;
            projectile.aiStyle = 1;
            aiType = ProjectileID.Bullet;
            projectile.ranged = true;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.tileCollide = true;
            projectile.ignoreWater = true;
            projectile.penetrate = 1;
            projectile.extraUpdates = 1;
            projectile.alpha = 255;
            projectile.scale = 1.2f;
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
        public override void Kill(int timeLeft)
        {
            Main.PlaySound(SoundID.Dig, (int)projectile.position.X, (int)projectile.position.Y, 1, 1f, 0f);
            for (int i = 0; i < 5; i++)
            {
                int num = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 186, 0f, 0f, 100, default(Color), 0.8f);
                Main.dust[num].noGravity = true;
                Main.dust[num].shader = GameShaders.Armor.GetSecondaryShader(56, Main.LocalPlayer);
                Dust dust = Main.dust[num];
                dust.velocity *= 1.5f;
                dust = Main.dust[num];
                dust.scale *= 0.9f;
                dust.shader = GameShaders.Armor.GetSecondaryShader(56, Main.LocalPlayer);
            }
            for (int j = 0; j < Main.rand.Next(5, 9); j++)
            {
                Vector2 vector = new Vector2(Main.rand.Next(-100, 101), Main.rand.Next(-100, 101));
                if (vector == Vector2.Zero)
                {
                    vector = new Vector2(Main.rand.Next(-100, 101), Main.rand.Next(-100, 101));
                }
                vector.Normalize();
                vector.X = vector.X * Main.rand.Next(40, 70) * 0.05f + Main.rand.Next(-20, 21) * 0.5f;
                vector.Y = vector.Y * Main.rand.Next(40, 70) * 0.05f + Main.rand.Next(-20, 21) * 0.5f;
                Projectile.NewProjectile(projectile.Center, vector, mod.ProjectileType("BlackCrystalBulletShard"), (int)(projectile.damage * 0.2), 0f, projectile.owner, 0f, 0f);
            }
        }
        public override Color? GetAlpha(Color lightColor)
        {
            if (projectile.alpha < 200)
            {
                return new Color(255 - projectile.alpha, 255 - projectile.alpha, 255 - projectile.alpha, 127);
            }
            return Color.Transparent;
        }
    }
}