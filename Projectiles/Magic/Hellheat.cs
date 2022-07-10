using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Projectiles.Magic
{
    public class Hellheat : ModProjectile
    {
        private int Count;
        private bool hadHoming;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Hellheat");
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 9;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
        }
		public override void SetDefaults()
		{
            projectile.width = 18;
			projectile.height = 18;
            projectile.magic = true;
            projectile.friendly = true;
			projectile.penetrate = 4;
			projectile.timeLeft = 360;
			projectile.ignoreWater = false;
            projectile.tileCollide = false;
            projectile.extraUpdates = 1;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 6;
            projectile.alpha = 255;
            projectile.scale = 1.2f;
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D texture = Main.projectileTexture[projectile.type];
            SpriteEffects spriteEffects = projectile.spriteDirection == -1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            int num = Main.projectileTexture[projectile.type].Height / Main.projFrames[projectile.type];
            int num2 = num * projectile.frame;
            Rectangle rectangle = new Rectangle(0, num2, texture.Width, num);
            Vector2 origin = rectangle.Size() / 2f;
            Color color = lightColor;
            color = projectile.GetAlpha(Color.LightPink);
            for (int i = 0; i < ProjectileID.Sets.TrailCacheLength[projectile.type]; i++)
            {
                Color color2 = color;
                color2 *= (float)(ProjectileID.Sets.TrailCacheLength[projectile.type] - i) / ProjectileID.Sets.TrailCacheLength[projectile.type];
                Vector2 value = projectile.oldPos[i];
                float num3 = projectile.oldRot[i];
                float num4 = (9 - i) / 9f;
                Main.spriteBatch.Draw(texture, value + projectile.Size / 2f - Main.screenPosition + new Vector2(0, projectile.gfxOffY), rectangle, color2, projectile.rotation, origin, projectile.scale * num4, spriteEffects, 0f);
            }
            Main.spriteBatch.Draw(texture, projectile.Center - Main.screenPosition + new Vector2(0f, projectile.gfxOffY), rectangle, projectile.GetAlpha(Color.White), projectile.rotation, origin, projectile.scale, spriteEffects, 0f);
            Main.spriteBatch.Draw(texture, projectile.Center - Main.screenPosition + new Vector2(0f, projectile.gfxOffY), rectangle, projectile.GetAlpha(Color.LightPink * 0.4f), projectile.rotation, origin, projectile.scale * 1.3f, spriteEffects, 0f);
            return false;
        }
        public override void AI()
        {
            projectile.rotation = (float)Math.Atan2(projectile.velocity.Y, projectile.velocity.X) + 1.57f;
            Count++;
            float num = projectile.localAI[1];
            projectile.localAI[1] = num + 1f;
            if (projectile.localAI[1] > 10f && Main.rand.Next(3) == 0)
            {
                int num2 = 6;
                int num3;
                for (int num4 = 0; num4 < num2; num4 = num3 + 1)
                {
                    Vector2 vector = Vector2.Normalize(projectile.velocity) * new Vector2(projectile.width, projectile.height) / 2f;
                    vector = vector.RotatedBy((num4 - (num2 / 2 - 1)) * 3.1415926535897931 / (float)num2, default(Vector2)) + projectile.Center;
                    Vector2 vector2 = ((float)(Main.rand.NextDouble() * 3.1415927410125732) - 1.57079637f).ToRotationVector2() * Main.rand.Next(3, 8);
                    /*int num5 = Dust.NewDust(vector + vector2, 0, 0, 60, vector2.X * 2f, vector2.Y * 2f, 100, default(Color), 1.4f);
                    Main.dust[num5].noGravity = true;
                    Main.dust[num5].noLight = true;
                    Dust dust = Main.dust[num5];
                    dust.velocity /= 4f;
                    dust = Main.dust[num5];
                    dust.velocity -= projectile.velocity;*/
                    num3 = num4;
                }
                projectile.alpha -= 60;//5
                if (projectile.alpha < 0)
                {
                    projectile.alpha = 0;
                }
                //projectile.rotation += projectile.velocity.X * 0.1f;
                Lighting.AddLight((int)projectile.Center.X / 16, (int)projectile.Center.Y / 16, 0.6f, 0.2f, 0.2f);
            }
            if (Count < 30)
            {
                if (Count > 10 && (projectile.ai[1] == 1 || projectile.ai[1] == 2))
                {
                    projectile.velocity.Y += projectile.ai[1] == 1 ? 0.2f : -0.2f;
                }
                return;
            }
            int num6 = -1;
            if (!hadHoming && (projectile.ai[1] == 1 || projectile.ai[1] == 2))
            {
                projectile.velocity.Y += projectile.ai[1] == 1 ? 0.2f : -0.2f;
            }
            Vector2 vector3 = projectile.Center;
            float num7 = 600f;
            if (projectile.localAI[0] > 0f)
            {
                num = projectile.localAI[0];
                projectile.localAI[0] = num - 1f;
            }
            if (projectile.ai[0] == 0f && projectile.localAI[0] == 0f)
            {
                int num8;
                for (int num9 = 0; num9 < Main.npc.Length; num9 = num8 + 1)
                {
                    NPC nPC = Main.npc[num9];
                    if (nPC.CanBeChasedBy(projectile, false) && (projectile.ai[0] == 0f || projectile.ai[0] == (num9 + 1)))
                    {
                        Vector2 center = nPC.Center;
                        float num10 = Vector2.Distance(center, vector3);
                        if (num10 < num7 && Collision.CanHit(projectile.position, projectile.width, projectile.height, nPC.position, nPC.width, nPC.height))
                        {
                            num7 = num10;
                            vector3 = center;
                            num6 = num9;
                            hadHoming = true;
                        }
                    }
                    num8 = num9;
                }
                if (num6 >= 0)
                {
                    projectile.ai[0] = num6 + 1;
                    projectile.netUpdate = true;
                }
            }
            if (projectile.localAI[0] == 0f && projectile.ai[0] == 0f)
            {
                projectile.localAI[0] = 30f;
            }
            bool flag = false;
            if (projectile.ai[0] != 0f)
            {
                int num11 = (int)(projectile.ai[0] - 1f);
                if (Main.npc[num11].active && !Main.npc[num11].dontTakeDamage && Main.npc[num11].immune[projectile.owner] == 0)
                {
                    float num12 = Main.npc[num11].position.X + (Main.npc[num11].width / 2);
                    float num13 = Main.npc[num11].position.Y + (Main.npc[num11].height / 2);
                    float num14 = Math.Abs(projectile.position.X + (projectile.width / 2) - num12) + Math.Abs(projectile.position.Y + (projectile.height / 2) - num13);
                    if (num14 < 1000f)
                    {
                        flag = true;
                        vector3 = Main.npc[num11].Center;
                    }
                }
                else
                {
                    projectile.ai[0] = 0f;
                    flag = false;
                    projectile.netUpdate = true;
                }
            }
            if (flag)
            {
                Vector2 vector4 = vector3 - projectile.Center;
                float num15 = projectile.velocity.ToRotation();
                float num16 = vector4.ToRotation();
                double num17 = num16 - num15;
                if (num17 > 3.1415926535897931)
                {
                    num17 -= 6.2831853071795862;
                }
                if (num17 < -3.1415926535897931)
                {
                    num17 += 6.2831853071795862;
                }
                projectile.velocity = projectile.velocity.RotatedBy(num17 * 0.10000000149011612, default(Vector2));
            }
            float num18 = projectile.velocity.Length();
            projectile.velocity.Normalize();
            projectile.velocity *= num18 + 0.0025f;
        }
        public override void Kill(int timeleft)
		{
            projectile.penetrate = -1;
            Main.PlaySound(SoundID.Zombie, (int)projectile.Center.X, (int)projectile.Center.Y, 103, 1f, 0f);
            projectile.position = projectile.Center;
            projectile.width = projectile.height = 200;
            projectile.position.X = projectile.position.X - (projectile.width / 2);
            projectile.position.Y = projectile.position.Y - (projectile.height / 2);
            for (int i = 0; i < 3; i++)
            {
                Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 182, 0f, 0f, 100, default(Color), 1.2f);
            }
            for (int j = 0; j < 20; j++)
            {
                int num = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 182, 0f, 0f, 0, default(Color), 2.0f);
                Main.dust[num].noGravity = true;
                Main.dust[num].velocity *= 3f;
                num = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 60, 0f, 0f, 100, default(Color), 1.5f);
                Main.dust[num].velocity *= 2f;
                Main.dust[num].noGravity = true;
            }
            projectile.Damage();
        }
    }
}