using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Projectiles.Melee
{
    public class ExplosionSound : ModProjectile
	{
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Sound");
            Main.projFrames[projectile.type] = 6;
		}
		public override void SetDefaults()
		{
            projectile.width = 120;
            projectile.height = 120;
            projectile.aiStyle = -1;
            projectile.melee = true;
            projectile.friendly = true;
            projectile.hostile = false;
			projectile.tileCollide = false;
            projectile.scale = 0.5f;
            projectile.GetGlobalProjectile<SinsProjectile>().drawCenter = true;
        }
        public override void AI()
        {
            projectile.frameCounter++;
            if (projectile.frameCounter > 2)
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
            if (projectile.ai[1] != 1f ? Main.npc[(int)projectile.ai[0]].active && Main.npc[(int)projectile.ai[0]].life > 0 : Main.player[(int)projectile.ai[0]].active && !Main.player[(int)projectile.ai[0]].dead)
            {
                projectile.Center = projectile.ai[1] != 1f ? Main.npc[(int)projectile.ai[0]].Center : Main.player[(int)projectile.ai[0]].Center;
                if (projectile.ai[1] != 1f ? Main.npc[(int)projectile.ai[0]].life <= 0 || !Main.npc[(int)projectile.ai[0]].active : Main.player[(int)projectile.ai[0]].dead || !Main.player[(int)projectile.ai[0]].active)
                {
                    projectile.ai[1] = -1;
                }
                return;
            }
            projectile.ai[1] = -1;
        }
		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
            target.immune[projectile.owner] = 3;
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