using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace SinsMod.Projectiles.Ranged
{
    public class Sakurajima2 : ModProjectile
	{
        public override string Texture => "SinsMod/Extra/Placeholder/BlankTex";
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Sakurazima");
            DisplayName.AddTranslation(GameCulture.Chinese, "÷“‡");
        }
		public override void SetDefaults()
		{
            projectile.aiStyle = -1;
            projectile.ranged = true;
            projectile.friendly = true;
            projectile.hostile = false;
			projectile.width = 1;
			projectile.height = 1;
			projectile.penetrate = 1;
			projectile.timeLeft = 1;
			projectile.tileCollide = false;
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
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            damage += target.defense / 2;
        }
        public override void ModifyHitPlayer(Player target, ref int damage, ref bool crit)
        {
            damage += target.statDefense / 2;
        }
        public override void ModifyHitPvp(Player target, ref int damage, ref bool crit)
        {
            damage += target.statDefense / 2;
        }
    }
}
