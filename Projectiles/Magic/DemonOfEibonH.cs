using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Projectiles.Magic
{
    public class DemonOfEibonH : ModProjectile
    {
        public override string Texture => "SinsMod/Projectiles/Magic/DemonOfEibon";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Demon of Eibon");
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 20;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
        }
        public override void SetDefaults()
        {
            projectile.width = 30;
            projectile.height = 30;
            projectile.aiStyle = -1;
            projectile.friendly = true;
            projectile.magic = true;
            projectile.tileCollide = true;
            projectile.ignoreWater = true;
            projectile.timeLeft = 600;
            projectile.penetrate = 4;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 0;
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D texture = Main.projectileTexture[projectile.type];
            Texture2D exTexture = mod.GetTexture("Extra/Projectile/DemonOfEibon_Extra");
            SpriteEffects spriteEffects = projectile.spriteDirection == -1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            int num = Main.projectileTexture[projectile.type].Height / Main.projFrames[projectile.type];
            int num2 = num * projectile.frame;
            Rectangle rectangle = new Rectangle(0, num2, texture.Width, num);
            Vector2 origin = rectangle.Size() / 2f;
            Color color = Color.White;
            color = projectile.GetAlpha(color);
            for (int i = 0; i < ProjectileID.Sets.TrailCacheLength[projectile.type]; i++)
            {
                if (i > 10)
                {
                    break;
                }
                if (i != 0)
                {
                    Color color2 = color;
                    color2 *= (float)(ProjectileID.Sets.TrailCacheLength[projectile.type] - i) / ProjectileID.Sets.TrailCacheLength[projectile.type];
                    Vector2 value = projectile.oldPos[i];
                    float num3 = projectile.oldRot[i];
                    float num4 = (20 - i) / 20f;
                    Main.spriteBatch.Draw(exTexture, value + projectile.Size / 2f - Main.screenPosition + new Vector2(0, projectile.gfxOffY), rectangle, color2, num3, origin, projectile.scale * num4, spriteEffects, 0f);
                }
            }
            Main.spriteBatch.Draw(texture, projectile.Center - Main.screenPosition + new Vector2(0f, projectile.gfxOffY), rectangle, projectile.GetAlpha(Color.White), projectile.rotation, origin, projectile.scale, spriteEffects, 0f);
            return false;
        }
        public override void AI()
        {
            int d = Dust.NewDust(projectile.position, projectile.width, projectile.height, 184, projectile.velocity.X, projectile.velocity.Y, 0, new Color(159, 0, 255));
            Main.dust[d].noGravity = true;
            Main.dust[d].velocity *= 0.3f;
            Main.dust[d].scale *= 1.3f;
            if (projectile.velocity.X < 0f)
            {
                projectile.rotation = (float)Math.Atan2(-projectile.velocity.Y, -projectile.velocity.X);
            }
            else
            {
                projectile.rotation = (float)Math.Atan2(projectile.velocity.Y, projectile.velocity.X);
            }
            projectile.spriteDirection = projectile.velocity.X < 0f ? -1 : 1;
            float num = (float)Math.Sqrt((projectile.velocity.X * projectile.velocity.X + projectile.velocity.Y * projectile.velocity.Y));
            float num2 = projectile.localAI[0];
            if (num2 == 0f)
            {
                projectile.localAI[0] = num;
                num2 = num;
            }
            float num3 = projectile.position.X;
            float num4 = projectile.position.Y;
            float num5 = 1000f;
            bool flag = false;
            int num6 = 0;
            projectile.ai[0] += 1f;
            if (projectile.ai[0] > 15f)
            {
                if (projectile.ai[1] == 0f)
                {
                    for (int num7 = 0; num7 < 200; num7++)
                    {
                        if (Main.npc[num7].CanBeChasedBy(projectile, false) && (projectile.ai[1] == 0f || projectile.ai[1] == (float)(num7 + 1)))
                        {
                            float num8 = Main.npc[num7].position.X + (Main.npc[num7].width / 2);
                            float num9 = Main.npc[num7].position.Y + (Main.npc[num7].height / 2);
                            float num10 = Math.Abs(projectile.position.X + (projectile.width / 2) - num8) + Math.Abs(projectile.position.Y + (projectile.height / 2) - num9);
                            if (num10 < num5 && Collision.CanHit(new Vector2(projectile.position.X + (projectile.width / 2), projectile.position.Y + (projectile.height / 2)), 1, 1, Main.npc[num7].position, Main.npc[num7].width, Main.npc[num7].height))
                            {
                                num5 = num10;
                                num3 = num8;
                                num4 = num9;
                                flag = true;
                                num6 = num7;
                            }
                        }
                    }
                    if (flag)
                    {
                        projectile.ai[1] = num6 + 1;
                    }
                    flag = false;
                }
                if (projectile.ai[1] != 0f)
                {
                    int num11 = (int)(projectile.ai[1] - 1f);
                    if (Main.npc[num11].active && Main.npc[num11].CanBeChasedBy(projectile, true))
                    {
                        float num12 = Main.npc[num11].position.X + (Main.npc[num11].width / 2);
                        float num13 = Main.npc[num11].position.Y + (Main.npc[num11].height / 2);
                        if (Math.Abs(projectile.position.X + (projectile.width / 2) - num12) + Math.Abs(projectile.position.Y + (projectile.height / 2) - num13) < 1000f)
                        {
                            flag = true;
                            num3 = Main.npc[num11].position.X + (Main.npc[num11].width / 2);
                            num4 = Main.npc[num11].position.Y + (Main.npc[num11].height / 2);
                        }
                    }
                }
                if (!projectile.friendly)
                {
                    flag = false;
                }
                if (flag)
                {
                    float num14 = num2;
                    Vector2 vector = new Vector2(projectile.position.X + projectile.width * 0.5f, projectile.position.Y + projectile.height * 0.5f);
                    float num15 = num3 - vector.X;
                    float num16 = num4 - vector.Y;
                    float num17 = (float)Math.Sqrt((num15 * num15 + num16 * num16));
                    num17 = num14 / num17;
                    num15 *= num17;
                    num16 *= num17;
                    int num18 = 8;
                    projectile.velocity.X = (projectile.velocity.X * (num18 - 1) + num15) / num18;
                    projectile.velocity.Y = (projectile.velocity.Y * (num18 - 1) + num16) / num18;
                }
            }
        }
        public override void Kill(int timeLeft)
        {
            Main.PlaySound(SoundID.NPCDeath12, (int)projectile.position.X, (int)projectile.position.Y);
            Main.PlaySound(SoundID.NPCDeath13, (int)projectile.position.X, (int)projectile.position.Y);
            for (int num = 0; num < 3; num++)
            {
                int num2 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 27, 0f, 0f, 100, default(Color), 1.2f);
                Main.dust[num2].noGravity = true;
                Main.dust[num2].velocity *= 0.8f;
                Main.dust[num2].velocity -= projectile.oldVelocity * 0.3f;
                Main.dust[num2].scale *= 1.5f;
            }
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(200, 200, 200) * (1f - projectile.alpha / 255f);
        }
    }
}