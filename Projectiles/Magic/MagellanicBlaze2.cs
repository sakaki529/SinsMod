using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Projectiles.Magic
{
    public class MagellanicBlaze2 : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Magellanic Blaze");
            Main.projFrames[projectile.type] = 4;
        }
        public override void SetDefaults()
        {
            projectile.width = 40;
            projectile.height = 40;
            projectile.aiStyle = 0;
            projectile.magic = true;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.penetrate = 1;
            projectile.tileCollide = true;
            projectile.ignoreWater = true;
            projectile.alpha = 255;
            projectile.extraUpdates = 3;
            projectile.GetGlobalProjectile<SinsProjectile>().drawCenter = true;
        }
        public override void AI()
        {
            float num = 5f;
            float num2 = 500f;
            float scaleFactor = 6f;
            Vector2 value = new Vector2(10f, 20f);
            float num3 = 1.0f;
            Vector3 rgb = new Vector3(0.9f, 0.4f, 0.4f);
            int num4 = 3 * projectile.MaxUpdates;
            int num5 = Utils.SelectRandom<int>(Main.rand, new int[]
            {
                    59,
                    60,
                    66
            });
            int num6 = 66;
            if (projectile.ai[1] == 0f)
            {
                projectile.ai[1] = 1f;
                projectile.localAI[0] = -Main.rand.Next(48);
                Main.PlaySound(SoundID.Item34, projectile.position);
            }
            else
            {
                if (projectile.ai[1] == 1f && projectile.owner == Main.myPlayer)
                {
                    int num7 = -1;
                    float num8 = num2;
                    for (int num9 = 0; num9 < 200; num9++)
                    {
                        if (Main.npc[num9].active && Main.npc[num9].CanBeChasedBy(projectile, false))
                        {
                            Vector2 center = Main.npc[num9].Center;
                            float num10 = Vector2.Distance(center, projectile.Center);
                            if (num10 < num8 && num7 == -1 && Collision.CanHitLine(projectile.Center, 1, 1, center, 1, 1))
                            {
                                num8 = num10;
                                num7 = num9;
                            }
                        }
                    }
                    if (num8 < 20f)
                    {
                        projectile.Kill();
                        return;
                    }
                    if (num7 != -1)
                    {
                        projectile.ai[1] = num + 1f;
                        projectile.ai[0] = num7;
                        projectile.netUpdate = true;
                    }
                }
                else
                {
                    if (projectile.ai[1] > num)
                    {
                        projectile.ai[1] += 1f;
                        int num11 = (int)projectile.ai[0];
                        if (!Main.npc[num11].active || !Main.npc[num11].CanBeChasedBy(projectile, false))
                        {
                            projectile.ai[1] = 1f;
                            projectile.ai[0] = 0f;
                            projectile.netUpdate = true;
                        }
                        else
                        {
                            projectile.velocity.ToRotation();
                            Vector2 vector = Main.npc[num11].Center - projectile.Center;
                            if (vector.Length() < 20f)
                            {
                                projectile.Kill();
                                return;
                            }
                            if (vector != Vector2.Zero)
                            {
                                vector.Normalize();
                                vector *= scaleFactor;
                            }
                            float num12 = 30f;
                            projectile.velocity = (projectile.velocity * (num12 - 1f) + vector) / num12;
                        }
                    }
                }
            }
            if (projectile.ai[1] >= 1f && projectile.ai[1] < num)
            {
                projectile.ai[1] += 1f;
                if (projectile.ai[1] == num)
                {
                    projectile.ai[1] = 1f;
                }
            }
            projectile.alpha -= 40;
            if (projectile.alpha < 0)
            {
                projectile.alpha = 0;
            }
            projectile.spriteDirection = projectile.direction;
            projectile.frameCounter++;
            if (projectile.frameCounter >= num4)
            {
                projectile.frame++;
                projectile.frameCounter = 0;
                if (projectile.frame >= 4)
                {
                    projectile.frame = 0;
                }
            }
            Lighting.AddLight(projectile.Center, rgb);
            projectile.rotation = projectile.velocity.ToRotation();
            projectile.localAI[0] += 1f;
            if (projectile.localAI[0] == 48f)
            {
                projectile.localAI[0] = 0f;
            }
            else
            {
                if (projectile.alpha == 0)
                {
                    for (int num13 = 0; num13 < 2; num13++)
                    {
                        Vector2 value2 = Vector2.UnitX * -30f;
                        value2 = -Vector2.UnitY.RotatedBy(projectile.localAI[0] * 0.1308997f + num13 * 3.14159274f, default(Vector2)) * value - projectile.rotation.ToRotationVector2() * 10f;
                        int num14 = Dust.NewDust(projectile.Center, 0, 0, num6, 0f, 0f, 160, new Color(200, 0, 0), 1f);
                        Main.dust[num14].scale = num3;
                        Main.dust[num14].noGravity = true;
                        Main.dust[num14].position = projectile.Center + value2 + projectile.velocity * 2f;
                        Main.dust[num14].velocity = Vector2.Normalize(projectile.Center + projectile.velocity * 2f * 8f - Main.dust[num14].position) * 2f + projectile.velocity * 2f;
                    }
                }
            }
            if (Main.rand.Next(12) == 0)
            {
                for (int num15 = 0; num15 < 1; num15++)
                {
                    Vector2 value3 = -Vector2.UnitX.RotatedByRandom(0.19634954631328583).RotatedBy(projectile.velocity.ToRotation(), default(Vector2));
                    int num16 = Dust.NewDust(projectile.position, projectile.width, projectile.height, 31, 0f, 0f, 100, new Color(200, 0, 0), 1f);
                    Main.dust[num16].velocity *= 0.1f;
                    Main.dust[num16].position = projectile.Center + value3 * projectile.width / 2f + projectile.velocity * 2f;
                    Main.dust[num16].fadeIn = 0.9f;
                }
            }
            if (Main.rand.Next(64) == 0)
            {
                for (int num17 = 0; num17 < 1; num17++)
                {
                    Vector2 value4 = -Vector2.UnitX.RotatedByRandom(0.39269909262657166).RotatedBy(projectile.velocity.ToRotation(), default(Vector2));
                    int num18 = Dust.NewDust(projectile.position, projectile.width, projectile.height, 31, 0f, 0f, 155, new Color(200, 0, 0), 0.8f);
                    Main.dust[num18].velocity *= 0.3f;
                    Main.dust[num18].position = projectile.Center + value4 * projectile.width / 2f;
                    if (Main.rand.Next(2) == 0)
                    {
                        Main.dust[num18].fadeIn = 1.4f;
                    }
                }
            }
            if (Main.rand.Next(4) == 0)
            {
                for (int num19 = 0; num19 < 2; num19++)
                {
                    Vector2 value5 = -Vector2.UnitX.RotatedByRandom(0.78539818525314331).RotatedBy(projectile.velocity.ToRotation(), default(Vector2));
                    int num20 = Dust.NewDust(projectile.position, projectile.width, projectile.height, num5, 0f, 0f, 0, new Color(200, 0, 0), 1.2f);
                    Main.dust[num20].velocity *= 0.3f;
                    Main.dust[num20].noGravity = true;
                    Main.dust[num20].position = projectile.Center + value5 * projectile.width / 2f;
                    if (Main.rand.Next(2) == 0)
                    {
                        Main.dust[num20].fadeIn = 1.4f;
                    }
                }
            }
            if (Main.rand.Next(3) == 0)
            {
                Vector2 value6 = -Vector2.UnitX.RotatedByRandom(0.19634954631328583).RotatedBy(projectile.velocity.ToRotation(), default(Vector2));
                int num21 = Dust.NewDust(projectile.position, projectile.width, projectile.height, num6, 0f, 0f, 100, new Color(200, 0, 0), 1f);
                Main.dust[num21].velocity *= 0.3f;
                Main.dust[num21].position = projectile.Center + value6 * projectile.width / 2f;
                Main.dust[num21].fadeIn = 1.2f;
                Main.dust[num21].scale = 1.5f;
                Main.dust[num21].noGravity = true;
            }
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.immune[projectile.owner] = 5;
        }
        public override void Kill(int timeLeft)
        {
            int num = Utils.SelectRandom<int>(Main.rand, new int[]
            {
                59,
                60,
                66,
                182
            });
            int num2 = 90;
            int num3 = 90;
            int height = 50;
            float num4 = 3.7f;
            float num5 = 1.5f;
            float num6 = 2.2f;
            Vector2 value = projectile.rotation.ToRotationVector2();
            Vector2 value2 = value * projectile.velocity.Length() * projectile.MaxUpdates;
            value2 *= 0.5f;
            Main.PlaySound(SoundID.Item14, projectile.position);
            projectile.position = projectile.Center;
            projectile.width = (projectile.height = height);
            projectile.Center = projectile.position;
            projectile.maxPenetrate = -1;
            projectile.penetrate = -1;
            projectile.Damage();
            int num7;
            for (int num8 = 0; num8 < 40; num8 = num7 + 1)
            {
                num = Utils.SelectRandom<int>(Main.rand, new int[]
                {
                    60,
                    59,
                    90
                });
                int num9 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, num, 0f, 0f, 200, new Color(200, 0, 0), num4);
                Main.dust[num9].position = projectile.Center + Vector2.UnitY.RotatedByRandom(3.1415927410125732) * (float)Main.rand.NextDouble() * projectile.width / 2f;
                Main.dust[num9].noGravity = true;
                Dust dust = Main.dust[num9];
                dust.velocity *= 3f;
                dust = Main.dust[num9];
                dust.velocity += value2 * Main.rand.NextFloat();
                num9 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, num2, 0f, 0f, 100, new Color(200, 0, 0), num5);
                Main.dust[num9].position = projectile.Center + Vector2.UnitY.RotatedByRandom(3.1415927410125732) * (float)Main.rand.NextDouble() * projectile.width / 2f;
                dust = Main.dust[num9];
                dust.velocity *= 2f;
                Main.dust[num9].noGravity = true;
                Main.dust[num9].fadeIn = 1f;
                Main.dust[num9].color = new Color(200, 0, 0);
                dust = Main.dust[num9];
                dust.velocity += value2 * Main.rand.NextFloat();
                num7 = num8;
            }
            for (int num10 = 0; num10 < 20; num10 = num7 + 1)
            {
                int num11 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, num3, 0f, 0f, 0, new Color(200, 0, 0), num6);
                Main.dust[num11].position = projectile.Center + Vector2.UnitX.RotatedByRandom(3.1415927410125732).RotatedBy(projectile.velocity.ToRotation(), default(Vector2)) * projectile.width / 3f;
                Main.dust[num11].noGravity = true;
                Dust dust = Main.dust[num11];
                dust.velocity *= 0.5f;
                dust = Main.dust[num11];
                dust.velocity += value2 * (0.6f + 0.6f * Main.rand.NextFloat());
                num7 = num10;
            }
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(255, 255, 255) * (1f - projectile.alpha / 255f);
        }
    }
}