using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Projectiles.Magic
{
    public class Avalanche : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Avalanche");
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 7;
            ProjectileID.Sets.TrailingMode[projectile.type] = 2;
        }
        public override void SetDefaults()
        {
            projectile.CloneDefaults(ProjectileID.SnowBallFriendly);
            aiType = ProjectileID.SnowBallFriendly;
            projectile.ranged = false;
            projectile.magic = true;
            projectile.tileCollide = false;
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D texture = Main.projectileTexture[projectile.type];
            int num = Main.projectileTexture[projectile.type].Height / Main.projFrames[projectile.type];
            int num2 = num * projectile.frame;
            Rectangle rectangle = new Rectangle(0, num2, texture.Width, num);
            Vector2 origin = rectangle.Size() / 2f;
            Color color = Color.White;
            color = projectile.GetAlpha(color);
            for (int i = 0; i < ProjectileID.Sets.TrailCacheLength[projectile.type]; i++)
            {
                Color color2 = color;
                color2 *= (float)(ProjectileID.Sets.TrailCacheLength[projectile.type] - i) / ProjectileID.Sets.TrailCacheLength[projectile.type];
                Vector2 value = projectile.oldPos[i];
                float num3 = projectile.oldRot[i];
                float num4 = (4 - i) / 4f;
                Main.spriteBatch.Draw(texture, value + projectile.Size / 2f - Main.screenPosition + new Vector2(0, projectile.gfxOffY), new Microsoft.Xna.Framework.Rectangle?(rectangle), color2, num3, origin, projectile.scale * num4, SpriteEffects.None, 0f);
            }
            Main.spriteBatch.Draw(texture, projectile.Center - Main.screenPosition + new Vector2(0f, projectile.gfxOffY), new Microsoft.Xna.Framework.Rectangle?(rectangle), projectile.GetAlpha(Color.White), projectile.rotation, origin, projectile.scale, SpriteEffects.None, 0f);
            return false;
        }
        public override void AI()
        {
            if (projectile.Center.Y > (int)projectile.ai[1])
            {
                projectile.tileCollide = true;
            }
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            projectile.type = ProjectileID.SnowBallFriendly;
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            projectile.type = ProjectileID.SnowBallFriendly;
        }
        public override void OnHitPvp(Player target, int damage, bool crit)
        {
            projectile.type = ProjectileID.SnowBallFriendly;
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            projectile.type = ProjectileID.SnowBallFriendly;
            return base.OnTileCollide(oldVelocity);
        }
        public override void Kill(int timeLeft)
        {
            projectile.type = ProjectileID.SnowBallFriendly;
            base.Kill(timeLeft);
        }
    }
}