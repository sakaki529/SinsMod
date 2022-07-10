using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Projectiles.Hostile
{
    public class AbyssalSpirit : ModProjectile
    {
        private float oldPosY;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Abyssal Flame");
            Main.projFrames[projectile.type] = 4;
        }
        public override void SetDefaults()
        {
            projectile.width = 8;
            projectile.height = 8;
            projectile.friendly = false;
            projectile.hostile = true;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
            projectile.timeLeft = 6000;
            projectile.penetrate = -1;
            projectile.alpha = 255;
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D texture = projectile.ai[0] < 0 ? mod.GetTexture("Extra/Projectile/AbyssalSpirit_Alt") : Main.projectileTexture[projectile.type];
            int num = Main.projectileTexture[projectile.type].Height / Main.projFrames[projectile.type];
            int num2 = num * projectile.frame;
            Rectangle rectangle = new Rectangle(0, num2, texture.Width, num);
            Vector2 origin = rectangle.Size() / 2f;
            Color color = lightColor;
            color = projectile.GetAlpha(color);
            Main.spriteBatch.Draw(texture, projectile.Center - Main.screenPosition + new Vector2(0f, projectile.gfxOffY), rectangle, projectile.GetAlpha(lightColor), projectile.rotation, origin, projectile.scale, SpriteEffects.None, 0f);
            return false;
        }
        public override void AI()
        {
            int dustType = projectile.ai[0] < 0 ? 226 : 272;
            if (projectile.timeLeft > 30 && projectile.alpha > 0)
            {
                projectile.alpha -= 25;
            }
            /*if (projectile.timeLeft > 30 && projectile.alpha < 128 && Collision.SolidCollision(projectile.position, projectile.width, projectile.height))
            {
                projectile.alpha = 128;
            }*/
            if (projectile.alpha < 0)
            {
                projectile.alpha = 0;
            }
            /*int num = projectile.frameCounter + 1;
            projectile.frameCounter = num;
            if (num > 4)
            {
                projectile.frameCounter = 0;
                num = projectile.frame + 1;
                projectile.frame = num;
                if (num >= 4)
                {
                    projectile.frame = 0;
                }
            }
            float num2 = 0.5f;
            if (projectile.timeLeft < 120)
            {
                num2 = 1.1f;
            }
            if (projectile.timeLeft < 60)
            {
                num2 = 1.6f;
            }
            float num3 = projectile.localAI[0];
            projectile.localAI[0] = num3 + 1f;
            float num4 = projectile.localAI[0] / 180f * 6.28318548f;
            for (float num5 = 0f; num5 < 3f; num5 = num3 + 1f)
            {
                if (Main.rand.Next(3) != 0)
                {
                    return;
                }
                Dust dust = Main.dust[Dust.NewDust(projectile.Center, 0, 0, dustType, 0f, -2f, 0, default(Color), 1f)];
                dust.position = projectile.Center + Vector2.UnitY.RotatedBy(num5 * 6.28318548f / 3f + projectile.localAI[0], default(Vector2)) * 10f;
                dust.noGravity = true;
                dust.velocity = projectile.DirectionFrom(dust.position);
                dust.scale = num2;
                dust.fadeIn = 0.5f;
                dust.alpha = 200;
                num3 = num5;
            }
            if (projectile.timeLeft < 4)
            {
                int num6 = 40;
                if (Main.expertMode)
                {
                    num6 = 30;
                }
                projectile.position = projectile.Center;
                projectile.width = projectile.height = 60;
                projectile.Center = projectile.position;
                projectile.damage = num6;
                for (int num7 = 0; num7 < 10; num7 = num + 1)
                {
                    Dust dust2 = Main.dust[Dust.NewDust(projectile.position, projectile.width, projectile.height, Utils.SelectRandom(Main.rand, new int[]
                    {
                        dustType,
                        6
                    }), 0f, -2f, 0, default(Color), 1f)];
                    dust2.noGravity = true;
                    if (dust2.position != projectile.Center)
                    {
                        dust2.velocity = projectile.DirectionTo(dust2.position) * 3f;
                    }
                    num = num7;
                }
            }*/
            /*if (base.get_projectile().ai[1] == 0f)
			{
				base.get_projectile().ai[1] = 1f;
				Main.PlaySound(2, (int)base.get_projectile().position.X, (int)base.get_projectile().position.Y, 92, 1f, 0f);
			}*/
            if (projectile.ai[0] < 0)
            {
                float num = 120f;
                float scaleFactor = 20f;
                float num2 = 40f;
                int num3 = (int)projectile.ai[0];
                if (num3 >= 0 && Main.player[num3].active && !Main.player[num3].dead)
                {
                    if (projectile.Distance(Main.player[num3].Center) > num2)
                    {
                        Vector2 vector = projectile.DirectionTo(Main.player[num3].Center);
                        if (Utils.HasNaNs(vector))
                        {
                            vector = Vector2.UnitY;
                        }
                        projectile.velocity = (projectile.velocity * (num - 1f) + vector * scaleFactor) / num;
                        return;
                    }
                }
                else
                {
                    if (projectile.timeLeft > 30)
                    {
                        projectile.timeLeft = 30;
                    }
                    if (projectile.ai[0] != -1f)
                    {
                        projectile.ai[0] = -1f;
                        projectile.netUpdate = true;
                    }
                }
            }
            else
            {
                projectile.localAI[1] += 1f;
                if (projectile.localAI[1] >= 90f)
                {
                    projectile.velocity *= 0.85f;
                    return;
                }
                int num = Main.rand.Next(2);
                projectile.velocity *= 1.05f;
                if (num == 0 && (projectile.velocity.X >= 25f || projectile.velocity.Y >= 25f))
                {
                    projectile.velocity.X = 0f;
                    projectile.velocity.Y = 10f;
                    return;
                }
                if (num == 1 && (projectile.velocity.X >= 25f || projectile.velocity.Y >= 25f))
                {
                    projectile.velocity.X = 10f;
                    projectile.velocity.Y = 0f;
                    return;
                }
                if (num == 0 && (projectile.velocity.X <= -25f || projectile.velocity.Y <= -25f))
                {
                    projectile.velocity.X = 0f;
                    projectile.velocity.Y = -10f;
                    return;
                }
                if (num == 1 && (projectile.velocity.X <= -25f || projectile.velocity.Y <= -25f))
                {
                    projectile.velocity.X = -10f;
                    projectile.velocity.Y = 0f;
                }
            }
            /*if (oldPosY == 0)
            {
                oldPosY = projectile.Center.Y;
                projectile.localAI[0] = 90;
            }
            else
            {
                projectile.localAI[0]++;
                if (projectile.localAI[0] > 0)
                {
                    projectile.velocity.Y += 0.16f;
                }
                else
                {
                    projectile.velocity.Y -= 0.16f;
                }
            }
            Player player = Main.player[(int)projectile.ai[1]];
            if (player.active && !player.dead)
            {
                if (projectile.Center.X < player.Center.X)
                {
                    projectile.velocity.X += 0.08f;
                    if (projectile.velocity.X > 16f)
                    {
                        projectile.velocity.X = 16f;
                    }
                }
                if (projectile.Center.X > player.Center.X)
                {
                    projectile.velocity.X -= 0.08f;
                    if (projectile.velocity.X < -16f)
                    {
                        projectile.velocity.X = -16f;
                    }
                }
            }
            else
            {
                projectile.ai[1] = Player.FindClosest(projectile.Center, 0, 0);
            }*/
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(projectile.ai[0] < 0 ? BuffID.Frostburn : mod.BuffType("AbyssalFlame"), 180);
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(projectile.ai[0] < 0 ? BuffID.Frostburn : mod.BuffType("AbyssalFlame"), 180);
        }
        public override void OnHitPvp(Player target, int damage, bool crit)
        {
            target.AddBuff(projectile.ai[0] < 0 ? BuffID.Frostburn : mod.BuffType("AbyssalFlame"), 180);
        }
        public override void Kill(int timeLeft)
        {
            int dustType = projectile.ai[0] < 0 ? 226 : 272;
            projectile.position = projectile.Center;
            projectile.width = projectile.height = 60;
            projectile.Center = projectile.position;
            projectile.Damage();
            Main.PlaySound(SoundID.Item14, projectile.position);
            int num;
            for (int num2 = 0; num2 < 4; num2 = num + 1)
            {
                int num3 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 31, 0f, 0f, 100, default(Color), 1.5f);
                Main.dust[num3].position = projectile.Center + Vector2.UnitY.RotatedByRandom(3.1415927410125732) * (float)Main.rand.NextDouble() * projectile.width / 2f;
                num = num2;
            }
            for (int num4 = 0; num4 < 20; num4 = num + 1)
            {
                int num5 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, dustType, 0f, 0f, 0, default(Color), 2.5f);
                Main.dust[num5].position = projectile.Center + Vector2.UnitY.RotatedByRandom(3.1415927410125732) * (float)Main.rand.NextDouble() * projectile.width / 2f;
                Main.dust[num5].noGravity = true;
                Dust dust = Main.dust[num5];
                dust.velocity *= 2f;
                num = num4;
            }
            for (int num6 = 0; num6 < 10; num6 = num + 1)
            {
                int num7 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 31, 0f, 0f, 0, default(Color), 1.5f);
                Main.dust[num7].position = projectile.Center + Vector2.UnitX.RotatedByRandom(3.1415927410125732).RotatedBy(projectile.velocity.ToRotation(), default(Vector2)) * (float)projectile.width / 2f;
                Main.dust[num7].noGravity = true;
                Dust dust = Main.dust[num7];
                dust.velocity *= 2f;
                num = num6;
            }
        }
    }
}