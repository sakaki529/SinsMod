using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Projectiles.Summon
{
    public class NightmareLaserSummon : ModProjectile
    {
        public override string Texture => "SinsMod/Projectiles/Ranged/NightmareLaser";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Laser");
        }
        public override void SetDefaults()
        {
            projectile.width = 6;
            projectile.height = 6;
            projectile.minion = true;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.aiStyle = 0;
            projectile.minionSlots = 0f;
            projectile.extraUpdates = 2;
            projectile.penetrate = 1;
            projectile.timeLeft = 300;
            projectile.tileCollide = true;
            projectile.ignoreWater = true;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 60;
            projectile.alpha = 255;
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            SpriteEffects spriteEffects = SpriteEffects.None;
            Color color = Lighting.GetColor((int)(projectile.position.X + projectile.width * 0.5) / 16, (int)((projectile.position.Y + projectile.height * 0.5) / 16.0));
            int num = 0;
            int num2 = 0;
            float num3 = (Main.projectileTexture[projectile.type].Width - projectile.width) * 0.5f + projectile.width * 0.5f;
            Rectangle value = new Rectangle((int)Main.screenPosition.X - 500, (int)Main.screenPosition.Y - 500, Main.screenWidth + 1000, Main.screenHeight + 1000);
            if (projectile.getRect().Intersects(value))
            {
                Vector2 value2 = new Vector2(projectile.position.X - Main.screenPosition.X + num3 + num2, projectile.position.Y - Main.screenPosition.Y + (projectile.height / 2) + projectile.gfxOffY);
                float num4 = 100f;
                float scaleFactor = 3f;
                if (projectile.ai[1] == 1f)
                {
                    num4 = (int)projectile.localAI[0];
                }
                for (int num5 = 1; num5 <= (int)projectile.localAI[0]; num5++)
                {
                    Vector2 value3 = Vector2.Normalize(projectile.velocity) * num5 * scaleFactor;
                    Color color2 = projectile.GetAlpha(color);
                    color2 *= (num4 - num5) / num4;
                    color2.A = 0;
                    Texture2D texture = Main.projectileTexture[projectile.type];
                    Vector2 vector = value2 - value3;
                    Rectangle? sourceRectangle2 = null;
                    Main.spriteBatch.Draw(texture, vector, sourceRectangle2, color2, projectile.rotation, new Vector2(num3, projectile.height / 2 + num), projectile.scale, spriteEffects, 0f);
                }
            }
            return false;
        }
        public override void AI()
        {
            projectile.rotation = (float)Math.Atan2(projectile.velocity.Y, projectile.velocity.X) + 1.57f;
            Lighting.AddLight((int)projectile.Center.X / 16, (int)projectile.Center.Y / 16, 0.4f, 0.4f, 0.4f);
            if (projectile.alpha > 0)
            {
                projectile.alpha -= 25;
            }
            if (projectile.alpha < 0)
            {
                projectile.alpha = 0;
            }
            if (projectile.ai[0] == 0f)
            {
                projectile.ai[0] = 1f;
                Main.PlaySound(SoundID.Item91, projectile.position);
            }
            float num = 100f;
            float num2 = 3f;
            if (projectile.ai[1] == 0f)
            {
                projectile.localAI[0] += num2;
                if (projectile.localAI[0] > num)
                {
                    projectile.localAI[0] = num;
                }
            }
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(mod.BuffType("Nightmare"), 180);
            Projectile.NewProjectile(target.Center.X, target.Center.Y, 0f, 0f, mod.ProjectileType("NigthmareExplosion"), projectile.damage, projectile.knockBack, projectile.owner, 0f, 2f);
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(mod.BuffType("Nightmare"), 180);
            Projectile.NewProjectile(target.Center.X, target.Center.Y, 0f, 0f, mod.ProjectileType("NigthmareExplosion"), projectile.damage, projectile.knockBack, projectile.owner, 0f, 2f);
        }
        public override void OnHitPvp(Player target, int damage, bool crit)
        {
            target.AddBuff(mod.BuffType("Nightmare"), 180);
            Projectile.NewProjectile(target.Center.X, target.Center.Y, 0f, 0f, mod.ProjectileType("NigthmareExplosion"), projectile.damage, projectile.knockBack, projectile.owner, 0f, 2f);
        }
        public override void Kill(int timeLeft)
        {
            int num = Main.rand.Next(3, 7);
            for (int num2 = 0; num2 < num; num2++)
            {
                int num3 = Dust.NewDust(projectile.Center - projectile.velocity / 2f, 0, 0, 182, 0f, 0f, 100, default(Color), 1.0f);
                Dust dust = Main.dust[num3];
                dust.velocity *= 2f;
                Main.dust[num3].noGravity = true;
            }
        }
        public override Color? GetAlpha(Color lightColor)
        {
            int num;
            int num2;
            int num3;
            num = 255 - projectile.alpha;
            num2 = 255 - projectile.alpha;
            num3 = 255 - projectile.alpha;
            float num10 = (255 - projectile.alpha) / 255f;
            num = (int)(lightColor.R * num10);
            num2 = (int)(lightColor.G * num10);
            num3 = (int)(lightColor.B * num10);
            int num4 = lightColor.A - projectile.alpha;
            if (num4 < 0)
            {
                num4 = 0;
            }
            if (num4 > 255)
            {
                num4 = 255;
            }
            return new Color(num, num2, num3, num4);
        }
    }
}