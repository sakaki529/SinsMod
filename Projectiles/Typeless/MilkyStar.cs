using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Projectiles.Typeless
{
    public class MilkyStar : ModProjectile
	{
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Star");
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 20;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
        }
		public override void SetDefaults()
		{
			projectile.width = 30;
			projectile.height = 30;
			projectile.minion = true;
			projectile.minionSlots = 0f;
			projectile.friendly = true;
			projectile.tileCollide = false;
			projectile.ignoreWater = true;
            projectile.timeLeft = 1200;
            projectile.penetrate = 1;
            projectile.netUpdate = true;
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            SpriteEffects spriteEffects = SpriteEffects.None;
            if (projectile.spriteDirection == 1)
            {
                spriteEffects = SpriteEffects.FlipHorizontally;
            }
            Vector2 vector = new Vector2(Main.projectileTexture[projectile.type].Width / 2, Main.projectileTexture[projectile.type].Height / Main.projFrames[projectile.type] / 2);
            Texture2D texture = Main.projectileTexture[projectile.type];
            Vector2 vector2 = projectile.Center - Main.screenPosition;
            vector2 -= new Vector2(texture.Width, texture.Height / Main.projFrames[projectile.type]) * projectile.scale / 2f;
            vector2 += vector * projectile.scale + new Vector2(0f, projectile.gfxOffY);
            float num = 1f / projectile.oldPos.Length * 1.1f;
            int num2 = projectile.oldPos.Length - 1;
            while (num2 >= 0)
            {
                float num3 = (float)(projectile.oldPos.Length - num2) / projectile.oldPos.Length;
                Color color = Color.White;
                color *= 1f - num * num2 / 1f;
                color.A = (byte)(color.A * (1f - num3));
                Main.spriteBatch.Draw(texture, vector2 + projectile.oldPos[num2] - projectile.position, null, projectile.GetAlpha(color), projectile.oldRot[num2], vector, projectile.scale * MathHelper.Lerp(0.8f, 0.3f, num3), spriteEffects, 0f);
                num2--;
            }
            texture = mod.GetTexture("Extra/Projectile/MilkyStar_Extra");
            Main.spriteBatch.Draw(texture, vector2, null, projectile.GetAlpha(Color.White), 0f, Utils.Size(texture) / 2f, projectile.scale, spriteEffects, 0f);
            return false;
        }
        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            SinsPlayer sinsPlayer = player.GetModPlayer<SinsPlayer>();
            float distance = Math.Abs(projectile.Center.X - player.Center.X) + Math.Abs(projectile.Center.Y - player.Center.Y);
            if (player.dead || !player.active || !sinsPlayer.setMilkyWay || distance > 1600)
            {
                projectile.Kill();
            }
            if (projectile.localAI[0] == 0f)
            {
                projectile.localAI[0] = 1f;
                for (int i = 0; i < 13; i++)
                {
                    int num = Dust.NewDust(projectile.position, projectile.width, projectile.height, 261, projectile.velocity.X * 0.5f, projectile.velocity.Y * 0.5f, 90, default(Color), 2.5f);
                    Main.dust[num].noGravity = true;
                    Main.dust[num].fadeIn = 1f;
                    Main.dust[num].velocity *= 4f;
                    Main.dust[num].noLight = true;
                }
            }
            for (int j = 0; j < 2; j++)
            {
                if (Main.rand.Next(10 - (int)Math.Min(7f, projectile.velocity.Length())) < 1)
                {
                    int num2 = Dust.NewDust(projectile.position, projectile.width, projectile.height, 261, projectile.velocity.X * 0.5f, projectile.velocity.Y * 0.5f, 90, default(Color), 2.5f);
                    Main.dust[num2].noGravity = true;
                    Main.dust[num2].velocity *= 0.2f;
                    Main.dust[num2].fadeIn = 0.4f;
                    if (Main.rand.Next(6) == 0)
                    {
                        Main.dust[num2].velocity *= 5f;
                        Main.dust[num2].noLight = true;
                    }
                    else
                    {
                        Main.dust[num2].velocity = projectile.DirectionFrom(Main.dust[num2].position) * Main.dust[num2].velocity.Length();
                    }
                }
            }
            //Lighting.AddLight(projectile.Center, 0.2f, 0.4f, 0.6f);
            Lighting.AddLight(projectile.Center, 0.6f, 0.6f, 0.6f);
            float centerY = projectile.Center.X;
            float centerX = projectile.Center.Y;
            float dist = 600f;
            bool flag = false;
            for (int k = 0; k < 200; k++)
            {
                if (Main.npc[k].CanBeChasedBy(projectile, false)/* && Collision.CanHit(projectile.Center, 1, 1, Main.npc[k].Center, 1, 1)*/)
                {
                    float num3 = Main.npc[k].position.X + (Main.npc[k].width / 2);
                    float num4 = Main.npc[k].position.Y + (Main.npc[k].height / 2);
                    float num5 = Math.Abs(projectile.position.X + (projectile.width / 2) - num3) + Math.Abs(projectile.position.Y + (projectile.height / 2) - num4);
                    if (num5 < dist)
                    {
                        dist = num5;
                        centerY = num3;
                        centerX = num4;
                        flag = true;
                    }
                }
            }
            if (flag)
            {
                float num6 = 8f;
                Vector2 vector = new Vector2(projectile.position.X + projectile.width * 0.5f, projectile.position.Y + projectile.height * 0.5f);
                float num7 = centerY - vector.X;
                float num8 = centerX - vector.Y;
                float num9 = (float)Math.Sqrt(num7 * num7 + num8 * num8);
                num9 = num6 / num9;
                num7 *= num9;
                num8 *= num9;
                projectile.velocity.X = (projectile.velocity.X * 20f + num7) / 21f;
                projectile.velocity.Y = (projectile.velocity.Y * 20f + num8) / 21f;
            }
            else
            {
                float num = 2f;
                int num2 = 64;
                projectile.localAI[1] += 0.05f;
                double distAdd = Math.Sin(projectile.localAI[1]);
                double rad = projectile.ai[0] * 0.017453292519943295;
                float targetX = player.Center.X - (float)(Math.Cos(rad) * (num2 + distAdd * 20.0));
                float targetY = player.Center.Y - (float)(Math.Sin(rad) * (num2 + distAdd * 20.0));
                if (projectile.ai[1] == 1)
                {
                    targetX = player.Center.X + (float)(Math.Cos(rad) * (num2 + distAdd * 20.0));
                    targetY = player.Center.Y + (float)(Math.Sin(rad) * (num2 + distAdd * 20.0));
                }
                if (projectile.ai[1] == 2)
                {
                    targetX = player.Center.X + (float)(Math.Cos(rad) * (num2 + distAdd * 20.0));
                    targetY = player.Center.Y - (float)(Math.Sin(rad) * (num2 + distAdd * 20.0));
                }
                if (projectile.ai[1] == 3)
                {
                    targetX = player.Center.X - (float)(Math.Cos(rad) * (num2 + distAdd * 20.0));
                    targetY = player.Center.Y + (float)(Math.Sin(rad) * (num2 + distAdd * 20.0));
                }
                Vector2 toTarget = new Vector2(targetX - projectile.Center.X, targetY - projectile.Center.Y);
                toTarget.Normalize();
                projectile.velocity += toTarget * num / 8f;
                if (Vector2.Distance(projectile.Center, player.Center) >= num2 + 64)
                {
                    projectile.velocity *= 0.98f;
                }
            }
            projectile.timeLeft = 1200;
        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            crit = false;
        }
        public override void ModifyHitPlayer(Player target, ref int damage, ref bool crit)
        {
            crit = false;
        }
        public override void ModifyHitPvp(Player target, ref int damage, ref bool crit)
        {
            crit = false;
        }
        public override void Kill(int timeLeft)
        {
            Vector2 spinningpoint = Utils.RotatedByRandom(new Vector2(0f, -3f), 3.1415927410125732);
            float num = 24f;
            Vector2 value = new Vector2(1.05f, 1f);
            float num2;
            for (float num3 = 0f; num3 < num; num3 = num2 + 1f)
            {
                int num4 = Dust.NewDust(projectile.Center, 0, 0, 66, 0f, 0f, 0, Color.Transparent, 1f);
                Main.dust[num4].position = projectile.Center;
                Main.dust[num4].velocity = Utils.RotatedBy(spinningpoint, 6.28318548f * num3 / num, default(Vector2)) * value * (0.8f + Utils.NextFloat(Main.rand) * 0.4f) * 2f;
                //Main.dust[num4].color = Color.SkyBlue;
                Main.dust[num4].color = Color.White;
                Main.dust[num4].noGravity = true;
                Main.dust[num4].scale += 0.5f + Utils.NextFloat(Main.rand);
                num2 = num3;
            }
        }
    }
}