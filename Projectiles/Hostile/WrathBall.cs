using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ModLoader;

namespace SinsMod.Projectiles.Hostile
{
    public class WrathBall : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Awakened Wrath");
            Main.projFrames[projectile.type] = 4;
        }
        public override void SetDefaults()
        {
            projectile.width = 44;
            projectile.height = 44;
            projectile.friendly = false;
            projectile.hostile = true;
            projectile.penetrate = 1;
            projectile.timeLeft = 300;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D texture = Main.projectileTexture[projectile.type];
            Color color = Color.Lerp(Color.Red, Color.DarkRed, (float)Math.Cos(6.28318548f * (Main.LocalPlayer.miscCounter / 100f)) * 0.4f + 0.5f);
            color.A = (byte)(255 - projectile.alpha);
            int num = Main.projectileTexture[projectile.type].Height / Main.projFrames[projectile.type];
            int num2 = num * projectile.frame;
            Main.spriteBatch.Draw(texture, projectile.Center - Main.screenPosition + new Vector2(0f, projectile.gfxOffY), new Rectangle(0, num2, texture.Width, num), projectile.GetAlpha(projectile.GetAlpha(color)), projectile.rotation, new Vector2(texture.Width / 2f, num / 2f), projectile.scale, 0, 0f);
            return false;
        }
        public override void AI()
        {
            projectile.rotation = projectile.velocity.ToRotation() + (float)Math.PI / 2f;
            projectile.frameCounter++;
            if (projectile.frameCounter >= 3)
            {
                projectile.frame++;
                projectile.frameCounter = 0;
            }
            if (projectile.frame >= Main.projFrames[projectile.type])
            {
                projectile.frame = 0;
            }
            float num = 120f;
            float num2 = 48f;
            float num3 = 100f;
            if (projectile.timeLeft > 30 && projectile.alpha > 50)
            {
                projectile.alpha -= 25;
            }
            if (projectile.timeLeft > 30 && projectile.alpha < 50 && Collision.SolidCollision(projectile.position, projectile.width, projectile.height))
            {
                projectile.alpha = 50;
            }
            if (projectile.alpha < 0)
            {
                projectile.alpha = 0;
            }
            Lighting.AddLight(projectile.Center, 1f, 0f, 0f);
            int num4 = (int)projectile.ai[0];
            if (num4 >= 0 && Main.player[num4].active && !Main.player[num4].dead)
            {
                if (projectile.Distance(Main.player[num4].Center) > num3)
                {
                    Vector2 vector = projectile.DirectionTo(Main.player[num4].Center);
                    if (vector.HasNaNs())
                    {
                        vector = Vector2.UnitY;
                    }
                    projectile.velocity = (projectile.velocity * (num - 1f) + vector * num2) / num;
                }
                if (Vector2.Distance(Main.player[num4].Center, projectile.Center) <= 64f)
                {
                    projectile.Kill();
                }
            }
            else
            {
                if (projectile.ai[0] != -1f)
                {
                    projectile.ai[0] = -1f;
                    projectile.netUpdate = true;
                }
            }
            projectile.localAI[0] += 1f;
            if (projectile.localAI[0] == 4f)
            {
                projectile.localAI[0] = 0f;
                for (int i = 0; i < 12; i++)
                {
                    Vector2 vector = Vector2.UnitX * -projectile.width / 2f;
                    vector += -Vector2.UnitY.RotatedBy(i * 3.14159274f / 6f, default(Vector2)) * new Vector2(8f, 16f);
                    vector = vector.RotatedBy(projectile.rotation - 1.57079637f, default(Vector2));
                    int num5 = Dust.NewDust(projectile.Center, 0, 0, 235, 0f, 0f, 160, default(Color), 1.1f);
                    Main.dust[num5].scale = 1.1f;
                    Main.dust[num5].noGravity = true;
                    Main.dust[num5].position = projectile.Center + vector;
                    Main.dust[num5].velocity = projectile.velocity * 0.1f;
                    Main.dust[num5].velocity = Vector2.Normalize(projectile.Center - projectile.velocity * 3f - Main.dust[num5].position) * 1.25f;
                }
            }
            projectile.localAI[1] += 1f;
            if (projectile.localAI[1] > 4f)
            {
                if (Main.rand.Next(4) == 0)
                {
                    for (int m = 0; m < 1; m++)
                    {
                        Vector2 value = -Vector2.UnitX.RotatedByRandom(0.19634954631328583).RotatedBy(projectile.velocity.ToRotation(), default(Vector2));
                        int num10 = Dust.NewDust(projectile.position, projectile.width, projectile.height, 31, 0f, 0f, 100, default(Color), 1f);
                        Main.dust[num10].velocity *= 0.1f;
                        Main.dust[num10].position = projectile.Center + value * projectile.width / 2f;
                        Main.dust[num10].fadeIn = 0.9f;
                    }
                }
                if (Main.rand.Next(32) == 0)
                {
                    for (int n = 0; n < 1; n++)
                    {
                        Vector2 value2 = -Vector2.UnitX.RotatedByRandom(0.39269909262657166).RotatedBy(projectile.velocity.ToRotation(), default(Vector2));
                        int num11 = Dust.NewDust(projectile.position, projectile.width, projectile.height, 31, 0f, 0f, 155, default(Color), 0.8f);
                        Main.dust[num11].velocity *= 0.3f;
                        Main.dust[num11].position = projectile.Center + value2 * projectile.width / 2f;
                        if (Main.rand.Next(2) == 0)
                        {
                            Main.dust[num11].fadeIn = 1.4f;
                        }
                    }
                }
                if (Main.rand.Next(2) == 0)
                {
                    for (int num12 = 0; num12 < 2; num12++)
                    {
                        Vector2 value3 = -Vector2.UnitX.RotatedByRandom(0.78539818525314331).RotatedBy(projectile.velocity.ToRotation(), default(Vector2));
                        int num13 = Dust.NewDust(projectile.position, projectile.width, projectile.height, 60, 0f, 0f, 0, default(Color), 1.2f);
                        Main.dust[num13].velocity *= 0.3f;
                        Main.dust[num13].noGravity = true;
                        Main.dust[num13].position = projectile.Center + value3 * (float)projectile.width / 2f;
                        if (Main.rand.Next(2) == 0)
                        {
                            Main.dust[num13].fadeIn = 1.4f;
                        }
                    }
                }
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
            Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 14, 1f, 0f);
            projectile.position.X = projectile.position.X + (projectile.width / 2);
            projectile.position.Y = projectile.position.Y + (projectile.height / 2);
            projectile.width = 180;
            projectile.height = 180;
            projectile.position.X = projectile.position.X - (projectile.width / 2);
            projectile.position.Y = projectile.position.Y - (projectile.height / 2);
            projectile.Damage();
            for (int i = 0; i < 30; i++)
            {
                int num = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 60, 0f, 0f, 100, default(Color), 1.2f);
                Main.dust[num].velocity *= 3f;
                Main.dust[num].noGravity = true;
                if (Main.rand.Next(2) == 0)
                {
                    Main.dust[num].scale = 0.5f;
                    Main.dust[num].fadeIn = 1f + Main.rand.Next(10) * 0.1f;
                }
            }
            for (int j = 0; j < 60; j++)
            {
                int num2 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 60, 0f, 0f, 100, default(Color), 1.7f);
                Main.dust[num2].noGravity = true;
                Main.dust[num2].velocity *= 5f;
                num2 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 60, 0f, 0f, 100, default(Color), 1f);
                Main.dust[num2].noGravity = true;
                Main.dust[num2].velocity *= 2f;
            }
        }
    }
}