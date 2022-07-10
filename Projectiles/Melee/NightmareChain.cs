using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Enums;
using Terraria.Graphics.Shaders;

namespace SinsMod.Projectiles.Melee
{
    public class NightmareChain : ModProjectile
	{
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Nightmare Chain");
		}
		public override void SetDefaults()
		{
            projectile.width = 16;
            projectile.height = 16;
            projectile.melee = true;
            projectile.friendly = true;
            projectile.alpha = 255;
            //projectile.penetrate = -1;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
            projectile.hide = true;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 1;
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Vector2 mountedCenter = Main.player[projectile.owner].MountedCenter;
            Color color = Lighting.GetColor((int)(projectile.position.X + projectile.width * 0.5) / 16, (int)((projectile.position.Y + projectile.height * 0.5) / 16.0));
            if (projectile.hide && !ProjectileID.Sets.DontAttachHideToAlpha[projectile.type])
            {
                color = Lighting.GetColor((int)mountedCenter.X / 16, (int)(mountedCenter.Y / 16f));
            }
            Vector2 projPos = projectile.position;
            projPos = (new Vector2(projectile.width, projectile.height) / 2f) + (Vector2.UnitY * projectile.gfxOffY) - Main.screenPosition;
            Texture2D texture2D = Main.projectileTexture[projectile.type];
            Color alpha = projectile.GetAlpha(color);
            if (projectile.velocity == Vector2.Zero)
            {
                return false;
            }
            float num = projectile.velocity.Length() + 16f;
            bool flag = num < 100f;
            Vector2 vector = Vector2.Normalize(projectile.velocity);
            Rectangle rectangle = new Rectangle(0, 0, texture2D.Width, 40);//Grip
            Vector2 vector2 = new Vector2(0f, Main.player[projectile.owner].gfxOffY);
            float num2 = projectile.rotation + 3.14159274f;
            Main.spriteBatch.Draw(texture2D, projectile.Center.Floor() - Main.screenPosition + vector2, new Rectangle?(rectangle), Color.White, num2, rectangle.Size() / 2f - Vector2.UnitY * 4f, projectile.scale, 0, 0f);
            num -= 40f * projectile.scale;
            Vector2 vector3 = projectile.Center.Floor();
            vector3 += vector * projectile.scale * 24f;
            rectangle = new Rectangle(0, 66, texture2D.Width, 18);//Middle by Head
            if (num > 0f)
            {
                float num3 = 0f;
                while (num3 + 1f < num)
                {
                    if (num - num3 < rectangle.Height)
                    {
                        rectangle.Height = (int)(num - num3);
                    }
                    Main.spriteBatch.Draw(texture2D, vector3 - Main.screenPosition + vector2, new Rectangle?(rectangle), Color.White, num2, new Vector2(rectangle.Width / 2, 0f), projectile.scale, 0, 0f);
                    num3 += rectangle.Height * projectile.scale;
                    vector3 += vector * rectangle.Height * projectile.scale;
                }
            }
            Vector2 vector4 = vector3;
            vector3 = projectile.Center.Floor();
            vector3 += vector * projectile.scale * 24f;
            rectangle = new Rectangle(0, 44, texture2D.Width, 18);//Middle by Grip
            int num4 = 18;
            if (flag)
            {
                num4 = 9;
            }
            float num5 = num;
            if (num > 0f)
            {
                float num6 = 0f;
                float num7 = num5 / num4;
                num6 += num7 * 0.25f;
                vector3 += vector * num7 * 0.25f;
                for (int i = 0; i < num4; i++)
                {
                    float num8 = num7;
                    if (i == 0)
                    {
                        num8 *= 0.75f;
                    }
                    Main.spriteBatch.Draw(texture2D, vector3 - Main.screenPosition + vector2, new Rectangle?(rectangle), Color.White, num2, new Vector2(rectangle.Width / 2, 0f), projectile.scale, 0, 0f);
                    num6 += num8;
                    vector3 += vector * num8;
                }
            }
            rectangle = new Rectangle(0, 88, texture2D.Width, 56);//Head
            Main.spriteBatch.Draw(texture2D, vector4 - Main.screenPosition + vector2, new Rectangle?(rectangle), Color.White, num2, texture2D.Frame(1, 1, 0, 0).Top(), projectile.scale, 0, 0f);
            return false;
        }
        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            float num = 1.57079637f;
            player.RotatedRelativePoint(player.MountedCenter, true);
            if (projectile.localAI[1] > 0f)
            {
                projectile.localAI[1] -= 1f;
            }
            projectile.alpha -= 42;
            if (projectile.alpha < 0)
            {
                projectile.alpha = 0;
            }
            if (projectile.localAI[0] == 0f)
            {
                projectile.localAI[0] = projectile.velocity.ToRotation();
            }
            float num2 = (projectile.localAI[0].ToRotationVector2().X >= 0f) ? 1 : -1;
            if (projectile.ai[1] <= 0f)
            {
                num2 *= -1f;
            }
            Vector2 vector = (num2 * (projectile.ai[0] / 30f * 6.28318548f - 1.57079637f)).ToRotationVector2();
            vector.Y *= (float)Math.Sin(projectile.ai[1]);
            if (projectile.ai[1] <= 0f)
            {
                vector.Y *= -1f;
            }
            vector = vector.RotatedBy(projectile.localAI[0], default(Vector2));
            projectile.ai[0] += 1f;
            if (projectile.ai[0] < 30f)
            {
                projectile.velocity += 48f * vector;
            }
            else
            {
                projectile.Kill();
            }
            projectile.position = player.RotatedRelativePoint(player.MountedCenter, true) - projectile.Size / 2f;
            projectile.rotation = projectile.velocity.ToRotation() + num;
            projectile.spriteDirection = projectile.direction;
            projectile.timeLeft = 2;
            player.ChangeDir(projectile.direction);
            player.heldProj = projectile.whoAmI;
            player.itemTime = 2;
            player.itemAnimation = 2;
            player.itemRotation = (float)Math.Atan2(projectile.velocity.Y * projectile.direction, projectile.velocity.X * projectile.direction);
            Vector2 vector2 = Main.OffsetsPlayerOnhand[player.bodyFrame.Y / 56] * 2f;
            if (player.direction != 1)
            {
                vector2.X = player.bodyFrame.Width - vector2.X;
            }
            if (player.gravDir != 1f)
            {
                vector2.Y = player.bodyFrame.Height - vector2.Y;
            }
            vector2 -= new Vector2(player.bodyFrame.Width - player.width, player.bodyFrame.Height - 42) / 2f;
            projectile.Center = player.RotatedRelativePoint(player.position + vector2, true) - projectile.velocity;
            if (projectile.alpha == 0)
            {
                for (int i = 0; i < 2; i++)
                {
                    Dust dust = Main.dust[Dust.NewDust(projectile.position + projectile.velocity * 2f, projectile.width, projectile.height, 172, 0f, 0f, 100, default(Color), 1.0f)];
                    dust.noGravity = true;
                    dust.velocity *= 2f;
                    dust.velocity += projectile.localAI[0].ToRotationVector2();
                    dust.fadeIn = 1.2f;
                    dust.shader = GameShaders.Armor.GetSecondaryShader(44, Main.LocalPlayer);
                }
                float num3 = 18f;
                int num4 = 0;
                while (num4 < num3)
                {
                    if (Main.rand.Next(4) == 0)
                    {
                        Vector2 position = projectile.position + projectile.velocity + projectile.velocity * (num4 / num3);
                        Dust dust = Main.dust[Dust.NewDust(position, projectile.width, projectile.height, 172, 0f, 0f, 100, default(Color), 1.2f)];
                        dust.noGravity = true;
                        dust.fadeIn = 0.5f;
                        dust.velocity += projectile.localAI[0].ToRotationVector2();
                        dust.noLight = true;
                        dust.shader = GameShaders.Armor.GetSecondaryShader(44, Main.LocalPlayer);
                    }
                    num4++;
                }
            }
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (projectile.penetrate == 1)
            {
                projectile.penetrate++;
            }
            target.AddBuff(mod.BuffType("Nightmare"), 300);
            if (projectile.localAI[1] <= 0f && projectile.owner == Main.myPlayer)
            {
                Projectile.NewProjectile(target.Center.X, target.Center.Y, 0f, 0f, mod.ProjectileType("NightmareBurst"), projectile.damage, projectile.knockBack, projectile.owner, target.whoAmI, 0f);
            }
            projectile.localAI[1] = 4f;
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(mod.BuffType("Nightmare"), 300);
            if (projectile.localAI[1] <= 0f && projectile.owner == Main.myPlayer)
            {
                Projectile.NewProjectile(target.Center.X, target.Center.Y, 0f, 0f, mod.ProjectileType("NightmareBurst"), projectile.damage, projectile.knockBack, projectile.owner, target.whoAmI, 1f);
            }
            projectile.localAI[1] = 4f;
        }
        public override void OnHitPvp(Player target, int damage, bool crit)
        {
            if (projectile.penetrate == 1)
            {
                projectile.penetrate++;
            }
            target.AddBuff(mod.BuffType("Nightmare"), 300);
            if (projectile.localAI[1] <= 0f && projectile.owner == Main.myPlayer)
            {
                Projectile.NewProjectile(target.Center.X, target.Center.Y, 0f, 0f, mod.ProjectileType("NightmareBurst"), projectile.damage, projectile.knockBack, projectile.owner, target.whoAmI, 1f);
            }
            projectile.localAI[1] = 4f;
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
                return true;
            }
            float num = 0f;
            if (Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), projectile.Center, projectile.Center + projectile.velocity, 16f * projectile.scale, ref num))
            {
                return true;
            }
            return false;
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(200, 200, 200, 200);
        }
    }
}