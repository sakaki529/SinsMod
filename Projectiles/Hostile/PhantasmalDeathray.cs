using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Projectiles.Hostile
{
    public class PhantasmalDeathray : ModProjectile
    {
        private const float maxTime = 180;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Phantasmal Deathray");
        }
        public override void SetDefaults()
        {
            projectile.width = 48;
            projectile.height = 48;
            projectile.friendly = false;
            projectile.hostile = true;
            projectile.alpha = 255;
            projectile.penetrate = -1;
            projectile.tileCollide = false;
            projectile.timeLeft = 600;
            projectile.hide = true;
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            if (projectile.velocity == Vector2.Zero)
            {
                return false;
            }
            Texture2D texture = Main.projectileTexture[projectile.type];
            Texture2D textureBody = mod.GetTexture("Extra/Projectile/PhantasmalDeathrayBody");
            Texture2D textureEnd = mod.GetTexture("Extra/Projectile/PhantasmalDeathrayHead");
            float num = projectile.localAI[1];
            Color color = new Color(255, 255, 255, 0) * 0.9f;
            SpriteBatch spriteBatch2 = Main.spriteBatch;
            Texture2D texture2D = texture;
            Vector2 vector = projectile.Center - Main.screenPosition;
            Rectangle? sourceRectangle = null;
            spriteBatch2.Draw(texture2D, vector, sourceRectangle, color, projectile.rotation, texture.Size() / 2f, projectile.scale, SpriteEffects.None, 0f);
            num -= (texture.Height / 2 + textureEnd.Height) * projectile.scale;
            Vector2 value = projectile.Center;
            value += projectile.velocity * projectile.scale * texture.Height / 2f;
            if (num > 0f)
            {
                float num2 = 0f;
                Rectangle rectangle = new Rectangle(0, 16 * (projectile.timeLeft / 3 % 5), textureBody.Width, 16);
                while (num2 + 1f < num)
                {
                    if (num - num2 < rectangle.Height)
                    {
                        rectangle.Height = (int)(num - num2);
                    }
                    Main.spriteBatch.Draw(textureBody, value - Main.screenPosition, new Microsoft.Xna.Framework.Rectangle?(rectangle), color, projectile.rotation, new Vector2(rectangle.Width / 2, 0f), projectile.scale, SpriteEffects.None, 0f);
                    num2 += rectangle.Height * projectile.scale;
                    value += projectile.velocity * rectangle.Height * projectile.scale;
                    rectangle.Y += 16;
                    if (rectangle.Y + rectangle.Height > textureBody.Height)
                    {
                        rectangle.Y = 0;
                    }
                }
            }
            SpriteBatch spriteBatch3 = Main.spriteBatch;
            Texture2D texture2D_2 = textureEnd;
            Vector2 vector2_3 = value - Main.screenPosition;
            sourceRectangle = null;
            spriteBatch3.Draw(texture2D_2, vector2_3, sourceRectangle, color, projectile.rotation, textureEnd.Frame(1, 1, 0, 0).Top(), projectile.scale, SpriteEffects.None, 0f);
            return false;
        }
        public override void AI()
        {
            projectile.hide = false;
            Vector2? vector = null;
            if (projectile.velocity.HasNaNs() || projectile.velocity == Vector2.Zero)
            {
                projectile.velocity = -Vector2.UnitY;
            }
            int ai1 = (int)projectile.ai[1];
            if (Main.npc[ai1].active)
            {
                if (Main.npc[ai1].type == mod.NPCType("LunarEye") || Main.npc[ai1].type == mod.NPCType("LunarEyeSmall"))
                {
                    Vector2 value23 = new Vector2(30f, 30f);
                    Vector2 value24 = Utils.Vector2FromElipse(Main.npc[ai1].localAI[0].ToRotationVector2(), value23 * Main.npc[ai1].localAI[1]);
                    projectile.position = Main.npc[ai1].Center + value24 - new Vector2(projectile.width, projectile.height) / 2f;
                }
                else
                {
                    projectile.Center = Main.npc[ai1].Center - Vector2.UnitY * 6f;
                    projectile.Kill();
                    return;
                }
            }
            else
            {
                projectile.Kill();
                return;
            }
            if (projectile.velocity.HasNaNs() || projectile.velocity == Vector2.Zero)
            {
                projectile.velocity = -Vector2.UnitY;
            }
            if (projectile.localAI[0] == 0f)
            {
                Main.PlaySound(SoundID.Zombie, (int)projectile.position.X, (int)projectile.position.Y, 104, 1f, 0f);
            }
            float num = 0.4f;
            projectile.localAI[0] += 1f;
            if (projectile.localAI[0] >= maxTime)
            {
                projectile.Kill();
                return;
            }
            projectile.scale = (float)Math.Sin(projectile.localAI[0] * 3.14159274f / maxTime) * 10f * num;
            if (projectile.scale > num)
            {
                projectile.scale = num;
            }
            float num2 = projectile.velocity.ToRotation();
            num2 += projectile.ai[0];
            projectile.rotation = num2 - 1.57079637f;
            projectile.velocity = num2.ToRotationVector2();
            float num3 = 3f;
            float num4 = projectile.width;
            Vector2 samplingPoint = projectile.Center;
            if (vector.HasValue)
            {
                samplingPoint = vector.Value;
            }
            float[] array = new float[(int)num3];
            Collision.LaserScan(samplingPoint, projectile.velocity, num4 * projectile.scale, 3000f, array);
            float num5 = 0f;
            int num6;
            for (int num7 = 0; num7 < array.Length; num7 = num6 + 1)
            {
                num5 += array[num7];
                num6 = num7;
            }
            num5 /= num3;
            float amount = 0.5f;
            projectile.localAI[1] = MathHelper.Lerp(projectile.localAI[1], num5, amount);
            Vector2 vector2 = projectile.Center + projectile.velocity * (projectile.localAI[1] - 14f);
            for (int num8 = 0; num8 < 2; num8 = num6 + 1)
            {
                float num9 = projectile.velocity.ToRotation() + ((Main.rand.Next(2) == 1) ? -1f : 1f) * 1.57079637f;
                float num10 = (float)Main.rand.NextDouble() * 2f + 2f;
                Vector2 vector2_3 = new Vector2((float)Math.Cos(num9) * num10, (float)Math.Sin(num9) * num10);
                int num11 = Dust.NewDust(vector2, 0, 0, 229, vector2_3.X, vector2_3.Y, 0, default(Color), 1f);
                Main.dust[num11].noGravity = true;
                Main.dust[num11].scale = 0.7f;
                num6 = num8;
            }
            if (Main.rand.Next(5) == 0)
            {
                Vector2 value = projectile.velocity.RotatedBy(1.5707963705062866, default(Vector2)) * ((float)Main.rand.NextDouble() - 0.5f) * projectile.width;
                int num12 = Dust.NewDust(vector2 + value - Vector2.One * 4f, 8, 8, 229, 0f, 0f, 100, default(Color), 1.5f);
                Dust dust = Main.dust[num12];
                dust.velocity *= 0.5f;
                Main.dust[num12].velocity.Y = -Math.Abs(Main.dust[num12].velocity.Y);
                Main.dust[num12].noGravity = true;
                Main.dust[num12].scale = 0.7f;
            }
            DelegateMethods.v3_1 = new Vector3(0.3f, 0.65f, 0.7f);
            Utils.PlotTileLine(projectile.Center, projectile.Center + projectile.velocity * projectile.localAI[1], projectile.width * projectile.scale, new Utils.PerLinePoint(DelegateMethods.CastLight));
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(BuffID.MoonLeech, 420);
        }
        public override void CutTiles()
        {
            DelegateMethods.tilecut_0 = TileCuttingContext.AttackProjectile;
            Vector2 unit = projectile.velocity;
            Utils.PlotTileLine(projectile.Center, projectile.Center + unit * projectile.localAI[1], projectile.width * projectile.scale, new Utils.PerLinePoint(DelegateMethods.CutTiles));
        }
        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            if (projHitbox.Intersects(targetHitbox))
            {
                return true;
            }
            float num6 = 0f;
            if (Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), projectile.Center, projectile.Center + projectile.velocity * projectile.localAI[1], 22f * projectile.scale, ref num6))
            {
                return true;
            }
            return false;
        }
    }
}