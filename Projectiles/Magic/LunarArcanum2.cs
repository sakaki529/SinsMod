using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

namespace SinsMod.Projectiles.Magic
{
    public class LunarArcanum2 : ModProjectile
    {
        public override string Texture => "SinsMod/Extra/Placeholder/BlankTex";
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Lunar Arcanum");
        }
		public override void SetDefaults()
		{
            projectile.tileCollide = false;
            projectile.width = 18;
            projectile.height = 30;
            projectile.penetrate = 12;
            projectile.timeLeft = 420;
            projectile.magic = true;
            projectile.friendly = true;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 1;
        }
        public override void AI()
        {
            int num1024 = mod.ProjectileType("LunarArcanum");
            float num1025 = 420f;
            float x3 = 0.15f;
            float y3 = 0.15f;
            bool flag60 = false;
            bool flag61 = true;
            if (flag61)
            {
                int num1026 = (int)projectile.ai[1];
                if (!Main.projectile[num1026].active || Main.projectile[num1026].type != num1024)
                {
                    projectile.Kill();
                    return;
                }
                projectile.timeLeft = 2;
            }
            float[] var_2_2EA1E_cp_0 = projectile.ai;
            int var_2_2EA1E_cp_1 = 0;
            float num73 = var_2_2EA1E_cp_0[var_2_2EA1E_cp_1];
            var_2_2EA1E_cp_0[var_2_2EA1E_cp_1] = num73 + 1f;
            if (projectile.ai[0] < num1025)
            {
                bool flag62 = true;
                int num1027 = (int)projectile.ai[1];
                if (Main.projectile[num1027].active && Main.projectile[num1027].type == num1024)
                {
                    if (!flag60 && Main.projectile[num1027].oldPos[1] != Vector2.Zero)
                    {
                        projectile.position += Main.projectile[num1027].position - Main.projectile[num1027].oldPos[1];
                    }
                    if (projectile.Center.HasNaNs())
                    {
                        projectile.Kill();
                        return;
                    }
                }
                if (!flag60)
                {
                    projectile.velocity += new Vector2((float)Math.Sign(Main.projectile[num1027].Center.X - projectile.Center.X), (float)Math.Sign(Main.projectile[num1027].Center.Y - projectile.Center.Y)) * new Vector2(x3, y3);
                    if (projectile.velocity.Length() > 6f)
                    {
                        projectile.velocity *= 6f / projectile.velocity.Length();
                    }
                }
                if (Main.rand.Next(2) == 0)
                {
                    int num1028 = Dust.NewDust(projectile.Center, 8, 8, 110, 0f, 0f, 0, default(Color), 1.5f);
                    Main.dust[num1028].position = projectile.Center;
                    Main.dust[num1028].velocity = projectile.velocity;
                    Main.dust[num1028].noGravity = true;
                    if (flag62)
                    {
                        Main.dust[num1028].customData = Main.projectile[(int)projectile.ai[1]];
                    }
                }
                projectile.alpha = 255;
            }
        }
        public override void Kill(int timeLeft)
        {
            projectile.ai[0] = 110f;
            int num3;
            for (int num114 = 0; num114 < 10; num114 = num3 + 1)
            {
                int num115 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, (int)projectile.ai[0], projectile.velocity.X * 0.1f, projectile.velocity.Y * 0.1f, 0, default(Color), 0.5f);
                Dust dust;
                if (Main.rand.Next(3) == 0)
                {
                    Main.dust[num115].fadeIn = 0.75f + (float)Main.rand.Next(-10, 11) * 0.01f;
                    Main.dust[num115].scale = 0.25f + (float)Main.rand.Next(-10, 11) * 0.005f;
                    dust = Main.dust[num115];
                    //dust.type++;
                }
                else
                {
                    Main.dust[num115].scale = 1f + (float)Main.rand.Next(-10, 11) * 0.01f;
                }
                Main.dust[num115].noGravity = true;
                dust = Main.dust[num115];
                dust.velocity *= 1.25f;
                dust = Main.dust[num115];
                dust.velocity -= projectile.oldVelocity / 10f;
                num3 = num114;
            }
        }
    }
}