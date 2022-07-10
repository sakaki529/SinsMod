using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Projectiles.Hostile
{
    public class SweetDream : ModProjectile
    {
        private int DustType
        {
            get
            {
                if ((int)projectile.ai[0] == 1)
                {
                    return 15;
                }
                if ((int)projectile.ai[0] == 2)
                {
                    return 57;//246
                }
                return 58;//234
            }
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sweet Dream");
            Main.projFrames[projectile.type] = 3;
        }
        public override void SetDefaults()
        {
            projectile.width = 10;
            projectile.height = 10;
            projectile.magic = true;
            projectile.friendly = false;
            projectile.hostile = true;
            projectile.tileCollide = true;
            projectile.ignoreWater = true;
            projectile.timeLeft = 240;
            projectile.penetrate = 1;
            projectile.alpha = 255;
            projectile.extraUpdates = 1;
        }
        public override void AI()
        {
            projectile.frame = (int)projectile.ai[0];
            projectile.rotation = projectile.velocity.ToRotation() + 1.57079637f;
            if (projectile.ai[1] != 0)
            {
                projectile.friendly = true;
                projectile.hostile = false;
            }
            if (projectile.alpha > 0)
            {
                projectile.alpha -= 75;
            }
            if (projectile.alpha < 0)
            {
                projectile.alpha = 0;
            }
            if (projectile.localAI[0] == 0)
            {
                float num = 30f;
                int num2 = 0;
                while (num2 < num)
                {
                    Vector2 vector = Vector2.UnitX * 0f;
                    vector += -Vector2.UnitY.RotatedBy(num2 * (6.28318548f / num), default(Vector2)) * new Vector2(4f, 12f);
                    vector = vector.RotatedBy(projectile.velocity.ToRotation(), default(Vector2));
                    int num3 = Dust.NewDust(projectile.Center, 0, 0, DustType, 0f, 0f, 0, default(Color), 1.2f);
                    Main.dust[num3].noGravity = true;
                    Main.dust[num3].position = projectile.Center + vector;
                    Main.dust[num3].velocity = projectile.velocity * 0f + vector.SafeNormalize(Vector2.UnitY) * 1f;
                    int num4 = num2;
                    num2 = num4 + 1;
                }
                projectile.localAI[0] = 1;
            }
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.Lovestruck, 420);
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(BuffID.Lovestruck, 420);
        }
        public override void OnHitPvp(Player target, int damage, bool crit)
        {
            target.AddBuff(BuffID.Lovestruck, 420);
        }
        public override void Kill(int timeLeft)
        {
            Main.PlaySound(SoundID.Item27, projectile.Center);
            for (int num = 0; num < 10; num++)
            {
                float num2 = projectile.oldVelocity.X * (5f / num);
                float num3 = projectile.oldVelocity.Y * (5f / num);
                int num4 = Dust.NewDust(new Vector2(projectile.oldPosition.X - num2, projectile.oldPosition.Y - num3), projectile.width, projectile.height, DustType, projectile.oldVelocity.X, projectile.oldVelocity.Y, 100, default(Color), 1.2f);
                Main.dust[num4].noGravity = true;
                Dust dust = Main.dust[num4];
                dust.velocity *= 0.08f;
                num4 = Dust.NewDust(new Vector2(projectile.oldPosition.X - num2, projectile.oldPosition.Y - num3), projectile.width, projectile.height, DustType, projectile.oldVelocity.X, projectile.oldVelocity.Y, 100, default(Color), 1.0f);
                dust = Main.dust[num4];
                dust.velocity *= 0.05f;
                dust.noGravity = true;
            }
            if (projectile.hostile && SinsWorld.LimitCut)
            {

            }
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White * (1f - projectile.alpha / 255f);
        }
    }
}