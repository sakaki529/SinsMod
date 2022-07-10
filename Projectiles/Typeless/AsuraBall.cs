using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Projectiles.Typeless
{
	public class AsuraBall : ModProjectile
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Madness");
            Main.projFrames[projectile.type] = 4;
        }
        public override void SetDefaults()
		{
			projectile.width = 40;
			projectile.height = 40;
			projectile.aiStyle = 0;
            projectile.friendly = true;
            projectile.hostile = false;
			projectile.ignoreWater = true;
			projectile.penetrate = 1;
			projectile.timeLeft = 900;
		}
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D texture2D = Main.projectileTexture[projectile.type];
            int num = Main.projectileTexture[projectile.type].Height / Main.projFrames[projectile.type];
            int num2 = num * projectile.frame;
            Main.spriteBatch.Draw(texture2D, projectile.Center - Main.screenPosition + new Vector2(0f, projectile.gfxOffY), new Rectangle?(new Rectangle(0, num2, texture2D.Width, num)), projectile.GetAlpha(lightColor), projectile.rotation, new Vector2(texture2D.Width / 2f, num / 2f), projectile.scale, 0, 0f);
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
            if (projectile.frame >= 4)
            {
                projectile.frame = 0;
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
                    int num8 = Dust.NewDust(projectile.Center, 0, 0, 21, 0f, 0f, 160, default(Color), 1.1f);
                    Main.dust[num8].scale = 1.1f;
                    Main.dust[num8].noGravity = true;
                    Main.dust[num8].position = projectile.Center + vector;
                    Main.dust[num8].velocity = projectile.velocity * 0.1f;
                    Main.dust[num8].velocity = Vector2.Normalize(projectile.Center - projectile.velocity * 3f - Main.dust[num8].position) * 1.25f;
                }
            }
            if (projectile.ai[1] == 0f)
            {
                projectile.ai[1] = 1f;
                Main.PlaySound(SoundID.Item34, projectile.position);
            }
            else
            {
                if (projectile.ai[1] == 1f && Main.netMode != 1)
                {
                    int num3 = -1;
                    float num4 = 2000f;
                    for (int k = 0; k < 200; k++)
                    {
                        if (Main.npc[k].CanBeChasedBy(projectile, false))
                        {
                            Vector2 center = Main.npc[k].Center;
                            float num5 = Vector2.Distance(center, projectile.Center);
                            if ((num5 < num4 || num3 == -1) && Collision.CanHit(projectile.Center, 1, 1, center, 1, 1))
                            {
                                num4 = num5;
                                num3 = k;
                            }
                        }
                    }
                    if (num4 < 20f)
                    {
                        projectile.Kill();
                        return;
                    }
                    if (num3 != -1)
                    {
                        projectile.ai[1] = 21f;
                        projectile.ai[0] = num3;
                        projectile.netUpdate = true;
                    }
                }
                else
                {
                    if (projectile.ai[1] > 20f && projectile.ai[1] < 200f)
                    {
                        projectile.ai[1] += 1f;
                        int num6 = (int)projectile.ai[0];
                        if (Main.npc[num6].CanBeChasedBy(projectile, false))
                        {
                            projectile.ai[1] = 1f;
                            projectile.ai[0] = 0f;
                            projectile.netUpdate = true;
                        }
                        else
                        {
                            float num7 = projectile.velocity.ToRotation();
                            Vector2 vector2 = Main.npc[num6].Center - projectile.Center;
                            if (vector2.Length() < 20f)
                            {
                                projectile.Kill();
                                return;
                            }
                            float targetAngle = vector2.ToRotation();
                            if (vector2 == Vector2.Zero)
                            {
                                targetAngle = num7;
                            }
                            float num8 = num7.AngleLerp(targetAngle, 0.008f);
                            projectile.velocity = new Vector2(projectile.velocity.Length(), 0f).RotatedBy(num8, default(Vector2));
                        }
                    }
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
                        int num13 = Dust.NewDust(projectile.position, projectile.width, projectile.height, 173, 0f, 0f, 0, default(Color), 1.2f);
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
            /*else
            {
                if (projectile.ai[1] == 1f && Main.netMode != 1)
                {
                    int num3 = -1;
                    float num4 = 2000f;
                    for (int k = 0; k < 255; k++)
                    {
                        if (Main.player[k].active && !Main.player[k].dead)
                        {
                            Vector2 center = Main.player[k].Center;
                            float num5 = Vector2.Distance(center, projectile.Center);
                            if ((num5 < num4 || num3 == -1) && Collision.CanHit(projectile.Center, 1, 1, center, 1, 1))
                            {
                                num4 = num5;
                                num3 = k;
                            }
                        }
                    }
                    if (num4 < 20f)
                    {
                        projectile.Kill();
                        return;
                    }
                    if (num3 != -1)
                    {
                        projectile.ai[1] = 21f;
                        projectile.ai[0] = (float)num3;
                        projectile.netUpdate = true;
                    }
                }
                else
                {
                    if (projectile.ai[1] > 20f && projectile.ai[1] < 200f)
                    {
                        projectile.ai[1] += 1f;
                        int num6 = (int)projectile.ai[0];
                        if (!Main.player[num6].active || Main.player[num6].dead)
                        {
                            projectile.ai[1] = 1f;
                            projectile.ai[0] = 0f;
                            projectile.netUpdate = true;
                        }
                        else
                        {
                            float num7 = projectile.velocity.ToRotation();
                            Vector2 vector2 = Main.player[num6].Center - projectile.Center;
                            if (vector2.Length() < 20f)
                            {
                                projectile.Kill();
                                return;
                            }
                            float targetAngle = vector2.ToRotation();
                            if (vector2 == Vector2.Zero)
                            {
                                targetAngle = num7;
                            }
                            float num8 = num7.AngleLerp(targetAngle, 0.008f);
                            projectile.velocity = new Vector2(projectile.velocity.Length(), 0f).RotatedBy((double)num8, default(Vector2));
                        }
                    }
                }
            }*/
            /*bool home = false;
            projectile.localAI[0]++;
            if (projectile.localAI[0] >= 20f)
            {
                AdjustMagnitude(ref projectile.velocity);
                if (projectile.localAI[0] >= 240f)
                {
                    home = true;
                }
            }
            Vector2 move = Vector2.Zero;
            float distance = 1800f;
            float distance2 = 100000f;
            bool target = false;
            for (int i = 0; i < 200; i++)
            {
                if (Main.npc[i].active && !Main.npc[i].behindTiles && !Main.npc[i].dontTakeDamage && !Main.npc[i].friendly && Main.npc[i].type != NPCID.TargetDummy)
                {
                    Vector2 newMove = Main.npc[i].Center - projectile.Center;
                    float distanceTo = (float)Math.Sqrt(newMove.X * newMove.X + newMove.Y * newMove.Y);
                    Player player = Main.player[projectile.owner];
                    Vector2 newMove2 = player.Center - projectile.Center;
                    float distanceTo2 = (float)Math.Sqrt(newMove2.X * newMove2.X + newMove2.Y * newMove2.Y);
                    if (distanceTo < distance)
                    {
                        move = newMove;
                        distance = distanceTo;
                        target = true;
                        if (home == true && target == false)
                        {
                            move = newMove2;
                            distance = distance2;
                            distance2 = distanceTo2;
                            projectile.penetrate = -1;
                        }
                    }
                }
            }
            if (target)
            {
                AdjustMagnitude(ref move);
                projectile.velocity = (12 * projectile.velocity + move) / 5f;//big move
                AdjustMagnitude(ref projectile.velocity);
            }*/
        }
        private void AdjustMagnitude(ref Vector2 vector)
        {
            float num = Main.rand.Next(16, 22);
            float magnitude = (float)Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
            if (magnitude > 6f)
            {
                vector *= num / magnitude;
            }
        }
        public override void Kill(int timeLeft)
        {
            Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 14, 1f, 0f);
            projectile.position.X = projectile.position.X + (projectile.width / 2);
            projectile.position.Y = projectile.position.Y + (projectile.height / 2);
            projectile.width = 60;
            projectile.height = 60;
            projectile.position.X = projectile.position.X - (projectile.width / 2);
            projectile.position.Y = projectile.position.Y - (projectile.height / 2);
            projectile.Damage();
            for (int i = 0; i < 30; i++)
            {
                int num = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 21, 0f, 0f, 100, default(Color), 1.2f);
                Main.dust[num].velocity *= 3f;
                if (Main.rand.Next(2) == 0)
                {
                    Main.dust[num].scale = 0.5f;
                    Main.dust[num].fadeIn = 1f + Main.rand.Next(10) * 0.1f;
                }
            }
            for (int i = 0; i < 60; i++)
            {
                int num = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 173, 0f, 0f, 100, default(Color), 1.7f);
                Main.dust[num].noGravity = true;
                Main.dust[num].velocity *= 5f;
                num = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 173, 0f, 0f, 100, default(Color), 1f);
                Main.dust[num].velocity *= 2f;
            }
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }
    }
}