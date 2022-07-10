using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Projectiles.Thrown
{
    public class BrutalDagger : ModProjectile
	{
        public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Dagger");
		}
		public override void SetDefaults()
		{
            projectile.width = 12;
            projectile.height = 12;
            projectile.aiStyle = 2;
            aiType = 48;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.thrown = true;
            projectile.penetrate = 2;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 8;
            projectile.GetGlobalProjectile<SinsProjectile>().drawCenter = true;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.Bleeding, 120);
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(BuffID.Bleeding, 120);
        }
        public override void OnHitPvp(Player target, int damage, bool crit)
        {
            target.AddBuff(BuffID.Bleeding, 120);
        }
        public override void Kill(int timeLeft)
        {
            Main.PlaySound(SoundID.Dig, (int)projectile.position.X, (int)projectile.position.Y, 1, 1f, 0f);
            for (int num = 0; num < 10; num++)
            {
                int num2 = Dust.NewDust(projectile.position, projectile.width, projectile.height, 50, 0f, 0f, 100, default(Color), 0.5f);
                Dust dust = Main.dust[num2];
                dust.velocity.X = dust.velocity.X * 2f;
                Dust dust2 = Main.dust[num2];
                dust2.velocity.Y = dust2.velocity.Y * 2f;
            }
        }
    }
}