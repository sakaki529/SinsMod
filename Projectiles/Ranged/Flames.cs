using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Projectiles.Ranged
{
    public class Flames : ModProjectile
    {
        public override string Texture => "SinsMod/Extra/Placeholder/BlankTex";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Flames");
        }
        public override void SetDefaults()
        {
            projectile.CloneDefaults(ProjectileID.Flames);
            projectile.ranged = true;
            projectile.friendly = true;
            projectile.tileCollide = true;
            projectile.penetrate = -1;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 30;
        }
        public override void AI()
        {
            projectile.type = ProjectileID.Flames;
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
        public override void Kill(int timeleft)
        {
            projectile.CloneDefaults(ProjectileID.Flames);
        }
    }
}