using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;

namespace SinsMod.Projectiles.Summon
{
    public class BlackCrystalExplosion : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Madness");
        }
        public override void SetDefaults()
        {
            projectile.width = 14;
            projectile.height = 14;
            projectile.minion = true;
            projectile.minionSlots = 0f;
            projectile.alpha = 255;
            projectile.penetrate = -1;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
            projectile.timeLeft = 900;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 2;
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Color color = Lighting.GetColor((int)(projectile.position.X + projectile.width * 0.5) / 16, (int)((projectile.position.Y + projectile.height * 0.5) / 16.0));
            Vector2 vector = projectile.position + new Vector2(projectile.width, projectile.height) / 2f + Vector2.UnitY * projectile.gfxOffY - Main.screenPosition;
            Texture2D texture2D = Main.projectileTexture[projectile.type];
            Rectangle rectangle = texture2D.Frame(1, Main.projFrames[projectile.type], 0, projectile.frame);
            Color alpha = projectile.GetAlpha(color);
            Vector2 vector2 = rectangle.Size() / 2f;
            //Color color2 = Main.hslToRgb(projectile.ai[0], 1f, 0.5f).MultiplyRGBA(new Color(255, 255, 255, 0));
            Color color2 = Color.Black;
            Main.spriteBatch.Draw(texture2D, vector, rectangle, color2, projectile.rotation, vector2, projectile.scale * 2f, 0, 0f);
            Main.spriteBatch.Draw(texture2D, vector, rectangle, color2, 0f, vector2, projectile.scale * 2f, 0, 0f);
            if (projectile.ai[1] != -1f && projectile.Opacity > 0.3f)
            {
                Vector2 vector3 = Main.projectile[(int)projectile.ai[1]].Center - projectile.Center;
                if (projectile.ai[0] < 0)
                {
                    vector3 = Main.npc[(int)projectile.ai[1]].Center - projectile.Center;
                }
                Vector2 vector4 = new Vector2(1f, vector3.Length() / texture2D.Height);
                float num = vector3.ToRotation() + 1.57079637f;
                float num2 = MathHelper.Distance(30f, projectile.localAI[1]) / 20f;
                num2 = MathHelper.Clamp(num2, 0f, 1f);
                if (num2 > 0f)
                {
                    Main.spriteBatch.Draw(texture2D, vector + vector3 / 2f, rectangle, color2 * num2, num, vector2, vector4, 0, 0f);
                    Main.spriteBatch.Draw(texture2D, vector + vector3 / 2f, rectangle, alpha * num2, num, vector2, vector4 / 2f, 0, 0f);
                }
            }
            return false;
        }
        public override bool PreAI()
        {
            if (projectile.ai[0] < 0)
            {
                projectile.friendly = false;
            }
            return base.PreAI();
        }
        public override void AI()
        {
            //Color newColor = Main.hslToRgb(projectile.ai[0], 1f, 0.5f);
            Color newColor = Color.Black;
            int num = (int)projectile.ai[1];
            if (projectile.ai[0] < 0)
            {
                if (num < 0 || num >= 1000 || !Main.npc[num].active)
                {
                    projectile.ai[1] = -1f;
                }
                else
                {
                    DelegateMethods.v3_1 = newColor.ToVector3() * 0.5f;
                    Utils.PlotTileLine(projectile.Center, Main.npc[num].Center, 8f, new Utils.PerLinePoint(DelegateMethods.CastLight));
                }
            }
            else
            {
                if (num < 0 || num >= 1000 || (!Main.projectile[num].active && Main.projectile[num].type != mod.ProjectileType("BlackCrystal")))
                {
                    projectile.ai[1] = -1f;
                }
                else
                {
                    DelegateMethods.v3_1 = newColor.ToVector3() * 0.5f;
                    Utils.PlotTileLine(projectile.Center, Main.projectile[num].Center, 8f, new Utils.PerLinePoint(DelegateMethods.CastLight));
                }
            }
            if (projectile.localAI[0] == 0f)
            {
                projectile.localAI[0] = Main.rand.NextFloat() * 0.8f + 0.8f;
                projectile.direction = ((Main.rand.Next(2) > 0) ? 1 : -1);
            }
            projectile.rotation = projectile.localAI[1] / 40f * 6.28318548f * projectile.direction;
            if (projectile.alpha > 0)
            {
                projectile.alpha -= 8;
            }
            if (projectile.alpha < 0)
            {
                projectile.alpha = 0;
            }
            if (projectile.alpha == 0)
            {
                Lighting.AddLight(projectile.Center, newColor.ToVector3() * 0.5f);
            }
            int num2;
            /*for (int i = 0; i < 2; i = num2 + 1)
            {
                if (Main.rand.Next(10) == 0)
                {
                    Vector2 vector = Vector2.UnitY.RotatedBy(i * 3.14159274f, default(Vector2)).RotatedBy(projectile.rotation, default(Vector2));
                    Dust dust = Main.dust[Dust.NewDust(projectile.Center, 0, 0, 112, 0f, 0f, 225, newColor, 1.5f)];
                    dust.noGravity = true;
                    dust.noLight = true;
                    dust.scale = projectile.Opacity * projectile.localAI[0];
                    dust.position = projectile.Center;
                    dust.velocity = vector * 2.5f;
                    dust.shader = GameShaders.Armor.GetSecondaryShader(82, Main.LocalPlayer);
                }
                num2 = i;
            }
            for (int j = 0; j < 2; j = num2 + 1)
            {
                if (Main.rand.Next(10) == 0)
                {
                    Vector2 vector2 = Vector2.UnitY.RotatedBy(j * 3.14159274f, default(Vector2));
                    Dust dust2 = Main.dust[Dust.NewDust(projectile.Center, 0, 0, 112, 0f, 0f, 225, newColor, 1.5f)];
                    dust2.noGravity = true;
                    dust2.noLight = true;
                    dust2.scale = projectile.Opacity * projectile.localAI[0];
                    dust2.position = projectile.Center;
                    dust2.velocity = vector2 * 2.5f;
                    dust2.shader = GameShaders.Armor.GetSecondaryShader(82, Main.LocalPlayer);
                }
                num2 = j;
            }*/
            for (int i = 0; i < 2; i = num2 + 1)
            {
                if (Main.rand.Next(10) == 0)
                {
                    Vector2 vector = Vector2.UnitY.RotatedBy(i * 3.14159274f, default(Vector2)).RotatedBy(projectile.rotation, default(Vector2));
                    Dust dust = Main.dust[Dust.NewDust(projectile.Center, 0, 0, 186, 0f, 0f, 225, newColor, 1.5f)];
                    dust.noGravity = true;
                    dust.noLight = true;
                    dust.scale = projectile.Opacity * projectile.localAI[0];
                    dust.position = projectile.Center;
                    dust.velocity = vector * 2.5f;
                    dust.shader = GameShaders.Armor.GetSecondaryShader(56, Main.LocalPlayer);
                }
                num2 = i;
            }
            for (int j = 0; j < 2; j = num2 + 1)
            {
                if (Main.rand.Next(10) == 0)
                {
                    Vector2 vector2 = Vector2.UnitY.RotatedBy(j * 3.14159274f, default(Vector2));
                    Dust dust2 = Main.dust[Dust.NewDust(projectile.Center, 0, 0, 245, 0f, 0f, 225, newColor, 1.5f)];
                    dust2.noGravity = true;
                    dust2.noLight = true;
                    dust2.scale = projectile.Opacity * projectile.localAI[0];
                    dust2.position = projectile.Center;
                    dust2.velocity = vector2 * 2.5f;
                    dust2.shader = GameShaders.Armor.GetSecondaryShader(56, Main.LocalPlayer);
                }
                num2 = j;
            }
            if (Main.rand.Next(10) == 0)
            {
                float num3 = 1f + Main.rand.NextFloat() * 2f;
                float fadeIn = 1f + Main.rand.NextFloat();
                float scale = 1f + Main.rand.NextFloat();
                Vector2 vector3 = Utils.RandomVector2(Main.rand, -1f, 1f);
                if (vector3 != Vector2.Zero)
                {
                    vector3.Normalize();
                }
                vector3 *= 20f + Main.rand.NextFloat() * 100f;
                Vector2 vector4 = projectile.Center + vector3;
                Point point = vector4.ToTileCoordinates();
                bool flag = true;
                if (!WorldGen.InWorld(point.X, point.Y, 0))
                {
                    flag = false;
                }
                if (flag && WorldGen.SolidTile(point.X, point.Y))
                {
                    flag = false;
                }
                /*if (flag)
                {
                    Dust dust3 = Main.dust[Dust.NewDust(vector4, 0, 0, 112, 0f, 0f, 127, newColor, 1f)];
                    dust3.noGravity = true;
                    dust3.position = vector4;
                    dust3.velocity = -Vector2.UnitY * num3 * (Main.rand.NextFloat() * 0.9f + 1.6f);
                    dust3.fadeIn = fadeIn;
                    dust3.scale = scale;
                    dust3.noLight = true;
                    dust3.shader = GameShaders.Armor.GetSecondaryShader(82, Main.LocalPlayer);
                    Dust dust4 = Dust.CloneDust(dust3);
                    Dust dust5 = dust4;
                    dust5.scale *= 0.65f;
                    dust5 = dust4;
                    dust5.fadeIn *= 0.65f;
                    dust5.shader = GameShaders.Armor.GetSecondaryShader(82, Main.LocalPlayer);
                    dust4.color = new Color(255, 255, 255, 255);
                    dust4.shader = GameShaders.Armor.GetSecondaryShader(82, Main.LocalPlayer);
                }*/
                if (flag)
                {
                    Dust dust3 = Main.dust[Dust.NewDust(vector4, 0, 0, 186, 0f, 0f, 127, newColor, 1f)];
                    dust3.noGravity = true;
                    dust3.position = vector4;
                    dust3.velocity = -Vector2.UnitY * num3 * (Main.rand.NextFloat() * 0.9f + 1.6f);
                    dust3.fadeIn = fadeIn;
                    dust3.scale = scale;
                    dust3.noLight = true;
                    dust3.shader = GameShaders.Armor.GetSecondaryShader(56, Main.LocalPlayer);
                    Dust dust4 = Dust.CloneDust(dust3);
                    Dust dust5 = dust4;
                    dust5.scale *= 0.65f;
                    dust5 = dust4;
                    dust5.fadeIn *= 0.65f;
                    dust5.shader = GameShaders.Armor.GetSecondaryShader(56, Main.LocalPlayer);
                    dust4.color = new Color(255, 255, 255, 255);
                    dust4.shader = GameShaders.Armor.GetSecondaryShader(56, Main.LocalPlayer);
                }
            }
            projectile.scale = projectile.Opacity / 2f * projectile.localAI[0];
            projectile.velocity = Vector2.Zero;
            float[] localAI = projectile.localAI;
            int num4 = 1;
            float num5 = localAI[num4];
            localAI[num4] = num5 + 1f;
            if (projectile.localAI[1] >= 60f)
            {
                projectile.Kill();
            }
        }
        public override void Kill(int timeLeft)
        {
            if (projectile.ai[0] < 0)
            {
                projectile.hostile = true;
            }
            Vector2 spinningpoint = new Vector2(0f, -3f).RotatedByRandom(3.1415927410125732);
            float num = Main.rand.Next(7, 13);
            Vector2 vector = new Vector2(2.1f, 2f);
            //Color newColor = Main.hslToRgb(projectile.ai[0], 1f, 0.5f);
            float num4;
            /*for (float num2 = 0f; num2 < num; num2 = num4 + 1f)
            {
                int num3 = Dust.NewDust(projectile.Center, 0, 0, 112, 0f, 0f, 0, default(Color), 1f);
                Main.dust[num3].position = projectile.Center;
                Main.dust[num3].velocity = spinningpoint.RotatedBy((6.28318548f * num2 / num), default(Vector2)) * vector * (0.8f + Main.rand.NextFloat() * 0.4f);
                Main.dust[num3].noGravity = true;
                Main.dust[num3].scale = 2f;
                Main.dust[num3].fadeIn = Main.rand.NextFloat() * 2f;
                Main.dust[num3].shader = GameShaders.Armor.GetSecondaryShader(82, Main.LocalPlayer);
                Dust dust = Dust.CloneDust(num3);
                Dust dust2 = dust;
                dust2.scale /= 2f;
                dust2 = dust;
                dust2.fadeIn /= 2f;
                dust.color = new Color(255, 255, 255, 255);
                num4 = num2;
            }
            for (float num5 = 0f; num5 < num; num5 = num4 + 1f)
            {
                int num6 = Dust.NewDust(projectile.Center, 0, 0, 112, 0f, 0f, 0, default(Color), 1f);
                Main.dust[num6].position = projectile.Center;
                Main.dust[num6].velocity = spinningpoint.RotatedBy(6.28318548f * num5 / num, default(Vector2)) * vector * (0.8f + Main.rand.NextFloat() * 0.4f);
                Main.dust[num6].shader = GameShaders.Armor.GetSecondaryShader(82, Main.LocalPlayer);
                Dust dust3 = Main.dust[num6];
                dust3.velocity *= Main.rand.NextFloat() * 0.8f;
                Main.dust[num6].noGravity = true;
                Main.dust[num6].scale = Main.rand.NextFloat() * 1f;
                Main.dust[num6].fadeIn = Main.rand.NextFloat() * 2f;
                Dust dust4 = Dust.CloneDust(num6);
                dust3 = dust4;
                dust3.scale /= 2f;
                dust3 = dust4;
                dust3.fadeIn /= 2f;
                dust4.color = new Color(255, 255, 255, 255);
                num4 = num5;
            }*/
            for (float num2 = 0f; num2 < num; num2 = num4 + 1f)
            {
                int num3 = Dust.NewDust(projectile.Center, 0, 0, 186, 0f, 0f, 0, default(Color), 1f);
                Main.dust[num3].position = projectile.Center;
                Main.dust[num3].velocity = spinningpoint.RotatedBy((6.28318548f * num2 / num), default(Vector2)) * vector * (0.8f + Main.rand.NextFloat() * 0.4f);
                Main.dust[num3].noGravity = true;
                Main.dust[num3].scale = 2f;
                Main.dust[num3].fadeIn = Main.rand.NextFloat() * 2f;
                Main.dust[num3].shader = GameShaders.Armor.GetSecondaryShader(56, Main.LocalPlayer);
                Dust dust = Dust.CloneDust(num3);
                Dust dust2 = dust;
                dust2.scale /= 2f;
                dust2 = dust;
                dust2.fadeIn /= 2f;
                dust.color = new Color(255, 255, 255, 255);
                num4 = num2;
            }
            for (float num5 = 0f; num5 < num; num5 = num4 + 1f)
            {
                int num6 = Dust.NewDust(projectile.Center, 0, 0, 245, 0f, 0f, 0, default(Color), 1f);
                Main.dust[num6].position = projectile.Center;
                Main.dust[num6].velocity = spinningpoint.RotatedBy(6.28318548f * num5 / num, default(Vector2)) * vector * (0.8f + Main.rand.NextFloat() * 0.4f);
                Main.dust[num6].shader = GameShaders.Armor.GetSecondaryShader(56, Main.LocalPlayer);
                Dust dust3 = Main.dust[num6];
                dust3.velocity *= Main.rand.NextFloat() * 0.8f;
                Main.dust[num6].noGravity = true;
                Main.dust[num6].scale = Main.rand.NextFloat() * 1f;
                Main.dust[num6].fadeIn = Main.rand.NextFloat() * 2f;
                Dust dust4 = Dust.CloneDust(num6);
                dust3 = dust4;
                dust3.scale /= 2f;
                dust3 = dust4;
                dust3.fadeIn /= 2f;
                dust4.color = new Color(255, 255, 255, 255);
                num4 = num5;
            }
            if (Main.myPlayer == projectile.owner)
            {
                //projectile.friendly = true;
                int width = projectile.width;
                int height = projectile.height;
                int penetrate = projectile.penetrate;
                projectile.position = projectile.Center;
                projectile.width = projectile.height = 60;
                projectile.Center = projectile.position;
                projectile.penetrate = -1;
                projectile.maxPenetrate = -1;
                //projectile.Damage();
                if (projectile.hostile)
                {
                    projectile.Damage();
                }
                projectile.penetrate = penetrate;
                projectile.position = projectile.Center;
                projectile.width = width;
                projectile.height = height;
                projectile.Center = projectile.position;
            }
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(255 - projectile.alpha, 255 - projectile.alpha, 255 - projectile.alpha, 0);
        }
    }
}