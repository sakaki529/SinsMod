using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Projectiles.Melee
{
    public class DarkOne : ModProjectile
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Nyarlathotep");
            ProjectileID.Sets.YoyosLifeTimeMultiplier[projectile.type] = 13f;
            ProjectileID.Sets.YoyosMaximumRange[projectile.type] = 240;
            ProjectileID.Sets.YoyosTopSpeed[projectile.type] = 14.0f;
        }
        public override void SetDefaults()
        {
            projectile.width = 16;
            projectile.height = 16;
            projectile.melee = true;
            projectile.aiStyle = 99;
            projectile.friendly = true;
            projectile.tileCollide = false;
            //projectile.penetrate = -1;
            projectile.extraUpdates = 0;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 1;
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