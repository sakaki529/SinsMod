using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SinsMod.Projectiles.Magic
{
    public class FrostShard : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Frost Shard");
            Main.projFrames[projectile.type] = 5;
        }
        public override void SetDefaults()
        {
            projectile.width = 12;
            projectile.height = 12;
            projectile.magic = true;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.penetrate = 7;
            projectile.tileCollide = true;
            projectile.ignoreWater = false;
            projectile.coldDamage = true;
            projectile.alpha = 255;
            projectile.GetGlobalProjectile<SinsProjectile>().drawCenter = true;
        }
        public override void AI()
        {
            projectile.frame = (int)projectile.ai[0];
            projectile.spriteDirection = projectile.direction;
            projectile.rotation = projectile.velocity.ToRotation() + 1.57079637f;
            if (projectile.alpha > 0)
            {
                projectile.alpha -= 50;
            }
            if (projectile.alpha < 0)
            {
                projectile.alpha = 0;
            }
            if (projectile.alpha == 0)
            {
                int num = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 76, projectile.velocity.X, projectile.velocity.Y, 100, default(Color), 1.2f);
                Main.dust[num].noGravity = true;
                Main.dust[num].velocity *= 0.3f;
                Main.dust[num].velocity -= projectile.velocity * 0.4f;
            }
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.Frostburn, 240);
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(BuffID.Frostburn, 240);
        }
        public override void OnHitPvp(Player target, int damage, bool crit)
        {
            target.AddBuff(BuffID.Frostburn, 240);
        }
        public override void Kill(int timeLeft)
        {
            Main.PlaySound(SoundID.Item27, projectile.Center);
            for (int i = 0; i < 3; i++)
            {
                int num = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 76, 0f, 0f, 0, default(Color), 1f);
                Main.dust[num].noGravity = true;
                Main.dust[num].noLight = true;
                Main.dust[num].scale = 0.7f;
            }
        }
        public override Color? GetAlpha(Color lightColor)
        {
            if (projectile.alpha > 0)
            {
                return Color.Transparent;
            }
            return new Color(255, 255, 255, 0);
        }
    }
}