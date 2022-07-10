using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Projectiles.Hostile
{
    public class SlothScythe : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Scythe");
        }
        public override void SetDefaults()
        {
            projectile.width = 48;
            projectile.height = 48;
            projectile.friendly = false;
            projectile.hostile = true;
            projectile.penetrate = -1;//5
            projectile.timeLeft = 300;
            projectile.tileCollide = true;
            projectile.ignoreWater = true;
            projectile.alpha = 100;
            projectile.scale = 0.9f;
            projectile.light = 0.2f;
            projectile.GetGlobalProjectile<SinsProjectile>().drawCenter = true;
        }
        public override void AI()
        {
            if (projectile.ai[0] == 1f)
            {
                projectile.magic = true;
                projectile.friendly = true;
                projectile.hostile = false;
                projectile.penetrate = 6;
            }
            if (projectile.localAI[1] == 0f)
            {
                projectile.localAI[1] = 1f;
                Main.PlaySound(SoundID.Item8, projectile.Center);
            }
            projectile.rotation += projectile.direction * 0.8f;
            projectile.localAI[0] += 1f;
            if (projectile.localAI[0] >= 30f)
            {
                if (projectile.localAI[0] < 100f)
                {
                    projectile.velocity *= 1.06f;
                }
                else
                {
                    projectile.localAI[0] = 200f;
                }
            }
            int num;
            for (int num2 = 0; num2 < 2; num2 = num + 1)
            {
                int num3 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 172, 0f, 0f, 100, default(Color), 1f);
                Main.dust[num3].noGravity = true;
                num = num2;
            }
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.immune[projectile.owner] = 6;
            target.AddBuff(BuffID.Slow, 240);
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(BuffID.Slow, 240);
        }
        public override void OnHitPvp(Player target, int damage, bool crit)
        {
            target.AddBuff(BuffID.Slow, 240);
        }
        public override void Kill(int timeLeft)
        {
            Main.PlaySound(SoundID.Item10, projectile.Center);
            int num;
            for (int num2 = 0; num2 < 30; num2 = num + 1)
            {
                int num3 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 172, projectile.velocity.X, projectile.velocity.Y, 100, default(Color), 1.7f);
                Main.dust[num3].noGravity = true;
                num3 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 172, projectile.velocity.X, projectile.velocity.Y, 100, default(Color), 1f);
                Main.dust[num3].noGravity = true;
                num = num2;
            }
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }
    }
}