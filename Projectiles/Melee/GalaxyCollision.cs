using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Projectiles.Melee
{
    public class GalaxyCollision : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Javelin");
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 20;
            ProjectileID.Sets.TrailingMode[projectile.type] = 2;
        }
        public override void SetDefaults()
        {
            projectile.width = 16;
            projectile.height = 16;
            projectile.melee = true;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.tileCollide = true;
            projectile.ignoreWater = true;
            projectile.penetrate = 5;
            projectile.alpha = 255;
            projectile.MaxUpdates = 2;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = -1;
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D texture = Main.projectileTexture[projectile.type];
            SpriteEffects spriteEffects = projectile.spriteDirection == -1 ? spriteEffects = SpriteEffects.FlipHorizontally : SpriteEffects.None;
            Color color = Lighting.GetColor((int)(projectile.position.X + projectile.width * 0.5) / 16, (int)((projectile.position.Y + projectile.height * 0.5) / 16.0));
            int num = Main.projectileTexture[projectile.type].Height / Main.projFrames[projectile.type];
            int y = num * projectile.frame;
            Rectangle rectangle = new Rectangle(0, y, texture.Width, num);
            Vector2 origin = rectangle.Size() / 2f;
            origin.Y = 10f;
            int num2 = 20;
            int num3 = 3;
            int num4 = 1;
            float value = 0.5f;
            float num5 = 0f;
            int num6 = num4;
            while ((num3 > 0 && num6 < num2) || (num3 < 0 && num6 > num2))
            {
                Color color2 = color;
                color2 = projectile.GetAlpha(color2);
                goto label2;
            label:
                num6 += num3;
                continue;
            label2:
                float num7 = num2 - num6;
                if (num3 < 0)
                {
                    num7 = num4 - num6;
                }
                color2 *= num7 / (ProjectileID.Sets.TrailCacheLength[projectile.type] * 1.5f);
                Vector2 value2 = projectile.oldPos[num6];
                float num8 = projectile.rotation;
                if (ProjectileID.Sets.TrailingMode[projectile.type] == 2)
                {
                    num8 = projectile.oldRot[num6];
                    spriteEffects = (projectile.oldSpriteDirection[num6] == -1) ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
                }
                Main.spriteBatch.Draw(texture, value2 + projectile.Size / 2f - Main.screenPosition + new Vector2(0f, projectile.gfxOffY), rectangle, color2, num8 + projectile.rotation * num5 * (num6 - 1) * (-spriteEffects.HasFlag(SpriteEffects.FlipHorizontally).ToDirectionInt()), origin, MathHelper.Lerp(projectile.scale, value, num6 / 15f), spriteEffects, 0f);
                goto label;
            }
            Main.spriteBatch.Draw(texture, projectile.Size / 2f - Main.screenPosition + new Vector2(0f, projectile.gfxOffY), rectangle, projectile.GetAlpha(lightColor), projectile.rotation * num5 * (num6 - 1) * (-spriteEffects.HasFlag(SpriteEffects.FlipHorizontally).ToDirectionInt()), origin, MathHelper.Lerp(projectile.scale, value, num6 / 15f), spriteEffects, 0f);
            return false;
        }
        public override void AI()
        {
            if (projectile.alpha > 0)
            {
                projectile.alpha -= 45;
            }
            if (projectile.alpha < 0)
            {
                projectile.alpha = 0;
            }
            if (projectile.ai[0] == 0f)
            {
                projectile.ai[1] += 1f;
                if (projectile.ai[1] >= 45)
                {
                    projectile.ai[1] = 45f;
                    float num5 = 0.995f;
                    float num6 = 0.15f;
                    projectile.ai[1] = 45f;
                    projectile.velocity.X = projectile.velocity.X * num5;
                    projectile.velocity.Y = projectile.velocity.Y + num6;
                }
                projectile.rotation = projectile.velocity.ToRotation() + MathHelper.ToRadians(90f);
            }
            if (projectile.ai[0] == 1f)
            {
                projectile.ignoreWater = true;
                projectile.tileCollide = false;
                int num7 = 5 * projectile.MaxUpdates;
                bool flag = false;
                projectile.localAI[0] += 1f;
                int num8 = (int)projectile.ai[1];
                if (projectile.localAI[0] >= 60 * num7)
                {
                    flag = true;
                }
                else
                {
                    if (num8 < 0 || num8 >= 200)
                    {
                        flag = true;
                    }
                    else
                    {
                        if (Main.npc[num8].active && !Main.npc[num8].dontTakeDamage)
                        {
                            projectile.Center = Main.npc[num8].Center - projectile.velocity * 2f;
                            projectile.gfxOffY = Main.npc[num8].gfxOffY;
                            if (projectile.localAI[0] % 30f == 0f)
                            {
                                Main.npc[num8].HitEffect(0, 1.0);
                            }
                        }
                        else
                        {
                            flag = true;
                        }
                    }
                }
                if (flag)
                {
                    projectile.Kill();
                }
            }
            Lighting.AddLight(projectile.Center, 0.3f, 0.3f, 0.7f);
        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            base.projectile.ai[0] = 1f;
            base.projectile.ai[1] = target.whoAmI;
            if (target.life < 0)
            {
                projectile.ai[1] = -1f;
            }
            base.projectile.velocity = (target.Center - base.projectile.Center) * 0.75f;
            base.projectile.netUpdate = true;
            int num = 1;
            Point[] array = new Point[num];
            int num2 = 0;
            for (int i = 0; i < 1000; i++)
            {
                Projectile projectile = Main.projectile[i];
                if (i != base.projectile.whoAmI && projectile.active && projectile.owner == Main.myPlayer && projectile.type == base.projectile.type && projectile.ai[0] == 1f && projectile.ai[1] == target.whoAmI)
                {
                    array[num2++] = new Point(i, projectile.timeLeft);
                    if (num2 >= array.Length)
                    {
                        Projectile.NewProjectile(target.Center.X, target.Center.Y, 0f, 0f, mod.ProjectileType("TidalExplosion"), projectile.damage, 0f, base.projectile.owner, projectile.melee ? 0f : 1f, -1f);
                        base.projectile.Kill();
                        break;
                    }
                }
            }
            if (num2 >= array.Length)
            {
                int num3 = 0;
                for (int j = 1; j < array.Length; j++)
                {
                    if (array[j].Y < array[num3].Y)
                    {
                        num3 = j;
                    }
                }
                Main.projectile[array[num3].X].Kill();
            }
            if (num2 >= array.Length)
            {
                for (int k = 0; k < 1000; k++)
                {
                    Projectile projectile2 = Main.projectile[k];
                    if (projectile2.active && projectile2.penetrate < 3 && projectile2.type == mod.ProjectileType("GalaxyCollision"))
                    {
                        projectile2.Kill();
                    }
                }
            }
            base.projectile.extraUpdates = 0;
            base.projectile.friendly = false;
        }
        public override void Kill(int timeLeft)
        {
            Rectangle hitbox = projectile.Hitbox;
            for (int num = 0; num < 6; num += 3)
            {
                hitbox.X = (int)projectile.oldPos[num].X;
                hitbox.Y = (int)projectile.oldPos[num].Y;
                int num2;
                for (int num3 = 0; num3 < 5; num3 = num2 + 1)
                {
                    int num4 = Utils.SelectRandom<int>(Main.rand, new int[]
                    {
                        172,
                        172,
                        172
                    });
                    int num5 = Dust.NewDust(hitbox.TopLeft(), projectile.width, projectile.height, num4, 2.5f * projectile.direction, -2.5f, 0, default(Color), 1f);
                    Main.dust[num5].alpha = 200;
                    Dust dust = Main.dust[num5];
                    dust.velocity *= 2.4f;
                    dust = Main.dust[num5];
                    dust.scale += Main.rand.NextFloat();
                    num2 = num3;
                }
            }
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(255 - projectile.alpha, 255 - projectile.alpha, 255 - projectile.alpha, 64 - projectile.alpha / 4);
        }
    }
}