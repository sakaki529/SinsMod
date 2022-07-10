using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Projectiles.Melee
{
    public class TrueInnocence : ModProjectile
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("True The Innocence");
            ProjectileID.Sets.YoyosLifeTimeMultiplier[projectile.type] = 16f;
            ProjectileID.Sets.YoyosMaximumRange[projectile.type] = 272f;
            ProjectileID.Sets.YoyosTopSpeed[projectile.type] = 14.2f;
        }
        public override void SetDefaults()
        {
            projectile.extraUpdates = 0;
            projectile.width = 16;
            projectile.height = 16;
            projectile.aiStyle = 99;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.melee = true;
            projectile.scale = 1f;
        }
        public override void AI()
        {
            if (Main.rand.Next(4) == 0)
            {
                Dust dust = Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, 57);
                dust.noGravity = true;
                dust.scale = 0.95f;
            }
            projectile.localAI[1] += 1f;
            if (projectile.localAI[1] >= 18f)
            {
                Vector2 shootVel = projectile.velocity;
                Vector2 randOffset = new Vector2(Main.rand.Next(-100, 101), Main.rand.Next(-100, 101));
                randOffset.Normalize();
                randOffset *= Main.rand.Next(10, 61) * 0.1f;
                if (Main.rand.Next(3) == 0)
                {
                    randOffset *= 2f;
                }
                shootVel *= 0.25f;
                shootVel += randOffset;
                shootVel *= 0.6f;
                Projectile.NewProjectile(projectile.Center - shootVel, shootVel, mod.ProjectileType("TrueInnocenceBall"), projectile.damage, projectile.knockBack, projectile.owner, Main.rand.Next(2), 0f);
                projectile.localAI[1] = 0f;
            }
        }
    }
}