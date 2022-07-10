using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace SinsMod.Projectiles.Melee
{
    public class TidalExplosion : ModProjectile
	{
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Explosion");
            Main.projFrames[projectile.type] = 5;
        }
		public override void SetDefaults()
		{
            projectile.width = 20;
            projectile.height = 20;
            projectile.melee = true;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.ignoreWater = true;
            projectile.tileCollide = false;
            projectile.penetrate = -1;
            projectile.timeLeft = 60;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 6;
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D texture = Main.projectileTexture[projectile.type];
            int num = Main.projectileTexture[projectile.type].Height / Main.projFrames[projectile.type];
            int num2 = num * projectile.frame;
            Main.spriteBatch.Draw(texture, projectile.Center - Main.screenPosition + new Vector2(0f, projectile.gfxOffY), new Rectangle?(new Rectangle(0, num2, texture.Width, num)), projectile.GetAlpha(lightColor), projectile.rotation, new Vector2(texture.Width / 2f, num / 2f), projectile.scale, 0, 0f);
            return false;
        }
        public override void AI()
        {
            projectile.velocity = Vector2.Zero;
            //projectile.ai[1] += 0.01f;
            //projectile.scale = projectile.ai[1];
            if (projectile.ai[1] < 0)
            {
                projectile.hide = true;
                projectile.localNPCHitCooldown = 4;
            }
            else
            {
                projectile.ai[1] += 0.01f;
                projectile.scale = projectile.ai[1];
                int num = Dust.NewDust(projectile.position, projectile.width, projectile.height, 172, 0f, 0f, 0, default(Color), 0.8f);
                Main.dust[num].noGravity = true;
                Main.dust[num].scale = 1.5f;
                Main.dust[num].velocity *= 1f;
            }
            if (projectile.ai[0] == 1f)
            {
                projectile.melee = false;
                projectile.thrown = true;
            }
            projectile.frameCounter++;
            if (projectile.frameCounter >= 3)
            {
                projectile.frame++;
                projectile.frameCounter = 0;
                if (projectile.frame >= Main.projFrames[projectile.type])
                {
                    projectile.Kill();
                }
            }
            /*int num = Dust.NewDust(projectile.position, projectile.width, projectile.height, 172, 0f, 0f, 0, default(Color), 0.8f);
            Main.dust[num].noGravity = true;
            Main.dust[num].scale = 1.5f;
            Main.dust[num].velocity *= 1f;*/
            if (projectile.localAI[0] == 0)
            {
                Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 14, 1f, 0f);
                projectile.localAI[0] = 1f;
            }
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.immune[projectile.owner] = 4;
            Rectangle hitbox = target.Hitbox;
            for (int num = 0; num < 20; num++)
            {
                int num2 = 172;
                int num3 = Dust.NewDust(hitbox.TopLeft(), target.width, target.height, num2, 0f, -2.5f, 0, default(Color), 1f);
                Main.dust[num3].alpha = 200;
                Dust dust = Main.dust[num3];
                dust.velocity *= 1.4f;
                dust = Main.dust[num3];
                dust.scale += Main.rand.NextFloat();
            }
        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if (crit)
            {
                damage /= 2;
            }
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            Rectangle hitbox = target.Hitbox;
            for (int num = 0; num < 20; num++)
            {
                int num2 = 172;
                int num3 = Dust.NewDust(hitbox.TopLeft(), target.width, target.height, num2, 0f, -2.5f, 0, default(Color), 1f);
                Main.dust[num3].alpha = 200;
                Dust dust = Main.dust[num3];
                dust.velocity *= 1.4f;
                dust = Main.dust[num3];
                dust.scale += Main.rand.NextFloat();
            }
        }
        public override void ModifyHitPlayer(Player target, ref int damage, ref bool crit)
        {
            if (crit)
            {
                damage /= 2;
            }
        }
        public override void OnHitPvp(Player target, int damage, bool crit)
        {
            Rectangle hitbox = target.Hitbox;
            for (int num = 0; num < 20; num++)
            {
                int num2 = 172;
                int num3 = Dust.NewDust(hitbox.TopLeft(), target.width, target.height, num2, 0f, -2.5f, 0, default(Color), 1f);
                Main.dust[num3].alpha = 200;
                Dust dust = Main.dust[num3];
                dust.velocity *= 1.4f;
                dust = Main.dust[num3];
                dust.scale += Main.rand.NextFloat();
            }
            /*Rectangle hitbox = target.Hitbox;
            for (int num = 0; num < 20; num++)
            {
                int num2 = Utils.SelectRandom<int>(Main.rand, new int[]
                {
                        6,
                        259,
                        158
                });
                int num3 = Dust.NewDust(hitbox.TopLeft(), target.width, target.height, num2, 0f, -2.5f, 0, default(Color), 1f);
                Main.dust[num3].alpha = 200;
                Dust dust = Main.dust[num3];
                dust.velocity *= 1.4f;
                dust = Main.dust[num3];
                dust.scale += Main.rand.NextFloat();
            }*/
        }
        public override void ModifyHitPvp(Player target, ref int damage, ref bool crit)
        {
            if (crit)
            {
                damage /= 2;
            }
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(255, 255, 255, 200);
        }
    }
}