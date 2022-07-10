using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace SinsMod.Projectiles.Ranged
{
    public class Shidarezakura : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Shidarezakura");
            DisplayName.AddTranslation(GameCulture.Chinese, "枝垂桜");
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;
            ProjectileID.Sets.TrailingMode[projectile.type] = 2;
        }
        public override void SetDefaults()
        {
            projectile.width = 15;
            projectile.height = 15;
            projectile.ranged = true;
            projectile.aiStyle = 1;
            aiType = 14;
            projectile.penetrate = -1;
            projectile.timeLeft = 210;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
            projectile.extraUpdates = 1;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 6;
            projectile.alpha = 255;
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
            projectile.rotation = (float)Math.Atan2(projectile.velocity.Y, projectile.velocity.X) + 1.57f;
            if (projectile.alpha > 100)
            {
                projectile.alpha -= 45;
            }
            if (Main.rand.Next(6) == 0)
            {
                int d = Dust.NewDust(projectile.position, projectile.width, projectile.height, 72, projectile.velocity.X, projectile.velocity.Y, 100, default(Color), 0.8f);
                Main.dust[d].noGravity = true;
                Main.dust[d].velocity *= 0.4f;
                Main.dust[d].scale = 0.8f;
            }
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (!Main.player[projectile.owner].moonLeech)
            {
                if (target.type != 488)
                {
                    float num = damage * 0.004f;
                    if ((int)num == 0)
                    {
                        return;
                    }
                    if (Main.player[Main.myPlayer].lifeSteal <= 0f)
                    {
                        return;
                    }
                    Main.player[Main.myPlayer].lifeSteal -= num;
                    int num2 = projectile.owner;
                    Projectile.NewProjectile(target.Center.X, target.Center.Y, 0f, 0f, mod.ProjectileType("SakuraHeal"), 0, 0f, projectile.owner, (float)num2, num);
                }
            }
        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            damage += target.defense / 2;
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            if (!Main.player[projectile.owner].moonLeech)
            {
                float num = damage * 0.004f;
                if ((int)num == 0)
                {
                    return;
                }
                if (Main.player[Main.myPlayer].lifeSteal <= 0f)
                {
                    return;
                }
                Main.player[Main.myPlayer].lifeSteal -= num;
                int num2 = projectile.owner;
                Projectile.NewProjectile(target.Center.X, target.Center.Y, 0f, 0f, mod.ProjectileType("SakuraHeal"), 0, 0f, projectile.owner, (float)num2, num);
            }
        }
        public override void ModifyHitPvp(Player target, ref int damage, ref bool crit)
        {
            damage += target.statDefense / 2;
        }
        public override void Kill(int timeLeft)
        {
            for (int num = 0; num < 3; num++)
            {
                int num2 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 73, 0f, 0f, 100, default(Color), 0.8f);
                Main.dust[num2].noGravity = true;
                Main.dust[num2].velocity *= 1.2f;
                Main.dust[num2].velocity -= projectile.oldVelocity * 0.3f;
            }
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White * (1f - projectile.alpha / 255f);
        }
    }
}