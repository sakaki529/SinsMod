using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Projectiles.Melee
{
    public class WhiteNightWave : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("White Night Wave");
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
        }
        public override void SetDefaults()
        {
            projectile.width = 60;
            projectile.height = 60;
            projectile.aiStyle = 0;
            projectile.friendly = true;
            projectile.melee = true;
            projectile.timeLeft = 300;
            //projectile.penetrate = -1;
            projectile.ignoreWater = true;
            projectile.tileCollide = false;
            projectile.extraUpdates = 1;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 20;
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width * 0.5f, projectile.height * 0.5f);
            for (int i = 0; i < projectile.oldPos.Length; i++)
            {
                Vector2 drawPos = projectile.oldPos[i] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
                Color color = projectile.GetAlpha(lightColor * 0.35f) * ((float)(projectile.oldPos.Length - i) / projectile.oldPos.Length);
                spriteBatch.Draw(Main.projectileTexture[projectile.type], drawPos, null, color, projectile.rotation, drawOrigin, projectile.scale, projectile.velocity.X < 0f ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0f);
            }
            return true;
        }
        public override void AI()
        {
            for (int i = 0; i < 1; i++)
            {
                Vector2 vector = Utils.RandomVector2(Main.rand, -0.5f, 0.5f) * new Vector2(48f, 48f);
                vector = vector.RotatedBy(projectile.velocity.ToRotation(), default(Vector2));
                Dust dust = Dust.NewDustDirect(projectile.Center, 0, 0, 247, 0f, 0f, 0, default(Color), 0.8f);
                dust.alpha = 127;
                dust.fadeIn = 0.9f;
                dust.velocity *= 0.3f;
                dust.position = projectile.Center + vector;
                dust.noGravity = true;
            }
            projectile.ai[0] += 1f;
            if (projectile.ai[0] > 210f)
            {
                projectile.alpha += 8;
                if (projectile.alpha > 255)
                {
                    projectile.active = false;
                }
            }
            else
            {
                projectile.alpha -= 25;
                if (projectile.alpha < 0)
                {
                    projectile.alpha = 0;
                }
            }
            projectile.rotation = (float)Math.Atan2(projectile.velocity.Y, projectile.velocity.X) + 1.57f;
            projectile.spriteDirection = projectile.direction;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (projectile.penetrate == 1)
            {
                projectile.penetrate++;
            }
            target.AddBuff(BuffID.Bleeding, 120);
            if (!Main.player[projectile.owner].moonLeech)
            {
                if (target.type != 488)
                {
                    if (Main.rand.Next(4) == 0)
                    {
                        float num = damage * 0.01f;
                        if ((int)num <= 0)
                        {
                            return;
                        }
                        Main.player[Main.myPlayer].statLife += (int)num;
                        Main.player[Main.myPlayer].HealEffect((int)num);
                    }
                }
            }
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(BuffID.Bleeding, 120);
        }
        public override void OnHitPvp(Player target, int damage, bool crit)
        {
            if (projectile.penetrate == 1)
            {
                projectile.penetrate++;
            }
            target.AddBuff(BuffID.Bleeding, 120);
            if (!Main.player[projectile.owner].moonLeech)
            {
                if (Main.rand.Next(4) == 0)
                {
                    float num = damage * 0.01f;
                    if ((int)num <= 0)
                    {
                        return;
                    }
                    Main.player[Main.myPlayer].statLife += (int)num;
                    Main.player[Main.myPlayer].HealEffect((int)num);
                }
            }
        }
        public override void Kill(int timeleft)
        {
            int num = Main.rand.Next(15, 25);
            for (int i = 0; i < num; i++)
            {
                int num2 = Dust.NewDust(projectile.Center, 0, 0, 247, 0f, 0f, 100, default(Color), 1.0f);
                Dust dust = Main.dust[num2];
                dust.velocity *= 3f * (0.3f + 0.7f * Main.rand.NextFloat());
                Main.dust[num2].fadeIn = 0.8f + Main.rand.NextFloat() * 0.2f;
                Main.dust[num2].noGravity = true;
                dust = Main.dust[num2];
                dust.position += Main.dust[num2].velocity * 4f;
            }
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(255, 255, 255, 0) * (1f - projectile.alpha / 255f);
        }
    }
}