using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;

namespace SinsMod.Projectiles.Magic
{
    public class AstralThread : ModProjectile
    {
        public override string Texture => "SinsMod/Extra/Placeholder/BlankTex";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Astral Thread");
        }
        public override void SetDefaults()
        {
            projectile.width = 4;
            projectile.height = 4;
            projectile.magic = true;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
            projectile.penetrate = -1;
            projectile.extraUpdates = 100;
            projectile.timeLeft = 42;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 5;
        }
        public override void AI()
        {
            projectile.localAI[0] += 1f;
            if (projectile.localAI[0] > 1f)
            {
                int num;
                for (int num2 = 0; num2 < 4; num2 = num + 1)
                {
                    Vector2 vector = projectile.position;
                    vector -= projectile.velocity * (num2 * 0.25f);
                    projectile.alpha = 255;
                    int num3 = Dust.NewDust(vector, 1, 1, 172, 0f, 0f, 0, default(Color), 0.75f);
                    Main.dust[num3].shader = GameShaders.Armor.GetSecondaryShader(44, Main.LocalPlayer);
                    Main.dust[num3].position = vector;
                    Main.dust[num3].noGravity = true;
                    Dust dust = Main.dust[num3];
                    dust.velocity *= 0.2f;
                    num = num2;
                }
                return;
            }
        }
    }
}