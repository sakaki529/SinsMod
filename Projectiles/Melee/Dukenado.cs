using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ModLoader;

namespace SinsMod.Projectiles.Melee
{
    public class Dukenado : ModProjectile
	{
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Tornado");
			Main.projFrames[projectile.type] = 6;
		}
		public override void SetDefaults()
		{
			projectile.width = 162;
			projectile.height = 44;
    		projectile.aiStyle = -1;
    		projectile.friendly = true; 
    		projectile.hostile = false;
            projectile.penetrate = -1;
			projectile.timeLeft = 90; 
    		projectile.tileCollide = false; 
			projectile.ignoreWater = true;
			projectile.melee = true;
            projectile.usesIDStaticNPCImmunity = true;
            projectile.idStaticNPCHitCooldown = 4;
            projectile.alpha = 255;
            projectile.scale = 0.75f;
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D texture = Main.projectileTexture[projectile.type];
            int num = Main.projectileTexture[projectile.type].Height / Main.projFrames[projectile.type];
            int num2 = num * projectile.frame;
            Main.spriteBatch.Draw(texture, projectile.Center - Main.screenPosition + new Vector2(0f, projectile.gfxOffY), new Rectangle?(new Rectangle(0, num2, texture.Width, num)), projectile.GetAlpha(lightColor), projectile.rotation, new Vector2((float)texture.Width / 2f, (float)num / 2f), projectile.scale, 0, 0f);
            return false;
        }
        public override void AI()
		{
            int num = 16;
            int num2 = 16;
            float num3 = 1.5f;
            int num4 = 150;
            int num5 = 42;
            if (projectile.velocity.X != 0f)
            {
                projectile.direction = (projectile.spriteDirection = -Math.Sign(projectile.velocity.X));
            }
            projectile.frameCounter++;
            if (projectile.frameCounter > 2)
            {
                projectile.frame++;
                projectile.frameCounter = 0;
                if (projectile.frame >= Main.projFrames[projectile.type])
                {
                    projectile.frame = 0;
                }
            }
            if (projectile.localAI[0] == 0f)
            {
                projectile.localAI[0] = 1f;
                projectile.position.X = projectile.position.X + (projectile.width / 2);
                projectile.position.Y = projectile.position.Y + (projectile.height / 2);
                projectile.scale = (num + num2 - projectile.ai[1]) * num3 / (num2 + num);
                projectile.width = (int)(num4 * projectile.scale);
                projectile.height = (int)(num5 * projectile.scale);
                projectile.position.X = projectile.position.X - (projectile.width / 2);
                projectile.position.Y = projectile.position.Y - (projectile.height / 2);
                projectile.netUpdate = true;
            }
            if (projectile.ai[1] != -1f)
            {
                projectile.scale = (num + num2 - projectile.ai[1]) * num3 / (num2 + num);
                projectile.width = (int)(num4 * projectile.scale);
                projectile.height = (int)(num5 * projectile.scale);
            }
            if (!Collision.SolidCollision(projectile.position, projectile.width, projectile.height))
            {
                projectile.alpha -= 3;
                if (projectile.alpha < 60)
                {
                    projectile.alpha = 60;
                }
            }
            else
            {
                projectile.alpha += 3;
                if (projectile.alpha > 150)
                {
                    projectile.alpha = 150;
                }
            }
            if (projectile.ai[0] > 0f)
            {
                projectile.ai[0] -= 1f;
            }
            if (projectile.ai[0] == 1f && projectile.ai[1] > 0f && projectile.owner == Main.myPlayer)
            {
                projectile.netUpdate = true;
                Vector2 center = projectile.Center;
                center.Y -= num5 * projectile.scale / 2f;
                float num6 = (num + num2 - projectile.ai[1] + 1f) * num3 / (num2 + num);
                center.Y -= num5 * num6 / 2f;
                center.Y += 2f;
                Projectile.NewProjectile(center.X, center.Y, projectile.velocity.X, projectile.velocity.Y, projectile.type, projectile.damage, projectile.knockBack, projectile.owner, 10f, projectile.ai[1] - 1f);
            }
            if (projectile.ai[0] <= 0f)
            {
                float num7 = 0.104719758f;
                float num8 = projectile.width / 5f;
                num8 *= 2f;
                float num9 = (float)(Math.Cos((double)num7 * -projectile.ai[0]) - 0.5) * num8;
                projectile.position.X = projectile.position.X - num9 * -projectile.direction;
                projectile.ai[0] -= 1f;
                num9 = (float)(Math.Cos(num7 * -projectile.ai[0]) - 0.5) * num8;
                projectile.position.X = projectile.position.X + num9 * -projectile.direction;
            }
        }
        public override void Kill(int timeLeft)
        {	
			for (int i = 0; i < 20; i++)
			{
				int num = Dust.NewDust(projectile.position, projectile.width, projectile.height, 172, projectile.direction * 2, 0f, 100, default(Color), 1.4f);
				Dust dust = Main.dust[num];
				dust.color = Color.CornflowerBlue;
				dust.color = Color.Lerp(dust.color, Color.White, 0.3f);
				dust.noGravity = true;
			}
		}
        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White * (1f - projectile.alpha / 255f);
        }
    }
}