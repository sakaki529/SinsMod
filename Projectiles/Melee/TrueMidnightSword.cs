using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Projectiles.Melee
{
	public class TrueMidnightSword : ModProjectile
	{
        public override string Texture => "SinsMod/Extra/Placeholder/BlankTex";
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("True Midnight Wave");
		}
		public override void SetDefaults()
		{
            projectile.aiStyle = -1;
            projectile.melee = true;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.width = 1;
			projectile.height = 1;
			projectile.penetrate = 1;
			projectile.timeLeft = 1;
			projectile.tileCollide = false;
            projectile.alpha = 255;
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
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(mod.BuffType("AbyssalFlame"), 120);
            target.AddBuff(BuffID.Bleeding, 180);
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(mod.BuffType("AbyssalFlame"), 120);
            target.AddBuff(BuffID.Bleeding, 180);
        }
        public override void OnHitPvp(Player target, int damage, bool crit)
        {
            target.AddBuff(mod.BuffType("AbyssalFlame"), 120);
            target.AddBuff(BuffID.Bleeding, 180);
        }
    }
}