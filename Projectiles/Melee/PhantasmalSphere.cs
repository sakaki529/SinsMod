using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Projectiles.Melee
{
    public class PhantasmalSphere : ModProjectile
    {
        public float rot;
        public Vector2 rotVec = new Vector2(0f, 0f);
        public Vector2 RotateVector(Vector2 origin, Vector2 vecToRot, float rot)
        {
            return new Vector2((float)(Math.Cos(rot) * ((double)vecToRot.X - origin.X) - Math.Sin(rot) * ((double)vecToRot.Y - origin.Y)) + origin.X, (float)(Math.Sin(rot) * ((double)vecToRot.X - origin.X) + Math.Cos(rot) * ((double)vecToRot.Y - origin.Y)) + origin.Y);
        }
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Phantasmal Sphere");
            Main.projFrames[projectile.type] = 2;
        }
		public override void SetDefaults()
		{
            projectile.width = 46;
            projectile.height = 46;
            projectile.melee = true;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.penetrate = -1;
            projectile.alpha = 255;
            projectile.timeLeft = 120;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D texture = Main.projectileTexture[projectile.type];
            int num = Main.projectileTexture[projectile.type].Height / Main.projFrames[projectile.type];
            int y = num * projectile.frame;
            Rectangle rectangle = new Rectangle(0, y, texture.Width, num);
            Vector2 origin = rectangle.Size() / 2f;
            Main.spriteBatch.Draw(texture, projectile.Center - Main.screenPosition + new Vector2(0f, projectile.gfxOffY), new Microsoft.Xna.Framework.Rectangle?(rectangle), projectile.GetAlpha(lightColor), projectile.rotation, origin, projectile.scale, SpriteEffects.None, 0f);
            return false;
        }
        public override bool PreAI()
        {
            Projectile parent = Main.projectile[(int)projectile.ai[0]];
            if (!parent.active || parent.type != mod.ProjectileType("TheTrueEyeOfCthulhu"))
            {
                projectile.Kill();
            }
            rotVec = new Vector2(0f, projectile.localAI[1]);
            //if (projectile.localAI[1] < 48f)
            {
                projectile.localAI[1] += 2.5f;
            }
            rot -= 0.09f * projectile.localAI[0];
            //projectile.rotation += 0.05f / projectile.Opacity;
            projectile.Center = parent.Center + RotateVector(new Vector2(), rotVec, rot + projectile.ai[1] * (6.28f / 6f));
            projectile.netUpdate = true;
            return true;
        }
        public override void AI()
        {
            //projectile.timeLeft = 2;
            projectile.alpha -= 4;
            if (projectile.alpha < 0)
            {
                projectile.alpha = 0;
            }
            projectile.scale = (1f - projectile.alpha / 255f) * 1f;
            for (int i = 0; i < 2; i++)
            {
                float num = Main.rand.NextFloat(-0.5f, 0.5f);
                Vector2 vector = new Vector2(-projectile.width * 0.65f * projectile.scale, 0.0f).RotatedBy(num * 6.28318548202515, new Vector2()).RotatedBy(projectile.velocity.ToRotation(), new Vector2());
                int num2 = Dust.NewDust(projectile.Center - Vector2.One * 5f, 10, 10, 229, -projectile.velocity.X / 3f, -projectile.velocity.Y / 3f, 150, Color.Transparent, 0.5f);
                Main.dust[num2].velocity = Vector2.Zero;
                Main.dust[num2].position = projectile.Center + vector;
                Main.dust[num2].noGravity = true;
            }
            int num3 = projectile.frameCounter + 1;
            projectile.frameCounter = num3;
            if (num3 >= 6)
            {
                projectile.frameCounter = 0;
                num3 = projectile.frame + 1;
                projectile.frame = num3;
                if (num3 >= Main.projFrames[projectile.type])
                {
                    projectile.frame = 0;
                }
            }
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.immune[projectile.owner] = 8;
            target.AddBuff(BuffID.MoonLeech, 300);
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(BuffID.MoonLeech, 300);
        }
        public override void OnHitPvp(Player target, int damage, bool crit)
        {
            target.AddBuff(BuffID.MoonLeech, 300);
        }
        public override void Kill(int timeleft)
		{
            Main.PlaySound(4, projectile.Center, 6);
            projectile.position = projectile.Center;
            projectile.width = projectile.height = (int)(82 * projectile.scale);
            projectile.Center = projectile.position;
            for (int i = 0; i < 3; ++i)
            {
                int num = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 31, 0.0f, 0.0f, 100, new Color(), 0.5f);
                Main.dust[num].position = new Vector2(projectile.width / 2, 0.0f).RotatedBy(6.28318548202515 * Main.rand.NextDouble(), new Vector2()) * (float)Main.rand.NextDouble() + projectile.Center;
            }
            for (int j = 0; j < 10; ++j)
            {
                int num2 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 229, 0.0f, 0.0f, 0, new Color(), 1.5f);
                Main.dust[num2].position = new Vector2(projectile.width / 2, 0.0f).RotatedBy(6.28318548202515 * Main.rand.NextDouble(), new Vector2()) * (float)Main.rand.NextDouble() + projectile.Center;
                Main.dust[num2].noGravity = true;
                Dust dust = Main.dust[num2];
                dust.velocity = dust.velocity * 1f;
                int num3 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 229, 0.0f, 0.0f, 100, new Color(), 1.0f);
                Main.dust[num3].position = new Vector2(projectile.width / 2, 0.0f).RotatedBy(6.28318548202515 * Main.rand.NextDouble(), new Vector2()) * (float)Main.rand.NextDouble() + projectile.Center;
                Dust dust2 = Main.dust[num3];
                dust2.velocity = dust2.velocity * 1f;
                Main.dust[num3].noGravity = true;
            }
            projectile.Damage();
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White * (1f - projectile.alpha / 255f);
        }
    }
}