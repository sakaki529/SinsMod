using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Projectiles.Melee
{
    public class Salamander : ModProjectile
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Salamander");
            ProjectileID.Sets.YoyosLifeTimeMultiplier[projectile.type] = 7.5f;
            ProjectileID.Sets.YoyosMaximumRange[projectile.type] = 176f;
            ProjectileID.Sets.YoyosTopSpeed[projectile.type] = 11.0f;
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
        public override void PostAI()
        {
            if (Main.rand.Next(4) == 0)
            {
                Dust dust = Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, 6);
                dust.noGravity = true;
                dust.scale = 0.9f;
            }
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.OnFire, 180);
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(BuffID.OnFire, 180);
        }
        public override void OnHitPvp(Player target, int damage, bool crit)
        {
            target.AddBuff(BuffID.OnFire, 180);
        }
    }
}