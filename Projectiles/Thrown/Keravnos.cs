using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Projectiles.Thrown
{
    public class Keravnos : ModProjectile
	{
        public override string Texture => "SinsMod/Items/Weapons/Thrown/Keravnos";
        public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Keravnos");
        }
		public override void SetDefaults()
		{
            projectile.width = 24;
			projectile.height = 24;
            projectile.thrown = true;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.penetrate = -1;
            projectile.tileCollide = true;
            projectile.ignoreWater = true;
            projectile.extraUpdates = 1;
            projectile.scale = 1.2f;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 120;
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Main.spriteBatch.Draw(Main.projectileTexture[projectile.type], new Vector2(projectile.position.X - Main.screenPosition.X + (projectile.width / 2), projectile.position.Y - Main.screenPosition.Y + (projectile.height / 2)), new Rectangle?(new Rectangle(0, 0, Main.projectileTexture[projectile.type].Width, Main.projectileTexture[projectile.type].Height)), projectile.GetAlpha(lightColor), projectile.rotation, new Vector2(Main.projectileTexture[projectile.type].Width, 0f), projectile.scale, SpriteEffects.None, 0f);
            return false;
        }
        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            for (int i = 0; i < 2; i++)
            {
                float num = projectile.velocity.X / 3f * i;
                float num2 = projectile.velocity.Y / 3f * i;
                int num3 = Dust.NewDust(projectile.position, projectile.width, projectile.height, 229, 0f, 0f, 0, default(Color), 1f);
                Main.dust[num3].position.X = projectile.Center.X - num;
                Main.dust[num3].position.Y = projectile.Center.Y - num2;
                Main.dust[num3].noGravity = true;
                Main.dust[num3].velocity *= 0f;
            }
            projectile.rotation = (float)Math.Atan2(projectile.velocity.Y, projectile.velocity.X) + 0.785f;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.Electrified, 1200);
            Projectile.NewProjectile(target.Center.X, target.Center.Y, 0f, 0f, mod.ProjectileType("ThunderBurst"), (int)((double)projectile.damage / 2), 0, projectile.owner);
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(BuffID.Electrified, 1200);
            Projectile.NewProjectile(target.Center.X, target.Center.Y, 0f, 0f, mod.ProjectileType("ThunderBurst"), (int)((double)projectile.damage / 2), 0, projectile.owner);
        }
        public override void OnHitPvp(Player target, int damage, bool crit)
        {
            target.AddBuff(BuffID.Electrified, 1200);
            Projectile.NewProjectile(target.Center.X, target.Center.Y, 0f, 0f, mod.ProjectileType("ThunderBurst"), (int)((double)projectile.damage / 2), 0, projectile.owner);
        }
        public override void Kill(int timeLeft)
        {
            for (int num = 0; num < 20; num++)
            {
                float num2 = projectile.oldVelocity.X * (30f / num);
                float num3 = projectile.oldVelocity.Y * (30f / num);
                int num4 = Dust.NewDust(new Vector2(projectile.oldPosition.X - num2, projectile.oldPosition.Y - num3), 16, 16, 229, projectile.oldVelocity.X, projectile.oldVelocity.Y, 100, default(Color), 1.8f);
                Main.dust[num4].noGravity = true;
                Dust dust = Main.dust[num4];
                dust.velocity *= 0.5f;
                num4 = Dust.NewDust(new Vector2(projectile.oldPosition.X - num2, projectile.oldPosition.Y - num3), 16, 16, 229, projectile.oldVelocity.X, projectile.oldVelocity.Y, 100, default(Color), 1.4f);
                dust = Main.dust[num4];
                dust.velocity *= 0.05f;
                dust.noGravity = true;
            }
            Rectangle hitbox = projectile.Hitbox;
            for (int num = 0; num < 6; num += 3)
            {
                hitbox.X = (int)projectile.oldPos[num].X;
                hitbox.Y = (int)projectile.oldPos[num].Y;
                int num2;
                for (int num3 = 0; num3 < 5; num3 = num2 + 1)
                {
                    int num4 = Utils.SelectRandom<int>(Main.rand, new int[]
                    {
                        229
                    });
                    int num5 = Dust.NewDust(hitbox.TopLeft(), projectile.width, projectile.height, num4, 2.5f * projectile.direction, -2.5f, 0, default(Color), 1.2f);
                    Main.dust[num5].alpha = 200;
                    Dust dust = Main.dust[num5];
                    dust.velocity *= 2.4f;
                    dust = Main.dust[num5];
                    dust.scale += Main.rand.NextFloat();
                    num2 = num3;
                    dust.noGravity = true;
                }
            }
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White * (1f - projectile.alpha / 255f);
        }
    }
}