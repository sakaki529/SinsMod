using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace SinsMod.Projectiles.Magic
{
    public class RedNote : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sound");
            Main.projFrames[projectile.type] = 3;
        }
        public override void SetDefaults()
        {
            projectile.width = 18;
            projectile.height = 18;
            projectile.aiStyle = -1;
            projectile.friendly = true;
            projectile.magic = true;
            projectile.penetrate = 5;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
            projectile.timeLeft = 180;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 10;
            projectile.GetGlobalProjectile<SinsProjectile>().drawCenter = true;
        }
        public override void AI()
        {
            projectile.frame = (int)projectile.ai[0];
            projectile.velocity *= 0.99f;
            if (projectile.timeLeft < 50)
            {
                projectile.scale -= 0.025f;
                if (projectile.scale <= 0f)
                {
                    projectile.Kill();
                }
                return;
            }
            if (projectile.localAI[0] == 0f)
            {
                projectile.scale += 0.02f;
                if (projectile.scale >= 1.25f)
                {
                    projectile.localAI[0] = 1f;
                }
            }
            if (projectile.localAI[0] == 1f)
            {
                projectile.scale -= 0.02f;
                if (projectile.scale <= 0.75f)
                {
                    projectile.localAI[0] = 0f;
                }
            }
        }
        public override void Kill(int timeLeft)
        {
            int num = 15;
            for (int i = 0; i < num; i++)
            {
                int num2 = Dust.NewDust(projectile.Center, 1, 1, 60, 0f, 0f, 0, default(Color), 1.5f);
                Main.dust[num2].noGravity = true;
                Main.dust[num2].noLight = true;
                Main.dust[num2].position = projectile.Center;
                Main.dust[num2].velocity = Utils.RotatedBy(new Vector2(1.5f, 0f), i * (6.28318548f / num), default(Vector2));
            }
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White * (1f - projectile.alpha / 255f);
        }
    }
}