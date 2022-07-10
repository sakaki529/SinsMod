using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Projectiles.Typeless
{
    public class BlackCoinsFalling : ModProjectile
	{
        protected bool falling = true;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Black Coin");
            ProjectileID.Sets.ForcePlateDetection[projectile.type] = true;
        }
        public override void SetDefaults()
		{
			projectile.width = 10;
			projectile.height = 10;
            projectile.friendly = true;
            projectile.hostile = false;
			projectile.penetrate = -1;
			projectile.tileCollide = true;
			projectile.ignoreWater = true;
            projectile.noDropItem = false;
            /*projectile.width = (int)(projectile.width * projectile.scale);
            projectile.height = (int)(projectile.height * projectile.scale);
            projectile.maxPenetrate = projectile.penetrate;*/
        }
        public override bool CanDamage()
        {
            return projectile.localAI[1] != -1f;
        }
        public override void AI()
		{
            if (Main.rand.Next(3) == 0)
            {
                int num = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 82, 0f, projectile.velocity.Y / 2f, 0, default(Color), 1f);
                Main.dust[num].noGravity = true;
                Dust dust = Main.dust[num];
                dust.velocity -= projectile.velocity * 0.5f;
            }
            projectile.tileCollide = true;
            projectile.localAI[1] = 0f;
            projectile.ai[0] = 1f;
            if (projectile.ai[0] == 1f)
            {
                if (!falling)
                {
                    projectile.ai[1] += 1f;
                    if (projectile.ai[1] >= 60f)
                    {
                        projectile.ai[1] = 60f;
                        projectile.velocity.Y += 0.2f;
                    }
                }
                else
                {
                    projectile.velocity.Y += 0.41f;
                }
            }
            else if (projectile.ai[0] == 2f)
            {
                projectile.velocity.Y += 0.2f;
                if (projectile.velocity.X < -0.04f)
                {
                    projectile.velocity.X += 0.04f;
                }
                else if (projectile.velocity.X > 0.04f)
                {
                    projectile.velocity.X -= 0.04f;
                }
                else
                {
                    projectile.velocity.X = 0f;
                }
            }
            projectile.rotation += 0.1f;
            if (projectile.velocity.Y > 10f)
            {
                projectile.velocity.Y = 10f;
            }
        }
        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough)
        {
            if (falling)
            {
                projectile.velocity = Collision.AnyCollision(projectile.position, projectile.velocity, projectile.width, projectile.height, true);
                return false;
            }
            projectile.velocity = Collision.TileCollision(projectile.position, projectile.velocity, projectile.width, projectile.height, fallThrough, fallThrough, 1);
            return false;
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            //Main.PlaySound(18, (int)projectile.position.X, (int)projectile.position.Y, 10, 1f, 0f);
            return true;
        }
        public override void Kill(int timeLeft)
        {
            for (int num = 0; num < 5; num++)
            {
                int num2 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 82, 0f, projectile.velocity.Y / 2f, 0, default(Color), 1f);
                Main.dust[num2].noGravity = true;
                Dust dust = Main.dust[num2];
                dust.velocity -= projectile.velocity * 0.5f;
            }
            int num3 = (int)(projectile.Center.X / 16f);
            int num4 = (int)(projectile.Center.Y / 16f);
            if (Main.tile[num3, num4].halfBrick() && projectile.velocity.Y > 0f && Math.Abs(projectile.velocity.Y) > Math.Abs(projectile.velocity.X))
            {
                num4--;
            }
            if (!Main.tile[num3, num4].active())
            {
                if (Main.tile[num3, num4].type == 314)
                {
                    return;
                }
                WorldGen.PlaceTile(num3, num4, mod.TileType("BlackCoinPile"), false, true, -1, 0);
                WorldGen.SquareTileFrame(num3, num4, true);
            }
        }
    }
}