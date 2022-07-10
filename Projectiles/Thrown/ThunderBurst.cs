using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Projectiles.Thrown
{
    public class ThunderBurst : ModProjectile
	{
        public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Thunder");
            Main.projFrames[projectile.type] = 5;
        }
		public override void SetDefaults()
		{
            projectile.width = 160;
			projectile.height = 160;
            projectile.thrown = true;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.penetrate = -1;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 2;
            projectile.GetGlobalProjectile<SinsProjectile>().drawCenter = true;
        }
        public override bool PreAI()
        {
            projectile.velocity = Vector2.Zero;
            projectile.rotation = 0;
            if (++projectile.frameCounter >= 7)
            {
                projectile.frameCounter = 0;
                if (++projectile.frame >= 6)
                {
                    projectile.Kill();
                }
            }
            if (projectile.localAI[0] == 0)
            {
                Main.PlaySound(SoundID.NPCDeath56, projectile.Center);
                projectile.localAI[0] = 1;
            }
            return false;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.Electrified, 1200);
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(BuffID.Electrified, 1200);
        }
        public override void OnHitPvp(Player target, int damage, bool crit)
        {
            target.AddBuff(BuffID.Electrified, 1200);
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(255, 255, 255, 150) * (1f - projectile.alpha / 255f);
        }
    }
}