using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Projectiles.Hostile
{
    public class CursedFlame : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cursed Flame");
        }
        public override void SetDefaults()
        {
            projectile.width = 12;
            projectile.height = 12;
            projectile.friendly = false;
            projectile.hostile = true;
            projectile.tileCollide = true;
            projectile.hide = true;
            projectile.penetrate = 3;
            projectile.timeLeft = 240;
            projectile.noEnchantments = true;
        }
        public override void AI()
        {
            if (projectile.velocity.X != projectile.oldVelocity.X)
            {
                projectile.velocity.X = projectile.oldVelocity.X * -0.1f;
            }
            projectile.ai[0] += 1f;
            if (projectile.ai[0] > 5f)
            {
                projectile.ai[0] = 5f;
                if (projectile.velocity.Y == 0f && projectile.velocity.X != 0f)
                {
                    projectile.velocity.X = projectile.velocity.X * 0.97f;
                    if (projectile.velocity.X > -0.01 && projectile.velocity.X < 0.01)
                    {
                        projectile.velocity.X = 0f;
                        projectile.netUpdate = true;
                    }
                }
                projectile.velocity.Y = projectile.velocity.Y + 0.2f;
            }
            projectile.rotation += projectile.velocity.X * 0.1f;

            projectile.alpha = 255;
            int num = Dust.NewDust(projectile.position, projectile.width, projectile.height, 75, 0f, 0f, 100, default(Color), 1f);
            Dust dust = Main.dust[num];
            dust.position.X = dust.position.X - 2f;
            Dust dust2 = Main.dust[num];
            dust2.position.Y = dust2.position.Y + 2f;
            Dust dust3 = Main.dust[num];
            dust3.scale += Main.rand.Next(50) * 0.01f;
            Main.dust[num].noGravity = true;
            Dust dust4 = Main.dust[num];
            dust4.velocity.Y = dust4.velocity.Y - 2f;
            if (Main.rand.Next(2) == 0)
            {
                int num2 = Dust.NewDust(projectile.position, projectile.width, projectile.height, 75, 0f, 0f, 100, default(Color), 1f);
                Dust dust5 = Main.dust[num2];
                dust5.position.X = dust5.position.X - 2f;
                Dust dust6 = Main.dust[num2];
                dust6.position.Y = dust6.position.Y + 2f;
                dust3 = Main.dust[num2];
                dust3.scale += 0.3f + Main.rand.Next(50) * 0.01f;
                Main.dust[num2].noGravity = true;
                dust3 = Main.dust[num2];
                dust3.velocity *= 0.1f;
            }
            if (projectile.velocity.Y > 16f)
            {
                projectile.velocity.Y = 16f;
            }
            /*int num = Dust.NewDust(projectile.position, projectile.width, projectile.height, 6, 0f, 0f, 100, default(Color), 0.7f);
            Dust dust = Main.dust[num];
            dust.position.X = dust.position.X - 2f;
            Dust dust2 = Main.dust[num];
            dust2.position.Y = dust2.position.Y + 2f;
            Dust dust3 = Main.dust[num];
            dust3.scale += Main.rand.Next(50) * 0.01f;
            Main.dust[num].noGravity = true;
            Dust dust4 = Main.dust[num];
            dust4.velocity.Y = dust4.velocity.Y - 2f;
            if (Main.rand.Next(2) == 0)
            {
                int num2 = Dust.NewDust(projectile.position, projectile.width, projectile.height, 6, 0f, 0f, 100, default(Color), 0.7f);
                Dust dust5 = Main.dust[num2];
                dust5.position.X = dust5.position.X - 2f;
                Dust dust6 = Main.dust[num2];
                dust6.position.Y = dust6.position.Y + 2f;
                dust3 = Main.dust[num2];
                dust3.scale += 0.3f + Main.rand.Next(50) * 0.01f;
                Main.dust[num2].noGravity = true;
                dust3 = Main.dust[num2];
                dust3.velocity *= 0.1f;
            }
            if (projectile.velocity.Y < 0.25 && projectile.velocity.Y > 0.15)
            {
                projectile.velocity.X = projectile.velocity.X * 0.8f;
            }
            projectile.rotation = -projectile.velocity.X * 0.05f;*/
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.CursedInferno, 60 * Main.rand.Next(3, 7));
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(BuffID.CursedInferno, 60 * Main.rand.Next(3, 7));
        }
        public override void OnHitPvp(Player target, int damage, bool crit)
        {
            target.AddBuff(BuffID.CursedInferno, 60 * Main.rand.Next(3, 7));
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (projectile.velocity.X != projectile.oldVelocity.X)
            {
                projectile.velocity.X = projectile.oldVelocity.X * -0.1f;
            }
            return false;
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return Color.Transparent;
        }
    }
}