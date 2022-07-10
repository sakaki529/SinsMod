using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace SinsMod.Projectiles.Summon
{
    public class MoonlightRay : ModProjectile
    {
        public override string Texture => "SinsMod/Extra/Placeholder/BlankTex";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Moonlight Ray");
        }
        public override void SetDefaults()
        {
            projectile.width = 4;
            projectile.height = 4;
            projectile.minion = true;
            projectile.minionSlots = 0f;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.tileCollide = true;
            projectile.ignoreWater = true;
            projectile.extraUpdates = 100;
            projectile.timeLeft = 100;
        }
        public override void AI()
        {
            for (int i = 0; i < 4; i++)
            {
                Vector2 vector = projectile.position;
                vector -= projectile.velocity * (i * 0.25f);
                projectile.alpha = 255;
                int num = Dust.NewDust(vector, 1, 1, 246, 0f, 0f, 0, default(Color), 1f);
                Main.dust[num].position = vector;
                Dust dust = Main.dust[num];
                dust.position.X = dust.position.X + projectile.width / 2;
                Dust dust2 = Main.dust[num];
                dust2.position.Y = dust2.position.Y + projectile.height / 2;
                Main.dust[num].scale = Main.rand.Next(70, 110) * 0.013f;
                Dust dust3 = Main.dust[num];
                dust3.velocity *= 0.2f;
            }
        }
    }
}