using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

namespace SinsMod.Projectiles.Magic
{
    public class FlamingPumpkin : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Anima");
            Main.projFrames[projectile.type] = 3;
        }
        public override void SetDefaults()
        {
            projectile.width = 24;
            projectile.height = 24;
            projectile.aiStyle = -1;
            projectile.friendly = true;
            projectile.magic = true;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
            projectile.timeLeft = 360;
            projectile.scale = 0.8f;
            projectile.GetGlobalProjectile<SinsProjectile>().drawCenter = true;
        }
        public override void AI()
        {
            projectile.frameCounter++;
            if (projectile.frameCounter > 0)
            {
                projectile.frame++;
                projectile.frameCounter = 0;
                if (projectile.frame >= Main.projFrames[projectile.type])
                {
                    projectile.frame = 0;
                }
            }
            if (Main.rand.NextBool(3))
            {
                Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, 6, projectile.velocity.X * 0.5f, projectile.velocity.Y * 0.5f);
            }
            if (projectile.velocity.X >= 0f)
            {
                projectile.spriteDirection = 1;
                projectile.rotation = (float)Math.Atan2(projectile.velocity.Y, projectile.velocity.X);
            }
            else
            {
                projectile.spriteDirection = -1;
                projectile.rotation = (float)Math.Atan2(-projectile.velocity.Y, -projectile.velocity.X);
            }
            float num = projectile.Center.X;
            float num2 = projectile.Center.Y;
            float num3 = 2200f;
            bool flag = false;
            for (int num4 = 0; num4 < 200; num4++)
            {
                if (Main.npc[num4].CanBeChasedBy(projectile, false))
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
                float num8 = 20f;
                Vector2 vector = new Vector2(projectile.position.X + projectile.width * 0.5f, projectile.position.Y + projectile.height * 0.5f);
                float num9 = num - vector.X;
                float num10 = num2 - vector.Y;
                float num11 = (float)Math.Sqrt(num9 * num9 + num10 * num10);
                num11 = num8 / num11;
                num9 *= num11;
                num10 *= num11;
                projectile.velocity.X = (projectile.velocity.X * 14f + num9) / 15f;
                projectile.velocity.Y = (projectile.velocity.Y * 14f + num10) / 15f;
            }
        }
        public override void Kill(int timeLeft)
        {
            int num;
            for (int num2 = 0; num2 < 10; num2 = num + 1)
            {
                int num3 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 6, -projectile.velocity.X * 0.2f, -projectile.velocity.Y * 0.2f, 100, default(Color), 2f);
                Main.dust[num3].noGravity = true;
                Dust dust = Main.dust[num3];
                dust.velocity *= 2f;
                num3 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 6, -projectile.velocity.X * 0.2f, -projectile.velocity.Y * 0.2f, 100, default(Color), 1f);
                dust = Main.dust[num3];
                dust.velocity *= 2f;
                num = num2;
            }
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(200, 200, 200, 0);
        }
    }
}