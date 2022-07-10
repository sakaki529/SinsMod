using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ModLoader;

namespace SinsMod.Projectiles.Magic
{
    public class EibonBall : ModProjectile
	{
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Ball of Knowledge");
            Main.projFrames[projectile.type] = 4;
        }
		public override void SetDefaults()
		{
            projectile.width = 44;
            projectile.height = 44;
            projectile.magic = true;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.penetrate = 1;
			projectile.timeLeft = 120;
			projectile.tileCollide = false;
            projectile.ignoreWater = true;
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D texture = Main.projectileTexture[projectile.type];
            Color color = Color.Lerp(Color.Purple, SinsColor.MediumBlack, (float)Math.Cos(6.28318548f * (Main.LocalPlayer.miscCounter / 100f)) * 0.5f + 0.5f);
            //color.A = (byte)(255 - projectile.alpha);
            int num = Main.projectileTexture[projectile.type].Height / Main.projFrames[projectile.type];
            int num2 = num * projectile.frame;
            Main.spriteBatch.Draw(texture, projectile.Center - Main.screenPosition + new Vector2(0f, projectile.gfxOffY), new Rectangle(0, num2, texture.Width, num), projectile.GetAlpha(color), projectile.rotation, new Vector2(texture.Width / 2f, num / 2f), projectile.scale, 0, 0f);
            return false;
        }
        public override void AI()
        {
            projectile.frameCounter++;
            if (projectile.frameCounter > 3)
            {
                projectile.frameCounter = 0;
                projectile.frame++;
                if (projectile.frame >= Main.projFrames[projectile.type])
                {
                    projectile.frame = 0;
                }
            }
            if (projectile.alpha < 30)
            {
                projectile.alpha = 30;
            }
            else if (projectile.alpha > 30)
            {
                projectile.alpha -= 12;
            }
            projectile.velocity.X *= 0.985f;
            projectile.velocity.Y *= 0.985f;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (projectile.timeLeft > 1)
            {
                projectile.timeLeft = 1;
            }
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            if (projectile.timeLeft > 1)
            {
                projectile.timeLeft = 1;
            }
        }
        public override void OnHitPvp(Player target, int damage, bool crit)
        {
            if (projectile.timeLeft > 1)
            {
                projectile.timeLeft = 1;
            }
        }
        public override void Kill(int timeLeft)
        {
            Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 14, 1f, 0f);
            projectile.position.X = projectile.position.X + (projectile.width / 2);
            projectile.position.Y = projectile.position.Y + (projectile.height / 2);
            projectile.width = 180;
            projectile.height = 180;
            projectile.position.X = projectile.position.X - (projectile.width / 2);
            projectile.position.Y = projectile.position.Y - (projectile.height / 2);
            projectile.Damage();
            for (int i = 0; i < 30; i++)
            {
                int num = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 173, 0f, 0f, 100, default(Color), 1.1f);
                Main.dust[num].velocity *= 3f;
                if (Main.rand.Next(2) == 0)
                {
                    Main.dust[num].scale = 0.5f;
                    Main.dust[num].fadeIn = 1f + Main.rand.Next(10) * 0.1f;
                }
            }
            for (int j = 0; j < 60; j++)
            {
                int num2 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 173, 0f, 0f, 100, default(Color), 1.2f);
                Main.dust[num2].noGravity = true;
                Main.dust[num2].velocity *= 5f;
                num2 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 173, 0f, 0f, 100, default(Color), 1f);
                Main.dust[num2].noGravity = true;
                Main.dust[num2].velocity *= 2f;
            }
        }
    }
}