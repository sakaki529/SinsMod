using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Projectiles.Magic
{
    public class BlazingWheel : ModProjectile
    {
        private int directionY;
        private bool collideX;
        private bool collideY;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Blazing Wheel");
            Main.projFrames[projectile.type] = 4;
        }
        public override void SetDefaults()
        {
            projectile.width = 34;
            projectile.height = 34;
            projectile.magic = true;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.penetrate = -1;
            projectile.timeLeft = 360;
            projectile.tileCollide = true;
            projectile.ignoreWater = false;
            projectile.alpha = 100;
            projectile.scale = 1.2f;
            projectile.GetGlobalProjectile<SinsProjectile>().drawCenter = true;
        }
        public override void AI()
        {
            projectile.frameCounter++;
            if (projectile.frameCounter >= 3)
            {
                projectile.frameCounter = 0;
                projectile.frame++;
                if (projectile.frame >= Main.projFrames[projectile.type])
                {
                    projectile.frame = 0;
                }
            }
            if (projectile.wet)
            {
                projectile.Kill();
            }
            if (projectile.ai[0] == 0f)
            {
                projectile.ai[0] = 1f;
            }
            int num = 6;
            if (projectile.ai[1] == 0f)
            {
                projectile.rotation += projectile.direction * directionY * 0.13f;
                if (collideY)
                {
                    projectile.ai[0] = 2f;
                }
                if (!collideY && projectile.ai[0] == 2f)
                {
                    projectile.direction = -projectile.direction;
                    projectile.ai[1] = 1f;
                    projectile.ai[0] = 1f;
                }
                if (collideX)
                {
                    directionY = -directionY;
                    projectile.ai[1] = 1f;
                }
            }
            else
            {
                projectile.rotation -= projectile.direction * directionY * 0.13f;
                if (collideX)
                {
                    projectile.ai[0] = 2f;
                }
                if (!collideX && projectile.ai[0] == 2f)
                {
                    directionY = -directionY;
                    projectile.ai[1] = 0f;
                    projectile.ai[0] = 1f;
                }
                if (collideY)
                {
                    projectile.direction = -projectile.direction;
                    projectile.ai[1] = 0f;
                }
            }
            if (collideX)
            {
                projectile.velocity.X = num * projectile.direction;
            }
            if (collideY)
            {
                projectile.velocity.Y = num * directionY;
            }
            directionY = projectile.velocity.Y < 0 ? -1 : 1;
            float num2 = (270 - Main.mouseTextColor) / 400f;
            Lighting.AddLight((int)(projectile.position.X + projectile.width / 2) / 16, (int)(projectile.position.Y + projectile.height / 2) / 16, 0.9f, 0.3f + num2, 0.2f);
            projectile.spriteDirection = projectile.direction;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (Main.rand.Next(3) == 0)
            {
                target.AddBuff(BuffID.OnFire, 180);
            }
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            if (Main.rand.Next(3) == 0)
            {
                target.AddBuff(BuffID.OnFire, 180);
            }
        }
        public override void OnHitPvp(Player target, int damage, bool crit)
        {
            if (Main.rand.Next(3) == 0)
            {
                target.AddBuff(BuffID.OnFire, 180);
            }
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (projectile.oldVelocity.X != projectile.velocity.X)
            {
                collideX = true;
            }
            if (projectile.oldVelocity.Y != projectile.velocity.Y)
            {
                collideY = true;
            }
            Vector2 position = new Vector2(projectile.position.X + projectile.width / 2, projectile.position.Y + projectile.height / 2);
            int num = 12;
            int num2 = 12;
            position.X -= num / 2;
            position.Y -= num2 / 2;
            projectile.velocity = Collision.noSlopeCollision(position, projectile.velocity, num, num2, true, true);
            return false;
        }
        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough)
        {
            width = 12;
            height = 12;
            return true;
        }
        public override void Kill(int timeLeft)
        {

        }
        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(255, 255, 255, 255 - projectile.alpha);
        }
    }
}