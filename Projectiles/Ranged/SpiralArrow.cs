using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace SinsMod.Projectiles.Ranged
{
    public class SpiralArrow : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Spiral");
        }
        public override void SetDefaults()
        {
            projectile.width = 10;
            projectile.height = 10;
            projectile.aiStyle = 122;
            projectile.ranged = true;
            projectile.arrow = true;
            projectile.friendly = true;
            projectile.tileCollide = false;
            projectile.alpha = 255;
            projectile.ignoreWater = true;
            projectile.extraUpdates = 1;
            projectile.usesLocalNPCImmunity = true;
        }
        public override void AI()
        {
            int num = (int)projectile.ai[0];
            bool flag = false;
            if (num == -1 || !Main.npc[num].active)
            {
                flag = true;
            }
            if (flag)
            {
                if (projectile.ai[0] != -1f)
                {
                    projectile.ai[0] = -1f;
                    projectile.netUpdate = true;
                }
            }
            if (!flag && projectile.Hitbox.Intersects(Main.npc[num].Hitbox))
            {
                projectile.Kill();
                projectile.localAI[1] = 1f;
                projectile.Damage();
                return;
            }
            if (projectile.ai[1] > 0f)
            {
                float num2 = projectile.ai[1];
                projectile.ai[1] = num2 - 1f;
                projectile.velocity = Vector2.Zero;
                return;
            }
            if (flag)
            {
                if (projectile.velocity == Vector2.Zero)
                {
                    projectile.Kill();
                }
                projectile.tileCollide = true;
                projectile.alpha += 10;
                if (projectile.alpha > 255)
                {
                    projectile.Kill();
                }
            }
            else
            {
                Vector2 vector = Main.npc[num].Center - projectile.Center;
                projectile.velocity = Vector2.Normalize(vector) * 12f;
                projectile.alpha -= 15;
                if (projectile.alpha < 0)
                {
                    projectile.alpha = 0;
                }
            }
            projectile.rotation = projectile.velocity.ToRotation() - 1.57079637f;
        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            damage += target.defense / 2;
        }
        public override void ModifyHitPlayer(Player target, ref int damage, ref bool crit)
        {
            damage += target.statDefense / 2;
        }
        public override void ModifyHitPvp(Player target, ref int damage, ref bool crit)
        {
            damage += target.statDefense / 2;
        }
        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < Main.rand.Next(5, 10); i++)
            {
                int num = Dust.NewDust(projectile.Center, 0, 0, 91, 0f, 0f, 100, default(Color), 1f);
                Dust dust = Main.dust[num];
                dust.velocity *= 1.6f;
                Dust dust2 = Main.dust[num];
                dust2.velocity.Y = dust2.velocity.Y - 1f;
                dust = Main.dust[num];
                dust.position -= Vector2.One * 4f;
                Main.dust[num].position = Vector2.Lerp(Main.dust[num].position, projectile.Center, 0.5f);
                Main.dust[num].noGravity = true;
            }
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(255 - projectile.alpha, 255 - projectile.alpha, 255 - projectile.alpha, 0);
        }
    }
}