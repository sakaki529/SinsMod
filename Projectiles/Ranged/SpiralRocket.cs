using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Projectiles.Ranged
{
    public class SpiralRocket : ModProjectile
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Spiral Rocket");
        }
        public override void SetDefaults()
		{
			projectile.width = 14;
			projectile.height = 14;
            projectile.aiStyle = 0;
            aiType = 1;
            projectile.ranged = true;
			projectile.friendly = true;
            projectile.extraUpdates = 2;
            projectile.timeLeft = 90 * projectile.MaxUpdates;
            projectile.penetrate = 1;
            projectile.alpha = 255;
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D texture = Main.projectileTexture[projectile.type];
            Texture2D glowTexture = mod.GetTexture("Glow/Projectile/SpiralRocket_Glow");
            int num = Main.projectileTexture[projectile.type].Height / Main.projFrames[projectile.type];
            int num2 = num * projectile.frame;
            Main.spriteBatch.Draw(texture, projectile.Center - Main.screenPosition + new Vector2(0f, projectile.gfxOffY), new Rectangle?(new Rectangle(0, num2, texture.Width, num)), projectile.GetAlpha(lightColor), projectile.rotation, new Vector2(texture.Width / 2f, num / 2f), projectile.scale, SpriteEffects.None, 0f);
            Main.spriteBatch.Draw(glowTexture, projectile.Center - Main.screenPosition + new Vector2(0f, projectile.gfxOffY), new Rectangle?(new Rectangle(0, num2, texture.Width, num)), projectile.GetAlpha(new Color(255, 255, 255, 127)), projectile.rotation, new Vector2(texture.Width / 2f, num / 2f), projectile.scale, SpriteEffects.None, 0f);
            return false;
        }
        public override void AI()
		{
            if (projectile.alpha < 170)
            {
                float num = 3f;
                int num2 = 0;
                while (num2 < num)
                {
                    int dust = Dust.NewDust(projectile.position, 1, 1, 91, 0f, 0f, 0, default(Color), 1f);
                    Main.dust[dust].position = projectile.Center - projectile.velocity / num * num2;
                    Main.dust[dust].velocity *= 0f;
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].alpha = 200;
                    Main.dust[dust].scale = 0.5f;
                    num2++;
                }
            }
            float num3 = (float)Math.Sqrt(projectile.velocity.X * projectile.velocity.X + projectile.velocity.Y * projectile.velocity.Y);
            float num4 = projectile.localAI[0];
            if (num4 == 0f)
            {
                projectile.localAI[0] = num3;
                num4 = num3;
            }
            if (projectile.alpha > 0)
            {
                projectile.alpha -= 25;
            }
            if (projectile.alpha < 0)
            {
                projectile.alpha = 0;
            }
            float num5 = projectile.position.X;
            float num6 = projectile.position.Y;
            float num7 = 800f;
            bool flag = false;
            int num8 = 0;
            projectile.ai[0] += 1f;
            if (projectile.ai[0] > 20f)
            {
                projectile.ai[0] -= 1f;
                if (projectile.ai[1] == 0f)
                {
                    for (int num9 = 0; num9 < 200; num9++)
                    {
                        if (Main.npc[num9].CanBeChasedBy(projectile, false) && (projectile.ai[1] == 0f || projectile.ai[1] == num9 + 1))
                        {
                            float num10 = Main.npc[num9].position.X + (Main.npc[num9].width / 2);
                            float num11 = Main.npc[num9].position.Y + (Main.npc[num9].height / 2);
                            float num12 = Math.Abs(projectile.position.X + (projectile.width / 2) - num10) + Math.Abs(projectile.position.Y + (projectile.height / 2) - num11);
                            if (num12 < num7 && Collision.CanHit(new Vector2(projectile.position.X + (projectile.width / 2), projectile.position.Y + (projectile.height / 2)), 1, 1, Main.npc[num9].position, Main.npc[num9].width, Main.npc[num9].height))
                            {
                                num7 = num12;
                                num5 = num10;
                                num6 = num11;
                                flag = true;
                                num8 = num9;
                            }
                        }
                    }
                    if (flag)
                    {
                        projectile.ai[1] = num8 + 1;
                    }
                    flag = false;
                }
                if (projectile.ai[1] != 0f)
                {
                    int num13 = (int)(projectile.ai[1] - 1f);
                    if (Main.npc[num13].active && Main.npc[num13].CanBeChasedBy(projectile, true))
                    {
                        float num14 = Main.npc[num13].position.X + (Main.npc[num13].width / 2);
                        float num15 = Main.npc[num13].position.Y + (Main.npc[num13].height / 2);
                        if (Math.Abs(projectile.position.X + (projectile.width / 2) - num14) + Math.Abs(projectile.position.Y + (projectile.height / 2) - num15) < 1000f)
                        {
                            flag = true;
                            num5 = Main.npc[num13].position.X + (Main.npc[num13].width / 2);
                            num6 = Main.npc[num13].position.Y + (Main.npc[num13].height / 2);
                        }
                    }
                }
                if (!projectile.friendly)
                {
                    flag = false;
                }
                if (flag)
                {
                    float value = num4;
                    Vector2 vector = new Vector2(projectile.position.X + projectile.width * 0.5f, projectile.position.Y + projectile.height * 0.5f);
                    float num16 = num5 - vector.X;
                    float num17 = num6 - vector.Y;
                    float num18 = (float)Math.Sqrt(num16 * num16 + num17 * num17);
                    num18 = value / num18;
                    num16 *= num18;
                    num17 *= num18;
                    int num19 = 8;
                    projectile.velocity.X = (projectile.velocity.X * (num19 - 1) + num16) / num19;
                    projectile.velocity.Y = (projectile.velocity.Y * (num19 - 1) + num17) / num19;
                }
            }
            projectile.rotation = projectile.velocity.ToRotation() + 1.57079637f;
        }
        public override void Kill(int timeLeft)
        {
            Main.PlaySound(SoundID.Item14, projectile.position);
            projectile.position = projectile.Center;
            projectile.width = projectile.height = 80;
            projectile.position.X = projectile.position.X - (projectile.width / 2);
            projectile.position.Y = projectile.position.Y - (projectile.height / 2);
            int num;
            for (int num2 = 0; num2 < 4; num2 = num + 1)
            {
                Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 31, 0f, 0f, 100, default(Color), 1.5f);
                num = num2;
            }
            for (int num3 = 0; num3 < 40; num3 = num + 1)
            {
                int num4 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 91, 0f, 0f, 200, default(Color), 2.5f);
                Main.dust[num4].noGravity = true;
                Dust dust = Main.dust[num4];
                dust.velocity *= 2f;
                num4 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 91, 0f, 0f, 200, default(Color), 1.5f);
                dust = Main.dust[num4];
                dust.velocity *= 1.2f;
                Main.dust[num4].noGravity = true;
                num = num3;
            }
            for (int num5 = 0; num5 < 1; num5 = num + 1)
            {
                int num6 = Gore.NewGore(projectile.position + new Vector2(projectile.width * Main.rand.Next(100) / 100f, projectile.height * Main.rand.Next(100) / 100f) - Vector2.One * 10f, default(Vector2), Main.rand.Next(61, 64), 1f);
                Gore gore = Main.gore[num6];
                gore.velocity *= 0.3f;
                Gore gore2 = Main.gore[num6];
                gore2.velocity.X = gore2.velocity.X + Main.rand.Next(-10, 11) * 0.05f;
                Gore gore3 = Main.gore[num6];
                gore3.velocity.Y = gore3.velocity.Y + Main.rand.Next(-10, 11) * 0.05f;
                num = num5;
            }
            projectile.Damage();
        }
    }
}