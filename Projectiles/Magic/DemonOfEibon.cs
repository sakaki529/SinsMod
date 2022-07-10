using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Projectiles.Magic
{
    public class DemonOfEibon : ModProjectile
    {
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
            projectile.penetrate = -1;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 4;
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