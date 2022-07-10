using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

namespace SinsMod.Projectiles.Heal
{
    public class NatureHeal : ModProjectile
    {
        public override string Texture => "SinsMod/Extra/Placeholder/BlankTex";
        public override void SetDefaults()
        {
            projectile.width = 6;
            projectile.height = 6;
            //projectile.aiStyle = 52;
            projectile.tileCollide = false;
            projectile.alpha = 255;
            projectile.extraUpdates = 10;
        }
        public override void AI()
        {
            int num = (int)projectile.ai[0];
            float num2 = 4f;
            Vector2 vector2 = new Vector2(projectile.position.X + projectile.width * 0.5f, projectile.position.Y + projectile.height * 0.5f);
            float num3 = Main.player[num].Center.X - vector2.X;
            float num4 = Main.player[num].Center.Y - vector2.Y;
            float num5 = (float)Math.Sqrt(num3 * num3 + num4 * num4);
            if (num5 < 50f && projectile.position.X < Main.player[num].position.X + Main.player[num].width && projectile.position.X + projectile.width > Main.player[num].position.X && projectile.position.Y < Main.player[num].position.Y + Main.player[num].height && projectile.position.Y + projectile.height > Main.player[num].position.Y)
            {
                if (projectile.owner == Main.myPlayer && !Main.player[Main.myPlayer].moonLeech)
                {
                    int num6 = (int)projectile.ai[1];
                    Main.player[num].HealEffect(num6, false);
                    Main.player[num].statLife += num6;
                    if (Main.player[num].statLife > Main.player[num].statLifeMax2)
                    {
                        Main.player[num].statLife = Main.player[num].statLifeMax2;
                    }
                    NetMessage.SendData(66, -1, -1, null, num, num6, 0f, 0f, 0, 0, 0);
                }
                projectile.Kill();
            }
            num5 = num2 / num5;
            num3 *= num5;
            num4 *= num5;
            projectile.velocity.X = (projectile.velocity.X * 15f + num3) / 16f;
            projectile.velocity.Y = (projectile.velocity.Y * 15f + num4) / 16f;
            for (int num7 = 0; num7 < 5; num7++)
            {
                float num8 = projectile.velocity.X * 0.2f * num7;
                float num9 = -(projectile.velocity.Y * 0.2f) * num7;
                int num10 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 110, 0f, 0f, 100, default(Color), 0.7f);
                Main.dust[num10].noGravity = true;
                Main.dust[num10].velocity *= 0f;
                Dust dust = Main.dust[num10];
                dust.position.X = dust.position.X - num8;
                Dust dust2 = Main.dust[num10];
                dust2.position.Y = dust2.position.Y - num9;
            }
        }
    }
}
