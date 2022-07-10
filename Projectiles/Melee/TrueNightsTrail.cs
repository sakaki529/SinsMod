using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Projectiles.Melee
{
    public class TrueNightsTrail : ModProjectile
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("True Night's Trail");
            ProjectileID.Sets.YoyosLifeTimeMultiplier[projectile.type] = 14f;
            ProjectileID.Sets.YoyosMaximumRange[projectile.type] = 224f;
            ProjectileID.Sets.YoyosTopSpeed[projectile.type] = 13.3f;
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
            if (Main.rand.Next(5) == 0)
            {
                Dust dust = Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, 27);
                dust.noGravity = true;
                dust.scale = 0.95f;
            }
            if (Main.rand.Next(5) == 0)
            {
                Dust dust = Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, 75);
                dust.noGravity = true;
                dust.scale = 1f;
            }
            if (projectile.localAI[1] > 0f)
            {
                projectile.localAI[1] -= 1f;
            }
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (projectile.localAI[1] <= 0f && projectile.owner == Main.myPlayer)
            {
                Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0f, 0f, mod.ProjectileType("SpreadShortcut"), projectile.damage, 1f, projectile.owner, mod.ProjectileType("TrueNightsBall"));
                projectile.localAI[1] = 20f;
            }
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            if (projectile.localAI[1] <= 0f && projectile.owner == Main.myPlayer)
            {
                Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0f, 0f, mod.ProjectileType("SpreadShortcut"), projectile.damage, 1f, projectile.owner, mod.ProjectileType("TrueNightsBall"));
                projectile.localAI[1] = 20f;
            }
        }
        public override void OnHitPvp(Player target, int damage, bool crit)
        {
            if (projectile.localAI[1] <= 0f && projectile.owner == Main.myPlayer)
            {
                Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0f, 0f, mod.ProjectileType("SpreadShortcut"), projectile.damage, 1f, projectile.owner, mod.ProjectileType("TrueNightsBall"));
                projectile.localAI[1] = 20f;
            }
        }
    }
}