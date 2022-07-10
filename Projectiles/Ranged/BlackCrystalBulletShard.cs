using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;

namespace SinsMod.Projectiles.Ranged
{
    public class BlackCrystalBulletShard : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Black Crystal Bullet");
        }
        public override void SetDefaults()
        {
            projectile.width = 6;
            projectile.height = 6;
            projectile.ranged = true;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.tileCollide = true;
            projectile.ignoreWater = true;
            projectile.penetrate = 3;
            projectile.light = 0.5f;
            projectile.alpha = 50;
            projectile.scale = 1.2f;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 1;
            projectile.GetGlobalProjectile<SinsProjectile>().drawCenter = true;
        }
        public override void AI()
        {
            projectile.light = projectile.scale * 0.5f;
            projectile.rotation += projectile.velocity.X * 0.2f;
            projectile.ai[1] += 1f;
            projectile.velocity *= 0.96f;
            if (projectile.ai[1] > 15f)
            {
                projectile.scale -= 0.05f;
                if (projectile.scale <= 0.2)
                {
                    projectile.scale = 0.2f;
                    projectile.Kill();
                    return;
                }
            }
        }
        public override void Kill(int timeLeft)
        {
            if (projectile.penetrate == 0)
            {
                for (int i = 0; i < Main.rand.Next(3, 6); i++)
                {
                    int num = Dust.NewDust(projectile.position, projectile.width, projectile.height, 186, 0f, 0f, 100, default(Color), 1f);
                    Main.dust[num].noGravity = true;
                    Main.dust[num].velocity *= 2f;
                    Main.dust[num].scale = 0.3f + Utils.NextFloat(Main.rand, 0.1f);
                    Main.dust[num].shader = GameShaders.Armor.GetSecondaryShader(56, Main.LocalPlayer);
                }
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