using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Projectiles.Melee
{
    public class Innocence : ModProjectile
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("The Innocence");
            ProjectileID.Sets.YoyosLifeTimeMultiplier[projectile.type] = 13f;
            ProjectileID.Sets.YoyosMaximumRange[projectile.type] = 240f;
            ProjectileID.Sets.YoyosTopSpeed[projectile.type] = 13.0f;
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
        }
    }
}