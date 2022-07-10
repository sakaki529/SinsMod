using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Projectiles.Melee
{
    public class NightsTrail : ModProjectile
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Night's Trail");
            ProjectileID.Sets.YoyosLifeTimeMultiplier[projectile.type] = 9.5f;
            ProjectileID.Sets.YoyosMaximumRange[projectile.type] = 192f;
            ProjectileID.Sets.YoyosTopSpeed[projectile.type] = 12.0f;
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
                Dust dust = Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, 27);
                dust.noGravity = true;
                dust.scale = 0.95f;
            }
        }
    }
}