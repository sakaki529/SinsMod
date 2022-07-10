using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace SinsMod.Projectiles.Hostile
{
    public class BlackMatter : ModProjectile
    {
        private bool Shot;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Black Matter");
            Main.projFrames[projectile.type] = 5;
        }
        public override void SetDefaults()
        {
            projectile.width = 38;
            projectile.height = 38;
            projectile.friendly = false;
            projectile.hostile = true;
            projectile.penetrate = -1;
            projectile.timeLeft = 420;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
            projectile.extraUpdates = 1;
            projectile.GetGlobalProjectile<SinsProjectile>().drawCenter = true;
        }
        public override bool PreAI()
        {
            projectile.frameCounter++;
            if (projectile.frameCounter > 12)
            {
                projectile.frameCounter = 0;
                projectile.frame++;
                if (projectile.frame >= Main.projFrames[projectile.type])
                {
                    projectile.frame = 0;
                }
            }
            return base.PreAI();
        }
        public override void AI()
        {
            if (projectile.velocity.Length() > 0.05f && !Shot)
            {
                projectile.velocity *= 0.95f;
                return;
            }
            if (!Shot)
            {
                int num = Player.FindClosest(projectile.Center, 0, 0);
                Vector2 value = Main.player[num].Center;
                if (Main.rand.Next(2) == 0)
                {
                    value += Main.player[num].velocity * Main.rand.NextFloat(0f, 45f);
                }
                Vector2 vector = Vector2.Subtract(value, projectile.Center);
                vector.Normalize();
                projectile.velocity = new Vector2(vector.X * 24f, vector.Y * 24f);
                Shot = true;
            }
        }
        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 5; i++)
            {
                Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, 21, projectile.oldVelocity.X * 0.001f, projectile.oldVelocity.Y * 0.001f, 0, default(Color), 1.0f);
            }
        }
        public override Color? GetAlpha(Color lightColor)
        {
            if (projectile.timeLeft < 30)
            {
                float num = projectile.timeLeft / 30f;
                projectile.alpha = (int)(255f - 255f * num);
            }
            return new Color?(new Color(255 - projectile.alpha, 255 - projectile.alpha, 255 - projectile.alpha, 0));
        }
    }
}