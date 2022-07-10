using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace SinsMod.Projectiles.Melee
{
	public class DaCapo : ModProjectile
	{
        public override string Texture => "SinsMod/Extra/Placeholder/BlankTex";
        public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Da Capo");
        }
		public override void SetDefaults()
		{
            projectile.aiStyle = -1;
            projectile.melee = true;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.width = 1;
			projectile.height = 1;
			projectile.penetrate = 5;
			projectile.timeLeft = 60;
			projectile.tileCollide = false;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 0;
        }
        public override bool PreAI()
        {
            projectile.velocity = Vector2.Zero;
            if (projectile.ai[1] != 1f ? Main.npc[(int)projectile.ai[0]].active && Main.npc[(int)projectile.ai[0]].life > 0 : Main.player[(int)projectile.ai[0]].active && !Main.player[(int)projectile.ai[0]].dead)
            {
                projectile.Center = projectile.ai[1] != 1f ? Main.npc[(int)projectile.ai[0]].Center : Main.player[(int)projectile.ai[0]].Center;
                if (projectile.ai[1] != 1f ? Main.npc[(int)projectile.ai[0]].life <= 0 || !Main.npc[(int)projectile.ai[0]].active : Main.player[(int)projectile.ai[0]].dead || !Main.player[(int)projectile.ai[0]].active)
                {
                    projectile.active = false;
                }
                return false;
            }
            projectile.active = false;
            return false;
        }
    }
}