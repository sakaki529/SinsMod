using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Projectiles.Thrown
{
    public class Mjolnir : ModProjectile
    {
        public override string Texture => "SinsMod/Items/Weapons/MultiType/Mjolnir";
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Mjolnir");
        }
		public override void SetDefaults()
		{
            projectile.width = 26;
			projectile.height = 26;
            projectile.aiStyle = -1;
            projectile.thrown = true;
            projectile.friendly = true;
            projectile.penetrate = -1;
			projectile.ignoreWater = true;
            projectile.tileCollide = true;
            projectile.netUpdate = true;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 10;
            projectile.extraUpdates = 1;
        }
        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            if (Main.rand.Next(2) == 0)
            {
                int d = Dust.NewDust(projectile.position, projectile.width, projectile.height, 57, 0f, 0f, 255, default(Color), 0.75f);
                Dust dust = Main.dust[d];
                dust.velocity *= 0.1f;
                Main.dust[d].noGravity = true;
            }
            float num = projectile.Center.X;
            float num2 = projectile.Center.Y;
            float num3 = 2200f;
            bool flag = false;
            for (int num4 = 0; num4 < 200; num4++)
            {
                if (Main.npc[num4].CanBeChasedBy(projectile, false) && projectile.localAI[0] == 0f && Collision.CanHit(projectile.Center, 1, 1, Main.npc[num4].Center, 1, 1))
                {
                    float num5 = Main.npc[num4].position.X + (Main.npc[num4].width / 2);
                    float num6 = Main.npc[num4].position.Y + (Main.npc[num4].height / 2);
                    float num7 = Math.Abs(projectile.position.X + (projectile.width / 2) - num5) + Math.Abs(projectile.position.Y + (projectile.height / 2) - num6);
                    if (num7 < num3)
                    {
                        num3 = num7;
                        num = num5;
                        num2 = num6;
                        flag = true;
                    }
                }
            }
            if (flag)
            {
                float num8 = 28f;
                Vector2 vector = new Vector2(projectile.position.X + projectile.width * 0.5f, projectile.position.Y + projectile.height * 0.5f);
                float num9 = num - vector.X;
                float num10 = num2 - vector.Y;
                float num11 = (float)Math.Sqrt(num9 * num9 + num10 * num10);
                num11 = num8 / num11;
                num9 *= num11;
                num10 *= num11;
                projectile.velocity.X = (projectile.velocity.X * 7f + num9) / 8f;
                projectile.velocity.Y = (projectile.velocity.Y * 7f + num10) / 8f;
            }
            projectile.alpha -= 15;
            if (projectile.alpha < 0)
            {
                projectile.alpha = 0;
            }
            if (projectile.soundDelay == 0)
            {
                projectile.soundDelay = 8;
                Main.PlaySound(SoundID.Item7, projectile.position);
            }
            if (projectile.localAI[0] == 0f)
            {
                projectile.localAI[1] += 1f;
                if ((projectile.localAI[1] >= 15f && !flag) || projectile.numHits > 0)
                {
                    projectile.localAI[0] = 1f;
                    projectile.localAI[1] = 0f;
                    projectile.netUpdate = true;
                }
            }
            else
            {
                projectile.tileCollide = false;
                float num12 = 28f;
                float num13 = 2f;
                Vector2 vector = new Vector2(projectile.position.X + projectile.width * 0.5f, projectile.position.Y + projectile.height * 0.5f);
                float num14 = Main.player[projectile.owner].position.X + (player.width / 2) - vector.X;
                float num15 = Main.player[projectile.owner].position.Y + (player.height / 2) - vector.Y;
                float num16 = (float)Math.Sqrt(num14 * num14 + num15 * num15);
                if (num16 > 3200f)
                {
                    projectile.Kill();
                }
                num16 = num12 / num16;
                num14 *= num16;
                num15 *= num16;
                if (projectile.velocity.X < num14)
                {
                    projectile.velocity.X = projectile.velocity.X + num13;
                    if (projectile.velocity.X < 0f && num14 > 0f)
                    {
                        projectile.velocity.X = projectile.velocity.X + num13;
                    }
                }
                else
                {
                    if (projectile.velocity.X > num14)
                    {
                        projectile.velocity.X = projectile.velocity.X - num13;
                        if (projectile.velocity.X > 0f && num14 < 0f)
                        {
                            projectile.velocity.X = projectile.velocity.X - num13;
                        }
                    }
                }
                if (projectile.velocity.Y < num15)
                {
                    projectile.velocity.Y = projectile.velocity.Y + num13;
                    if (projectile.velocity.Y < 0f && num15 > 0f)
                    {
                        projectile.velocity.Y = projectile.velocity.Y + num13;
                    }
                }
                else
                {
                    if (projectile.velocity.Y > num15)
                    {
                        projectile.velocity.Y = projectile.velocity.Y - num13;
                        if (projectile.velocity.Y > 0f && num15 < 0f)
                        {
                            projectile.velocity.Y = projectile.velocity.Y - num13;
                        }
                    }
                }
                if (Main.myPlayer == projectile.owner && projectile.Hitbox.Intersects(player.Hitbox))
                {
                    projectile.Kill();
                }
            }
            float dir = (projectile.direction <= 0) ? -1f : 1f;
            projectile.rotation += dir * (0.3f / (projectile.extraUpdates + 1));
            projectile.spriteDirection = projectile.velocity.X < 0f ? -1 : 1;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
            Projectile.NewProjectile(target.Center.X, target.Center.Y, 0f, 0f, mod.ProjectileType("Electricity"), projectile.damage, 0f, projectile.owner, target.whoAmI, 0);
        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            damage += target.defense / 2;
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            Projectile.NewProjectile(target.Center.X, target.Center.Y, 0f, 0f, mod.ProjectileType("Electricity"), projectile.damage, 0f, projectile.owner, target.whoAmI, 1);
        }
        public override void ModifyHitPlayer(Player target, ref int damage, ref bool crit)
        {
            damage += target.statDefense / 2;
        }
        public override void OnHitPvp(Player target, int damage, bool crit)
        {
            Projectile.NewProjectile(target.Center.X, target.Center.Y, 0f, 0f, mod.ProjectileType("Electricity"), projectile.damage, 0f, projectile.owner, target.whoAmI, 1);
        }
        public override void ModifyHitPvp(Player target, ref int damage, ref bool crit)
        {
            damage += target.statDefense / 2;
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (projectile.velocity.X != oldVelocity.X)
            {
                projectile.velocity.X = -oldVelocity.X;
            }
            if (projectile.velocity.Y != oldVelocity.Y)
            {
                projectile.velocity.Y = -oldVelocity.Y;
            }
            return false;
        }
    }
}