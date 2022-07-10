using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Projectiles.Thrown
{
    public class PolarNightKnives : ModProjectile
	{
        private int Bounce = 6;
        public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Polar Night Knives");
		}
		public override void SetDefaults()
		{
            projectile.width = 14;
			projectile.height = 14;
            projectile.thrown = true;
            projectile.penetrate = 1;
            projectile.timeLeft = 150;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.ignoreWater = true;
            projectile.extraUpdates = 1;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 0;
        }
        public override void AI()
        {
            projectile.rotation = (float)Math.Atan2(projectile.velocity.Y, projectile.velocity.X) + 1.57f;
            if (Main.rand.Next(6) == 0)
            {
                int d = Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, 186, projectile.velocity.X * 0.1f, projectile.velocity.Y * 0.1f, 0, new Color(255, 255, 255, 100), 0.8f);
                Main.dust[d].noGravity = true;
                Main.dust[d].shader = GameShaders.Armor.GetSecondaryShader(56, Main.LocalPlayer);
            }
            projectile.ai[0]++;
            if (projectile.ai[0] >= 15)
            {
                float num = projectile.Center.X;
                float num2 = projectile.Center.Y;
                float num3 = 400f;
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
                    float num8 = 8f;
                    Vector2 vector = new Vector2(projectile.position.X + projectile.width * 0.5f, projectile.position.Y + projectile.height * 0.5f);
                    float num9 = num - vector.X;
                    float num10 = num2 - vector.Y;
                    float num11 = (float)Math.Sqrt(num9 * num9 + num10 * num10);
                    num11 = num8 / num11;
                    num9 *= num11;
                    num10 *= num11;
                    projectile.velocity.X = (projectile.velocity.X * 6f + num9) / 7f;
                    projectile.velocity.Y = (projectile.velocity.Y * 6f + num10) / 7f;
                }
            }
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.Bleeding, 60);
            if (!Main.player[projectile.owner].moonLeech)
            {
                if (target.type != 488)
                {
                    float num = damage * 0.001f;
                    if ((int)num <= 0)
                    {
                        return;
                    }
                    Main.player[Main.myPlayer].statLife += (int)num;
                    Main.player[Main.myPlayer].HealEffect((int)num);
                }
            }
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(BuffID.Bleeding, 60);
        }
        public override void OnHitPvp(Player target, int damage, bool crit)
        {
            target.AddBuff(BuffID.Bleeding, 60);
            if (!Main.player[projectile.owner].moonLeech)
            {
                float num = damage * 0.001f;
                if ((int)num <= 0)
                {
                    return;
                }
                Main.player[Main.myPlayer].statLife += (int)num;
                Main.player[Main.myPlayer].HealEffect((int)num);
            }
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Bounce--;
            if (Bounce <= 0)
            {
                projectile.Kill();
            }
            else
            {
                if (projectile.velocity.X != oldVelocity.X)
                {
                    projectile.velocity.X = -oldVelocity.X;
                }
                if (projectile.velocity.Y != oldVelocity.Y)
                {
                    projectile.velocity.Y = -oldVelocity.Y;
                }
            }
            return false;
        }
        public override void Kill(int timeLeft)
        {
            for (int num = 0; num < 10; num++)
            {
                int num2 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 186, -projectile.velocity.X * 0.2f, -projectile.velocity.Y * 0.2f, 0, default(Color), 0.6f);
                Main.dust[num2].noGravity = true;
                Main.dust[num2].shader = GameShaders.Armor.GetSecondaryShader(56, Main.LocalPlayer);
                Dust dust = Main.dust[num2];
                dust.velocity *= 0.6f;
                num2 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 186, -projectile.velocity.X * 0.2f, -projectile.velocity.Y * 0.2f, 0, default(Color), 0.6f);
                dust = Main.dust[num2];
                dust.noGravity = true;
                dust.velocity *= 0.6f;
                dust.shader = GameShaders.Armor.GetSecondaryShader(56, Main.LocalPlayer);
            }
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White * (1f - projectile.alpha / 255f);
        }
    }
}