using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace SinsMod.Projectiles.Magic
{
    public class StormOfEibon : ModProjectile
    {
        public override string Texture => "SinsMod/Projectiles/Magic/Storm";
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Storm of Knowledge");
        }
		public override void SetDefaults()
		{
            projectile.width = 10;
            projectile.height = 10;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.magic = true;
            projectile.penetrate = -1;
            projectile.timeLeft = 60;
            projectile.tileCollide = false;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 5;
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            float num = 60f;//timeleft
            float num2 = 15f;//size
            float num3 = 15f;
            float num4 = projectile.ai[0];
            float num5 = MathHelper.Clamp(num4 / 30f, 0f, 1f);
            if (num4 > num - 60f)
            {
                num5 = MathHelper.Lerp(1f, 0f, (num4 - (num - 60f)) / 60f);
            }
            Point point = projectile.Center.ToTileCoordinates();
            int num6;
            int num7;
            Collision.ExpandVertically(point.X, point.Y, out num6, out num7, (int)num2, (int)num3);
            num6++;
            num7--;
            float num8 = 0.2f;
            Vector2 vector = new Vector2(point.X, num6) * 16f + new Vector2(8f);
            Vector2 vector2 = new Vector2(point.X, num7) * 16f + new Vector2(8f);
            Vector2.Lerp(vector, vector2, 0.5f);
            Vector2 vector3 = new Vector2(0f, vector2.Y - vector.Y);
            vector3.X = vector3.Y * num8;
            new Vector2(vector.X - vector3.X / 2f, vector.Y);
            Texture2D texture2D = Main.projectileTexture[projectile.type];
            Rectangle rectangle = texture2D.Frame(1, 1, 0, 0);
            Vector2 vector4 = rectangle.Size() / 2f;
            float num9 = -0.06283186f * num4;
            Vector2 spinningpoint = Vector2.UnitY.RotatedBy(num4 * 0.1f, default(Vector2));
            float num10 = 0f;
            float num11 = 5.1f;
            Color color = new Color(Main.DiscoR + 255, Main.DiscoR, 0);
            for (float num12 = (float)((int)vector2.Y); num12 > (float)((int)vector.Y); num12 -= num11)
            {
                num10 += num11;
                float num13 = num10 / vector3.Y;
                float num14 = num10 * 6.28318548f / -20f;
                float num15 = num13 - 0.15f;
                Vector2 vector5 = spinningpoint.RotatedBy(num14, default(Vector2));
                Vector2 vector6 = new Vector2(0f, num13 + 1f);
                vector6.X = vector6.Y * num8;
                Color color2 = Color.Lerp(Color.Transparent, color, num13 * 2f);
                if (num13 > 0.5f)
                {
                    color2 = Color.Lerp(Color.Transparent, color, 2f - num13 * 2f);
                }
                byte A = (byte)(color2.A * 0.5f);
                color2 *= num5;
                vector5 *= vector6 * 100f;
                vector5.Y = 0f;
                vector5.X = 0f;
                vector5 += new Vector2(vector2.X, num12) - Main.screenPosition;
                Main.spriteBatch.Draw(texture2D, vector5, new Rectangle?(rectangle), color2, num9 + num14, vector4, 1f + num15, 0, 0f);
            }
            return false;
        }
        public override void AI()
        {
            float num = 900f;
            if (projectile.soundDelay == 0)
            {
                projectile.soundDelay = -1;
                Main.PlaySound(2, projectile.Center, 122);
            }
            projectile.ai[0] += 1f;
            if (projectile.ai[0] >= num)
            {
                projectile.Kill();
            }
            if (projectile.localAI[0] >= 30f)
            {
                projectile.damage = 0;
                if (projectile.ai[0] < num - 120f)
                {
                    float num2 = projectile.ai[0] % 60f;
                    projectile.ai[0] = num - 120f + num2;
                    projectile.netUpdate = true;
                }
            }
            float num3 = 15f;//size?
            float num4 = 15f;
            Point point = projectile.Center.ToTileCoordinates();
            int num5;
            int num6;
            Collision.ExpandVertically(point.X, point.Y, out num5, out num6, (int)num3, (int)num4);
            num5++;
            num6--;
            Vector2 vector = new Vector2(point.X, num5) * 16f + new Vector2(8f);
            Vector2 vector2 = new Vector2(point.X, num6) * 16f + new Vector2(8f);
            Vector2 center = Vector2.Lerp(vector, vector2, 0.5f);
            Vector2 vector3 = new Vector2(0f, vector2.Y - vector.Y);
            vector3.X = vector3.Y * 0.2f;
            projectile.width = (int)(vector3.X * 0.65f);
            projectile.height = (int)vector3.Y;
            projectile.Center = center;
            if (projectile.owner == Main.myPlayer)
            {
                bool flag = false;
                Vector2 center2 = Main.player[projectile.owner].Center;
                Vector2 top = Main.player[projectile.owner].Top;
                for (float num7 = 0f; num7 < 1f; num7 += 0.05f)
                {
                    Vector2 position = Vector2.Lerp(vector, vector2, num7);
                    if (Collision.CanHitLine(position, 0, 0, center2, 0, 0) || Collision.CanHitLine(position, 0, 0, top, 0, 0))
                    {
                        flag = true;
                        break;
                    }
                    flag = true;
                }
                if (!flag && projectile.ai[0] < num - 120f)
                {
                    float num8 = projectile.ai[0] % 60f;
                    projectile.ai[0] = num - 120f + num8;
                    projectile.netUpdate = true;
                }
            }
            float arg_2E9_0 = projectile.ai[0];
            float arg_2E8_0 = num - 120f;
        }
    }
}