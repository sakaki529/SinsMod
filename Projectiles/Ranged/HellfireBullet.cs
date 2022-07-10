using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Projectiles.Ranged
{
    public class HellfireBullet : ModProjectile
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Hellfire Bullet");
        }
        public override void SetDefaults()
		{
			projectile.width = 4;
			projectile.height = 4;
            projectile.aiStyle = 1;
            aiType = ProjectileID.Bullet;
            projectile.timeLeft = 600;
            projectile.ranged = true;
			projectile.penetrate = -1;
            projectile.friendly = true;
			projectile.tileCollide = true;
			projectile.ignoreWater = false;
            projectile.extraUpdates = 1;
            projectile.alpha = 255;
            projectile.usesLocalNPCImmunity = true;
        }
        public override void AI()
        {
            Lighting.AddLight((int)((projectile.position.X + (projectile.width / 2)) / 16f), (int)((projectile.position.Y + (projectile.height / 2)) / 16f), 0.5f, 0.35f, 0.05f);
            projectile.rotation = (float)Math.Atan2(projectile.velocity.Y, projectile.velocity.X) + 1.57f;
            projectile.localAI[0] += 1f;
            if (projectile.localAI[0] >= 6f)
            {
                int num = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width / 4, projectile.height / 4, 31, 0f, 0f, 100, default(Color), 0.7f);
                Main.dust[num].noGravity = true;
                num = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 6, 0f, 0f, 100, default(Color), 0.8f);
                Main.dust[num].noGravity = true;
            }
            if (projectile.alpha > 0)
            {
                projectile.alpha -= 25;
            }
            if (projectile.alpha < 0)
            {
                projectile.alpha = 0;
            }
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (projectile.timeLeft > 1)
            {
                projectile.timeLeft = 1;
            }
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            if (projectile.timeLeft > 1)
            {
                projectile.timeLeft = 1;
            }
        }
        public override void OnHitPvp(Player target, int damage, bool crit)
        {
            if (projectile.timeLeft > 1)
            {
                projectile.timeLeft = 1;
            }
        }
        public override void Kill(int timeLeft)
        {
            Main.PlaySound(SoundID.Item14, projectile.position);
            int num;
            for (int num2 = 0; num2 < 10; num2 = num + 1)
            {
                Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 31, 0f, 0f, 100, default(Color), 1.5f);
                num = num2;
            }
            for (int num3 = 0; num3 < 5; num3 = num + 1)
            {
                int num4 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 6, 0f, 0f, 100, default(Color), 1.5f);
                Main.dust[num4].noGravity = true;
                Dust dust = Main.dust[num4];
                dust.velocity *= 3f;
                num4 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 6, 0f, 0f, 100, default(Color), 1.0f);
                dust = Main.dust[num4];
                dust.velocity *= 2f;
                num = num3;
            }
            int num5 = Gore.NewGore(new Vector2(projectile.position.X, projectile.position.Y), default(Vector2), Main.rand.Next(61, 64), 1f);
            Gore gore = Main.gore[num5];
            gore.velocity *= 0.4f;
            Gore gore2 = Main.gore[num5];
            gore2.velocity.X = gore2.velocity.X + Main.rand.Next(-10, 11) * 0.1f;
            Gore gore3 = Main.gore[num5];
            gore3.velocity.Y = gore3.velocity.Y + Main.rand.Next(-10, 11) * 0.1f;
            num5 = Gore.NewGore(new Vector2(projectile.position.X, projectile.position.Y), default(Vector2), Main.rand.Next(61, 64), 1f);
            gore = Main.gore[num5];
            gore.velocity *= 0.4f;
            Gore gore4 = Main.gore[num5];
            gore4.velocity.X = gore4.velocity.X + Main.rand.Next(-10, 11) * 0.1f;
            Gore gore5 = Main.gore[num5];
            gore5.velocity.Y = gore5.velocity.Y + Main.rand.Next(-10, 11) * 0.1f;
            if (projectile.owner == Main.myPlayer)
            {
                projectile.penetrate = -1;
                projectile.position.X = projectile.position.X + (projectile.width / 2);
                projectile.position.Y = projectile.position.Y + (projectile.height / 2);
                projectile.width = 64;
                projectile.height = 64;
                projectile.position.X = projectile.position.X - (projectile.width / 2);
                projectile.position.Y = projectile.position.Y - (projectile.height / 2);
                projectile.Damage();
            }
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White * (1f - projectile.alpha / 255f);
        }
    }
}