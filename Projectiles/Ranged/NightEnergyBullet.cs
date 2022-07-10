using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Projectiles.Ranged
{
    public class NightEnergyBullet : ModProjectile
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Night Energy Bullet");
        }
        public override void SetDefaults()
		{
			projectile.width = 2;
			projectile.height = 2;
            projectile.aiStyle = 1;
            aiType = ProjectileID.Bullet;
            projectile.ranged = true;
			projectile.penetrate = 3;
            projectile.timeLeft = 180;
			projectile.friendly = true;
			projectile.tileCollide = true;
			projectile.ignoreWater = true;
            projectile.alpha = 255;
            projectile.extraUpdates = 3;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = -1;
            projectile.scale = 1.25f;
        }
		public override void AI()
		{
            Lighting.AddLight((int)((projectile.position.X + (projectile.width / 2)) / 16f), (int)((projectile.position.Y + (projectile.height / 2)) / 16f), 0.05f, 0.05f, 0.05f);
            if (projectile.alpha > 0)
            {
                projectile.alpha -= 25;
            }
            if (projectile.alpha < 0)
            {
                projectile.alpha = 0;
            }
            Rectangle hitbox = projectile.Hitbox;
            hitbox.Offset((int)projectile.velocity.X, (int)projectile.velocity.Y);
            bool flag = false;
            for (int i = 0; i < Main.maxNPCs; i++)
            {
                NPC nPC = Main.npc[i];
                if (nPC.active && !nPC.dontTakeDamage && nPC.immune[projectile.owner] == 0 && projectile.localNPCImmunity[i] == 0 && nPC.Hitbox.Intersects(hitbox) && !nPC.friendly)
                {
                    flag = true;
                    break;
                }
            }
            if (flag)
            {
                int num = Main.rand.Next(15, 31);
                for (int i = 0; i < num; i++)
                {
                    int num2 = Dust.NewDust(projectile.Center, 0, 0, 186, 0f, 0f, 100, default(Color), 0.8f);//229
                    Main.dust[num2].velocity *= 1.6f;
                    Main.dust[num2].shader = GameShaders.Armor.GetSecondaryShader(56, Main.LocalPlayer);
                    Dust dust = Main.dust[num2];
                    dust.velocity.Y = dust.velocity.Y - 1f;
                    Main.dust[num2].velocity += projectile.velocity;
                    Main.dust[num2].noGravity = true;
                }
            }
        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            damage /= 3;
            target.immune[projectile.owner] = 0;
        }
        public override void ModifyHitPlayer(Player target, ref int damage, ref bool crit)
        {
            damage /= 3;
        }
        public override void ModifyHitPvp(Player target, ref int damage, ref bool crit)
        {
            damage /= 3;
        }
        public override void Kill(int timeLeft)
        {
            Main.PlaySound(SoundID.Item10, projectile.Center);
            for (int i = 0; i < 10; i++)
            {
                int num = Dust.NewDust(projectile.position, projectile.width, projectile.height, 186, 0f, 0f, 100, default(Color), 1f);
                Main.dust[num].scale = Main.rand.Next(1, 10) * 0.1f;
                Main.dust[num].noGravity = true;
                //Main.dust[num].fadeIn = 1.5f;
                Main.dust[num].shader = GameShaders.Armor.GetSecondaryShader(56, Main.LocalPlayer);
                Dust dust = Main.dust[num];
                dust.velocity *= 0.75f;
                dust.shader = GameShaders.Armor.GetSecondaryShader(56, Main.LocalPlayer);
            }
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White * (1f - projectile.alpha / 255f);
        }
    }
}