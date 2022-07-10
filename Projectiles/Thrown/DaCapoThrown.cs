using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Projectiles.Thrown
{
    public class DaCapoThrown : ModProjectile
	{
        public override string Texture => "SinsMod/Items/Weapons/MultiType/DaCapo";
        public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Da Capo");
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 8;
            ProjectileID.Sets.TrailingMode[projectile.type] = 2;
        }
		public override void SetDefaults()
		{
            projectile.width = 30;
			projectile.height = 30;
            projectile.thrown = true;
            projectile.penetrate = -1;
            projectile.timeLeft = 180;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
            projectile.extraUpdates = 1;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 2;
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D texture = Main.projectileTexture[projectile.type];
            SpriteEffects spriteEffects = projectile.direction == -1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            int num = Main.projectileTexture[projectile.type].Height / Main.projFrames[projectile.type];
            int num2 = num * projectile.frame;
            Rectangle rectangle = new Rectangle(0, num2, texture.Width, num);
            Vector2 origin = rectangle.Size() / 2f;
            Color color = lightColor;
            color = projectile.GetAlpha(color);
            for (int i = 0; i < ProjectileID.Sets.TrailCacheLength[projectile.type]; i += 2)
            {
                Color color2 = color;
                color2 *= (float)(ProjectileID.Sets.TrailCacheLength[projectile.type] - i) / ProjectileID.Sets.TrailCacheLength[projectile.type];
                Vector2 value = projectile.oldPos[i];
                float num3 = projectile.oldRot[i];
                Main.spriteBatch.Draw(texture, value + projectile.Size / 2f - Main.screenPosition + new Vector2(0, projectile.gfxOffY), new Microsoft.Xna.Framework.Rectangle?(rectangle), color2, num3, origin, projectile.scale, spriteEffects, 0f);
            }
            Main.spriteBatch.Draw(texture, projectile.Center - Main.screenPosition + new Vector2(0f, projectile.gfxOffY), new Microsoft.Xna.Framework.Rectangle?(rectangle), projectile.GetAlpha(lightColor), projectile.rotation, origin, projectile.scale, spriteEffects, 0f);
            return false;
        }
        public override void AI()
        {
            projectile.rotation += 0.5f * projectile.direction;
            Vector2 vector = new Vector2(6f, -10f);
            float num = projectile.rotation;
            if (projectile.direction == -1)
            {
                vector.X = -10f;
            }
            vector = vector.RotatedBy(num, default(Vector2));
            for (int num2 = 0; num2 < 1; num2++)
            {
                int num3 = Dust.NewDust(projectile.Center + vector - Vector2.One * 5f, 0, 0, 92, 0f, 0f, 0, default(Color), 1f);
                Main.dust[num3].noGravity = true;
                Main.dust[num3].velocity = Main.dust[num3].velocity * 0.25f + Vector2.Normalize(vector) * 1f;
                Main.dust[num3].velocity = Main.dust[num3].velocity.RotatedBy(-1.57079637f * projectile.direction, default(Vector2));
            }
            Vector2 vector2 = new Vector2(-6f, 10f);
            float num4 = projectile.rotation;
            if (projectile.direction == -1)
            {
                vector.X = -10f;
            }
            vector = vector.RotatedBy(num, default(Vector2));
            for (int num5 = 0; num5 < 1; num5++)
            {
                int num6 = Dust.NewDust(projectile.Center + vector - Vector2.One * 5f, 0, 0, 92, 0f, 0f, 0, default(Color), 1f);
                Main.dust[num6].noGravity = true;
                Main.dust[num6].velocity = Main.dust[num6].velocity * 0.25f + Vector2.Normalize(vector) * 1f;
                Main.dust[num6].velocity = Main.dust[num6].velocity.RotatedBy(-1.57079637f * projectile.direction, default(Vector2));
            }
            /*float num = 30f;
            int num2 = 0;
            Vector2 vector = Vector2.UnitX * 0f;
            vector += -Vector2.UnitY.RotatedBy((double)((float)num2 * (6.28318548f / num)), default(Vector2)) * new Vector2(4f, 12f);
            vector = vector.RotatedBy((double)projectile.velocity.ToRotation(), default(Vector2));
            int num3 = Dust.NewDust(projectile.Center, 0, 0, 92, 0f, 0f, 0, default(Color), 1.0f);
            Main.dust[num3].noGravity = true;
            Main.dust[num3].velocity *= 0;
            num3 = Dust.NewDust(projectile.Center, 0, 0, 92, 0f, 0f, 0, default(Color), 1.0f);
            Main.dust[num3].noGravity = true;
            Main.dust[num3].position = projectile.Center + vector;
            Main.dust[num3].velocity *= 0;
            num3 = Dust.NewDust(projectile.Center, 0, 0, 92, 0f, 0f, 0, default(Color), 1.0f);
            Main.dust[num3].noGravity = true;
            Main.dust[num3].position = projectile.Center - vector;
            Main.dust[num3].velocity *= 0;
            int num4 = num2;
            num2 = num4 + 1;*/
        }
    }
}