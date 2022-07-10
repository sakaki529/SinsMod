using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Projectiles.Summon
{
    public class AbyssalSphereSentry : ModProjectile
    {
        public override string Texture => "SinsMod/Projectiles/Hostile/AbyssalSphere";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Abyssal Sphere");
            Main.projFrames[projectile.type] = 4;
        }
        public override void SetDefaults()
        {
            projectile.width = 80;
            projectile.height = 80;
            projectile.sentry = true;
            projectile.timeLeft = Projectile.SentryLifeTime;
            projectile.penetrate = -1;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.alpha = 255;
            projectile.ignoreWater = true;
            projectile.tileCollide = false;
        }
        public override bool CanDamage()
        {
            return false;
        }
        public override bool? CanHitNPC(NPC target)
        {
            return false;
        }
        public override bool CanHitPlayer(Player target)
        {
            return false;
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D texture = Main.projectileTexture[projectile.type];
            lightColor = Lighting.GetColor((int)(projectile.position.X + (projectile.width * 0.5)) / 16, (int)((projectile.position.Y + (projectile.height * 0.5)) / 16.0));
            int num = Main.projectileTexture[projectile.type].Height / Main.projFrames[projectile.type];
            int num2 = num * projectile.frame;
            Main.spriteBatch.Draw(texture, projectile.Center - Main.screenPosition + new Vector2(0f, projectile.gfxOffY), new Rectangle?(new Rectangle(0, num2, texture.Width, num)), projectile.GetAlpha(lightColor), projectile.rotation, new Vector2(texture.Width / 2f, num / 2f), projectile.scale, 0, 0f);
            return false;
        }
        public override void AI()
        {
            int num;
            num = projectile.frameCounter + 1;
            projectile.frameCounter = num;
            if (num >= 4)
            {
                projectile.frameCounter = 0;
                num = projectile.frame + 1;
                projectile.frame = num;
                if (num >= Main.projFrames[projectile.type])
                {
                    projectile.frame = 0;
                }
            }
            Lighting.AddLight(projectile.Center, 0.4f, 0.85f, 0.9f);
            if (projectile.localAI[1] == 0f)
            {
                Main.PlaySound(SoundID.Item121, projectile.position);
                projectile.localAI[1] = 1f;
            }
            if (projectile.timeLeft > 60)
            {
                projectile.alpha -= 5;
                if (projectile.alpha < 0)
                {
                    projectile.alpha = 0;
                }
            }
            else
            {
                projectile.alpha += 5;
                if (projectile.alpha > 255)
                {
                    projectile.alpha = 255;
                    projectile.Kill();
                    return;
                }
            }
            float[] vs = projectile.ai;
            int num2 = 0;
            float num3 = vs[num2];
            vs[num2] = num3 + 1f;
            if (projectile.ai[0] % 30f == 0f && projectile.timeLeft > 60)
            {
                int[] array = new int[5];
                Vector2[] array2 = new Vector2[5];
                int num4 = 0;
                float num5 = 2000f;
                for (int num6 = 0; num6 < 200; num6 = num + 1)
                {
                    NPC nPC = Main.npc[num6];
                    if (projectile.OwnerMinionAttackTargetNPC != null)
                    {
                        nPC = projectile.OwnerMinionAttackTargetNPC;
                    }
                    if (nPC.active && nPC.CanBeChasedBy(projectile, false))
                    {
                        Vector2 center = nPC.Center;
                        float num7 = Vector2.Distance(center, projectile.Center);
                        if (num7 < num5 && Collision.CanHit(projectile.Center, 1, 1, center, 1, 1))
                        {
                            array[num4] = num6;
                            array2[num4] = center;
                            num = num4 + 1;
                            num4 = num;
                            if (num >= array2.Length)
                            {
                                break;
                            }
                        }
                    }
                    num = num6;
                }
                for (int num8 = 0; num8 < num4; num8 = num + 1)
                {
                    Vector2 vector = array2[num8] - projectile.Center;
                    float ai = Main.rand.Next(100);
                    Vector2 vector95 = Vector2.Normalize(vector.RotatedByRandom(0.78539818525314331)) * 7f;
                    Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, vector95.X, vector95.Y, mod.ProjectileType("LightningArc"), projectile.damage, .490f, projectile.owner, vector.ToRotation(), ai);
                    num = num8;
                    break;
                }
            }
            if (projectile.alpha < 150 && projectile.timeLeft > 60)
            {
                for (int num9 = 0; num9 < 1; num9 = num + 1)
                {
                    float num10 = (float)Main.rand.NextDouble() * 1f - 0.5f;
                    if (num10 < -0.5f)
                    {
                        num10 = -0.5f;
                    }
                    if (num10 > 0.5f)
                    {
                        num10 = 0.5f;
                    }
                    Vector2 value = new Vector2((-projectile.width) * 0.2f * projectile.scale, 0f).RotatedBy(num10 * 6.28318548f, default(Vector2)).RotatedBy(projectile.velocity.ToRotation(), default(Vector2));
                    int num11 = Dust.NewDust(projectile.Center - Vector2.One * 5f, 10, 10, 226 - 5, -projectile.velocity.X / 3f, -projectile.velocity.Y / 3f, 150, Color.Transparent, 0.7f);
                    Main.dust[num11].position = projectile.Center + value;
                    Main.dust[num11].velocity = Vector2.Normalize(Main.dust[num11].position - projectile.Center) * 2f;
                    Main.dust[num11].noGravity = true;
                    Main.dust[num11].color = Color.MidnightBlue;
                    num = num9;
                }
                for (int num12 = 0; num12 < 1; num12 = num + 1)
                {
                    float num13 = (float)Main.rand.NextDouble() * 1f - 0.5f;
                    if (num13 < -0.5f)
                    {
                        num13 = -0.5f;
                    }
                    if (num13 > 0.5f)
                    {
                        num13 = 0.5f;
                    }
                    Vector2 value2 = new Vector2((-projectile.width) * 0.6f * projectile.scale, 0f).RotatedBy(num13 * 6.28318548f, default(Vector2)).RotatedBy(projectile.velocity.ToRotation(), default(Vector2));
                    int num14 = Dust.NewDust(projectile.Center - Vector2.One * 5f, 10, 10, 226 - 5, -projectile.velocity.X / 3f, -projectile.velocity.Y / 3f, 150, Color.Transparent, 0.7f);
                    Main.dust[num14].velocity = Vector2.Zero;
                    Main.dust[num14].position = projectile.Center + value2;
                    Main.dust[num14].noGravity = true;
                    Main.dust[num14].color = Color.MidnightBlue;
                    num = num12;
                }
                return;
            }
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(255, 255, 255, 0) * (1f - projectile.alpha / 255f);
        }
    }
}