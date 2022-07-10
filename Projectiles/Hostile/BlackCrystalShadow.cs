using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Projectiles.Hostile
{
    public class BlackCrystalShadow : ModProjectile
    {
        private int target = -1;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Black Shadow");
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 10;
            ProjectileID.Sets.TrailingMode[projectile.type] = 2;
        }
        public override void SetDefaults()
        {
            projectile.width = 44;
            projectile.height = 44;
            projectile.penetrate = -1;
            projectile.hostile = true;
            projectile.alpha = 255;
            projectile.ignoreWater = true;
            projectile.tileCollide = false;
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D texture = Main.projectileTexture[projectile.type];
            Texture2D glowTexture = mod.GetTexture("Glow/Projectile/BlackCrystalShadow_Glow");
            SpriteEffects spriteEffects = (projectile.direction <= 0) ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            int num = Main.projectileTexture[projectile.type].Height / Main.projFrames[projectile.type];
            int num2 = num * projectile.frame;
            Rectangle rectangle = new Rectangle(0, num2, texture.Width, num);
            Vector2 origin = rectangle.Size() / 2f;
            Color color = lightColor;
            color = projectile.GetAlpha(color);
            for (int i = 0; i < ProjectileID.Sets.TrailCacheLength[projectile.type]; i++)
            {
                Color color2 = color;
                color2 *= (float)(ProjectileID.Sets.TrailCacheLength[projectile.type] - i) / ProjectileID.Sets.TrailCacheLength[projectile.type];
                Color color3 = projectile.GetAlpha(SinsColor.BW);
                color3 *= (float)(ProjectileID.Sets.TrailCacheLength[projectile.type] - i) / ProjectileID.Sets.TrailCacheLength[projectile.type];
                Vector2 value = projectile.oldPos[i];
                float num3 = projectile.oldRot[i];
                Main.spriteBatch.Draw(texture, value + projectile.Size / 2f - Main.screenPosition + new Vector2(0, projectile.gfxOffY), rectangle, color2, num3, origin, projectile.scale, spriteEffects, 0f);
                Main.spriteBatch.Draw(glowTexture, value + projectile.Size / 2f - Main.screenPosition + new Vector2(0, projectile.gfxOffY), rectangle, color3, num3, origin, projectile.scale, spriteEffects, 0f);
            }
            Main.spriteBatch.Draw(texture, projectile.Center - Main.screenPosition + new Vector2(0f, projectile.gfxOffY), rectangle, projectile.GetAlpha(lightColor), projectile.rotation, origin, projectile.scale, spriteEffects, 0f);
            Main.spriteBatch.Draw(glowTexture, projectile.Center - Main.screenPosition + new Vector2(0f, projectile.gfxOffY), rectangle, projectile.GetAlpha(SinsColor.BW), projectile.rotation, origin, projectile.scale, spriteEffects, 0f);
            return false;
        }
        public override void AI()
        {
            if (projectile.localAI[0] == 0f)
            {
                projectile.localAI[0] = 1f;
                /*for (int i = 0; i < 50; ++i)
                {
                    Dust dust = Main.dust[Dust.NewDust(projectile.position, projectile.width, projectile.height, 242, 0.0f, 0.0f, 0, new Color(), 1f)];
                    dust.velocity *= 10f;
                    dust.fadeIn = 1f;
                    dust.scale = 1 + Main.rand.NextFloat() + Main.rand.Next(4) * 0.3f;
                    if (Main.rand.Next(3) != 0)
                    {
                        dust.noGravity = true;
                        dust.velocity *= 3f;
                        dust.scale *= 2f;
                    }
                }*/
            }
            if (projectile.alpha > 0)
            {
                projectile.alpha -= 2;
                if (projectile.alpha <= 0)
                {
                    projectile.alpha = 0;
                    if (target != -1)
                    {
                        //Main.PlaySound(SoundID.Item89, projectile.Center);
                        projectile.velocity = Main.player[target].Center - projectile.Center;
                        float distance = projectile.velocity.Length();
                        projectile.velocity.Normalize();
                        //const float speed = 46f;
                        const float speed = 64f;
                        projectile.velocity *= speed;
                        projectile.timeLeft = (int)(distance / speed) * 2;
                        projectile.netUpdate = true;
                        return;
                    }
                    else
                    {
                        projectile.Kill();
                    }
                }
                //projectile.velocity *= 0.985f;
                //projectile.velocity.Y += 10f / 120f;
                projectile.rotation = projectile.velocity.ToRotation() + 1.57079637f;
                //projectile.rotation += (projectile.velocity.Length() + 1.57079637f) / 20f;
                if (target >= 0 && Main.player[target].active && !Main.player[target].dead)
                {
                    if (projectile.alpha < 100)
                    {
                        //projectile.rotation = projectile.rotation.AngleLerp((Main.player[target].Center - projectile.Center).ToRotation() + 1.57079637f, (255 - projectile.alpha) / 255f * 0.08f);
                    }
                }
                else
                {
                    int newTarget = -1;
                    float maxDist = 9000f;
                    for (int i = 0; i < 255; i++)
                    {
                        if (Main.player[i].active && !Main.player[i].dead)
                        {
                            float distance = projectile.Distance(Main.player[i].Center);
                            if (distance < maxDist)
                            {
                                newTarget = i;
                                maxDist = distance;
                            }
                        }
                    }
                    if (newTarget != -1)
                    {
                        target = newTarget;
                        projectile.netUpdate = true;
                    }
                }
            }
            else
            {
                projectile.rotation = projectile.velocity.ToRotation() + 1.57079637f;
            }
        }
        public override void Kill(int timeLeft)
        {
            int num = Main.rand.Next(3, 5);
            for (int i = 0; i < num; i++)
            {
                float num2 = Utils.NextFloat(Main.rand, 14f, 18f);
                Vector2 vector = new Vector2(Main.rand.Next(-100, 100), Main.rand.Next(-100, 100));
                vector.Normalize();
                vector.X *= num2;
                vector.Y *= num2;
                Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, vector.X, vector.Y, mod.ProjectileType("BlackCrystalShadowSmall"), (int)(projectile.damage * 0.5), 0f, Main.myPlayer, 0f, 0f);
            }
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(255, 255, 255, 255) * (1f - projectile.alpha / 255f);
        }
    }
}