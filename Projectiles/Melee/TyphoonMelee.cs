using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Projectiles.Melee
{
    public class TyphoonMelee : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Typhoon");
            Main.projFrames[projectile.type] = 3;
        }
        public override void SetDefaults()
        {
            projectile.CloneDefaults(ProjectileID.Typhoon);
            projectile.magic = false;
            projectile.melee = true;
        }
        public override void AI()
        {
            projectile.type = ProjectileID.Typhoon;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            projectile.type = ProjectileID.Typhoon;
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            projectile.type = ProjectileID.Typhoon;
        }
        public override void OnHitPvp(Player target, int damage, bool crit)
        {
            projectile.type = ProjectileID.Typhoon;
        }
    }
}