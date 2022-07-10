using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Projectiles.Melee
{
    public class BloodyTongue : ModProjectile
	{
        public override string Texture => "SinsMod/Extra/Placeholder/BlankTex";
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Bloody Tongue");
		}
		public override void SetDefaults()
		{
            projectile.width = 40;
			projectile.height = 40;
            projectile.melee = true;
            projectile.friendly = true;
            projectile.hostile = false;
			//projectile.penetrate = -1;
			projectile.tileCollide = false;
			projectile.ignoreWater = true;
            projectile.MaxUpdates = 3;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 4;
        }
        public override void AI()
        {
            if (projectile.velocity.X != projectile.velocity.X)
            {
                if (Math.Abs(projectile.velocity.X) < 1f)
                {
                    projectile.velocity.X = -projectile.velocity.X;
                }
                else
                {
                    projectile.Kill();
                }
            }
            if (projectile.velocity.Y != projectile.velocity.Y)
            {
                if (Math.Abs(projectile.velocity.Y) < 1f)
                {
                    projectile.velocity.Y = -projectile.velocity.Y;
                }
                else
                {
                    projectile.Kill();
                }
            }
            Vector2 center = projectile.Center;
            projectile.scale = 1f - projectile.localAI[0];
            projectile.width = (int)(20f * projectile.scale);
            projectile.height = projectile.width;
            projectile.position.X = center.X - (projectile.width / 2);
            projectile.position.Y = center.Y - (projectile.height / 2);
            if (projectile.localAI[0] < 0.1)
            {
                projectile.localAI[0] += 0.01f;
            }
            else
            {
                projectile.localAI[0] += 0.025f;
            }
            if (projectile.localAI[0] >= 0.95f)
            {
                projectile.Kill();
            }
            projectile.velocity.X = projectile.velocity.X + projectile.ai[0] * 2f;
            projectile.velocity.Y = projectile.velocity.Y + projectile.ai[1] * 2f;
            if (projectile.velocity.Length() > 16f)
            {
                projectile.velocity.Normalize();
                projectile.velocity *= 16f;
            }
            projectile.ai[0] *= 1.05f;
            projectile.ai[1] *= 1.05f;
            if (projectile.scale < 1f)
            {
                int num = 0;
                while (num < projectile.scale * 10f)
                {
                    int num2 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, (Main.rand.Next(2) == 0) ? 183 : 235, projectile.velocity.X, projectile.velocity.Y, 100, default(Color), 1.5f);
                    Main.dust[num2].position = (Main.dust[num2].position + projectile.Center) / 2f;
                    Main.dust[num2].noGravity = true;
                    Main.dust[num2].velocity *= 0.1f;
                    Main.dust[num2].velocity -= projectile.velocity * (1.3f - projectile.scale);
                    Main.dust[num2].scale += projectile.scale * 0.05f;
                    num++;
                }
            }
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
            if (projectile.penetrate == 1)
            {
                projectile.penetrate++;
            }
            target.AddBuff(BuffID.Bleeding, 300);
            if (!Main.player[projectile.owner].moonLeech && target.type != 488)
            {
                float num = damage * 0.003f;
                if ((int)num == 0)
                {
                    return;
                }
                if (Main.player[Main.myPlayer].lifeSteal <= 0f)
                {
                    return;
                }
                Main.player[Main.myPlayer].lifeSteal -= num;
                int num2 = projectile.owner;
                Projectile.NewProjectile(target.Center.X, target.Center.Y, 0f, 0f, mod.ProjectileType("RedHeal"), 0, 0f, projectile.owner, num2, num);
            }
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(BuffID.Bleeding, 300);
        }
        public override void OnHitPvp(Player target, int damage, bool crit)
        {
            if (projectile.penetrate == 1)
            {
                projectile.penetrate++;
            }
            target.AddBuff(BuffID.Bleeding, 300);
            if (!Main.player[projectile.owner].moonLeech)
            {
                float num = damage * 0.003f;
                if ((int)num == 0)
                {
                    return;
                }
                if (Main.player[Main.myPlayer].lifeSteal <= 0f)
                {
                    return;
                }
                Main.player[Main.myPlayer].lifeSteal -= num;
                int num2 = projectile.owner;
                Projectile.NewProjectile(target.Center.X, target.Center.Y, 0f, 0f, mod.ProjectileType("RedHeal"), 0, 0f, projectile.owner, num2, num);
            }
        }
    }
}