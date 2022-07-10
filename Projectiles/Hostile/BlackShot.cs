using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace SinsMod.Projectiles.Hostile
{
    public class BlackShot : ModProjectile
    {
        public override string Texture => "SinsMod/Extra/Placeholder/BlankTex";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Madness");
        }
        public override void SetDefaults()
        {
            projectile.width = 14;
            projectile.height = 14;
            projectile.friendly = false;
            projectile.hostile = true;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
            projectile.penetrate = -1;
            projectile.timeLeft = 600;
        }
        public override void AI()
        {
            projectile.alpha -= 40;
            if (projectile.alpha < 0)
            {
                projectile.alpha = 0;
            }
            if (projectile.ai[0] == 0f)
            {
                projectile.localAI[0]++;
                if (projectile.localAI[0] >= 45f)
                {
                    projectile.localAI[0] = 0f;
                    projectile.ai[0] = 1f;
                    projectile.ai[1] = -projectile.ai[1];
                    projectile.netUpdate = true;
                }
                projectile.velocity.X = projectile.velocity.RotatedBy(projectile.ai[1], default(Vector2)).X;
                projectile.velocity.X = MathHelper.Clamp(projectile.velocity.X, -6f, 6f);
                projectile.velocity.Y = projectile.velocity.Y - 0.08f;
                if (projectile.velocity.Y > 0f)
                {
                    projectile.velocity.Y = projectile.velocity.Y - 0.2f;
                }
                if (projectile.velocity.Y < -7f)
                {
                    projectile.velocity.Y = -7f;
                }
            }
            else
            {
                if (projectile.ai[0] == 1f)
                {
                    projectile.localAI[1]++;
                    if (projectile.localAI[0] >= 90f)
                    {
                        projectile.localAI[0] = 0f;
                        projectile.ai[0] = 2f;
                        projectile.ai[1] = Player.FindClosest(projectile.position, projectile.width, projectile.height);
                        projectile.netUpdate = true;
                    }
                    projectile.velocity.X = projectile.velocity.RotatedBy(projectile.ai[1], default(Vector2)).X;
                    projectile.velocity.X = MathHelper.Clamp(projectile.velocity.X, -6f, 6f);
                    projectile.velocity.Y = projectile.velocity.Y - 0.08f;
                    if (projectile.velocity.Y > 0f)
                    {
                        projectile.velocity.Y = projectile.velocity.Y - 0.2f;
                    }
                    if (projectile.velocity.Y < -7f)
                    {
                        projectile.velocity.Y = -7f;
                    }
                }
                else
                {
                    if (projectile.ai[0] == 2f)
                    {
                        Vector2 vector = Main.player[(int)projectile.ai[1]].Center - projectile.Center;
                        if (vector.Length() < 30f)
                        {
                            projectile.Kill();
                            return;
                        }
                        vector.Normalize();
                        vector *= 14f;
                        vector = Vector2.Lerp(projectile.velocity, vector, 0.6f);
                        if (vector.Y < 6f)
                        {
                            vector.Y = 6f;
                        }
                        float num3 = 0.4f;
                        if (projectile.velocity.X < vector.X)
                        {
                            projectile.velocity.X = projectile.velocity.X + num3;
                            if (projectile.velocity.X < 0f && vector.X > 0f)
                            {
                                projectile.velocity.X = projectile.velocity.X + num3;
                            }
                        }
                        else
                        {
                            if (projectile.velocity.X > vector.X)
                            {
                                projectile.velocity.X = projectile.velocity.X - num3;
                                if (projectile.velocity.X > 0f && vector.X < 0f)
                                {
                                    projectile.velocity.X = projectile.velocity.X - num3;
                                }
                            }
                        }
                        if (projectile.velocity.Y < vector.Y)
                        {
                            projectile.velocity.Y = projectile.velocity.Y + num3;
                            if (projectile.velocity.Y < 0f && vector.Y > 0f)
                            {
                                projectile.velocity.Y = projectile.velocity.Y + num3;
                            }
                        }
                        else
                        {
                            if (projectile.velocity.Y > vector.Y)
                            {
                                projectile.velocity.Y = projectile.velocity.Y - num3;
                                if (projectile.velocity.Y > 0f && vector.Y < 0f)
                                {
                                    projectile.velocity.Y = projectile.velocity.Y - num3;
                                }
                            }
                        }
                    }
                }
            }
            if (projectile.alpha < 40)
            {
                int num791 = Dust.NewDust(projectile.Center - Vector2.One * 5f, 10, 10, 229, -projectile.velocity.X / 3f, -projectile.velocity.Y / 3f, 150, Color.Transparent, 1.2f);
                Main.dust[num791].noGravity = true;
            }
            projectile.rotation = projectile.velocity.ToRotation() + 1.57079637f;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(mod.BuffType("Sin"), 600);
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(mod.BuffType("Sin"), 600);
        }
        public override void Kill(int timeLeft)
        {
            Main.PlaySound(29, (int)projectile.position.X, (int)projectile.position.Y, 103, 1f, 0f);
            projectile.position = projectile.Center;
            projectile.width = projectile.height = 144;
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
                int num4 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 229, 0f, 0f, 0, default(Color), 2.5f);
                Main.dust[num4].noGravity = true;
                Dust dust = Main.dust[num4];
                dust.velocity *= 3f;
                num4 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 229, 0f, 0f, 100, default(Color), 1.5f);
                dust = Main.dust[num4];
                dust.velocity *= 2f;
                Main.dust[num4].noGravity = true;
                num = num3;
            }
            for (int num5 = 0; num5 < 1; num5 = num + 1)
            {
                int num6 = Gore.NewGore(projectile.position + new Vector2(projectile.width * Main.rand.Next(100) / 100f, projectile.height * Main.rand.Next(100) / 100f) - Vector2.One * 10f, default(Vector2), Main.rand.Next(61, 64), 1f);
                Gore gore = Main.gore[num6];
                gore.velocity *= 0.3f;
                Main.gore[num6].velocity.X = Main.gore[num6].velocity.X + Main.rand.Next(-10, 11) * 0.05f;
                Main.gore[num6].velocity.Y = Main.gore[num6].velocity.Y + Main.rand.Next(-10, 11) * 0.05f;
                num = num5;
            }
            projectile.Damage();
        }
    }
}