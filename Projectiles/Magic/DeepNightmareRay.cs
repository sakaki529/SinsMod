using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Enums;
using Terraria.GameContent.Shaders;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;

namespace SinsMod.Projectiles.Magic
{
    public class DeepNightmareRay : ModProjectile
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Deep Nightmare");
        }
        public override void SetDefaults()
        {
            projectile.width = 18;
            projectile.height = 18;
            projectile.friendly = true;
            projectile.magic = true;
            //projectile.penetrate = -1;
            projectile.alpha = 255;
            projectile.tileCollide = false;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 1;
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            if (projectile.velocity == Vector2.Zero)
            {
                return false;
            }
            Texture2D texture = Main.projectileTexture[projectile.type];
            float num = projectile.localAI[1];

            Vector2 value = projectile.Center.Floor();
            value += projectile.velocity * projectile.scale * 10.5f;
            num -= projectile.scale * 14.5f * projectile.scale;
            Vector2 vector = new Vector2(projectile.scale);
            DelegateMethods.f_1 = 0.5f;
            DelegateMethods.c_1 = new Color(20 + (int)(Main.DiscoR * 1.0f), 0, 20 + (int)(Main.DiscoR * 1.0f), 127) * 0.75f * projectile.Opacity; //outside
            //projectile.oldPos[0] + new Vector2((float)projectile.width, (float)projectile.height) / 2f + Vector2.UnitY * projectile.gfxOffY - Main.screenPosition;
            Utils.DrawLaser(Main.spriteBatch, texture, value - Main.screenPosition, value + projectile.velocity * num - Main.screenPosition, vector, new Utils.LaserLineFraming(DelegateMethods.RainbowLaserDraw)); //outside
            DelegateMethods.c_1 = new Color(20 + (int)(Main.DiscoR * 1.0f), 0, 20 + (int)(Main.DiscoR * 1.0f), 127) * 0.75f * projectile.Opacity; //inside
            Utils.DrawLaser(Main.spriteBatch, texture, value - Main.screenPosition, value + projectile.velocity * num - Main.screenPosition, vector / 2f, new Utils.LaserLineFraming(DelegateMethods.RainbowLaserDraw)); //inside
            return false;
        }
        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            Vector2? vector = null;
            if (projectile.velocity.HasNaNs() || projectile.velocity == Vector2.Zero)
            {
                projectile.velocity = -Vector2.UnitY;
            }
            if (!Main.player[projectile.owner].channel)
            {
                projectile.Kill();
            }

            float num = (int)projectile.ai[0] - 2.5f;
            Vector2 value = Vector2.Normalize(Main.projectile[(int)projectile.ai[1]].velocity);
            Projectile proj = Main.projectile[(int)projectile.ai[1]];
            if (!proj.active || proj.type != mod.ProjectileType("DeepNightmare"))
            {
                projectile.Kill();
            }
            float num2 = num * 0.5235988f;
            Vector2 value2 = Vector2.Zero;
            float num3;
            float y;
            float num4;
            float scaleFactor;
            if (proj.ai[0] < 180f)
            {
                num3 = 1f - proj.ai[0] / 180f;
                y = 20f - proj.ai[0] / 180f * 14f;
                if (proj.ai[0] < 120f)
                {
                    num4 = 20f - 4f * (proj.ai[0] / 120f);
                    projectile.Opacity = proj.ai[0] / 120f * 0.4f;
                }
                else
                {
                    num4 = 16f - 10f * ((proj.ai[0] - 120f) / 60f);
                    projectile.Opacity = 0.4f + (proj.ai[0] - 120f) / 60f * 0.6f;
                }
                scaleFactor = -22f + proj.ai[0] / 180f * 20f;
            }
            else
            {
                num3 = 0f;
                num4 = 1.75f;
                y = 6f;
                projectile.Opacity = 1f;
                scaleFactor = -2f;
            }
            float num5 = (proj.ai[0] + num * num4) / (num4 * 6f) * 6.28318548f;
            num2 = Vector2.UnitY.RotatedBy(num5, default(Vector2)).Y * 0.5235988f * num3;
            value2 = (Vector2.UnitY.RotatedBy(num5, default(Vector2)) * new Vector2(4f, y)).RotatedBy(proj.velocity.ToRotation(), default(Vector2));
            projectile.position = proj.Center + value * 16f - projectile.Size / 2f + new Vector2(0f, -Main.projectile[(int)projectile.ai[1]].gfxOffY);
            projectile.position += proj.velocity.ToRotation().ToRotationVector2() * scaleFactor;
            projectile.position += value2;
            projectile.velocity = Vector2.Normalize(proj.velocity).RotatedBy(num2, default(Vector2));
            projectile.scale = 1.4f * (1f - num3);
            projectile.damage = proj.damage;
            if (proj.ai[0] >= 180f)
            {
                projectile.damage *= 5;
                vector = new Vector2?(proj.Center);
            }
            if (!Collision.CanHitLine(Main.player[projectile.owner].Center, 0, 0, proj.Center, 0, 0))
            {
                vector = new Vector2?(Main.player[projectile.owner].Center);
            }
            projectile.friendly = proj.ai[0] > 30f;

            if (projectile.velocity.HasNaNs() || projectile.velocity == Vector2.Zero)
            {
                projectile.velocity = -Vector2.UnitY;
            }

            float num6 = projectile.velocity.ToRotation();

            projectile.rotation = num6 - 1.57079637f;
            projectile.velocity = num6.ToRotationVector2();
            float num7 = 0f;
            float num8 = 0f;
            Vector2 samplingPoint = projectile.Center;
            if (vector.HasValue)
            {
                samplingPoint = vector.Value;
            }
            
            num7 = 2f;
            num8 = 0f;
            
            float[] array = new float[(int)num7];
            Collision.LaserScan(samplingPoint, projectile.velocity, num8 * projectile.scale, 2400f, array);
            float num9 = 0f;
            for (int num10 = 0; num10 < array.Length; num10++)
            {
                num9 += array[num10];
            }
            num9 /= num7;
            float amount = 0.5f;

            projectile.localAI[1] = MathHelper.Lerp(projectile.localAI[1], num9, amount);

            if (Math.Abs(projectile.localAI[1] - num9) < 100f && projectile.scale > 0.15f)
            {
                //float prismHue = projectile.GetPrismHue(projectile.ai[0]);
                //Color color = Main.hslToRgb(prismHue, 1f, 0.5f);
                Color color = new Color(255, 0, 255); //light color
                color.A = 0;
                Vector2 vector2 = projectile.Center + projectile.velocity * (projectile.localAI[1] - 14.5f * projectile.scale);
                float x = Main.rgbToHsl(new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB)).X;
                for (int num11 = 0; num11 < 2; num11++)
                {
                    float num12 = projectile.velocity.ToRotation() + ((Main.rand.Next(2) == 1) ? -1f : 1f) * 1.57079637f;
                    float num13 = (float)Main.rand.NextDouble() * 0.8f + 1f;
                    Vector2 vector3 = new Vector2((float)Math.Cos(num12) * num13, (float)Math.Sin(num12) * num13);
                    int d = Dust.NewDust(vector2, 0, 0, 112, vector3.X, vector3.Y, 0, default(Color), 1f);
                    Main.dust[d].color = color;
                    Main.dust[d].scale = 1.2f;
                    Main.dust[d].shader = GameShaders.Armor.GetSecondaryShader(82, Main.LocalPlayer);
                    if (projectile.scale > 1f)
                    {
                        Main.dust[d].velocity *= projectile.scale;
                        Main.dust[d].scale *= projectile.scale;
                    }
                    Main.dust[d].noGravity = true;
                    if (projectile.scale != 1.4f)
                    {
                        Dust dust = Dust.CloneDust(d);
                        dust.color = Color.White;
                        dust.scale /= 2f;
                    }
                    float hue = (x + Main.rand.NextFloat() * 0.4f) % 1f;
                    Main.dust[d].color = Color.Lerp(color, Main.hslToRgb(hue, 1f, 0.75f), projectile.scale / 1.4f);
                }
                if (Main.rand.Next(5) == 0)
                {
                    /*Vector2 value3 = projectile.velocity.RotatedBy(1.5707963705062866, default(Vector2)) * ((float)Main.rand.NextDouble() - 0.5f) * projectile.width;
                    int d = Dust.NewDust(vector2 + value3 - Vector2.One * 4f, 8, 8, 31, 0f, 0f, 100, default(Color), 1.5f);
                    Main.dust[d].velocity *= 0.5f;
                    Main.dust[d].velocity.Y = -Math.Abs(Main.dust[d].velocity.Y);*/
                }
                DelegateMethods.v3_1 = color.ToVector3() * 0.3f;
                float value4 = 0.1f * (float)Math.Sin(Main.GlobalTime * 20f);
                Vector2 size = new Vector2(projectile.velocity.Length() * projectile.localAI[1], projectile.width * projectile.scale);
                float num14 = projectile.velocity.ToRotation();
                if (Main.netMode != 2)
                {
                    ((WaterShaderData)Filters.Scene["WaterDistortion"].GetShader()).QueueRipple(projectile.position + new Vector2(size.X * 0.5f, 0f).RotatedBy(num14, default(Vector2)), new Color(0.5f, 0.1f * Math.Sign(value4) + 0.5f, 0f, 1f) * Math.Abs(value4), size, RippleShape.Square, num14);
                }
                Utils.PlotTileLine(projectile.Center, projectile.Center + projectile.velocity * projectile.localAI[1], projectile.width * projectile.scale, new Utils.PerLinePoint(DelegateMethods.CastLight));
                return;
            }
        }
        public override void CutTiles()
        {
            DelegateMethods.tilecut_0 = TileCuttingContext.AttackProjectile;
            Utils.PlotTileLine(projectile.Center, projectile.Center + projectile.velocity * projectile.localAI[1], projectile.width * projectile.scale, new Utils.PerLinePoint(DelegateMethods.CutTiles));
        }
        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            float num = 0f;
            if (Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), projectile.Center, projectile.Center + projectile.velocity * projectile.localAI[1], 22f * projectile.scale, ref num))
            {
                return true;
            }
            return false;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (projectile.penetrate == 1)
            {
                projectile.penetrate++;
            }
        }
        public override void OnHitPvp(Player target, int damage, bool crit)
        {
            if (projectile.penetrate == 1)
            {
                projectile.penetrate++;
            }
        }
    }
}