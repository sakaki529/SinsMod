using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Projectiles.Melee
{
    public class NightfallEnergy : ModProjectile
    {
        public override string Texture => "SinsMod/Extra/Placeholder/BlankTex";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Nightfall Energy");
        }
        public override void SetDefaults()
        {
            projectile.ranged = true;
            projectile.aiStyle = -1;
            projectile.width = 4;
            projectile.height = 4;
            projectile.penetrate = 1;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
            projectile.timeLeft = 120;
            projectile.extraUpdates = 2;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 0;
            projectile.netUpdate = true;
        }
        public override void AI()
        {
            float num = projectile.Center.X;
            float num2 = projectile.Center.Y;
            float num3 = 600f;
            bool flag = false;
            for (int num4 = 0; num4 < 200; num4++)
            {
                if (Main.npc[num4].CanBeChasedBy(projectile, false))
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
                float num8 = 30f;
                Vector2 vector = new Vector2(projectile.position.X + projectile.width * 0.5f, projectile.position.Y + projectile.height * 0.5f);
                float num9 = num - vector.X;
                float num10 = num2 - vector.Y;
                float num11 = (float)Math.Sqrt(num9 * num9 + num10 * num10);
                num11 = num8 / num11;
                num9 *= num11;
                num10 *= num11;
                projectile.velocity.X = (projectile.velocity.X * 20f + num9) / 21f;
                projectile.velocity.Y = (projectile.velocity.Y * 20f + num10) / 21f;
            }
            for (int i = 0; i < 10; i++)
            {
                float x = projectile.Center.X - projectile.velocity.X / 10f * i;
                float y = projectile.Center.Y - projectile.velocity.Y / 10f * i;
                int dust = Dust.NewDust(new Vector2(x, y), 1, 1, 174, 0f, 0f, 0, default(Color), 0.5f);
                Main.dust[dust].alpha = projectile.alpha;
                Main.dust[dust].position.X = x;
                Main.dust[dust].position.Y = y;
                Main.dust[dust].velocity *= 0f;
                Main.dust[dust].noGravity = true;
            }
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0f, 0f, mod.ProjectileType("NightfallExplosion"), projectile.damage / 2, projectile.knockBack, projectile.owner, 0f, 1f);
            target.AddBuff(BuffID.Daybreak, 30);
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0f, 0f, mod.ProjectileType("NightfallExplosion"), projectile.damage / 2, projectile.knockBack, projectile.owner, 0f, 1f);
            target.AddBuff(BuffID.Daybreak, 30);
        }
        public override void OnHitPvp(Player target, int damage, bool crit)
        {
            Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0f, 0f, mod.ProjectileType("NightfallExplosion"), projectile.damage / 2, projectile.knockBack, projectile.owner, 0f, 1f);
            target.AddBuff(BuffID.Daybreak, 30);
        }
    }
}