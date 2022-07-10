using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Projectiles.Thrown
{
    public class NightmareReaper : ModProjectile
	{
        private bool Spawn;
        private int Count;
        public override string Texture => "SinsMod/Items/Weapons/MultiType/NightmareReaper";
        public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Nightmare Reaper");
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 8;
            ProjectileID.Sets.TrailingMode[projectile.type] = 2;
        }
		public override void SetDefaults()
		{
            projectile.width = 56;
			projectile.height = 56;
            projectile.thrown = true;
            projectile.friendly = true;
            projectile.hostile = false;
            //projectile.penetrate = -1;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
            projectile.alpha = 255;
            projectile.extraUpdates = 1;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 1;
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D texture = Main.projectileTexture[projectile.type];
            Texture2D glowTexture = mod.GetTexture("Glow/Item/NightmareReaper_Glow");
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
                Color color3 = Color.White;
                color3 *= (float)(ProjectileID.Sets.TrailCacheLength[projectile.type] - i) / ProjectileID.Sets.TrailCacheLength[projectile.type];
                Vector2 value = projectile.oldPos[i];
                float num3 = projectile.oldRot[i];
                Main.spriteBatch.Draw(texture, value + projectile.Size / 2f - Main.screenPosition + new Vector2(0, projectile.gfxOffY), rectangle, color2, num3, origin, projectile.scale, spriteEffects, 0f);
                Main.spriteBatch.Draw(glowTexture, value + projectile.Size / 2f - Main.screenPosition + new Vector2(0, projectile.gfxOffY), rectangle, color3, num3, origin, projectile.scale, spriteEffects, 0f);
            }
            Main.spriteBatch.Draw(texture, projectile.Center - Main.screenPosition + new Vector2(0f, projectile.gfxOffY), rectangle, projectile.GetAlpha(lightColor), projectile.rotation, origin, projectile.scale, spriteEffects, 0f);
            Main.spriteBatch.Draw(glowTexture, projectile.Center - Main.screenPosition + new Vector2(0f, projectile.gfxOffY), rectangle, projectile.GetAlpha(Color.White), projectile.rotation, origin, projectile.scale, spriteEffects, 0f);
            return false;
        }
        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, 172, 0f, 0f, 0, default(Color), 1f);
            Main.dust[dust].noGravity = true;
            Main.dust[dust].velocity = Utils.NextFloat(Main.rand, 0.3f, 0.6f) * projectile.velocity;
            Main.dust[dust].scale = 0.9f + Utils.NextFloat(Main.rand, 0.3f);
            Main.dust[dust].shader = GameShaders.Armor.GetSecondaryShader(44, Main.LocalPlayer);
            projectile.alpha -= 15;
            if (projectile.alpha < 0)
            {
                projectile.alpha = 0;
            }
            if (projectile.soundDelay == 0)
            {
                projectile.soundDelay = 8;
                Main.PlaySound(SoundID.Item7, projectile.position);
            }
            if (projectile.ai[0] != 0)
            {
                projectile.melee = true;
                projectile.thrown = false;
            }
            if (projectile.localAI[0] == 0f)
            {
                projectile.localAI[1] += 1f;
                if (projectile.localAI[1] >= 50f)
                {
                    projectile.localAI[0] = 1f;
                    projectile.localAI[1] = 0f;
                    projectile.netUpdate = true;
                }
            }
            else
            {
                float num = 20f;
                float num2 = 2f;
                Vector2 vector = new Vector2(projectile.position.X + projectile.width * 0.5f, projectile.position.Y + projectile.height * 0.5f);
                float num3 = Main.player[projectile.owner].position.X + (player.width / 2) - vector.X;
                float num4 = Main.player[projectile.owner].position.Y + (player.height / 2) - vector.Y;
                float num5 = (float)Math.Sqrt(num3 * num3 + num4 * num4);
                if (num5 > 3000f)
                {
                    projectile.Kill();
                }
                num5 = num / num5;
                num3 *= num5;
                num4 *= num5;
                if (projectile.velocity.X < num3)
                {
                    projectile.velocity.X = projectile.velocity.X + num2;
                    if (projectile.velocity.X < 0f && num3 > 0f)
                    {
                        projectile.velocity.X = projectile.velocity.X + num2;
                    }
                }
                else
                {
                    if (projectile.velocity.X > num3)
                    {
                        projectile.velocity.X = projectile.velocity.X - num2;
                        if (projectile.velocity.X > 0f && num3 < 0f)
                        {
                            projectile.velocity.X = projectile.velocity.X - num2;
                        }
                    }
                }
                if (projectile.velocity.Y < num4)
                {
                    projectile.velocity.Y = projectile.velocity.Y + num2;
                    if (projectile.velocity.Y < 0f && num4 > 0f)
                    {
                        projectile.velocity.Y = projectile.velocity.Y + num2;
                    }
                }
                else
                {
                    if (projectile.velocity.Y > num4)
                    {
                        projectile.velocity.Y = projectile.velocity.Y - num2;
                        if (projectile.velocity.Y > 0f && num4 < 0f)
                        {
                            projectile.velocity.Y = projectile.velocity.Y - num2;
                        }
                    }
                }
                if (Main.myPlayer == projectile.owner && projectile.Hitbox.Intersects(player.Hitbox))
                {
                    projectile.Kill();
                }
            }
            Count--;
            if (!Spawn)
            {
                Count = Main.rand.Next(8, 16);
                Spawn = true;
            }
            if (Count < 0 && Spawn)
            {
                float num6 = (projectile.direction <= 0) ? -1f : 1f;
                Vector2 vector = projectile.Center + new Vector2(Main.rand.Next(0, 16), Main.rand.Next(0, 16));
                if (projectile.owner == Main.myPlayer)
                {
                    Projectile.NewProjectile(vector, Vector2.Zero, mod.ProjectileType("NightmareReaperBlade"), projectile.damage / 3, 1f, projectile.owner, projectile.ai[0], num6);
                }
                Count = Main.rand.Next(12, 18);
            }
            float dir = (projectile.direction <= 0) ? -1f : 1f;
            projectile.rotation += dir * (0.3f / (projectile.extraUpdates + 1));
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (projectile.penetrate == 1)
            {
                projectile.penetrate++;
            }
            target.AddBuff(mod.BuffType("Nightmare"), 120);
            target.AddBuff(BuffID.Bleeding, 300);
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(mod.BuffType("Nightmare"), 120);
            target.AddBuff(BuffID.Bleeding, 300);
        }
        public override void OnHitPvp(Player target, int damage, bool crit)
        {
            if (projectile.penetrate == 1)
            {
                projectile.penetrate++;
            }
            target.AddBuff(mod.BuffType("Nightmare"), 120);
            target.AddBuff(BuffID.Bleeding, 300);
        }
    }
}