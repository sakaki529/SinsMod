using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace SinsMod.Projectiles.Hostile
{
    public class AbyssalFlames : ModProjectile
    {
        public override string Texture => "SinsMod/Extra/Placeholder/BlankTex";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Abyssal Flame");
        }
        public override void SetDefaults()
        {
            projectile.width = 6;
            projectile.height = 6;
            projectile.friendly = false;
            projectile.hostile = true;
            projectile.tileCollide = false;
            projectile.ignoreWater = false;
            projectile.timeLeft = 60;
            projectile.penetrate = -1;
            projectile.alpha = 255;
            projectile.extraUpdates = 3;
        }
        public override void AI()
        {
            if (projectile.ai[0] > 4f)
            {
                projectile.ai[0] += 1f;
                if (Main.rand.Next(2) == 0)
                {
                    int num;
                    for (int num2 = 0; num2 < 1; num2 = num + 1)
                    {
                        int num3 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 21, projectile.velocity.X * 0.2f, projectile.velocity.Y * 0.2f, 100, default(Color), projectile.scale);
                        Dust dust;
                        /*if (Main.rand.Next(3) != 0)
                        {*/
                            Main.dust[num3].noGravity = true;
                            dust = Main.dust[num3];
                            dust.scale *= 3f;
                            Dust dust2 = Main.dust[num3];
                            dust2.velocity.X = dust2.velocity.X * 2f;
                            Dust dust3 = Main.dust[num3];
                            dust3.velocity.Y = dust3.velocity.Y * 2f;
                        //}
                        dust = Main.dust[num3];
                        dust.scale *= 1.25f;
                        Dust dust4 = Main.dust[num3];
                        dust4.velocity.X = dust4.velocity.X * 1.2f;
                        Dust dust5 = Main.dust[num3];
                        dust5.velocity.Y = dust5.velocity.Y * 1.2f;
                        dust = Main.dust[num3];
                        dust.scale *= 0.5f;

                        dust = Main.dust[num3];
                        dust.velocity += projectile.velocity;
                        if (!Main.dust[num3].noGravity)
                        {
                            dust = Main.dust[num3];
                            dust.velocity *= 0.5f;
                        }
                        num = num2;
                    }
                }
            }
            else
            {
                projectile.ai[0] += 1f;
            }
            if (projectile.scale <= 1.7f)
            {
                projectile.scale *= 1.02f;
                projectile.position.X = projectile.position.X + (projectile.width / 2);
                projectile.position.Y = projectile.position.Y + (projectile.height / 2);
                projectile.width = (int)(6 * projectile.scale);
                projectile.height = (int)(6 * projectile.scale);
                projectile.position.X = projectile.position.X - (projectile.width / 2);
                projectile.position.Y = projectile.position.Y - (projectile.height / 2);
            }
            projectile.rotation += 0.3f * projectile.direction;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(mod.BuffType("AbyssalFlame"), 600);
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(mod.BuffType("AbyssalFlame"), 600);
        }
        public override void OnHitPvp(Player target, int damage, bool crit)
        {
            target.AddBuff(mod.BuffType("AbyssalFlame"), 600);
        }
    }
}