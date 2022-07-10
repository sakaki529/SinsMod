using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Projectiles.Melee
{
    public class Nyarlathotep : ModProjectile
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Nyarlathotep");
            ProjectileID.Sets.YoyosLifeTimeMultiplier[projectile.type] = -1f;
            ProjectileID.Sets.YoyosMaximumRange[projectile.type] = 640;
            ProjectileID.Sets.YoyosTopSpeed[projectile.type] = 18.0f;
        }
        public override void SetDefaults()
        {
            projectile.width = 16;
            projectile.height = 16;
            projectile.melee = true;
            projectile.aiStyle = 99;
            projectile.friendly = true;
            //projectile.penetrate = -1;
            projectile.extraUpdates = 1;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 1;
        }
        public override void AI()
        {
            Vector2 vector = new Vector2(Main.rand.NextFloat(4f, 7f), Main.rand.NextFloat(4f, 7f)).RotatedByRandom(MathHelper.ToRadians(360));
            for (int i = 0; i < 200; i++)
            {
                if (Main.npc[i].CanBeChasedBy(projectile, false))
                {
                    if (Vector2.Distance(Main.npc[i].Center, projectile.Center) < 160f)
                    {
                        vector = Main.npc[i].Center - projectile.Center;
                        vector.Normalize();
                        vector *= Main.rand.NextFloat(5.5f, 7f);
                    }
                }
            }
            float num = Main.rand.Next(10, 50) * 0.001f;
            if (Main.rand.Next(2) == 0)
            {
                num *= -1f;
            }
            float num2 = Main.rand.Next(10, 50) * 0.001f;
            if (Main.rand.Next(2) == 0)
            {
                num2 *= -1f;
            }
            if (Main.rand.Next(15) == 0)//12
            {
                Projectile.NewProjectile(projectile.Center, vector, mod.ProjectileType("NyarlathotepTentacle"), projectile.damage, projectile.knockBack, projectile.owner, num, num2);
            }
            projectile.localAI[1] += 1f;
            if (projectile.localAI[1] >= 15f)//10
            {
                for (int j = 0; j < Main.rand.Next(1, 4); j++)
                {
                    Projectile.NewProjectile(projectile.Center, vector, mod.ProjectileType("NyarlathotepTentacle"), projectile.damage, projectile.knockBack, projectile.owner, num, num2);
                }
                projectile.localAI[1] = 0f;
            }
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (projectile.penetrate == 1)
            {
                projectile.penetrate++;
            }
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            if (projectile.penetrate == 1)
            {
                projectile.penetrate++;
            }
        }
        public override void OnHitPvp(Player target, int damage, bool crit)
        {
            if (projectile.penetrate == 1)
            {
                projectile.penetrate++;
            }
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White * (1f - projectile.alpha / 255f);
        }
    }
}