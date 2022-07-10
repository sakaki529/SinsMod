using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Projectiles.Melee
{
    public class NightmareWave : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Nightmare Wave");
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
        }
        public override void SetDefaults()
        {
            projectile.width = 120;
            projectile.height = 120;
            projectile.aiStyle = 0;
            projectile.friendly = true;
            projectile.melee = true;
            projectile.timeLeft = 300;
            //projectile.penetrate = -1;
            projectile.ignoreWater = true;
            projectile.tileCollide = false;
            projectile.extraUpdates = 1;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 0;
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
                Vector2 vector = Utils.RandomVector2(Main.rand, -0.5f, 0.5f) * new Vector2(96f, 96f);
                vector = vector.RotatedBy(projectile.velocity.ToRotation(), default(Vector2));
                Dust dust = Dust.NewDustDirect(projectile.Center, 0, 0, 182, 0f, 0f, 0, default(Color), 0.6f);
                dust.alpha = 127;
                dust.fadeIn = 0.7f;
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
            target.AddBuff(mod.BuffType("Nightmare"), 120);
            target.AddBuff(BuffID.Bleeding, 300);
            target.AddBuff(BuffID.Ichor, 180);
            if (!Main.player[projectile.owner].moonLeech && projectile.numHits < 3)
            {
                if (target.type != 488)
                {
                    float num = damage * 0.003f;
                    if ((int)num <= 0)
                    {
                        return;
                    }
                    Main.player[Main.myPlayer].statLife += (int)num;
                    Main.player[Main.myPlayer].HealEffect((int)num);
                }
            }
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(mod.BuffType("Nightmare"), 120);
            target.AddBuff(BuffID.Bleeding, 300);
            target.AddBuff(BuffID.Ichor, 180);
        }
        public override void OnHitPvp(Player target, int damage, bool crit)
        {
            if (projectile.penetrate == 1)
            {
                projectile.penetrate++;
            }
            target.AddBuff(mod.BuffType("Nightmare"), 120);
            target.AddBuff(BuffID.Bleeding, 300);
            target.AddBuff(BuffID.Ichor, 180);
            if (!Main.player[projectile.owner].moonLeech && projectile.numHits < 3)
            {
                float num = damage * 0.003f;
                if ((int)num <= 0)
                {
                    return;
                }
                Main.player[Main.myPlayer].statLife += (int)num;
                Main.player[Main.myPlayer].HealEffect((int)num);
            }
        }
        public override void Kill(int timeleft)
        {
            int num = Main.rand.Next(15, 25);
            for (int i = 0; i < num; i++)
            {
                int num2 = Dust.NewDust(projectile.Center, 0, 0, 182, 0f, 0f, 100, default(Color), 0.8f);
                Dust dust = Main.dust[num2];
                dust.velocity *= 4f * (0.3f + 0.7f * Main.rand.NextFloat());
                Main.dust[num2].fadeIn = 0.8f + Main.rand.NextFloat() * 0.2f;
                Main.dust[num2].noGravity = true;
                dust = Main.dust[num2];
                dust.position += Main.dust[num2].velocity * 4f;
            }
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(200, 200, 200, 0) * (1f - projectile.alpha / 255f);
        }
    }
}