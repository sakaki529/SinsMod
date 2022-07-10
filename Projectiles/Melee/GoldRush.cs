using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace SinsMod.Projectiles.Melee
{
    public class GoldRush : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Gold Rush");
		}
		public override void SetDefaults()
		{
            projectile.width = 30;
			projectile.height = 30;
            projectile.melee = true;
            projectile.friendly = true;
			projectile.hostile = false;
            projectile.penetrate = -1;
            projectile.ignoreWater = true;
            projectile.tileCollide = false;
            projectile.hide = true;
            projectile.ownerHitCheck = true;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 0;
        }
        public override void AI()
        {
            if (projectile.ai[0] != -1)
            {
                Main.player[projectile.owner].direction = projectile.direction;
                projectile.spriteDirection = projectile.direction;
                Main.player[projectile.owner].heldProj = projectile.whoAmI;
                Main.player[projectile.owner].itemTime = Main.player[projectile.owner].itemAnimation;
                projectile.position.X = Main.player[projectile.owner].position.X + (float)(Main.player[projectile.owner].width / 2) - (float)(projectile.width / 2);
                projectile.position.Y = Main.player[projectile.owner].position.Y + (float)(Main.player[projectile.owner].height / 2) - (float)(projectile.height / 2);
                projectile.position += projectile.velocity * projectile.localAI[0];
                if (Main.player[projectile.owner].itemAnimation < Main.player[projectile.owner].itemAnimationMax / 2)
                {
                    projectile.localAI[0] -= 0.5f;
                    /*if (!ShotProjectile)
                    {
                        Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, projectile.oldVelocity.X, projectile.oldVelocity.Y, mod.ProjectileType("AsteroidFist2"), (int)((double)projectile.damage * 1.0), (float)((int)((double)projectile.knockBack * 1.0)), projectile.owner, 0f, 0f);
                        Main.PlaySound(SoundID.DD2_FlameburstTowerShot, (int)projectile.position.X, (int)projectile.position.Y);
                        ShotProjectile = true;
                    }*/
                }
                else
                {
                    projectile.localAI[0] += 0.5f;
                }
                if (Main.player[projectile.owner].itemAnimation == 0)
                {
                    projectile.Kill();
                }
                projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;
                return;
            }
            Vector2 vector = Main.player[projectile.owner].RotatedRelativePoint(Main.player[projectile.owner].MountedCenter, true);
            if (Main.player[projectile.owner].channel)
            {
                float num = Main.player[projectile.owner].inventory[Main.player[projectile.owner].selectedItem].shootSpeed * projectile.scale;
                Vector2 vector2 = vector;
                float num2 = Main.mouseX + Main.screenPosition.X - vector2.X;
                float num3 = Main.mouseY + Main.screenPosition.Y - vector2.Y;
                if (Main.player[projectile.owner].gravDir == -1f)
                {
                    num3 = Main.screenHeight - Main.mouseY + Main.screenPosition.Y - vector2.Y;
                }
                float num4 = (float)Math.Sqrt(num2 * num2 + num3 * num3);
                num4 = (float)Math.Sqrt(num2 * num2 + num3 * num3);
                num4 = num / num4;
                num2 *= num4;
                num3 *= num4;
                if (num2 != projectile.velocity.X || num3 != projectile.velocity.Y)
                {
                    projectile.netUpdate = true;
                }
                projectile.velocity.X = num2;
                projectile.velocity.Y = num3;
            }
            else
            {
                projectile.Kill();
            }
            if (projectile.velocity.X > 0f)
            {
                Main.player[projectile.owner].ChangeDir(1);
            }
            else
            {
                if (projectile.velocity.X < 0f)
                {
                    Main.player[projectile.owner].ChangeDir(-1);
                }
            }
            projectile.spriteDirection = projectile.direction;
            Main.player[projectile.owner].ChangeDir(projectile.direction);
            Main.player[projectile.owner].heldProj = projectile.whoAmI;
            Main.player[projectile.owner].itemTime = 2;
            Main.player[projectile.owner].itemAnimation = 2;
            projectile.position.X = vector.X - (projectile.width / 2);
            projectile.position.Y = vector.Y - (projectile.height / 2) + 4;
            projectile.rotation = (float)(Math.Atan2(projectile.velocity.Y, projectile.velocity.X) + 1.5700000524520874);
            if (Main.player[projectile.owner].direction == 1)
            {
                Main.player[projectile.owner].itemRotation = (float)Math.Atan2(projectile.velocity.Y * projectile.direction, projectile.velocity.X * projectile.direction);
            }
            else
            {
                Main.player[projectile.owner].itemRotation = (float)Math.Atan2(projectile.velocity.Y * projectile.direction, projectile.velocity.X * projectile.direction);
            }
            projectile.velocity.X = projectile.velocity.X * (1f + Main.rand.Next(-3, 4) * 0.01f);

            int num5 = Dust.NewDust(projectile.position, projectile.width, projectile.height, 57, 0f, 0f, 255, default(Color), 0.75f);
            Dust dust = Main.dust[num5];
            dust.velocity *= 0.1f;
            Main.dust[num5].noGravity = true;

            Player player = Main.player[projectile.owner];
            float mag = 11f;
            player.velocity = mag * player.DirectionTo(Main.MouseWorld);
            
            player.noFallDmg = true;
            player.blackBelt = true;
            player.statDefense += 40;
            player.longInvince = true;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.Midas, 3600);
            Main.PlaySound(SoundID.Item1, target.Center);
        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            damage += target.defense / 2;
        }
        public override void OnHitPvp(Player target, int damage, bool crit)
        {
            target.AddBuff(BuffID.Midas, 3600);
            Main.PlaySound(SoundID.Item1, target.Center);
        }
        public override void ModifyHitPvp(Player target, ref int damage, ref bool crit)
        {
            damage += target.statDefense / 2;
        }
    }
}