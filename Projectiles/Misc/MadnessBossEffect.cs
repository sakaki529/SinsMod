using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Projectiles.Misc
{
    public class MadnessBossEffect : ModProjectile
	{
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("");
            Main.projFrames[projectile.type] = 6;
		}
		public override void SetDefaults()
		{
            projectile.width = 120;
            projectile.height = 120;
            projectile.aiStyle = -1;
            projectile.melee = true;
            projectile.friendly = false;
            projectile.hostile = false;
			projectile.tileCollide = false;
            projectile.GetGlobalProjectile<SinsProjectile>().drawCenter = true;
        }
        public override bool? CanHitNPC(NPC target)
        {
            return false;
        }
        public override bool CanHitPlayer(Player target)
        {
            return false;
        }
        public override bool CanDamage()
        {
            return false;
        }
        public override bool PreAI()
        {
            projectile.frameCounter++;
            if (projectile.frameCounter > 3)
            {
                projectile.frameCounter = 0;
                projectile.frame++;
                if (projectile.frame >= Main.projFrames[projectile.type])
                {
                    projectile.Kill();
                }
            }
            if (projectile.localAI[0] == 0f)
            {
                //Main.PlaySound(SoundID.Item47, projectile.Center);
                projectile.localAI[0] = 1f;
            }
            return false;
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White * (1f - projectile.alpha / 255f);
        }
    }
}