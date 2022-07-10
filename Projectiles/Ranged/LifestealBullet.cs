using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Projectiles.Ranged
{
    public class LifestealBullet : ModProjectile
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Lifesteal Bullet");
        }
        public override void SetDefaults()
		{
			projectile.width = 2;
			projectile.height = 2;
            projectile.aiStyle = 1;
            aiType = ProjectileID.Bullet;
            projectile.timeLeft = 180;
            projectile.ranged = true;
			projectile.penetrate = 1;
			projectile.friendly = true;
			projectile.tileCollide = true;
			projectile.ignoreWater = false;
            projectile.extraUpdates = 1;
            projectile.alpha = 255;
        }
		public override void AI()
		{
            Lighting.AddLight((int)((projectile.position.X + (projectile.width / 2)) / 16f), (int)((projectile.position.Y + (projectile.height / 2)) / 16f), 0.5f, 0.35f, 0.05f);
            projectile.localAI[0] += 1f;
            if (projectile.localAI[0] >= 6f)
            {
                for (int num = 0; num < 7; num++)
                {
                    float x = projectile.Center.X - projectile.velocity.X / 10f * num;
                    float y = projectile.Center.Y - projectile.velocity.Y / 10f * num;
                    int num164 = Dust.NewDust(new Vector2(x, y), 1, 1, 182, 0f, 0f, 0, default(Color), 0.4f);
                    Main.dust[num164].alpha = projectile.alpha;
                    Main.dust[num164].position.X = x;
                    Main.dust[num164].position.Y = y;
                    Main.dust[num164].velocity *= 0f;
                    Main.dust[num164].noGravity = true;
                }
            }
            if (projectile.alpha > 0)
            {
                projectile.alpha -= 25;
            }
            if (projectile.alpha < 0)
            {
                projectile.alpha = 0;
            }
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (!Main.player[projectile.owner].moonLeech)
            {
                if (target.type != NPCID.TargetDummy)
                {
                    if (Main.rand.Next(5) == 0)
                    {
                        float num = 1 + Main.rand.Next(0, 2);
                        if (Main.player[Main.myPlayer].lifeSteal <= 0f)
                        {
                            return;
                        }
                        Main.player[Main.myPlayer].lifeSteal -= num;
                        int num2 = projectile.owner;
                        Projectile.NewProjectile(target.Center.X, target.Center.Y, 0f, 0f, mod.ProjectileType("RedHeal"), 0, 0f, projectile.owner, num2, num);
                    }
                }
            }
        }
        public override void OnHitPvp(Player target, int damage, bool crit)
		{
            if (!Main.player[projectile.owner].moonLeech)
            {
                if (Main.rand.Next(5) == 0)
                {
                    float num = 1 + Main.rand.Next(0, 2);
                    if (Main.player[Main.myPlayer].lifeSteal <= 0f)
                    {
                        return;
                    }
                    Main.player[Main.myPlayer].lifeSteal -= num;
                    int num2 = projectile.owner;
                    Projectile.NewProjectile(target.Center.X, target.Center.Y, 0f, 0f, mod.ProjectileType("RedHeal"), 0, 0f, projectile.owner, num2, num);
                }
            }
		}
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Collision.HitTiles(projectile.position, oldVelocity, projectile.width, projectile.height);
            return base.OnTileCollide(oldVelocity);
        }
        public override void Kill(int timeLeft)
        {
            Main.PlaySound(SoundID.Item10, projectile.position);
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White * (1f - projectile.alpha / 255f);
        }
    }
}