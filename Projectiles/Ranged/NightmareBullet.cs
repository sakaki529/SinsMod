using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Projectiles.Ranged
{
    public class NightmareBullet : ModProjectile
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Nightmare Bullet");
        }
        public override void SetDefaults()
		{
			projectile.width = 2;
			projectile.height = 2;
            projectile.aiStyle = 1;
            aiType = ProjectileID.Bullet;
            projectile.ranged = true;
			projectile.penetrate = 1;
            projectile.timeLeft = 180;
			projectile.friendly = true;
			projectile.tileCollide = true;
			projectile.ignoreWater = true;
            projectile.alpha = 255;
            projectile.extraUpdates = 2;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 60;
        }
		public override void AI()
		{
            Lighting.AddLight((int)((projectile.position.X + (projectile.width / 2)) / 16f), (int)((projectile.position.Y + (projectile.height / 2)) / 16f), 0.2f, 0.25f, 0.3f);
            projectile.localAI[0] += 1f;
            if (projectile.localAI[0] >= 6f)
            {
                for (int num = 0; num < 8; num++)
                {
                    float x = projectile.Center.X - projectile.velocity.X / 10f * num;
                    float y = projectile.Center.Y - projectile.velocity.Y / 10f * num;
                    Dust dust;
                    dust = Dust.NewDustPerfect(new Vector2(x, y), 172, new Vector2(0f, 0f), 0, new Color(255, 255, 255, 100), 0.5f);
                    dust.position.X = x;
                    dust.position.Y = y;
                    dust.noGravity = true;
                    dust.velocity *= 0f;
                    dust.alpha = projectile.alpha;
                    dust.shader = GameShaders.Armor.GetSecondaryShader(44, Main.LocalPlayer);
                }
            }
            if (projectile.alpha > 0)
            {
                //projectile.alpha -= 25;
            }
            if (projectile.alpha < 0)
            {
                projectile.alpha = 0;
            }
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (projectile.penetrate == 1)
            {
                projectile.penetrate++;
            }
            target.AddBuff(mod.BuffType("Nightmare"), 30);
            if (Main.rand.Next(4) == 0)
            {
                Projectile.NewProjectile(target.Center.X, target.Center.Y, 0f, 0f, mod.ProjectileType("NigthmareExplosion"), projectile.damage / 4, projectile.knockBack, projectile.owner, 0f, 0f);
            }
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(mod.BuffType("Nightmare"), 30);
            if (Main.rand.Next(4) == 0)
            {
                Projectile.NewProjectile(target.Center.X, target.Center.Y, 0f, 0f, mod.ProjectileType("NigthmareExplosion"), projectile.damage / 4, projectile.knockBack, projectile.owner, 0f, 0f);
            }
        }
        public override void OnHitPvp(Player target, int damage, bool crit)
        {
            if (projectile.penetrate == 1)
            {
                projectile.penetrate++;
            }
            target.AddBuff(mod.BuffType("Nightmare"), 30);
            if (Main.rand.Next(4) == 0)
            {
                Projectile.NewProjectile(target.Center.X, target.Center.Y, 0f, 0f, mod.ProjectileType("NigthmareExplosion"), projectile.damage / 4, projectile.knockBack, projectile.owner, 0f, 0f);
            }
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0f, 0f, mod.ProjectileType("NigthmareExplosion"), projectile.damage / 5, projectile.knockBack, projectile.owner, 0f, 0f);
            return base.OnTileCollide(oldVelocity);
        }
        public override void Kill(int timeLeft)
        {
            Main.PlaySound(SoundID.Item10, projectile.Center);
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White * (1f - projectile.alpha / 255f);
        }
    }
}