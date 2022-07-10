using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Projectiles.Melee
{
    public class NightmareBurst : ModProjectile
	{
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Burst");
		}
		public override void SetDefaults()
		{
            projectile.width = 60;
            projectile.height = 60;
            projectile.aiStyle = -1;
            projectile.melee = true;
            projectile.friendly = true;
            projectile.hostile = false;
			//projectile.penetrate = -1;
			projectile.timeLeft = 30;
			projectile.tileCollide = false;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 0;
        }
        public override void AI()
        {
            projectile.rotation += 0.5f;
            if (projectile.localAI[0] == 0f)
            {
                Main.PlaySound(SoundID.Item62, projectile.Center);
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
            if (projectile.penetrate == 1)
            {
                projectile.penetrate++;
            }
            target.AddBuff(mod.BuffType("Nightmare"), 600);
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(mod.BuffType("Nightmare"), 600);
        }
        public override void OnHitPvp(Player target, int damage, bool crit)
        {
            if (projectile.penetrate == 1)
            {
                projectile.penetrate++;
            }
            target.AddBuff(mod.BuffType("Nightmare"), 600);
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White * (1f - projectile.alpha / 255f);
        }
    }
}