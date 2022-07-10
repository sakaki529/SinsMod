using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace SinsMod.Projectiles.Thrown
{
    public class Sakurahubuki : ModProjectile
	{
        public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Sakurahubuki");
            DisplayName.AddTranslation(GameCulture.Chinese, "÷á");
        }
		public override void SetDefaults()
		{
            projectile.width = 15;
			projectile.height = 15;
            projectile.timeLeft = 450;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.melee = false;
            projectile.thrown = true;
            projectile.penetrate = 1;
        }
        public override void AI()
        {
            projectile.ai[0] += 1f;
            if (projectile.ai[0] >= 390f)
            {
                projectile.alpha += 4;
                projectile.damage = (int)(projectile.damage * 0.95);
                projectile.knockBack = (int)(projectile.knockBack * 0.95);
            }
            if (projectile.ai[0] < 390f)
            {
                projectile.rotation = (float)Math.Atan2(projectile.velocity.Y, projectile.velocity.X) + 1.57f;
            }
            else
            {
                projectile.rotation += 0.4f;
            }
            float num = projectile.Center.X;
            float num2 = projectile.Center.Y;
            float num3 = 600f;
            bool flag = false;
            for (int num4 = 0; num4 < 200; num4++)
            {
                if (Main.npc[num4].CanBeChasedBy(projectile, false) && Collision.CanHit(projectile.Center, 1, 1, Main.npc[num4].Center, 1, 1))
                {
                    float num5 = Main.npc[num4].position.X + (Main.npc[num4].width / 2);
                    float num6 = Main.npc[num4].position.Y + (Main.npc[num4].height / 2);
                    float num7 = Math.Abs(projectile.position.X + (projectile.width / 2) - num5) + Math.Abs(projectile.position.Y + (projectile.height / 2) - num6);
                    if (num7 < num3)
                    {
                        num3 = num7;
                        num = num5;
                        num2 = num6;
                        flag = true;
                    }
                }
            }
            if (flag)
            {
                Vector2 vector = new Vector2(projectile.position.X + projectile.width * 0.5f, projectile.position.Y + projectile.height * 0.5f);
                float num8 = 27f;
                float num9 = num - vector.X;
                float num10 = num2 - vector.Y;
                float num11 = (float)Math.Sqrt(num9 * num9 + num10 * num10);
                num11 = num8 / num11;
                num9 *= num11;
                num10 *= num11;
                projectile.velocity.X = (projectile.velocity.X * 20f + num9) / 21f;
                projectile.velocity.Y = (projectile.velocity.Y * 20f + num10) / 21f;
            }
            if (Main.rand.Next(10) == 0)
            {
                Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, 72, projectile.velocity.X * 0.3f, projectile.velocity.Y * 0.3f, 100, default(Color), 0.8f);
            }
        }
        public override void Kill(int timeLeft)
        {
            for (int num = 0; num < 3; num++)
            {
                int num2 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 72, 0f, 0f, 100, default(Color), 0.8f);
                Main.dust[num2].noGravity = true;
                Main.dust[num2].velocity *= 1.2f;
                Main.dust[num2].velocity -= projectile.oldVelocity * 0.3f;
            }
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (!Main.player[projectile.owner].moonLeech)
            {
                if (target.type != 488)
                {
                    float num = damage * 0.004f;
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
                    Projectile.NewProjectile(target.Center.X, target.Center.Y, 0f, 0f, mod.ProjectileType("SakuraHeal"), 0, 0f, projectile.owner, num2, num);
                }
            }
        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            damage += target.defense / 2;
        }
        public override void ModifyHitPlayer(Player target, ref int damage, ref bool crit)
        {
            damage += target.statDefense / 2;
        }
        public override void OnHitPvp(Player target, int damage, bool crit)
        {
            if (!Main.player[projectile.owner].moonLeech)
            {
                float num = damage * 0.004f;
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
                Projectile.NewProjectile(target.Center.X, target.Center.Y, 0f, 0f, mod.ProjectileType("SakuraHeal"), 0, 0f, projectile.owner, num2, num);
            }
        }
        public override void ModifyHitPvp(Player target, ref int damage, ref bool crit)
        {
            damage += target.statDefense / 2;
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White * (1f - projectile.alpha / 255f);
        }
    }
}