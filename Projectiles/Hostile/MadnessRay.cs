using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Enums;
using Terraria.ModLoader;

namespace SinsMod.Projectiles.Hostile
{
    public class MadnessRay : ModProjectile
    {
        private const float maxTime = 180;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Deep Nightmare");
        }
        public override void SetDefaults()
        {
            projectile.width = 48;
            projectile.height = 48;
            projectile.hostile = true;
            projectile.alpha = 255;
            projectile.penetrate = -1;
            projectile.tileCollide = false;
            projectile.timeLeft = 600;
            //cooldownSlot = 1;
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            if (projectile.velocity == Vector2.Zero)
            {
                return false;
            }
            Texture2D texture2D = Main.projectileTexture[projectile.type];
            Texture2D texture = mod.GetTexture("Extra/Projectile/MadnessRayBody");
            Texture2D texture2 = mod.GetTexture("Extra/Projectile/MadnessRayHead");
            float num = projectile.localAI[1];
            Color color = Color.Lerp(Color.Black, SinsColor.DarknessPurple, (float)Math.Cos(6.28318548f * (Main.LocalPlayer.miscCounter / 100f)) * 0.4f + 0.5f) * 0.9f;
            SpriteBatch spriteBatch2 = Main.spriteBatch;
            Texture2D texture2D2 = texture2D;
            Vector2 vector = projectile.Center - Main.screenPosition;
            spriteBatch2.Draw(texture2D2, vector, null, color, projectile.rotation, texture2D.Size() / 2f, projectile.scale, 0, 0f);
            num -= (texture2D.Height / 2 + texture2.Height) * projectile.scale;
            Vector2 vector2 = projectile.Center;
            vector2 += projectile.velocity * projectile.scale * texture2D.Height / 2f;
            if (num > 0f)
            {
                float num2 = 0f;
                Rectangle value = new Rectangle(0, 16 * (projectile.timeLeft / 3 % 5), texture.Width, 16);
                while (num2 + 1f < num)
                {
                    if (num - num2 < value.Height)
                    {
                        value.Height = (int)(num - num2);
                    }
                    Main.spriteBatch.Draw(texture, vector2 - Main.screenPosition, new Rectangle?(value), color, projectile.rotation, new Vector2(value.Width / 2, 0f), projectile.scale, 0, 0f);
                    num2 += value.Height * projectile.scale;
                    vector2 += projectile.velocity * value.Height * projectile.scale;
                    value.Y += 16;
                    if (value.Y + value.Height > texture.Height)
                    {
                        value.Y = 0;
                    }
                }
            }
            SpriteBatch spriteBatch3 = Main.spriteBatch;
            Texture2D texture2D3 = texture2;
            Vector2 vector3 = vector2 - Main.screenPosition;
            spriteBatch3.Draw(texture2D3, vector3, null, color, projectile.rotation, texture2.Frame(1, 1, 0, 0).Top(), projectile.scale, 0, 0f);
            return false;
        }
        public override void AI()
        {
            Vector2? vector = null;
            if (projectile.velocity.HasNaNs() || projectile.velocity == Vector2.Zero)
            {
                projectile.velocity = -Vector2.UnitY;
            }
            if (Main.npc[(int)projectile.ai[1]].active)
            {
                if (Main.npc[(int)projectile.ai[1]].type == mod.NPCType("BlackCrystalNoMove") || Main.npc[(int)projectile.ai[1]].type == mod.NPCType("BlackCrystal") || Main.npc[(int)projectile.ai[1]].type == mod.NPCType("BlackCrystalCore"))
                {
                    projectile.Center = Main.npc[(int)projectile.ai[1]].Center;
                }
                else
                {
                    Vector2 vector2 = new Vector2(23f, 59f);
                    Vector2 vector3 = new Vector2(Main.npc[(int)projectile.ai[1]].Center.X, Main.npc[(int)projectile.ai[1]].Center.Y - 16f);
                    Vector2 vector4 = Utils.Vector2FromElipse(Main.npc[(int)projectile.ai[1]].localAI[0].ToRotationVector2(), vector2 * Main.npc[(int)projectile.ai[1]].localAI[1]);
                    projectile.position = vector3 + vector4 - new Vector2(projectile.width, projectile.height) / 2f;
                }
            }
            else
            {
                projectile.Kill();
            }
            if (projectile.velocity.HasNaNs() || projectile.velocity == Vector2.Zero)
            {
                projectile.velocity = -Vector2.UnitY;
            }
            float num = 1f;
            projectile.localAI[0] += 1f;
            if (projectile.localAI[0] >= 180f)
            {
                projectile.Kill();
                return;
            }
            projectile.scale = (float)Math.Sin(projectile.localAI[0] * 3.14159274f / 180f) * 10f * num;
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
            Collision.LaserScan(samplingPoint, projectile.velocity, num4 * projectile.scale, 2400f, array);
            float num5 = 0f;
            int num6;
            for (int i = 0; i < array.Length; i = num6 + 1)
            {
                num5 += array[i];
                num6 = i;
            }
            num5 /= num3;
            float num7 = 0.5f;
            projectile.localAI[1] = MathHelper.Lerp(projectile.localAI[1], num5, num7);
            Vector2 vector5 = projectile.Center + projectile.velocity * (projectile.localAI[1] - 14f);
            for (int j = 0; j < 2; j = num6 + 1)
            {
                float num8 = projectile.velocity.ToRotation() + ((Main.rand.Next(2) == 1) ? -1f : 1f) * 1.57079637f;
                float num9 = (float)Main.rand.NextDouble() * 2f + 2f;
                Vector2 vector6 = new Vector2((float)Math.Cos(num8) * num9, (float)Math.Sin(num8) * num9);
                int num10 = Dust.NewDust(vector5, 0, 0, 21, vector6.X, vector6.Y, 0, default(Color), 1f);
                Main.dust[num10].noGravity = true;
                Main.dust[num10].scale = 1.7f;
                num6 = j;
            }
            if (Main.rand.Next(5) == 0)
            {
                Vector2 vector7 = projectile.velocity.RotatedBy(1.5707963705062866, default(Vector2)) * ((float)Main.rand.NextDouble() - 0.5f) * projectile.width;
                int num11 = Dust.NewDust(vector5 + vector7 - Vector2.One * 4f, 8, 8, 21, 0f, 0f, 100, default(Color), 1.5f);
                Dust dust = Main.dust[num11];
                dust.velocity *= 0.5f;
                Main.dust[num11].velocity.Y = -Math.Abs(Main.dust[num11].velocity.Y);
            }
            DelegateMethods.v3_1 = new Vector3(0.3f, 0.65f, 0.7f);
            Utils.PlotTileLine(projectile.Center, projectile.Center + projectile.velocity * projectile.localAI[1], projectile.width * projectile.scale, new Utils.PerLinePoint(DelegateMethods.CastLight));
        }
        public override void CutTiles()
        {
            DelegateMethods.tilecut_0 = TileCuttingContext.AttackProjectile;
            Vector2 velocity = projectile.velocity;
            Utils.PlotTileLine(projectile.Center, projectile.Center + velocity * projectile.localAI[1], projectile.width * projectile.scale, new Utils.PerLinePoint(DelegateMethods.CutTiles));
        }
        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            if (projHitbox.Intersects(targetHitbox))
            {
                return new bool?(true);
            }
            float num = 0f;
            if (Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), projectile.Center, projectile.Center + projectile.velocity * projectile.localAI[1], 22f * projectile.scale, ref num))
            {
                return true;
            }
            return false;
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(mod.BuffType("Nightmare"), 300);
        }
    }
}