using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Projectiles.Summon
{
    public class PhantasmDragonHead : ModProjectile
	{
        private bool chase;
        private bool near;
        private int spawn = 3;
        private int cantDetectTail;
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Phantasm Dragon");
            Main.projFrames[projectile.type] = 3;
            ProjectileID.Sets.MinionSacrificable[projectile.type] = true;
            ProjectileID.Sets.MinionTargettingFeature[projectile.type] = true;
        }
		public override void SetDefaults()
		{
            projectile.width = 30;
            projectile.height = 30;
            projectile.minion = true;
            projectile.penetrate = -1;
            projectile.timeLeft = 18000;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.netUpdate = true;
            projectile.netImportant = true;
            projectile.alpha = 255;
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D texture = Main.projectileTexture[projectile.type];
            int num = Main.projectileTexture[projectile.type].Height / Main.projFrames[projectile.type];
            int num2 = num * projectile.frame;
            Main.spriteBatch.Draw(texture, projectile.Center - Main.screenPosition + new Vector2(0f, projectile.gfxOffY), new Rectangle?(new Rectangle(0, num2, texture.Width, num)), projectile.GetAlpha(lightColor), projectile.rotation, new Vector2(texture.Width / 2f, num / 2f), projectile.scale, (projectile.spriteDirection == 1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
            return false;
        }
        public override void AI()
        {
            if (near)
            {
                projectile.frame = 2;
            }
            else if (chase)
            {
                projectile.frame = 1;
            }
            else
            {
                projectile.frame = 0;
            }
            if (spawn > 0)
            {
                for (int i = 0; i < 10; i++)
                {
                    int d = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width * 2, projectile.height * 2, 228, 0f, 0f, 50, default(Color), 2.0f);
                    Main.dust[d].velocity *= 4f;
                    Main.dust[d].noGravity = true;
                    Main.dust[d].fadeIn = 1f + Main.rand.Next(10) * 0.1f;
                }
                spawn--;
            }
            Player player = Main.player[projectile.owner];
            SinsPlayer modPlayer = player.GetModPlayer<SinsPlayer>();
            player.AddBuff(mod.BuffType("PhantasmDragonMinion"), 3600, true);
            if (Main.time % 120 == 0)
            {
                projectile.netUpdate = true;
            }
            if (player.dead)
            {
                modPlayer.PhantasmDragonMinion = false;
            }
            if (!player.active)
            {
                projectile.active = false;
            }
            if (modPlayer.PhantasmDragonMinion)
            {
                projectile.timeLeft = 2;
            }

            Vector2 center = player.Center;
            int num3 = 30;
            float num4 = 900f;
            float num5 = 1200f;
            int num6 = -1;
            if (projectile.Distance(center) > 2400f)
            {
                projectile.Center = center;
                projectile.netUpdate = true;
            }
            NPC ownerTarget = projectile.OwnerMinionAttackTargetNPC;
            if (ownerTarget != null && ownerTarget.CanBeChasedBy(projectile))
            {
                for (int i = 0; i < 200; i++)
                {
                    NPC nPC = Main.npc[i];
                    if (ownerTarget.CanBeChasedBy(projectile, false) && player.Distance(ownerTarget.Center) < num5)
                    {
                        float num7 = projectile.Distance(ownerTarget.Center);
                        if (num7 < num4)
                        {
                            if (nPC == ownerTarget)
                            {
                                num6 = i;
                            }
                        }
                    }
                }
            }
            else for (int j = 0; j < 200; j++)
            {
                NPC nPC = Main.npc[j];
                if (nPC.CanBeChasedBy(projectile, false) && player.Distance(nPC.Center) < num5)
                {
                    float num7 = projectile.Distance(nPC.Center);
                    if (num7 < num4)
                    {
                        num6 = j;
                    }
                }
            }
            if (num6 != -1)
            {
                NPC nPC = Main.npc[num6];
                Vector2 vector = nPC.Center - projectile.Center;
                (vector.X > 0f).ToDirectionInt();
                (vector.Y > 0f).ToDirectionInt();
                float num8 = 0.4f;
                if (vector.Length() < 600f)
                {
                    num8 = 0.6f;
                }
                if (vector.Length() < 300f)
                {
                    num8 = 0.8f;
                    chase = false;
                    near = true;
                }
                else
                {
                    chase = true;
                    near = false;
                }
                if (vector.Length() > nPC.Size.Length() * 0.75f)
                {
                    projectile.velocity += Vector2.Normalize(vector) * num8 * 1.5f;
                    if (Vector2.Dot(projectile.velocity, vector) < 0.25f)
                    {
                        projectile.velocity *= 0.8f;
                    }
                }
                float num9 = 30f;
                if (projectile.velocity.Length() > num9)
                {
                    projectile.velocity = Vector2.Normalize(projectile.velocity) * num9;
                }
            }
            else
            {
                chase = false;
                near = false;
                float num10 = 0.2f;
                Vector2 vector2 = center - projectile.Center;
                if (vector2.Length() < 300f)
                {
                    num10 = 0.12f;
                }
                if (vector2.Length() < 200f)
                {
                    num10 = 0.06f;
                }
                if (vector2.Length() > 100f)
                {
                    if (Math.Abs(center.X - projectile.Center.X) > 20f)
                    {
                        projectile.velocity.X = projectile.velocity.X + num10 * Math.Sign(center.X - projectile.Center.X);
                    }
                    if (Math.Abs(center.Y - projectile.Center.Y) > 10f)
                    {
                        projectile.velocity.Y = projectile.velocity.Y + num10 * Math.Sign(center.Y - projectile.Center.Y);
                    }
                }
                else
                {
                    if (projectile.velocity.Length() > 2f)
                    {
                        projectile.velocity *= 0.96f;
                    }
                }
                if (Math.Abs(projectile.velocity.Y) < 1f)
                {
                    projectile.velocity.Y = projectile.velocity.Y - 0.1f;
                }
                float num11 = 15f;
                if (projectile.velocity.Length() > num11)
                {
                    projectile.velocity = Vector2.Normalize(projectile.velocity) * num11;
                }
            }
            projectile.rotation = projectile.velocity.ToRotation() + 1.57079637f;
            int direction = projectile.direction;
            projectile.direction = projectile.spriteDirection = (projectile.velocity.X > 0f) ? 1 : -1;
            if (direction != projectile.direction)
            {
                projectile.netUpdate = true;
            }
            float num12 = MathHelper.Clamp(projectile.localAI[0], 0f, 50f);
            projectile.position = projectile.Center;
            //projectile.scale = 1f + num12 * 0.01f;
            projectile.scale = 1f;
            projectile.width = projectile.height = (int)(num3 * projectile.scale);
            projectile.Center = projectile.position;
            if (projectile.alpha > 0)
            {
                projectile.alpha -= 42;
                if (projectile.alpha < 0)
                {
                    projectile.alpha = 0;
                }
            }
            if (player.ownedProjectileCounts[mod.ProjectileType("PhantasmDragonTail")] < 1)
            {
                cantDetectTail++;
                if (cantDetectTail > 1)
                {
                    projectile.Kill();
                }
            }
            else
            {
                cantDetectTail = 0;
            }
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.immune[projectile.owner] = 4;
        }
        public override void Kill(int timeLeft)
        {
            projectile.position.X = projectile.position.X + (projectile.width / 2);
            projectile.position.Y = projectile.position.Y + (projectile.height / 2);
            projectile.width = 50;
            projectile.height = 50;
            projectile.position.X = projectile.position.X - (projectile.width / 2);
            projectile.position.Y = projectile.position.Y - (projectile.height / 2);
            for (int i = 0; i < 10; i++)
            {
                int d = Dust.NewDust(projectile.position, projectile.width, projectile.height, 16, 0f, 0f, 0, default(Color), 1.5f);
                Dust dust = Main.dust[d];
                dust.velocity *= 2f;
                Main.dust[d].noGravity = true;
            }
            int num = Main.rand.Next(1, 4);
            Vector2 vector = Vector2.UnitY.RotatedByRandom(6.2831854820251465);
            for (int i = 0; i < num; i++)
            {
                int g = Gore.NewGore(projectile.position - vector * 5f, Vector2.Zero, Main.rand.Next(11, 14), projectile.scale);
                Gore gore = Main.gore[g];
                gore.velocity *= 0.8f;
            }
        }
        public override Color? GetAlpha(Color lightColor)
        {
            int num = lightColor.A - projectile.alpha;
            lightColor = Color.Lerp(lightColor, Color.White, 0.4f);
            lightColor.A = 150;
            lightColor *= num / 255f;
            return lightColor;
        }
    }
}