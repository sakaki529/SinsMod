using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace SinsMod.Projectiles.Ranged
{
	public class PhantasmalPulse : ModProjectile
	{
        public override string Texture => "SinsMod/Extra/Placeholder/BlankTex";
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Phantasmal Pulse");
        }
		public override void SetDefaults()
		{
            projectile.width = 12;
            projectile.height = 12;
            projectile.ranged = true;
            projectile.friendly = true;
            projectile.tileCollide = true;
            projectile.penetrate = - 1;
            projectile.extraUpdates = 2;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 60;
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            SinsUtility.DrawAroundOrigin(projectile.whoAmI, lightColor);
            return false;
        }
        public override bool PreAI()
        {
            for (int i = 0; i < 10; i++)
            {
                float x = projectile.Center.X - projectile.velocity.X / 10f * i;
                float y = projectile.Center.Y - projectile.velocity.Y / 10f * i;
                int dust = Dust.NewDust(new Vector2(x, y), 1, 1, 58, 0f, 0f, 0, default(Color), 1f);
                Main.dust[dust].alpha = projectile.alpha;
                Main.dust[dust].position.X = x;
                Main.dust[dust].position.Y = y;
                Main.dust[dust].velocity *= 0f;
                Main.dust[dust].noGravity = true;
            }
            if (projectile.localAI[1] == 0f)
            {
                projectile.localAI[1] = 1f;
            }
            if (projectile.ai[0] == 0f || projectile.ai[0] == 2f)
            {
                projectile.scale += 0.01f;
                projectile.alpha -= 50;
                if (projectile.alpha <= 0)
                {
                    projectile.ai[0] = 1f;
                    projectile.alpha = 0;
                }
            }
            else if (projectile.ai[0] == 1f)
            {
                projectile.scale -= 0.01f;
                projectile.alpha += 50;
                if (projectile.alpha >= 255)
                {
                    projectile.ai[0] = 2f;
                    projectile.alpha = 255;
                }
            }
            return false;
        }
        public override void Kill(int timeLeft)
        {
            projectile.localNPCHitCooldown = 0;
            Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 14);
            SinsUtility.Explode(projectile.whoAmI, 120, 120, delegate
            {
                for (int i = 0; i < 40; i++)
                {
                    int num = Dust.NewDust(projectile.position, projectile.width, projectile.height, 58, 0f, -2f, 0, default(Color), 2f);
                    Main.dust[num].noGravity = true;
                    Dust dust = Main.dust[num];
                    dust.position.X = dust.position.X + (Main.rand.Next(-50, 51) / 20 - 1.5f);
                    Dust dust2 = Main.dust[num];
                    dust2.position.Y = dust2.position.Y + (Main.rand.Next(-50, 51) / 20 - 1.5f);
                    if (Main.dust[num].position != projectile.Center)
                    {
                        Main.dust[num].velocity = projectile.DirectionTo(Main.dust[num].position) * 6f;
                    }
                }
            });
            projectile.Damage();
        }
    }
}