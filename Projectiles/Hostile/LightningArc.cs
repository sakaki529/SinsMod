using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace SinsMod.Projectiles.Hostile
{
    public class LightningArc : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Lightning");
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 20;
            ProjectileID.Sets.TrailingMode[projectile.type] = 1;
        }
        public override void SetDefaults()
        {
            projectile.width = 20;
            projectile.height = 20;
            projectile.scale = 1f;
            projectile.aiStyle = -1;
            projectile.friendly = true;
            projectile.minion = true;
            projectile.alpha = 100;
            projectile.ignoreWater = true;
            projectile.tileCollide = true;
            projectile.extraUpdates = 4;
            projectile.timeLeft = 120 * (projectile.extraUpdates + 1);
            projectile.penetrate = -1;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 10;
        }
        public override bool PreDraw(SpriteBatch sb, Color lightColor)
        {
            Color color = Lighting.GetColor((int)(projectile.position.X + projectile.width * 0.5) / 16, (int)((projectile.position.Y + projectile.height * 0.5) / 16.0));
            Vector2 end = projectile.position + new Vector2(projectile.width, projectile.height) / 2f + Vector2.UnitY * projectile.gfxOffY - Main.screenPosition;
            Texture2D tex = Main.extraTexture[33];
            projectile.GetAlpha(color);
            Vector2 scale = new Vector2(projectile.scale) / 2f;
            for (int i = 0; i < 3; i++)
            {
                if (projectile.localAI[1] != -1f)
                {
                    float arg_11B_0 = projectile.localAI[1];
                }
                if (i == 0)
                {
                    scale = new Vector2(projectile.scale) * 0.6f;
                    DelegateMethods.c_1 = new Color(115, 204, 219, 0) * 0.5f;
                }
                else
                {
                    if (i == 1)
                    {
                        scale = new Vector2(projectile.scale) * 0.4f;
                        DelegateMethods.c_1 = new Color(113, 251, 255, 0) * 0.5f;
                    }
                    else
                    {
                        scale = new Vector2(projectile.scale) * 0.2f;
                        DelegateMethods.c_1 = new Color(255, 255, 255, 0) * 0.5f;
                    }
                }
                DelegateMethods.f_1 = 1f;
                for (int j = projectile.oldPos.Length - 1; j > 0; j--)
                {
                    if (!(projectile.oldPos[j] == Vector2.Zero))
                    {
                        Vector2 start = projectile.oldPos[j] + new Vector2(projectile.width, projectile.height) / 2f + Vector2.UnitY * projectile.gfxOffY - Main.screenPosition;
                        Vector2 end2 = projectile.oldPos[j - 1] + new Vector2(projectile.width, projectile.height) / 2f + Vector2.UnitY * projectile.gfxOffY - Main.screenPosition;
                        Utils.DrawLaser(Main.spriteBatch, tex, start, end2, scale, new Utils.LaserLineFraming(DelegateMethods.LightningLaserDraw));
                    }
                }
                if (projectile.oldPos[0] != Vector2.Zero)
                {
                    Vector2 start2 = projectile.oldPos[0] + new Vector2(projectile.width, projectile.height) / 2f + Vector2.UnitY * projectile.gfxOffY - Main.screenPosition;
                    Utils.DrawLaser(Main.spriteBatch, tex, start2, end, scale, new Utils.LaserLineFraming(DelegateMethods.LightningLaserDraw));
                }
            }
            return false;
        }
        public override void AI()
        {
            int num = projectile.frameCounter;
            projectile.frameCounter = num + 1;
            Lighting.AddLight(projectile.Center, 0.3f, 0.45f, 0.5f);
            if (projectile.velocity == Vector2.Zero)
            {
                if (projectile.frameCounter >= projectile.extraUpdates * 2)
                {
                    projectile.frameCounter = 0;
                    bool flag = true;
                    for (int i = 1; i < projectile.oldPos.Length; i = num + 1)
                    {
                        if (projectile.oldPos[i] != projectile.oldPos[0])
                        {
                            flag = false;
                        }
                        num = i;
                    }
                    if (flag)
                    {
                        projectile.Kill();
                        return;
                    }
                }
                if (Main.rand.Next(projectile.extraUpdates) == 0)
                {
                    for (int j = 0; j < 2; j = num + 1)
                    {
                        float num2 = projectile.rotation + ((Main.rand.Next(2) == 1) ? -1f : 1f) * 1.57079637f;
                        float num3 = (float)Main.rand.NextDouble() * 0.8f + 1f;
                        Vector2 vector = new Vector2((float)Math.Cos(num2) * num3, (float)Math.Sin(num2) * num3);
                        int num4 = Dust.NewDust(projectile.Center, 0, 0, 226, vector.X, vector.Y, 0, default(Color), 1f);
                        Main.dust[num4].noGravity = true;
                        Main.dust[num4].scale = 1.2f;
                        num = j;
                    }
                    if (Main.rand.Next(5) == 0)
                    {
                        Vector2 vector2 = projectile.velocity.RotatedBy(1.5707963705062866, default(Vector2)) * ((float)Main.rand.NextDouble() - 0.5f) * projectile.width;
                        int num5 = Dust.NewDust(projectile.Center + vector2 - Vector2.One * 4f, 8, 8, 31, 0f, 0f, 100, default(Color), 1.5f);
                        Dust dust = Main.dust[num5];
                        dust.velocity *= 0.5f;
                        Main.dust[num5].velocity.Y = -Math.Abs(Main.dust[num5].velocity.Y);
                        return;
                    }
                }
            }
            else
            {
                if (projectile.frameCounter >= projectile.extraUpdates * 2)
                {
                    projectile.frameCounter = 0;
                    float num6 = projectile.velocity.Length();
                    UnifiedRandom unifiedRandom = new UnifiedRandom((int)projectile.ai[1]);
                    int num7 = 0;
                    Vector2 spinningpoint = -Vector2.UnitY;
                    Vector2 vector3;
                    do
                    {
                        int num8 = unifiedRandom.Next();
                        projectile.ai[1] = num8;
                        num8 %= 100;
                        float f = num8 / 100f * 6.28318548f;
                        vector3 = f.ToRotationVector2();
                        if (vector3.Y > 0f)
                        {
                            vector3.Y *= -1f;
                        }
                        bool flag2 = false;
                        if (vector3.Y > -0.02f)
                        {
                            flag2 = true;
                        }
                        if (vector3.X * (projectile.extraUpdates + 1) * 2f * num6 + projectile.localAI[0] > 40f)
                        {
                            flag2 = true;
                        }
                        if (vector3.X * (projectile.extraUpdates + 1) * 2f * num6 + projectile.localAI[0] < -40f)
                        {
                            flag2 = true;
                        }
                        if (!flag2)
                        {
                            goto IL_1;
                        }
                        num = num7;
                        num7 = num + 1;
                    }
                    while (num < 100);
                    projectile.velocity = Vector2.Zero;
                    projectile.localAI[1] = 1f;
                    goto IL_2;
                IL_1:
                    spinningpoint = vector3;
                IL_2:
                    if (projectile.velocity != Vector2.Zero)
                    {
                        projectile.localAI[0] += spinningpoint.X * (projectile.extraUpdates + 1) * 2f * num6;
                        projectile.velocity = spinningpoint.RotatedBy(projectile.ai[0] + 1.57079637f, default(Vector2)) * num6;
                        projectile.rotation = projectile.velocity.ToRotation() + 1.57079637f;
                    }
                }
            }
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.Electrified, 240);
            /*if (projectile.localAI[1] < 1f)
            {
                projectile.localAI[1] += 2f;
                projectile.position += projectile.velocity;
                projectile.velocity = Vector2.Zero;
            }
            projectile.damage = 0;
            projectile.velocity *= 0f;*/
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(BuffID.Electrified, 240, false);
            /*if (projectile.localAI[1] < 1f)
            {
                projectile.localAI[1] += 2f;
                projectile.position += projectile.velocity;
                projectile.velocity = Vector2.Zero;
            }
            projectile.damage = 0;
            projectile.velocity *= 0f;*/
        }
        public override void OnHitPvp(Player target, int damage, bool crit)
        {
            target.AddBuff(BuffID.Electrified, 240, false);
            /*if (projectile.localAI[1] < 1f)
            {
                projectile.localAI[1] += 2f;
                projectile.position += projectile.velocity;
                projectile.velocity = Vector2.Zero;
            }
            projectile.damage = 0;
            projectile.velocity *= 0f;*/
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (projectile.localAI[1] < 1f)
            {
                projectile.localAI[1] += 2f;
                projectile.position += projectile.velocity;
                projectile.velocity = Vector2.Zero;
            }
            return false;
        }
        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            int num = 0;
            while (num < projectile.oldPos.Length && (projectile.oldPos[num].X != 0f || projectile.oldPos[num].Y != 0f))
            {
                projHitbox.X = (int)projectile.oldPos[num].X;
                projHitbox.Y = (int)projectile.oldPos[num].Y;
                if (projHitbox.Intersects(targetHitbox))
                {
                    return true;
                }
                num++;
            }
            return false;
        }
        public override void Kill(int timeLeft)
        {
            float num2 = (float)(projectile.rotation + 1.57079637050629 + (Main.rand.Next(2) == 1 ? -1.0 : 1.0) * 1.57079637050629);
            float num3 = (float)(Main.rand.NextDouble() * 2.0 + 2.0);
            Vector2 vector2 = new Vector2((float)Math.Cos(num2) * num3, (float)Math.Sin(num2) * num3);
            for (int i = 0; i < projectile.oldPos.Length; i++)
            {
                int index = Dust.NewDust(projectile.oldPos[i], 0, 0, 229, vector2.X, vector2.Y, 0, new Color(), 1f);
                Main.dust[index].noGravity = true;
                Main.dust[index].scale = 1.7f;
            }
        }
        /*public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            for (int index = 0; index < projectile.oldPos.Length && (projectile.oldPos[index].X != 0.0 || projectile.oldPos[index].Y != 0.0); ++index)
            {
                Rectangle myRect = projHitbox;
                myRect.X = (int)projectile.oldPos[index].X;
                myRect.Y = (int)projectile.oldPos[index].Y;
                if (myRect.Intersects(targetHitbox))
                {
                    return true;
                }
            }
            return false;
        }*/
        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(255, 255, 255, 0) * (1f - projectile.alpha / 255f);
        }
    }
}